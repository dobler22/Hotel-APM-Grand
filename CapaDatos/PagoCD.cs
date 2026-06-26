using System;
using System.Collections.Generic;
using System.Linq;
using CapaDatos;
using HotelAPMGrand.Entidades;

namespace Datos.Hotel
{
    public class PagoCD
    {
        public static List<sp_Pago_ListarResult> Listar()
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                    return DB.sp_Pago_Listar().ToList();
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al listar Pagos", ex); }
            finally { DB = null; }
        }

        public static List<sp_Pago_HistorialPorReservaResult> HistorialPorReserva(int idReserva)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                    return DB.sp_Pago_HistorialPorReserva(idReserva).ToList();
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al obtener historial de Pagos", ex); }
            finally { DB = null; }
        }

        public static sp_Pago_ObtenerPorIdResult ObtenerPorId(int idPago)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                    return DB.sp_Pago_ObtenerPorId(idPago).FirstOrDefault();
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al obtener Pago por ID", ex); }
            finally { DB = null; }
        }

        public static void Registrar(Pago ol)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                {
                    int? idOut = 0;
                    DB.sp_Pago_Registrar(ol.IdReserva, ol.Monto,
                        ol.MetodoPago, ol.Referencia, ref idOut);
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al registrar Pago", ex); }
            finally { DB = null; }
        }

        public static void CambiarEstado(int idPago, string estado)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                {
                    DB.sp_Pago_CambiarEstado(idPago, estado);
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al cambiar estado de Pago", ex); }
            finally { DB = null; }
        }

        public static void Eliminar(int idPago)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                {
                    DB.sp_Pago_Eliminar(idPago);
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al eliminar Pago", ex); }
            finally { DB = null; }
        }
    }
}
