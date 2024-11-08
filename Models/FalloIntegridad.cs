using System.Collections.Generic;

namespace Models
{
    public class FalloIntegridad
    {
        public bool Fallo { get; set; }
        public List<string> Tablas { get; set; }
    }
}