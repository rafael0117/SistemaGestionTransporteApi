using Microsoft.Data.SqlClient;
using SistemaGestionTransporteApi.Models;
using SistemaGestionTransporteApi.Repositorio.Interfaces;
using System.Data;

namespace SistemaGestionTransporteApi.Repositorio.DAO
{
    public class BusDAO : IBus
    {
        private readonly string cadena;

        public BusDAO()
        {
            cadena = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build()
                .GetConnectionString("sql");
        }

        public IEnumerable<Bus> getBuses()
        {
            List<Bus> lista = new List<Bus>();
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM bus", cn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new Bus
                    {
                        IdBus = dr.GetInt32(0),
                        modelo = dr.GetString(1),
                        marca = dr.GetString(2),
                        anio = dr.GetInt32(3),
                        capacidad = dr.GetInt32(4),
                        placa = dr.GetString(5)
                    });
                }
                dr.Close();
            }
            return lista;
        }

        public Bus getBus(int id)
        {
            return getBuses().FirstOrDefault(b => b.IdBus == id);
        }

        public string insertBus(Bus bus)
        {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_insertar_bus", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@modelo", bus.modelo);
                    cmd.Parameters.AddWithValue("@marca", bus.marca);
                    cmd.Parameters.AddWithValue("@anio", bus.anio);
                    cmd.Parameters.AddWithValue("@capacidad", bus.capacidad);
                    cmd.Parameters.AddWithValue("@placa", bus.placa);
                    cn.Open();
                    int i = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha registrado {i} bus(es).";
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

        public string updateBus(Bus bus)
        {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_actualizar_bus", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_bus", bus.IdBus);
                    cmd.Parameters.AddWithValue("@modelo", bus.modelo);
                    cmd.Parameters.AddWithValue("@marca", bus.marca);
                    cmd.Parameters.AddWithValue("@anio", bus.anio);
                    cmd.Parameters.AddWithValue("@capacidad", bus.capacidad);
                    cmd.Parameters.AddWithValue("@placa", bus.placa);
                    cn.Open();
                    int i = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha actualizado {i} bus(es).";
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

        public string deleteBus(int id)
        {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_eliminar_bus", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_Bus", id);
                    cn.Open();
                    int i = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha eliminado {i} bus(es).";
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
