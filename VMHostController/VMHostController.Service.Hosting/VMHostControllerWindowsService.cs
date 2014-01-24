using System.ServiceModel;
using System.ServiceProcess;
using VMHostController.BIZ;
using VMWareController.Service;

namespace VMHostController.Service.Hosting
{
    public class VMHostControllerWindowsService : ServiceBase
    {
        public ServiceHost serviceHost = null;

        public VMHostControllerWindowsService()
        {
            ServiceName = "VMHostService";
        }

        public static void Main()
        {
            Run(new VMHostControllerWindowsService());
        }


        // Start the Windows service.
        protected override void OnStart(string[] args)
        {
            if (serviceHost != null)
            {
                serviceHost.Close();
            }

            // Create a ServiceHost for the CalculatorService type and 
            // provide the base address.
            serviceHost = new ServiceHost(typeof(VMHostService));

            // Open the ServiceHostBase to create listeners and start 
            // listening for messages.
            serviceHost.Open();

            VMStatusPolling.StartTimer();
        }

        protected override void OnStop()
        {
            VMStatusPolling.StopTimer();
            if (serviceHost != null)
            {
                serviceHost.Close();
                serviceHost = null;
            }
        }
    }
}
