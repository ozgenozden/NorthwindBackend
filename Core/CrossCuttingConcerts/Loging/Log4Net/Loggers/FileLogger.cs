using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerts.Loging.Log4Net.Loggers
{
    public class FileLogger : LoggerServiceBase
    {
        public FileLogger() : base("JsonFileLogger")
        {
        }
    }
}
