using CapaDatos;
using CapadeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuario = CapadeEntidades.Usuario.Usuario;

namespace CapadeLogica
{
    public class UsuarioLN
    {
        public Usuario Login(string email, string passwordHash)
        {
            try
            {
                sp_LoginResult result = UsuarioCD.Login(email, passwordHash);
                if (result != null)
                {
                    Usuario us = new Usuario();
                    us.Id = result.id_usuario;
                    us.Email = result.email;
                    us.Rol = result.rol;
                    us.Activo = result.activo;
                    return us;
                }
                return null; 
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al realizar el inicio de sesión", ex);
            }
        }
        // 2. Listar todos los usuarios
        public List<Usuario> ListarUsuarios()
        {
            List<Usuario> lista = new List<Usuario>();
            try
            {
                List<sp_Usuario_ListarResult> auxLista = UsuarioCD.Listar();
                if (auxLista != null)
                {
                    foreach (sp_Usuario_ListarResult obj in auxLista)
                    {
                        Usuario usuario = new Usuario(
                            obj.id_usuario,
                            obj.email,
                            "", // El procedimiento no retorna password_hash por seguridad
                            obj.rol,
                            obj.activo,
                            obj.created_at,
                            obj.updated_at
                        );
                        lista.Add(usuario);
                    }
                }
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al mostrar la lista de usuarios", ex);
            }
            return lista;
        }

        // 3. Obtener un usuario por ID
        public Usuario ObtenerPorId(int idUsuario)
        {
            if (idUsuario <= 0)
                throw new LogicaExcepciones("El ID del usuario debe ser un número positivo.");

            try
            {
                sp_Usuario_ObtenerPorIdResult obj = UsuarioCD.ObtenerPorId(idUsuario);
                if (obj == null) return null;

                return new Usuario(
                    obj.id_usuario,
                    obj.email,
                    "", // El SP no incluye password_hash por seguridad
                    obj.rol,
                    obj.activo,
                    obj.created_at,
                    obj.updated_at
                );
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al obtener el usuario por ID", ex);
            }
        }

        // 4. Crear nuevo usuario
        public bool CrearUsuario(Usuario usuario)
        {
            if (usuario == null)
                throw new LogicaExcepciones("Los datos del usuario no pueden ser nulos.");

            if (string.IsNullOrWhiteSpace(usuario.Email))
                throw new LogicaExcepciones("El correo electrónico es obligatorio.");

            if (string.IsNullOrWhiteSpace(usuario.PasswordHash))
                throw new LogicaExcepciones("La contraseña es obligatoria.");

            if (string.IsNullOrWhiteSpace(usuario.Rol))
                throw new LogicaExcepciones("El rol asignado es obligatorio.");

            try
            {
                UsuarioCD.Crear(usuario.Email.Trim(), usuario.PasswordHash, usuario.Rol.Trim());
                return true;
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al registrar el nuevo usuario", ex);
            }
        }

        // 5. Actualizar usuario (Email y Rol)
        public bool ActualizarUsuario(Usuario usuario)
        {
            if (usuario == null)
                throw new LogicaExcepciones("Los datos del usuario no pueden ser nulos.");

            if (usuario.Id <= 0)
                throw new LogicaExcepciones("El ID del usuario es inválido.");

            if (string.IsNullOrWhiteSpace(usuario.Email))
                throw new LogicaExcepciones("El correo electrónico es obligatorio.");

            if (string.IsNullOrWhiteSpace(usuario.Rol))
                throw new LogicaExcepciones("El rol asignado es obligatorio.");

            try
            {
                UsuarioCD.Actualizar(usuario.Id, usuario.Email.Trim(), usuario.Rol.Trim());
                return true;
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al actualizar la información del usuario", ex);
            }
        }

        // 6. Cambiar el estado del usuario (Activo / Inactivo)
        public bool CambiarEstadoUsuario(int idUsuario, bool activo)
        {
            if (idUsuario <= 0)
                throw new LogicaExcepciones("El ID del usuario es inválido.");

            try
            {
                UsuarioCD.CambiarEstado(idUsuario, activo);
                return true;
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al cambiar el estado del usuario", ex);
            }
        }

        // 7. Cambiar la contraseña del usuario
        public bool CambiarPasswordUsuario(int idUsuario, string nuevoHash)
        {
            if (idUsuario <= 0)
                throw new LogicaExcepciones("El ID del usuario es inválido.");

            if (string.IsNullOrWhiteSpace(nuevoHash))
                throw new LogicaExcepciones("La nueva contraseña no puede estar vacía.");

            try
            {
                UsuarioCD.CambiarPassword(idUsuario, nuevoHash);
                return true;
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al actualizar la contraseña del usuario", ex);
            }
        }

        // 8. Eliminar usuario
        public bool EliminarUsuario(int idUsuario)
        {
            if (idUsuario <= 0)
                throw new LogicaExcepciones("El ID del usuario es inválido.");

            try
            {
                UsuarioCD.Eliminar(idUsuario);
                return true;
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al eliminar el usuario de la base de datos", ex);
            }
        }

        // Método adicional en UsuarioLN.cs
        public List<Usuario> ListarUsuariosPorRol(string rolSesion, string filtroEmail = "")
        {
            var todos = ListarUsuarios();

            // 1. Filtrar por texto de búsqueda si existe
            if (!string.IsNullOrWhiteSpace(filtroEmail))
            {
                todos = todos.Where(u => u.Email.ToLower().Contains(filtroEmail.ToLower().Trim())).ToList();
            }

            // 2. Aplicar restricciones de seguridad según el rol en sesión
            if (rolSesion == "Empleado")
            {
                // Un empleado solo puede gestionar/ver clientes
                return todos.Where(u => u.Rol.Equals("Cliente", StringComparison.OrdinalIgnoreCase)).ToList();
            }

            // Si es Administrador, retorna la lista completa
            return todos;
        }


    }
}
