using Microsoft.Data.SqlClient;
using SistemaGestionTransporteApi.Models;
using SistemaGestionTransporteApi.Repositorio.Interfaces;
using System.Data;

namespace SistemaGestionTransporteApi.Repositorio.DAO
{
    public class DestinoDAO : IDestino
    {
        private readonly string cadena;

        public DestinoDAO()
        {
            cadena = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build()
                .GetConnectionString("sql");
        }

        public IEnumerable<Destino> getDestinos()
        {
            List<Destino> lista = new List<Destino>();
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM destino", cn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new Destino
                    {
                        IdDestino = dr.GetInt32(0),
                        nombre = dr.GetString(1),
                        imagen = dr.IsDBNull(2) ? null : dr.GetString(2)
                    });
                }
                dr.Close();
            }
            return lista;
        }

        public Destino getDestino(int id)
        {
            return getDestinos().FirstOrDefault(d => d.IdDestino == id);
        }

        public string insertDestino(Destino destino)
        {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_insertar_destino", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombre_des", destino.nombre);
                    cmd.Parameters.AddWithValue("@imagen", string.IsNullOrEmpty(destino.imagen) ? DBNull.Value : destino.imagen);
                    cn.Open();
                    int i = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha registrado {i} destino(s).";
                }
                catch (Exception ex)
                {
                    mensaje = ex.Message;
                }
                finally
                {
                    cn.Close();
                }
            }
            return mensaje;
        }

        public string updateDestino(Destino destino)
        {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_actualizar_destino", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_destino", destino.IdDestino);
                    cmd.Parameters.AddWithValue("@nombre_des", destino.nombre);
                    cmd.Parameters.AddWithValue("@imagen", string.IsNullOrEmpty(destino.imagen) ? DBNull.Value : destino.imagen);
                    cn.Open();
                    int i = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha actualizado {i} destino(s).";
                }
                catch (Exception ex)
                {
                    mensaje = ex.Message;
                }
                finally
                {
                    cn.Close();
                }
            }
            return mensaje;
        }

        public string deleteDestino(int id)
        {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_eliminar_destino", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_destino", id);
                    cn.Open();
                    int i = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha eliminado {i} destino(s).";
                }
                catch (Exception ex)
                {
                    mensaje = ex.Message;
                }
                finally
                {
                    cn.Close();
                }
            }
            return mensaje;
        }
    }
}
