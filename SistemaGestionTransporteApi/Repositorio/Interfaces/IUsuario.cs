using SistemaGestionTransporteApi.Models;

namespace SistemaGestionTransporteApi.Repositorio.Interfaces
{
    public interface IUsuario
    {
        IEnumerable<Usuario> getUsuarios();
        Usuario getUsuario(long id);
        Usuario login(Usuario usuario);       
        string insertUsuarioCliente(Usuario usuario);
    }
}
