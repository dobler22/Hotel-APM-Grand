using System;
using System.Web.UI;
using CapadeLogica;
using Cliente = CapadeEntidades.Cliente.Cliente;
using Usuario = CapadeEntidades.Usuario.Usuario;

namespace Presentacion
{
    public partial class Registro : Page
    {
        private readonly ClienteLN clienteLN = new ClienteLN();
        private readonly UsuarioLN usuarioLN = new UsuarioLN();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Si el usuario ya está autenticado, redirigir al inicio
                if (Session["Usuario"] != null)
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            pnlError.Visible = false;

            string documento = txtCedula.Text.Trim();
            string nombre = txtNombre.Text.Trim();
            string apellido = txtApellido.Text.Trim();
            string telefono = txtTelefono.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

            try
            {
                // 1. Instanciar el objeto Cliente con sus datos básicos
                Cliente nuevoCliente = new Cliente
                {
                    Nombre = nombre,
                    Apellido = apellido,
                    Telefono = telefono,
                    DocumentoIdentidad = documento,
                    Nacionalidad = "Ecuatoriana", // Valor por defecto o el asignado
                    FechaNacimiento = DateTime.MinValue
                };

                // 2. Invocar el método CrearCliente de ClienteLN
                // Este método ejecuta el Stored Procedure sp_Cliente_Crear que guarda en Usuarios y Clientes
                bool registroExitoso = clienteLN.CrearCliente(email, password, nuevoCliente);

                if (registroExitoso)
                {
                    // 3. Realizar el Login automático invocando tu UsuarioLN
                    Usuario usuarioSesion = usuarioLN.Login(email, password);

                    if (usuarioSesion != null)
                    {
                        // Validar que la cuenta esté activa (propiedad de tu entidad Usuario)
                        if (!usuarioSesion.Activo)
                        {
                            MostrarMensajeError("Su cuenta se creó pero se encuentra inactiva. Contacte a soporte.");
                            return;
                        }

                        // Guardar la sesión con el formato exacto de tu proyecto
                        Session["Usuario"] = usuarioSesion;
                        Session["Rol"] = usuarioSesion.Rol;
                        Session["UsuarioId"] = usuarioSesion.Id;

                        // Redirigir al portal principal
                        Response.Redirect("Default.aspx");
                    }
                    else
                    {
                        // Si falla el auto-login, redirige al formulario de Login
                        Response.Redirect("Login.aspx");
                    }
                }
                else
                {
                    MostrarMensajeError("No se pudo registrar la cuenta. Inténtelo de nuevo.");
                }
            }
            catch (LogicaExcepciones ex)
            {
                MostrarMensajeError(ex.Message);
            }
            catch (Exception ex)
            {
                MostrarMensajeError("Ocurrió un error al registrar la cuenta: " + ex.Message);
            }
        }

        private void MostrarMensajeError(string mensaje)
        {
            pnlError.Visible = true;
            lblMensajeError.Text = mensaje;
        }
    }
}