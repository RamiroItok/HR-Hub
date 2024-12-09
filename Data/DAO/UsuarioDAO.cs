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
                    { "@IdPuesto", null },
                    { "@IdArea", (int)usuario.Area },
                    { "@FechaNacimiento", usuario.FechaNacimiento },
                    { "@Genero", usuario.Genero },
                    { "@FechaIngreso", usuario.FechaIngreso },
                    { "@Direccion", usuario.Direccion },
                    { "@NumeroDireccion", usuario.NumeroDireccion },
                    { "@Departamento", usuario.Departamento },
                    { "@CodigoPostal", usuario.CodigoPostal },
                    { "@Ciudad", usuario.Ciudad },
                    { "@Provincia", usuario.Provincia },
                    { "@Pais", usuario.Pais },
                    { "@Estado", usuario.Estado }
                };

                DataSet resultado = _acceso.ExecuteStoredProcedureReader("sp_i_usuario", parameters);

                return Convert.ToInt32(resultado.Tables[0].Rows[0]["Id"]);
            }
            catch (Exception)
            {
                throw new Exception("ErrorRegistrarUsuario");
            }
        }

        public int ModificarUsuario(Usuario usuario)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@Email", usuario.Email },
                    { "@IdPuesto", (int)usuario.Puesto },
                    { "@IdArea", (int)usuario.Area },
                    { "@Genero", usuario.Genero },
                    { "@Direccion", usuario.Direccion },
                    { "@NumeroDireccion", usuario.NumeroDireccion },
                    { "@Departamento", usuario.Departamento },
                    { "@CodigoPostal", usuario.CodigoPostal },
                    { "@Ciudad", usuario.Ciudad },
                    { "@Provincia", usuario.Provincia },
                    { "@Pais", usuario.Pais }
                };

                DataSet resultado = _acceso.ExecuteStoredProcedureReader("sp_u_usuario", parameters);

                return Convert.ToInt32(resultado.Tables[0].Rows[0]["Id"]);
            }
            catch (Exception)
            {
                throw new Exception("ErrorModificarUsuario");
            }
        }

        public void ModificarPermisoUsuario(Usuario usuario)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@Email", usuario.Email },
                    { "@IdPuesto", (int)usuario.Puesto }
                };

                DataSet resultado = _acceso.ExecuteStoredProcedureReader("sp_u_usuario_IdPuestoporEmail", parameters);

                if (resultado != null)
                {
                    Dictionary<string, object> parametero = new Dictionary<string, object>
                    {
                        { "@parPadreId", (int)usuario.Puesto }
                    };

                    var permisos = _acceso.ExecuteStoredProcedureReader("sp_s_FamiliaPatente_permisos", parametero);
                    DataTable dt = permisos.Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        Dictionary<string, object> parametero2 = new Dictionary<string, object>
                        {
                            { "@Email", usuario.Email }
                        };

                        _acceso.ExecuteStoredProcedureReader("sp_d_usuarioPermiso_porEmail", parametero2);

                        foreach (DataRow rows in dt.Rows)
                        {
                            Dictionary<string, object> parametros2 = new Dictionary<string, object>
                            {
                                { "@Email", usuario.Email },
                                { "@PatenteId", int.Parse(rows["HijoId"].ToString()) }
                            };

                            _acceso.ExecuteStoredProcedureReader("sp_i_usuarioPermiso_porPermisoyUsuario", parametros2);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("ErrorModificarPuestoUsuario");
            }
        }

        public DataSet ObtenerPuestos()
        {
            try
            {
                DataSet resultado = _acceso.ExecuteStoredProcedureReader("sp_s_puesto", null);

                return resultado;
            }
            catch (Exception)
            {
                throw new Exception("ErrorObtenerPuestos");
            }
        }

        public DataSet ObtenerAreas()
        {
            try
            {
                DataSet resultado = _acceso.ExecuteStoredProcedureReader("sp_s_area", null);

                return resultado;
            }
            catch (Exception)
            {
                throw new Exception("ErrorObtenerAreas");
            }
        }

        public DataSet ListarUsuarios()
        {
            try
            {
                return _acceso.ExecuteStoredProcedureReader("sp_s_usuarios", null);
            }
            catch (Exception)
            {
                throw new Exception("ErrorListarUsuarios");
            }
        }

        public DataSet ObtenerUsuariosBloqueados()
        {
            try
            {
                return _acceso.ExecuteStoredProcedureReader("sp_s_usuariosBloqueados", null);
            }
            catch (Exception)
            {
                throw new Exception("ErrorObtenerUsuariosBloqueados");
            }
        }

        public DataSet ObtenerEmpleados()
        {
            try
            {
                return _acceso.ExecuteStoredProcedureReader("sp_s_usuariosEmpleados", null);
            }
            catch (Exception)
            {
                throw new Exception("ErrorObtenerEmpleados");
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
            catch(Exception)
            {
                throw new Exception("ErrorBloquearUsuario");
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
            catch(Exception)
            {
                throw new Exception("ErrorDesbloquearUsuario");
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
            catch (Exception)
            {
                throw new Exception("ErrorObtenerUsuarioPorMail");
            }
        }

        public DataSet ObtenerUsuarioWebmaster()
        {
            try
            {
                var resultado = _acceso.ExecuteStoredProcedureReader("sp_s_usuarioWebMaster", null);

                if (resultado.Tables[0].Rows.Count == 0)
                {
                    return null;
                }

                return resultado;
            }
            catch (Exception)
            {
                throw new Exception("ErrorObtenerUsuarioWebmaster");
            }
        }

        public DataSet ObtenerUsuarioPorId(int id)
        {
            try
            {
                Dictionary<string, object> parametros = new Dictionary<string, object>
                {
                    { "@Id", id }
                };

                var resultado = _acceso.ExecuteStoredProcedureReader("sp_s_usuarioPorId", parametros);

                if (resultado.Tables[0].Rows.Count == 0)
                {
                    return null;
                }

                return resultado;
            }
            catch (Exception)
            {
                throw new Exception("ErrorObtenerUsuarioPorId");
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
            catch(Exception)
            {
                throw new Exception("ErrorActualizarContrasena");
            }
        }
    }
}