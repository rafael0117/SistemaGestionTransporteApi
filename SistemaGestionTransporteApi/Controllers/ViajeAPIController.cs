using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaGestionTransporteApi.Models;
using SistemaGestionTransporteApi.Repositorio.DAO;

namespace SistemaGestionTransporteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViajeAPIController : ControllerBase
    {
        [HttpGet("getViajes")]
        public async Task<ActionResult<List<Viaje>>> getViajes()
        {
            var lista = await Task.Run(() => new ViajeDAO().getViajes());
            return Ok(lista);
        }

        [HttpGet("getViaje/{id}")]
        public async Task<ActionResult<Viaje>> getViaje(int id)
        {
            var viaje = await Task.Run(() => new ViajeDAO().getViaje(id));
            return Ok(viaje);
        }

        [HttpPost("insertViaje")]
        public async Task<ActionResult<string>> insertViaje(Viaje reg)
        {
            var mensaje = await Task.Run(() => new ViajeDAO().insertViaje(reg));
            return Ok(mensaje);
        }

        [HttpPut("updateViaje")]
        public async Task<ActionResult<string>> updateViaje(Viaje reg)
        {
            var mensaje = await Task.Run(() => new ViajeDAO().updateViaje(reg));
            return Ok(mensaje);
        }

        [HttpDelete("deleteViaje/{id}")]
        public async Task<ActionResult<string>> deleteViaje(int id)
        {
            var mensaje = await Task.Run(() => new ViajeDAO().deleteViaje(id));
            return Ok(mensaje);
        }
    }
}
