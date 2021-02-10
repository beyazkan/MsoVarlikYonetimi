using MsoSocket.Library;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
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
        List<Client> mClients;
        public bool isRunning { get; set; }
        Logger logger = LogManager.GetCurrentClassLogger();
        public EventHandler<ClientConnectedEventArgs> RaiseClientConnectedEvent;

        public MsoSocketServer(IPAddress iPAddress = null, int port = 2684)
        {
            mClients = new List<Client>();

            if(iPAddress == null)
            {
                mIp = IPAddress.Any;
            }

            if(port <= 0 || port >= 65536)
            {
                mPort = 2684;
            }
            else
            {
                mPort = port;
            }
        }

        protected virtual void OnRaiseClientConnectedEvent(ClientConnectedEventArgs e)
        {
            EventHandler<ClientConnectedEventArgs> handler = RaiseClientConnectedEvent;

            if(handler != null)
            {
                handler(this, e);
            }
        }

        public void Start()
        {
            try
            {
                // TcpListener Nesnesi
                mListener = new TcpListener(mIp, mPort);
                mListener.Start();
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
                Client tempClient = new Client();
                var client = await mListener.AcceptTcpClientAsync();
                tempClient.client = client;
                mClients.Add(tempClient);

                logger.Log(LogLevel.Info, string.Format("İstemci başarılı bir şekilde bağlandı. Sayısı {0} - Ip Adresi : {1}", mClients.Count, client.Client.RemoteEndPoint));
                // Client Bağlantı Eventi
                ClientConnectedEventArgs eaClientConnected;
                eaClientConnected = new ClientConnectedEventArgs(client.Client.RemoteEndPoint.ToString());
                OnRaiseClientConnectedEvent(eaClientConnected);
                Send(client, PackageType.Header);
                Send(client, Headers.Authorization);
                Listening(client);
            }
        }

        private async void Listening(TcpClient client)
        {
            NetworkStream networkStream = null;
            try
            {
                while (isRunning)
                {
                    networkStream = client.GetStream();

                    byte[] buff = new byte[1024];
                    //int nRet = await networkStream.ReadAsync(buff, 0, buff.Length);

                    switch ((PackageType)networkStream.ReadByte())
                    {
                        case PackageType.Header:
                            break;
                        case PackageType.Text:
                            break;
                        case PackageType.Object:
                            logger.Info("Nesne Bilgisi Geldi.");
                            BinaryFormatter binaryFormatter = new BinaryFormatter();
                            Computer computer = (Computer)binaryFormatter.Deserialize(networkStream);
                            logger.Info(string.Format("Bilgisayar Adi: {0}", computer.name));
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

                    /*if(nRet == 0)
                    {
                        //Disconnect işlemi
                    }*/
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }
            
        }

        private void Send(TcpClient client, PackageType packageType)
        {
            NetworkStream networkStream = client.GetStream();
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

        private void Send(TcpClient client, Headers headers)
        {
            NetworkStream networkStream = client.GetStream();
            byte[] buff = new byte[1024];

            try
            {
                networkStream.WriteByte((byte)headers);
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