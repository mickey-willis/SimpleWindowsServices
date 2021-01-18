using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace SimpleWindowsServices.ServiceBases
{
    public abstract class AsynchronousWindowsService : WindowsService
    {
        protected abstract IList<Func<CancellationToken, Task>> Processes { get; }

        protected IList<Func<CancellationToken, Task>> CreateDelegates(Func<CancellationToken, Task>[] args)
        {
            return args.ToList();
        }

        protected sealed override IList<ElapsedEventHandler> CreateElapsedEventHandlers()
        {
            return new List<ElapsedEventHandler>() { CreateElapsedEventHandler() };
        }

        protected abstract void LogException(Exception exception);

        private ElapsedEventHandler CreateElapsedEventHandler()
        {
            return new ElapsedEventHandler(ProcessTasks);
        }

        private IList<Task> GetProcessesTasks()
        {
            return Processes
                .Select(process => process(GetCancellationToken()))
                .ToList();
        }

        private async void ProcessTasks(object sender, EventArgs e)
        {
            try
            {
                await Task.WhenAll(GetProcessesTasks());
            }
            catch (Exception exception)
            {
                LogException(exception);
            }
        }
    }
}
