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

		public IEnumerable<VentaPasaje> getVentaPasajes()
		{
			List<VentaPasaje> lista = new List<VentaPasaje>();
			using (SqlConnection cn = new SqlConnection(cadena))
			{
				cn.Open();
				SqlCommand cmd = new SqlCommand("SELECT * FROM venta_pasaje", cn);
				SqlDataReader dr = cmd.ExecuteReader();
				while (dr.Read())
				{
					lista.Add(new VentaPasaje
					{
						id_venta = dr.GetInt32(0),
						estado = dr.GetString(1),
						fecha_venta = dr.GetDateTime(2),
						total = Convert.ToSingle(dr.GetDouble(3)),
						id_usuario = dr.GetInt64(4),
						numero = dr.GetString(5)
					});
				}
				dr.Close();
			}
			return lista;
		}

		public VentaPasaje getVentaPasaje(int id)
		{
			return getVentaPasajes().FirstOrDefault(v => v.id_venta == id);
		}

		public string insertVentaPasaje(VentaPasaje venta)
		{
			string mensaje = "";
			using (SqlConnection cn = new SqlConnection(cadena))
			{
				try
				{
					SqlCommand cmd = new SqlCommand("usp_insertar_venta_pasaje", cn);
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("@fecha_venta", venta.fecha_venta);
					cmd.Parameters.AddWithValue("@total", venta.total);
					cmd.Parameters.AddWithValue("@id_usuario", venta.id_usuario);
					cmd.Parameters.AddWithValue("@numero", venta.numero);
					cn.Open();
					int i = cmd.ExecuteNonQuery();
					mensaje = $"Se ha registrado {i} venta(s).";
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
