using System;

namespace CapaEntidades.Empleado
{
    public class VwEmpleado
    {
        private string   nombreCompleto;
        private string   cargo;
        private string   area;
        private string   telefono;
        private DateTime fechaIngreso;
        private string   correo;
        private string   estado;

        public string   NombreCompleto { get => nombreCompleto; set => nombreCompleto = value; }
        public string   Cargo          { get => cargo;          set => cargo          = value; }
        public string   Area           { get => area;           set => area           = value; }
        public string   Telefono       { get => telefono;       set => telefono       = value; }
        public DateTime FechaIngreso   { get => fechaIngreso;   set => fechaIngreso   = value; }
        public string   Correo         { get => correo;         set => correo         = value; }
        public string   Estado         { get => estado;         set => estado         = value; }

        public VwEmpleado(string nombreCompleto, string cargo, string area,
                          string telefono, DateTime fechaIngreso, string correo, string estado)
        {
            this.NombreCompleto = nombreCompleto;
            this.Cargo          = cargo;
            this.Area           = area;
            this.Telefono       = telefono;
            this.FechaIngreso   = fechaIngreso;
            this.Correo         = correo;
            this.Estado         = estado;
        }

        public VwEmpleado() { }
    }
}
