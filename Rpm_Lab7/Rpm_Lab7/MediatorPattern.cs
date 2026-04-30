using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Rpm_Lab7.Colleagues;
using static Rpm_Lab7.Logger;

namespace Rpm_Lab7
{
    public class MediatorPattern
    {
        public class PrintMediator : IPrintMediator
        {
            public PrintQueue Queue { get; }
            public Printer Printer { get; }
            private readonly Logger.Logger _logger;

            public PrintMediator(Logger.Logger logger, string errorDocId = null)
            {
                _logger = logger;
                Queue = new PrintQueue(this);
                Printer = new Printer(this, errorDocId);
            }

            public void Log(string message) => _logger.Write(message);
            public void AddToQueue(Document doc) => Queue.Enqueue(doc);

            public void StartPrintJob(Document doc)
            {
                Log($"Посредник передаёт '{doc.Title}' на печать.");
                Printer.Print(doc);
            }

            public void ReportSuccess(Document doc) => Log($"Итог: '{doc.Title}' успешно обработан.");
            public void ReportError(Document doc) => Log($"Итог: '{doc.Title}' требует вмешательства или повторной отправки.");

            public void ProcessQueue()
            {
                Log("Запуск обработки очереди...");
                while (!Queue.IsEmpty())
                {
                    var doc = Queue.Dequeue();
                    if (doc != null)
                        doc.StartPrinting();
                }
                Log("Очередь полностью обработана.");
            }
        }
    }
}
