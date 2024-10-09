using Models.Composite;
using Models.Enums;
using System;
using System.Collections.Generic;

namespace Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Contraseña { get; set; }
        public Puesto Puesto { get; set; }
        public Area Area { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Genero { get; set; }
        public DateTime FechaIngreso { get; set; }
        public int Estado { get; set; }
        public int DVH { get; set; }

        List<Componente> _permisos = new List<Componente>();
        public List<Componente> Permisos
        {
            get
            {
                return _permisos;
            }
        }
    }
}
