using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Rpm_Lab7
{
    public class Logger
    {
        public class Logger
        {
            public void Write(string message) =>
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss:ss}] {message}");
        }

        public interface IPrintMediator
        {
            void AddToQueue(Document doc);
            void StartPrintJob(Document doc);
            void ReportSuccess(Document doc);
            void ReportError(Document doc);
            void Log(string message);
        }
    }
}