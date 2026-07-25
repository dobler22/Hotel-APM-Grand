using System;

namespace CapaEntidades.Factura
{
    public class VwFactura
    {
        private int      idFactura;
        private string   cliente;
        private string   correoCliente;
        private string   habitacion;
        private DateTime entrada;
        private DateTime salida;
        private decimal  alojamiento;
        private decimal  servicios;
        private decimal  total;
        private string   estado;
        private string   fechaEmision;

        public int      IdFactura     { get => idFactura;     set => idFactura     = value; }
        public string   Cliente       { get => cliente;       set => cliente       = value; }
        public string   CorreoCliente { get => correoCliente; set => correoCliente = value; }
        public string   Habitacion    { get => habitacion;    set => habitacion    = value; }
        public DateTime Entrada       { get => entrada;       set => entrada       = value; }
        public DateTime Salida        { get => salida;        set => salida        = value; }
        public decimal  Alojamiento   { get => alojamiento;   set => alojamiento   = value; }
        public decimal  Servicios     { get => servicios;     set => servicios     = value; }
        public decimal  Total         { get => total;         set => total         = value; }
        public string   Estado        { get => estado;        set => estado        = value; }
        public string   FechaEmision  { get => fechaEmision;  set => fechaEmision  = value; }

        public VwFactura(int idFactura, string cliente, string correoCliente,
                         string habitacion, DateTime entrada, DateTime salida,
                         decimal alojamiento, decimal servicios, decimal total,
                         string estado, string fechaEmision)
        {
            this.IdFactura     = idFactura;
            this.Cliente       = cliente;
            this.CorreoCliente = correoCliente;
            this.Habitacion    = habitacion;
            this.Entrada       = entrada;
            this.Salida        = salida;
            this.Alojamiento   = alojamiento;
            this.Servicios     = servicios;
            this.Total         = total;
            this.Estado        = estado;
            this.FechaEmision  = fechaEmision;
        }

        public VwFactura() { }
    }
}
