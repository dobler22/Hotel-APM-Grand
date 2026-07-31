using System;
using System.Web.UI;
using Usuario = CapadeEntidades.Usuario.Usuario;

namespace Capara_Presentacion_Web
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                VerificarEstadoSesion();
            }
        }

        private void VerificarEstadoSesion()
        {
            bool estaAutenticado = Session["Usuario"] != null;

            // Paneles de usuario a la derecha
            pnlAnonimo.Visible = !estaAutenticado;
            pnlAutenticado.Visible = estaAutenticado;

            // Opción "Reservar": Se oculta si YA inició sesión
            liReservar.Visible = !estaAutenticado;

            // Opciones privadas: Se MUESTRAN si inició sesión
            liMisReservas.Visible = estaAutenticado;
            liMisFacturas.Visible = estaAutenticado;

            if (estaAutenticado)
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                lblNombreUsuario.Text = !string.IsNullOrEmpty(usuario.Email) ? usuario.Email : "Cliente";
            }
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Default.aspx");
        }
    }
}