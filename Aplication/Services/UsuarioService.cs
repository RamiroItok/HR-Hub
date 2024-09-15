using Aplication.Interfaces;
using Data.Interfaces;
using Models;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;

namespace Aplication
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioDAO _usuarioDAO;
        private readonly IDigitoVerificadorService _iDigitoVerificadorService;
        private readonly IBitacoraService _iBitacoraService;

        public UsuarioService(IUsuarioDAO usuarioDAO, IDigitoVerificadorService digitoVerificadorService, IBitacoraService bitacoraService)
        {
            _usuarioDAO = usuarioDAO;
            _iDigitoVerificadorService = digitoVerificadorService;
            _iBitacoraService = bitacoraService;
        }

        public int RegistrarUsuario(Usuario usuario)
        {
            try
            {
                Usuario usuarioReal = new Usuario()
                {
                    Nombre = EncriptacionService.Encriptar_AES(usuario.Nombre),
                    Apellido = EncriptacionService.Encriptar_AES(usuario.Apellido),
                    Email = EncriptacionService.Encriptar_AES(usuario.Email),
                    Contraseña = EncriptacionService.Encriptar_MD5(usuario.Contraseña),
                    Puesto = usuario.Puesto,
                    Area = usuario.Area,
                    FechaIngreso = usuario.FechaIngreso,
                    Estado = 0
                };

                var idUsuario = _usuarioDAO.RegistrarUsuario(usuarioReal);

                _iDigitoVerificadorService.CalcularDVTabla("Usuario");

                return idUsuario;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable ObtenerPuestos()
        {
            try
            {
                var resultado = _usuarioDAO.ObtenerPuestos();

                return resultado.Tables[0];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string ValidarUsuario(Usuario usuario, string email, string contraseña)
        {
            try
            {
                if(usuario == null)
                    return "El email no se ha dado de alta en el sistema.";

                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(contraseña))
                    return "Hay campos sin completar.";

                var contraseñaReal = EncriptacionService.Encriptar_MD5(contraseña);

                if(contraseña.Length < 8 || !ValidarFormatoContraseña(contraseña))
                    return "La contraseña no posee el formato correcto.";
                
                if(usuario.Estado == 3)
                    return "El usuario está bloqueado. Contacte un administrador para su desbloqueo.";

                if (contraseñaReal != usuario.Contraseña)
                {
                    EstadoBloqueoUsuario(usuario.Email);
                    return "La contraseña es incorrecta.";
                }
                else
                {
                    DesbloquearUsuario(usuario.Email);
                    _iBitacoraService.AltaBitacora(usuario.Email, usuario.Puesto, "Inicio de sesion", Criticidad.MEDIA);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ValidarFormatoContraseña(string contraseña)
        {
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*(),.?""':{}|<>]).+$";

            if(!Regex.IsMatch(contraseña, pattern))
                return false;

            return true;
        }

        public List<Usuario> ListarUsuarios()
        {
            try
            {
                var resultado = _usuarioDAO.ListarUsuarios();

                List<Usuario> listaUsuarios = new List<Usuario>();

                foreach (DataRow row in resultado.Tables[0].Rows)
                {
                    Usuario usuario = new Usuario
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        Nombre = EncriptacionService.Decrypt_AES(row["Nombre"].ToString()),
                        Apellido = EncriptacionService.Decrypt_AES(row["Apellido"].ToString()),
                        Email = EncriptacionService.Decrypt_AES(row["Email"].ToString()),
                        Puesto = (Puesto)Enum.Parse(typeof(Puesto), row["IdPuesto"].ToString()),
                        Area = row["Area"].ToString(),
                        FechaIngreso = (DateTime)row["FechaIngreso"]
                    };

                    listaUsuarios.Add(usuario);
                }

                return listaUsuarios;
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
                _usuarioDAO.EstadoBloqueoUsuario(email);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DesbloquearUsuario(string email)
        {
            try
            {
                _usuarioDAO.DesbloquearUsuario(email);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Usuario ObtenerUsuarioPorEmail(string email)
        {
            try
            {
                email = EncriptacionService.Encriptar_AES(email);
                var resultado = _usuarioDAO.ObtenerUsuarioPorEmail(email);

                var usuario = new Usuario()
                {
                    Id = (int)resultado.Tables[0].Rows[0]["Id"],
                    Nombre = resultado.Tables[0].Rows[0]["Nombre"].ToString(),
                    Apellido = resultado.Tables[0].Rows[0]["Apellido"].ToString(),
                    Email = resultado.Tables[0].Rows[0]["Email"].ToString(),
                    Contraseña = resultado.Tables[0].Rows[0]["Contraseña"].ToString(),
                    Puesto = (Models.Enums.Puesto)resultado.Tables[0].Rows[0]["IdPuesto"],
                    Area = resultado.Tables[0].Rows[0]["Area"].ToString(),
                    FechaIngreso = (DateTime)resultado.Tables[0].Rows[0]["FechaIngreso"],
                    Estado = (int)resultado.Tables[0].Rows[0]["Estado"]
                };

                return usuario;
            }
            catch (Exception ex)
            {
                var mensaje = "";
                if (ex.Message.Contains("SQL") || ex.Message.Contains("BD"))
                {
                    mensaje = "Se ha perdido la conexión con la base de datos. Vuelva a intentar en unos minutos";
                }
                else
                    mensaje = ex.Message;

                throw new Exception(mensaje);
            }
        }
    }
}