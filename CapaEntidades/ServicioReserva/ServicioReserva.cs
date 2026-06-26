using System;

namespace CapaEntidades.ServicioReserva
{
    public class ServicioReserva
    {
        private int      idServReserva;
        private int      idReserva;
        private int      idServicio;
        private int      cantidad;
        private decimal  subtotal;
        private DateTime fechaSolicitud;

        public int      IdServReserva  { get => idServReserva;  set => idServReserva  = value; }
        public int      IdReserva      { get => idReserva;      set => idReserva      = value; }
        public int      IdServicio     { get => idServicio;     set => idServicio     = value; }
        public int      Cantidad       { get => cantidad;       set => cantidad       = value; }
        public decimal  Subtotal       { get => subtotal;       set => subtotal       = value; }
        public DateTime FechaSolicitud { get => fechaSolicitud; set => fechaSolicitud = value; }

        public ServicioReserva(int idServReserva, int idReserva, int idServicio,
                               int cantidad, decimal subtotal, DateTime fechaSolicitud)
        {
            this.IdServReserva  = idServReserva;
            this.IdReserva      = idReserva;
            this.IdServicio     = idServicio;
            this.Cantidad       = cantidad;
            this.Subtotal       = subtotal;
            this.FechaSolicitud = fechaSolicitud;
        }

        public ServicioReserva() { }
    }
}
