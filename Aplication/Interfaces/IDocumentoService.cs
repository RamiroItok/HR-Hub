using Models;
using System.Collections.Generic;
using System.Data;

namespace Aplication.Interfaces
{
    public interface IDocumentoService
    {
        int CargarDocumento(Documento documento, Usuario userSession);
        void AsignarDocumento(int idDocumento);
        void AsignarDocumentosAUsuario(int idUsuario);
        List<Documento> ObtenerDocumentos();
        List<UsuarioDocumento> ObtenerDocumentosPorUsuario(bool firmado, Usuario userSession);
        byte[] ObtenerContenidoPorId(int idDocumento);
        void FirmarDocumento(int idDocumento, Usuario userSession);
        void QuitarDocumentosAUsuario(int idUsuario);
        DataSet ObtenerPorcentajeFirmasPorDocumento();
    }
}