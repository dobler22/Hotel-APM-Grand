using CapaDatos;
using CapadeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cliente = CapadeEntidades.Cliente.Cliente;    

namespace CapadeLogica
{
    public class ClienteLN
    {
        // 1. Listar todos los clientes
        public List<Cliente> ListarClientes()
        {
            List<Cliente> lista = new List<Cliente>();
            try
            {
                List<sp_Cliente_ListarResult> auxLista = ClienteCD.Listar();
                if (auxLista != null)
                {
                    foreach (sp_Cliente_ListarResult obj in auxLista)
                    {
                        Cliente cli = new Cliente(
                            obj.id_cliente,
                            0, // sp_Cliente_Listar no retorna id_usuario
                            obj.nombre,
                            obj.apellido,
                            obj.telefono ?? "",
                            obj.documento_identidad ?? "",
                            obj.nacionalidad ?? "",
                            obj.fecha_nacimiento ?? DateTime.MinValue
                        );
                        lista.Add(cli);
                    }
                }
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al mostrar la lista de clientes", ex);
            }
            return lista;
        }

        // 2. Obtener cliente por ID
        public Cliente ObtenerPorId(int idCliente)
        {
            if (idCliente <= 0)
                throw new LogicaExcepciones("El ID del cliente debe ser un número positivo.");

            try
            {
                sp_Cliente_ObtenerPorIdResult obj = ClienteCD.ObtenerPorId(idCliente);
                if (obj == null) return null;

                return new Cliente(
                    obj.id_cliente,
                    obj.id_usuario, // sp_Cliente_ObtenerPorId sí proyecta c.id_usuario
                    obj.nombre,
                    obj.apellido,
                    obj.telefono ?? "",
                    obj.documento_identidad ?? "",
                    obj.nacionalidad ?? "",
                    obj.fecha_nacimiento ?? DateTime.MinValue
                );
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al obtener el cliente por ID", ex);
            }
        }

        // 3. Crear cliente (incluye su cuenta de usuario con rol 'cliente')
        public bool CrearCliente(string email, string passwordHash, Cliente cliente)
        {
            if (cliente == null)
                throw new LogicaExcepciones("Los datos del cliente no pueden ser nulos.");

            if (string.IsNullOrWhiteSpace(email))
                throw new LogicaExcepciones("El correo electrónico es obligatorio.");

            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new LogicaExcepciones("La contraseña es obligatoria.");

            if (string.IsNullOrWhiteSpace(cliente.Nombre))
                throw new LogicaExcepciones("El nombre del cliente es obligatorio.");

            if (string.IsNullOrWhiteSpace(cliente.Apellido))
                throw new LogicaExcepciones("El apellido del cliente es obligatorio.");

            try
            {
                ClienteCD.Crear(
                    email.Trim(),
                    passwordHash,
                    cliente.Nombre.Trim(),
                    cliente.Apellido.Trim(),
                    string.IsNullOrWhiteSpace(cliente.Telefono) ? null : cliente.Telefono.Trim(),
                    string.IsNullOrWhiteSpace(cliente.DocumentoIdentidad) ? null : cliente.DocumentoIdentidad.Trim(),
                    string.IsNullOrWhiteSpace(cliente.Nacionalidad) ? null : cliente.Nacionalidad.Trim(),
                    cliente.FechaNacimiento == DateTime.MinValue ? (DateTime?)null : cliente.FechaNacimiento
                );
                return true;
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al registrar el nuevo cliente", ex);
            }
        }

        // 4. Actualizar datos personales de un cliente existente
        public bool ActualizarCliente(Cliente cliente)
        {
            if (cliente == null)
                throw new LogicaExcepciones("Los datos del cliente no pueden ser nulos.");

            if (cliente.IdCliente <= 0)
                throw new LogicaExcepciones("El ID del cliente es inválido.");

            if (string.IsNullOrWhiteSpace(cliente.Nombre))
                throw new LogicaExcepciones("El nombre es obligatorio.");

            if (string.IsNullOrWhiteSpace(cliente.Apellido))
                throw new LogicaExcepciones("El apellido es obligatorio.");

            try
            {
                ClienteCD.Actualizar(
                    cliente.IdCliente,
                    cliente.Nombre.Trim(),
                    cliente.Apellido.Trim(),
                    string.IsNullOrWhiteSpace(cliente.Telefono) ? null : cliente.Telefono.Trim(),
                    string.IsNullOrWhiteSpace(cliente.Nacionalidad) ? null : cliente.Nacionalidad.Trim(),
                    cliente.FechaNacimiento == DateTime.MinValue ? (DateTime?)null : cliente.FechaNacimiento
                );
                return true;
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al actualizar la información del cliente", ex);
            }
        }

        // 5. Buscar clientes por documento, nombre, apellido o email (desde Base de Datos)
        public List<sp_Cliente_BuscarResult> BuscarClientes(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return new List<sp_Cliente_BuscarResult>();

            try
            {
                return ClienteCD.Buscar(texto.Trim());
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al buscar clientes", ex);
            }
        }

        // 6. Obtener historial de reservas y consumos del cliente
        public List<sp_Cliente_HistorialResult> ObtenerHistorial(int idCliente)
        {
            if (idCliente <= 0)
                throw new LogicaExcepciones("El ID del cliente es inválido.");

            try
            {
                return ClienteCD.Historial(idCliente);
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al consultar el historial del cliente", ex);
            }
        }

        // 7. Eliminar cliente (elimina también la cuenta de usuario asociada por la FK)
        public bool EliminarCliente(int idCliente)
        {
            if (idCliente <= 0)
                throw new LogicaExcepciones("El ID del cliente es inválido.");

            try
            {
                ClienteCD.Eliminar(idCliente);
                return true;
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al eliminar el cliente", ex);
            }
        }

        // 8. Obtener datos del cliente buscando por id_usuario (CORREGIDO)
        public Cliente ObtenerPorIdUsuario(int idUsuario)
        {
            if (idUsuario <= 0)
                throw new LogicaExcepciones("El ID de usuario es inválido.");

            try
            {
                List<Cliente> clientes = ListarClientes();

                // Recorremos los clientes y consultamos el detalle individual que SÍ trae id_usuario
                foreach (var c in clientes)
                {
                    Cliente cliDetalle = ObtenerPorId(c.IdCliente);
                    if (cliDetalle != null && cliDetalle.IdUsuario == idUsuario)
                    {
                        return cliDetalle;
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al buscar cliente por ID de usuario", ex);
            }
        }
    }
}
