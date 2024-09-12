using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPV.Controlador;

namespace TPV.Modelo
{
    public class FormaPago
    {
        private int _id;
        public int Id { get { return _id; } }
        private string _nombre;
        public string Nombre { get { return _nombre; } set { _nombre = value; } }
        private string _descripcion;
        public string Descripcion { get { return _descripcion; } set { _descripcion = value; } }

        public FormaPago(string nombre, string descripcion)
        {
            _id = ControladorComun.BD!.SelectMAXInt("FormaPago", "_id") + 1;
            _nombre = nombre;
            _descripcion = descripcion;
        }

        public override string ToString()
        {
            return $"Id: {Id}, Nombre: {Nombre}, Descripcion: {Descripcion}";
        }
    }
}
