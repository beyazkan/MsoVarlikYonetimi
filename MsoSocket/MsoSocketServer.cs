using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MsoSocket
{
    public class MsoSocketServer
    {
        // Tanımlamalar
        IPAddress mIp;
        int mPort;
        TcpListener mListener;
        List<TcpClient> mClients;

        public MsoSocketServer(IPAddress iPAddress = null, int port = 2684)
        {
            mClients = new List<TcpClient>();

            if(iPAddress == null)
            {
                mIp = IPAddress.Any;
            }

            if(port <= 0 && port >= 65536)
            {
                mPort = 2684;
            }


        }
    }
}