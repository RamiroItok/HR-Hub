using Models.Enums;
using System;

namespace Models
{
    public class UsuarioDocumento
    {
        public Documento Documento { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime FechaEntrega { get; set; }
        public DateTime? FechaFirma { get; set; }
        public EstadoFirma Firmado { get; set; }
    }
}