using SistemaGestionTransporteApi.Models;

namespace SistemaGestionTransporteApi.Repositorio.Interfaces
{
    public interface IRevisionBus
    {
        IEnumerable<RevisionBus> getRevisionBuses();
        RevisionBus getRevisionBus(int id);
        string insertRevisionBus(RevisionBus revisionBus);
        string updateRevisionBus(RevisionBus revisionBus);
        string deleteRevisionBus(int id);
    }
}
