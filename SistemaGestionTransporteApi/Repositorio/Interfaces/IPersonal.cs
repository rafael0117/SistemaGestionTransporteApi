using SistemaGestionTransporteApi.Models;

namespace SistemaGestionTransporteApi.Repositorio.Interfaces
{
    public interface IPersonal
    {
        IEnumerable<Personal> getPersonales();
        Personal getPersonal(int id);
        string insertPersonal(Personal revisionBus);
        string updatePersonal(Personal revisionBus);
        string deletePersonal(int id);
    }
}
