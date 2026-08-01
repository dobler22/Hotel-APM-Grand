using CapadeEntidades.Usuario;
using Microsoft.Reporting.WinForms;
using Presentacion.Clientes;
using Presentacion.Empleados;
using Presentacion.Factura;
using Presentacion.Habitaciones;
using Presentacion.Reportes;
using Presentacion.Reservas;
using Presentacion.Servicios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Principal
{
    public partial class frmMenuPrinicpal : Form
    {

        private string conexionString = @"Data Source=.;Initial Catalog=""Base_Datos_Hotel_APM_Grand"";Integrated Security=True;Encrypt=False;TrustServerCertificate=True;";
        private CapadeEntidades.Usuario.Usuario usuarioEnSesion; // Usuario autenticado actualmente

        public frmMenuPrinicpal(CapadeEntidades.Usuario.Usuario usuario)
        {
            InitializeComponent();
            this.usuarioEnSesion = usuario;
        }
        private void frmMenuPrinicpal_Load(object sender, EventArgs e)
        {
            CargarTarjetaReservas();
            CargarTarjetaClientes();
            CargarTarjetaEmpleados();
            CargarTarjetaFacturas();

            // Carga y renderizado del gráfico de barras RDLC
            CargarReporteGrafico();
            // 1. Mostrar la información del usuario en el Label principal y en el título
            label7.Text = $"Usuario: {usuarioEnSesion.Email} | Rol: {usuarioEnSesion.Rol}";
            this.Text = $"Gestión de Usuarios - Hotel APM Grand";
        }
        private void CargarTarjetaReservas()
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(conexionString))
                {
                    conexion.Open();

                    // 1. Obtener el Total General de Reservas
                    string queryTotal = "SELECT COUNT(*) FROM Reservas";
                    using (SqlCommand cmdTotal = new SqlCommand(queryTotal, conexion))
                    {
                        int totalReservas = Convert.ToInt32(cmdTotal.ExecuteScalar());
                        lblTotalReservas.Text = totalReservas.ToString();
                    }

                    // 2. Obtener las Nuevas Reservas creadas HOY usando 'fecha_creacion'
                    string queryNuevas = "SELECT COUNT(*) FROM Reservas WHERE CAST(fecha_creacion AS DATE) = CAST(GETDATE() AS DATE)";
                    using (SqlCommand cmdNuevas = new SqlCommand(queryNuevas, conexion))
                    {
                        int nuevasHoy = Convert.ToInt32(cmdNuevas.ExecuteScalar());
                        lblNuevasReservas.Text = $"{nuevasHoy} Nuevas Reservas";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos de Reservas: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CargarTarjetaClientes()
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(conexionString))
                {
                    conexion.Open();

                    // 1. Obtener el Total General de Clientes
                    string queryTotal = "SELECT COUNT(*) FROM Clientes";
                    using (SqlCommand cmdTotal = new SqlCommand(queryTotal, conexion))
                    {
                        int totalClientes = Convert.ToInt32(cmdTotal.ExecuteScalar());
                        lblClientesTotal.Text = totalClientes.ToString(); // Asigna aquí el ID de tu Label
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos de Clientes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CargarTarjetaEmpleados()
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(conexionString))
                {
                    conexion.Open();

                    // 1. Obtener el Total General de Empleados
                    string queryTotal = "SELECT COUNT(*) FROM Empleados";
                    using (SqlCommand cmdTotal = new SqlCommand(queryTotal, conexion))
                    {
                        int totalEmpleados = Convert.ToInt32(cmdTotal.ExecuteScalar());
                        lblTotalEmpleados.Text = totalEmpleados.ToString(); // Asigna aquí el ID de tu Label
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos de Empleados: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CargarTarjetaFacturas()
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(conexionString))
                {
                    conexion.Open();

                    // 1. Obtener el Total General de Facturas
                    string queryTotal = "SELECT COUNT(*) FROM Facturas"; // Ajusta 'Facturas' si tu tabla se llama de otro modo (ej. 'Factura')
                    using (SqlCommand cmdTotal = new SqlCommand(queryTotal, conexion))
                    {
                        int totalFacturas = Convert.ToInt32(cmdTotal.ExecuteScalar());
                        lblTotalFacturas.Text = totalFacturas.ToString(); // Asigna aquí el ID de tu Label principal (el del centro)
                    }

                    // 2. Obtener las Nuevas Facturas emitidas HOY
                    string queryNuevas = "SELECT COUNT(*) FROM Facturas WHERE CAST(fecha_emision AS DATE) = CAST(GETDATE() AS DATE)";
                    // NOTA: Ajusta 'fecha_emision' por el nombre exacto de la columna de fecha en tu base de datos (ej. 'fecha', 'fecha_creacion')

                    using (SqlCommand cmdNuevas = new SqlCommand(queryNuevas, conexion))
                    {
                        int nuevasHoy = Convert.ToInt32(cmdNuevas.ExecuteScalar());
                        lblNuevasFacturas.Text = $"{nuevasHoy} Nuevas Facturas"; // Asigna aquí el ID de tu Label inferior
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos de Facturas: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CargarReporteGrafico()
        {
            try
            {
                DataTable dt = new DataTable();

                using (SqlConnection conexion = new SqlConnection(conexionString))
                {
                    conexion.Open();
                    string query = @"SELECT 
                                UPPER(LEFT(DATENAME(MONTH, fecha_creacion), 1)) + LOWER(SUBSTRING(DATENAME(MONTH, fecha_creacion), 2, 10)) AS Mes,
                                COUNT(*) AS TotalReservas
                             FROM Reservas
                             GROUP BY DATENAME(MONTH, fecha_creacion), MONTH(fecha_creacion)
                             ORDER BY MONTH(fecha_creacion)";

                    using (SqlDataAdapter da = new SqlDataAdapter(query, conexion))
                    {
                        da.Fill(dt);
                    }
                }

                reportViewer1.LocalReport.DataSources.Clear();
                ReportDataSource rds = new ReportDataSource("DataSet1", dt); // "DataSet1" debe coincidir con el nombre de DataSet dentro del archivo .rdlc
                reportViewer1.LocalReport.ReportPath = @"Principal\rptReservasGrafico.rdlc";
                reportViewer1.LocalReport.DataSources.Add(rds);
                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el gráfico: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // ===== MÉTODO GENÉRICO DE NAVEGACIÓN =====
        private void AbrirFormEnPanel(Form formulario)
        {
            // Ocultar el dashboard (tarjetas)
            tableLayoutPanel1.Visible = false;

            // Cerrar cualquier formulario embebido anterior
            foreach (Control c in panelContenido.Controls)
            {
                if (c is Form)
                {
                    ((Form)c).Close();
                }
            }

            // Embeber el nuevo formulario
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;

            panelContenido.Controls.Add(formulario);
            formulario.BringToFront();
            formulario.Show();
        }
        private void MostrarDashboard()
        {
            // Cerrar formulario embebido si hay uno
            foreach (Control c in panelContenido.Controls)
            {
                if (c is Form)
                {
                    ((Form)c).Close();
                }
            }
            // Volver a mostrar las tarjetas
            tableLayoutPanel1.Visible = true;
            tableLayoutPanel1.BringToFront();
        }

        private void btnMenu0_Click(object sender, EventArgs e)
        {
            MostrarDashboard();
        }

        private void btnMenu1_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel(new frmClientes());
        }

        private void btnMenu2_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel(new frmHabitaciones());
        }

        private void btnMenu3_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel(new frmReservas());
        }

        private void btnMenu4_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel(new frmFactura());
        }

        private void btnMenu5_Click(object sender, EventArgs e)
        {

        }

        private void btnMenu6_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel(new frmServicios());
        }

        private void btnMenu7_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel(new frmEmpleados());
        }

        private void btnMenu8_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel(new frmReportes());
        }

        private void btnMenu9_Click(object sender, EventArgs e)
        {
        }

        private void btnMenu10_Click(object sender, EventArgs e)
        {

        }
    }
}
