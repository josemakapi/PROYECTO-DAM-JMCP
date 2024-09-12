using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using TPV.Controlador;
using TPV.Datos;
using TPV.Modelo;
using TPV.Vista;

namespace TPV.Controlador
{
    /// <summary>
    /// Superclase estática que controla el inicio y fin del programa y tiene recursos comunes a todos los elementos 
    /// del programa para evitar crear instancias innecesarias.
    /// </summary>
    public static class ControladorComun
    {
        public static TPVBase? TpvBase;
        public static BDMongo? BD;
        public static Tienda? TiendaActual;
        public static List<Tienda>? Tiendas;

        public static bool IniciarBD(string host, int puerto, string user, string pass)
        {
            BD = new BDMongo(host, puerto, user, pass);
            if (!BD.ConectarBDDirecta("TPVJMCP"))
            {
                return false;
            }
            return true;
        }
        public static bool IniciarBD(string user, string pass)
        {
            BD = new BDMongo(user, pass);
            if (!BD.ConectarBDCloud("TPVJMCP"))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Llamado desde el botón Conectar de la ventana de PreInicio para inicializar los parámetros de la tienda
        /// </summary>
        public static void PreInicializaTienda()
        {
            CargarTiendas();
        }

        public static bool CargarPantallaVentas()
        {
            TpvBase = new TPVBase();
            //await Task.Run(() => TpvBase.InicioTPV()); //Da petes
            TpvBase.InicioTPV();
            return true;
        }
        public static void CargarTiendas()
        {
            if (BD!.ContarObjetos<Tienda>() < 1)
            {
                //Tiendas![0].Logo = new BitmapImage(new Uri("ISOLOGOJMCP64.jpg", UriKind.Relative));
                Tiendas = new List<Tienda> { new Tienda(0, 0, 1, "192.168.1.200", "Tienda de audio de JMCP", "JMCP Audio\nNIF:99999990T", ControladorComun.DameImagen("ISOLOGOJMCP64.jpg")) };
                BD!.PersistirObjeto(Tiendas[0]);
            }
            else
            {
                Tiendas = BD!.LeerObjetosTipo<Tienda>();
            }
        }

        public static void CerrarPrograma()
        {
            if (TpvBase != null)
            {
                TpvBase!.FinTPV();
                BD!.DesconectarBD();
                Application.Current.Shutdown();
            }
        }

        public static BitmapImage DameImagen(string nombreImagenProyecto)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri($"pack://application:,,,/Recursos/Imagenes/{nombreImagenProyecto}");
            bitmap.EndInit();
            return bitmap;
        }
        public static BitmapImage DameImagenProducto(string nombreImagenProyecto)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri($"pack://application:,,,/Recursos/Imagenes/Productos/{nombreImagenProyecto}");
            bitmap.EndInit();
            return bitmap;
        }

        //public static LineaPantalla DimeUltimaLinea(List<LineaPantalla> listaLineas)
        //{
        //    List<LineaPantalla> lineas = BD!.LeerObjetosLista<LineaPantalla>(listaLineas, "NumLinea");
        //    int numLinea = lineas.Last().NumeroLinea;

        //    return numLinea;
        //}

    }
}
