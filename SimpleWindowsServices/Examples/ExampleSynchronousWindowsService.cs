using SimpleWindowsServices.ServiceBases;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Examples
{
    public class ExampleSynchronousWindowsService : SynchronousWindowsService
    {
        protected sealed override int Interval { get { return 10000; } }

        protected sealed override IList<Action<CancellationToken>> Processes
        {
            get 
            { 
                return new List<Action<CancellationToken>>()
                {
                    new Action<CancellationToken>(ProcessOne),
                    new Action<CancellationToken>(ProcessTwo),
                    new Action<CancellationToken>(ProcessThree),
                }; 
            }
        }

        protected sealed override void LogException(Exception exception)
        {
            Console.WriteLine($"ERROR  - {nameof(ExampleSynchronousWindowsService)} - {exception.Message}!");
        }

        protected sealed override void OnDispose()
        {
        }

        protected sealed override void OnStartInitialisation()
        {
            Console.WriteLine($"{nameof(ExampleSynchronousWindowsService)} is starting.");
        }

        private void ProcessOne(CancellationToken cancellationToken)
        {
            Console.WriteLine($"{nameof(ExampleSynchronousWindowsService)} - Process One has Run.");
        }

        private void ProcessTwo(CancellationToken cancellationToken)
        {
            Console.WriteLine($"{nameof(ExampleSynchronousWindowsService)} - Process Two has Run.");
        }

        private void ProcessThree(CancellationToken cancellationToken)
        {
            Console.WriteLine($"{nameof(ExampleSynchronousWindowsService)} - Process Three has Run.");
        }
    }
}
