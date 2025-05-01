using SistemaGestionTransporteApi.Models;

namespace SistemaGestionTransporteApi.Repositorio.Interfaces
{
    public interface IDestino
    {
        IEnumerable<Destino> getDestinos();
        Destino getDestino(int id);
        Task<string> insertDestino(Destino destino, IFormFile imagen);
        Task<string> updateDestino(Destino destino, IFormFile imagen);

		string deleteDestino(int id);
        Task<string> SubirStorage(Stream archivo, string nombre);
    }
}
