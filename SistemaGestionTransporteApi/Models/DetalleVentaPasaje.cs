namespace SistemaGestionTransporteApi.Models
{
	public class DetalleVentaPasaje
	{
		public int id_detalle { get; set; }
		public int cantidad { get; set; }
		public float precio { get; set; }
		public float total { get; set; }
		public int id_venta { get; set; }
		public int id_viaje { get; set; }
	}
}
