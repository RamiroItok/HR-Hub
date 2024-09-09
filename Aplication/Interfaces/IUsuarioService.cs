using Models;
using System.Collections.Generic;
using System.Data;

namespace Aplication.Interfaces
{
    public interface IUsuarioService
    {
        int RegistrarUsuario(Usuario usuario);
        DataTable ObtenerPuestos();
        Usuario ValidarUsuarioContraseña(string email, string contraseña);
        List<Usuario> ListarUsuarios();
        string ValidarCampos(string usuario, string contraeña);
    }
}