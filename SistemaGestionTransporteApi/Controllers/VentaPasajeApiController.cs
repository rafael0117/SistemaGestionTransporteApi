using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaGestionTransporteApi.Models;
using SistemaGestionTransporteApi.Repositorio.DAO;

namespace SistemaGestionTransporteApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class VentaPasajeAPIController : ControllerBase
	{
		[HttpGet("getVentaPasajes")]
		public async Task<ActionResult<List<VentaPasaje>>> getVentaPasajes()
		{
			var lista = await Task.Run(() => new VentaPasajeDAO().getVentaPasajes());
			return Ok(lista);
		}

		[HttpGet("getVentaPasaje/{id}")]
		public async Task<ActionResult<VentaPasaje>> getVentaPasaje(int id)
		{
			var venta = await Task.Run(() => new VentaPasajeDAO().getVentaPasaje(id));
			return Ok(venta);
		}

		[HttpPost("insertVentaPasaje")]
		public async Task<ActionResult<string>> insertVentaPasaje(VentaPasaje reg)
		{
			var mensaje = await Task.Run(() => new VentaPasajeDAO().insertVentaPasaje(reg));
			return Ok(mensaje);
		}


		// ================================================
		// Métodos para DETALLE de venta
		// ================================================

		[HttpGet("getDetalleVentaPasajes")]
		public async Task<ActionResult<List<DetalleVentaPasaje>>> getDetalleVentaPasajes()
		{
			var lista = await Task.Run(() => new DetalleVentaPasajeDAO().getDetalleVentaPasajes());
			return Ok(lista);
		}

		[HttpGet("getDetalleVentaPasaje/{id}")]
		public async Task<ActionResult<DetalleVentaPasaje>> getDetalleVentaPasaje(int id)
		{
			var detalle = await Task.Run(() => new DetalleVentaPasajeDAO().getDetalleVentaPasajesByVenta(id));
			return Ok(detalle);
		}

		[HttpPost("insertDetalleVentaPasaje")]
		public async Task<ActionResult<string>> insertDetalleVentaPasaje(DetalleVentaPasaje reg)
		{
			var mensaje = await Task.Run(() => new DetalleVentaPasajeDAO().insertDetalleVentaPasaje(reg));
			return Ok(mensaje);
		}

	}
}
