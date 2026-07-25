namespace CapaEntidades.Pago
{
    public class VwPago
    {
        private string  cliente;
        private string  habitacion;
        private decimal monto;
        private string  metodoPago;
        private string  estado;
        private string  referencia;
        private string  fechaPago;

        public string  Cliente    { get => cliente;    set => cliente    = value; }
        public string  Habitacion { get => habitacion; set => habitacion = value; }
        public decimal Monto      { get => monto;      set => monto      = value; }
        public string  MetodoPago { get => metodoPago; set => metodoPago = value; }
        public string  Estado     { get => estado;     set => estado     = value; }
        public string  Referencia { get => referencia; set => referencia = value; }
        public string  FechaPago  { get => fechaPago;  set => fechaPago  = value; }

        public VwPago(string cliente, string habitacion, decimal monto, string metodoPago,
                      string estado, string referencia, string fechaPago)
        {
            this.Cliente    = cliente;
            this.Habitacion = habitacion;
            this.Monto      = monto;
            this.MetodoPago = metodoPago;
            this.Estado     = estado;
            this.Referencia = referencia;
            this.FechaPago  = fechaPago;
        }

        public VwPago() { }
    }
}
