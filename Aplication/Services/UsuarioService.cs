using Aplication.Interfaces;
using Data.Interfaces;
using Models;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

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
            catch (Exception ex) when (ex.Message.Contains("SQL") || ex.Message.Contains("BD"))
            {
                throw new Exception("Se ha perdido la conexión con la base de datos. Vuelva a intentar en unos minutos");
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

                return CompletarUsuario(resultado);
            }
            catch (Exception ex) when (ex.Message.Contains("SQL") || ex.Message.Contains("BD"))
            {
                throw new Exception("Se ha perdido la conexión con la base de datos. Vuelva a intentar en unos minutos");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string GenerarContraseña()
        {
            const string minusculas = "abcdefghijklmnopqrstuvwxyz";
            const string mayusculas = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string numeros = "1234567890";
            const string caracteresEspeciales = "!@#$%^&*()";
            const string todosCaracteres = minusculas + mayusculas + numeros + caracteresEspeciales;

            StringBuilder contraseña = new StringBuilder();
            Random random = new Random();

            contraseña.Append(minusculas[random.Next(minusculas.Length)]);
            contraseña.Append(mayusculas[random.Next(mayusculas.Length)]);
            contraseña.Append(numeros[random.Next(numeros.Length)]);
            contraseña.Append(caracteresEspeciales[random.Next(caracteresEspeciales.Length)]);

            for (int i = contraseña.Length; i < 10; i++)
            {
                contraseña.Append(todosCaracteres[random.Next(todosCaracteres.Length)]);
            }

            return MezclarCaracteres(contraseña.ToString());
        }

        private string MezclarCaracteres(string contraseña)
        {
            char[] array = contraseña.ToCharArray();
            Random random = new Random();
            for (int i = array.Length - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                char temp = array[i];
                array[i] = array[j];
                array[j] = temp;
            }
            return new string(array);
        }

        public bool ActualizarContraseña(Usuario usuario, string contraseña, TipoOperacionContraseña tipoOperacion)
        {
            var contraseñaEncriptada = EncriptacionService.Encriptar_MD5(contraseña);
            var resultado = _usuarioDAO.ActualizarContraseña(usuario.Email, contraseñaEncriptada);

            var descripcion = tipoOperacion == TipoOperacionContraseña.Recuperacion ? "Recuperacion de contraseña" : "Cambio de contraseña";

            if (resultado)
                _iBitacoraService.AltaBitacora(usuario.Email, usuario.Puesto, descripcion, Criticidad.ALTA);

            return resultado;
        }

        public void EnviarMail(string email, string contraseña)
        {
            try
            {
                MailMessage mensaje = new MailMessage();
                mensaje.From = new MailAddress("noreply@hrhub.com", "HR Hub");
                mensaje.To.Add(email);
                mensaje.Subject = "Recuperación de contraseña";

                string body = File.ReadAllText(HttpContext.Current.Server.MapPath("~/Templates/RecuperarContraseñaTemplate.html"));

                body = body.Replace("{{CONTRASEÑA}}", contraseña);

                mensaje.Body = body;
                mensaje.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.Port = 587;
                smtp.Credentials = new System.Net.NetworkCredential("noreply.hrhub@gmail.com", "wjfjskqjkcoaxmhm");
                smtp.EnableSsl = true;
                smtp.Send(mensaje);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private Usuario CompletarUsuario(DataSet tabla)
        {
            var usuario = new Usuario()
            {
                Id = (int)tabla.Tables[0].Rows[0]["Id"],
                Nombre = tabla.Tables[0].Rows[0]["Nombre"].ToString(),
                Apellido = tabla.Tables[0].Rows[0]["Apellido"].ToString(),
                Email = tabla.Tables[0].Rows[0]["Email"].ToString(),
                Contraseña = tabla.Tables[0].Rows[0]["Contraseña"].ToString(),
                Puesto = (Models.Enums.Puesto)tabla.Tables[0].Rows[0]["IdPuesto"],
                Area = tabla.Tables[0].Rows[0]["Area"].ToString(),
                FechaIngreso = (DateTime)tabla.Tables[0].Rows[0]["FechaIngreso"],
                Estado = (int)tabla.Tables[0].Rows[0]["Estado"]
            };

            return usuario;
        }

        public string ValidarContraseñas(Usuario usuario, string contraseñaActual, string contraseñaNueva, string confirmarContraseña)
        {
            var contraseñaEncriptada = EncriptacionService.Encriptar_MD5(contraseñaActual);

            if(usuario.Contraseña != contraseñaEncriptada)
            {
                return "La contraseña actual es incorrecta";
            }

            if (contraseñaNueva != confirmarContraseña)
                return "La nueva contraseña y la confirmación no coinciden";

            if (contraseñaNueva.Length < 8 || !ValidarFormatoContraseña(contraseñaNueva))
                return "La contraseña no posee el formato correcto.";

            return null;
        }
    }
}