using CapaDatos;
using CapadeDatos;
using HotelAPMGrand.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapadeLogica
{
    public class FacturaLN
    {
        // 1. Listar todas las facturas
        public List<Factura> ListarFacturas()
        {
            List<Factura> lista = new List<Factura>();
            try
            {
                List<sp_Factura_ListarResult> auxLista = FacturaCD.Listar();
                if (auxLista != null)
                {
                    foreach (sp_Factura_ListarResult obj in auxLista)
                    {
                        Factura factura = new Factura(
                            obj.id_factura,
                            0, // sp_Factura_Listar retorna el nombre del Cliente en lugar del id_reserva
                            obj.monto_alojamiento,
                            obj.monto_servicios,
                            obj.total ??  0,
                            obj.fecha_emision,
                            obj.estado ?? ""
                        );
                        lista.Add(factura);
                    }
                }
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al consultar el listado general de facturas.", ex);
            }
            return lista;
        }

        // 2. Listar facturas con estado 'pendiente'
        public List<Factura> ListarPendientes()
        {
            List<Factura> lista = new List<Factura>();
            try
            {
                List<sp_Factura_ListarPendientesResult> auxLista = FacturaCD.ListarPendientes();
                if (auxLista != null)
                {
                    foreach (sp_Factura_ListarPendientesResult obj in auxLista)
                    {
                        Factura factura = new Factura(
                            obj.id_factura,
                            0, // sp_Factura_ListarPendientes retorna el nombre del Cliente en lugar del id_reserva
                            obj.monto_alojamiento,
                            obj.monto_servicios,
                            obj.total ?? 0,
                            obj.fecha_emision,
                            "pendiente"
                        );
                        lista.Add(factura);
                    }
                }
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al consultar la lista de facturas pendientes.", ex);
            }
            return lista;
        }

        // 3. Obtener el detalle de una factura por ID
        public Factura ObtenerPorId(int idFactura)
        {
            if (idFactura <= 0)
                throw new LogicaExcepciones("El ID de la factura es inválido.");

            try
            {
                sp_Factura_ObtenerPorIdResult obj = FacturaCD.ObtenerPorId(idFactura);
                if (obj == null) return null;

                return new Factura(
                    obj.id_factura,
                    obj.id_reserva,
                    obj.monto_alojamiento,
                    obj.monto_servicios,
                    obj.total ?? 0,
                    obj.fecha_emision,
                    obj.estado ?? ""
                );
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al obtener los detalles de la factura por ID.", ex);
            }
        }

        // 4. Generar o regenerar una factura para una reserva
        public bool GenerarFactura(int idReserva)
        {
            if (idReserva <= 0)
                throw new LogicaExcepciones("Debe especificar un ID de reserva válido para generar la factura.");

            try
            {
                FacturaCD.Generar(idReserva);
                return true;
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al procesar la generación de la factura.", ex);
            }
        }

        // 5. Marcar una factura como pagada
        public bool MarcarPagada(int idFactura)
        {
            if (idFactura <= 0)
                throw new LogicaExcepciones("El ID de la factura es inválido.");

            try
            {
                FacturaCD.MarcarPagada(idFactura);
                return true;
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al cambiar el estado de la factura a pagada.", ex);
            }
        }

        // 6. Anular factura
        public bool AnularFactura(int idFactura)
        {
            if (idFactura <= 0)
                throw new LogicaExcepciones("El ID de la factura es inválido.");

            try
            {
                FacturaCD.Anular(idFactura);
                return true;
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al anular la factura.", ex);
            }
        }

        // 7. Eliminar registro de factura
        public bool EliminarFactura(int idFactura)
        {
            if (idFactura <= 0)
                throw new LogicaExcepciones("El ID de la factura es inválido.");

            try
            {
                FacturaCD.Eliminar(idFactura);
                return true;
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al eliminar el registro de la factura.", ex);
            }
        }
    }
}
