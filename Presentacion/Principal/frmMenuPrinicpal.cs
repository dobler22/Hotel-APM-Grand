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
    }
}
