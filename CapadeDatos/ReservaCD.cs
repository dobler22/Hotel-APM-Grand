using CapadeDatos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CapaDatos
{
    public class ReservaCD
    {
        public static List<sp_Reserva_ListarResult> Listar()
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                    return DB.sp_Reserva_Listar().ToList();
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al listar Reservas", ex); }
            finally { DB = null; }
        }

        public static List<sp_Reserva_ListarPorEstadoResult> ListarPorEstado(string estado)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                    return DB.sp_Reserva_ListarPorEstado(estado).ToList();
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al listar Reservas por estado", ex); }
            finally { DB = null; }
        }

        public static sp_Reserva_ObtenerPorIdResult ObtenerPorId(int idReserva)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                    return DB.sp_Reserva_ObtenerPorId(idReserva).FirstOrDefault();
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al obtener Reserva por ID", ex); }
            finally { DB = null; }
        }

        public static List<sp_Reserva_DetalleResult> Detalle(int idReserva)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                    return DB.sp_Reserva_Detalle(idReserva).ToList();
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al obtener detalle de Reserva", ex); }
            finally { DB = null; }
        }

        public static void Crear(int idCliente, int idHabitacion,
                                  System.Nullable<DateTime> fechaEntrada,
                                  System.Nullable<DateTime> fechaSalida)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                {
                    System.Nullable<int> idOut = 0;
                    DB.sp_Reserva_Crear(idCliente, idHabitacion, fechaEntrada, fechaSalida, ref idOut);
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al crear Reserva", ex); }
            finally { DB = null; }
        }

        public static void Actualizar(int idReserva,
                                       System.Nullable<DateTime> fechaEntrada,
                                       System.Nullable<DateTime> fechaSalida)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                {
                    DB.sp_Reserva_Actualizar(idReserva, fechaEntrada, fechaSalida);
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al actualizar Reserva", ex); }
            finally { DB = null; }
        }

        public static void CambiarEstado(int idReserva, string estado)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                {
                    DB.sp_Reserva_CambiarEstado(idReserva, estado);
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al cambiar estado de Reserva", ex); }
            finally { DB = null; }
        }

        public static void Eliminar(int idReserva)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                {
                    DB.sp_Reserva_Eliminar(idReserva);
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al eliminar Reserva", ex); }
            finally { DB = null; }
        }
    }
}
