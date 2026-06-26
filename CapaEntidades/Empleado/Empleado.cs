using System;

namespace CapaEntidades.Empleado
{
    public class Empleado
    {
        private int      idEmpleado;
        private int      idUsuario;
        private string   nombre;
        private string   apellido;
        private string   cargo;
        private string   area;
        private string   telefono;
        private DateTime fechaIngreso;

        public int      IdEmpleado   { get => idEmpleado;   set => idEmpleado   = value; }
        public int      IdUsuario    { get => idUsuario;    set => idUsuario    = value; }
        public string   Nombre       { get => nombre;       set => nombre       = value; }
        public string   Apellido     { get => apellido;     set => apellido     = value; }
        public string   Cargo        { get => cargo;        set => cargo        = value; }
        public string   Area         { get => area;         set => area         = value; }
        public string   Telefono     { get => telefono;     set => telefono     = value; }
        public DateTime FechaIngreso { get => fechaIngreso; set => fechaIngreso = value; }

        public Empleado(int idEmpleado, int idUsuario, string nombre, string apellido,
                        string cargo, string area, string telefono, DateTime fechaIngreso)
        {
            this.IdEmpleado   = idEmpleado;
            this.IdUsuario    = idUsuario;
            this.Nombre       = nombre;
            this.Apellido     = apellido;
            this.Cargo        = cargo;
            this.Area         = area;
            this.Telefono     = telefono;
            this.FechaIngreso = fechaIngreso;
        }

        public Empleado() { }
    }
}
