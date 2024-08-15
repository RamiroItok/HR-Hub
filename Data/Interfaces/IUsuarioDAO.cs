using Models;
using System.Data;

namespace Data.Interfaces
{
    public interface IUsuarioDAO
    {
        int RegistrarUsuario(Usuario usuario);
        DataSet ObtenerPuestos();
        Usuario ValidarUsuarioContraseña(string email, string contraseña);
    }
}
