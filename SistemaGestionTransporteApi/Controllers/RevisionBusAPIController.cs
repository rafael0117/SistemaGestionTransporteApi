using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaGestionTransporteApi.Models;
using SistemaGestionTransporteApi.Repositorio.DAO;

namespace SistemaGestionTransporteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RevisionBusAPIController : ControllerBase
    {
        [HttpGet("getRevisionBuses")]
        public async Task<ActionResult<List<RevisionBus>>> getRevisionBuses()
        {
            var lista = await Task.Run(() => new RevisionBusDAO().getRevisionBuses());
            return Ok(lista);
        }

        [HttpGet("getRevisionBus/{id}")]
        public async Task<ActionResult<RevisionBus>> getRevisionBus(int id)
        {
            var destino = await Task.Run(() => new RevisionBusDAO().getRevisionBus(id));
            return Ok(destino);
        }

        [HttpPost("insertRevisionBus")]
        public async Task<ActionResult<string>> insertRevisionBus(RevisionBus reg)
        {
            var mensaje = await Task.Run(() => new RevisionBusDAO().insertRevisionBus(reg));
            return Ok(mensaje);
        }

        [HttpPut("updateRevisionBus")]
        public async Task<ActionResult<string>> updateRevisionBus(RevisionBus reg)
        {
            var mensaje = await Task.Run(() => new RevisionBusDAO().updateRevisionBus(reg));
            return Ok(mensaje);
        }

        [HttpDelete("deleteRevisionBus/{id}")]
        public async Task<ActionResult<string>> deleteRevisionBus(int id)
        {
            var mensaje = await Task.Run(() => new RevisionBusDAO().deleteRevisionBus(id));
            return Ok(mensaje);
        }
    }
}
