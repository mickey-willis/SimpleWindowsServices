using SimpleWindowsServices.ServiceBases;
using System;
using System.Collections.Generic;
using System.Timers;

namespace Examples
{
    public class ExampleWindowsService : WindowsService
    {
        protected sealed override int Interval { get { return 10000; } }

        protected sealed override IList<ElapsedEventHandler> CreateElapsedEventHandlers()
        {
            return new List<ElapsedEventHandler>()
            {
                new ElapsedEventHandler(ProcessOne),
                new ElapsedEventHandler(ProcessTwo),
                new ElapsedEventHandler(ProcessThree),
            };
        }

        protected sealed override void OnDispose()
        {
        }

        protected sealed override void OnStartInitialisation()
        {
            Console.WriteLine($"{nameof(ExampleWindowsService)} is starting.");
        }
        
        private void ProcessOne(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine($"{nameof(ExampleWindowsService)} - Process One has Run.");
        }

        private void ProcessTwo(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine($"{nameof(ExampleWindowsService)} - Process Two has Run.");
        }

        private void ProcessThree(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine($"{nameof(ExampleWindowsService)} - Process Three has Run.");
        }
    }
}
