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
                    { "@Firmado", (int)EstadoFima.NoFirmado }
                };

                _acceso.ExecuteStoredProcedureReader("sp_i_usuarioDocumento", parameters);
            }
            catch
            {
                throw new Exception("Error al realizar la copia.");
            }
        }

        public int CargarDocumento(Documento documento)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@Nombre", documento.Nombre },
                    { "@TipoArchivo", documento.TipoArchivo},
                    { "@Contenido", documento.Contenido },
                    { "@FechaCarga", documento.FechaCarga }
                };

                DataSet resultado = _acceso.ExecuteStoredProcedureReader("sp_i_documento", parameters);

                return Convert.ToInt32(resultado.Tables[0].Rows[0]["Id"]);
            }
            catch
            {
                throw new Exception("Error al realizar la copia.");
            }
        }
    }
}
