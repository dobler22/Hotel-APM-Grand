using System;
using System.Collections.Generic;
using System.Linq;
using CapaDatos;
using CapaEntidades.Reserva;
using HotelAPMGrand.Entidades;

namespace Datos.Hotel
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

        public static void Crear(Reserva ol)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                {
                    int? idOut = 0;
                    DB.sp_Reserva_Crear(ol.IdCliente, ol.IdHabitacion,
                        ol.FechaEntrada, ol.FechaSalida, ref idOut);
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al crear Reserva", ex); }
            finally { DB = null; }
        }

        public static void Actualizar(Reserva ol)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                {
                    DB.sp_Reserva_Actualizar(ol.IdReserva, ol.FechaEntrada, ol.FechaSalida);
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
