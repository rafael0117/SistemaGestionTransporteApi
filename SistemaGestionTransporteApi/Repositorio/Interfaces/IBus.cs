using SistemaGestionTransporteApi.Models;

namespace SistemaGestionTransporteApi.Repositorio.Interfaces
{
    public interface IBus
    {
        IEnumerable<Bus> getBuses();
        Bus getBus(int id);
        string insertBus(Bus bus);
        string updateBus(Bus bus);
        string deleteBus(int id);
    }
}
