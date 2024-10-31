using System;

namespace Models
{
    public class Compra
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public DateTime FechaPago { get; set; }
        public decimal Total { get; set; }
    }
}