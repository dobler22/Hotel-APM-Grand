using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapadeLogica;
using HabitacionEntidad = CapaEntidades.Habitacion.Habitacion;
using UsuarioEntidad = CapadeEntidades.Usuario.Usuario;
using ClienteEntidad = CapadeEntidades.Cliente.Cliente;

namespace Capara_Presentacion_Web
{
    public partial class frmReservas : Page
    {
        private readonly HabitacionLN habitacionLN = new HabitacionLN();
        private readonly ReservaLN reservaLN = new ReservaLN();
        private readonly ClienteLN clienteLN = new ClienteLN();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 1. Validar Sesión de Usuario
                if (Session["Usuario"] == null)
                {
                    pnlNoAutenticado.Visible = true;
                    pnlFormularioReserva.Visible = false;
                    return;
                }

                pnlNoAutenticado.Visible = false;
                pnlFormularioReserva.Visible = true;

                // 2. Obtener el ID de la Habitación desde la QueryString (?id=...)
                if (Request.QueryString["id"] != null && int.TryParse(Request.QueryString["id"], out int idHabitacion))
                {
                    ViewState["IdHabitacion"] = idHabitacion;
                    CargarDetalleHabitacion(idHabitacion);
                }
                else
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }

        private void CargarDetalleHabitacion(int idHabitacion)
        {
            try
            {
                HabitacionEntidad hab = habitacionLN.ObtenerPorId(idHabitacion);
                if (hab != null)
                {
                    lblTipoHabitacion.Text = hab.Tipo;
                    lblNumero.Text = hab.Numero;
                    lblPiso.Text = hab.Piso.ToString();
                    lblCapacidad.Text = hab.Capacidad.ToString();
                    lblPrecio.Text = hab.PrecioPorNoche.ToString("N2");

                    ViewState["PrecioPorNoche"] = hab.PrecioPorNoche;

                    string tipoLower = hab.Tipo != null ? hab.Tipo.ToLower() : "";
                    if (tipoLower.Contains("suite"))
                        imgHabitacion.ImageUrl = "https://images.unsplash.com/photo-1611892440504-42a792e24d32?auto=format&fit=crop&w=600&q=80";
                    else if (tipoLower.Contains("doble"))
                        imgHabitacion.ImageUrl = "https://images.unsplash.com/photo-1590490360182-c33d57733427?auto=format&fit=crop&w=600&q=80";
                    else if (tipoLower.Contains("familiar"))
                        imgHabitacion.ImageUrl = "https://images.unsplash.com/photo-1582719478250-c89cae4dc85b?auto=format&fit=crop&w=600&q=80";
                    else
                        imgHabitacion.ImageUrl = "https://images.unsplash.com/photo-1566665797739-1674de7a421a?auto=format&fit=crop&w=600&q=80";
                }
            }
            catch (Exception ex)
            {
                MostrarMensajeError("Error al cargar datos de la habitación: " + ex.Message);
            }
        }

        // ==========================================
        // DIBUJO DEL CALENDARIO Y DISPONIBILIDAD
        // ==========================================
        protected void calDisponibilidad_DayRender(object sender, DayRenderEventArgs e)
        {
            // Bloquear días pasados a la fecha actual
            if (e.Day.Date < DateTime.Today)
            {
                e.Day.IsSelectable = false;
                e.Cell.BackColor = System.Drawing.Color.FromArgb(235, 238, 242);
                e.Cell.ForeColor = System.Drawing.Color.LightGray;
                return;
            }

            // Resaltar el rango seleccionado en las cajas de texto
            if (DateTime.TryParse(txtFechaEntrada.Text, out DateTime entrada) &&
                DateTime.TryParse(txtFechaSalida.Text, out DateTime salida))
            {
                if (e.Day.Date >= entrada && e.Day.Date < salida)
                {
                    e.Cell.BackColor = System.Drawing.Color.FromArgb(255, 193, 7);
                    e.Cell.ForeColor = System.Drawing.Color.Black;
                    e.Cell.Font.Bold = true;
                    return;
                }
            }

            // Validar si el día está libre u ocupado
            if (ViewState["IdHabitacion"] != null)
            {
                int idHabitacion = (int)ViewState["IdHabitacion"];
                bool disponible = EsDiaDisponible(idHabitacion, e.Day.Date);

                if (!disponible)
                {
                    e.Day.IsSelectable = false;
                    e.Cell.BackColor = System.Drawing.Color.FromArgb(220, 53, 69); // Rojo
                    e.Cell.ForeColor = System.Drawing.Color.White;
                    e.Cell.ToolTip = "Fecha ocupada o reservada";
                }
                else
                {
                    e.Cell.BackColor = System.Drawing.Color.FromArgb(212, 237, 218); // Verde suave
                    e.Cell.ForeColor = System.Drawing.Color.FromArgb(21, 87, 36);
                }
            }
        }

        private bool EsDiaDisponible(int idHabitacion, DateTime fecha)
        {
            try
            {
                DateTime? fEntrada = fecha;
                DateTime? fSalida = fecha.AddDays(1);

                var listaDisponibles = habitacionLN.ListarHabitacionesDisponibles(fEntrada, fSalida, "");

                return listaDisponibles.Exists(h => h.IdHabitacion == idHabitacion);
            }
            catch
            {
                return true;
            }
        }

        // ==========================================
        // CAMBIO EN CAJAS DE FECHA Y CÁLCULO
        // ==========================================
        protected void txtFechas_TextChanged(object sender, EventArgs e)
        {
            OcultarMensajes();

            if (DateTime.TryParse(txtFechaEntrada.Text, out DateTime entrada) &&
                DateTime.TryParse(txtFechaSalida.Text, out DateTime salida))
            {
                if (entrada < DateTime.Today)
                {
                    MostrarMensajeError("La fecha de entrada no puede ser anterior a hoy.");
                    ResetearTotales();
                    return;
                }

                if (salida <= entrada)
                {
                    MostrarMensajeError("La fecha de salida debe ser posterior a la fecha de entrada.");
                    ResetearTotales();
                    return;
                }

                int idHabitacion = (int)ViewState["IdHabitacion"];

                DateTime? fEntrada = entrada;
                DateTime? fSalida = salida;
                var disponibles = habitacionLN.ListarHabitacionesDisponibles(fEntrada, fSalida, "");

                bool libre = disponibles.Exists(h => h.IdHabitacion == idHabitacion);

                if (!libre)
                {
                    MostrarMensajeError("La habitación no está disponible para todo el período seleccionado.");
                    ResetearTotales();
                    return;
                }

                // Cálculo visual del monto total estimado
                int totalNoches = (salida - entrada).Days;
                decimal precioNoche = Convert.ToDecimal(ViewState["PrecioPorNoche"]);
                decimal totalPagar = totalNoches * precioNoche;

                lblTotalEstimado.Text = totalPagar.ToString("N2");
                btnConfirmarReserva.Enabled = true;
            }
            else
            {
                ResetearTotales();
            }
        }

        // ==========================================
        // CONFIRMACIÓN DE LA RESERVA EN LA BD
        // ==========================================
        protected void btnConfirmarReserva_Click(object sender, EventArgs e)
        {
            try
            {
                UsuarioEntidad usuario = (UsuarioEntidad)Session["Usuario"];
                ClienteEntidad cliente = clienteLN.ObtenerPorIdUsuario(usuario.Id);

                if (cliente == null)
                {
                    MostrarMensajeError("No se encontró un perfil de cliente activo vinculado a tu usuario.");
                    return;
                }

                int idHabitacion = (int)ViewState["IdHabitacion"];
                DateTime entrada = DateTime.Parse(txtFechaEntrada.Text);
                DateTime salida = DateTime.Parse(txtFechaSalida.Text);

                // 1. Instanciamos la entidad Reserva
                CapaEntidades.Reserva.Reserva nuevaReserva = new CapaEntidades.Reserva.Reserva
                {
                    IdCliente = cliente.IdCliente,
                    IdHabitacion = idHabitacion,
                    FechaEntrada = entrada,
                    FechaSalida = salida
                };

                // 2. Llamamos al método de la capa de lógica
                reservaLN.CrearReserva(nuevaReserva);

                MostrarMensajeExito("¡Reserva creada exitosamente! Estado asignado: PENDIENTE.");
                btnConfirmarReserva.Enabled = false;
            }
            catch (Exception ex)
            {
                MostrarMensajeError("Error al guardar la reserva: " + ex.Message);
            }
        }

        private void ResetearTotales()
        {
            lblTotalEstimado.Text = "0.00";
            btnConfirmarReserva.Enabled = false;
        }

        private void MostrarMensajeError(string msg)
        {
            lblMensajeError.Text = msg;
            lblMensajeError.Visible = true;
            lblMensajeExito.Visible = false;
        }

        private void MostrarMensajeExito(string msg)
        {
            lblMensajeExito.Text = msg;
            lblMensajeExito.Visible = true;
            lblMensajeError.Visible = false;
        }

        private void OcultarMensajes()
        {
            lblMensajeError.Visible = false;
            lblMensajeExito.Visible = false;
        }
    }
}