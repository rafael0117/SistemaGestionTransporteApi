using Firebase.Auth;
using Firebase.Storage;
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
        public async Task<ActionResult<string>> InsertDestino([FromForm] string nombre,[FromForm] IFormFile imagen)
        {
            if (imagen == null || imagen.Length == 0)
            {
                return BadRequest("No se ha proporcionado una imagen");
            }

            string extension = Path.GetExtension(imagen.FileName).ToLowerInvariant();
            string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };

            if (!allowedExtensions.Contains(extension))
            {
                return BadRequest("El archivo debe ser una imagen válida");
            }

            if (imagen.Length > 5 * 1024 * 1024)
            {
                return BadRequest("La imagen no puede exceder los 5MB");
            }

            var destino = new Destino { nombre = nombre };
            var mensaje = await new DestinoDAO().insertDestino(destino, imagen);
            return Ok(mensaje);
        }


        [HttpPut("updateDestino")]
        public async Task<ActionResult<string>> UpdateDestino([FromForm] Destino reg, [FromForm] IFormFile imagen)
        {
            // Validar si la imagen es nula
            if (imagen != null)
            {
                // Obtener la extensión del archivo
                string extension = Path.GetExtension(imagen.FileName).ToLowerInvariant();

                // Definir extensiones permitidas
                string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };

                // Validar que la extensión sea una de las permitidas
                if (!allowedExtensions.Contains(extension))
                {
                    return BadRequest("El archivo debe ser una imagen (.jpg, .jpeg, .png, .gif)");
                }

                // Validar el tamaño de la imagen (máximo 5MB)
                if (imagen.Length > 5 * 1024 * 1024)
                {
                    return BadRequest("La imagen no puede exceder 5MB");
                }
            }

            // Aquí se puede procesar la actualización del destino
            var mensaje = await new DestinoDAO().updateDestino(reg, imagen);

            // Retornar el mensaje de éxito o error
            if (!string.IsNullOrEmpty(mensaje))
            {
                return Ok(mensaje);  // Si todo está bien, retornamos un OK con el mensaje.
            }
            else
            {
                return BadRequest("Ocurrió un error al actualizar el destino.");
            }
        }


        [HttpDelete("deleteDestino/{id}")]
        public async Task<ActionResult<string>> deleteDestino(int id)
        {
            var mensaje = await Task.Run(() => new DestinoDAO().deleteDestino(id));
            return Ok(mensaje);
        }


      
    }
}
