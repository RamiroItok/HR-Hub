using Models.Composite;
using System.Collections.Generic;

namespace Models.DTOs
{
    public class UsuarioDTO
    {
        List<Componente> _permisos;
        public UsuarioDTO()
        {
            _permisos = new List<Componente>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        //public IIdioma Idioma { get; set; }
        public List<Componente> Permisos
        {
            get
            {
                return _permisos;
            }
        }

        public static UsuarioDTO FillObject(Usuario usuario)
        {
            UsuarioDTO usuarioDTO = new UsuarioDTO()
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Email = usuario.Email
            };

            return usuarioDTO;
        }

        public static List<UsuarioDTO> FillListDTO(List<Usuario> usuarios)
        {
            List<UsuarioDTO> usuarioDTO = new List<UsuarioDTO>();
            foreach (Usuario usuario in usuarios)
            {
                usuarioDTO.Add(FillObject(usuario));
            }
            return usuarioDTO;
        }
    }
}
