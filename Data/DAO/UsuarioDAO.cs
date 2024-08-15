using Data.Conexion;
using Data.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace Data.DAO
{
    public class UsuarioDAO : IUsuarioDAO 
    {
        private readonly Acceso _acceso;

        public UsuarioDAO()
        {
            _acceso = Acceso.GetInstance;
        }

        public int RegistrarUsuario(Usuario usuario)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@Nombre", usuario.Nombre },
                    { "@Apellido", usuario.Apellido },
                    { "@Email", usuario.Email },
                    { "@Contraseña", usuario.Contraseña },
                    { "@IdPuesto", (int)usuario.Puesto }
                };

                DataSet resultado = _acceso.ExecuteStoredProcedureReader("sp_i_usuario", parameters);

                return Convert.ToInt32(resultado.Tables[0].Rows[0]["Id"]);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet ObtenerPuestos()
        {
            try
            {
                DataSet resultado = _acceso.ExecuteStoredProcedureReader("sp_s_puesto", null);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
