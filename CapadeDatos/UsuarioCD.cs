using CapadeDatos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CapaDatos
{
    public class UsuarioCD
    {
        public static List<sp_Usuario_ListarResult> Listar()
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                    return DB.sp_Usuario_Listar().ToList();
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al listar Usuarios", ex); }
            finally { DB = null; }
        }

        public static sp_Usuario_ObtenerPorIdResult ObtenerPorId(int idUsuario)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                    return DB.sp_Usuario_ObtenerPorId(idUsuario).FirstOrDefault();
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al obtener Usuario por ID", ex); }
            finally { DB = null; }
        }

        public static void Crear(string email, string passwordHash, string rol)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                {
                    System.Nullable<int> idOut = 0;
                    DB.sp_Usuario_Crear(email, passwordHash, rol, ref idOut);
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al crear Usuario", ex); }
            finally { DB = null; }
        }

        public static void Actualizar(int idUsuario, string email, string rol)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                {
                    DB.sp_Usuario_Actualizar(idUsuario, email, rol);
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al actualizar Usuario", ex); }
            finally { DB = null; }
        }

        public static void CambiarEstado(int idUsuario, bool activo)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                {
                    DB.sp_Usuario_CambiarEstado(idUsuario, activo);
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al cambiar estado de Usuario", ex); }
            finally { DB = null; }
        }

        public static void CambiarPassword(int idUsuario, string nuevoHash)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                {
                    DB.sp_Usuario_CambiarPassword(idUsuario, nuevoHash);
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al cambiar contraseña", ex); }
            finally { DB = null; }
        }

        public static void Eliminar(int idUsuario)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                {
                    DB.sp_Usuario_Eliminar(idUsuario);
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al eliminar Usuario", ex); }
            finally { DB = null; }
        }

        public static sp_LoginResult Login(string email, string passwordHash)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                    return DB.sp_Login(email, passwordHash).FirstOrDefault();
            }
            catch (Exception ex) { throw new DatosExcepciones("Error al iniciar sesión", ex); }
            finally { DB = null; }
        }
    }
}
