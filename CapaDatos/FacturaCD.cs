using System;
using System.Collections.Generic;
using System.Linq;
using CapaDatos;
using HotelAPMGrand.Entidades;

namespace Datos.Hotel
{
    public class FacturaCD
    {
        public static List<sp_Factura_ListarResult> Listar()
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                    return DB.sp_Factura_Listar().ToList();
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al listar Facturas", ex); }
            finally { DB = null; }
        }

        public static List<sp_Factura_ListarPendientesResult> ListarPendientes()
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                    return DB.sp_Factura_ListarPendientes().ToList();
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al listar Facturas pendientes", ex); }
            finally { DB = null; }
        }

        public static sp_Factura_ObtenerPorIdResult ObtenerPorId(int idFactura)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                    return DB.sp_Factura_ObtenerPorId(idFactura).FirstOrDefault();
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al obtener Factura por ID", ex); }
            finally { DB = null; }
        }

        public static void Generar(int idReserva)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                {
                    int? idOut = 0;
                    DB.sp_Factura_Generar(idReserva, ref idOut);
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al generar Factura", ex); }
            finally { DB = null; }
        }

        public static void MarcarPagada(int idFactura)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                {
                    DB.sp_Factura_MarcarPagada(idFactura);
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al marcar Factura como pagada", ex); }
            finally { DB = null; }
        }

        public static void Anular(int idFactura)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                {
                    DB.sp_Factura_Anular(idFactura);
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al anular Factura", ex); }
            finally { DB = null; }
        }

        public static void Eliminar(int idFactura)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                {
                    DB.sp_Factura_Eliminar(idFactura);
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al eliminar Factura", ex); }
            finally { DB = null; }
        }
    }
}
