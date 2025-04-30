using SistemaGestionTransporteApi.Models;

namespace SistemaGestionTransporteApi.Repositorio.Interfaces
{
    public interface IRol
    {
        IEnumerable<Rol> getRoles();
        Rol getRol(int id);
    }
}
