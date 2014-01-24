using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMHostController.DAL.Model;

namespace VMHostController.DAL
{
    /// <summary>
    /// 
    /// </summary>
    public interface IVMInfoRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="vmInfo"></param>
        void UpdateVMInfo(VMInfo vmInfo);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hostName"></param>
        /// <returns></returns>
        VMHostInfo GetVMHostInfoByName(string hostName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        VMHostInfo GetVMHostInfoByID(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<VMHostInfo> GetAllVMHostInfo();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="macAddress"></param>
        /// <returns></returns>
        VMHostInfo GetVMHostInfoByVMMacAddress(string macAddress);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="macAddress"></param>
        /// <returns></returns>
        string GetVMNameByMacAddress(string macAddress);
    }
}
