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
    public class SedeAD
    {
        // ----------------------------    CADENA DE CONEXIÓN     ----------------------------------- //

        private string connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

        public List<Sede> ListarSedesOlimpicas()
        {
            List<Sede> lista = new List<Sede>();

            using (SqlConnection cnx = new SqlConnection(connectionString))
            {
                cnx.Open();
                try
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Listar_Sede_Olimpica", cnx))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Sede sede = new Sede();
                                sede.IdSedeOlimpica = Convert.ToInt32(reader.GetValue(0).ToString());
                                sede.Nombre = reader.GetValue(1).ToString();
                                sede.Ubicacion = reader.GetValue(2).ToString();
                                sede.NumeroComplejo = Convert.ToInt32(reader.GetValue(3).ToString());
                                sede.Presupuesto = Convert.ToDecimal(reader.GetValue(4).ToString());
                                lista.Add(sede);
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

        public List<Sede> ObtenerSede(int id)
        {
            List<Sede> lista = new List<Sede>();

            using (SqlConnection cnx = new SqlConnection(connectionString))
            {
                cnx.Open();
                try
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Listar_Sede_Id", cnx))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@IdSedeOlimpica", id);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Sede sede = new Sede();
                                sede.IdSedeOlimpica = Convert.ToInt32(reader.GetValue(0).ToString());
                                sede.Nombre = reader.GetValue(1).ToString();
                                sede.Ubicacion = reader.GetValue(2).ToString();
                                sede.NumeroComplejo = Convert.ToInt32(reader.GetValue(3).ToString());
                                sede.Presupuesto = Convert.ToDecimal(reader.GetValue(4).ToString());
                                lista.Add(sede);
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

        public bool InsertarSedeOlimpica(Sede sede)
        {
            bool estado = false;

            using (SqlConnection cnx = new SqlConnection(connectionString))
            {
                cnx.Open();

                try
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Insertar_Sede_Olimpica", cnx))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Nombre", sede.Nombre);
                        cmd.Parameters.AddWithValue("@Ubicacion", sede.Ubicacion);
                        cmd.Parameters.AddWithValue("@NumeroComplejo", sede.NumeroComplejo);
                        cmd.Parameters.AddWithValue("@Presupuesto", sede.Presupuesto);

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

        public bool ActualizarSedeOlimpica(Sede sede)
        {
            bool estado = false;

            using (SqlConnection cnx = new SqlConnection(connectionString))
            {
                cnx.Open();

                try
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Actualizar_Sede_Olimpica", cnx))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@IdSedeOlimpica", sede.IdSedeOlimpica);
                        cmd.Parameters.AddWithValue("@Nombre", sede.Nombre);
                        cmd.Parameters.AddWithValue("@Ubicacion", sede.Ubicacion);
                        cmd.Parameters.AddWithValue("@NumeroComplejo", sede.NumeroComplejo);
                        cmd.Parameters.AddWithValue("@Presupuesto", sede.Presupuesto);

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

        public bool EliminarSedeOlimpica(int IdSedeOlimpica)
        {
            bool estado = false;

            using (SqlConnection cnx = new SqlConnection(connectionString))
            {
                cnx.Open();

                try
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Eliminar_Sede_Olimpica", cnx))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@IdSedeOlimpica", IdSedeOlimpica);

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
