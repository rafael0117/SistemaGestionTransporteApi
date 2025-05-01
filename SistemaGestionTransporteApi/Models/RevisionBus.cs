namespace SistemaGestionTransporteApi.Models
{
    public class RevisionBus
    {
        public int IdRevision { get; set; }
        public int IdBus { get; set; }
        public DateTime FechaRevision { get; set; }
        public string TipoRevision { get; set; }
        public string Resultado { get; set; }
        public string Observaciones { get; set; }
    }
}
