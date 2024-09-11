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
                /*Usuario usuarioReal = new Usuario()
                {
                    Nombre = EncriptacionService.Encriptar_AES(usuario.Nombre),
                    Apellido = EncriptacionService.Encriptar_AES(usuario.Apellido),
                    Email = EncriptacionService.Encriptar_AES(usuario.Email),
                    Contraseña = EncriptacionService.Encriptar_MD5(usuario.Contraseña),
                    Puesto = usuario.Puesto
                };*/

                Usuario usuarioReal = new Usuario()
                {
                    Nombre = usuario.Nombre,
                    Apellido = usuario.Apellido,
                    Email = usuario.Email,
                    Contraseña = usuario.Contraseña,
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

                var contraseñaReal = usuario.Contraseña;
                string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*(),.?""':{}|<>]).+$";

                if(contraseña.Length < 8 || !Regex.IsMatch(contraseña, pattern))
                    return "La contraseña no posee el formato correcto.";

                if(usuario.Estado == 3)
                    return "El usuario está bloqueado. Contacte un administrador para su desbloqueo.";

                if (contraseñaReal != contraseña)
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
                        Nombre = row["Nombre"].ToString(),
                        Apellido = row["Apellido"].ToString(),
                        Email = row["Email"].ToString(),
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
                //email = EncriptacionService.Encriptar_AES(email);
                var resultado = _usuarioDAO.ObtenerUsuarioPorEmail(email);
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}