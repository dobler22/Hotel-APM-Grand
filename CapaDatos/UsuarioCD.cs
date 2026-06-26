using System;
using System.Collections.Generic;
using System.Linq;
using CapaDatos;
using CapaEntidades.Usuario;
using HotelAPMGrand.Entidades;

namespace Datos.Hotel
{
    public class UsuarioCD
    {
        // ── LISTAR ────────────────────────────────────────
        public static List<sp_Usuario_ListarResult> Listar()
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                    return DB.sp_Usuario_Listar().ToList();
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al listar Usuarios", ex);
            }
            finally { DB = null; }
        }

        // ── OBTENER POR ID ────────────────────────────────
        public static sp_Usuario_ObtenerPorIdResult ObtenerPorId(int idUsuario)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                    return DB.sp_Usuario_ObtenerPorId(idUsuario).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al obtener Usuario por ID", ex);
            }
            finally { DB = null; }
        }

        // ── CREAR ─────────────────────────────────────────
        public static void Crear(Usuario ol)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                {
                    int? idOut = 0;
                    DB.sp_Usuario_Crear(ol.Email, ol.PasswordHash, ol.Rol, ref idOut);
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al crear Usuario", ex);
            }
            finally { DB = null; }
        }

        // ── ACTUALIZAR ────────────────────────────────────
        public static void Actualizar(Usuario ol)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                {
                    DB.sp_Usuario_Actualizar(ol.Id, ol.Email, ol.Rol);
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al actualizar Usuario", ex);
            }
            finally { DB = null; }
        }

        // ── CAMBIAR ESTADO ────────────────────────────────
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
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al cambiar estado de Usuario", ex);
            }
            finally { DB = null; }
        }

        // ── CAMBIAR PASSWORD ──────────────────────────────
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
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al cambiar contraseña de Usuario", ex);
            }
            finally { DB = null; }
        }

        // ── ELIMINAR ──────────────────────────────────────
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
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al eliminar Usuario", ex);
            }
            finally { DB = null; }
        }

        // ── LOGIN ─────────────────────────────────────────
        public static sp_LoginResult Login(string email, string passwordHash)
        {
            Hotel_APM_GrandDataContext DB = null;
            try
            {
                using (DB = new Hotel_APM_GrandDataContext())
                    return DB.sp_Login(email, passwordHash).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al iniciar sesión", ex);
            }
            finally { DB = null; }
        }
    }
}
