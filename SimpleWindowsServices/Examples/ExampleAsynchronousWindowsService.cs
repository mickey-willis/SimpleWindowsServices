using SimpleWindowsServices.ServiceBases;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Policy;
using System.Threading;
using System.Threading.Tasks;

namespace Examples
{
    public class ExampleAsynchronousWindowsService : AsynchronousWindowsService
    {
        private HttpClient _httpClient;
        
        public ExampleAsynchronousWindowsService()
        {
            _httpClient = new HttpClient();
        }

        protected sealed override int Interval { get { return 30000; } }

        protected sealed override IList<Func<CancellationToken, Task>> Processes
        {
            get
            {
                return new List<Func<CancellationToken, Task>>()
                {
                    new Func<CancellationToken, Task>(CheckGoogleIsAlive),
                    new Func<CancellationToken, Task>(CheckYouTubeIsAlive)
                };
            }
        }

        protected sealed override void LogException(Exception exception)
        {
            Console.WriteLine($"ERROR  - {nameof(ExampleAsynchronousWindowsService)} - {exception.Message}!");
        }

        protected sealed override void OnDispose()
        {
            _httpClient.Dispose();
        }

        protected sealed override void OnStartInitialisation()
        {
            Console.WriteLine($"{nameof(ExampleAsynchronousWindowsService)} is starting.");
        }

        private async Task CheckGoogleIsAlive(CancellationToken cancellationToken)
        {
            Console.WriteLine($"{nameof(ExampleAsynchronousWindowsService)} - Checking if Google is Alive.");
            if (await IsAlive(new Url("https://www.google.co.uk/"), cancellationToken))
            {
                Console.WriteLine($"{nameof(ExampleAsynchronousWindowsService)} - Google is Alive.");
            }
            else
            {
                Console.WriteLine($"{nameof(ExampleAsynchronousWindowsService)} - Google is DOWN!");
            }
        }

        private async Task CheckYouTubeIsAlive(CancellationToken cancellationToken)
        {
            Console.WriteLine($"{nameof(ExampleAsynchronousWindowsService)} - Checking if YouTube is Alive.");
            if (await IsAlive(new Url("https://www.youtube.com/"), cancellationToken))
            {
                Console.WriteLine($"{nameof(ExampleAsynchronousWindowsService)} - YouTube is Alive.");
            }
            else
            {
                Console.WriteLine($"{nameof(ExampleAsynchronousWindowsService)} - YouTube is DOWN!");
            }
        }

        private async Task<bool> IsAlive(Url url, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(url.Value, cancellationToken);
            return response != null && response.IsSuccessStatusCode;
        }
    }
}
