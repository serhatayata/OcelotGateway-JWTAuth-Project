using System;
using System.Collections.Generic;
using System.Text;
using Core.CrossCuttingConcerns.Logging.Log4Net;

namespace Core.CrossCuttingConcerns.Logging.Log4Net.Loggers
{
    public class FileLogger : LoggerServiceBase
    {
        public FileLogger() : base("JsonFileLogger")
        {
        }
    }
}
