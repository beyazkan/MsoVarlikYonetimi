using MsoSocket;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MsoServer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MsoSocketServer socketServer;

        public MainWindow()
        {
            InitializeComponent();
            socketServer = new MsoSocketServer();
            socketServer.RaiseClientConnectedEvent += HandleClientConnected;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (socketServer.isRunning)
            {
                socketServer.isRunning = false;
                btnStart.Content = "Başlat";
                TxtConsole.AppendText(string.Format("Sunucu Durduruldu. {0}", Environment.NewLine));
            }
            else
            {
                socketServer.Start();
                btnStart.Content = "Durdur";
                TxtConsole.AppendText(string.Format("Sunucu Başlatıldı.{0}", Environment.NewLine));
            }
        }

        void HandleClientConnected(object sender, ClientConnectedEventArgs ccea)
        {
            TxtConsole.AppendText(string.Format("{0} - New client connected: {1}{2}", DateTime.Now, ccea.NewClient, Environment.NewLine));
        }
    }
}
