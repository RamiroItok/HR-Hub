using Data.Conexion;
using Data.Interfaces;
using Models;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DAO
{
    public class DocumentoDAO : IDocumentoDAO
    {
        private readonly Acceso _acceso;

        public DocumentoDAO()
        {
            _acceso = Acceso.GetInstance;
        }

        public void AsignarDocumento(int idDocumento, int idUsuario)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@IdDocumento", idDocumento },
                    { "@IdUsuario", idUsuario },
                    { "@FechaEntrega", DateTime.Now },
                    { "@FechaFirma", null },
                    { "@Firmado", (int)EstadoFirma.NoFirmado }
                };

                _acceso.ExecuteStoredProcedureReader("sp_i_usuarioDocumento", parameters);
            }
            catch
            {
                throw new Exception("Error al asignar documento.");
            }
        }

        public int CargarDocumento(Documento documento)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@Nombre", documento.Nombre },
                    { "@TipoArchivo", documento.TipoArchivo },
                    { "@Contenido", documento.Contenido },
                    { "@FechaCarga", documento.FechaCarga }
                };

                DataSet resultado = _acceso.ExecuteStoredProcedureReader("sp_i_documento", parameters);

                return Convert.ToInt32(resultado.Tables[0].Rows[0]["Id"]);
            }
            catch
            {
                throw new Exception("Error al cargar documento.");
            }
        }

        public void FirmarDocumento(int idDocumento, int idUsuario)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@IdDocumento", idDocumento },
                    { "@IdUsuario", idUsuario },
                    { "@FechaFirma", DateTime.Now },
                    { "@Firmado", (int)EstadoFirma.Firmado }
                };

                _acceso.ExecuteStoredProcedureReader("sp_u_usuarioDocumento_firma", parameters);
            }
            catch
            {
                throw new Exception("Error al firmar documento.");
            }
        }

        public DataSet ObtenerContenidoPorId(int idDocumento)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@Id", idDocumento }
                };

                return _acceso.ExecuteStoredProcedureReader("sp_s_documento_porId", parameters);
            }
            catch
            {
                throw new Exception("Error al obtener el contenido del documento.");
            }
        }

        public DataSet ObtenerDocumentosPorUsuario(bool firmado, int idUsuario)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@Firmado", firmado },
                    { "@IdUsuario", idUsuario }
                };

                return _acceso.ExecuteStoredProcedureReader("sp_s_usuarioDocumento_porUsuario", parameters);
            }
            catch
            {
                throw new Exception("Error al obtener documentos del usuario.");
            }
        }
    }
}
