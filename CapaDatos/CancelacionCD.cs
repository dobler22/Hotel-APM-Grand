using System;
using System.Collections.Generic;
using System.Linq;
using CapaDatos;
using HotelAPMGrand.Entidades;
using System.Data.SqlClient;

namespace Datos.Hotel
{
    public class CancelacionCD
    {
        public static List<sp_Cancelacion_ListarResult> Listar(DateTime? fechaInicio, DateTime? fechaFin)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                    return DB.sp_Cancelacion_Listar(fechaInicio, fechaFin).ToList();
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al listar Cancelaciones", ex); }
            finally { DB = null; }
        }

        public static sp_Cancelacion_ObtenerPorIdResult ObtenerPorId(int idCancelacion)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                    return DB.sp_Cancelacion_ObtenerPorId(idCancelacion).FirstOrDefault();
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al obtener Cancelacion por ID", ex); }
            finally { DB = null; }
        }

        public static void Registrar(Cancelacion ol)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                {
                    DB.sp_Cancelacion_Registrar(ol.IdReserva, ol.Motivo,
                        ol.Penalizacion, ol.Reembolso, ol.SolicitadoPor);
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al registrar Cancelacion", ex); }
            finally { DB = null; }
        }

        public static void Actualizar(Cancelacion ol)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                {
                    DB.sp_Cancelacion_Actualizar(ol.IdCancelacion, ol.Motivo,
                        ol.Penalizacion, ol.Reembolso);
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al actualizar Cancelacion", ex); }
            finally { DB = null; }
        }

        public static void Eliminar(int idCancelacion)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                {
                    DB.sp_Cancelacion_Eliminar(idCancelacion);
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al eliminar Cancelacion", ex); }
            finally { DB = null; }
        }
    }
}
