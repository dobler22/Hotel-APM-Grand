using CapaDatos;
using CapadeDatos;
using CapaEntidades.Servicio;
using System;
using System.Collections.Generic;
using System.Linq;
using ServicioEntidad = CapaEntidades.Servicio.Servicio;

namespace CapadeLogica
{
    public class ServicioLN
    {
        // 1. Listar todos los servicios
        public List<ServicioEntidad> ListarServicios()
        {
            List<ServicioEntidad> lista = new List<ServicioEntidad>();
            try
            {
                List<sp_Servicio_ListarResult> auxLista = ServicioCD.Listar();
                if (auxLista != null)
                {
                    foreach (sp_Servicio_ListarResult obj in auxLista)
                    {
                        ServicioEntidad serv = new ServicioEntidad(
                            obj.id_servicio,
                            obj.nombre ?? "",
                            obj.descripcion ?? "",
                            obj.precio,
                            obj.disponible
                        );
                        lista.Add(serv);
                    }
                }
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al mostrar la lista de servicios", ex);
            }
            return lista;
        }

        // 2. Obtener servicio por ID
        public ServicioEntidad ObtenerPorId(int idServicio)
        {
            if (idServicio <= 0)
                throw new LogicaExcepciones("El ID del servicio debe ser un número positivo.");

            try
            {
                sp_Servicio_ObtenerPorIdResult obj = ServicioCD.ObtenerPorId(idServicio);
                if (obj == null) return null;

                return new ServicioEntidad(
                    obj.id_servicio,
                    obj.nombre ?? "",
                    obj.descripcion ?? "",
                    obj.precio,
                    obj.disponible
                );
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al obtener el servicio por ID", ex);
            }
        }

        // 3. Crear nuevo servicio
        public bool CrearServicio(ServicioEntidad servicio)
        {
            if (servicio == null)
                throw new LogicaExcepciones("Los datos del servicio no pueden ser nulos.");

            if (string.IsNullOrWhiteSpace(servicio.Nombre))
                throw new LogicaExcepciones("El nombre del servicio es obligatorio.");

            if (servicio.Precio <= 0)
                throw new LogicaExcepciones("El precio del servicio debe ser mayor a 0.");

            try
            {
                ServicioCD.Crear(
                    servicio.Nombre.Trim(),
                    string.IsNullOrWhiteSpace(servicio.Descripcion) ? null : servicio.Descripcion.Trim(),
                    servicio.Precio
                );
                return true;
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al registrar el nuevo servicio", ex);
            }
        }

        // 4. Actualizar información de servicio
        public bool ActualizarServicio(ServicioEntidad servicio)
        {
            if (servicio == null)
                throw new LogicaExcepciones("Los datos del servicio no pueden ser nulos.");

            if (servicio.IdServicio <= 0)
                throw new LogicaExcepciones("El ID del servicio es inválido.");

            if (string.IsNullOrWhiteSpace(servicio.Nombre))
                throw new LogicaExcepciones("El nombre del servicio es obligatorio.");

            if (servicio.Precio <= 0)
                throw new LogicaExcepciones("El precio del servicio debe ser mayor a 0.");

            try
            {
                ServicioCD.Actualizar(
                    servicio.IdServicio,
                    servicio.Nombre.Trim(),
                    string.IsNullOrWhiteSpace(servicio.Descripcion) ? null : servicio.Descripcion.Trim(),
                    servicio.Precio
                );
                return true;
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al actualizar la información del servicio", ex);
            }
        }

        // 5. Cambiar disponibilidad del servicio
        public bool CambiarDisponibilidadServicio(int idServicio, bool disponible)
        {
            if (idServicio <= 0)
                throw new LogicaExcepciones("El ID del servicio es inválido.");

            try
            {
                ServicioCD.CambiarDisponibilidad(idServicio, disponible);
                return true;
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al cambiar la disponibilidad del servicio", ex);
            }
        }

        // 6. Eliminar servicio
        public bool EliminarServicio(int idServicio)
        {
            if (idServicio <= 0)
                throw new LogicaExcepciones("El ID del servicio es inválido.");

            try
            {
                ServicioCD.Eliminar(idServicio);
                return true;
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al eliminar el servicio", ex);
            }
        }
    }
}