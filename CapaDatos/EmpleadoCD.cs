using System;
using System.Collections.Generic;
using System.Linq;
using CapaDatos;
using CapaEntidades.Empleado;
using HotelAPMGrand.Entidades;

namespace Datos.Hotel
{
    public class EmpleadoCD
    {
        public static List<sp_Empleado_ListarResult> Listar()
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                    return DB.sp_Empleado_Listar().ToList();
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al listar Empleados", ex); }
            finally { DB = null; }
        }

        public static List<sp_Empleado_ListarActivosResult> ListarActivos()
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                    return DB.sp_Empleado_ListarActivos().ToList();
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al listar Empleados activos", ex); }
            finally { DB = null; }
        }

        public static sp_Empleado_ObtenerPorIdResult ObtenerPorId(int idEmpleado)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                    return DB.sp_Empleado_ObtenerPorId(idEmpleado).FirstOrDefault();
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al obtener Empleado por ID", ex); }
            finally { DB = null; }
        }

        public static void Crear(Empleado ol)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                {
                    int? idOut = 0;
                    DB.sp_Empleado_Crear(ol.Nombre, ol.Apellido, ol.Cargo,
                        ol.Area, ol.Telefono, ol.FechaIngreso, "", "", ref idOut);
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al crear Empleado", ex); }
            finally { DB = null; }
        }

        public static void Actualizar(Empleado ol)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                {
                    DB.sp_Empleado_Actualizar(ol.IdEmpleado, ol.Cargo, ol.Area, ol.Telefono);
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al actualizar Empleado", ex); }
            finally { DB = null; }
        }

        public static void Eliminar(int idEmpleado)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                {
                    DB.sp_Empleado_Eliminar(idEmpleado);
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al eliminar Empleado", ex); }
            finally { DB = null; }
        }
    }
}
