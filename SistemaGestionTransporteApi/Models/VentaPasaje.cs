namespace SistemaGestionTransporteApi.Models
{
	public class VentaPasaje
	{
		public int id_venta { get; set; }
		public string estado { get; set; }
		public DateTime fecha_venta { get; set; }
		public float total { get; set; }
		public long id_usuario { get; set; }
		public string numero { get; set; }
	}
}
