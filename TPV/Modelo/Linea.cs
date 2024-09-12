using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPV.Controlador;

namespace TPV.Modelo
{
    /// <summary>
    /// Definición de línea, será usada en la posición de venta y en el ticket
    /// </summary>
    public class Linea
    {
        private int _id;
        public int Id { get => _id; set => _id = value; }
        private int _numLinea;
        public int NumLinea { get => _numLinea; set => _numLinea = value; }
        private Producto _producto;
        public Producto Producto { get => _producto; set => _producto = value; }
        private double _precio;
        public double Precio { get => _precio; set => _precio = value; } //Aunque lo tendrá Producto y cuando se añada desde el constructor será calculado, posiblemente se permitirá en el futuro cambiar el precio desde el grid de ventas manualmente

        public Linea(Producto producto, double precio)
        {
            _id = ControladorComun.BD!.SelectMAXInt("Linea", "_id") + 1;
            _numLinea = UltimoNumLinea() + 1;
            _producto = producto;
            _precio = precio;
        }

        public int UltimoNumLinea() //para NumLinea en el constructor
        {
            int maxNumLinea = 0;
            foreach (Linea linea in ControladorComun.TpvBase!.PosicionVentaActual!.LineasPantalla)
            {
                if (linea.NumLinea > maxNumLinea)
                {
                    maxNumLinea = linea.NumLinea;
                }
            }
            return maxNumLinea;
        }

    }
}
