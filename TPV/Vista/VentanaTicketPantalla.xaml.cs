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
using TPV.Controlador;

namespace TPV.Vista
{
    /// <summary>
    /// Lógica de interacción para VentanaTicketPantalla.xaml
    /// </summary>
    public partial class VentanaTicketPantalla : Window
    {
        public VentanaTicketPantalla()
        {
            InitializeComponent();
            this.DataContext = ControladorComun.TiendaActual;
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnCancelarCobro_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
