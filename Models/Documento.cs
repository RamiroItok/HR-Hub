using System;

namespace Models
{
    public class Documento
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string TipoArchivo { get; set; }
        public byte[] Contenido { get; set; }
        public DateTime FechaCarga { get; set; }
        public int DVH { get; set; }
    }
}