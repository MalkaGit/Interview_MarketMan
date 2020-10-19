using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MM.Common.Utils
{
    //https://stackify.com/net-core-loggerfactory-use-correctly/
    //install package Microsoft.Extensions.Logging.Abstractions


    /// <summary>
    /// wrapper over the LoggerFactory (using single LoggerFactory instace)
    /// </summary>
    public class LogManager
    {
        private static ILoggerFactory LoggerFactory { get; set; }


        /// <summary>
        /// called from starup.cs :: Confugure method
        /// </summary>
        /// <param name="loggerFactory"></param>
        public static void init_LogFactory  (ILoggerFactory loggerFactory)
        {
            LoggerFactory = loggerFactory;

            var path = Directory.GetCurrentDirectory();
            loggerFactory.AddFile($"{path}\\Logs\\Log.txt"); //  //note: add nuget package    Serilog.Extensions.Logging.File   
        }


        /// <summary>
        /// called from 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static ILogger CreateLogger<T>()
        {
            var logger = LoggerFactory.CreateLogger(typeof(T).FullName);
            return logger;
        }
    }
}
