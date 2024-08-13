using Aplication.Interfaces;
using Data.Interfaces;
using Models;
using System;

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
                var idUsuario = _usuarioDAO.RegistrarUsuario(usuario);
                return idUsuario;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
