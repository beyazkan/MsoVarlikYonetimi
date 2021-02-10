using MsoSocket.Library;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MsoSocket
{
    public class MsoSocketSpecClient
    {
        IPAddress mIp;
        int mPort;
        TcpClient mClient;
        Logger logger = LogManager.GetCurrentClassLogger();
        public bool isRunning { get; set; }
        
        public MsoSocketSpecClient(IPAddress ipAddr = null, int port = 2684)
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
                isRunning = true;
                await mClient.ConnectAsync(mIp, mPort);
                logger.Info(string.Format("{0}:{1} ip adresli sunucuya bağlanıldı.", mIp, mPort));

                await Listening(mClient);
            }
            catch (Exception e)
            {
                logger.Error(e.ToString());
                logger.Error(e.Message);
            }
        }

        public void Disconnect()
        {
            mClient.Close();
            isRunning = false;
            logger.Info("Bağlantı Kesildi.");
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
                    //readByteCount = await networkStream.ReadAsync(buff, 0, buff.Length);
                    // Gelen Veriler

                    switch ((PackageType)networkStream.ReadByte())
                    {
                        case PackageType.Header:
                            logger.Debug("Header Geldi.");
                            GetHeader(networkStream);
                            break;
                        case PackageType.Text:
                            break;
                        case PackageType.Object:
                            break;
                        case PackageType.Command:
                            break;
                        case PackageType.Notification:
                            break;
                        case PackageType.File:
                            break;
                        case PackageType.Image:
                            break;
                        case PackageType.Sound:
                            break;
                        case PackageType.Video:
                            break;
                        default:
                            break;
                    }
                    /*if (readByteCount <= 0)
                    {
                        Console.WriteLine("Disconnected from server.");
                        tcpClient.Close();
                        break;
                    }*/

                    Array.Clear(buff, 0, buff.Length);
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                throw;
            }
        }

        private void GetHeader(NetworkStream networkStream)
        {
            switch ((Headers)networkStream.ReadByte())
            {
                case Headers.Connect:
                    break;
                case Headers.Disconnect:
                    break;
                case Headers.Authorization:
                    logger.Info("Kimlik Doğrulama");
                    Send(networkStream, PackageType.Object);
                    sendComputerInformation(networkStream);
                    networkStream.Flush();
                    break;
                case Headers.ReConnect:
                    break;
                default:
                    break;
            }
        }

        private void sendComputerInformation(NetworkStream networkStream)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            Computer computer = new Computer {
                name = Environment.MachineName,
                userName = Environment.UserName,
                ipAddr = Dns.GetHostByName(Environment.MachineName).AddressList
            };

            binaryFormatter.Serialize(networkStream, computer);

        }

        private void Send(NetworkStream networkStream, PackageType packageType)
        {
            byte[] buff = new byte[1024];

            try
            {
                networkStream.WriteByte((byte)packageType);
                networkStream.Flush();
                Array.Clear(buff, 0, buff.Length);
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }
        }
    }
}
