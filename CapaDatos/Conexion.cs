// Crea esta clase en tu proyecto Datos (capa de datos)
// Archivo: Conexion.cs

using System;
using System.Data.SqlClient;

namespace Datos
{
    public static class Conexion
    {
        // Cadena de conexión - Base_Datos_Hotel_APM_Grand
        private static string cadenaConexion = "Data Source=LAPTOP-J6VDLG09\\DOBLER;Initial Catalog=Base_Datos_Hotel_APM_Grand;Integrated Security=True;TrustServerCertificate=True";

        public static SqlConnection ObtenerConexion()
        {
            try
            {
                SqlConnection con = new SqlConnection(cadenaConexion);
                con.Open();
                return con;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al conectar con la base de datos: " + ex.Message);
            }
        }

        public static string CadenaConexion
        {
            get { return cadenaConexion; }
        }
    }
}