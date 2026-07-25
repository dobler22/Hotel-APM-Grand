namespace CapaEntidades.Habitacion
{
    public class VwHabitacionDisponible
    {
        private string  numero;
        private string  tipo;
        private int     capacidad;
        private int     piso;
        private decimal precioPorNoche;
        private string  estado;

        public string  Numero         { get => numero;         set => numero         = value; }
        public string  Tipo           { get => tipo;           set => tipo           = value; }
        public int     Capacidad      { get => capacidad;      set => capacidad      = value; }
        public int     Piso           { get => piso;           set => piso           = value; }
        public decimal PrecioPorNoche { get => precioPorNoche; set => precioPorNoche = value; }
        public string  Estado         { get => estado;         set => estado         = value; }

        public VwHabitacionDisponible(string numero, string tipo, int capacidad,
                                      int piso, decimal precioPorNoche, string estado)
        {
            this.Numero         = numero;
            this.Tipo           = tipo;
            this.Capacidad      = capacidad;
            this.Piso           = piso;
            this.PrecioPorNoche = precioPorNoche;
            this.Estado         = estado;
        }

        public VwHabitacionDisponible() { }
    }
}
