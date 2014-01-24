using System;
using System.Collections.Generic;
using System.Linq;
using VMHostController.DAL.Model;

namespace VMHostController.DAL
{
    public class VMInfoRepository : IVMInfoRepository
    {
        public void UpdateVMInfo(Model.VMInfo vmInfo)
        {
            using (var db = new ReportingSiteEntities())
            {
                //Get vminfo from DB by name and its host ID (Need to keep name of VM to be identical in one VMHost)
                var returnedVMInfo = (from i
                                        in db.VMInfoes
                                      where i.Name.Equals(vmInfo.Name, StringComparison.OrdinalIgnoreCase) && vmInfo.HostId == i.HostId
                                      select i).SingleOrDefault();

                //IF return a null vminfo from DB, then need to insert this VM info directly
                if (returnedVMInfo == null)
                {
                    db.VMInfoes.Add(vmInfo);
                }
                else
                {
                    returnedVMInfo.Name = vmInfo.Name;
                    returnedVMInfo.Status = vmInfo.Status;

                    //Update ip address info in DB Only when ip address in DB is null, or when diff with ip address which is fetched from VM SDK
                    if (string.IsNullOrEmpty(returnedVMInfo.IPAddress) ||
                        (vmInfo.IPAddress != null && !returnedVMInfo.IPAddress.Equals(vmInfo.IPAddress, StringComparison.OrdinalIgnoreCase)))
                    {
                        returnedVMInfo.IPAddress = vmInfo.IPAddress;
                    }
                    if (string.IsNullOrEmpty(returnedVMInfo.MacAddress) ||
                        (vmInfo.MacAddress != null && !returnedVMInfo.MacAddress.Equals(vmInfo.MacAddress, StringComparison.OrdinalIgnoreCase)))
                    {
                        returnedVMInfo.MacAddress = vmInfo.MacAddress;
                    }

                    if (vmInfo.LastBootTime != DateTime.MinValue)
                    {
                        returnedVMInfo.LastBootTime = vmInfo.LastBootTime;
                    }
                }
                db.SaveChanges();
            }
        }

        public Model.VMHostInfo GetVMHostInfoByName(string hostName)
        {
            using (var db = new ReportingSiteEntities())
            {
                return
                    db.VMHostInfoes.SingleOrDefault(i => i.Name.Equals(hostName, StringComparison.OrdinalIgnoreCase));
            }
        }

        public Model.VMHostInfo GetVMHostInfoByID(int id)
        {
            using (var db = new ReportingSiteEntities())
            {
                return
                    db.VMHostInfoes.SingleOrDefault(i => i.Id == id);
            }
        }

        public IEnumerable<VMHostInfo> GetAllVMHostInfo()
        {
            using (var db = new ReportingSiteEntities())
            {
                return db.VMHostInfoes.ToList();
            }
        }

        public VMHostInfo GetVMHostInfoByVMMacAddress(string macAddress)
        {
            using (var db = new ReportingSiteEntities())
            {
                var vm = db.VMInfoes.SingleOrDefault(i => i.MacAddress.Equals(macAddress, StringComparison.OrdinalIgnoreCase));
                if (vm == null)
                {
                    return null;
                }
                return vm.VMHostInfo;
            }
        }

        public string GetVMNameByMacAddress(string macAddress)
        {
            using (var db = new ReportingSiteEntities())
            {
                var vm =
                    db.VMInfoes.SingleOrDefault(i => i.MacAddress.Equals(macAddress, StringComparison.OrdinalIgnoreCase));
                if (vm == null)
                {
                    return null;
                }
                return vm.Name;
            }
        }
    }
}
