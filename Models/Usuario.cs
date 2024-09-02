using Models.Enums;
using System;

namespace Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Contraseña { get; set; }
        public Genero Genero { get; set; }
        public Puesto Puesto { get; set; }
        public string Area { get; set; }
        public DateTime FechaIngreso { get; set; }
    }
}
