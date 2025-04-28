using SistemaGestionTransporteApi.Models;

namespace SistemaGestionTransporteApi.Repositorio.Interfaces
{
	public interface IVentaPasaje
	{
		IEnumerable<VentaPasaje> getVentaPasajes();
		VentaPasaje getVentaPasaje(int id);
		string insertVentaPasaje(VentaPasaje venta);
	}
}
