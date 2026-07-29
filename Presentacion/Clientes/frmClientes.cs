using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapadeLogica;
using Cliente = CapadeEntidades.Cliente.Cliente;

namespace Presentacion.Clientes
{
    public partial class frmClientes : Form
    {
        private readonly ClienteLN clienteLN = new ClienteLN();

        public frmClientes()
        {
            InitializeComponent();
            CargarClientes();
            textBox7.PasswordChar = '*';
        }

        // ---------------- Carga del grid ----------------
        private void CargarClientes()
        {
            dataGridView1.DataSource = clienteLN.ListarClientes();
        }

        // ---------------- Eventos de texto (sin validación en tiempo real por ahora) ----------------
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e) // Id Cliente
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e) // Documento/Cédula
        {
        }

        private void textBox4_TextChanged(object sender, EventArgs e) // Nombre
        {
        }

        private void textBox3_TextChanged(object sender, EventArgs e) // Apellido
        {
        }

        private void textBox6_TextChanged(object sender, EventArgs e) // Telefono
        {
        }

        private void textBox5_TextChanged(object sender, EventArgs e) // Nacionalidad
        {
        }

        private void textBox8_TextChanged(object sender, EventArgs e) // Correo
        {
        }

        private void textBox7_TextChanged(object sender, EventArgs e) // Contraseña
        {
        }

        private void textBox9_TextChanged(object sender, EventArgs e) // Buscar (sin uso por ahora)
        {
        }

        // ---------------- Click en el grid: cargar el cliente seleccionado en los campos ----------------
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dataGridView1.Rows[e.RowIndex].DataBoundItem is Cliente cliente)
            {
                textBox1.Text = cliente.IdCliente.ToString();
                textBox2.Text = cliente.DocumentoIdentidad;
                textBox4.Text = cliente.Nombre;
                textBox3.Text = cliente.Apellido;
                textBox6.Text = cliente.Telefono;
                textBox5.Text = cliente.Nacionalidad;

                if (cliente.FechaNacimiento != DateTime.MinValue)
                    dateTimePicker1.Value = cliente.FechaNacimiento;

                // Al editar un cliente existente ya no se toca la cuenta de acceso
                textBox8.Text = "";
                textBox7.Text = "";
                textBox8.Enabled = false;
                textBox7.Enabled = false;
            }
        }

        // ---------------- Guardar (crea si no hay Id, actualiza si ya existe) ----------------
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox4.Text) || string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Nombre y apellido son obligatorios.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                bool esNuevo = string.IsNullOrWhiteSpace(textBox1.Text);

                if (esNuevo)
                {
                    if (string.IsNullOrWhiteSpace(textBox8.Text) || string.IsNullOrWhiteSpace(textBox7.Text))
                    {
                        MessageBox.Show("Correo y contraseña son obligatorios para crear el cliente.", "Validación",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    Cliente nuevoCliente = new Cliente(
                        0, 0,
                        textBox4.Text.Trim(),
                        textBox3.Text.Trim(),
                        textBox6.Text.Trim(),
                        textBox2.Text.Trim(),
                        textBox5.Text.Trim(),
                        dateTimePicker1.Value);

                    clienteLN.CrearCliente(textBox8.Text.Trim(), textBox7.Text, nuevoCliente);
                    MessageBox.Show("Cliente creado correctamente.", "Aviso",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Cliente cliente = new Cliente(
                        Convert.ToInt32(textBox1.Text),
                        0,
                        textBox4.Text.Trim(),
                        textBox3.Text.Trim(),
                        textBox6.Text.Trim(),
                        textBox2.Text.Trim(),
                        textBox5.Text.Trim(),
                        dateTimePicker1.Value);

                    clienteLN.ActualizarCliente(cliente);
                    MessageBox.Show("Cliente actualizado correctamente.", "Aviso",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                CargarClientes();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ---------------- Cancelar: descarta lo que se estaba editando ----------------
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        // ---------------- Limpiar: deja el formulario listo para un registro nuevo ----------------
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            dateTimePicker1.Value = DateTime.Now;

            textBox7.Enabled = true;
            textBox8.Enabled = true;
        }

        private void frmClientes_Load(object sender, EventArgs e)
        {

        }
    }
}