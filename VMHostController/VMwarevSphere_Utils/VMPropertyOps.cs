using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppUtil;
using VimyyApi;

namespace VMwarevSphere_Utils
{
    internal class VMPropertyOps
    {
        private AppUtil.AppUtil cb;
        private static VimService _service;
        private static ServiceContent _sic;

        /// <summary>
        /// Array of typename + all its properties
        /// </summary>
        private string[][] typeInfo = new string[][] { 
              new string[] { "Folder", "name", "childEntity" }, };

        public VMPropertyOps(AppUtil.AppUtil cb)
        {
            this.cb = cb;
        }

        public static OptionSpec[] ConstructOptions()
        {
            OptionSpec[] useroptions = new OptionSpec[2];
            useroptions[0] = new OptionSpec("typename", "String", 1
                                            , "Type of managed entity"
                                            , null);
            useroptions[1] = new OptionSpec("propertyname", "String", 1
                                            , "Name of the Property"
                                            , null);
            return useroptions;
        }

        private string[][] BuildTypeInfo()
        {
            string[] typenprops = new[] { "VirtualMachine", "runtime" };
            string[] typeprops2 = new[] { "VirtualMachine", "name" };
            string[] typeprops3 = new[] { "VirtualMachine", "guest" };
            return typeInfo = new string[][] { typenprops, typeprops2, typeprops3 };
        }

        public IEnumerable<VMInfomation> GetProperties()
        {
            var buildTypeInfo = BuildTypeInfo();
            var ret = new List<VMInfomation>();

            // Retrieve Contents recursively starting at the root folder 
            // and using the default property collector.            
            ObjectContent[] ocary = cb.getServiceUtil().GetContentsRecursively(null, null, buildTypeInfo, true);
            foreach (var objectContent in ocary)
            {
                if (objectContent.propSet.Length != 3)
                {
                    throw new Exception("Dynamic property array must be 3!!");
                }
                var name = (from i in objectContent.propSet where i.name == "name" select i.val).Single().ToString();
                var runtime =
                    (from i in objectContent.propSet where i.name == "runtime" select i.val).Single() as
                        VirtualMachineRuntimeInfo;
                if (runtime == null)
                {
                    throw new NullReferenceException("Fail to cast from object to VirtualMachineRuntimeInfo");
                }

                var guest = (from i in objectContent.propSet where i.name == "guest" select i.val).Single() as GuestInfo;
                if (guest == null)
                {
                    throw  new NullReferenceException("Fail to cast from object to GestInfo");
                }

                var vmInfo = new VMInfomation();
                vmInfo.Name = name;
                switch (runtime.powerState)
                {
                    case VirtualMachinePowerState.poweredOff:
                        vmInfo.Status = VMStatus.OFF;
                        break;
                    case VirtualMachinePowerState.poweredOn:
                        vmInfo.Status = VMStatus.ON;
                        break;
                    case VirtualMachinePowerState.suspended:
                        vmInfo.Status = VMStatus.SUSPEND;
                        break;
                }
                vmInfo.LastBootTime = runtime.bootTime;
                vmInfo.IPAddress = guest.ipAddress;
                if (guest.net != null && guest.net.Length == 1)
                {
                    vmInfo.MacAddress = guest.net[0].macAddress;
                }
                else if (guest.net != null && guest.net.Length != 1)
                {
                    throw new Exception("There are more than one networks adapter in this VM!!");
                }
                ret.Add(vmInfo);
            }

            return ret;
        }
    }
}
