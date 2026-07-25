using System;

namespace HotelAPMGrand.Entidades
{
    public class Mantenimiento
    {
        private int      idMantenimiento;
        private int      idHabitacion;
        private int      idEmpleado;
        private string   tipoTrabajo;
        private DateTime fechaInicio;
        private DateTime fechaFin;
        private string   estado;
        private string   observaciones;

        public int      IdMantenimiento { get => idMantenimiento; set => idMantenimiento = value; }
        public int      IdHabitacion    { get => idHabitacion;    set => idHabitacion    = value; }
        public int      IdEmpleado      { get => idEmpleado;      set => idEmpleado      = value; }
        public string   TipoTrabajo     { get => tipoTrabajo;     set => tipoTrabajo     = value; }
        public DateTime FechaInicio     { get => fechaInicio;     set => fechaInicio     = value; }
        public DateTime FechaFin        { get => fechaFin;        set => fechaFin        = value; }
        public string   Estado          { get => estado;          set => estado          = value; }
        public string   Observaciones   { get => observaciones;   set => observaciones   = value; }

        public Mantenimiento(int idMantenimiento, int idHabitacion, int idEmpleado,
                             string tipoTrabajo, DateTime fechaInicio, DateTime fechaFin,
                             string estado, string observaciones)
        {
            this.IdMantenimiento = idMantenimiento;
            this.IdHabitacion    = idHabitacion;
            this.IdEmpleado      = idEmpleado;
            this.TipoTrabajo     = tipoTrabajo;
            this.FechaInicio     = fechaInicio;
            this.FechaFin        = fechaFin;
            this.Estado          = estado;
            this.Observaciones   = observaciones;
        }

        public Mantenimiento() { }
    }
}
