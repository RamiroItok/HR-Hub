using Models;
using System.Data;

namespace Data.Interfaces
{
    public interface IUsuarioDAO
    {
        int RegistrarUsuario(Usuario usuario);
        DataSet ObtenerPuestos();
        DataSet ListarUsuarios();
        void EstadoBloqueoUsuario(string email);
        void DesbloquearUsuario(string email);
        Usuario ObtenerUsuarioPorEmail(string email);
    }
}