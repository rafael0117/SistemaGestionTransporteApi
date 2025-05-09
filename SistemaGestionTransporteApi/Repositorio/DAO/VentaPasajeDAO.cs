using Microsoft.Data.SqlClient;
using SistemaGestionTransporteApi.Models;
using System.Data;
using SistemaGestionTransporteApi.Repositorio.Interfaces;

namespace SistemaGestionTransporteApi.Repositorio.DAO
{
	public class VentaPasajeDAO : IVentaPasaje
	{

		private readonly string cadena;

		public VentaPasajeDAO()
		{
			cadena = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json")
				.Build()
				.GetConnectionString("sql");
		}
        public int RegistrarVenta(VentaPasaje venta)
        {
            int idVenta = 0;

            using (SqlConnection conn = new SqlConnection(cadena))
            {
                conn.Open();

                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // Insertar venta
                        using (SqlCommand cmd = new SqlCommand("sp_insertar_venta_pasaje", conn, transaction))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@estado", venta.Estado);
                            cmd.Parameters.AddWithValue("@fecha_venta", venta.FechaVenta);
                            cmd.Parameters.AddWithValue("@total", venta.Total);
                            cmd.Parameters.AddWithValue("@id_usuario", venta.IdUsuario);
                            cmd.Parameters.AddWithValue("@numero", venta.Numero);

                            SqlParameter outputId = new SqlParameter("@id_venta", SqlDbType.Int)
                            {
                                Direction = ParameterDirection.Output
                            };
                            cmd.Parameters.Add(outputId);

                            cmd.ExecuteNonQuery();
                            idVenta = (int)outputId.Value;
                        }

                        // Insertar detalles
                        foreach (var detalle in venta.Detalles)
                        {
                            using (SqlCommand cmd = new SqlCommand("sp_insertar_detalle_venta_pasaje", conn, transaction))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@id_venta", idVenta);
                                cmd.Parameters.AddWithValue("@id_viaje", detalle.IdViaje);
                                cmd.Parameters.AddWithValue("@cantidad", detalle.Cantidad);
                                cmd.Parameters.AddWithValue("@precio", detalle.Precio);
                                cmd.Parameters.AddWithValue("@total", detalle.Total);

                                cmd.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }

            return idVenta;
        }
        public VentaPasaje ObtenerVentaPorId(int idVenta)
        {
            VentaPasaje venta = null;

            using (SqlConnection conn = new SqlConnection(cadena))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("sp_obtener_venta_pasaje_por_id", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_venta", idVenta);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            venta = new VentaPasaje
                            {
                                IdVenta = Convert.ToInt32(reader["id_venta"]),
                                Estado = reader["estado"].ToString(),
                                FechaVenta = Convert.ToDateTime(reader["fecha_venta"]),
                                Total = Convert.ToDecimal(reader["total"]),
                                IdUsuario = Convert.ToInt32(reader["id_usuario"]),
                                Numero = reader["numero"].ToString(),
                                Detalles = new List<DetalleVentaPasaje>()
                            };
                        }

                        if (reader.NextResult())
                        {
                            while (reader.Read())
                            {
                                var detalle = new DetalleVentaPasaje
                                {
                                    IdDetalle = Convert.ToInt32(reader["id_detalle"]),
                                    IdVenta = Convert.ToInt32(reader["id_venta"]),
                                    IdViaje = Convert.ToInt32(reader["id_viaje"]),
                                    Cantidad = Convert.ToInt32(reader["cantidad"]),
                                    Precio = Convert.ToDecimal(reader["precio"]),
                                    Total = Convert.ToDecimal(reader["total"])
                                };

                                venta.Detalles.Add(detalle);
                            }
                        }
                    }
                }
            }

            return venta;
        }


    }
}
