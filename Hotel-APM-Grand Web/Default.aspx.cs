using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapadeLogica;
using Habitacion = CapaEntidades.Habitacion.Habitacion;
using ServicioEntidad = CapaEntidades.Servicio.Servicio;
using Usuario = CapadeEntidades.Usuario.Usuario;
using Cliente = CapadeEntidades.Cliente.Cliente;

namespace Presentacion
{
    public partial class _Default : Page
    {
        private readonly ClienteLN clienteLN = new ClienteLN();
        private readonly HabitacionLN habitacionLN = new HabitacionLN();
        private readonly ServicioLN servicioLN = new ServicioLN();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarEstadoSesion();
                CargarHabitacionesDinamicas();
                CargarServiciosDinamicos();
            }
        }

        private void CargarEstadoSesion()
        {
            if (Session["Usuario"] != null)
            {
                Usuario us = (Usuario)Session["Usuario"];
                pnlAutenticado.Visible = true;
                pnlInvitado.Visible = false;

                try
                {
                    Cliente cli = clienteLN.ObtenerPorIdUsuario(us.Id);
                    if (cli != null && !string.IsNullOrWhiteSpace(cli.Nombre))
                    {
                        lblUsuarioSesion.Text = cli.Nombre + " " + cli.Apellido;
                    }
                    else
                    {
                        lblUsuarioSesion.Text = us.Email;
                    }
                }
                catch
                {
                    lblUsuarioSesion.Text = us.Email;
                }
            }
            else
            {
                pnlAutenticado.Visible = false;
                pnlInvitado.Visible = true;
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