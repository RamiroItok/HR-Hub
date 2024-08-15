using Aplication.Interfaces;
using Data.Interfaces;
using Models;
using System;
using System.Data;

namespace Aplication
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioDAO _usuarioDAO;

        public UsuarioService(IUsuarioDAO usuarioDAO)
        {
            _usuarioDAO = usuarioDAO;
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
                    Puesto = usuario.Puesto
                };

                var idUsuario = _usuarioDAO.RegistrarUsuario(usuarioReal);
                return idUsuario;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable ObtenerPuestos()
        {
            var resultado = _usuarioDAO.ObtenerPuestos();

            return resultado.Tables[0];
        }

        public Usuario ValidarUsuarioContraseña(string email, string contraseña)
        {
            var resultado = _usuarioDAO.ValidarUsuarioContraseña(email, contraseña);
            return resultado;
        }
    }
}
