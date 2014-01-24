using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VMHostController.BIZ;
using VMwarevSphere_Utils;

namespace TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //VMAccount account  = new VMAccount("root", "Nihaoreal");
            ////PowerOps powerOps = new PowerOps(account, "192.168.35.57", @"https://192.168.35.57/sdk");
            ////powerOps.On("Test");
            ////Thread.Sleep(5000);

            ////SnapshotOps snapshotOps = new SnapshotOps(account, "192.168.35.57", @"https://192.168.35.57/sdk");
            ////snapshotOps.List("Test").ToList().ForEach( i => Console.WriteLine("The snapshot name is " + i));
            ////snapshotOps.Revert("Test", "Snapshot 3");

            //PropertyOps propertyOps = new PropertyOps(account, "192.168.35.57", @"https://192.168.35.57/sdk");
            ////Console.WriteLine(propertyOps.IsPowerOn("Test"));
            //var ret = propertyOps.GetRuntimeInfo();

            //foreach (var virtualMachineRuntimeInfo in ret)
            //{
            //    Console.WriteLine("Name: [{0}]", virtualMachineRuntimeInfo.Name);
            //    Console.WriteLine("BootTime: [{0}]", virtualMachineRuntimeInfo.LastBootTime);
            //    Console.WriteLine("PowerState [{0}]", virtualMachineRuntimeInfo.Status);
            //    Console.WriteLine("IPAddress [{0}]", virtualMachineRuntimeInfo.IPAddress);
            //    Console.WriteLine("MacAddress [{0}]", virtualMachineRuntimeInfo.MacAddress);
            //    Console.WriteLine();
            //}

            //while (true)
            //{
            //    Console.WriteLine("Start to update VM information!!");
            //    IVMHostBIZ biz = new VMHostBIZ();
            //    biz.UpdateVMInfo();
            //    Console.WriteLine("End to update VM information!!");
            //    Console.WriteLine();
            //    Thread.Sleep(1000);
            //}

            IVMHostBIZ biz = new VMHostBIZ();
            biz.UpdateVMInfo();
        }
    }
}
