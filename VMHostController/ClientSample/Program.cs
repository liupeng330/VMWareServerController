using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using VMWareController.Service;

namespace ClientSample
{
    class Program
    {
        static void Main(string[] args)
        {
            using (VMHostServiceClient client = new VMHostServiceClient())
            {
                client.UpdateVMInfo();
            }
        }
    }
}
