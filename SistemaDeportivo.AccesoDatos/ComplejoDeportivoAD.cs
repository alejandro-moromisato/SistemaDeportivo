using SistemaDeportivo.EntidadNegocio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeportivo.AccesoDatos
{
    public class ComplejoDeportivoAD
    {
        // ----------------------------    CADENA DE CONEXIÓN     ----------------------------------- //

        private string connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

        public List<ComplejoDeportivo> ListarComplejosDeportivos()
        {
            List<ComplejoDeportivo> lista = new List<ComplejoDeportivo>();

            using (SqlConnection cnx = new SqlConnection(connectionString))
            {
                cnx.Open();
                try
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Listar_Complejo_Deportivo", cnx))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ComplejoDeportivo cd = new ComplejoDeportivo();
                                cd.IdComplejoDeportivo = Convert.ToInt32(reader.GetValue(0).ToString());
                                cd.Localizacion = reader.GetValue(1).ToString();
                                cd.JefeOrganizacion = reader.GetValue(2).ToString();
                                cd.AreaTotal = Convert.ToInt32(reader.GetValue(3).ToString());
                                cd.Presupuesto = Convert.ToDecimal(reader.GetValue(4).ToString());
                                cd.NombreSede = reader.GetValue(5).ToString();
                                lista.Add(cd);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    lista = null;
                }
            }

            return lista;
        }

        public List<ComplejoDeportivo> ObtenerComplejoDeportivo(int id)
        {
            List<ComplejoDeportivo> lista = new List<ComplejoDeportivo>();

            using (SqlConnection cnx = new SqlConnection(connectionString))
            {
                cnx.Open();
                try
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Listar_Complejo_Id", cnx))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@IdComplejoDeportivo", id);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ComplejoDeportivo cd = new ComplejoDeportivo();
                                cd.IdComplejoDeportivo = Convert.ToInt32(reader.GetValue(0).ToString());
                                cd.IdSedeOlimpica = Convert.ToInt32(reader.GetValue(1).ToString());
                                cd.Localizacion = reader.GetValue(2).ToString();
                                cd.JefeOrganizacion = reader.GetValue(3).ToString();
                                cd.AreaTotal = Convert.ToInt32(reader.GetValue(4).ToString());
                                cd.Presupuesto = Convert.ToDecimal(reader.GetValue(5).ToString());
                                lista.Add(cd);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    lista = null;
                }
            }

            return lista;
        }

        public bool InsertarComplejoDeportivo(ComplejoDeportivo complejoDeportivo)
        {
            bool estado = false;

            using (SqlConnection cnx = new SqlConnection(connectionString))
            {
                cnx.Open();

                try
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Insertar_Complejo_Deportivo", cnx))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@IdSedeOlimpica", complejoDeportivo.IdSedeOlimpica);
                        cmd.Parameters.AddWithValue("@Localizacion", complejoDeportivo.Localizacion);
                        cmd.Parameters.AddWithValue("@JefeOrganizacion", complejoDeportivo.JefeOrganizacion);
                        cmd.Parameters.AddWithValue("@AreaTotal", complejoDeportivo.AreaTotal);
                        cmd.Parameters.AddWithValue("@Presupuesto", complejoDeportivo.Presupuesto);

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

        public bool ActualizarComplejoDeportivo(ComplejoDeportivo complejoDeportivo)
        {
            bool estado = false;

            using (SqlConnection cnx = new SqlConnection(connectionString))
            {
                cnx.Open();

                try
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Actualizar_Complejo_Deportivo", cnx))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@IdComplejoDeportivo", complejoDeportivo.IdComplejoDeportivo);
                        cmd.Parameters.AddWithValue("@IdSedeOlimpica", complejoDeportivo.IdSedeOlimpica);
                        cmd.Parameters.AddWithValue("@Localizacion", complejoDeportivo.Localizacion);
                        cmd.Parameters.AddWithValue("@JefeOrganizacion", complejoDeportivo.JefeOrganizacion);
                        cmd.Parameters.AddWithValue("@AreaTotal", complejoDeportivo.AreaTotal);
                        cmd.Parameters.AddWithValue("@Presupuesto", complejoDeportivo.Presupuesto);

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

        public bool EliminarComplejoDeportivo(int IdComplejoDeportivo)
        {
            bool estado = false;

            using (SqlConnection cnx = new SqlConnection(connectionString))
            {
                cnx.Open();

                try
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Eliminar_Complejo_Deportivo", cnx))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@IdComplejoDeportivo", IdComplejoDeportivo);

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

        public DataTable ObtenerValidacionSede(int id)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection cnx = new SqlConnection(connectionString))
            {
                cnx.Open();
                try
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Validar_Sede_Complejo", cnx))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@IdSedeOlimpica", id);

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

    }
}
