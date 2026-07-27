using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapadeLogica;
using HotelAPMGrand.Entidades;
using ClienteEntidad = CapadeEntidades.Cliente.Cliente;
using UsuarioEntidad = CapadeEntidades.Usuario.Usuario;

namespace Presentacion
{
    public partial class frmHistorialCliente : Page
    {
        private readonly ClienteLN clienteLN = new ClienteLN();
        private readonly CancelacionLN cancelacionLN = new CancelacionLN();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarHistorialCliente();
            }
        }

        private void CargarHistorialCliente()
        {
            if (Session["Usuario"] == null && Session["UsuarioId"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            try
            {
                int idUsuario = 0;
                if (Session["Usuario"] != null)
                {
                    UsuarioEntidad usuario = (UsuarioEntidad)Session["Usuario"];
                    idUsuario = usuario.Id;
                }
                else if (Session["UsuarioId"] != null)
                {
                    idUsuario = Convert.ToInt32(Session["UsuarioId"]);
                }

                ClienteEntidad cliente = clienteLN.ObtenerPorIdUsuario(idUsuario);

                if (cliente != null && cliente.IdCliente > 0)
                {
                    var historial = clienteLN.ObtenerHistorial(cliente.IdCliente);
                    gvHistorial.DataSource = historial;
                    gvHistorial.DataBind();

                    if (historial == null || historial.Count == 0)
                    {
                        MostrarMensaje("Aún no registras reservaciones en tu historial.");
                    }
                }
                else
                {
                    MostrarMensaje("No se encontró la ficha del cliente asociada al usuario.");
                }
            }
            catch (LogicaExcepciones ex)
            {
                MostrarMensaje("Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                MostrarMensaje("Ocurrió un error al cargar su historial: " + ex.Message);
            }
        }

        protected void gvHistorial_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblEstado = (Label)e.Row.FindControl("lblEstado");
                LinkButton btnAbrirCancelar = (LinkButton)e.Row.FindControl("btnAbrirCancelar");

                if (lblEstado != null)
                {
                    string estado = lblEstado.Text.ToLower().Trim();

                    switch (estado)
                    {
                        case "confirmada":
                            lblEstado.CssClass = "badge bg-info text-dark px-3 py-2 rounded-pill";
                            lblEstado.Text = "<i class='fas fa-clock me-1'></i>Confirmada";
                            if (btnAbrirCancelar != null) btnAbrirCancelar.Visible = true;
                            break;
                        case "activa":
                            lblEstado.CssClass = "badge bg-success px-3 py-2 rounded-pill";
                            lblEstado.Text = "<i class='fas fa-bed me-1'></i>En Estancia";
                            if (btnAbrirCancelar != null) btnAbrirCancelar.Visible = true;
                            break;
                        case "completada":
                        case "finalizada":
                            lblEstado.CssClass = "badge bg-secondary px-3 py-2 rounded-pill";
                            lblEstado.Text = "<i class='fas fa-check-circle me-1'></i>Completada";
                            if (btnAbrirCancelar != null) btnAbrirCancelar.Visible = false; // Ocultar botón
                            break;
                        case "cancelada":
                            lblEstado.CssClass = "badge bg-danger px-3 py-2 rounded-pill";
                            lblEstado.Text = "<i class='fas fa-times-circle me-1'></i>Cancelada";
                            if (btnAbrirCancelar != null) btnAbrirCancelar.Visible = false; // Ocultar botón
                            break;
                        default:
                            lblEstado.CssClass = "badge bg-light text-dark border px-3 py-2 rounded-pill";
                            if (btnAbrirCancelar != null) btnAbrirCancelar.Visible = false;
                            break;
                    }
                }
            }
        }

        // Manejar el clic del botón Cancelar en la fila del GridView
        protected void gvHistorial_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "SolicitarCancelacion")
            {
                string idReserva = e.CommandArgument.ToString();
                hfIdReservaCancelar.Value = idReserva;
                lblIdReservaModal.Text = idReserva;
                txtMotivoCancelacion.Text = string.Empty;

                // Invocar script para mostrar el modal de Bootstrap
                ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "AbrirModalCancelacion();", true);
            }
        }

        // Confirmación enviada desde el modal
        protected void btnConfirmarCancelacion_Click(object sender, EventArgs e)
        {
            pnlMensaje.Visible = false;

            int idReserva = Convert.ToInt32(hfIdReservaCancelar.Value);
            string motivo = txtMotivoCancelacion.Text.Trim();

            if (string.IsNullOrWhiteSpace(motivo))
            {
                MostrarMensaje("Debe ingresar un motivo para poder cancelar la reservación.");
                return;
            }

            try
            {
                // Instanciamos el DTO / Entidad Cancelacion
                Cancelacion nuevaCancelacion = new Cancelacion
                {
                    IdReserva = idReserva,
                    Motivo = motivo,
                    Penalizacion = 0m,     // 0 o el monto que determine tu regla
                    Reembolso = 0m,        // 0 o el valor sujeto a reembolso
                    SolicitadoPor = "Cliente"
                };

                // Invocamos el método de tu clase CancelacionLN
                bool resultado = cancelacionLN.RegistrarCancelacion(nuevaCancelacion);

                if (resultado)
                {
                    MostrarMensaje("La reservación #" + idReserva + " ha sido cancelada exitosamente.");
                    CargarHistorialCliente(); // Recargar el GridView para ver el estado actualizado
                }
                else
                {
                    MostrarMensaje("No se pudo procesar la cancelación. Inténtalo de nuevo.");
                }
            }
            catch (LogicaExcepciones ex)
            {
                MostrarMensaje(ex.Message);
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cancelar la reserva: " + ex.Message);
            }
        }

        private void MostrarMensaje(string mensaje)
        {
            pnlMensaje.Visible = true;
            lblMensaje.Text = mensaje;
        }
    }
}