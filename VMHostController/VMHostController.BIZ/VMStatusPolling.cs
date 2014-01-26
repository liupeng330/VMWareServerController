using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using log4net;
using log4net.Core;
using VMHostController.DAL;
using VMHostController.DAL.Model;
using VMwarevSphere_Utils;

namespace VMHostController.BIZ
{
    public static class VMStatusPolling
    {
        private readonly static Timer _timer = new Timer(int.Parse(ConfigurationManager.AppSettings["VMStatusFrequency"]));
        private readonly static VMHostBIZ _vmHostBiz = new VMHostBIZ();
        private static readonly ILog _logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static VMStatusPolling()
        {
            _timer.Elapsed += _timer_Elapsed;
        }

        static void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _logger.Info("Start to update VM");
            _vmHostBiz.UpdateVMInfo();
            _logger.Info("End to update VM");
        }

        public static void StartTimer()
        {
            _logger.Info("Open timer");
           _timer.Start(); 
        }

        public static void StopTimer()
        {
            _logger.Info("Close timer");
            _timer.Stop();
        }
    }
}
