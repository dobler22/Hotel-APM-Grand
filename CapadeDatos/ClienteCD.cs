using CapadeDatos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CapaDatos
{
    public class ClienteCD
    {
        public static List<sp_Cliente_ListarResult> Listar()
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                    return DB.sp_Cliente_Listar().ToList();
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al listar Clientes", ex); }
            finally { DB = null; }
        }

        public static sp_Cliente_ObtenerPorIdResult ObtenerPorId(int idCliente)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                    return DB.sp_Cliente_ObtenerPorId(idCliente).FirstOrDefault();
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al obtener Cliente por ID", ex); }
            finally { DB = null; }
        }

        public static void Crear(string email, string passwordHash, string nombre, string apellido,
                                  string telefono, string documento, string nacionalidad,
                                  System.Nullable<DateTime> fechaNacimiento)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                {
                    System.Nullable<int> idOut = 0;
                    DB.sp_Cliente_Crear(email, passwordHash, nombre, apellido,
                        telefono, documento, nacionalidad, fechaNacimiento, ref idOut);
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al crear Cliente", ex); }
            finally { DB = null; }
        }

        public static void Actualizar(int idCliente, string nombre, string apellido,
                                       string telefono, string nacionalidad,
                                       System.Nullable<DateTime> fechaNacimiento)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                {
                    DB.sp_Cliente_Actualizar(idCliente, nombre, apellido, telefono, nacionalidad, fechaNacimiento);
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al actualizar Cliente", ex); }
            finally { DB = null; }
        }

        public static List<sp_Cliente_BuscarResult> Buscar(string texto)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                    return DB.sp_Cliente_Buscar(texto).ToList();
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al buscar Clientes", ex); }
            finally { DB = null; }
        }

        public static List<sp_Cliente_HistorialResult> Historial(int idCliente)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                    return DB.sp_Cliente_Historial(idCliente).ToList();
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al obtener historial del Cliente", ex); }
            finally { DB = null; }
        }

        public static void Eliminar(int idCliente)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                {
                    DB.sp_Cliente_Eliminar(idCliente);
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al eliminar Cliente", ex); }
            finally { DB = null; }
        }
    }
}
