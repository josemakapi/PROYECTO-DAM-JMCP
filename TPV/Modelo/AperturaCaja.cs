using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPV.Controlador;

namespace TPV.Modelo
{
    public class AperturaCaja
    {
        private DateOnly _fecha;
        public DateOnly Fecha { get => _fecha; set => _fecha = value; }
        private double _fondoCaja;
        public double FondoCaja { get => _fondoCaja; set => _fondoCaja = value; }
        private int _idUsuario;
        public int IdUsuario { get => _idUsuario; set => _idUsuario = value; }
        private bool _cerrada;
        public bool Cerrada { get => _cerrada; set => _cerrada = value; }

        public AperturaCaja(DateOnly fecha, double fondoCaja, int idUsuario, bool cerrada)
        {
            _fecha = fecha;
            _fondoCaja = fondoCaja;
            _idUsuario = idUsuario;
            _cerrada = cerrada;
        }
    }
}
