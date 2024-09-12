using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace TPV.Modelo
{
    public class Usuario
    {
        private int _id;
        public int Id { get { return _id; } }
        private int _codTienda;
        public int CodTienda{ get => _codTienda; set => _codTienda = value; }
        private string _clave;
        public string Clave { get => _clave; set => _clave = value;  }
        private bool _esEncargado;
        public bool EsEncargado { get => _esEncargado; set => _esEncargado = value; }
        private string _nombre;
        public string Nombre { get => _nombre; set => _nombre = value; }
        private BitmapImage? _avatar;
        public BitmapImage? Avatar { get => _avatar; set => _avatar = value; }
        private bool _esActivo;
        public bool EsActivo { get => _esActivo; set => _esActivo = value; }
        
        public Usuario(int id, string clave, int codTienda, bool esEncargado, string nombre, BitmapImage avatar)
        {
            _id = id;
            _clave = clave;
            _codTienda = codTienda;
            _esEncargado = esEncargado;
            _nombre = nombre;
            _avatar = avatar;
            _esActivo = true;
        }

        public Usuario(int id, string clave, bool esEncargado, string nombre)
        {
            _id = id;
            _clave = clave;
            _esEncargado = esEncargado;
            _nombre = nombre;
            _esActivo = true;
        }
    }
}
