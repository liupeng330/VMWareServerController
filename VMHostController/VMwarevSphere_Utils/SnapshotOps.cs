using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMwarevSphere_Utils
{
    public class SnapshotOps
    {
        private VMAccount _account;
        private string _hostName;
        private string _hostURL;

        public SnapshotOps(VMAccount account, string hostName, string hostURL)
        {
            this._account = account;
            this._hostName = hostName;
            this._hostURL = hostURL;
        }

        private Hashtable GenerateArgs(string vmName, string snapshotName, SnapshotOpsEnum setStatus)
        {
            Hashtable args = new Hashtable();
            args["username"] = this._account.Username;
            args["password"] = this._account.Password;
            args["hostname"] = this._hostName;
            args["url"] = this._hostURL;
            args["vmname"] = vmName;
            args["operation"] = setStatus.GetStringValue();
            args["snapshotname"] = snapshotName;

            return args;
        }

        public void Revert(string vmName, string snapshotName)
        {
            DoSnapshotOps(vmName, snapshotName, SnapshotOpsEnum.Revert);
        }

        public IEnumerable<string> List(string vmName)
        {
            return DoSnapshotOps(vmName, string.Empty, SnapshotOpsEnum.List);
        }

        private List<string> DoSnapshotOps(string vmName, string snapshotName, SnapshotOpsEnum ops)
        {
            List<string> snapshotNames = new List<string>();
            var cb = AppUtil.AppUtil.initialize("VMSnapshot"
                        , VMSnapshotOps.constructOptions()
                       , GenerateArgs(vmName, snapshotName, ops));
            var vmsnapshotOps = new VMSnapshotOps(cb);
            if (vmsnapshotOps.CustomValidation())
            {
                try
                {
                    cb.connect();
                    snapshotNames = vmsnapshotOps.DoSnapshotOps();
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
            else
            {
                throw new Exception("Validation is failed for vm snapshot ops!!");
            }

            return snapshotNames;
        }
    }
}
