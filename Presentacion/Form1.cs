using CapadeEntidades.Usuario;
using CapadeLogica;
using Presentacion.Usuario;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("El campo de correo electrónico no puede estar vacío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("El campo de contraseña no puede estar vacío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 1. Validar campos de texto
            if (!ValidarCampos())
            {
                return;
            }

            try
            {
                UsuarioLN usuarioLN = new UsuarioLN();
                string email = textBox1.Text.Trim();
                string password = textBox2.Text.Trim();

                // 2. Intentar login
                CapadeEntidades.Usuario.Usuario usuarioEncontrado = usuarioLN.Login(email, password);

                // 3. Verificar respuesta
                if (usuarioEncontrado != null)
                {
                    MessageBox.Show($"¡Inicio de sesión exitoso!\nBienvenido(a) [{usuarioEncontrado.Rol}]: {usuarioEncontrado.Email}",
                                    "Bienvenido",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                    // 4. Abrir el formulario de gestión pasando el usuario autenticado
                    frmAdminUsuario adminForm = new frmAdminUsuario(usuarioEncontrado);
                    adminForm.Show();

                    // Ocultar el Login
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Credenciales incorrectas o usuario inactivo.",
                                    "Acceso Denegado",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error en el Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
