using System;
using System.Web.UI;

namespace Presentacion
{
    public partial class Contacto : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pnlMensaje.Visible = false;
            }
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();
            string email = txtEmail.Text.Trim();
            string asunto = txtAsunto.Text.Trim();

            // Aquí puedes conectar a tu BLL si deseas guardar los mensajes de contacto en la BD.

            pnlMensaje.Visible = true;
            lblMensaje.Text = $"Gracias <strong>{nombre}</strong>, hemos recibido tu mensaje sobre '<em>{asunto}</em>'. Te responderemos al correo <strong>{email}</strong> a la brevedad posible.";

            // Limpiar los campos del formulario
            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            txtNombre.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtAsunto.Text = string.Empty;
            txtMensaje.Text = string.Empty;
        }
    }
}