using Microsoft.Data.SqlClient;
using SistemaGestionTransporteApi.Models;
using System.Data;
using SistemaGestionTransporteApi.Repositorio.Interfaces;

namespace SistemaGestionTransporteApi.Repositorio.DAO
{
	public class DetalleVentaPasajeDAO : IDetalleVentaPasaje
	{

		private readonly string cadena;

		public DetalleVentaPasajeDAO()
		{
			cadena = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json")
				.Build()
				.GetConnectionString("sql");
		}

		public IEnumerable<DetalleVentaPasaje> getDetalleVentaPasajes()
		{
			List<DetalleVentaPasaje> lista = new List<DetalleVentaPasaje>();
			using (SqlConnection cn = new SqlConnection(cadena))
			{
				cn.Open();
				SqlCommand cmd = new SqlCommand("SELECT * FROM detalle_venta_pasaje", cn);
				SqlDataReader dr = cmd.ExecuteReader();
				while (dr.Read())
				{
					lista.Add(new DetalleVentaPasaje
					{
						id_detalle = dr.GetInt32(0),
						cantidad = dr.GetInt32(1),
						precio = Convert.ToSingle(dr.GetDouble(2)),
						total = Convert.ToSingle(dr.GetDouble(3)),
						id_venta = dr.GetInt32(4),
						id_viaje = dr.GetInt32(5)
					});
				}
				dr.Close();
			}
			return lista;
		}

		public IEnumerable<DetalleVentaPasaje> getDetalleVentaPasajesByVenta(int idVenta)
		{
			return getDetalleVentaPasajes().Where(d => d.id_venta == idVenta);
		}

		public string insertDetalleVentaPasaje(DetalleVentaPasaje detalle)
		{
			string mensaje = "";
			using (SqlConnection cn = new SqlConnection(cadena))
			{
				try
				{
					SqlCommand cmd = new SqlCommand("usp_insertar_detalle_venta_pasaje", cn);
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("@cantidad", detalle.cantidad);
					cmd.Parameters.AddWithValue("@precio", detalle.precio);
					cmd.Parameters.AddWithValue("@total", detalle.total);
					cmd.Parameters.AddWithValue("@id_venta", detalle.id_venta);
					cmd.Parameters.AddWithValue("@id_viaje", detalle.id_viaje);
					cn.Open();
					int i = cmd.ExecuteNonQuery();
					mensaje = $"Se ha registrado {i} detalle(s) de venta.";
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
