using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPV.Controlador;

namespace TPV.Modelo
{
    public class Seccion
    {
        private int _id;
        public int Id { get => _id; set => _id = value; }
        private int _codSeccion;
        public int CodSeccion { get => _codSeccion; set => _codSeccion = value; }
        private string _nombre;
        public string Nombre { get => _nombre; set => _nombre = value; }
        private int _codTienda;
        public int CodTienda { get => _codTienda; set => _codTienda = value; }
        private List<Producto>? _productos;
        public List<Producto>? Productos { get => _productos; set => _productos = value; }

        public Seccion(int codSeccion, string nombre, int codTienda)
        {
            _id = ControladorComun.BD!.SelectMAXInt("Seccion", "_id") + 1;
            _codSeccion = codSeccion;
            _nombre = nombre;
            _codTienda = codTienda;
            _productos = new List<Producto>();
        }
        public Seccion(int id, int codSeccion, string nombre, int codTienda)
        {
            _id = id;
            _codSeccion = codSeccion;
            _nombre = nombre;
            _codTienda = codTienda;
            _productos = new List<Producto>();
        }

        public Seccion(int codSeccion, string nombre, int codTienda, List<Producto> listaProductos)
        {
            _id = ControladorComun.BD!.SelectMAXInt("Seccion", "_id") + 1;
            _codSeccion = codSeccion;
            _nombre = nombre;
            _codTienda = codTienda;
            _productos = listaProductos;
        }

        public void AddProducto(Producto producto)
        {
            _productos!.Add(producto);
        }
    }
}
