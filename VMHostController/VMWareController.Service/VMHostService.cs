using VMHostController.BIZ;

namespace VMWareController.Service
{
    public class VMHostService : IVMHostService
    {
        private readonly VMHostBIZ _vmHostBiz = new VMHostBIZ();

        public void PowerOn(string macAddress)
        {
            _vmHostBiz.PowerOn(macAddress);
        }

        public void PowerOff(string macAddress)
        {
            _vmHostBiz.PowerOff(macAddress);
        }

        public void Restore(string macAddress)
        {
            _vmHostBiz.Restore(macAddress);
        }

        public bool IsPowerOn(string macAddress)
        {
            return _vmHostBiz.IsPowerOn(macAddress);
        }

        public void UpdateVMInfo()
        {
            _vmHostBiz.UpdateVMInfo();
        }
    }
}
