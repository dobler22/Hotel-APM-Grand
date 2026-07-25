using CapadeEntidades.Cliente;
using CapadeEntidades.Empleado;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.MonthCalendar;

namespace Presentacion.Usuariofrms
{
    public partial class frmEditUsuario : Form
    {

        public frmEditUsuario()
        {
            InitializeComponent();
        }
        // Propiedades públicas para recuperar los datos desde frmAdminUsuario
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string RolSeleccionado { get; set; }

        public Empleado Emp { get; set; }
        public Cliente Cli { get; set; }

        private bool esEdicion = false;

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                string rol = comboBox1.SelectedItem.ToString();

                if (rol.Equals("Empleado", StringComparison.OrdinalIgnoreCase))
                {
                    groupBox1.Enabled = true;
                    groupBox1.Visible = true;

                    groupBox2.Enabled = false;
                    groupBox2.Visible = false;
                }
                else if (rol.Equals("Cliente", StringComparison.OrdinalIgnoreCase))
                {
                    groupBox1.Enabled = false;
                    groupBox1.Visible = false;

                    groupBox2.Enabled = true;
                    groupBox2.Visible = true;
                }
            }
        }

        public bool ValidarDatos()
        {
            // Validaciones de cuenta (solo en inserción)
            if (!esEdicion)
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                    string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    MessageBox.Show("El correo electrónico y la contraseña son obligatorios.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            // Validaciones generales de usuario
            if (string.IsNullOrWhiteSpace(textBox12.Text) ||
                string.IsNullOrWhiteSpace(textBox11.Text))
            {
                MessageBox.Show("El nombre y el apellido son obligatorios.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Validaciones según el Rol activo
            string rol = comboBox1.SelectedItem?.ToString();

            if (rol.Equals("Empleado", StringComparison.OrdinalIgnoreCase))
            {
                if (string.IsNullOrWhiteSpace(textBox5.Text))
                {
                    MessageBox.Show("El cargo del empleado es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            else if (rol.Equals("Cliente", StringComparison.OrdinalIgnoreCase))
            {
                if (string.IsNullOrWhiteSpace(textBox9.Text))
                {
                    MessageBox.Show("El documento de identidad es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            return true;
        }

        private void frmEditUsuario_Load(object sender, EventArgs e)
        {
            if (!esEdicion)
            {
                if (comboBox1.Items.Count == 0)
                {
                    comboBox1.Items.Add("Empleado");
                    comboBox1.Items.Add("Cliente");
                    comboBox1.Items.Add("Administrador");
                }
                comboBox1.SelectedIndex = 0;
            }
        }
        public void CrearObjeto()
        {
            // Capturamos los datos generales de cuenta
            Email = textBox1.Text.Trim();
            PasswordHash = textBox2.Text;
            RolSeleccionado = comboBox1.SelectedItem.ToString();

            // Intentamos parsear el ID del TextBox (si es nuevo será 0)
            int.TryParse(textBox3.Text, out int idActual);

            // Mapeo según el Rol seleccionado en el ComboBox
            if (RolSeleccionado.Equals("Empleado", StringComparison.OrdinalIgnoreCase))
            {
                Emp = new Empleado
                {
                    IdEmpleado = idActual, // Trae 0 si es nuevo, o el id_empleado si es edición
                    Nombre = textBox12.Text.Trim(),
                    Apellido = textBox11.Text.Trim(),
                    Cargo = textBox5.Text.Trim(),
                    Area = textBox6.Text.Trim(),
                    Telefono = textBox7.Text.Trim(),
                    FechaIngreso = dateTimePicker1.Value
                };
            }
            else if (RolSeleccionado.Equals("Cliente", StringComparison.OrdinalIgnoreCase))
            {
                Cli = new Cliente
                {
                    IdCliente = idActual, // Trae 0 si es nuevo, o el id_cliente si es edición
                    Nombre = textBox12.Text.Trim(),
                    Apellido = textBox11.Text.Trim(),
                    Telefono = textBox10.Text.Trim(),
                    DocumentoIdentidad = textBox9.Text.Trim(),
                    Nacionalidad = textBox8.Text.Trim(),
                    FechaNacimiento = dateTimePicker2.Value
                };
            }
        }
        // Método nuevo en frmEditUsuario.cs para cargar un Administrador
        public void SetDatosUsuarioGeneral(CapadeEntidades.Usuario.Usuario usuario)
        {
            esEdicion = true;
            RolSeleccionado = usuario.Rol;

            textBox3.Text = usuario.Id.ToString(); // Muestra id_usuario
            textBox1.Text = usuario.Email;
            textBox1.Enabled = false;
            textBox2.Enabled = false;

            comboBox1.Items.Clear();
            comboBox1.Items.Add(usuario.Rol);
            comboBox1.SelectedIndex = 0;
            comboBox1.Enabled = false;

            // Deshabilitamos o invisibilizamos paneles de datos extendidos (Empleado/Cliente)
            groupBox1.Visible = false;
            groupBox2.Visible = false;
        }
        // Carga los datos para editar un Empleado existente
        public void SetDatosEmpleado(Empleado emp, string email)
        {
            esEdicion = true;
            Emp = emp;
            RolSeleccionado = "Empleado";

            textBox3.Text = emp.IdEmpleado.ToString(); // Muestra el id_empleado
            textBox1.Text = email;                     
            textBox1.Enabled = false;
            textBox2.Enabled = false;

            comboBox1.Items.Clear();
            comboBox1.Items.Add("Empleado");
            comboBox1.SelectedIndex = 0;
            comboBox1.Enabled = false;

            textBox12.Text = emp.Nombre;
            textBox11.Text = emp.Apellido;
            textBox5.Text = emp.Cargo;
            textBox6.Text = emp.Area;
            textBox7.Text = emp.Telefono;
            if (emp.FechaIngreso != DateTime.MinValue) dateTimePicker1.Value = emp.FechaIngreso;
        }

        // Carga los datos para editar un Cliente existente
        public void SetDatosCliente(Cliente cli, string email)
        {
            esEdicion = true;
            Cli = cli;
            RolSeleccionado = "Cliente";

            textBox3.Text = cli.IdCliente.ToString(); // Muestra el id_cliente
            textBox1.Text = email;
            textBox1.Enabled = false;
            textBox2.Enabled = false;

            comboBox1.Items.Clear();
            comboBox1.Items.Add("Cliente");
            comboBox1.SelectedIndex = 0;
            comboBox1.Enabled = false;

            textBox12.Text = cli.Nombre;
            textBox11.Text = cli.Apellido;
            textBox10.Text = cli.Telefono;
            textBox9.Text = cli.DocumentoIdentidad;
            textBox9.Enabled = false;
            textBox8.Text = cli.Nacionalidad;
            if (cli.FechaNacimiento != DateTime.MinValue) dateTimePicker2.Value = cli.FechaNacimiento;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                CrearObjeto();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
