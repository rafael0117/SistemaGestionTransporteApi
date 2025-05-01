using Microsoft.Data.SqlClient;
using SistemaGestionTransporteApi.Models;
using SistemaGestionTransporteApi.Repositorio.Interfaces;
using System.Data;

namespace SistemaGestionTransporteApi.Repositorio.DAO
{
    public class ViajeDAO : IViaje
    {
        private readonly string cadena;

        public ViajeDAO()
        {
            cadena = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build()
                .GetConnectionString("sql");
        }

        public IEnumerable<Viaje> getViajes()
        {
            List<Viaje> lista = new List<Viaje>();
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM viaje", cn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new Viaje
                    {
                        IdViaje = dr.GetInt32(0),
                        IdBus = dr.GetInt32(1),
                        IdDestino = dr.GetInt32(2),
                        fechaSalida = dr.GetDateTime(3),
                        fechaLlegada = dr.GetDateTime(4),
                        incidencias = dr.IsDBNull(5) ? null : dr.GetString(5),
                        precio = dr.GetDouble(6)
                    });
                }
                dr.Close();
            }
            return lista;
        }

        public Viaje getViaje(int id)
        {
            return getViajes().FirstOrDefault(v => v.IdViaje == id);
        }

        public string insertViaje(Viaje viaje)
        {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_insertar_viaje", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_bus", viaje.IdBus);
                    cmd.Parameters.AddWithValue("@id_destino", viaje.IdDestino);
                    cmd.Parameters.AddWithValue("@fech_sal", viaje.fechaSalida);
                    cmd.Parameters.AddWithValue("@fech_lle", viaje.fechaLlegada);
                    cmd.Parameters.AddWithValue("@incidencias", string.IsNullOrEmpty(viaje.incidencias) ? DBNull.Value : viaje.incidencias);
                    cmd.Parameters.AddWithValue("@precio", viaje.precio);
                    cn.Open();
                    int i = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha registrado {i} viaje(s).";
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

        public string updateViaje(Viaje viaje)
        {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_actualizar_viaje", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_viaje", viaje.IdViaje);
                    cmd.Parameters.AddWithValue("@id_bus", viaje.IdBus);
                    cmd.Parameters.AddWithValue("@id_destino", viaje.IdDestino);
                    cmd.Parameters.AddWithValue("@fech_sal", viaje.fechaSalida);
                    cmd.Parameters.AddWithValue("@fech_lle", viaje.fechaLlegada);
                    cmd.Parameters.AddWithValue("@incidencias", string.IsNullOrEmpty(viaje.incidencias) ? DBNull.Value : viaje.incidencias);
                    cmd.Parameters.AddWithValue("@precio", viaje.precio);
                    cn.Open();
                    int i = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha actualizado {i} viaje(s).";
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

        public string deleteViaje(int id)
        {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_eliminar_viaje", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_viaje", id);
                    cn.Open();
                    int i = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha eliminado {i} viaje(s).";
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
        public IEnumerable<Viaje> getViajesPorDestino(int idDestino)
        {
            List<Viaje> lista = new List<Viaje>();

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_viajes_por_destino", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idDestino", idDestino);

                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Viaje
                    {
                        IdViaje = dr.GetInt32(0),
                        IdBus = dr.GetInt32(1),
                        IdDestino = dr.GetInt32(2),
                        NombreDestino = dr.GetString(3), // Aquí asumo que estás devolviendo el nombre del destino en la consulta
                        Imagen = dr.GetString(4), // Aquí asumo que estás devolviendo la imagen del destino
                        fechaSalida = dr.GetDateTime(5),
                        fechaLlegada = dr.GetDateTime(6),
                        incidencias = dr.IsDBNull(7) ? null : dr.GetString(7),
                        precio = dr.GetDouble(8)
                    });
                }
                dr.Close();
            }

            return lista;
        }

    }
}