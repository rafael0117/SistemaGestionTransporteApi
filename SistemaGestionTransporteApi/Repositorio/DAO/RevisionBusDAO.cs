using Microsoft.Data.SqlClient;
using SistemaGestionTransporteApi.Models;
using SistemaGestionTransporteApi.Repositorio.Interfaces;
using System.Data;

namespace SistemaGestionTransporteApi.Repositorio.DAO
{
    public class RevisionBusDAO : IRevisionBus
    {
        private readonly string cadena;

        public RevisionBusDAO()
        {
            cadena = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build()
                .GetConnectionString("sql");
        }

        public IEnumerable<RevisionBus> getRevisionBuses()
        {
            List<RevisionBus> lista = new List<RevisionBus>();
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM revision", cn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new RevisionBus
                    {
                        IdRevision = dr.GetInt32(0),
                        FechaRevision = dr.GetDateTime(1),
                        Observaciones = dr.IsDBNull(2) ? null : dr.GetString(2),
                        Resultado = dr.GetString(3),
                        TipoRevision = dr.GetString(4),
                        IdBus = dr.GetInt32(5)
                    });
                }
                dr.Close();
            }
            return lista;
        }

        public RevisionBus getRevisionBus(int id)
        {
            return getRevisionBuses().FirstOrDefault(r => r.IdRevision == id);
        }

        public string insertRevisionBus(RevisionBus revisionBus)
        {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_insertar_revision_bus", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@fecha_revision", revisionBus.FechaRevision);
                    cmd.Parameters.AddWithValue("@observaciones", string.IsNullOrEmpty(revisionBus.Observaciones) ? DBNull.Value : revisionBus.Observaciones);
                    cmd.Parameters.AddWithValue("@resultado", revisionBus.Resultado);
                    cmd.Parameters.AddWithValue("@tipo_revision", revisionBus.TipoRevision);
                    cmd.Parameters.AddWithValue("@idbus", revisionBus.IdBus);
                    cn.Open();
                    int i = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha registrado {i} revisión(es) de bus.";
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

        public string updateRevisionBus(RevisionBus revisionBus)
        {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_actualizar_revision_bus", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@revision_id", revisionBus.IdRevision);
                    cmd.Parameters.AddWithValue("@fecha_revision", revisionBus.FechaRevision);
                    cmd.Parameters.AddWithValue("@observaciones", string.IsNullOrEmpty(revisionBus.Observaciones) ? DBNull.Value : revisionBus.Observaciones);
                    cmd.Parameters.AddWithValue("@resultado", revisionBus.Resultado);
                    cmd.Parameters.AddWithValue("@tipo_revision", revisionBus.TipoRevision);
                    cmd.Parameters.AddWithValue("@id_bus", revisionBus.IdBus);
                    cn.Open();
                    int i = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha actualizado {i} revisión(es) de bus.";
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

        public string deleteRevisionBus(int id)
        {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_eliminar_revision_bus", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@revision_id", id);
                    cn.Open();
                    int i = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha eliminado {i} revisión(es) de bus.";
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
