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
    /// Lógica de interacción para VentanaTarifa.xaml
    /// </summary>
    public partial class VentanaTarifa : Window
    {
        public VentanaTarifa()
        {
            InitializeComponent();
            cmbTarifa.ItemsSource = ControladorComun.TpvBase!.ListaTarifas();
        }

        private void btnTarifa_Click(object sender, RoutedEventArgs e)
        {
            if (cmbTarifa.SelectedItem != null)
            {
                ControladorComun.TpvBase!.TarifaActual = (Modelo.Tarifa)cmbTarifa.SelectedItem;
                this.Close();
            }
            else
            {
                MessageBox.Show("Debe seleccionar una tarifa","Información");
            }
        }
    }
}
