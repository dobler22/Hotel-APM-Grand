using CapaDatos;
using CapadeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Empleado = CapadeEntidades.Empleado.Empleado;


namespace CapadeLogica
{
    public class EmpleadoLN
    {
        // 1. Listar todos los empleados
        public List<Empleado> ListarEmpleados()
        {
            List<Empleado> lista = new List<Empleado>();
            try
            {
                List<sp_Empleado_ListarResult> auxLista = EmpleadoCD.Listar();
                if (auxLista != null)
                {
                    foreach (sp_Empleado_ListarResult obj in auxLista)
                    {
                        Empleado emp = new Empleado(
                            obj.id_empleado,
                            0, // sp_Empleado_Listar no retorna id_usuario
                            obj.nombre,
                            obj.apellido,
                            obj.cargo,
                            obj.area,
                            obj.telefono,
                            obj.fecha_ingreso ?? DateTime.MinValue
                        );
                        lista.Add(emp);
                    }
                }
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al mostrar la lista de empleados", ex);
            }
            return lista;
        }

        // 2. Listar solo empleados activos
        public List<Empleado> ListarEmpleadosActivos()
        {
            List<Empleado> lista = new List<Empleado>();
            try
            {
                List<sp_Empleado_ListarActivosResult> auxLista = EmpleadoCD.ListarActivos();
                if (auxLista != null)
                {
                    foreach (sp_Empleado_ListarActivosResult obj in auxLista)
                    {
                        Empleado emp = new Empleado(
                            obj.id_empleado,
                            0, // sp_Empleado_ListarActivos no retorna id_usuario
                            obj.nombre,
                            obj.apellido,
                            obj.cargo,
                            obj.area,
                            "", // sp_Empleado_ListarActivos no retorna la columna telefono
                            obj.fecha_ingreso ?? DateTime.MinValue
                        );
                        lista.Add(emp);
                    }
                }
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al mostrar los empleados activos", ex);
            }
            return lista;
        }

        // 3. Obtener un empleado por ID
        public Empleado ObtenerPorId(int idEmpleado)
        {
            if (idEmpleado <= 0)
                throw new LogicaExcepciones("El ID del empleado debe ser un número positivo.");

            try
            {
                sp_Empleado_ObtenerPorIdResult obj = EmpleadoCD.ObtenerPorId(idEmpleado);
                if (obj == null) return null;

                return new Empleado(
                    obj.id_empleado,
                    obj.id_usuario, // sp_Empleado_ObtenerPorId sí retorna id_usuario
                    obj.nombre,
                    obj.apellido,
                    obj.cargo,
                    obj.area,
                    obj.telefono,
                    obj.fecha_ingreso ?? DateTime.MinValue
                );
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al obtener el empleado por ID", ex);
            }
        }

        // 4. Crear un nuevo empleado (con su usuario asignado)
        public bool CrearEmpleado(string email, string passwordHash, Empleado empleado)
        {
            if (empleado == null)
                throw new LogicaExcepciones("Los datos del empleado no pueden ser nulos.");

            if (string.IsNullOrWhiteSpace(email))
                throw new LogicaExcepciones("El correo electrónico es obligatorio.");

            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new LogicaExcepciones("La contraseña es obligatoria.");

            if (string.IsNullOrWhiteSpace(empleado.Nombre))
                throw new LogicaExcepciones("El nombre del empleado es obligatorio.");

            if (string.IsNullOrWhiteSpace(empleado.Apellido))
                throw new LogicaExcepciones("El apellido del empleado es obligatorio.");

            try
            {
                EmpleadoCD.Crear(
                    email.Trim(),
                    passwordHash,
                    empleado.Nombre.Trim(),
                    empleado.Apellido.Trim(),
                    string.IsNullOrWhiteSpace(empleado.Cargo) ? null : empleado.Cargo.Trim(),
                    string.IsNullOrWhiteSpace(empleado.Area) ? null : empleado.Area.Trim(),
                    string.IsNullOrWhiteSpace(empleado.Telefono) ? null : empleado.Telefono.Trim(),
                    empleado.FechaIngreso == DateTime.MinValue ? (DateTime?)null : empleado.FechaIngreso
                );
                return true;
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al registrar el nuevo empleado", ex);
            }
        }

        // 5. Actualizar información del empleado (Cargo, Área, Teléfono)
        public bool ActualizarEmpleado(Empleado empleado)
        {
            if (empleado == null)
                throw new LogicaExcepciones("Los datos del empleado no pueden ser nulos.");

            if (empleado.IdEmpleado <= 0)
                throw new LogicaExcepciones("El ID del empleado es inválido.");

            try
            {
                EmpleadoCD.Actualizar(
                    empleado.IdEmpleado,
                    string.IsNullOrWhiteSpace(empleado.Cargo) ? null : empleado.Cargo.Trim(),
                    string.IsNullOrWhiteSpace(empleado.Area) ? null : empleado.Area.Trim(),
                    string.IsNullOrWhiteSpace(empleado.Telefono) ? null : empleado.Telefono.Trim()
                );
                return true;
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al actualizar la información del empleado", ex);
            }
        }

        // 6. Eliminar un empleado (Elimina también su usuario por FK / SP)
        public bool EliminarEmpleado(int idEmpleado)
        {
            if (idEmpleado <= 0)
                throw new LogicaExcepciones("El ID del empleado es inválido.");

            try
            {
                EmpleadoCD.Eliminar(idEmpleado);
                return true;
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al eliminar el empleado de la base de datos", ex);
            }
        }

        // 7. Búsqueda y filtrado de empleados por texto (Nombre, Apellido, Cargo, Área)
        public List<Empleado> BuscarEmpleados(string filtro = "", bool soloActivos = false)
        {
            var todos = soloActivos ? ListarEmpleadosActivos() : ListarEmpleados();

            if (!string.IsNullOrWhiteSpace(filtro))
            {
                string busqueda = filtro.ToLower().Trim();
                todos = todos.Where(e =>
                    (e.Nombre != null && e.Nombre.ToLower().Contains(busqueda)) ||
                    (e.Apellido != null && e.Apellido.ToLower().Contains(busqueda)) ||
                    (e.Cargo != null && e.Cargo.ToLower().Contains(busqueda)) ||
                    (e.Area != null && e.Area.ToLower().Contains(busqueda))
                ).ToList();
            }

            return todos;
        }

        // 8. Obtener datos del empleado buscando por id_usuario
        public Empleado ObtenerPorIdUsuario(int idUsuario)
        {
            if (idUsuario <= 0)
                throw new LogicaExcepciones("El ID de usuario es inválido.");

            try
            {
                List<Empleado> empleados = ListarEmpleados();

                // Recorremos los empleados y consultamos el detalle individual que SÍ trae id_usuario
                foreach (var e in empleados)
                {
                    Empleado empDetalle = ObtenerPorId(e.IdEmpleado);
                    if (empDetalle != null && empDetalle.IdUsuario == idUsuario)
                    {
                        return empDetalle;
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al buscar empleado por ID de usuario", ex);
            }
        }

    }
}
