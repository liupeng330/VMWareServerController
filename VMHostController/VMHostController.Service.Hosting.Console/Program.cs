using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using VMHostController.BIZ;
using VMWareController.Service;

namespace VMHostController.Service.Hosting.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof (VMHostService)))
            {
                host.Opened += host_Opened;
                host.Closing += host_Closing;
                host.Open();
                System.Console.Read();
            }
        }

        private static void host_Opened(object sender, EventArgs e)
        {
            VMStatusPolling.StartTimer();
        }

        static void host_Closing(object sender, EventArgs e)
        {
            VMStatusPolling.StopTimer();
        }
    }
}
