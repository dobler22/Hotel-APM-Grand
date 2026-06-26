using System;

namespace HotelAPMGrand.Entidades
{
    public class Resena
    {
        private int      idResena;
        private int      idReserva;
        private int      idCliente;
        private int      calificacion;
        private string   comentario;
        private DateTime fechaResena;

        public int      IdResena     { get => idResena;     set => idResena     = value; }
        public int      IdReserva    { get => idReserva;    set => idReserva    = value; }
        public int      IdCliente    { get => idCliente;    set => idCliente    = value; }
        public int      Calificacion { get => calificacion; set => calificacion = value; }
        public string   Comentario   { get => comentario;   set => comentario   = value; }
        public DateTime FechaResena  { get => fechaResena;  set => fechaResena  = value; }

        public Resena(int idResena, int idReserva, int idCliente,
                      int calificacion, string comentario, DateTime fechaResena)
        {
            this.IdResena     = idResena;
            this.IdReserva    = idReserva;
            this.IdCliente    = idCliente;
            this.Calificacion = calificacion;
            this.Comentario   = comentario;
            this.FechaResena  = fechaResena;
        }

        public Resena() { }
    }
}
