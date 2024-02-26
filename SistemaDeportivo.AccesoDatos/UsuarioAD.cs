using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaDeportivo.EntidadNegocio;

namespace SistemaDeportivo.AccesoDatos
{
    public class UsuarioAD
    {
        // ----------------------------    CADENA DE CONEXIÓN     ----------------------------------- //

        private string connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

        public DataTable ObtenerUsuario(Usuario usuario)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection cnx = new SqlConnection(connectionString))
            {
                cnx.Open();
                try
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Listar_Usuario", cnx))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@NombreUsuario", usuario.nombreUsuario);
                        cmd.Parameters.AddWithValue("@Clave", usuario.clave);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Cargar el DataTable con los datos del SqlDataReader
                            dataTable.Load(reader);
                        }
                    }
                }
                catch (Exception)
                {
                    // Manejar la excepción según tus necesidades
                    dataTable = null;
                }
            }

            return dataTable;
        }

        public bool InsertarUsuario(Usuario usuario)
        {
            bool estado = false;

            using (SqlConnection cnx = new SqlConnection(connectionString))
            {
                cnx.Open();

                try
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Insertar_Usuario", cnx))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@NombreUsuario", usuario.nombreUsuario);
                        cmd.Parameters.AddWithValue("@Clave", usuario.clave);

                        var res = cmd.ExecuteNonQuery();

                        if (res == 1)
                        {
                            estado = true;
                        }
                    }
                }
                catch (Exception)
                {
                    estado = false;
                }
            }

            return estado;
        }

    }
}
