using System.Configuration;
using System.Linq;
using log4net;
using VMHostController.DAL;
using VMHostController.DAL.Model;
using VMwarevSphere_Utils;

namespace VMHostController.BIZ
{
    public class VMHostBIZ : IVMHostBIZ
    {
        private readonly IVMInfoRepository _vmInfoRepository = new VMInfoRepository();
        private static readonly ILog _logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private void PowerOps(string macAddress, VMStatus status)
        {
            var hostInfo = _vmInfoRepository.GetVMHostInfoByVMMacAddress(macAddress);
            if (hostInfo == null)
            {
                _logger.Debug(string.Format("[PowerOps]Fail to get hostInfo for macaddress [{0}]", macAddress));
                return;
            }

            var vmName = _vmInfoRepository.GetVMNameByMacAddress(macAddress);
            _logger.Debug(string.Format("[PowerOps]Get VM name [{0}] from mac address [{1}]", vmName, macAddress));

            var account = new VMAccount(hostInfo.UserName, hostInfo.Password);
            var ops = new PowerOps(account, hostInfo.Name, hostInfo.URL);

            _logger.Debug(string.Format("[PowerOps]Start to power ops VM name [{0}]", vmName));
            switch (status)
            {
                case VMStatus.ON:
                    ops.On(vmName);
                    break;
                case VMStatus.OFF:
                    ops.Off(vmName);
                    break;
            }
            _logger.Debug(string.Format("[PowerOps]End to power ops VM name [{0}]", vmName));
        }

        public void PowerOn(string macAddress)
        {
            _logger.Debug("[PowerOn]PowerOn for " + macAddress);
            PowerOps(macAddress, VMStatus.ON);
        }

        public void PowerOff(string macAddress)
        {
            _logger.Debug("[PowerOff]PowerOff for " + macAddress);
            PowerOps(macAddress, VMStatus.OFF);
        }

        public void Restore(string macAddress)
        {
            _logger.Debug("[Restore]Restore for " + macAddress);
            var hostInfo = _vmInfoRepository.GetVMHostInfoByVMMacAddress(macAddress);
            if (hostInfo == null)
            {
                _logger.Debug(string.Format("[Restore]Fail to get hostInfo for macaddress [{0}]", macAddress));
                return;
            }

            var vmName = _vmInfoRepository.GetVMNameByMacAddress(macAddress);
            _logger.Debug(string.Format("[Restore]Get VM name [{0}] from mac address [{1}]", vmName, macAddress));

            var account = new VMAccount(hostInfo.UserName, hostInfo.Password);
            var ops = new SnapshotOps(account, hostInfo.Name, hostInfo.URL);

            _logger.Debug(string.Format("[Restore]Start to revert VM name [{0}]", vmName));
            ops.Revert(vmName, ConfigurationManager.AppSettings["SnapshotName"]);
            _logger.Debug(string.Format("[Restore]End to revert VM name [{0}]", vmName));
        }

        public bool IsPowerOn(string macAddress)
        {
            _logger.Debug("[IsPowerOn]Verify if power on for " + macAddress);
            var hostInfo = _vmInfoRepository.GetVMHostInfoByVMMacAddress(macAddress);
            if (hostInfo == null)
            {
                _logger.Debug(string.Format("[IsPowerOn]Fail to get hostInfo for macaddress [{0}]", macAddress));
                return false;
            }

            var vmName = _vmInfoRepository.GetVMNameByMacAddress(macAddress);
            _logger.Debug(string.Format("[IsPowerOn]Get VM name [{0}] from mac address [{1}]", vmName, macAddress));
            
            var account = new VMAccount(hostInfo.UserName, hostInfo.Password);
            var ops = new PropertyOps(account, hostInfo.Name, hostInfo.URL);
            return ops.IsPowerOn(vmName);
        }

        public void UpdateVMInfo()
        {
            _logger.Debug("[UpdateVMInfo]Update VM info");
            //Get all VMHost info from DB first
            var allVMHostInfoes = _vmInfoRepository.GetAllVMHostInfo().ToArray();
            _logger.Debug(string.Format("[UpdateVMInfo]Get [{0}] VM Host from SDK", allVMHostInfoes.Length));

            foreach (var hostInfo in allVMHostInfoes)
            {
                VMAccount account = new VMAccount(hostInfo.UserName, hostInfo.Password);
                PropertyOps ops = new PropertyOps(account, hostInfo.Name, hostInfo.URL);
                var runTimeInfoes = ops.GetRuntimeInfo();

                _logger.Debug(string.Format("[UpdateVMInfo]Start to fetch all VM info from VMHost [{0}] ", hostInfo.Name));

                //Get information from every VMs in this VM Host
                foreach (var vmInfo in runTimeInfoes)
                {
                    _logger.Debug(string.Format("[UpdateVMInfo]Start to fetch info for VM [{0}] ", vmInfo.Name));
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
                    _logger.Debug(string.Format("[UpdateVMInfo]Update VM [{0}] info to DB", vmInfo.Name));
                    _vmInfoRepository.UpdateVMInfo(vmInfoInDB);
                }
            }
        }
    }
}
