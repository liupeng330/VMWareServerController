using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
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
                switch (args[0])
                {
                    case "on":
                        client.PowerOn(args[1]);
                        break;
                    case "off":
                        client.PowerOff(args[1]);
                        break;
                    case "restore":
                        client.Restore(args[1]);
                        break;
                    case "ispoweron":
                        Console.WriteLine(client.IsPowerOn(args[1]));
                        break;
                }
            }
        }
    }
}
