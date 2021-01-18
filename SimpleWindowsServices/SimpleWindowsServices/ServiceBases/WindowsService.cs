using System.Collections.Generic;
using System.ServiceProcess;
using System.Threading;
using System.Timers;
using Timer = System.Timers.Timer;

namespace SimpleWindowsServices.ServiceBases
{
    public abstract class WindowsService : ServiceBase
    {
        private CancellationTokenSource _cancellationTokenSource;

        private Timer _timer;

        public void Run()
        {
            OnStart(null);
        }

        protected abstract int Interval { get; }

        protected abstract IList<ElapsedEventHandler> CreateElapsedEventHandlers();

        protected sealed override void Dispose(bool disposing)
        {
            _timer.Stop();
            _timer.Dispose();
            OnDispose();
        }

        protected CancellationToken GetCancellationToken()
        {
            return _cancellationTokenSource.Token;
        }

        protected abstract void OnDispose();

        protected sealed override void OnContinue()
        {
            _timer.Start();
        }

        protected sealed override void OnPause()
        {
            _timer.Stop();
        }

        protected sealed override void OnShutdown()
        {
            _cancellationTokenSource.Cancel();
            _timer.Stop();
        }

        protected sealed override void OnStart(string[] args)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            OnStartInitialisation();
            InitialiseTimer();
        }

        protected abstract void OnStartInitialisation();

        private void InitialiseTimer()
        {
            _timer = new Timer(Interval);
            foreach (ElapsedEventHandler handler in CreateElapsedEventHandlers())
            {
                _timer.Elapsed += handler;
            }
            _timer.Start();
        }
    }
}
