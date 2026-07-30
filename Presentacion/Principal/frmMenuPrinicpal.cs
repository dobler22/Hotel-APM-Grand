using Microsoft.Reporting.WinForms;
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
        private string conexionString = "Data Source=localhost;Initial Catalog=Base_Datos_Hotel_APM_Grand;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";
        public frmMenuPrinicpal()
        {
            InitializeComponent();
        }

        private void frmMenuPrinicpal_Load(object sender, EventArgs e)
        {
            CargarTarjetaReservas();
            CargarTarjetaClientes();
            CargarTarjetaEmpleados();

            // Carga y renderizado del gráfico de barras RDLC
            CargarReporteGrafico();
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
    }
}
