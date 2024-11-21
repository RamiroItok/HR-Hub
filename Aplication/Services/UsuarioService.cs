using Aplication.Interfaces;
using Data.Interfaces;
using Models;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
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
        private readonly IPermisoService _iPermisoService;

        public UsuarioService(IUsuarioDAO usuarioDAO, IDigitoVerificadorService digitoVerificadorService, IBitacoraService bitacoraService, IPermisoService permisoService)
        {
            _usuarioDAO = usuarioDAO;
            _iDigitoVerificadorService = digitoVerificadorService;
            _iBitacoraService = bitacoraService;
            _iPermisoService = permisoService;
        }

        public int RegistrarUsuario(Usuario usuario, Usuario userSession)
        {
            try
            {
                Usuario usuarioReal = new Usuario()
                {
                    Nombre = EncriptacionService.Encriptar_AES(usuario.Nombre),
                    Apellido = EncriptacionService.Encriptar_AES(usuario.Apellido),
                    Email = EncriptacionService.Encriptar_AES(usuario.Email),
                    Contraseña = EncriptacionService.Encriptar_MD5(usuario.Contraseña),
                    Area = usuario.Area,
                    FechaNacimiento = usuario.FechaNacimiento,
                    Genero = usuario.Genero,
                    FechaIngreso = usuario.FechaIngreso,
                    Direccion = usuario.Direccion,
                    NumeroDireccion = usuario.NumeroDireccion,
                    Departamento = usuario.Departamento,
                    CodigoPostal = usuario.CodigoPostal,
                    Ciudad = usuario.Ciudad,
                    Provincia = usuario.Provincia,
                    Pais = usuario.Pais,
                    Estado = 0
                };

                var idUsuario = _usuarioDAO.RegistrarUsuario(usuarioReal);
                _iBitacoraService.AltaBitacora(userSession.Email, userSession.Puesto, $"Registra el usuario {usuario.Nombre} {usuario.Apellido}", Criticidad.BAJA);
                _iDigitoVerificadorService.CalcularDVTabla("Usuario");

                return idUsuario;
            }
            catch (Exception ex) when (ex.Message.Contains("SQL") || ex.Message.Contains("BD"))
            {
                throw new Exception("ErrorBD");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int ModificarUsuario(Usuario usuario, Usuario userSession)
        {
            try
            {
                Usuario usuarioReal = new Usuario()
                {
                    Email = EncriptacionService.Encriptar_AES(usuario.Email),
                    Puesto = usuario.Puesto,
                    Area = usuario.Area,
                    Genero = usuario.Genero,
                    Direccion = usuario.Direccion,
                    NumeroDireccion = usuario.NumeroDireccion,
                    Departamento = usuario.Departamento,
                    CodigoPostal = usuario.CodigoPostal,
                    Ciudad = usuario.Ciudad,
                    Provincia = usuario.Provincia,
                    Pais = usuario.Pais
                };

                var idUsuario = _usuarioDAO.ModificarUsuario(usuarioReal);
                _iBitacoraService.AltaBitacora(userSession.Email, userSession.Puesto, $"Se modifco el usuario {usuario.Nombre} {usuario.Apellido}", Criticidad.MEDIA);
                _iDigitoVerificadorService.CalcularDVTabla("Usuario");

                return idUsuario;
            }
            catch (Exception ex) when (ex.Message.Contains("SQL") || ex.Message.Contains("BD"))
            {
                throw new Exception("ErrorBD");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ModificarPermisoUsuario(Usuario usuario, Usuario userSession)
        {
            try
            {
                Usuario usuarioReal = new Usuario()
                {
                    Email = EncriptacionService.Encriptar_AES(usuario.Email),
                    Puesto = usuario.Puesto,
                };

                _usuarioDAO.ModificarPermisoUsuario(usuarioReal);
                _iBitacoraService.AltaBitacora(userSession.Email, userSession.Puesto, $"Se modifco el puesto de {usuario.Nombre} {usuario.Apellido} a {usuario.Puesto}", Criticidad.ALTA);
                _iDigitoVerificadorService.CalcularDVTabla("Usuario");
                _iDigitoVerificadorService.CalcularDVTabla("UsuarioPermiso");
            }
            catch (Exception ex) when (ex.Message.Contains("SQL") || ex.Message.Contains("BD"))
            {
                throw new Exception("ErrorBD");
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
            catch (Exception ex) when (ex.Message.Contains("SQL") || ex.Message.Contains("BD"))
            {
                throw new Exception("ErrorBD");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable ObtenerAreas()
        {
            try
            {
                var resultado = _usuarioDAO.ObtenerAreas();

                return resultado.Tables[0];
            }
            catch (Exception ex) when (ex.Message.Contains("SQL") || ex.Message.Contains("BD"))
            {
                throw new Exception("ErrorBD");
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
                    throw new Exception("MailDadoAlta");

                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(contraseña))
                    throw new Exception("MensajeCamposIncompletos");

                var contraseñaReal = EncriptacionService.Encriptar_MD5(contraseña);

                if (usuario.Estado == 3)
                    throw new Exception("UsuarioBloqueado");

                if (contraseñaReal != usuario.Contraseña)
                {
                    EstadoBloqueoUsuario(usuario);
                    throw new Exception("ContrasenaIncorrecta");
                }
                else
                {
                    DesbloquearUsuario(usuario.Email, usuario, true);
                    _iPermisoService.GetComponenteUsuario(usuario);
                    _iBitacoraService.AltaBitacora(usuario.Email, usuario.Puesto, "Inicio de sesion", Criticidad.BAJA);
                }

                return null;
            }
            catch (Exception ex) when (ex.Message.Contains("SQL") || ex.Message.Contains("BD"))
            {
                throw new Exception("ErrorBD");
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

        public Usuario ValidarUsuarioWebMaster(string email, string contraseña)
        {
            var emailEncriptado = EncriptacionService.Encriptar_AES(email);
            var contraseñaEncriptada = EncriptacionService.Encriptar_MD5(contraseña);

            Usuario usuarioReal = ObtenerUsuarioPorEmail(email);

            if (usuarioReal != null && usuarioReal.Email == emailEncriptado && usuarioReal.Contraseña == contraseñaEncriptada && usuarioReal.Puesto == Puesto.WebMaster)
            {
                _iPermisoService.GetComponenteUsuario(usuarioReal);
                return usuarioReal;
            }

            var emailContingencia = ConfigurationManager.AppSettings["EmailContingencia"];
            var contraseñaContingencia = ConfigurationManager.AppSettings["PasswordContingencia"];

            if (emailEncriptado == emailContingencia && contraseñaEncriptada == contraseñaContingencia)
            {
                usuarioReal = ObtenerUsuarioWebmaster();
                _iPermisoService.GetComponenteUsuario(usuarioReal);
                return usuarioReal;
            }

            return null;
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
                        Puesto = row["IdPuesto"] != DBNull.Value 
                                ? (Puesto?)Enum.Parse(typeof(Puesto), row["IdPuesto"].ToString()) 
                                : null, 
                        Area = (Area)Enum.Parse(typeof(Area), row["IdArea"].ToString()),
                        FechaNacimiento = (DateTime)row["FechaNacimiento"],
                        Genero = row["Genero"].ToString(),
                        FechaIngreso = (DateTime)row["FechaIngreso"],
                        Direccion = row["Direccion"].ToString(),
                        NumeroDireccion = Convert.ToInt32(row["NumeroDireccion"]),
                        Departamento = row["Departamento"].ToString(),
                        CodigoPostal = row["CodigoPostal"].ToString(),
                        Ciudad = row["Ciudad"].ToString(),
                        Provincia = row["Provincia"].ToString(),
                        Pais = row["Pais"].ToString(),
                        Estado = Convert.ToInt32(row["Estado"])
                    };

                    listaUsuarios.Add(usuario);
                }

                return listaUsuarios;
            }
            catch (Exception ex) when (ex.Message.Contains("SQL") || ex.Message.Contains("BD"))
            {
                throw new Exception("ErrorBD");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void EstadoBloqueoUsuario(Usuario usuario)
        {
            try
            {
                _usuarioDAO.EstadoBloqueoUsuario(usuario.Email);
                _iDigitoVerificadorService.CalcularDVTabla("Usuario");
                _iBitacoraService.AltaBitacora(usuario.Email, usuario.Puesto, "Intento de login fallido", Criticidad.ALTA);
            }
            catch (Exception ex) when (ex.Message.Contains("SQL") || ex.Message.Contains("BD"))
            {
                throw new Exception("ErrorBD");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DesbloquearUsuario(string email, Usuario userSession, bool esLogin)
        {
            try
            {
                _usuarioDAO.DesbloquearUsuario(email);
                _iDigitoVerificadorService.CalcularDVTabla("Usuario");
                if (!esLogin)
                {
                    _iBitacoraService.AltaBitacora(userSession.Email, userSession.Puesto, $"Se desbloqueo el usuario {EncriptacionService.Decrypt_AES(email)}", Criticidad.ALTA);
                }
                
                return true;
            }
            catch (Exception ex) when (ex.Message.Contains("SQL") || ex.Message.Contains("BD"))
            {
                throw new Exception("ErrorBD");
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

                if (resultado != null)
                    return CompletarUsuario(resultado);

                return null;
            }
            catch (Exception ex) when (ex.Message.Contains("SQL") || ex.Message.Contains("BD"))
            {
                throw new Exception("ErrorBD");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Usuario ObtenerUsuarioWebmaster()
        {
            try
            {
                var resultado = _usuarioDAO.ObtenerUsuarioWebmaster();

                if (resultado != null)
                    return CompletarUsuario(resultado);

                return null;
            }
            catch (Exception ex) when (ex.Message.Contains("SQL") || ex.Message.Contains("BD"))
            {
                throw new Exception("ErrorBD");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Usuario ObtenerUsuarioPorId(int id)
        {
            try
            {
                var resultado = _usuarioDAO.ObtenerUsuarioPorId(id);

                if (resultado != null)
                    return CompletarUsuarioDesencriptado(resultado);

                return null;
            }
            catch (Exception ex) when (ex.Message.Contains("SQL") || ex.Message.Contains("BD"))
            {
                throw new Exception("ErrorBD");
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
            try
            {
                var contraseñaEncriptada = EncriptacionService.Encriptar_MD5(contraseña);
                var resultado = _usuarioDAO.ActualizarContraseña(usuario.Email, contraseñaEncriptada);
                _iDigitoVerificadorService.CalcularDVTabla("Usuario");

                var descripcion = tipoOperacion == TipoOperacionContraseña.Recuperacion ? "Recuperacion de contraseña" : "Cambio de contraseña";

                if (resultado)
                    _iBitacoraService.AltaBitacora(usuario.Email, usuario.Puesto, descripcion, Criticidad.ALTA);

                return resultado;
            }
            catch (Exception ex) when (ex.Message.Contains("SQL") || ex.Message.Contains("BD"))
            {
                throw new Exception("ErrorBD");
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
                Puesto = tabla.Tables[0].Rows[0]["IdPuesto"] == DBNull.Value ? (Puesto?)null : (Puesto)Enum.Parse(typeof(Puesto), tabla.Tables[0].Rows[0]["IdPuesto"].ToString()),
                Area = (Area)tabla.Tables[0].Rows[0]["IdArea"],
                FechaNacimiento = (DateTime)tabla.Tables[0].Rows[0]["FechaNacimiento"],
                Genero = tabla.Tables[0].Rows[0]["Genero"].ToString(),
                FechaIngreso = (DateTime)tabla.Tables[0].Rows[0]["FechaIngreso"],
                Direccion = tabla.Tables[0].Rows[0]["Direccion"].ToString(),
                NumeroDireccion = (int)tabla.Tables[0].Rows[0]["NumeroDireccion"],
                Departamento = tabla.Tables[0].Rows[0]["Departamento"].ToString(),
                CodigoPostal = tabla.Tables[0].Rows[0]["CodigoPostal"].ToString(),
                Ciudad = tabla.Tables[0].Rows[0]["Ciudad"].ToString(),
                Provincia = tabla.Tables[0].Rows[0]["Provincia"].ToString(),
                Pais = tabla.Tables[0].Rows[0]["Pais"].ToString(),
                Estado = (int)tabla.Tables[0].Rows[0]["Estado"]
            };

            return usuario;
        }

        private Usuario CompletarUsuarioDesencriptado(DataSet tabla)
        {
            Usuario usuario = new Usuario
            {
                Id = Convert.ToInt32(tabla.Tables[0].Rows[0]["Id"]),
                Nombre = EncriptacionService.Decrypt_AES(tabla.Tables[0].Rows[0]["Nombre"].ToString()),
                Apellido = EncriptacionService.Decrypt_AES(tabla.Tables[0].Rows[0]["Apellido"].ToString()),
                Email = EncriptacionService.Decrypt_AES(tabla.Tables[0].Rows[0]["Email"].ToString()),
                Puesto = (Puesto)tabla.Tables[0].Rows[0]["IdPuesto"],
                Area = (Area)tabla.Tables[0].Rows[0]["IdArea"],
                FechaNacimiento = (DateTime)tabla.Tables[0].Rows[0]["FechaNacimiento"],
                Genero = tabla.Tables[0].Rows[0]["Genero"].ToString(),
                FechaIngreso = (DateTime)tabla.Tables[0].Rows[0]["FechaIngreso"],
                Direccion = tabla.Tables[0].Rows[0]["Direccion"].ToString(),
                NumeroDireccion = (int)tabla.Tables[0].Rows[0]["NumeroDireccion"],
                Departamento = tabla.Tables[0].Rows[0]["Departamento"].ToString(),
                CodigoPostal = tabla.Tables[0].Rows[0]["CodigoPostal"].ToString(),
                Ciudad = tabla.Tables[0].Rows[0]["Ciudad"].ToString(),
                Provincia = tabla.Tables[0].Rows[0]["Provincia"].ToString(),
                Pais = tabla.Tables[0].Rows[0]["Pais"].ToString(),
                Estado = (int)tabla.Tables[0].Rows[0]["Estado"]
            };

            return usuario;
        }

        public string ValidarContraseñas(Usuario usuario, string contraseñaActual, string contraseñaNueva, string confirmarContraseña)
        {
            var contraseñaEncriptada = EncriptacionService.Encriptar_MD5(contraseñaActual);

            if(usuario.Contraseña != contraseñaEncriptada)
                throw new Exception("ContrasenaActualIncorrecta");

            if (contraseñaNueva != confirmarContraseña)
                throw new Exception("PasswordNoCoincideVieja");

            if (contraseñaNueva.Length < 8 || !ValidarFormatoContraseña(contraseñaNueva))
                throw new Exception("FormatoPasswordIncorrecto");

            return null;
        }

        public string ObtenerCuerpoCorreo(AsuntoMail asuntoMail)
        {
            string templatePath = "";
            switch (asuntoMail)
            {
                case AsuntoMail.RecuperacionContraseña:
                    templatePath = "~/Templates/RecuperarContraseñaTemplate.html";
                    break;
                case AsuntoMail.GeneracionContraseña:
                    templatePath = "~/Templates/GenerarContraseñaTemplate.html";
                    break;
                default:
                    throw new ArgumentException("Tipo de asunto no válido");
            }

            return File.ReadAllText(HttpContext.Current.Server.MapPath(templatePath));
        }

        public List<Usuario> ObtenerUsuariosBloqueados()
        {
            try
            {
                var resultado = _usuarioDAO.ObtenerUsuariosBloqueados();

                List<Usuario> listaUsuarios = new List<Usuario>();

                foreach (DataRow row in resultado.Tables[0].Rows)
                {
                    Usuario usuario = new Usuario
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        Nombre = EncriptacionService.Decrypt_AES(row["Nombre"].ToString()),
                        Apellido = EncriptacionService.Decrypt_AES(row["Apellido"].ToString()),
                        Email = EncriptacionService.Decrypt_AES(row["Email"].ToString()),
                        Estado = Convert.ToInt32(row["Estado"])
                    };

                    listaUsuarios.Add(usuario);
                }

                return listaUsuarios;
            }
            catch (Exception ex) when (ex.Message.Contains("SQL") || ex.Message.Contains("BD"))
            {
                throw new Exception("ErrorBD");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Usuario> ObtenerEmpleados()
        {
            try
            {
                var resultado = _usuarioDAO.ObtenerEmpleados();

                List<Usuario> listaUsuarios = new List<Usuario>();

                foreach (DataRow row in resultado.Tables[0].Rows)
                {
                    Usuario usuario = new Usuario
                    {
                        Id = Convert.ToInt32(row["Id"])
                    };

                    listaUsuarios.Add(usuario);
                }

                return listaUsuarios;
            }
            catch (Exception ex) when (ex.Message.Contains("SQL") || ex.Message.Contains("BD"))
            {
                throw new Exception("ErrorBD");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}