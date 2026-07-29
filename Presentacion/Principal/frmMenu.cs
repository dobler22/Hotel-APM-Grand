using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Presentacion.Clientes;

namespace Presentacion.Principal
{
    public partial class frmMenu : Form
    {
        // Guarda el formulario actualmente mostrado dentro del panel
        private Form formularioActivo = null;

        public frmMenu()
        {
            InitializeComponent();

            this.btnMenu1.MouseDown += new MouseEventHandler(this.btnMenu1_MouseDown);
            this.btnMenu1.MouseUp += new MouseEventHandler(this.btnMenu1_MouseUp);
        }


        private void btnMenu1_MouseDown(object sender, MouseEventArgs e)
        {
            btnMenu1.BackColor = Color.LightGray; // color de sombreado
        }

        // Evento: volver al color normal al soltar
        private void btnMenu1_MouseUp(object sender, MouseEventArgs e)
        {
            btnMenu1.BackColor = SystemColors.Control; // color original
        }

        private void btnMenu1_Paint(object sender, PaintEventArgs e)
        {
            this.btnMenu1.Click += new System.EventHandler(this.btnMenu1_Click);
        }

        // OJO: este es el evento Click (el que se dispara al presionar el panel/botón).
        // btnMenu1 es un Panel, así que tenés que engancharlo vos desde el diseñador:
        // seleccioná btnMenu1 -> Propiedades -> ícono del rayo (eventos) -> en "Click"
        // elegí "btnMenu1_Click" de la lista desplegable.
        private void btnMenu1_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new frmClientes());
        }

        // Carga cualquier formulario hijo dentro de panel1, reemplazando el anterior
        private void AbrirFormularioEnPanel(Form formularioHijo)
        {
            // Cierra y limpia el formulario que estuviera mostrándose antes
            if (formularioActivo != null)
            {
                formularioActivo.Close();
                formularioActivo.Dispose();
            }

            panel1.Controls.Clear();

            formularioHijo.TopLevel = false;
            formularioHijo.FormBorderStyle = FormBorderStyle.None;
            formularioHijo.Dock = DockStyle.Fill;

            panel1.Controls.Add(formularioHijo);
            formularioHijo.Show();

            formularioActivo = formularioHijo;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            // No necesita lógica: el contenido se agrega dinámicamente
            // desde AbrirFormularioEnPanel, no dibujándolo acá.
        }
    }
}