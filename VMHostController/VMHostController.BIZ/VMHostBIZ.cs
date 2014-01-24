using System.Configuration;
using VMHostController.DAL;
using VMHostController.DAL.Model;
using VMwarevSphere_Utils;

namespace VMHostController.BIZ
{
    public class VMHostBIZ : IVMHostBIZ
    {
        private readonly IVMInfoRepository _vmInfoRepository = new VMInfoRepository();

        private void PowerOps(string macAddress, VMStatus status)
        {
            var hostInfo = _vmInfoRepository.GetVMHostInfoByVMMacAddress(macAddress);
            if (hostInfo == null)
            {
                //TODO: Log here
                return;
            }

            var vmName = _vmInfoRepository.GetVMNameByMacAddress(macAddress);

            var account = new VMAccount(hostInfo.UserName, hostInfo.Password);
            var ops = new PowerOps(account, hostInfo.Name, hostInfo.URL);

            switch (status)
            {
                case VMStatus.ON:
                    ops.On(vmName);
                    break;
                case VMStatus.OFF:
                    ops.Off(vmName);
                    break;
            }
        }

        public void PowerOn(string macAddress)
        {
            PowerOps(macAddress, VMStatus.ON);
        }

        public void PowerOff(string macAddress)
        {
            PowerOps(macAddress, VMStatus.OFF);
        }

        public void Restore(string macAddress)
        {
            var hostInfo = _vmInfoRepository.GetVMHostInfoByVMMacAddress(macAddress);
            if (hostInfo == null)
            {
                //TODO: Log here
                return;
            }

            var vmName = _vmInfoRepository.GetVMNameByMacAddress(macAddress);

            var account = new VMAccount(hostInfo.UserName, hostInfo.Password);
            var ops = new SnapshotOps(account, hostInfo.Name, hostInfo.URL);
            ops.Revert(vmName, ConfigurationManager.AppSettings["SnapshotName"]);
        }

        public bool IsPowerOn(string macAddress)
        {
            var hostInfo = _vmInfoRepository.GetVMHostInfoByVMMacAddress(macAddress);
            if (hostInfo == null)
            {
                //TODO: Log here
                return false;
            }

            var vmName = _vmInfoRepository.GetVMNameByMacAddress(macAddress);
            var account = new VMAccount(hostInfo.UserName, hostInfo.Password);
            var ops = new PropertyOps(account, hostInfo.Name, hostInfo.URL);
            return ops.IsPowerOn(vmName);
        }

        public void UpdateVMInfo()
        {
            //Get all VMHost info from DB first
            var allVMHostInfoes = _vmInfoRepository.GetAllVMHostInfo();

            foreach (var hostInfo in allVMHostInfoes)
            {
                VMAccount account = new VMAccount(hostInfo.UserName, hostInfo.Password);
                PropertyOps ops = new PropertyOps(account, hostInfo.Name, hostInfo.URL);
                var runTimeInfoes = ops.GetRuntimeInfo();

                //Get information from every VMs in this VM Host
                foreach (var vmInfo in runTimeInfoes)
                {
                    VMInfo vmInfoInDB = new VMInfo
                    {
                        HostId = hostInfo.Id,
                        Name = vmInfo.Name,
                        IPAddress = vmInfo.IPAddress,
                        MacAddress = vmInfo.MacAddress,
                        LastBootTime = vmInfo.LastBootTime,
                        Status = (int)vmInfo.Status,
                    };

                    //Update this vminfo into DB
                    _vmInfoRepository.UpdateVMInfo(vmInfoInDB);
                }
            }
        }
    }
}
