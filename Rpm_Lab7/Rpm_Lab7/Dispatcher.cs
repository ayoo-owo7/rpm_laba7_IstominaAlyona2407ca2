using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Rpm_Lab7.Logger;
using static Rpm_Lab7.MediatorPattern;

namespace Rpm_Lab7
{
    public class Dispatcher
    {
        public class PrintDispatcher
        {
            public void RunDemo()
            {
                var logger = new Logger.Logger();
                var mediator = new PrintMediator(logger, errorDocId: "DOC-002");

                var doc1 = new Document("DOC-001", "Отчёт за Q1", mediator);
                var doc2 = new Document("DOC-002", "Финансовая сводка", mediator);
                var doc3 = new Document("DOC-003", "Протокол совещания", mediator);

                Console.WriteLine("1. Добавление документов в очередь");
                doc1.TryAddToQueue();
                doc2.TryAddToQueue();
                doc3.TryAddToQueue();

                Console.WriteLine("\n2. Запуск печати (FIFO)");
                mediator.ProcessQueue();

                Console.WriteLine("\n3. Попытка добавить уже напечатанный документ");
                doc1.TryAddToQueue();

                Console.WriteLine("\n4. Повторная отправка документа с ошибкой");
                doc2.Retry();

                Console.WriteLine("\n5. Повторная обработка очереди");
                mediator.ProcessQueue();

                Console.WriteLine("\n6. Проверка финальных состояний");
                Console.WriteLine($"{doc1.Title} -> {doc1.State.GetType().Name}");
                Console.WriteLine($"{doc2.Title} -> {doc2.State.GetType().Name}");
                Console.WriteLine($"{doc3.Title} -> {doc3.State.GetType().Name}");

            }
        }
    }
}
