using CapadeDatos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CapaDatos
{
    public class ServicioReservaCD
    {
        public static List<sp_ServicioReserva_ListarResult> Listar(int idReserva)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                    return DB.sp_ServicioReserva_Listar(idReserva).ToList();
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al listar Servicios de Reserva", ex); }
            finally { DB = null; }
        }

        public static sp_ServicioReserva_ObtenerPorIdResult ObtenerPorId(int idServReserva)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                    return DB.sp_ServicioReserva_ObtenerPorId(idServReserva).FirstOrDefault();
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al obtener ServicioReserva por ID", ex); }
            finally { DB = null; }
        }

        public static void Agregar(int idReserva, int idServicio, int cantidad)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                {
                    DB.sp_ServicioReserva_Agregar(idReserva, idServicio, cantidad);
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al agregar Servicio a Reserva", ex); }
            finally { DB = null; }
        }

        public static void Actualizar(int idServReserva, int cantidad)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                {
                    DB.sp_ServicioReserva_Actualizar(idServReserva, cantidad);
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al actualizar ServicioReserva", ex); }
            finally { DB = null; }
        }

        public static void Quitar(int idServReserva)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                {
                    DB.sp_ServicioReserva_Quitar(idServReserva);
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al quitar Servicio de Reserva", ex); }
            finally { DB = null; }
        }
    }
}
