using Models;
using System.Collections.Generic;
using System.Data;

namespace Aplication.Interfaces
{
    public interface IUsuarioService
    {
        int RegistrarUsuario(Usuario usuario);
        DataTable ObtenerPuestos();
        string ValidarUsuario(Usuario usuario, string email, string contraseña);
        List<Usuario> ListarUsuarios();
        void EstadoBloqueoUsuario(string email);
        void DesbloquearUsuario(string email);
        Usuario ObtenerUsuarioPorEmail(string email);
    }
}