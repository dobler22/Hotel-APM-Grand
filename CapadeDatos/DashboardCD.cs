using CapadeDatos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CapaDatos
{
    public class DashboardCD
    {
        public static sp_Dashboard_KPIsAdminResult KPIsAdmin()
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                    return DB.sp_Dashboard_KPIsAdmin().FirstOrDefault();
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al obtener KPIs de Admin", ex); }
            finally { DB = null; }
        }

        public static List<sp_Dashboard_ResumenEmpleadoResult> ResumenEmpleado()
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                    return DB.sp_Dashboard_ResumenEmpleado().ToList();
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al obtener resumen de Empleado", ex); }
            finally { DB = null; }
        }
    }
}
