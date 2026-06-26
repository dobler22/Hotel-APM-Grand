using System;
using System.Collections.Generic;
using System.Linq;
using CapaDatos;
using HotelAPMGrand.Entidades;

namespace Datos.Hotel
{
    public class ResenaCD
    {
        public static List<sp_Resena_ListarResult> Listar(int calificacionMinima = 1)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                    return DB.sp_Resena_Listar(calificacionMinima).ToList();
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al listar Reseñas", ex); }
            finally { DB = null; }
        }

        public static sp_Resena_ObtenerPorIdResult ObtenerPorId(int idResena)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                    return DB.sp_Resena_ObtenerPorId(idResena).FirstOrDefault();
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al obtener Reseña por ID", ex); }
            finally { DB = null; }
        }

        public static sp_Resena_PromedioGeneralResult PromedioGeneral()
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                    return DB.sp_Resena_PromedioGeneral().FirstOrDefault();
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al obtener promedio de Reseñas", ex); }
            finally { DB = null; }
        }

        public static void Crear(Resena ol)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                {
                    DB.sp_Resena_Crear(ol.IdReserva, ol.IdCliente,
                        ol.Calificacion, ol.Comentario);
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al crear Reseña", ex); }
            finally { DB = null; }
        }

        public static void Actualizar(Resena ol)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                {
                    DB.sp_Resena_Actualizar(ol.IdResena, ol.Calificacion, ol.Comentario);
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al actualizar Reseña", ex); }
            finally { DB = null; }
        }

        public static void Eliminar(int idResena)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                {
                    DB.sp_Resena_Eliminar(idResena);
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al eliminar Reseña", ex); }
            finally { DB = null; }
        }
    }
}
