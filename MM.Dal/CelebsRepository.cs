using Microsoft.Extensions.Logging;
using MM.Common;
using MM.Common.Models;
using MM.Common.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;

namespace MM.Dal
{
    public class CelebsRepository
    {
        private static ILogger                      Logger                  { get; set; }
        private static string                       celebs_json_path            = "celebs_data.json";
        private static string                       celebs_json_backup_path     = "celebs_data.backup.json";
        private static ReaderWriterLock             readerWriterLock            = new ReaderWriterLock();           //https://docs.microsoft.com/en-us/dotnet/api/system.threading.readerwriterlock?view=netcore-3.1
        private static int                          lockTimeout_in_ms           = 600000;


        static CelebsRepository()
        {

            Logger                  = LogManager.CreateLogger<CelebsRepository>();
            if (File.Exists(celebs_json_path) == false)
            {
                ResetData();      //data was never read from imdb
            }
        }

        public IEnumerable<Celeb> GetAll()
        {
            String celebsJson = string.Empty;

            try
            {
                readerWriterLock.AcquireReaderLock(lockTimeout_in_ms);
                celebsJson = File.ReadAllText(celebs_json_path);
            }
            finally
            {
                readerWriterLock.ReleaseReaderLock();
            }

            var result      = JsonConvert.DeserializeObject<IEnumerable<Celeb>>(celebsJson);

            return result;
        }

        public bool Delete(int index)
        {
            var celebs = GetAll().ToList();

            var celeb = celebs.Where(c => c.Index == index).FirstOrDefault();
            if (celeb != null)
            {
                celebs.Remove(celeb);
                try
                {
                    readerWriterLock.AcquireWriterLock(lockTimeout_in_ms);
                    File.WriteAllText(celebs_json_path, JsonConvert.SerializeObject(celebs));
                }
                finally
                {
                    readerWriterLock.ReleaseWriterLock();
                }
                
                Logger.LogInformation("Deleted celeb with id " + index);
            }

            return (celeb != null);
        }

        /// <summary
        ///  init json file from backup json (if exist) or from imdb
        /// </summary>
        public static void ResetData()
        {
            try
            {
                readerWriterLock.AcquireWriterLock(lockTimeout_in_ms);

                if (File.Exists(celebs_json_backup_path))
                {
                    Logger.LogInformation("ResetData (from backup file) ...");
                    File.Copy(celebs_json_backup_path, celebs_json_path, true);
                    Logger.LogInformation("ResetData (from backup file) ... Done");
                }
                else
                {

                    Logger.LogInformation("ResetData (from imdb) ...");
                    var webScrapper = new WebScrapper();
                    var celebs = webScrapper.Get_Celebs_From_WebSite();                      //throws exception on error
                    File.WriteAllText(celebs_json_path,         JsonConvert.SerializeObject(celebs));
                    File.WriteAllText(celebs_json_backup_path,  JsonConvert.SerializeObject(celebs));
                    Logger.LogInformation("ResetData (from imdb) ... Done");
                }

            }
            finally
            {
                readerWriterLock.ReleaseWriterLock();
            }         
        }

    }
}
