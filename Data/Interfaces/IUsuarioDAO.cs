using Models;
using System.Data;

namespace Data.Interfaces
{
    public interface IUsuarioDAO
    {
        int RegistrarUsuario(Usuario usuario);
        DataSet ObtenerPuestos();
    }
}
