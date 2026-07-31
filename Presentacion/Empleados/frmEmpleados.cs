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
using Empleado = CapadeEntidades.Empleado.Empleado;

namespace Presentacion.Empleados
{
    public partial class frmEmpleados : Form
    {
        private readonly EmpleadoLN empleadoLN = new EmpleadoLN();

        public frmEmpleados()
        {
            InitializeComponent();

            // La tabla arranca oculta. Se muestra recién al presionar "Ver tabla" en el toolStrip.
            dataGridView1.Visible = false;

            // Opciones para el Cargo (comboBox1)
            if (comboBox1.Items.Count == 0)
                comboBox1.Items.AddRange(new object[] { "Recepcionista", "Botones", "Gerente", "Mantenimiento", "Limpieza", "Cocina", "Seguridad" });

            // Opciones para el Área (comboBox2)
            if (comboBox2.Items.Count == 0)
                comboBox2.Items.AddRange(new object[] { "Administración", "Recepción", "Servicios Generales", "Alojamiento", "Alimentos y Bebidas", "Seguridad" });

            // CellContentClick solo se dispara al clickear contenido de una celda.
            // Enganchamos también CellClick para que funcione al hacer clic en cualquier
            // parte de la fila.
            this.dataGridView1.CellClick += new DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
        }

        // ---------------- Carga del grid ----------------
        private void CargarEmpleados()
        {
            dataGridView1.DataSource = empleadoLN.ListarEmpleados();
        }

        // ---------------- Eventos de texto ----------------
        private void textBox1_TextChanged(object sender, EventArgs e) // Id Empleado
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

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e) // Fecha de ingreso
        {
        }

        private void textBox8_TextChanged(object sender, EventArgs e) // Correo
        {
        }

        private void textBox7_TextChanged(object sender, EventArgs e) // Contraseña
        {
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) // Cargo
        {
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) // Area
        {
        }

        // ---------------- Click en el grid: cargar el empleado seleccionado en los campos ----------------
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dataGridView1.Rows[e.RowIndex].DataBoundItem is Empleado empleado)
            {
                textBox1.Text = empleado.IdEmpleado.ToString();
                textBox4.Text = empleado.Nombre;
                textBox3.Text = empleado.Apellido;
                comboBox1.Text = empleado.Cargo;
                comboBox2.Text = empleado.Area; // Ahora asigna el área al comboBox2
                textBox6.Text = empleado.Telefono;

                if (empleado.FechaIngreso != DateTime.MinValue)
                    dateTimePicker1.Value = empleado.FechaIngreso;

                // Al editar un empleado existente ya no se toca la cuenta de acceso
                textBox8.Text = "";
                textBox7.Text = "";
                textBox8.Enabled = false;
                textBox7.Enabled = false;
            }
        }

        // ---------------- Guardar: inserta un empleado nuevo ----------------
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
                MessageBox.Show("Correo y contraseña son obligatorios para crear el empleado.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Empleado nuevoEmpleado = new Empleado(
                    0, 0,
                    textBox4.Text.Trim(),
                    textBox3.Text.Trim(),
                    comboBox1.Text.Trim(),
                    comboBox2.Text.Trim(), // Toma el área desde comboBox2
                    textBox6.Text.Trim(),
                    dateTimePicker1.Value);

                empleadoLN.CrearEmpleado(textBox8.Text.Trim(), textBox7.Text, nuevoEmpleado);

                MessageBox.Show("Los datos se insertaron correctamente.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarEmpleados();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ---------------- Editar: actualiza el empleado seleccionado ----------------
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Seleccione un empleado del listado para editar.", "Validación",
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
                Empleado empleado = new Empleado(
                    Convert.ToInt32(textBox1.Text),
                    0,
                    textBox4.Text.Trim(),
                    textBox3.Text.Trim(),
                    comboBox1.Text.Trim(),
                    comboBox2.Text.Trim(), // Toma el área desde comboBox2
                    textBox6.Text.Trim(),
                    dateTimePicker1.Value);

                empleadoLN.ActualizarEmpleado(empleado);

                MessageBox.Show("Los datos se modificaron correctamente.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarEmpleados();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ---------------- Eliminar: borra el empleado seleccionado ----------------
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Seleccione un empleado del listado para eliminar.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirmacion = MessageBox.Show(
                "¿Está seguro de eliminar este empleado?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacion != DialogResult.Yes) return;

            try
            {
                empleadoLN.EliminarEmpleado(Convert.ToInt32(textBox1.Text));

                MessageBox.Show("El empleado se eliminó correctamente.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarEmpleados();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ---------------- Ver tabla: muestra el grid y carga los datos ----------------
        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            dataGridView1.Visible = true;
            CargarEmpleados();
        }

        private void LimpiarCampos()
        {
            textBox1.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            comboBox1.SelectedIndex = -1; 
            comboBox2.SelectedIndex = -1; 
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            dateTimePicker1.Value = DateTime.Now;

            textBox7.Enabled = true;
            textBox8.Enabled = true;
        }
    }
}