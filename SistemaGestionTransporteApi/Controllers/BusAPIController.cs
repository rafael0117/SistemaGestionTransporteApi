using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaGestionTransporteApi.Models;
using SistemaGestionTransporteApi.Repositorio.DAO;

namespace SistemaGestionTransporteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusAPIController : ControllerBase
    {
        [HttpGet("getBuses")]
        public async Task<ActionResult<List<Bus>>> getBuses()
        {
            var lista = await Task.Run(() => new BusDAO().getBuses());
            return Ok(lista);
        }

        [HttpGet("getBus/{id}")]
        public async Task<ActionResult<Bus>> getBus(int id)
        {
            var bus = await Task.Run(() => new BusDAO().getBus(id));
            return Ok(bus);
        }

        [HttpPost("insertBus")]
        public async Task<ActionResult<string>> insertBus(Bus reg)
        {
            var mensaje = await Task.Run(() => new BusDAO().insertBus(reg));
            return Ok(mensaje);
        }

        [HttpPut("updateBus")]
        public async Task<ActionResult<string>> updateBus(Bus reg)
        {
            var mensaje = await Task.Run(() => new BusDAO().updateBus(reg));
            return Ok(mensaje);
        }

        [HttpDelete("deleteBus/{id}")]
        public async Task<ActionResult<string>> deleteBus(int id)
        {
            var mensaje = await Task.Run(() => new BusDAO().deleteBus(id));
            return Ok(mensaje);
        }
    }
}
