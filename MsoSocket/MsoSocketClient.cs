using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MsoSocket
{
    public class MsoSocketClient
    {
        IPAddress mIp;
        int mPort;
        TcpClient mClient;
        Logger logger = LogManager.GetCurrentClassLogger();

        public MsoSocketClient(IPAddress ipAddr = null, int port = 2684)
        {
            IPAddress.TryParse("127.0.0.1", out mIp);
            mPort = 2684;
        }

        public async Task ConnectToServer()
        {
            if(mClient == null)
            {
                mClient = new TcpClient();
            }

            try
            {
                await mClient.ConnectAsync(mIp, mPort);
                logger.Info(string.Format("{0}:{1} ip adresli sunucuya bağlanıldı.", mIp, mPort));

                await Listening(mClient);
            }
            catch (SocketException e)
            {
                _= ConnectToServer();
                logger.Info("Sunucuya bağlanamadı. 1 dk içinde tekrar denenecek.");
                Thread.Sleep(60000);
            }
            catch (Exception e)
            {
                logger.Error(e.ToString());
                logger.Error(e.Message);
            }
        }

        private async Task Listening(TcpClient tcpClient)
        {
            try
            {
                NetworkStream networkStream = tcpClient.GetStream();
                byte[] buff = new byte[1024];
                int readByteCount = 0;

                while (true)
                {
                    readByteCount = await networkStream.ReadAsync(buff, 0, buff.Length);

                    if (readByteCount <= 0)
                    {
                        Console.WriteLine("Disconnected from server.");
                        tcpClient.Close();
                        break;
                    }

                    // Gelen Veriler

                    Array.Clear(buff, 0, buff.Length);
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                throw;
            }
        }


    }
}
