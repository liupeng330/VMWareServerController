using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VimyyApi;

namespace VMwarevSphere_Utils
{
    public class PropertyOps
    {
        private VMAccount _account;
        private string _hostName;
        private string _hostURL;

        public PropertyOps(VMAccount account, string hostName, string hostURL)
        {
            this._account = account;
            this._hostName = hostName;
            this._hostURL = hostURL;
        }

        private Hashtable GenerateArgs()
        {
            Hashtable args = new Hashtable();
            args["username"] = this._account.Username;
            args["password"] = this._account.Password;
            args["url"] = this._hostURL;

            return args;
        }

        public bool IsPowerOn(string vmName)
        {
            var cb = AppUtil.AppUtil.initialize("VmState", GenerateArgs());
            VirtualMachineRuntimeInfo runtimeInfo;
            try
            {
                cb.connect();
                var vmmor = cb.getServiceUtil().GetDecendentMoRef(null,
                    "VirtualMachine",
                    vmName);

                runtimeInfo = cb.getServiceUtil().GetDynamicProperty(vmmor, "runtime") as VirtualMachineRuntimeInfo;
                if (runtimeInfo == null)
                {
                    throw new NullReferenceException("Convert to runtimeinfo object failed!!");
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cb.disConnect();
            }
            return runtimeInfo.powerState == VirtualMachinePowerState.poweredOn;
        }

        public IEnumerable<VMInfomation> GetRuntimeInfo()
        {
            var cb = AppUtil.AppUtil.initialize("Browser"
                                            , null
                                            , GenerateArgs());
            VMPropertyOps ops = new VMPropertyOps(cb);
            try
            {
                cb.connect();
                return ops.GetProperties();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                cb.disConnect();
            }
        }
    }
}
