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
using TPV.Modelo;

namespace TPV.Vista
{
    /// <summary>
    /// Lógica de interacción para VentanaPrincipal.xaml
    /// </summary>
    public partial class VentanaPrincipal : Window
    {
        public VentanaPrincipal()
        {
            InitializeComponent();
            GenerarTabItems(ControladorComun.TpvBase!.Secciones!);
            tab1M.Visibility = Visibility.Collapsed;
            gridProductos.Visibility = Visibility.Collapsed;
            this.DataContext = ControladorComun.TpvBase;
            tabControl.SelectedItem = tabControl.Items[1]; //Porque el 0 es la muestra estática
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ControladorComun.CerrarPrograma();
        }

        private void btnCobrar_Click(object sender, RoutedEventArgs e)
        {
            new VentanaTicketPantalla().ShowDialog();
            //ControladorComun.TpvBase!.GeneraTicket();
            ActualizaInfoUsuario();
        }

        private void btnFuncEnc_Click(object sender, RoutedEventArgs e)
        {
            new VentanaEncargado().ShowDialog();
            ActualizaInfoUsuario();
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            ControladorComun.CerrarPrograma();
        }
        public void ActualizaInfoUsuario()
        {
            lblUsuario.Content = ControladorComun.TpvBase!.UsuarioActual!.Nombre;
            imgAvatar.Source = ControladorComun.TpvBase!.UsuarioActual!.Avatar;
            lblTarifa.Content = ControladorComun.TpvBase!.TarifaActual!.Nombre;
            if (ControladorComun.TpvBase!.UsuarioActual!.EsEncargado)
            {
                btnTarifa.Width = 128;
                btnFuncEnc.Visibility = Visibility.Visible;
                lblUsuario.Foreground = Brushes.Red;
            }
            else
            {
                btnTarifa.Width = 271;
                btnFuncEnc.Visibility = Visibility.Collapsed;
                lblUsuario.Foreground = Brushes.Black;
            }
        }

        private void imgExit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ControladorComun.CerrarPrograma();
        }

        private void imgAvatar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ControladorComun.TpvBase!.BloqueaTPV();
            ActualizaInfoUsuario();
        }

        private void btnTarifa_Click(object sender, RoutedEventArgs e)
        {
            new VentanaTarifa().ShowDialog();
            ActualizaInfoUsuario();
        }

        private void btnAcciones_Click(object sender, RoutedEventArgs e)
        {
            VentanaAcciones ventanaAcciones = new VentanaAcciones();
            this.Show();
            ventanaAcciones.ShowDialog();
        }

        public void GenerarTabItems(List<Seccion> secciones)
        {
            foreach (Seccion seccion in secciones)
            {
                TabItem tabItem = new TabItem();
                tabItem.Header = seccion.Nombre;

                Grid gridProductos = new Grid();
                gridProductos.Background = Brushes.AliceBlue;
                gridProductos.VerticalAlignment = VerticalAlignment.Center;
                gridProductos.HorizontalAlignment = HorizontalAlignment.Center;
                gridProductos.Width = 693;
                gridProductos.Height = 692;

                int rowCount = (seccion.Productos!.Count + 6) / 7; // Hallar el nº de filas
                for (int i = 0; i < rowCount; i++)
                {
                    gridProductos.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(98) });
                }

                int columnCount = Math.Min(seccion.Productos.Count, 7); // Máximo de 7 columnas
                for (int i = 0; i < columnCount; i++)
                {
                    gridProductos.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(98) });
                }

                int productIndex = 0;
                for (int row = 0; row < rowCount; row++)
                {
                    for (int col = 0; col < columnCount; col++)
                    {
                        if (productIndex <= seccion.Productos.Count)
                        {
                            Producto producto = seccion.Productos[productIndex];

                            Image image = new Image();
                            image.Source = producto.Imagen;
                            image.Width = 98;
                            image.Height = 98;
                            Grid.SetRow(image, row);
                            Grid.SetColumn(image, col);
                            image.MouseLeftButtonDown += (sender, e) =>
                            {
                                ControladorComun.TpvBase!.InsertarLineaVenta(producto);
                                MessageBox.Show("Producto añadido a la venta");
                            };
                            gridProductos.Children.Add(image);
                            productIndex++;
                        }
                    }
                }

                tabItem.Content = gridProductos;
                tabControl.Items.Add(tabItem);
            }
        }

    }
}
