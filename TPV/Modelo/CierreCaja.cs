using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPV.Controlador;

namespace TPV_.Modelo
{
    public class CierreCaja
    {
        private int _id;
        public int Id { get => _id; set => _id = value; }
        private DateTime _fecha;
        public DateTime Fecha { get => _fecha; set => _fecha = value; }
        private int _codTPV;
        public int CodTPV { get => _codTPV; set => _codTPV = value; }
        private int _codUsuario;
        public int CodUsuario { get => _codUsuario; set => _codUsuario = value; }
        private double _saldoInicial;
        public double SaldoInicial { get => _saldoInicial; set => _saldoInicial = value; }
        private double _saldoFinal;
        public double SaldoFinal { get => _saldoFinal; set => _saldoFinal = value; }
        private double _ingresos;
        public double Ingresos { get => _ingresos; set => _ingresos = value; }
        private double _gastos;
        public double Gastos { get => _gastos; set => _gastos = value; }
        private double _efectivoInicial;
        public double EfectivoInicial { get => _efectivoInicial; set => _efectivoInicial = value; }
        private double _efectivoFinal;
        public double EfectivoFinal { get => _efectivoFinal; set => _efectivoFinal = value; }
        private double _tarjetaInicial;
        public double TarjetaInicial { get => _tarjetaInicial; set => _tarjetaInicial = value; }
        private double _tarjetaFinal;
        public double TarjetaFinal { get => _tarjetaFinal; set => _tarjetaFinal = value; }

        public CierreCaja(int codTPV, int codUsuario, double saldoInicial, double efectivoInicial, double tarjetaInicial)
        {
            this._id = ControladorComun.BD!.SelectMAXInt("AperturaCaja", "_id") + 1;
            this._fecha = DateTime.Now;
            this._codTPV = codTPV;
            this._codUsuario = codUsuario;
            this._saldoInicial = saldoInicial;
            this._saldoFinal = saldoInicial;
            this._ingresos = 0;
            this._gastos = 0;
            this._efectivoInicial = efectivoInicial;
            this._efectivoFinal = efectivoInicial;
            this._tarjetaInicial = tarjetaInicial;
            this._tarjetaFinal = tarjetaInicial;
        }
    }
}
