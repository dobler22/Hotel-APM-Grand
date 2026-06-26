namespace CapaEntidades.Cliente
{
    public class VwCliente
    {
        private string nombreCompleto;
        private string documento;
        private string nacionalidad;
        private string telefono;
        private string correo;
        private string fechaNacimiento;
        private string estado;

        public string NombreCompleto  { get => nombreCompleto;  set => nombreCompleto  = value; }
        public string Documento       { get => documento;       set => documento       = value; }
        public string Nacionalidad    { get => nacionalidad;    set => nacionalidad    = value; }
        public string Telefono        { get => telefono;        set => telefono        = value; }
        public string Correo          { get => correo;          set => correo          = value; }
        public string FechaNacimiento { get => fechaNacimiento; set => fechaNacimiento = value; }
        public string Estado          { get => estado;          set => estado          = value; }

        public VwCliente(string nombreCompleto, string documento, string nacionalidad,
                         string telefono, string correo, string fechaNacimiento, string estado)
        {
            this.NombreCompleto  = nombreCompleto;
            this.Documento       = documento;
            this.Nacionalidad    = nacionalidad;
            this.Telefono        = telefono;
            this.Correo          = correo;
            this.FechaNacimiento = fechaNacimiento;
            this.Estado          = estado;
        }

        public VwCliente() { }
    }
}
