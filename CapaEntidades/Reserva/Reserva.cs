using System;

namespace CapaEntidades.Reserva
{
    public class Reserva
    {
        private int      idReserva;
        private int      idCliente;
        private int      idHabitacion;
        private DateTime fechaEntrada;
        private DateTime fechaSalida;
        private string   estado;
        private DateTime fechaCreacion;

        public int      IdReserva     { get => idReserva;     set => idReserva     = value; }
        public int      IdCliente     { get => idCliente;     set => idCliente     = value; }
        public int      IdHabitacion  { get => idHabitacion;  set => idHabitacion  = value; }
        public DateTime FechaEntrada  { get => fechaEntrada;  set => fechaEntrada  = value; }
        public DateTime FechaSalida   { get => fechaSalida;   set => fechaSalida   = value; }
        public string   Estado        { get => estado;        set => estado        = value; }
        public DateTime FechaCreacion { get => fechaCreacion; set => fechaCreacion = value; }

        public Reserva(int idReserva, int idCliente, int idHabitacion,
                       DateTime fechaEntrada, DateTime fechaSalida,
                       string estado, DateTime fechaCreacion)
        {
            this.IdReserva     = idReserva;
            this.IdCliente     = idCliente;
            this.IdHabitacion  = idHabitacion;
            this.FechaEntrada  = fechaEntrada;
            this.FechaSalida   = fechaSalida;
            this.Estado        = estado;
            this.FechaCreacion = fechaCreacion;
        }

        public Reserva() { }
    }
}
