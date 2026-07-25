using System;

namespace HotelAPMGrand.Entidades
{
    public class Pago
    {
        private int      idPago;
        private int      idReserva;
        private decimal  monto;
        private string   metodoPago;
        private string   estado;
        private DateTime fechaPago;
        private string   referencia;

        public int      IdPago     { get => idPago;     set => idPago     = value; }
        public int      IdReserva  { get => idReserva;  set => idReserva  = value; }
        public decimal  Monto      { get => monto;      set => monto      = value; }
        public string   MetodoPago { get => metodoPago; set => metodoPago = value; }
        public string   Estado     { get => estado;     set => estado     = value; }
        public DateTime FechaPago  { get => fechaPago;  set => fechaPago  = value; }
        public string   Referencia { get => referencia; set => referencia = value; }

        public Pago(int idPago, int idReserva, decimal monto, string metodoPago,
                    string estado, DateTime fechaPago, string referencia)
        {
            this.IdPago     = idPago;
            this.IdReserva  = idReserva;
            this.Monto      = monto;
            this.MetodoPago = metodoPago;
            this.Estado     = estado;
            this.FechaPago  = fechaPago;
            this.Referencia = referencia;
        }

        public Pago() { }
    }
}
