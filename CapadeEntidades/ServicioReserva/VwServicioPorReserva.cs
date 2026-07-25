using System;

namespace CapaEntidades.ServicioReserva
{
    public class VwServicioPorReserva
    {
        private int      idReserva;
        private string   cliente;
        private string   habitacion;
        private string   servicio;
        private int      cantidad;
        private decimal  precioUnitario;
        private decimal  subtotal;
        private string   fechaSolicitud;

        public int      IdReserva      { get => idReserva;      set => idReserva      = value; }
        public string   Cliente        { get => cliente;        set => cliente        = value; }
        public string   Habitacion     { get => habitacion;     set => habitacion     = value; }
        public string   Servicio       { get => servicio;       set => servicio       = value; }
        public int      Cantidad       { get => cantidad;       set => cantidad       = value; }
        public decimal  PrecioUnitario { get => precioUnitario; set => precioUnitario = value; }
        public decimal  Subtotal       { get => subtotal;       set => subtotal       = value; }
        public string   FechaSolicitud { get => fechaSolicitud; set => fechaSolicitud = value; }

        public VwServicioPorReserva(int idReserva, string cliente, string habitacion,
                                    string servicio, int cantidad, decimal precioUnitario,
                                    decimal subtotal, string fechaSolicitud)
        {
            this.IdReserva      = idReserva;
            this.Cliente        = cliente;
            this.Habitacion     = habitacion;
            this.Servicio       = servicio;
            this.Cantidad       = cantidad;
            this.PrecioUnitario = precioUnitario;
            this.Subtotal       = subtotal;
            this.FechaSolicitud = fechaSolicitud;
        }

        public VwServicioPorReserva() { }
    }
}
