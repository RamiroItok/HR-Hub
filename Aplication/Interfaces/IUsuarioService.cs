using Models;
using Models.Enums;
using System.Collections.Generic;
using System.Data;

namespace Aplication.Interfaces
{
    public interface IUsuarioService
    {
        int RegistrarUsuario(Usuario usuario);
        DataTable ObtenerPuestos();
        Usuario ValidarUsuarioContraseña(string email, string contraseña);
    }
}
