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
    /// Teclado numérico que sirve tanto para validar claves de usuario como para introducir números
    /// </summary>
    public partial class VentanaTecladoNumericoUsuario : Window
    {
        private string _numeros;
        private bool _esClave;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="esClave">True si se va a usar para validar claves de usuario. False si se va a usar para recoger valores numéricos</param>
        public VentanaTecladoNumericoUsuario(bool esClave)
        {
            InitializeComponent();
            _numeros = string.Empty;
            _esClave = esClave;
            if (_esClave)
            {
                VentanaTecladoNumericoUsuario1.Title = "Introduzca su clave de usuario";
                lblVisor.Visibility = Visibility.Visible;
            }
            else
            {
                VentanaTecladoNumericoUsuario1.Title = "Introduzca el número";
                lblVisor.Visibility = Visibility.Visible;
            }
        }
        private void ActualizarVisor()
        {
            if (_numeros.Length < 1)
            {
                lblVisor.Visibility = Visibility.Collapsed;
            }
            else
            {
                lblVisor.Visibility = Visibility.Visible;
                if (_esClave)
                {
                    lblVisor.Content = new string('*', _numeros.Length);
                }
                else
                {
                    lblVisor.Content = _numeros;
                }
            }
            
        }
        private void btnBorrar_Click(object sender, RoutedEventArgs e)
        {
            if (_numeros.Length > 0)
            {
                _numeros = _numeros.Substring(0, _numeros.Length - 1);
            }
            ActualizarVisor();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (_esClave)
            {
                if (ControladorComun.TpvBase!.CompruebaClave(_numeros))
                {
                    ControladorComun.TpvBase!.UsuarioActual = ControladorComun.BD!.BuscarObjetosString<Usuario>("Clave", _numeros)[0];
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Clave incorrecta");
                    _numeros = string.Empty;
                    ActualizarVisor();
                }

            }
            else
            { 
                ControladorComun.TpvBase!.NumerosTeclado = Convert.ToInt16(_numeros);
                this.Close();
            }
            
        }

        private void btn7_Click(object sender, RoutedEventArgs e)
        {
            _numeros += "7";
            ActualizarVisor();
        }
        private void btn0_Click(object sender, RoutedEventArgs e)
        {
            _numeros += "0";
            ActualizarVisor();
        }
        private void btn3_Click(object sender, RoutedEventArgs e)
        {
            _numeros += "3";
            ActualizarVisor();
        }
        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            _numeros += "1";
            ActualizarVisor();
        }

        private void btn6_Click(object sender, RoutedEventArgs e)
        {
            _numeros += "6";
            ActualizarVisor();
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            _numeros += "2";
            ActualizarVisor();
        }

        private void btn5_Click(object sender, RoutedEventArgs e)
        {
            _numeros += "5";
            ActualizarVisor();
        }

        private void btn4_Click(object sender, RoutedEventArgs e)
        {
            _numeros += "4";
            ActualizarVisor();
        }

        private void btn9_Click(object sender, RoutedEventArgs e)
        {
            _numeros += "9";
            ActualizarVisor();
        }

        private void btn8_Click(object sender, RoutedEventArgs e)
        {
            _numeros += "8";
            ActualizarVisor();
        }
    }
}
