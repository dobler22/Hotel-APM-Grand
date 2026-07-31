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
using Habitacion = CapaEntidades.Habitacion.Habitacion;

namespace Presentacion.Habitaciones
{
    public partial class frmHabitaciones : Form
    {
        private readonly HabitacionLN habitacionLN = new HabitacionLN();

        public frmHabitaciones()
        {
            InitializeComponent();

            // El Designer no tiene cargados los Items de los combobox, así que los llenamos acá
            if (comboBox1.Items.Count == 0)
                comboBox1.Items.AddRange(new object[] { "Simple", "Doble", "Suite", "Familiar" });

            if (comboBox2.Items.Count == 0)
                comboBox2.Items.AddRange(new object[] { "Disponible", "Ocupada", "Mantenimiento" });

            // El label "Buscar", el textbox de búsqueda y la tabla arrancan ocultos.
            // Se muestran recién cuando el usuario presiona "Ver tabla" (toolStripButton4).
            label10.Visible = false;
            textBox9.Visible = false;
            dataGridView1.Visible = false;

            // CellContentClick solo se dispara al clickear contenido de una celda.
            // Enganchamos también CellClick para que funcione al hacer clic en cualquier
            // parte de la fila (incluida la selección con la flecha del margen izquierdo).
            this.dataGridView1.CellClick += new DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
        }

        // ---------------- Carga del grid ----------------
        private void CargarHabitaciones()
        {
            dataGridView1.DataSource = habitacionLN.ListarHabitaciones();
        }

        // ---------------- Eventos de texto (sin validación en tiempo real por ahora) ----------------
        private void textBox1_TextChanged(object sender, EventArgs e) // Id Habitacion
        {
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) // Tipo
        {
        }

        private void textBox4_TextChanged(object sender, EventArgs e) // Numero
        {
        }

        private void textBox3_TextChanged(object sender, EventArgs e) // Piso
        {
        }

        private void textBox6_TextChanged(object sender, EventArgs e) // Capacidad
        {
        }

        private void textBox5_TextChanged(object sender, EventArgs e) // Precio por noche
        {
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) // Estado
        {
        }

        private void label10_Click(object sender, EventArgs e) // Label "Buscar"
        {
        }

        private void textBox9_TextChanged(object sender, EventArgs e) // Buscar
        {
            string filtro = textBox9.Text.Trim();

            if (string.IsNullOrWhiteSpace(filtro))
            {
                CargarHabitaciones();
            }
            else
            {
                // HabitacionLN no tiene un método de búsqueda por texto, así que filtramos
                // en memoria sobre lo ya listado (sin tocar la capa lógica).
                var filtradas = habitacionLN.ListarHabitaciones()
                    .Where(h =>
                        (h.Numero != null && h.Numero.ToLower().Contains(filtro.ToLower())) ||
                        (h.Tipo != null && h.Tipo.ToLower().Contains(filtro.ToLower())) ||
                        (h.Estado != null && h.Estado.ToLower().Contains(filtro.ToLower())))
                    .ToList();

                dataGridView1.DataSource = filtradas;
            }
        }

        // ---------------- Click en el grid: cargar la habitación seleccionada en los campos ----------------
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dataGridView1.Rows[e.RowIndex].DataBoundItem is Habitacion habitacion)
            {
                textBox1.Text = habitacion.IdHabitacion.ToString();
                textBox4.Text = habitacion.Numero;
                comboBox1.Text = habitacion.Tipo;
                textBox3.Text = habitacion.Piso.ToString();
                textBox6.Text = habitacion.Capacidad.ToString();
                textBox5.Text = habitacion.PrecioPorNoche.ToString("0.00");
                comboBox2.Text = habitacion.Estado;
            }
        }

        // ---------------- Guardar: inserta una habitación nueva ----------------
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox4.Text) || string.IsNullOrWhiteSpace(comboBox1.Text))
            {
                MessageBox.Show("Número y tipo son obligatorios.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(textBox3.Text, out int piso) ||
                !int.TryParse(textBox6.Text, out int capacidad) ||
                !decimal.TryParse(textBox5.Text, out decimal precio))
            {
                MessageBox.Show("Piso, capacidad y precio deben ser valores numéricos válidos.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Habitacion nuevaHabitacion = new Habitacion(
                    0,
                    textBox4.Text.Trim(),
                    comboBox1.Text.Trim(),
                    capacidad,
                    piso,
                    precio,
                    !string.IsNullOrWhiteSpace(comboBox2.Text) ? comboBox2.Text.Trim() : "disponible");

                habitacionLN.CrearHabitacion(nuevaHabitacion);

                MessageBox.Show("Los datos se insertaron correctamente.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarHabitaciones();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ---------------- Editar: actualiza la habitación seleccionada ----------------
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Seleccione una habitación del listado para editar.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(comboBox1.Text))
            {
                MessageBox.Show("El tipo de habitación es obligatorio.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(textBox6.Text, out int capacidad) ||
                !decimal.TryParse(textBox5.Text, out decimal precio))
            {
                MessageBox.Show("Capacidad y precio deben ser valores numéricos válidos.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Habitacion habitacion = new Habitacion(
                    Convert.ToInt32(textBox1.Text),
                    textBox4.Text.Trim(),
                    comboBox1.Text.Trim(),
                    capacidad,
                    0, // El piso no se actualiza (HabitacionLN.ActualizarHabitacion no lo recibe)
                    precio,
                    !string.IsNullOrWhiteSpace(comboBox2.Text) ? comboBox2.Text.Trim() : "");

                habitacionLN.ActualizarHabitacion(habitacion);

                // ActualizarHabitacion no toca el Estado (solo Tipo, Capacidad y Precio),
                // así que lo actualizamos aparte con el método que sí existe para eso.
                if (!string.IsNullOrWhiteSpace(comboBox2.Text))
                {
                    habitacionLN.CambiarEstadoHabitacion(habitacion.IdHabitacion, comboBox2.Text.Trim());
                }

                MessageBox.Show("Los datos se modificaron correctamente.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarHabitaciones();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Seleccione una habitación del listado para eliminar.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirmacion = MessageBox.Show(
                "¿Está seguro de eliminar esta habitación?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacion != DialogResult.Yes) return;

            try
            {
                habitacionLN.EliminarHabitacion(Convert.ToInt32(textBox1.Text));

                MessageBox.Show("La habitación se eliminó correctamente.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarHabitaciones();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ---------------- Ver tabla: muestra el buscador y el grid, y carga los datos ----------------
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            label10.Visible = true;
            textBox9.Visible = true;
            dataGridView1.Visible = true;

            CargarHabitaciones();
        }

        private void LimpiarCampos()
        {
            textBox1.Text = "";
            textBox4.Text = "";
            textBox3.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
        }
    }
}