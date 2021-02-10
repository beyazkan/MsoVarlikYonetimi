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

namespace MsoSpecClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MsoSocketSpecClient socketClient;

        public MainWindow()
        {
            InitializeComponent();
            socketClient = new MsoSocketSpecClient();
        }

        private void btnBaslat_Click(object sender, RoutedEventArgs e)
        {
            if (!socketClient.isRunning) {
                _= socketClient.ConnectToServer();
                btnBaslat.Content = "Bağlantıyı Kes";
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
