using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using VMHostController.DAL;
using VMHostController.DAL.Model;
using VMwarevSphere_Utils;

namespace VMHostController.BIZ
{
    public static class VMStatusPolling
    {
        private readonly static Timer _timer = new Timer(int.Parse(ConfigurationManager.AppSettings["VMStatusFrequency"]));
        private readonly static VMHostBIZ _vmHostBiz = new VMHostBIZ();

        static VMStatusPolling()
        {
            _timer.Elapsed += _timer_Elapsed;
        }

        static void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _vmHostBiz.UpdateVMInfo();
        }

        public static void StartTimer()
        {
           _timer.Start(); 
        }

        public static void StopTimer()
        {
            _timer.Stop();
        }
    }
}
