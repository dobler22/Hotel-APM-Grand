using CapadeEntidades.Cliente;
using CapadeEntidades.Empleado;
using CapadeEntidades.Usuario;
using CapadeLogica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Usuario
{
    public partial class frmAdminUsuario : Form
    {
        private UsuarioLN usuarioLN = new UsuarioLN();
        private EmpleadoLN empleadoLN = new EmpleadoLN();
        private ClienteLN clienteLN = new ClienteLN();
        private CapadeEntidades.Usuario.Usuario usuarioEnSesion; // Usuario autenticado actualmente
        public frmAdminUsuario(CapadeEntidades.Usuario.Usuario usuarioSesion)
        {
            InitializeComponent();
            this.usuarioEnSesion = usuarioSesion;
        }
        public void ListarUsuarios(string filtroText)
        {
            try
            {
                dataGridView1.DataSource = usuarioLN.ListarUsuariosPorRol(usuarioEnSesion.Rol, filtroText);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al listar usuarios: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmAdminUsuario_Load(object sender, EventArgs e)
        {
            // 1. Mostrar la información del usuario en el Label principal y en el título
            label1.Text = $"Usuario: {usuarioEnSesion.Email} | Rol: {usuarioEnSesion.Rol}";
            this.Text = $"Gestión de Usuarios - Hotel APM Grand";

            // 2. Restricción adicional: Si ingresa un 'Cliente', se desactivan los botones de acción
            if (usuarioEnSesion.Rol == "Cliente")
            {
                button1.Enabled = false; // Nuevo
                button2.Enabled = false; // Modificar
                button3.Enabled = false; // Eliminar

                MessageBox.Show("Usted ha ingresado con rol de Cliente. Solo tiene permisos de lectura.",
                                "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // 3. Cargar la lista aplicando las restricciones (Empleado solo ve Clientes, Admin ve todos)
            ListarUsuarios("");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            ListarUsuarios(textBox1.Text);
        }

        public void Nuevo()
        {
            try
            {
                Usuariofrms.frmEditUsuario frm = new Usuariofrms.frmEditUsuario();
                frm.Text = "Agregar Usuario";

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    if (frm.RolSeleccionado.Equals("Empleado", StringComparison.OrdinalIgnoreCase))
                    {
                        empleadoLN.CrearEmpleado(frm.Email, frm.PasswordHash, frm.Emp);
                        MessageBox.Show("Empleado registrado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (frm.RolSeleccionado.Equals("Cliente", StringComparison.OrdinalIgnoreCase))
                    {
                        clienteLN.CrearCliente(frm.Email, frm.PasswordHash, frm.Cli);
                        MessageBox.Show("Cliente registrado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    ListarUsuarios(textBox1.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Modificar()
        {
            if (dataGridView1.CurrentRow != null)
            {
                try
                {
                    CapadeEntidades.Usuario.Usuario usuarioSeleccionado = (CapadeEntidades.Usuario.Usuario)dataGridView1.CurrentRow.DataBoundItem;

                    // Restricción de seguridad: Un empleado solo modifica clientes
                    if (usuarioEnSesion.Rol.Equals("Empleado", StringComparison.OrdinalIgnoreCase) &&
                        !usuarioSeleccionado.Rol.Equals("Cliente", StringComparison.OrdinalIgnoreCase))
                    {
                        MessageBox.Show("Solo está autorizado a modificar cuentas de Clientes.", "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    Usuariofrms.frmEditUsuario frm = new Usuariofrms.frmEditUsuario();
                    frm.Text = "Editar Usuario";

                    // Carga de datos según el Rol del usuario
                    if (usuarioSeleccionado.Rol.Equals("Empleado", StringComparison.OrdinalIgnoreCase))
                    {
                        Empleado emp = empleadoLN.ObtenerPorIdUsuario(usuarioSeleccionado.Id);
                        if (emp == null)
                        {
                            MessageBox.Show("No se encontraron los datos de perfil para este empleado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        frm.SetDatosEmpleado(emp, usuarioSeleccionado.Email);
                    }
                    else if (usuarioSeleccionado.Rol.Equals("Cliente", StringComparison.OrdinalIgnoreCase))
                    {
                        Cliente cli = clienteLN.ObtenerPorIdUsuario(usuarioSeleccionado.Id);
                        if (cli == null)
                        {
                            MessageBox.Show("No se encontraron los datos de perfil para este cliente.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        frm.SetDatosCliente(cli, usuarioSeleccionado.Email);
                    }
                    else
                    {
                        // Para Administrador u otros usuarios que solo residen en la tabla Usuario
                        frm.SetDatosUsuarioGeneral(usuarioSeleccionado);
                    }

                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        if (frm.RolSeleccionado.Equals("Empleado", StringComparison.OrdinalIgnoreCase))
                        {
                            empleadoLN.ActualizarEmpleado(frm.Emp);
                        }
                        else if (frm.RolSeleccionado.Equals("Cliente", StringComparison.OrdinalIgnoreCase))
                        {
                            clienteLN.ActualizarCliente(frm.Cli);
                        }

                        ListarUsuarios(textBox1.Text);
                        MessageBox.Show("Información actualizada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al modificar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Seleccione un usuario de la lista para editar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void Eliminar()
        {
            if (dataGridView1.CurrentRow != null)
            {
                try
                {
                    CapadeEntidades.Usuario.Usuario usuarioSeleccionado = (CapadeEntidades.Usuario.Usuario)dataGridView1.CurrentRow.DataBoundItem;

                    // Restricción de seguridad
                    if (usuarioEnSesion.Rol.Equals("Empleado", StringComparison.OrdinalIgnoreCase) &&
                        !usuarioSeleccionado.Rol.Equals("Cliente", StringComparison.OrdinalIgnoreCase))
                    {
                        MessageBox.Show("Solo está autorizado a eliminar cuentas de Clientes.", "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Prevenir autoeleminación
                    if (usuarioSeleccionado.Id == usuarioEnSesion.Id)
                    {
                        MessageBox.Show("No puede eliminar su propia cuenta en uso.", "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    DialogResult respuesta = MessageBox.Show($"¿Está seguro de eliminar al usuario {usuarioSeleccionado.Email}?",
                                                             "Confirmar eliminación",
                                                             MessageBoxButtons.YesNo,
                                                             MessageBoxIcon.Question);

                    if (respuesta == DialogResult.Yes)
                    {
                        // En SQL Server la eliminación de la cuenta principal se maneja desde UsuarioLN
                        usuarioLN.EliminarUsuario(usuarioSeleccionado.Id);

                        ListarUsuarios(textBox1.Text);
                        MessageBox.Show("Usuario eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Seleccione un usuario para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Nuevo();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Modificar();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Eliminar();
        }
    }
}
