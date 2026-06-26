using System;
using System.Collections.Generic;
using System.Linq;
using CapaDatos;
using CapaEntidades.Cliente;
using HotelAPMGrand.Entidades;

namespace Datos.Hotel
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

        public static void Crear(Cliente ol)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                {
                    int? idOut = 0;
                    DB.sp_Cliente_Crear(ol.Nombre, ol.Apellido, ol.Telefono,
                        ol.DocumentoIdentidad, ol.Nacionalidad, ol.FechaNacimiento,
                        "", "", ref idOut);
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al crear Cliente", ex); }
            finally { DB = null; }
        }

        public static void Actualizar(Cliente ol)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                {
                    DB.sp_Cliente_Actualizar(ol.IdCliente, ol.Nombre, ol.Apellido,
                        ol.Telefono, ol.Nacionalidad, ol.FechaNacimiento);
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
