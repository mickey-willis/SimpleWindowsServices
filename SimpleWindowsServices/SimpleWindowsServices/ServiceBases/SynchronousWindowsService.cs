using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Timers;

namespace SimpleWindowsServices.ServiceBases
{
    public abstract class SynchronousWindowsService : WindowsService
    {
        protected abstract IList<Action<CancellationToken>> Processes { get; }

        protected sealed override IList<ElapsedEventHandler> CreateElapsedEventHandlers() 
        {
            return Processes
                .Select(CreateElapsedEventHandler)
                .ToList();
        }

        protected abstract void LogException(Exception exception);

        private ElapsedEventHandler CreateElapsedEventHandler(Action<CancellationToken> process)
        {
            return new ElapsedEventHandler((sender, e) =>
            {
                try
                {
                    process(GetCancellationToken());
                }
                catch (Exception exception)
                {
                    LogException(exception);
                }
            });
        }
    }
}
