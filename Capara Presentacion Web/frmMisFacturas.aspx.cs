using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapadeLogica;
using HotelAPMGrand.Entidades;

namespace Capara_Presentacion_Web.Facturacion
{
    public partial class frmMisFacturas : System.Web.UI.Page
    {
        private FacturaLN facturaLN = new FacturaLN();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarFacturas();
            }
        }

        private void CargarFacturas()
        {
            try
            {
                // Carga del listado general desde la capa de lógica
                List<Factura> lista = facturaLN.ListarFacturas();

                gvFacturas.DataSource = lista;
                gvFacturas.DataBind();
            }
            catch (LogicaExcepciones ex)
            {
                MostrarMensaje("Error al consultar sus comprobantes: " + ex.Message, "danger");
            }
            catch (Exception ex)
            {
                MostrarMensaje("Ocurrió un problema inesperado al cargar la información.", "danger");
            }
        }

        protected void btnRefrescar_Click(object sender, EventArgs e)
        {
            CargarFacturas();
            MostrarMensaje("Listado actualizado correctamente.", "info");
        }

        protected void gvFacturas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Formato personalizado para la etiqueta de estado (badge Bootstrap)
                Label lblBadge = (Label)e.Row.FindControl("lblEstadoBadge");
                if (lblBadge != null)
                {
                    string estado = lblBadge.Text.ToLower();
                    switch (estado)
                    {
                        case "pagada":
                        case "pagado":
                            lblBadge.CssClass = "badge bg-success";
                            lblBadge.Text = "<i class='fas fa-check-circle me-1'></i>Pagada";
                            break;
                        case "pendiente":
                            lblBadge.CssClass = "badge bg-warning text-dark";
                            lblBadge.Text = "<i class='fas fa-clock me-1'></i>Pendiente";
                            break;
                        case "anulada":
                        case "anulado":
                            lblBadge.CssClass = "badge bg-danger";
                            lblBadge.Text = "<i class='fas fa-times-circle me-1'></i>Anulada";
                            break;
                        default:
                            lblBadge.CssClass = "badge bg-secondary";
                            break;
                    }
                }
            }
        }

        protected void gvFacturas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "VerDetalle")
            {
                int idFactura = Convert.ToInt32(e.CommandArgument);
                CargarDetalleModal(idFactura);
            }
        }

        private void CargarDetalleModal(int idFactura)
        {
            try
            {
                Factura f = facturaLN.ObtenerPorId(idFactura);
                if (f != null)
                {
                    lblModalIdFactura.Text = "#" + f.IdFactura.ToString("D6");
                    lblModalFecha.Text = f.FechaEmision.ToString("dd/MM/yyyy HH:mm");
                    lblModalEstado.Text = f.Estado;
                    lblModalAlojamiento.Text = "$" + f.MontoAlojamiento.ToString("N2");
                    lblModalServicios.Text = "$" + f.MontoServicios.ToString("N2");
                    lblModalTotal.Text = "$" + f.Total.ToString("N2");

                    // Invocación del modal por JavaScript
                    ScriptManager.RegisterStartupScript(this, GetType(), "PopDetalle", "abrirModalDetalle();", true);
                }
                else
                {
                    MostrarMensaje("No se pudo encontrar el detalle de la factura solicitada.", "warning");
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al consultar el detalle: " + ex.Message, "danger");
            }
        }

        private void MostrarMensaje(string mensaje, string tipo)
        {
            pnlMensaje.Visible = true;
            pnlMensaje.CssClass = $"alert alert-{tipo} alert-dismissible fade show mb-4";
            lblMensaje.Text = mensaje;
        }
    }
}