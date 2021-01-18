using SimpleWindowsServices;
using SimpleWindowsServices.Infrastructure;
using SimpleWindowsServices.ServiceBases;
using System.Collections.Generic;

namespace Examples
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static void Main()
        {
            bool isDebug;

#if DEBUG
            isDebug = true;
#endif

            IList<WindowsService> services =
                new List<WindowsService>()
                {
                    //new ExampleWindowsService(),
                    new ExampleSynchronousWindowsService(),
                    //new ExampleAsynchronousWindowsService()
                };

            ServiceCollection serivceCollection = new ServiceCollection(services);
            IServiceManager serviceManager = new ServiceManager(serivceCollection);
            serviceManager.RunServices(isDebug);
        }
    }
}
