using System;

namespace CapaEntidades.Cliente
{
    public class Cliente
    {
        private int      idCliente;
        private int      idUsuario;
        private string   nombre;
        private string   apellido;
        private string   telefono;
        private string   documentoIdentidad;
        private string   nacionalidad;
        private DateTime fechaNacimiento;

        public int      IdCliente          { get => idCliente;          set => idCliente          = value; }
        public int      IdUsuario          { get => idUsuario;          set => idUsuario          = value; }
        public string   Nombre             { get => nombre;             set => nombre             = value; }
        public string   Apellido           { get => apellido;           set => apellido           = value; }
        public string   Telefono           { get => telefono;           set => telefono           = value; }
        public string   DocumentoIdentidad { get => documentoIdentidad; set => documentoIdentidad = value; }
        public string   Nacionalidad       { get => nacionalidad;       set => nacionalidad       = value; }
        public DateTime FechaNacimiento    { get => fechaNacimiento;    set => fechaNacimiento    = value; }

        public Cliente(int idCliente, int idUsuario, string nombre, string apellido,
                       string telefono, string documentoIdentidad, string nacionalidad,
                       DateTime fechaNacimiento)
        {
            this.IdCliente          = idCliente;
            this.IdUsuario          = idUsuario;
            this.Nombre             = nombre;
            this.Apellido           = apellido;
            this.Telefono           = telefono;
            this.DocumentoIdentidad = documentoIdentidad;
            this.Nacionalidad       = nacionalidad;
            this.FechaNacimiento    = fechaNacimiento;
        }

        public Cliente() { }
    }
}
