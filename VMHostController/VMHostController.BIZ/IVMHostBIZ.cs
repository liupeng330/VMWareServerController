namespace VMHostController.BIZ
{
    public interface IVMHostBIZ
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ipAddress"></param>
        void PowerOn(string macAddress);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ipAddress"></param>
        void PowerOff(string macAddress);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ipAddress"></param>
        void Restore(string macAddress);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        bool IsPowerOn(string macAddress);

        /// <summary>
        /// 
        /// </summary>
        void UpdateVMInfo();
    }
}
