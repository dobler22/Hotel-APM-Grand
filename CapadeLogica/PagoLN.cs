using CapaDatos;
using CapadeDatos;
using HotelAPMGrand.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using Pago = HotelAPMGrand.Entidades.Pago;

namespace CapadeLogica
{
    public class PagoLN
    {
        // 1. Listar todos los pagos (vista general con datos de cliente)
        public List<Pago> ListarPagos()
        {
            List<Pago> lista = new List<Pago>();
            try
            {
                List<sp_Pago_ListarResult> auxLista = PagoCD.Listar();
                if (auxLista != null)
                {
                    foreach (sp_Pago_ListarResult obj in auxLista)
                    {
                       
                        Pago pago = new Pago(
                            obj.id_pago,
                            0, 
                            obj.monto,
                            obj.metodo_pago ?? "",
                            obj.estado ?? "",
                            obj.fecha_pago,
                            obj.referencia ?? ""
                        );
                        lista.Add(pago);
                    }
                }
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al consultar el listado general de pagos.", ex);
            }
            return lista;
        }

        // 2. Historial de pagos realizados para una reserva en específico
        public List<Pago> HistorialPorReserva(int idReserva)
        {
            if (idReserva <= 0)
                throw new LogicaExcepciones("El ID de la reserva debe ser un número entero positivo.");

            List<Pago> lista = new List<Pago>();
            try
            {
                List<sp_Pago_HistorialPorReservaResult> auxLista = PagoCD.HistorialPorReserva(idReserva);
                if (auxLista != null)
                {
                    foreach (sp_Pago_HistorialPorReservaResult obj in auxLista)
                    {
                        Pago pago = new Pago(
                            obj.id_pago,
                            idReserva,
                            obj.monto,
                            obj.metodo_pago ?? "",
                            obj.estado ?? "",
                            obj.fecha_pago,
                            obj.referencia ?? ""
                        );
                        lista.Add(pago);
                    }
                }
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al consultar el historial de pagos de la reserva.", ex);
            }
            return lista;
        }

        // 3. Obtener el detalle de un pago por ID
        public Pago ObtenerPorId(int idPago)
        {
            if (idPago <= 0)
                throw new LogicaExcepciones("El ID del pago es inválido.");

            try
            {
                sp_Pago_ObtenerPorIdResult obj = PagoCD.ObtenerPorId(idPago);
                if (obj == null) return null;

                return new Pago(
                    obj.id_pago,
                    obj.id_reserva,
                    obj.monto,
                    obj.metodo_pago ?? "",
                    obj.estado ?? "",
                    obj.fecha_pago,
                    obj.referencia ?? ""
                );
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al obtener los detalles del pago por ID.", ex);
            }
        }

        // 4. Registrar un nuevo pago para una reserva
        public bool RegistrarPago(Pago pago)
        {
            if (pago == null)
                throw new LogicaExcepciones("Los datos del pago no pueden ser nulos.");

            if (pago.IdReserva <= 0)
                throw new LogicaExcepciones("Debe especificar un ID de reserva válido.");

            if (pago.Monto <= 0)
                throw new LogicaExcepciones("El monto a pagar debe ser un valor mayor a cero.");

            if (string.IsNullOrWhiteSpace(pago.MetodoPago))
                throw new LogicaExcepciones("Debe especificar un método de pago (ej. Efectivo, Tarjeta, Transferencia).");

            try
            {
                PagoCD.Registrar(
                    pago.IdReserva,
                    pago.Monto,
                    pago.MetodoPago.Trim(),
                    string.IsNullOrWhiteSpace(pago.Referencia) ? null : pago.Referencia.Trim()
                );
                return true;
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al procesar el registro del pago.", ex);
            }
        }

        // 5. Cambiar el estado del pago (ej. 'confirmado', 'anulado', 'rechazado')
        public bool CambiarEstado(int idPago, string nuevoEstado)
        {
            if (idPago <= 0)
                throw new LogicaExcepciones("El ID del pago es inválido.");

            if (string.IsNullOrWhiteSpace(nuevoEstado))
                throw new LogicaExcepciones("El nuevo estado del pago es obligatorio.");

            try
            {
                PagoCD.CambiarEstado(idPago, nuevoEstado.Trim().ToLower());
                return true;
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al cambiar el estado del pago.", ex);
            }
        }

        // 6. Eliminar registro de pago
        public bool EliminarPago(int idPago)
        {
            if (idPago <= 0)
                throw new LogicaExcepciones("El ID del pago es inválido.");

            try
            {
                PagoCD.Eliminar(idPago);
                return true;
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al eliminar el registro de pago.", ex);
            }
        }
    }
}