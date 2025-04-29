using SistemaGestionTransporteApi.Models;

namespace SistemaGestionTransporteApi.Repositorio.Interfaces
{
    public interface IViaje
    {
        IEnumerable<Viaje> getViajes();
        Viaje getViaje(int id);
        string insertViaje(Viaje viaje);
        string updateViaje(Viaje viaje);
        string deleteViaje(int id);
    }
}
