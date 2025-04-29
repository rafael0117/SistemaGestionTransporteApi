using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaGestionTransporteApi.Models;
using SistemaGestionTransporteApi.Repositorio.DAO;

namespace SistemaGestionTransporteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DestinoAPIController : ControllerBase
    {
        [HttpGet("getDestinos")]
        public async Task<ActionResult<List<Destino>>> getDestinos()
        {
            var lista = await Task.Run(() => new DestinoDAO().getDestinos());
            return Ok(lista);
        }

        [HttpGet("getDestino/{id}")]
        public async Task<ActionResult<Destino>> getDestino(int id)
        {
            var destino = await Task.Run(() => new DestinoDAO().getDestino(id));
            return Ok(destino);
        }

        [HttpPost("insertDestino")]
        public async Task<ActionResult<string>> insertDestino(Destino reg)
        {
            var mensaje = await Task.Run(() => new DestinoDAO().insertDestino(reg));
            return Ok(mensaje);
        }

        [HttpPut("updateDestino")]
        public async Task<ActionResult<string>> updateDestino(Destino reg)
        {
            var mensaje = await Task.Run(() => new DestinoDAO().updateDestino(reg));
            return Ok(mensaje);
        }

        [HttpDelete("deleteDestino/{id}")]
        public async Task<ActionResult<string>> deleteDestino(int id)
        {
            var mensaje = await Task.Run(() => new DestinoDAO().deleteDestino(id));
            return Ok(mensaje);
        }
    }
}
