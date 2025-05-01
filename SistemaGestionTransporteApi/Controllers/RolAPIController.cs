using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaGestionTransporteApi.Models;
using SistemaGestionTransporteApi.Repositorio.DAO;

namespace SistemaGestionTransporteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolAPIController : ControllerBase
    {
        [HttpGet("getRoles")]
        public async Task<ActionResult<List<Rol>>> getRoles()
        {
            var lista = await Task.Run(() => new RolDAO().getRoles());
            return Ok(lista);
        }

        [HttpGet("getRol/{id}")]
        public async Task<ActionResult<Rol>> getRol(int id)
        {
            var usuario = await Task.Run(() => new RolDAO().getRol(id));
            if (usuario == null)
            {
                return NotFound("Usuario no encontrado.");
            }
            return Ok(usuario);
        }
    }
}
