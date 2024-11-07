using Models;

namespace Aplication.Interfaces
{
    public interface IDocumentoService
    {
        int CargarDocumento(Documento documento, Usuario userSession);
        void AsignarDocumento(int idDocumento);
    }
}