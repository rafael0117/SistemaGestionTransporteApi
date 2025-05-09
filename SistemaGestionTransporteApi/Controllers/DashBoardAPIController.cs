using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaGestionTransporteApi.Repositorio.DAO;

namespace SistemaGestionTransporteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashBoardAPIController : ControllerBase
    {
        [HttpGet("resumen")]
        public IActionResult Dashboard()
        {
            int totalUsuarios = new UsuarioDAO().getUsuarios().Count();
            int totalViajes = new ViajeDAO().getViajes().Count();
            int totalBuses = new BusDAO().getBuses().Count();

            var resumen = new
            {
                totalUsuarios,
                totalViajes,
                totalBuses,
       
            };

            return Ok(resumen);
        }

    }
}
