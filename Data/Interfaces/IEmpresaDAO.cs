using Models;
using System.Data;

namespace Data.Interfaces
{
    public interface IEmpresaDAO
    {
        int Registrar(Empresa empresa);
        void Eliminar(int idEmpresa);
        int Modificar(Empresa empresa);
        DataSet ObtenerEmpresas();
        DataSet ObtenerEmpresaPorId(int id);
    }
}