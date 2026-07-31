using System;
using System.Web.UI;
using CapadeLogica;
using Usuario = CapadeEntidades.Usuario.Usuario;

namespace Capara_Presentacion_Web
{
    public partial class Login : Page
    {
        private readonly UsuarioLN usuarioLN = new UsuarioLN();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Si la sesión ya existe y es válida, redirige a la página principal
                if (Session["Usuario"] != null)
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

            pnlError.Visible = false;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MostrarMensajeError("Por favor, ingresa el correo y la contraseña.");
                return;
            }

            try
            {
                // 1. Invoca el método Login de la Capa de Lógica
                Usuario usuario = usuarioLN.Login(email, password);

                if (usuario != null)
                {
                    // 2. Verificar que el usuario esté activo
                    if (!usuario.Activo)
                    {
                        MostrarMensajeError("Su cuenta se encuentra inactiva. Contacte a soporte.");
                        return;
                    }

                    // 3. Validar restricción para permitir únicamente el acceso a Clientes
                    if (!usuario.Rol.Equals("Cliente", StringComparison.OrdinalIgnoreCase))
                    {
                        MostrarMensajeError("Acceso denegado: Este portal está destinado exclusivamente a clientes.");
                        return;
                    }

                    // 4. Guardar datos del cliente en la sesión del servidor
                    Session["Usuario"] = usuario;
                    Session["Rol"] = usuario.Rol;
                    Session["UsuarioId"] = usuario.Id;

                    // 5. Redireccionar al portal principal
                    Response.Redirect("Default.aspx", false);
                }
                else
                {
                    MostrarMensajeError("Credenciales incorrectas. Verifique su correo y contraseña.");
                }
            }
            catch (LogicaExcepciones ex)
            {
                MostrarMensajeError(ex.Message);
            }
            catch (Exception)
            {
                MostrarMensajeError("Ocurrió un error inesperado al procesar su solicitud.");
            }
        }

        private void MostrarMensajeError(string mensaje)
        {
            pnlError.Visible = true;
            lblMensajeError.Text = mensaje;
        }
    }
}