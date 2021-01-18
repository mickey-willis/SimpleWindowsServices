using SimpleWindowsServices.ServiceBases;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;

namespace SimpleWindowsServices
{
    public class ServiceCollection : List<WindowsService>
    {
        public ServiceCollection(IEnumerable<WindowsService> services) : base(services.ToList())
        {
        }

        public ServiceCollection(IList<WindowsService> services) : base(services)
        {
        }

        public static implicit operator ServiceBase[](ServiceCollection collection)
        {
            return collection
                .Select(service => (ServiceBase)service)
                .ToArray();
        }
    }
}
