using Models;
using System.Data;

namespace Data.Interfaces
{
    public interface IDocumentoDAO
    {
        int CargarDocumento(Documento documento);
        void AsignarDocumento(int idDocumento, int idUsuario);
        DataSet ObtenerDocumentosPorUsuario(bool firmado, int idUsuario);
        DataSet ObtenerContenidoPorId(int idDocumento);
        void FirmarDocumento(int idDocumento, int idUsuario);
    }
}