using System;

namespace CapaEntidades.Resena
{
    public class VwResena
    {
        private string   cliente;
        private string   habitacion;
        private string   tipoHabitacion;
        private int      calificacion;
        private string   estrellas;
        private string   comentario;
        private DateTime fecha;

        public string   Cliente        { get => cliente;        set => cliente        = value; }
        public string   Habitacion     { get => habitacion;     set => habitacion     = value; }
        public string   TipoHabitacion { get => tipoHabitacion; set => tipoHabitacion = value; }
        public int      Calificacion   { get => calificacion;   set => calificacion   = value; }
        public string   Estrellas      { get => estrellas;      set => estrellas      = value; }
        public string   Comentario     { get => comentario;     set => comentario     = value; }
        public DateTime Fecha          { get => fecha;          set => fecha          = value; }

        public VwResena(string cliente, string habitacion, string tipoHabitacion,
                        int calificacion, string estrellas, string comentario, DateTime fecha)
        {
            this.Cliente        = cliente;
            this.Habitacion     = habitacion;
            this.TipoHabitacion = tipoHabitacion;
            this.Calificacion   = calificacion;
            this.Estrellas      = estrellas;
            this.Comentario     = comentario;
            this.Fecha          = fecha;
        }

        public VwResena() { }
    }
}
