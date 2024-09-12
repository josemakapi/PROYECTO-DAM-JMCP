using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPV.Controlador;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TPV.Modelo
{
    public class Ticket
    {
        private int _id;
        private DateTime _fechaHora;
        public DateTime FechaHora { get { return _fechaHora; } set { _fechaHora = value; } }
        private string _numTicket;
        public string NumTicket { get { return _numTicket; } set { _numTicket = value; } }
        private char _tipo;
        public char Tipo { get { return _tipo; } set { _tipo = value; } } //T = Ticket normal, D = Devolucion
        private int _numTPV;
        public int NumTPV { get { return _numTPV; } set { _numTPV = value; } }
        private Usuario _vendedor;
        public Usuario Vendedor { get { return _vendedor; } set { _vendedor = value; } }
        private int _ejercicio;
        public int Ejercicio { get { return _ejercicio; } set { _ejercicio = value; } }
        private string _tienda;
        public string Tienda { get { return _tienda; } set { _tienda = value; } }
        private List<Linea> _lineasPantalla;
        public List<Linea> LineasPantalla { get { return _lineasPantalla; } set { _lineasPantalla = value; } }
        private bool _cerrado;
        public bool Cerrado { get { return _cerrado; } set { _cerrado = value; } }
        private DateTime _fechaHoraCierre;
        public DateTime FechaHoraCierre { get { return _fechaHoraCierre; } set { _fechaHoraCierre = value; } }
        private List<Vencimiento> _vencimientos; //Cantidad, FormaPago
        public List<Vencimiento> Vencimientos { get { return _vencimientos; } set { _vencimientos = value; } }


        public Ticket(char tipo, int numTPV, int ejercicio, string tienda, Usuario vendedor)
        {
            this._id = ControladorComun.BD!.SelectMAXInt("Ticket", "_id") + 1;
            this._fechaHora = DateTime.Now;
            this._tipo = tipo;
            this._numTPV = numTPV;
            this._ejercicio = ejercicio;
            this._tienda = tienda;
            this._lineasPantalla = ControladorComun.TpvBase!.PosicionVentaActual!.LineasPantalla;
            this._numTicket = char.ToUpper(this._tipo).ToString() + this._tienda + this._id.ToString().PadLeft(8, '0');
            this._vencimientos = new List<Vencimiento>();
            this._vendedor = vendedor;
        }

        public void AgregarVencimiento(Vencimiento vencimiento)
        {
            this._vencimientos.Add(vencimiento);
        }



    }
}
