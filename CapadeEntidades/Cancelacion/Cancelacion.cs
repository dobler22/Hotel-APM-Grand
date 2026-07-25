using System;

namespace HotelAPMGrand.Entidades
{
    public class Cancelacion
    {
        private int      idCancelacion;
        private int      idReserva;
        private DateTime fechaCancelacion;
        private string   motivo;
        private decimal  penalizacion;
        private decimal  reembolso;
        private string   solicitadoPor;

        public int      IdCancelacion    { get => idCancelacion;    set => idCancelacion    = value; }
        public int      IdReserva        { get => idReserva;        set => idReserva        = value; }
        public DateTime FechaCancelacion { get => fechaCancelacion; set => fechaCancelacion = value; }
        public string   Motivo           { get => motivo;           set => motivo           = value; }
        public decimal  Penalizacion     { get => penalizacion;     set => penalizacion     = value; }
        public decimal  Reembolso        { get => reembolso;        set => reembolso        = value; }
        public string   SolicitadoPor    { get => solicitadoPor;    set => solicitadoPor    = value; }

        public Cancelacion(int idCancelacion, int idReserva, DateTime fechaCancelacion,
                           string motivo, decimal penalizacion, decimal reembolso,
                           string solicitadoPor)
        {
            this.IdCancelacion    = idCancelacion;
            this.IdReserva        = idReserva;
            this.FechaCancelacion = fechaCancelacion;
            this.Motivo           = motivo;
            this.Penalizacion     = penalizacion;
            this.Reembolso        = reembolso;
            this.SolicitadoPor    = solicitadoPor;
        }

        public Cancelacion() { }
    }
}
