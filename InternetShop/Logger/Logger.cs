using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public class Logger : ILogger
    {
        private readonly ILog log;
        public Logger(Type type)
        {
            log = LogManager.GetLogger(type);
            XmlConfigurator.Configure();
        }
        public void Debug(string message)
        {
            log.Debug(message);
        }

        public void Error(string message)
        {
            log.Error(message);
        }

        public void Fatal(string message)
        {
            log.Fatal(message);
        }

        public void Info(string message)
        {
            log.Info(message);
        }

        public void Warn(string message)
        {
            log.Warn(message);
        }
    }
}
