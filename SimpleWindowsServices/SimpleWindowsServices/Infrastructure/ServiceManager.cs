using SimpleWindowsServices.ServiceBases;
using System.ServiceProcess;
using System.Threading;

namespace SimpleWindowsServices.Infrastructure
{
    public class ServiceManager : IServiceManager
    {
        private ServiceCollection _services;

        public ServiceManager(ServiceCollection services)
        {
            _services = services;
        }

        public void RunServices(bool debugging)
        {
            if (debugging)
            {
                RunServicesManually();
            }
            else
            {
                ServiceBase.Run(_services);
            }
        }

        private void RunServicesManually()
        {
            foreach (WindowsService service in _services)
            {
                service.Run();
            }
            Thread.Sleep(Timeout.Infinite);
        }
    }
}
