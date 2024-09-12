using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPV.Controlador;
using System.Windows.Media.Imaging;

namespace TPV.Modelo
{
    public class Producto
    {
        private int _id;
        public int Id { get => _id; set => _id = value; }
        private string _nombre;
        public string Nombre { get => _nombre; set => _nombre = value; }
        private int _codProducto;
        public int CodProducto { get => _codProducto; set => _codProducto = value; }
        private string? _descripcion;
        private int _codTienda;
        public int CodTienda { get => _codTienda; set => _codTienda = value; }
        public string? Descripcion { get => _descripcion; set => _descripcion = value; }
        private BitmapImage? _imagen;
        public BitmapImage? Imagen { get => _imagen; set => _imagen = value; }
        //private string? _seccion;
        //public string? Seccion { get { return _seccion; } set { _seccion = value; } }
        //private string? _subseccion;
        //public string? Subseccion { get { return _subseccion; } set { _subseccion = value; } }


        public Producto(int codProd, string nombre, int codTienda)
        {
            _id = ControladorComun.BD!.SelectMAXInt("Producto", "_id") + 1;
            _codProducto = codProd;
            _nombre = nombre;
            _codTienda = codTienda;
        }
        public Producto(int id, int codProd, string nombre, int codTienda)
        {
            _id = id;
            _codProducto = codProd;
            _nombre = nombre;
            _codTienda = codTienda;
        }

        public Producto(int codProd, string nombre, string descripcion, int codTienda)
        {
            _id = ControladorComun.BD!.SelectMAXInt("Producto", "_id") + 1;
            _codProducto = codProd;
            _nombre = nombre;
            _descripcion = descripcion;
            _codTienda = codTienda;
        }

        public Producto(int codProd, string nombre,  string descripcion, int codTienda, BitmapImage imagen)
        {
            _id = ControladorComun.BD!.SelectMAXInt("Producto", "_id") + 1;
            _codProducto = codProd;
            _nombre = nombre;
            _descripcion = descripcion;
            _codTienda = codTienda;
            _imagen = imagen;
        }
        public Producto(int id, int codProd, string nombre, string descripcion, int codTienda, BitmapImage imagen)
        {
            _id = id;
            _codProducto = codProd;
            _nombre = nombre;
            _descripcion = descripcion;
            _codTienda = codTienda;
            _imagen = imagen;
        }
    }
}
