using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPV.Controlador;

namespace TPV.Modelo
{
    public class Tarifa
    {
        private int _id;
        public int Id { get => _id; set => _id = value; }
        private int _codTarifa;
        public int CodTarifa { get => _codTarifa; set => _codTarifa = value; }
        private double _iva;
        public double Iva { get => _iva; set => _iva = value; }
        private int _codTienda;
        public int CodTienda { get => _codTienda; set => _codTienda = value; }
        private string _nombre;
        public string Nombre { get => _nombre; set => _nombre = value; }
        private string _descripcion;
        public string Descripcion { get => _descripcion; set => _descripcion = value; }

        private List<ObjProdPrecio> _listaProductos; //CodProducto, Precio, CodTarifa
        public List<ObjProdPrecio> ListaProductos { get => _listaProductos; set => _listaProductos = value; }

        public Tarifa(int codTarifa, string nombre, int codTienda,  double iva, string descripcion)
        {
            this._id = ControladorComun.BD!.SelectMAXInt("Tarifa", "_id") + 1;
            this._codTarifa = codTarifa;
            this._iva = iva;
            this._codTienda = codTienda;
            this._nombre = nombre;
            this._descripcion = descripcion;
            this._listaProductos = new List<ObjProdPrecio>();
        }
        public Tarifa(int id, int codTarifa, string nombre, int codTienda, double iva, string descripcion)
        {
            this._id = id;
            this._codTarifa = codTarifa;
            this._iva = iva;
            this._codTienda = codTienda;
            this._nombre = nombre;
            this._descripcion = descripcion;
            this._listaProductos = new List<ObjProdPrecio>();
        }

        public void AnadirProducto(Producto producto, double precio, bool persistencia)
        {
            ObjProdPrecio obj = new ObjProdPrecio(producto.CodProducto, this.CodTarifa, precio);
            _listaProductos!.Add(obj);
            if (persistencia)
            {
                if (ControladorComun.BD!.BuscarObjetosIntAndInt<ObjProdPrecio>("CodProducto", producto.CodProducto, "CodTarifa", this._codTarifa).Count < 1)
                {
                    ControladorComun.BD!.PersistirObjeto(obj);
                }
                ControladorComun.BD!.PersistirObjeto(this);
            }
        }
        public void AnadirProducto(int codProducto, double precio, bool persistencia)
        {
            ObjProdPrecio obj = new ObjProdPrecio(codProducto, this.CodTarifa, precio);
            _listaProductos!.Add(obj);
            if (persistencia)
            {
                if (ControladorComun.BD!.BuscarObjetosIntAndInt<ObjProdPrecio>("CodProducto", codProducto, "CodTarifa", this._codTarifa).Count < 1)
                {
                    ControladorComun.BD!.PersistirObjeto(obj);
                }
                ControladorComun.BD!.PersistirObjeto(this);
            }
        }

        public bool EliminarProducto(int codProducto, bool persistencia)
        {

            if (this._listaProductos!.Remove(_listaProductos.Find(x => x.CodProducto == codProducto)!))
            {
                if (persistencia)
                {
                    ControladorComun.BD!.EliminarObjeto<ObjProdPrecio>(_listaProductos.Find(x => x.CodProducto == codProducto)!, codProducto.ToString());
                    ControladorComun.BD!.ActualizarObjeto(this);
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool EliminarProducto(Producto producto, bool persistencia)
        {
            if (this._listaProductos!.Remove(_listaProductos.Find(x => x.CodProducto == producto.CodProducto)!))
            {
                if (persistencia)
                {
                    ControladorComun.BD!.EliminarObjeto<ObjProdPrecio>(_listaProductos.Find(x => x.CodProducto == producto.CodProducto)!, producto.CodProducto.ToString());
                    ControladorComun.BD!.ActualizarObjeto(this);
                }
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
