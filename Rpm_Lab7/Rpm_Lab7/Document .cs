using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Rpm_Lab7.Logger;
using static Rpm_Lab7.StatePattern;

namespace Rpm_Lab7
{
    public class Document
    {
            public string Id { get; }
            public string Title { get; }
            public IDocumentState State { get; set; }
            public IPrintMediator Mediator { get; }

            public Document(string id, string title, IPrintMediator mediator)
            {
                Id = id;
                Title = title;
                Mediator = mediator;
                State = new NewState();
            }

            public void TryAddToQueue() => State.HandleAddToQueue(this);
            public void StartPrinting() => State.HandleStartPrint(this);
            public void Complete() => State.HandleComplete(this);
            public void Fail() => State.HandleError(this);
            public void Retry() => State.HandleRetry(this);
    }
}
