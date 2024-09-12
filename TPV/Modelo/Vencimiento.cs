using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPV.Controlador;

namespace TPV.Modelo
{
    public class Vencimiento
    {
        private int _id;
        public int Id { get { return _id; } set { _id = value; } }
        private string _ticketNum;
        public string TicketNum { get { return _ticketNum; } set { _ticketNum = value; } }
        private FormaPago _formaPago;
        public FormaPago FormaPago { get { return _formaPago; } set { _formaPago = value; } }
        private double _cantidad;
        public double Cantidad { get { return _cantidad; } set { _cantidad = value; } }

        public Vencimiento(string ticketNum, FormaPago formaPago, double cantidad)
        {
            this._id = ControladorComun.BD!.SelectMAXInt("Vencimiento", "_id") + 1;
            this._ticketNum = ticketNum;
            this._formaPago = formaPago;
            this._cantidad = cantidad;
        }
    }
}
