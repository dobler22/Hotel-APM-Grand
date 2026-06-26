using System;

namespace CapaEntidades.Cancelacion
{
    public class VwCancelacion
    {
        private string   cliente;
        private string   habitacion;
        private DateTime entradaPrevista;
        private DateTime salidaPrevista;
        private DateTime fechaCancelacion;
        private string   motivo;
        private decimal  penalizacion;
        private decimal  reembolso;
        private string   solicitadoPor;

        public string   Cliente          { get => cliente;          set => cliente          = value; }
        public string   Habitacion       { get => habitacion;       set => habitacion       = value; }
        public DateTime EntradaPrevista  { get => entradaPrevista;  set => entradaPrevista  = value; }
        public DateTime SalidaPrevista   { get => salidaPrevista;   set => salidaPrevista   = value; }
        public DateTime FechaCancelacion { get => fechaCancelacion; set => fechaCancelacion = value; }
        public string   Motivo           { get => motivo;           set => motivo           = value; }
        public decimal  Penalizacion     { get => penalizacion;     set => penalizacion     = value; }
        public decimal  Reembolso        { get => reembolso;        set => reembolso        = value; }
        public string   SolicitadoPor    { get => solicitadoPor;    set => solicitadoPor    = value; }

        public VwCancelacion(string cliente, string habitacion, DateTime entradaPrevista,
                             DateTime salidaPrevista, DateTime fechaCancelacion, string motivo,
                             decimal penalizacion, decimal reembolso, string solicitadoPor)
        {
            this.Cliente          = cliente;
            this.Habitacion       = habitacion;
            this.EntradaPrevista  = entradaPrevista;
            this.SalidaPrevista   = salidaPrevista;
            this.FechaCancelacion = fechaCancelacion;
            this.Motivo           = motivo;
            this.Penalizacion     = penalizacion;
            this.Reembolso        = reembolso;
            this.SolicitadoPor    = solicitadoPor;
        }

        public VwCancelacion() { }
    }
}
