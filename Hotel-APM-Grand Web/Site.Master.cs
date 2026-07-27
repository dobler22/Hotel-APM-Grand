using System;
using System.Web.UI;
using UsuarioEntidad = CapadeEntidades.Usuario.Usuario;

namespace Presentacion
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ActualizarEstadoNavegacion();
            }
        }

        private void ActualizarEstadoNavegacion()
        {
            if (Session["Usuario"] != null)
            {
                UsuarioEntidad usuario = (UsuarioEntidad)Session["Usuario"];

                pnlAnonimo.Visible = false;
                pnlAutenticado.Visible = true;
                liMisReservas.Visible = true;

                // Usa la propiedad que tenga tu entidad Usuario (Email, Nombre, etc.)
                lblNombreUsuario.Text = usuario.Email;
            }
            else
            {
                pnlAnonimo.Visible = true;
                pnlAutenticado.Visible = false;
                liMisReservas.Visible = false;
            }
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Login.aspx");
        }
    }
}