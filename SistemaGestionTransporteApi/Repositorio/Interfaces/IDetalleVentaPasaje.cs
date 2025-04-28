using SistemaGestionTransporteApi.Models;

namespace SistemaGestionTransporteApi.Repositorio.Interfaces
{
	public interface IDetalleVentaPasaje
	{
		IEnumerable<DetalleVentaPasaje> getDetalleVentaPasajes();
		IEnumerable<DetalleVentaPasaje> getDetalleVentaPasajesByVenta(int idVenta);
		string insertDetalleVentaPasaje(DetalleVentaPasaje detalle);
	}
}
