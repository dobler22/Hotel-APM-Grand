using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapadeLogica;
using Habitacion = CapaEntidades.Habitacion.Habitacion;
using ServicioEntidad = CapaEntidades.Servicio.Servicio;
using Usuario = CapadeEntidades.Usuario.Usuario;
using Cliente = CapadeEntidades.Cliente.Cliente;
using Resena = HotelAPMGrand.Entidades.Resena;

namespace Capara_Presentacion_Web
{
    public partial class _Default : Page
    {
        private readonly ClienteLN clienteLN = new ClienteLN();
        private readonly HabitacionLN habitacionLN = new HabitacionLN();
        private readonly ServicioLN servicioLN = new ServicioLN();
        private readonly ResenaLN resenaLN = new ResenaLN();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarEstadoSesion();
                CargarHabitacionesDinamicas();
                CargarServiciosDinamicos();
                CargarResenasDinamicas();
            }
        }

        private void CargarEstadoSesion()
        {
            bool estaAutenticado = Session["Usuario"] != null;

            // Control seguro de visibilidad para paneles del Header
            var pnlAut = FindControl("pnlAutenticado");
            if (pnlAut != null) pnlAut.Visible = estaAutenticado;

            var pnlInv = FindControl("pnlInvitado");
            if (pnlInv != null) pnlInv.Visible = !estaAutenticado;

            // Control de visibilidad para los paneles de Reseñas en Default.aspx
            if (pnlCrearResena != null) pnlCrearResena.Visible = estaAutenticado;
            if (pnlInvitadoResena != null) pnlInvitadoResena.Visible = !estaAutenticado;

            if (estaAutenticado)
            {
                Usuario us = (Usuario)Session["Usuario"];
                try
                {
                    Cliente cli = clienteLN.ObtenerPorIdUsuario(us.Id);
                    if (cli != null && !string.IsNullOrWhiteSpace(cli.Nombre))
                    {
                        if (lblUsuarioSesion != null)
                            lblUsuarioSesion.Text = cli.Nombre + " " + cli.Apellido;
                    }
                    else
                    {
                        if (lblUsuarioSesion != null)
                            lblUsuarioSesion.Text = us.Email;
                    }
                }
                catch
                {
                    if (lblUsuarioSesion != null)
                        lblUsuarioSesion.Text = us.Email;
                }
            }
        }

        private void CargarHabitacionesDinamicas()
        {
            try
            {
                List<Habitacion> listaHabitaciones = habitacionLN.ListarHabitaciones();
                rptHabitaciones.DataSource = listaHabitaciones;
                rptHabitaciones.DataBind();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al cargar habitaciones: " + ex.Message);
            }
        }

        private void CargarServiciosDinamicos()
        {
            try
            {
                List<ServicioEntidad> listaServicios = servicioLN.ListarServicios();
                List<ServicioEntidad> serviciosDisponibles = listaServicios.FindAll(s => s.Disponible);

                rptServicios.DataSource = serviciosDisponibles;
                rptServicios.DataBind();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al cargar servicios: " + ex.Message);
            }
        }

        private void CargarResenasDinamicas()
        {
            try
            {
                // 1. Cargar Promedio General y Total de opiniones desde BD
                int totalResenas;
                decimal promedio = resenaLN.ObtenerPromedioGeneral(out totalResenas);
                
                lblPromedio.Text = promedio.ToString("0.0");
                lblTotalResenas.Text = totalResenas.ToString();

                // 2. Cargar Lista de Reseñas (Filtro a partir de 1 estrella)
                List<Resena> listaResenas = resenaLN.ListarResenas(1);
                rptResenas.DataSource = listaResenas;
                rptResenas.DataBind();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al cargar reseñas: " + ex.Message);
            }
        }

        protected void btnEnviarResena_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            try
            {
                Usuario us = (Usuario)Session["Usuario"];
                Cliente cli = clienteLN.ObtenerPorIdUsuario(us.Id);

                if (cli == null)
                {
                    MostrarMensajeResena("No se encontró la información de cliente asociada a este usuario.", true);
                    return;
                }

                int idReserva;
                if (!int.TryParse(txtIdReserva.Text.Trim(), out idReserva) || idReserva <= 0)
                {
                    MostrarMensajeResena("Ingresa un código/ID de reserva válido.", true);
                    return;
                }

                Resena nuevaResena = new Resena(
                    0,
                    idReserva,
                    cli.IdCliente, // Obtiene el IdCliente dinámicamente desde el objeto Cliente
                    Convert.ToInt32(ddlCalificacion.SelectedValue),
                    txtComentario.Text.Trim(),
                    DateTime.Now
                );

                if (resenaLN.CrearResena(nuevaResena))
                {
                    MostrarMensajeResena("¡Gracias por publicar tu reseña!", false);
                    txtComentario.Text = string.Empty;
                    txtIdReserva.Text = string.Empty;
                    ddlCalificacion.SelectedIndex = 0;

                    // Actualizar el listado y los promedios en pantalla
                    CargarResenasDinamicas();
                }
            }
            catch (Exception ex)
            {
                MostrarMensajeResena(ex.Message, true);
            }
        }

        private void MostrarMensajeResena(string mensaje, bool esError)
        {
            lblMensajeResena.CssClass = esError 
                ? "d-block text-center mt-2 small fw-bold text-danger" 
                : "d-block text-center mt-2 small fw-bold text-success";
            lblMensajeResena.Text = mensaje;
        }

        // Métodos auxiliares de UI y formateo
        public string FormatearPrecio(object precioObj)
        {
            if (precioObj != null && decimal.TryParse(precioObj.ToString(), out decimal valor))
            {
                return valor.ToString("N2");
            }
            return "0.00";
        }

        public string ObtenerImagenHabitacion(object tipoObj)
        {
            string tipo = tipoObj?.ToString().ToLower() ?? "";

            if (tipo.Contains("suite"))
                return "https://images.unsplash.com/photo-1611892440504-42a792e24d32?auto=format&fit=crop&w=600&q=80";
            if (tipo.Contains("doble"))
                return "https://images.unsplash.com/photo-1590490360182-c33d57733427?auto=format&fit=crop&w=600&q=80";
            if (tipo.Contains("familiar"))
                return "https://images.unsplash.com/photo-1582719478250-c89cae4dc85b?auto=format&fit=crop&w=600&q=80";

            return "https://images.unsplash.com/photo-1566665797739-1674de7a421a?auto=format&fit=crop&w=600&q=80";
        }

        public string ObtenerIconoServicio(object nombreObj)
        {
            string nombre = nombreObj?.ToString().ToLower() ?? "";

            if (nombre.Contains("desayuno") || nombre.Contains("buffet")) return "☕";
            if (nombre.Contains("lavandería") || nombre.Contains("lavanderia")) return "🧺";
            if (nombre.Contains("room") || nombre.Contains("comida")) return "🍽️";
            if (nombre.Contains("transporte") || nombre.Contains("traslado")) return "🚗";
            if (nombre.Contains("spa") || nombre.Contains("relax")) return "💆‍♂️";
            if (nombre.Contains("piscina")) return "🏊‍♂️";
            if (nombre.Contains("gym") || nombre.Contains("gimnasio")) return "🏋️‍♂️";
            if (nombre.Contains("wifi") || nombre.Contains("internet")) return "📶";

            return "✨";
        }

        public string GenerarEstrellas(int calificacion)
        {
            string estrellas = "";
            for (int i = 0; i < calificacion; i++)
            {
                estrellas += "<i class=\"fas fa-star\"></i> ";
            }
            return estrellas;
        }

        protected void btnSolicitarReserva_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string idHabitacion = btn.CommandArgument;

            if (Session["Usuario"] == null)
            {
                Response.Redirect("Login.aspx?next=frmReservas.aspx?id=" + idHabitacion);
            }
            else
            {
                Response.Redirect("frmReservas.aspx?id=" + idHabitacion);
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("Default.aspx");
        }
    }
}