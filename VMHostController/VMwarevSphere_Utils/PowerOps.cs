using System;
using System.Collections;

namespace VMwarevSphere_Utils
{
    public class PowerOps
    {
        private VMAccount _account;
        private string _hostName;
        private string _hostURL;

        public PowerOps(VMAccount account, string hostName, string hostURL)
        {
            this._account = account;
            this._hostName = hostName;
            this._hostURL = hostURL;
        }

        private Hashtable GenerateArgs(string vmName, VMStatus setStatus)
        {
            Hashtable args = new Hashtable();
            args["username"] = this._account.Username;
            args["password"] = this._account.Password;
            args["hostname"] = this._hostName;
            args["url"] = this._hostURL;
            args["vmname"] = vmName;
            args["operation"] = setStatus.GetStringValue();

            return args;
        }

        public void On(string vmName)
        {
            DoOps(vmName, VMStatus.ON);
        }

        public void Off(string vmName)
        {
            DoOps(vmName, VMStatus.OFF);
        }

        private void DoOps(string vmName, VMStatus status)
        {
            var cb = AppUtil.AppUtil.initialize("VmPowerOps"
                , VMPowerOps.constructOptions()
                , GenerateArgs(vmName, status));
            VMPowerOps ops = new VMPowerOps(cb);

            try
            {
                cb.connect();
                ops.DoPowerOps();
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
