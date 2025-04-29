using SistemaGestionTransporteApi.Models;

namespace SistemaGestionTransporteApi.Repositorio.Interfaces
{
    public interface IDestino
    {
        IEnumerable<Destino> getDestinos();
        Destino getDestino(int id);
        string insertDestino(Destino destino);
        string updateDestino(Destino destino);
        string deleteDestino(int id);
    }
}
