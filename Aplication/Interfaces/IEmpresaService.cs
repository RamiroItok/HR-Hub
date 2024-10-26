using Models;
using System.Data;

namespace Aplication.Interfaces
{
    public interface IEmpresaService
    {
        int Registrar(Empresa empresa, Usuario userSession);
        int Modificar(Empresa empresa, Usuario userSession);
        void Eliminar(Empresa empresa, Usuario userSession);
        DataTable ObtenerEmpresas();
        Empresa ObtenerEmpresaPorId(int id);
    }
}