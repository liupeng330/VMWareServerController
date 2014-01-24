using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMwarevSphere_Utils
{
    public class VMInfomation
    {
        public string Name { get; set; }
        public string IPAddress { get; set; }
        public string MacAddress { get; set; }
        public VMStatus Status { get; set; }
        public DateTime LastBootTime { get; set; }
    }
}
