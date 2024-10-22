using Models;
using System.Data;

namespace Data.Interfaces
{
    public interface IUsuarioDAO
    {
        int RegistrarUsuario(Usuario usuario);
        int ModificarUsuario(Usuario usuario);
        DataSet ObtenerPuestos();
        DataSet ObtenerAreas();
        DataSet ListarUsuarios();
        void EstadoBloqueoUsuario(string email);
        void DesbloquearUsuario(string email);
        DataSet ObtenerUsuarioPorEmail(string email);
        bool ActualizarContraseña(string email, string contraseña);
    }
}