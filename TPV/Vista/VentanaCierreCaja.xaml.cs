using System.Windows;

namespace TPV.Vista
{
    /// <summary>
    /// Lógica de interacción para los cierres de caja
    /// </summary>
    public partial class VentanaCierreCaja : Window
    {
        public VentanaCierreCaja()
        {
            InitializeComponent();
        }

        private void btnCierreCaja_Click(object sender, RoutedEventArgs e)
        {

            this.Close();
        }
    }
}
