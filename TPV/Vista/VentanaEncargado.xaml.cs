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
using System.Windows.Shapes;

namespace TPV.Vista
{
    /// <summary>
    /// Lógica de interacción para VentanaEncargado.xaml
    /// </summary>
    public partial class VentanaEncargado : Window
    {
        public VentanaEncargado()
        {
            InitializeComponent();
        }

        private void btnAtras_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAdmon_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAnulaciones_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
