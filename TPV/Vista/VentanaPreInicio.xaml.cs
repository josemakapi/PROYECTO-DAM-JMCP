using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using TPV.Modelo;

namespace TPV.Vista
{
    /// <summary>
    /// Ventana que se carga al inicio del programa para seleccionar la tienda y conectarse a la BD
    /// </summary>
    public partial class VentanaPreInicio : Window
    {
        public VentanaPreInicio()
        {
            InitializeComponent();
            grpTienda.Visibility = Visibility.Hidden;
            btnTienda.Visibility = Visibility.Hidden;
        }

        private void txtBPuerto_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnConectar_Click(object sender, RoutedEventArgs e)
        {
            if (chkNube.IsChecked == true)
            {
                if (ControladorComun.IniciarBD(txtBUsuario.Text, txtBPassword.Password))
                {
                    ControladorComun.PreInicializaTienda();
                    OcultaControles();
                }
                else
                {
                    MessageBox.Show("Error al conectar a la BD. Corrija los datos e inténtelo de nuevo");
                }
            }
            else
            {
                if (ControladorComun.IniciarBD(txtBHost.Text, Convert.ToInt16(txtBPuerto.Text), txtBUsuario.Text, txtBPassword.Password))
                {
                    ControladorComun.PreInicializaTienda();
                    OcultaControles();
                }
                else
                {
                    MessageBox.Show("Error al conectar a la BD. Corrija los datos e inténtelo de nuevo");
                }
            }
        }

        private void OcultaControles()
        {
            cmbBTiendas.ItemsSource = ControladorComun.Tiendas;
            grpBD.Visibility = Visibility.Hidden;
            grpTienda.Visibility = Visibility.Visible;
            btnTienda.Visibility = Visibility.Visible;
            btnConectar.Content = "¡Conectado!";
            btnConectar.IsEnabled = false;
            chkNube.Visibility = Visibility.Hidden;
        }

        private void btnTienda_Click(object sender, RoutedEventArgs e)
        {
            if (cmbBTiendas.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar una tienda");
            }
            else
            {
                ControladorComun.TiendaActual = (Tienda)cmbBTiendas.SelectedItem;
                this.Close();
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            ControladorComun.CargarPantallaVentas();
        }

        private void chkNube_Checked(object sender, RoutedEventArgs e)
        {
            chkCheck();
        }

        private void chkNube_Unchecked(object sender, RoutedEventArgs e)
        {
            chkCheck();
        }

        private void chkCheck()
        {
            if (chkNube.IsChecked == true)
            {
                txtBHost.IsEnabled = false;
                txtBPuerto.IsEnabled = false;
            }
            else
            {
                txtBHost.IsEnabled = true;
                txtBPuerto.IsEnabled = true;
            }
        }
    }


}
