using CapadeDatos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CapaDatos
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

        public static void Crear(string email, string passwordHash, string nombre, string apellido,
                                  string cargo, string area, string telefono,
                                  System.Nullable<DateTime> fechaIngreso)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                {
                    System.Nullable<int> idOut = 0;
                    DB.sp_Empleado_Crear(email, passwordHash, nombre, apellido,
                        cargo, area, telefono, fechaIngreso, ref idOut);
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al crear Empleado", ex); }
            finally { DB = null; }
        }

        public static void Actualizar(int idEmpleado, string cargo, string area, string telefono)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                {
                    DB.sp_Empleado_Actualizar(idEmpleado, cargo, area, telefono);
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
