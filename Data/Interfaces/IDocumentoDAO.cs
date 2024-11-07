using Models;

namespace Data.Interfaces
{
    public interface IDocumentoDAO
    {
        int CargarDocumento(Documento documento);
        void AsignarDocumento(int idDocumento, int idUsuario);
    }
}