using Models;

namespace Aplication.Interfaces
{
    public interface IEmpresaService
    {
        int Registrar(Empresa empresa, Usuario userSession);
        int Modificar(Empresa empresa, Usuario userSession);
        void Eliminar(Empresa empresa, Usuario userSession);
    }
}