using CapaDatos;
using CapadeDatos;
using CapaEntidades.ServicioReserva;
using System;
using System.Collections.Generic;
using System.Linq;
using ServicioReservaEntidad = CapaEntidades.ServicioReserva.ServicioReserva;

namespace CapadeLogica
{
    public class ServicioReservaLN
    {
        // 1. Listar servicios asociados a una reserva específica
        public List<ServicioReservaEntidad> ListarServiciosPorReserva(int idReserva)
        {
            if (idReserva <= 0)
                throw new LogicaExcepciones("El ID de la reserva debe ser un número positivo.");

            List<ServicioReservaEntidad> lista = new List<ServicioReservaEntidad>();
            try
            {
                List<sp_ServicioReserva_ListarResult> auxLista = ServicioReservaCD.Listar(idReserva);
                if (auxLista != null)
                {
                    foreach (sp_ServicioReserva_ListarResult obj in auxLista)
                    {
                        ServicioReservaEntidad sr = new ServicioReservaEntidad(
                            obj.id_serv_reserva,
                            idReserva,
                            0, // El SP de Listar devuelve el Nombre del servicio en lugar del id_servicio
                            obj.cantidad,
                            obj.subtotal,
                            obj.fecha_solicitud
                        );
                        lista.Add(sr);
                    }
                }
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al listar los servicios de la reserva", ex);
            }
            return lista;
        }

        // 2. Obtener un ServicioReserva por ID
        public ServicioReservaEntidad ObtenerPorId(int idServReserva)
        {
            if (idServReserva <= 0)
                throw new LogicaExcepciones("El ID del registro de servicio-reserva debe ser un número positivo.");

            try
            {
                sp_ServicioReserva_ObtenerPorIdResult obj = ServicioReservaCD.ObtenerPorId(idServReserva);
                if (obj == null) return null;

                return new ServicioReservaEntidad(
                    obj.id_serv_reserva,
                    obj.id_reserva,
                    obj.id_servicio,
                    obj.cantidad,
                    obj.subtotal,
                    obj.fecha_solicitud
                );
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al obtener el detalle del servicio contratado", ex);
            }
        }

        // 3. Agregar un servicio a una reserva
        public bool AgregarServicioAReserva(ServicioReservaEntidad servicioReserva)
        {
            if (servicioReserva == null)
                throw new LogicaExcepciones("Los datos de la reserva de servicio no pueden ser nulos.");

            if (servicioReserva.IdReserva <= 0)
                throw new LogicaExcepciones("El ID de la reserva es obligatorio y debe ser un número positivo.");

            if (servicioReserva.IdServicio <= 0)
                throw new LogicaExcepciones("El ID del servicio es obligatorio y debe ser un número positivo.");

            if (servicioReserva.Cantidad <= 0)
                throw new LogicaExcepciones("La cantidad debe ser mayor a 0.");

            try
            {
                ServicioReservaCD.Agregar(
                    servicioReserva.IdReserva,
                    servicioReserva.IdServicio,
                    servicioReserva.Cantidad
                );
                return true;
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al agregar el servicio a la reserva", ex);
            }
        }

        // 4. Actualizar la cantidad de un servicio contratado
        public bool ActualizarCantidadServicio(int idServReserva, int cantidad)
        {
            if (idServReserva <= 0)
                throw new LogicaExcepciones("El ID del detalle del servicio-reserva es inválido.");

            if (cantidad <= 0)
                throw new LogicaExcepciones("La cantidad contratada debe ser mayor a 0.");

            try
            {
                ServicioReservaCD.Actualizar(idServReserva, cantidad);
                return true;
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al actualizar la cantidad del servicio contratado", ex);
            }
        }

        // 5. Quitar un servicio de una reserva
        public bool QuitarServicioDeReserva(int idServReserva)
        {
            if (idServReserva <= 0)
                throw new LogicaExcepciones("El ID del detalle del servicio-reserva es inválido.");

            try
            {
                ServicioReservaCD.Quitar(idServReserva);
                return true;
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al remover el servicio de la reserva", ex);
            }
        }
    }
}