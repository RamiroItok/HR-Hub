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
        bool ValidarFormatoContraseña(string contraseña);
        string GenerarContraseña();
        bool ActualizarContraseña(Usuario usuario, string contraseña);
        void EnviarMail(string email, string contraeña);
    }
}