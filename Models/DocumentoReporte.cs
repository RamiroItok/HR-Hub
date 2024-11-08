namespace Models
{
    public class DocumentoReporte
    {
        public int IdDocumento { get; set; }
        public string NombreDocumento { get; set; }
        public int TotalAsignados { get; set; }
        public int TotalFirmados { get; set; }
        public int TotalNoFirmados { get; set; }
        public decimal PorcentajeFirmado { get; set; }
        public decimal PorcentajeNoFirmado { get; set; }
    }
}