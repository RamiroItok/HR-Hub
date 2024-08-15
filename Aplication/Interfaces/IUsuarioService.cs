using Models;
using Models.Enums;
using System.Collections.Generic;
using System.Data;

namespace Aplication.Interfaces
{
    public interface IUsuarioService
    {
        int RegistrarUsuario(Usuario usuario);
        //List<Puesto> ObtenerPuestos();
        DataTable ObtenerPuestos();
    }
}
