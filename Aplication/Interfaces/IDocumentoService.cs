using Models;
using System.Collections.Generic;

namespace Aplication.Interfaces
{
    public interface IDocumentoService
    {
        int CargarDocumento(Documento documento, Usuario userSession);
        void AsignarDocumento(int idDocumento);
        List<UsuarioDocumento> ObtenerDocumentosPorUsuario(bool firmado, Usuario userSession);
        byte[] ObtenerContenidoPorId(int idDocumento);
        void FirmarDocumento(int idDocumento, Usuario userSession);
    }
}