using SistemaGestionTransporteApi.Models;

namespace SistemaGestionTransporteApi.Repositorio.Interfaces
{
    public interface IRol
    {
        List<Rol> getRoles();
        Rol getRol(int id);
    }
}
