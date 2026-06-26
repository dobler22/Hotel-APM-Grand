using System;
using System.Collections.Generic;
using System.Linq;
using CapaDatos;
using CapaEntidades.Servicio;
using HotelAPMGrand.Entidades;

namespace Datos.Hotel
{
    public class ServicioCD
    {
        public static List<sp_Servicio_ListarResult> Listar()
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                    return DB.sp_Servicio_Listar().ToList();
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al listar Servicios", ex); }
            finally { DB = null; }
        }

        public static sp_Servicio_ObtenerPorIdResult ObtenerPorId(int idServicio)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                    return DB.sp_Servicio_ObtenerPorId(idServicio).FirstOrDefault();
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al obtener Servicio por ID", ex); }
            finally { DB = null; }
        }

        public static void Crear(Servicio ol)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                {
                    DB.sp_Servicio_Crear(ol.Nombre, ol.Descripcion, ol.Precio);
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al crear Servicio", ex); }
            finally { DB = null; }
        }

        public static void Actualizar(Servicio ol)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                {
                    DB.sp_Servicio_Actualizar(ol.IdServicio, ol.Nombre,
                        ol.Descripcion, ol.Precio);
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al actualizar Servicio", ex); }
            finally { DB = null; }
        }

        public static void CambiarDisponibilidad(int idServicio, bool disponible)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                {
                    DB.sp_Servicio_CambiarDisponibilidad(idServicio, disponible);
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al cambiar disponibilidad de Servicio", ex); }
            finally { DB = null; }
        }

        public static void Eliminar(int idServicio)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                {
                    DB.sp_Servicio_Eliminar(idServicio);
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al eliminar Servicio", ex); }
            finally { DB = null; }
        }
    }
}
