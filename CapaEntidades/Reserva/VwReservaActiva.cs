using System;

namespace HotelAPMGrand.Entidades.Vistas
{
    public class VwReservaActiva
    {
        private int      idReserva;
        private string   cliente;
        private string   correoCliente;
        private string   habitacion;
        private string   tipoHabitacion;
        private decimal  precioPorNoche;
        private DateTime entrada;
        private DateTime salida;
        private int      noches;
        private string   estado;

        public int      IdReserva      { get => idReserva;      set => idReserva      = value; }
        public string   Cliente        { get => cliente;        set => cliente        = value; }
        public string   CorreoCliente  { get => correoCliente;  set => correoCliente  = value; }
        public string   Habitacion     { get => habitacion;     set => habitacion     = value; }
        public string   TipoHabitacion { get => tipoHabitacion; set => tipoHabitacion = value; }
        public decimal  PrecioPorNoche { get => precioPorNoche; set => precioPorNoche = value; }
        public DateTime Entrada        { get => entrada;        set => entrada        = value; }
        public DateTime Salida         { get => salida;         set => salida         = value; }
        public int      Noches         { get => noches;         set => noches         = value; }
        public string   Estado         { get => estado;         set => estado         = value; }

        public VwReservaActiva(int idReserva, string cliente, string correoCliente,
                               string habitacion, string tipoHabitacion, decimal precioPorNoche,
                               DateTime entrada, DateTime salida, int noches, string estado)
        {
            this.IdReserva      = idReserva;
            this.Cliente        = cliente;
            this.CorreoCliente  = correoCliente;
            this.Habitacion     = habitacion;
            this.TipoHabitacion = tipoHabitacion;
            this.PrecioPorNoche = precioPorNoche;
            this.Entrada        = entrada;
            this.Salida         = salida;
            this.Noches         = noches;
            this.Estado         = estado;
        }

        public VwReservaActiva() { }
    }
}
