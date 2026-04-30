using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Rpm_Lab7
{
    public class StatePattern
    {
        public interface IDocumentState
        {
            void HandleAddToQueue(Document doc);
            void HandleStartPrint(Document doc);
            void HandleComplete(Document doc);
            void HandleError(Document doc);
            void HandleRetry(Document doc);
        }

        public class NewState : IDocumentState
        {
            public void HandleAddToQueue(Document doc)
            {
                doc.Mediator.Log($"'{doc.Title}' в состоянии New. Добавляю в очередь.");
                doc.Mediator.AddToQueue(doc);
            }
            public void HandleStartPrint(Document doc)
            {
                doc.State = new PrintingState();
                doc.Mediator.Log($"'{doc.Title}' переведён в состояние Printing.");
                doc.Mediator.StartPrintJob(doc);
            }
            public void HandleComplete(Document doc) => doc.Mediator.Log($"Ошибка: нельзя завершить '{doc.Title}' из состояния New.");
            public void HandleError(Document doc) => doc.Mediator.Log($"Ошибка: '{doc.Title}' не может перейти в Error из New.");
            public void HandleRetry(Document doc) => doc.Mediator.Log($"Ошибка: '{doc.Title}' не требует повторной отправки (он новый).");
        }

        public class PrintingState : IDocumentState
        {
            public void HandleAddToQueue(Document doc) => doc.Mediator.Log($"Ошибка: '{doc.Title}' уже печатается. Нельзя добавить в очередь.");
            public void HandleStartPrint(Document doc) => doc.Mediator.Log($"Ошибка: '{doc.Title}' уже печатается.");
            public void HandleComplete(Document doc)
            {
                doc.State = new DoneState();
                doc.Mediator.Log($"'{doc.Title}' переведён в состояние Done.");
                doc.Mediator.ReportSuccess(doc);
            }
            public void HandleError(Document doc)
            {
                doc.State = new ErrorState();
                doc.Mediator.Log($"'{doc.Title}' переведён в состояние Error.");
                doc.Mediator.ReportError(doc);
            }
            public void HandleRetry(Document doc) => doc.Mediator.Log($"Ошибка: нельзя повторить '{doc.Title}', пока он печатается.");
        }

        public class DoneState : IDocumentState
        {
            public void HandleAddToQueue(Document doc) => doc.Mediator.Log($"Ошибка: '{doc.Title}' уже напечатан. Действие запрещено.");
            public void HandleStartPrint(Document doc) => doc.Mediator.Log($"Ошибка: '{doc.Title}' уже напечатан.");
            public void HandleComplete(Document doc) => doc.Mediator.Log($"Ошибка: '{doc.Title}' уже в Done.");
            public void HandleError(Document doc) => doc.Mediator.Log($"Ошибка: '{doc.Title}' уже в Done.");
            public void HandleRetry(Document doc) => doc.Mediator.Log($"Ошибка: '{doc.Title}' уже напечатан успешно.");
        }

        public class ErrorState : IDocumentState
        {
            public void HandleAddToQueue(Document doc) => doc.Mediator.Log($"Ошибка: для '{doc.Title}' в состоянии Error используйте Retry().");
            public void HandleStartPrint(Document doc) => doc.Mediator.Log($"Ошибка: для '{doc.Title}' в состоянии Error используйте Retry().");
            public void HandleComplete(Document doc) => doc.Mediator.Log($"Ошибка: '{doc.Title}' в Error. Сначала выполните Retry().");
            public void HandleError(Document doc) => doc.Mediator.Log($"Ошибка: '{doc.Title}' уже в Error.");
            public void HandleRetry(Document doc)
            {
                doc.State = new NewState();
                doc.Mediator.Log($"'{doc.Title}' переведён в New. Повторно добавляю в очередь.");
                doc.Mediator.AddToQueue(doc);
            }
        }
    }
}
