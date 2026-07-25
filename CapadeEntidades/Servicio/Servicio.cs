namespace CapaEntidades.Servicio
{
    public class Servicio
    {
        private int     idServicio;
        private string  nombre;
        private string  descripcion;
        private decimal precio;
        private bool    disponible;

        public int     IdServicio  { get => idServicio;  set => idServicio  = value; }
        public string  Nombre      { get => nombre;      set => nombre      = value; }
        public string  Descripcion { get => descripcion; set => descripcion = value; }
        public decimal Precio      { get => precio;      set => precio      = value; }
        public bool    Disponible  { get => disponible;  set => disponible  = value; }

        public Servicio(int idServicio, string nombre, string descripcion,
                        decimal precio, bool disponible)
        {
            this.IdServicio  = idServicio;
            this.Nombre      = nombre;
            this.Descripcion = descripcion;
            this.Precio      = precio;
            this.Disponible  = disponible;
        }

        public Servicio() { }
    }
}
