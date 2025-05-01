using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaGestionTransporteApi.Models;
using SistemaGestionTransporteApi.Repositorio.DAO;

namespace SistemaGestionTransporteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalAPIController : ControllerBase
    {
        [HttpGet("getPersonales")]
        public async Task<ActionResult<List<Personal>>> getPersonales()
        {
            var lista = await Task.Run(() => new PersonalDAO().getPersonales());
            return Ok(lista);
        }

        [HttpGet("getPersonal/{id}")]
        public async Task<ActionResult<Personal>> getPersonal(int id)
        {
            var destino = await Task.Run(() => new PersonalDAO().getPersonal(id));
            return Ok(destino);
        }

        [HttpPost("insertPersonal")]
        public async Task<ActionResult<string>> insertPersonal(Personal reg)
        {
            var mensaje = await Task.Run(() => new PersonalDAO().insertPersonal(reg));
            return Ok(mensaje);
        }

        [HttpPut("updatePersonal")]
        public async Task<ActionResult<string>> updatePersonal(Personal reg)
        {
            var mensaje = await Task.Run(() => new PersonalDAO().updatePersonal(reg));
            return Ok(mensaje);
        }

        [HttpDelete("deletePersonal/{id}")]
        public async Task<ActionResult<string>> deleteDestino(int id)
        {
            var mensaje = await Task.Run(() => new PersonalDAO().deletePersonal(id));
            return Ok(mensaje);
        }
    }
}
