using Aplication.Interfaces;
using Data.Interfaces;
using Models;
using Models.Enums;
using System;
using System.Collections.Generic;
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
                    Puesto = usuario.Puesto,
                    Area = usuario.Area,
                    FechaIngreso = usuario.FechaIngreso,
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

        public Usuario ValidarUsuarioContraseña(string email, string contraseña)
        {
            try
            {
                var resultado = _usuarioDAO.ValidarUsuarioContraseña(email, contraseña);
                return resultado;
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
    }
}
