using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using TPV.Controlador;
using TPV.Modelo;
using TPV.Vista;
namespace TPV.Controlador
{
    /// <summary>
    /// Contiene la lógica de los aspectos específicos del TPV y su uso
    /// </summary>
    public class TPVBase
    {
        private int _numTPV = 1; //Esto se define a mano desde aquí. Determina el número de TPV inicial si no se ha definido
        private int _numerosTeclado;
        public int NumerosTeclado { get => _numerosTeclado; set => _numerosTeclado = value; }
        private List<Usuario>? _listaUsuarios;
        public List<Usuario> ListaUsuarios { get => _listaUsuarios!; set => _listaUsuarios = value; }
        private Usuario? _usuarioActual;
        public Usuario? UsuarioActual { get => _usuarioActual; set => _usuarioActual = value; }
        private TPVObj _tpvCFG = null!;
        private TPVMaster _procesoMaster = null!;
        private Tarifa? _tarifaActual;
        public Tarifa? TarifaActual { get => _tarifaActual; set => _tarifaActual = value; }
        private PosicionVenta? _posicionVentaActual;
        public PosicionVenta? PosicionVentaActual { get => _posicionVentaActual; set => _posicionVentaActual = value; }
        private VentanaPrincipal? _ventanaPrincipal;
        private List<Seccion>? _secciones;
        public List<Seccion>? Secciones { get => _secciones; set => _secciones = value; }
        private List<Producto>? _productos;
        private List<Tarifa>? _tarifas;
        public List<Tarifa>? Tarifas { get => _tarifas; set => _tarifas = value; }

        /// <summary>
        /// Método que coordina las comprobaciones y operaciones de inicio del TPV
        /// </summary>
        public void InicioTPV()
        {
            CargarCfgTPV();
            CargarProductos();
            CargarTarifas();
            CargarSecciones();
            CargarUsuarioPredeterminado();
            CompruebaCierreCaja();
            CompruebaAperturaCaja();
            ControladorComun.BD!.ActualizarObjeto(ControladorComun.TiendaActual);
            if (_tpvCFG.IsTPVMaster)
            {
                Task.Run(() =>
                {
                    _procesoMaster = new TPVMaster();
                    _procesoMaster.Iniciar();
                });
            }


            _ventanaPrincipal = new VentanaPrincipal();
            BloqueaTPV();
            _ventanaPrincipal.Show();
        }

        private bool CompruebaAperturaCaja()
        {

            return true;
        }

        private bool AbrirCaja()
        {
            return true;
        }

        private void CargarCfgTPV()
        {
            if (ControladorComun.BD!.ContarObjetos<TPVObj>() < 1)
            {
                this._tpvCFG = new TPVObj(0, 1, true, 0, 0);
                ControladorComun.BD!.PersistirObjeto(_tpvCFG);
            }
            else
            {
                this._tpvCFG = ControladorComun.BD!.BuscarObjetosIntAndInt<TPVObj>("CodTienda", ControladorComun.TiendaActual!.CodTienda, "NumTPV", _numTPV)[0];
            }
        }

        private void CargarUsuarioPredeterminado()
        {
            if (ControladorComun.BD!.ContarObjetos<Usuario>() < 1)
            {
                ControladorComun.BD!.PersistirObjeto(new Usuario(0, "7777", 0, true, "admin", new BitmapImage(new Uri("C:\\Proyecto 2DAM\\TPV\\TPV-JMCP\\TPV-WINDOWS\\Recursos\\Imagenes\\profile.png"))));
                ControladorComun.BD!.PersistirObjeto(new Usuario(1, "1111", 0, false, "user", new BitmapImage(new Uri("C:\\Proyecto 2DAM\\TPV\\TPV-JMCP\\TPV-WINDOWS\\Recursos\\Imagenes\\pngegg48_.png"))));
            }
        }

        public void CargarTarifas()
        {
            if (ControladorComun.BD!.ContarObjetos<Tarifa>() < 1)
            {
                this._tarifaActual = new Tarifa(0, 0, "Tarifa Base PVP", 0, 21, "PVP sin IVA incluido");
                Tarifa tarifaAlt = new Tarifa(1, "Tarifa Oferta PVP", 0, 21, "PVP Oferta sin IVA incluido");

                this._tarifaActual.AnadirProducto(0, 600, true);
                tarifaAlt.AnadirProducto(0, 515, true);
                this._tarifaActual.AnadirProducto(1, 1200, true);
                tarifaAlt.AnadirProducto(1, 1100, true);
                this._tarifaActual.AnadirProducto(2, 219, true);
                tarifaAlt.AnadirProducto(2, 190, true);
                this._tarifaActual.AnadirProducto(3, 100, true);
                tarifaAlt.AnadirProducto(3, 90, true);
                ControladorComun.BD!.ActualizarObjeto(_tarifaActual);
                ControladorComun.BD!.ActualizarObjeto(tarifaAlt);
            }
            else
            {
                this._tarifaActual = ControladorComun.BD!.BuscarObjetosIntAndInt<Tarifa>("CodTarifa", _tpvCFG.TarifaDefecto, "CodTienda", _tpvCFG.CodTienda)[0];
                ControladorComun.TiendaActual!.TarifaDefecto = _tarifaActual;
            }
            ControladorComun.TiendaActual!.TarifaDefecto = _tarifaActual;
        }

        public List<Tarifa> ListaTarifas()
        {
            List<Tarifa> _listarifa = new List<Tarifa>();
            _listarifa = ControladorComun.BD!.BuscarObjetosInt<Tarifa>("CodTienda", _tpvCFG.CodTienda).ToList();
            return _listarifa;
        }

        private void CargarSecciones()
        {
            if (ControladorComun.BD!.ContarObjetos<Seccion>() < 1)
            {
                _secciones = new List<Seccion>();
                _secciones.Add(new Seccion(0, 0, "Auriculares", 0));
                _secciones[0].AddProducto(_productos![0]);
                _secciones[0].AddProducto(_productos![1]);


                _secciones.Add(new Seccion(1, 1, "DACs", 0));
                _secciones[1].AddProducto(_productos![2]);
                _secciones[1].AddProducto(_productos![3]);

                foreach (Seccion s in _secciones)
                {
                    ControladorComun.BD!.PersistirObjeto(s);
                }
            }
            _secciones = ControladorComun.BD!.BuscarObjetosInt<Seccion>("CodTienda", _tpvCFG.CodTienda).ToList();
        }



        private void CargarProductos()
        {
            if (ControladorComun.BD!.ContarObjetos<Producto>() < 1)
            {
                ControladorComun.BD!.PersistirObjeto(new Producto(0, 0, "Stax SR-507", "Auricular electrostático", _tpvCFG.CodTienda, ControladorComun.DameImagenProducto("sr507.png")));
                ControladorComun.BD!.PersistirObjeto(new Producto(1, "Audeze LCD2", "Auricular magnetoplanar", _tpvCFG.CodTienda, ControladorComun.DameImagenProducto("lcd2.png")));
                ControladorComun.BD!.PersistirObjeto(new Producto(2, "Cayin RU-6", "DAC portátil R2R", _tpvCFG.CodTienda, ControladorComun.DameImagenProducto("ru6.png")));
                ControladorComun.BD!.PersistirObjeto(new Producto(3, "Topping E30", "DAC estacionario Delta-Sigma", _tpvCFG.CodTienda, ControladorComun.DameImagenProducto("e30.jpg")));
            }
            _productos = ControladorComun.BD!.BuscarObjetosInt<Producto>("CodTienda", _tpvCFG.CodTienda).ToList();
        }

        public string GeneraTicket()
        {
            string numTicket = RecibirNumTicketDesdeMaster();
            return numTicket;
        }

        public string RecibirNumTicketDesdeMaster()
        {
            try
            {
                TcpClient client = new TcpClient();
                client.Connect(ControladorComun.TiendaActual!.IPTPVMaster, 12700);
                NetworkStream stream = client.GetStream();
                MemoryStream ms = new MemoryStream();
                byte[] buffer = new byte[1024];
                int bytesLeidos;
                while ((bytesLeidos = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, bytesLeidos);
                }
                byte[] msgEncriptado = ms.ToArray();           
                client.Close();

                return DecryptData(msgEncriptado, ControladorComun.ClaveCompartida, ControladorComun.Iv); ;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al conectar con TPVMaster: " + ex.Message);
                return string.Empty;
            }
        }

        private string DecryptData(byte[] data, byte[] key, byte[] iv)
        {

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.Padding = PaddingMode.PKCS7;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(data))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }

        public bool FinTPV()
        {
            if (this._tpvCFG.IsTPVMaster && this._procesoMaster != null)
            {
                if (!_procesoMaster!.IsRunning)
                {
                    return false;
                }
                else
                {
                    this._procesoMaster.Parar();
                }
            }
            return false;
        }

        public void BloqueaTPV()
        {
            new VentanaTecladoNumericoUsuario(true).ShowDialog();
            //_usuarioActual = ControladorComun.BD!.BuscarObjetosIntAndInt<Usuario>("Clave", _numerosTeclado, "CodTienda", _tpvCFG.CodTienda)[0]; //kk
            _ventanaPrincipal!.ActualizaInfoUsuario();
        }

        public bool CompruebaCierreCaja()
        {
            return true;
        }

        public void TestVariados()
        {

        }
        public bool ExistenTicketCerrados()
        {
            List<Ticket> ticketsCerrados = ControladorComun.BD!.BuscarObjetosBool<Ticket>("Cerrado", true).ToList();
            return ticketsCerrados.Any();
        }

        public bool CompruebaClave(string clave)
        {
            List<Usuario> usuarios = ControladorComun.BD!.BuscarObjetosStringAndInt<Usuario>("Clave", clave, "CodTienda", _tpvCFG.CodTienda).ToList();
            if (usuarios.Count > 0)
            {
                return usuarios.Any(x => x.EsActivo);
            }
            return false;
        }

        public string ElegirImagenAvatar()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog.Title = "Selecciona imagen de avatar";

            if (openFileDialog.ShowDialog() == true)
            {
                return openFileDialog.FileName;
            }

            return string.Empty;
        }

        public BitmapImage? CargarImagen(string rutaImagen) //Para que el usuario suba su avatar
        {
            if (!string.IsNullOrEmpty(rutaImagen))
            {
                BitmapImage imagen = new BitmapImage();
                imagen.BeginInit();
                imagen.UriSource = new Uri(rutaImagen);
                imagen.EndInit();
                return imagen;
            }
            return null;
        }
        public void AgregarProductoASeccion(Producto producto, int CodSeccion)
        {
            Seccion seccion = _secciones?.FirstOrDefault(s => s.CodSeccion == CodSeccion)!;
            if (seccion != null)
            {
                seccion.AddProducto(producto);
                ControladorComun.BD!.PersistirObjeto(seccion);
            }
        }
        public void AgregarProductoASeccion(int CodSeccion, int CodProducto)
        {
            Seccion seccion = _secciones?.FirstOrDefault(s => s.CodSeccion == CodSeccion)!;
            if (seccion != null)
            {
                seccion.AddProducto(ControladorComun.BD!.BuscarObjetosInt<Producto>("CodProducto", CodProducto)[0]);
                ControladorComun.BD!.PersistirObjeto(seccion);
            }
        }

        public void InsertarLineaVenta(Producto producto)
        {
            if (_posicionVentaActual != null)
            {
                _posicionVentaActual = new PosicionVenta(0, 0, _tarifaActual!.CodTarifa);
            }
        }

        private static byte[] DecryptData(byte[] data, byte[] key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.Mode = CipherMode.ECB;
                aesAlg.Padding = PaddingMode.None;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream())
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Write))
                    {
                        csDecrypt.Write(data, 0, data.Length);
                        csDecrypt.FlushFinalBlock();
                        return msDecrypt.ToArray();
                    }
                }
            }
        }
    }
}
