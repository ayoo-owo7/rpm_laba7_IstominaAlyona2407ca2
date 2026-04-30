using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Rpm_Lab7.Logger;

namespace Rpm_Lab7
{
    public class Colleagues
    {
        public class PrintQueue
        {
            private readonly Queue<Document> _queue = new();
            private readonly IPrintMediator _mediator;

            public PrintQueue(IPrintMediator mediator) => _mediator = mediator;

            public void Enqueue(Document doc)
            {
                _queue.Enqueue(doc);
                _mediator.Log($"В очереди: '{doc.Title}'. Размер: {_queue.Count}");
            }

            public Document Dequeue() => _queue.Count > 0 ? _queue.Dequeue() : null;
            public bool IsEmpty() => _queue.Count == 0;
        }

        public class Printer
        {
            private readonly IPrintMediator _mediator;
            private readonly string _targetErrorDocId;

            public Printer(IPrintMediator mediator, string targetErrorDocId = null)
            {
                _mediator = mediator;
                _targetErrorDocId = targetErrorDocId;
            }

            public void Print(Document doc)
            {
                _mediator.Log($"Принтер начинает печать '{doc.Title}'...");
                Thread.Sleep(400);

                if (doc.Id == _targetErrorDocId)
                {
                    _mediator.Log("Произошла ошибка: Замятие бумаги. Печать прервана.");
                    doc.Fail();
                }
                else
                {
                    _mediator.Log($"Принтер успешно напечатал '{doc.Title}'.");
                    doc.Complete();
                }
            }
        }
    }
}
