namespace CapaEntidades.Servicio
{
    public class VwServicio
    {
        private string  servicio;
        private string  descripcion;
        private decimal precio;
        private string  estado;

        public string  Servicio    { get => servicio;    set => servicio    = value; }
        public string  Descripcion { get => descripcion; set => descripcion = value; }
        public decimal Precio      { get => precio;      set => precio      = value; }
        public string  Estado      { get => estado;      set => estado      = value; }

        public VwServicio(string servicio, string descripcion, decimal precio, string estado)
        {
            this.Servicio    = servicio;
            this.Descripcion = descripcion;
            this.Precio      = precio;
            this.Estado      = estado;
        }

        public VwServicio() { }
    }
}
