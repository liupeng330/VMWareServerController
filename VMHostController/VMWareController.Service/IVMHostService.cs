using System.ServiceModel;

namespace VMWareController.Service
{
    [ServiceContract(Name = "VMHostService", Namespace = "http://confluence.real.com/")]
    public interface IVMHostService
    {
        [OperationContract]
        void PowerOn(string macAddress);

        [OperationContract]
        void PowerOff(string macAddress);

        [OperationContract]
        void Restore(string macAddress);

        [OperationContract]
        bool IsPowerOn(string macAddress);

        [OperationContract]
        void UpdateVMInfo();
    }
}
