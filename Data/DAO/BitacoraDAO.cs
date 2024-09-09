using Data.Conexion;
using Data.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace Data.DAO
{
    public class BitacoraDAO : IBitacoraDAO
    {
        private readonly Acceso _acceso;

        public BitacoraDAO()
        {
            _acceso = Acceso.GetInstance;
        }

        public int RegistrarBitacora(Bitacora bitacora)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@Email", bitacora.Email },
                    { "@TipoUsuario", bitacora.TipoUsuario },
                    { "@Descripcion", bitacora.Descripcion },
                    { "@Fecha", bitacora.Fecha },
                    { "@Criticidad", bitacora.Criticidad },
                    { "@DVH", 0 }
                };

                DataSet resultado = _acceso.ExecuteStoredProcedureReader("sp_i_bitacora", parameters);

                return Convert.ToInt32(resultado.Tables[0].Rows[0]["Id"]);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool BajaBitacora(string fechaIni, string fechaFin)
        {
            throw new NotImplementedException();
        }

        public DataSet ListarEventos()
        {
            try
            {
                DataSet resultado = _acceso.ExecuteStoredProcedureReader("sp_s_bitacora", null);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}