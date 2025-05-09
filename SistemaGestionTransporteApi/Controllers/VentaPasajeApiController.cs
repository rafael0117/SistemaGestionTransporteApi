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
        [HttpPost("registrar")]
        public async Task<ActionResult<int>> RegistrarVenta([FromBody] VentaPasaje venta)
        {
            try
            {
                var idVenta = await Task.Run(() => new VentaPasajeDAO().RegistrarVenta(venta));
                return Ok(idVenta);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al registrar venta: {ex.Message}");
            }
        }
        [HttpGet("obtenerporid/{id}")]
        public async Task<ActionResult<VentaPasaje>> ObtenerVentaPorId(int id)
        {
            try
            {
                var venta = await Task.Run(() => new VentaPasajeDAO().ObtenerVentaPorId(id));

                if (venta == null)
                    return NotFound("Venta no encontrada.");

                return Ok(venta);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener venta: {ex.Message}");
            }
        }

    }
}
