using CapadeDatos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CapaDatos
{
    public class HabitacionCD
    {
        public static List<sp_Habitacion_ListarResult> Listar()
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                    return DB.sp_Habitacion_Listar().ToList();
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al listar Habitaciones", ex); }
            finally { DB = null; }
        }

        public static List<sp_Habitacion_DisponiblesResult> ListarDisponibles(
            System.Nullable<DateTime> entrada, System.Nullable<DateTime> salida, string tipo)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                    return DB.sp_Habitacion_Disponibles(entrada, salida, tipo).ToList();
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al listar Habitaciones disponibles", ex); }
            finally { DB = null; }
        }

        public static sp_Habitacion_ObtenerPorIdResult ObtenerPorId(int idHabitacion)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                    return DB.sp_Habitacion_ObtenerPorId(idHabitacion).FirstOrDefault();
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al obtener Habitacion por ID", ex); }
            finally { DB = null; }
        }

        public static void Crear(string numero, string tipo, int capacidad,
                                  int piso, decimal precioPorNoche)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                {
                    DB.sp_Habitacion_Crear(numero, tipo, capacidad, piso, precioPorNoche);
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al crear Habitacion", ex); }
            finally { DB = null; }
        }

        public static void Actualizar(int idHabitacion, string tipo, int capacidad, decimal precio)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                {
                    DB.sp_Habitacion_Actualizar(idHabitacion, tipo, capacidad, precio);
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al actualizar Habitacion", ex); }
            finally { DB = null; }
        }

        public static void CambiarEstado(int idHabitacion, string estado)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                {
                    DB.sp_Habitacion_CambiarEstado(idHabitacion, estado);
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al cambiar estado de Habitacion", ex); }
            finally { DB = null; }
        }

        public static void Eliminar(int idHabitacion)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                {
                    DB.sp_Habitacion_Eliminar(idHabitacion);
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al eliminar Habitacion", ex); }
            finally { DB = null; }
        }
    }
}
