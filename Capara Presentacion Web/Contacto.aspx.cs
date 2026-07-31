using System;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace Capara_Presentacion_Web
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
            string nombreCliente = txtNombre.Text.Trim();
            string emailCliente = txtEmail.Text.Trim();
            string asuntoCliente = txtAsunto.Text.Trim();
            string mensajeCliente = txtMensaje.Text.Trim();

            // 1. Validar sintaxis y existencia del dominio del correo ingresado
            if (!EsCorreoValido(emailCliente))
            {
                pnlMensaje.CssClass = "alert alert-warning alert-dismissible fade show mb-4";
                lblMensaje.Text = "Por favor, ingresa una dirección de correo electrónico válida y existente.";
                pnlMensaje.Visible = true;
                return;
            }

            // Configuración de credenciales de envío
            string correoDespachador = "clientecuentahotelapmgrand@gmail.com";
            string claveDespachador = "uitl jglk ahyx gilh"; // Clave de aplicación asignada

            // Correo receptor de los desarrolladores
            string correoDestino = "hotelapmgrand.dev@gmail.com";

            try
            {
                MailMessage correo = new MailMessage();
                // Remitente del sistema
                correo.From = new MailAddress(correoDespachador, "Sistema Web Hotel APM Grand");

                // Destinatario principal (Desarrolladores)
                correo.To.Add(correoDestino);

                // Configurar para que al responder llegue directamente al cliente
                correo.ReplyToList.Add(new MailAddress(emailCliente, nombreCliente));

                correo.Subject = $"[Mensaje Web / Queja] {asuntoCliente}";
                correo.Body = $@"
                    <div style='font-family: Arial, sans-serif; color: #333;'>
                        <h2 style='color: #f59e0b;'>Nuevo mensaje recibido desde el Portal Web</h2>
                        <p><strong>Nombre del Cliente:</strong> {nombreCliente}</p>
                        <p><strong>Correo de Contacto:</strong> {emailCliente}</p>
                        <p><strong>Asunto:</strong> {asuntoCliente}</p>
                        <hr style='border: 1px solid #eee;' />
                        <p><strong>Mensaje / Consulta:</strong></p>
                        <p style='background-color: #f8f9fa; padding: 15px; border-left: 4px solid #f59e0b;'>{mensajeCliente.Replace("\n", "<br />")}</p>
                        <hr style='border: 1px solid #eee;' />
                        <p><small style='color: #777;'>Este correo fue generado automáticamente desde el sitio web de Hotel APM Grand (UTMACH).</small></p>
                    </div>
                ";
                correo.IsBodyHtml = true;

                // Configuración SMTP de Gmail
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential(correoDespachador, claveDespachador);
                smtp.EnableSsl = true;

                // Envío del mensaje
                smtp.Send(correo);

                // Feedback visual de éxito
                pnlMensaje.CssClass = "alert alert-success alert-dismissible fade show mb-4";
                lblMensaje.Text = $"Gracias <strong>{nombreCliente}</strong>, tu mensaje ha sido enviado correctamente a nuestro equipo técnico y de atención. Te responderemos pronto a <strong>{emailCliente}</strong>.";
                pnlMensaje.Visible = true;

                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                // Feedback visual de error
                pnlMensaje.CssClass = "alert alert-danger alert-dismissible fade show mb-4";
                lblMensaje.Text = $"Ocurrió un error al enviar el mensaje: {ex.Message}";
                pnlMensaje.Visible = true;
            }
        }

        /// <summary>
        /// Método de verificación que valida sintaxis y resuelve si el dominio del correo existe.
        /// </summary>
        private bool EsCorreoValido(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // A) Verificación 1: Expresión regular estándar para la estructura del correo
                string patronRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                if (!Regex.IsMatch(email, patronRegex, RegexOptions.IgnoreCase))
                    return false;

                // B) Verificación 2: Estructura usando MailAddress
                var direccionMail = new MailAddress(email);

                // C) Verificación 3: Validar que el dominio del correo tenga resolución DNS válida
                string dominio = direccionMail.Host;
                IPHostEntry hostEntry = Dns.GetHostEntry(dominio);

                return hostEntry.AddressList.Length > 0;
            }
            catch
            {
                // Si la conversión falla o el dominio no existe en la red, retorna false
                return false;
            }
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