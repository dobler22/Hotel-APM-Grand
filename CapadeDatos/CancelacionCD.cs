using CapadeDatos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CapaDatos
{
    public class CancelacionCD
    {
        public static List<sp_Cancelacion_ListarResult> Listar(
            System.Nullable<DateTime> fechaInicio, System.Nullable<DateTime> fechaFin)
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

        public static void Registrar(int idReserva, string motivo,
                                      System.Nullable<decimal> penalizacion,
                                      System.Nullable<decimal> reembolso, string solicitadoPor)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                {
                    DB.sp_Cancelacion_Registrar(idReserva, motivo, penalizacion, reembolso, solicitadoPor);
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al registrar Cancelacion", ex); }
            finally { DB = null; }
        }

        public static void Actualizar(int idCancelacion, string motivo,
                                       System.Nullable<decimal> penalizacion,
                                       System.Nullable<decimal> reembolso)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                {
                    DB.sp_Cancelacion_Actualizar(idCancelacion, motivo, penalizacion, reembolso);
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
