using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaGestionTransporteApi.Models;
using SistemaGestionTransporteApi.Repositorio.DAO;

namespace SistemaGestionTransporteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioAPIController : ControllerBase
    {
        [HttpGet("getUsuarios")]
        public async Task<ActionResult<List<Usuario>>> getUsuarios()
        {
            var lista = await Task.Run(() => new UsuarioDAO().getUsuarios());
            return Ok(lista);
        }

        [HttpGet("getUsuario/{id}")]
        public async Task<ActionResult<Usuario>> getUsuario(int id)
        {
            var usuario = await Task.Run(() => new UsuarioDAO().getUsuario(id));
            if (usuario == null)
            {
                return NotFound("Usuario no encontrado.");
            }
            return Ok(usuario);
        }

        [HttpPost("insertUsuarioCliente")]
        public async Task<ActionResult<string>> insertUsuarioCliente(Usuario reg)
        {
            var mensaje = await Task.Run(() => new UsuarioDAO().insertUsuarioCliente(reg));
            return Ok(mensaje);
        }
        [HttpPost("login")]
        public async Task<ActionResult<object>> login(Usuario credenciales)
        {
            var usuario = await Task.Run(() => new UsuarioDAO().login(credenciales));

            if (usuario == null)
            {
                return Unauthorized(new { mensaje = "Credenciales inválidas." });
            }

            return Ok(new
            {
                mensaje = "Bienvenido",
                usuario = new
                {
                    usuario.IdUsuario,
                    usuario.Nombres,
                    usuario.Apellidos,
                    usuario.Username,
                    usuario.Correo,
                    usuario.Direccion,
                    usuario.IdRol
                }
            });
        }



    }
}
