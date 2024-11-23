using System;

namespace Models
{
    public class Bitacora
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string TipoUsuario { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public string Criticidad { get; set; }
    }
}