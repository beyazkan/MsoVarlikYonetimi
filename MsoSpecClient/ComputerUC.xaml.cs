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
    /// Interaction logic for ComputerUC.xaml
    /// </summary>
    public partial class ComputerUC : UserControl
    {
        public ComputerUC()
        {
            InitializeComponent();
        }

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            MessageBox.Show("Test");
        }
    }
}
