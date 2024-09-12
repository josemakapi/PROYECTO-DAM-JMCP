using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPV.Controlador;

namespace TPV.Modelo
{
    /// <summary>
    /// Objeto usado por la tarifa para almacenar los productos y sus precios porque MongoDB tiene problemas con los diccionarios
    /// </summary>
    public class ObjProdPrecio
    {
        private int _id;
        public int Id { get => _id; set => _id = value; }
        private int codProducto;
        public int CodProducto { get => codProducto; set => codProducto = value; }
        private int codTarifa;
        public int CodTarifa { get => codTarifa; set => codTarifa = value; }
        private double precio;
        public double Precio { get => precio; set => precio = value; }

        public ObjProdPrecio(int codProducto, int codTarifa, double precio)
        {
            this._id = ControladorComun.BD!.SelectMAXInt("ObjProdPrecio", "_id") + 1;
            this.codProducto = codProducto;
            this.codTarifa = codTarifa;
            this.precio = precio;
        }
    }
}
