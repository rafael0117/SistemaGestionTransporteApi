namespace SistemaGestionTransporteApi.Models
{
    public class Viaje
    {
        public int IdViaje { get; set; }
        public int IdBus { get; set; }
        public int IdDestino { get; set; }
        public string NombreDestino { get; set; }
        public string Imagen { get; set; }
        public DateTime fechaSalida { get; set; }
        public DateTime fechaLlegada { get; set; }
        public string incidencias { get; set; }
        public double precio { get; set; }
    }
}
