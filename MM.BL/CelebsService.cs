using MM.Common;
using MM.Common.Models;
using MM.Dal;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace MM.BL
{
    public class CelebsService
    {
        public IEnumerable<Celeb> GetAll()
        {
            var celebsRepository    = new CelebsRepository();
            var result              = celebsRepository.GetAll();
            return result;
        }


        public bool Delete(int index)
        {
            var celebsRepository    = new CelebsRepository();
            var result              = celebsRepository.Delete(index);
            return result;
        }

        public static void ResetData()
        {
            CelebsRepository.ResetData();
        }

    }
}
