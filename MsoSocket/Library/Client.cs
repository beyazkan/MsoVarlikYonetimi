﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MsoSocket.Library
{
    public class Client
    {
        public TcpClient client { get; set; }
        public Computer computer { get; set; }
    }
}
