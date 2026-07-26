using CapaDatos;
using CapadeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reserva = CapaEntidades.Reserva.Reserva;


namespace CapadeLogica
{
    public class ReservaLN
    {
        // 1. Listar todas las reservas (Vista previa general)
        public List<sp_Reserva_ListarResult> ListarReservas()
        {
            try
            {
                return ReservaCD.Listar() ?? new List<sp_Reserva_ListarResult>();
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al listar las reservas", ex);
            }
        }

        // 2. Listar reservas filtradas por estado ('pendiente', 'activa', 'finalizada', etc.)
        public List<sp_Reserva_ListarPorEstadoResult> ListarPorEstado(string estado)
        {
            if (string.IsNullOrWhiteSpace(estado))
                throw new LogicaExcepciones("El estado es obligatorio para realizar el filtro.");

            try
            {
                return ReservaCD.ListarPorEstado(estado.Trim()) ?? new List<sp_Reserva_ListarPorEstadoResult>();
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al listar las reservas por estado", ex);
            }
        }

        // 3. Obtener entidad Reserva mapeada por ID
        public Reserva ObtenerPorId(int idReserva)
        {
            if (idReserva <= 0)
                throw new LogicaExcepciones("El ID de la reserva debe ser un número positivo.");

            try
            {
                sp_Reserva_ObtenerPorIdResult obj = ReservaCD.ObtenerPorId(idReserva);
                if (obj == null) return null;

                return new Reserva(
                    obj.id_reserva,
                    obj.id_cliente,
                    obj.id_habitacion,
                    obj.fecha_entrada,
                    obj.fecha_salida,
                    obj.estado ?? "",
                    obj.fecha_creacion
                );
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al obtener la reserva por ID", ex);
            }
        }

        // 4. Obtener detalle completo para reportes o comprobantes (Cliente, Habitación, Noches, Subtotales)
        public List<sp_Reserva_DetalleResult> ObtenerDetalle(int idReserva)
        {
            if (idReserva <= 0)
                throw new LogicaExcepciones("El ID de la reserva es inválido.");

            try
            {
                return ReservaCD.Detalle(idReserva) ?? new List<sp_Reserva_DetalleResult>();
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al consultar el detalle de la reserva", ex);
            }
        }

        // 5. Crear nueva reserva validando fechas y parámetros de entidad
        public bool CrearReserva(Reserva reserva)
        {
            if (reserva == null)
                throw new LogicaExcepciones("Los datos de la reserva no pueden ser nulos.");

            if (reserva.IdCliente <= 0)
                throw new LogicaExcepciones("Debe especificar un cliente válido.");

            if (reserva.IdHabitacion <= 0)
                throw new LogicaExcepciones("Debe especificar una habitación válida.");

            if (reserva.FechaEntrada == DateTime.MinValue)
                throw new LogicaExcepciones("La fecha de entrada es obligatoria.");

            if (reserva.FechaSalida == DateTime.MinValue)
                throw new LogicaExcepciones("La fecha de salida es obligatoria.");

            if (reserva.FechaSalida <= reserva.FechaEntrada)
                throw new LogicaExcepciones("La fecha de salida debe ser posterior a la fecha de entrada.");

            try
            {
                ReservaCD.Crear(
                    reserva.IdCliente,
                    reserva.IdHabitacion,
                    reserva.FechaEntrada,
                    reserva.FechaSalida
                );
                return true;
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al registrar la nueva reserva", ex);
            }
        }

        // 6. Actualizar rango de fechas de una reserva existente
        public bool ActualizarReserva(int idReserva, DateTime fechaEntrada, DateTime fechaSalida)
        {
            if (idReserva <= 0)
                throw new LogicaExcepciones("El ID de la reserva es inválido.");

            if (fechaEntrada == DateTime.MinValue || fechaSalida == DateTime.MinValue)
                throw new LogicaExcepciones("Las fechas de entrada y salida son obligatorias.");

            if (fechaSalida <= fechaEntrada)
                throw new LogicaExcepciones("La fecha de salida debe ser posterior a la fecha de entrada.");

            try
            {
                ReservaCD.Actualizar(idReserva, fechaEntrada, fechaSalida);
                return true;
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al actualizar la información de la reserva", ex);
            }
        }

        // 7. Cambiar estado de la reserva ('activa', 'finalizada', 'cancelada')
        public bool CambiarEstadoReserva(int idReserva, string estado)
        {
            if (idReserva <= 0)
                throw new LogicaExcepciones("El ID de la reserva es inválido.");

            if (string.IsNullOrWhiteSpace(estado))
                throw new LogicaExcepciones("El estado de la reserva es obligatorio.");

            try
            {
                ReservaCD.CambiarEstado(idReserva, estado.Trim().ToLower());
                return true;
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al cambiar el estado de la reserva", ex);
            }
        }

        // 8. Eliminar reserva en cascada (Borra consumos, facturas y pagos vinculados)
        public bool EliminarReserva(int idReserva)
        {
            if (idReserva <= 0)
                throw new LogicaExcepciones("El ID de la reserva es inválido.");

            try
            {
                ReservaCD.Eliminar(idReserva);
                return true;
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al eliminar la reserva", ex);
            }
        }

    }
}
