using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public bool isRunning { get; set; }
        Logger logger = LogManager.GetCurrentClassLogger();

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

        public async void Start()
        {
            try
            {
                // TcpListener Nesnesi
                mListener = new TcpListener(mIp, mPort);
                isRunning = true;

                // Dinleme Metodu
                AcceptClients();
            }
            catch (Exception e)
            {
                logger.Debug(e.Message);
            }
        }

        private async void AcceptClients()
        {
            while (isRunning)
            {
                var client = await mListener.AcceptTcpClientAsync();
                mClients.Add(client);

                logger.Log(LogLevel.Info, string.Format("İstemci başarılı bir şekilde bağlandı. Sayısı {0} - Ip Adresi : {1}", mClients.Count, client.Client.RemoteEndPoint));
            }
        }
    }
}