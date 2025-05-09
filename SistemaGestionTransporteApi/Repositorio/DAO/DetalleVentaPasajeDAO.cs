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

		
	}
}
