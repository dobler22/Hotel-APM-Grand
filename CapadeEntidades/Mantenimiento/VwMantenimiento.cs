using System;

namespace CapaEntidades.Mantenimiento
{
    public class VwMantenimiento
    {
        private string   habitacion;
        private string   tipoHabitacion;
        private string   responsable;
        private string   cargoResponsable;
        private string   tipoTrabajo;
        private DateTime fechaInicio;
        private string   fechaFin;
        private string   estado;
        private string   observaciones;

        public string   Habitacion       { get => habitacion;       set => habitacion       = value; }
        public string   TipoHabitacion   { get => tipoHabitacion;   set => tipoHabitacion   = value; }
        public string   Responsable      { get => responsable;      set => responsable      = value; }
        public string   CargoResponsable { get => cargoResponsable; set => cargoResponsable = value; }
        public string   TipoTrabajo      { get => tipoTrabajo;      set => tipoTrabajo      = value; }
        public DateTime FechaInicio      { get => fechaInicio;      set => fechaInicio      = value; }
        public string   FechaFin         { get => fechaFin;         set => fechaFin         = value; }
        public string   Estado           { get => estado;           set => estado           = value; }
        public string   Observaciones    { get => observaciones;    set => observaciones    = value; }

        public VwMantenimiento(string habitacion, string tipoHabitacion, string responsable,
                               string cargoResponsable, string tipoTrabajo, DateTime fechaInicio,
                               string fechaFin, string estado, string observaciones)
        {
            this.Habitacion       = habitacion;
            this.TipoHabitacion   = tipoHabitacion;
            this.Responsable      = responsable;
            this.CargoResponsable = cargoResponsable;
            this.TipoTrabajo      = tipoTrabajo;
            this.FechaInicio      = fechaInicio;
            this.FechaFin         = fechaFin;
            this.Estado           = estado;
            this.Observaciones    = observaciones;
        }

        public VwMantenimiento() { }
    }
}
