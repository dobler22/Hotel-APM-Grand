using System;

namespace HotelAPMGrand.Entidades
{
    public class Factura
    {
        private int      idFactura;
        private int      idReserva;
        private decimal  montoAlojamiento;
        private decimal  montoServicios;
        private decimal  total;
        private DateTime fechaEmision;
        private string   estado;

        public int      IdFactura        { get => idFactura;        set => idFactura        = value; }
        public int      IdReserva        { get => idReserva;        set => idReserva        = value; }
        public decimal  MontoAlojamiento { get => montoAlojamiento; set => montoAlojamiento = value; }
        public decimal  MontoServicios   { get => montoServicios;   set => montoServicios   = value; }
        public decimal  Total            { get => total;            set => total            = value; }
        public DateTime FechaEmision     { get => fechaEmision;     set => fechaEmision     = value; }
        public string   Estado           { get => estado;           set => estado           = value; }

        public Factura(int idFactura, int idReserva, decimal montoAlojamiento,
                       decimal montoServicios, decimal total,
                       DateTime fechaEmision, string estado)
        {
            this.IdFactura        = idFactura;
            this.IdReserva        = idReserva;
            this.MontoAlojamiento = montoAlojamiento;
            this.MontoServicios   = montoServicios;
            this.Total            = total;
            this.FechaEmision     = fechaEmision;
            this.Estado           = estado;
        }

        public Factura() { }
    }
}
