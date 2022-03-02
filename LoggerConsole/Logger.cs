using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerConsole
{
    internal class Logger
    {
        public static void WriteLog(string message)
        {
            string logPath = ConfigurationManager.AppSettings["logPath"];
            using(StreamWriter write=new StreamWriter(logPath, true))
            {
                write.WriteLine($"{DateTime.Now}:{message}");
            }
        }
    }
}
