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
using Cliente = CapadeEntidades.Cliente.Cliente;

namespace Presentacion.Clientes
{
    public partial class frmClientes : Form
    {
        private readonly ClienteLN clienteLN = new ClienteLN();

        public frmClientes()
        {
            InitializeComponent();

            // La tabla arranca oculta. Se muestra al presionar "Ver tabla" en el toolStrip.
            dataGridView1.Visible = false;
            textBox7.PasswordChar = '*';

            // Enganchamos CellClick para permitir selección completa de la fila
            this.dataGridView1.CellClick += new DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
        }

        // ---------------- Carga del grid ----------------
        private void CargarClientes()
        {
            dataGridView1.DataSource = clienteLN.ListarClientes();
        }

        // ---------------- Eventos de texto ----------------
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

        private void textBox9_TextChanged(object sender, EventArgs e) // Buscar
        {
            FiltrarClientes(textBox9.Text.Trim());
        }

        public void FiltrarClientes(string texto)
        {
            try
            {
                string filtro = texto?.Trim().ToLower() ?? "";

                List<Cliente> listaCompleta = clienteLN.ListarClientes();

                if (string.IsNullOrWhiteSpace(filtro))
                {
                    dataGridView1.DataSource = listaCompleta;
                }
                else
                {
                    var listaFiltrada = listaCompleta.Where(c =>
                        (c.Nombre != null && c.Nombre.ToLower().Contains(filtro)) ||
                        (c.Apellido != null && c.Apellido.ToLower().Contains(filtro)) ||
                        (c.DocumentoIdentidad != null && c.DocumentoIdentidad.ToLower().Contains(filtro)) ||
                        (c.Telefono != null && c.Telefono.ToLower().Contains(filtro))
                    ).ToList();

                    dataGridView1.DataSource = listaFiltrada;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar clientes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        // ---------------- Guardar: inserta un cliente nuevo ----------------
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox4.Text) || string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Nombre y apellido son obligatorios.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox8.Text) || string.IsNullOrWhiteSpace(textBox7.Text))
            {
                MessageBox.Show("Correo y contraseña son obligatorios para crear el cliente.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
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

                CargarClientes();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ---------------- Editar: actualiza el cliente seleccionado ----------------
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Seleccione un cliente del listado para editar.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox4.Text) || string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Nombre y apellido son obligatorios.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
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

                CargarClientes();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ---------------- Eliminar: borra el cliente seleccionado ----------------
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Seleccione un cliente del listado para eliminar.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirmacion = MessageBox.Show(
                "¿Está seguro de eliminar este cliente?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacion != DialogResult.Yes) return;

            try
            {
                clienteLN.EliminarCliente(Convert.ToInt32(textBox1.Text));

                MessageBox.Show("El cliente se eliminó correctamente.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarClientes();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ---------------- Ver tabla: muestra el grid y carga los datos ----------------
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = true;
            CargarClientes();
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