using System;

namespace HotelAPMGrand.Entidades.Vistas
{
    public class VwReservaHistorial
    {
        private int      idReserva;
        private string   cliente;
        private string   habitacion;
        private string   tipoHabitacion;
        private DateTime entrada;
        private DateTime salida;
        private int      noches;
        private string   estado;
        private string   fechaCreacion;

        public int      IdReserva      { get => idReserva;      set => idReserva      = value; }
        public string   Cliente        { get => cliente;        set => cliente        = value; }
        public string   Habitacion     { get => habitacion;     set => habitacion     = value; }
        public string   TipoHabitacion { get => tipoHabitacion; set => tipoHabitacion = value; }
        public DateTime Entrada        { get => entrada;        set => entrada        = value; }
        public DateTime Salida         { get => salida;         set => salida         = value; }
        public int      Noches         { get => noches;         set => noches         = value; }
        public string   Estado         { get => estado;         set => estado         = value; }
        public string   FechaCreacion  { get => fechaCreacion;  set => fechaCreacion  = value; }

        public VwReservaHistorial(int idReserva, string cliente, string habitacion,
                                  string tipoHabitacion, DateTime entrada, DateTime salida,
                                  int noches, string estado, string fechaCreacion)
        {
            this.IdReserva      = idReserva;
            this.Cliente        = cliente;
            this.Habitacion     = habitacion;
            this.TipoHabitacion = tipoHabitacion;
            this.Entrada        = entrada;
            this.Salida         = salida;
            this.Noches         = noches;
            this.Estado         = estado;
            this.FechaCreacion  = fechaCreacion;
        }

        public VwReservaHistorial() { }
    }
}
