using CapaDatos;
using CapadeDatos;
using HotelAPMGrand.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CapadeLogica
{
    public class CancelacionLN
    {
        // 1. Listar cancelaciones (con o sin filtro por rango de fechas)
        public List<Cancelacion> ListarCancelaciones(DateTime? fechaInicio = null, DateTime? fechaFin = null)
        {
            if (fechaInicio.HasValue && fechaFin.HasValue && fechaInicio.Value > fechaFin.Value)
                throw new LogicaExcepciones("La fecha de inicio no puede ser posterior a la fecha de fin.");

            List<Cancelacion> lista = new List<Cancelacion>();
            try
            {
                List<sp_Cancelacion_ListarResult> auxLista = CancelacionCD.Listar(fechaInicio, fechaFin);
                if (auxLista != null)
                {
                    foreach (sp_Cancelacion_ListarResult obj in auxLista)
                    {
                        Cancelacion cancelacion = new Cancelacion(
                            obj.id_cancelacion,
                            0, // El SP de listar hace JOIN y devuelve Cliente y Habitacion en lugar de id_reserva
                            obj.fecha_cancelacion,
                            obj.motivo ?? "",
                            obj.penalizacion,
                            obj.reembolso,
                            obj.solicitado_por ?? ""
                        );
                        lista.Add(cancelacion);
                    }
                }
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al listar las cancelaciones", ex);
            }
            return lista;
        }

        // 2. Obtener cancelación por ID
        public Cancelacion ObtenerPorId(int idCancelacion)
        {
            if (idCancelacion <= 0)
                throw new LogicaExcepciones("El ID de la cancelación debe ser un número positivo.");

            try
            {
                sp_Cancelacion_ObtenerPorIdResult obj = CancelacionCD.ObtenerPorId(idCancelacion);
                if (obj == null) return null;

                return new Cancelacion(
                    obj.id_cancelacion,
                    obj.id_reserva,
                    obj.fecha_cancelacion,
                    obj.motivo ?? "",
                    obj.penalizacion,
                    obj.reembolso,
                    obj.solicitado_por ?? ""
                );
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al obtener la cancelación por ID", ex);
            }
        }

        // 3. Registrar una nueva cancelación
        public bool RegistrarCancelacion(Cancelacion cancelacion)
        {
            if (cancelacion == null)
                throw new LogicaExcepciones("Los datos de la cancelación no pueden ser nulos.");

            if (cancelacion.IdReserva <= 0)
                throw new LogicaExcepciones("El ID de la reserva es obligatorio.");

            if (string.IsNullOrWhiteSpace(cancelacion.Motivo))
                throw new LogicaExcepciones("El motivo de la cancelación es obligatorio.");

            if (cancelacion.Penalizacion < 0)
                throw new LogicaExcepciones("La penalización no puede ser un valor negativo.");

            if (cancelacion.Reembolso < 0)
                throw new LogicaExcepciones("El reembolso no puede ser un valor negativo.");

            if (string.IsNullOrWhiteSpace(cancelacion.SolicitadoPor))
                throw new LogicaExcepciones("Debe especificar quién solicita la cancelación (ej. Cliente o Empleado).");

            try
            {
                CancelacionCD.Registrar(
                    cancelacion.IdReserva,
                    cancelacion.Motivo.Trim(),
                    cancelacion.Penalizacion,
                    cancelacion.Reembolso,
                    cancelacion.SolicitadoPor.Trim()
                );
                return true;
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al registrar la cancelación de la reserva", ex);
            }
        }

        // 4. Actualizar información de una cancelación (motivo, penalización y reembolso)
        public bool ActualizarCancelacion(Cancelacion cancelacion)
        {
            if (cancelacion == null)
                throw new LogicaExcepciones("Los datos de la cancelación no pueden ser nulos.");

            if (cancelacion.IdCancelacion <= 0)
                throw new LogicaExcepciones("El ID de la cancelación es inválido.");

            if (string.IsNullOrWhiteSpace(cancelacion.Motivo))
                throw new LogicaExcepciones("El motivo de la cancelación es obligatorio.");

            if (cancelacion.Penalizacion < 0)
                throw new LogicaExcepciones("La penalización no puede ser un valor negativo.");

            if (cancelacion.Reembolso < 0)
                throw new LogicaExcepciones("El reembolso no puede ser un valor negativo.");

            try
            {
                CancelacionCD.Actualizar(
                    cancelacion.IdCancelacion,
                    cancelacion.Motivo.Trim(),
                    cancelacion.Penalizacion,
                    cancelacion.Reembolso
                );
                return true;
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al actualizar la información de la cancelación", ex);
            }
        }

        // 5. Eliminar registro de cancelación
        public bool EliminarCancelacion(int idCancelacion)
        {
            if (idCancelacion <= 0)
                throw new LogicaExcepciones("El ID de la cancelación es inválido.");

            try
            {
                CancelacionCD.Eliminar(idCancelacion);
                return true;
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al eliminar el registro de cancelación", ex);
            }
        }
    }
}