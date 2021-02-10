using MsoSocket;
using System;
using System.Collections.Generic;
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

namespace MsoClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MsoSocketClient socketClient;

        public MainWindow()
        {
            InitializeComponent();
            socketClient = new MsoSocketClient();
            
        }

        private void btnBaslat_Click(object sender, RoutedEventArgs e)
        {
            if (!socketClient.isRunning)
            {
                btnBaslat.Content = "Bağlantıyı Kes";
                _ = socketClient.ConnectToServer();
            }
            else
            {
                socketClient.isRunning = false;
                socketClient.Disconnect();
                btnBaslat.Content = "Bağlan";
            }
            
        }
    }
}
