using System;
using System.Collections.Generic;
using System.Linq;
using CapaDatos;
using HotelAPMGrand.Entidades;
using Mantenimiento = CapaDatos.Mantenimiento;

namespace Datos.Hotel
{
    public class MantenimientoCD
    {
        public static List<sp_Mantenimiento_ListarResult> Listar()
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                    return DB.sp_Mantenimiento_Listar().ToList();
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al listar Mantenimientos", ex); }
            finally { DB = null; }
        }

        public static List<sp_Mantenimiento_EnProgresoResult> ListarEnProgreso()
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                    return DB.sp_Mantenimiento_EnProgreso().ToList();
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al listar Mantenimientos en progreso", ex); }
            finally { DB = null; }
        }

        public static sp_Mantenimiento_ObtenerPorIdResult ObtenerPorId(int idMantenimiento)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                    return DB.sp_Mantenimiento_ObtenerPorId(idMantenimiento).FirstOrDefault();
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al obtener Mantenimiento por ID", ex); }
            finally { DB = null; }
        }

        public static void Crear(Mantenimiento ol)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                {
                    DB.sp_Mantenimiento_Crear(ol.IdHabitacion, ol.IdEmpleado,
                        ol.TipoTrabajo, ol.FechaInicio, ol.Observaciones);
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al crear Mantenimiento", ex); }
            finally { DB = null; }
        }

        public static void Actualizar(Mantenimiento ol)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                {
                    DB.sp_Mantenimiento_Actualizar(ol.IdMantenimiento,
                        ol.TipoTrabajo, ol.Observaciones);
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al actualizar Mantenimiento", ex); }
            finally { DB = null; }
        }

        public static void Finalizar(int idMantenimiento, DateTime fechaFin)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                {
                    DB.sp_Mantenimiento_Finalizar(idMantenimiento, fechaFin);
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al finalizar Mantenimiento", ex); }
            finally { DB = null; }
        }

        public static void Eliminar(int idMantenimiento)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                {
                    DB.sp_Mantenimiento_Eliminar(idMantenimiento);
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al eliminar Mantenimiento", ex); }
            finally { DB = null; }
        }
    }
}
