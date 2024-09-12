using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPV.Modelo
{
    public class Cliente
    {
        private int _id;
        public int Id { get => _id; set => _id = value; }
        private string _nombre;
        public string Nombre { get => _nombre; set => _nombre = value; } 
        private string _dni;
        public string Dni { get => _dni; set => _dni = value; }

        public Cliente(int id, string nombre, string dni)
        {
            _id = id;
            _nombre = nombre;
            _dni = dni;
        }

    }
}
