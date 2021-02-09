using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MsoSocket.Library
{
    [Serializable]
    public class Computer
    {
        public string name { get; set; }
        public IPAddress[] ipAddr { get; set; }
        public string userName { get; set; }
    }
}
