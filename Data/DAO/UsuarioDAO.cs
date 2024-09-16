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
                    { "@IdPuesto", (int)usuario.Puesto },
                    { "@Area", usuario.Area },
                    { "@FechaIngreso", usuario.FechaIngreso },
                    { "@Estado", usuario.Estado }
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

        public Usuario ValidarUsuarioContraseña(string email, string contraseña)
        {
            try
            {
                Dictionary<string, object> parametros = new Dictionary<string, object>
                {
                    { "@Email", email },
                    { "@Contraseña", contraseña }
                };

                var resultado = _acceso.ExecuteStoredProcedureReader("sp_s_validarUsuarioContraseña", parametros);

                if (resultado.Tables[0].Rows.Count == 0)
                {
                    return null;
                }

                var usuario = new Usuario()
                {
                    Id = (int)resultado.Tables[0].Rows[0]["Id"],
                    Nombre = resultado.Tables[0].Rows[0]["Nombre"].ToString(),
                    Apellido = resultado.Tables[0].Rows[0]["Apellido"].ToString(),
                    Email = resultado.Tables[0].Rows[0]["Email"].ToString(),
                    Puesto = (Models.Enums.Puesto)resultado.Tables[0].Rows[0]["IdPuesto"],
                    Area = resultado.Tables[0].Rows[0]["Area"].ToString(),
                    FechaIngreso = (DateTime)resultado.Tables[0].Rows[0]["FechaIngreso"],
                    Estado = (int)resultado.Tables[0].Rows[0]["Estado"]
                };

                return usuario;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet ListarUsuarios()
        {
            try
            {
                var resultado = _acceso.ExecuteStoredProcedureReader("sp_s_usuarios", null);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void EstadoBloqueoUsuario(string email)
        {
            try
            {
                Dictionary<string, object> parametros = new Dictionary<string, object>
                {
                    { "@Email", email }
                };

                _acceso.ExecuteStoredProcedureReader("sp_u_EstadoUsuarioBloqueo", parametros);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DesbloquearUsuario(string email)
        {
            try
            {
                Dictionary<string, object> parametros = new Dictionary<string, object>
                {
                    { "@Email", email }
                };

                _acceso.ExecuteStoredProcedureReader("sp_u_DesbloquearUsuario", parametros);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet ObtenerUsuarioPorEmail(string email)
        {
            try
            {
                Dictionary<string, object> parametros = new Dictionary<string, object>
                {
                    { "@Email", email }
                };

                var resultado = _acceso.ExecuteStoredProcedureReader("sp_s_UsuarioEmail", parametros);

                if (resultado.Tables[0].Rows.Count == 0)
                {
                    return null;
                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ActualizarContraseña(string email, string contraseña)
        {
            try
            {
                Dictionary<string, object> parametros = new Dictionary<string, object>
                {
                    { "@email", email },
                    { "@contraseña", contraseña }
                };

                var resultado = _acceso.ExecuteStoredProcedureReader("sp_u_usuarioContraseña", parametros);

                if (resultado.Tables[0].Rows.Count == 0)
                {
                    return false;
                }

                return true;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}