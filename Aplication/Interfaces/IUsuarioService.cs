using Models;
using Models.Enums;
using System.Collections.Generic;
using System.Data;

namespace Aplication.Interfaces
{
    public interface IUsuarioService
    {
        int RegistrarUsuario(Usuario usuario, Usuario userSession);
        DataTable ObtenerPuestos();
        DataTable ObtenerAreas();
        string ValidarUsuario(Usuario usuario, string email, string contraseña);
        List<Usuario> ListarUsuarios();
        void EstadoBloqueoUsuario(Usuario usuario);
        bool DesbloquearUsuario(string email, Usuario userSession, bool esLogin);
        Usuario ObtenerUsuarioPorId(int id);
        Usuario ObtenerUsuarioPorEmail(string email);
        bool ValidarFormatoContraseña(string contraseña);
        string GenerarContraseña();
        bool ActualizarContraseña(Usuario usuario, string contraseña, TipoOperacionContraseña tipoOperacionContraseña);
        void EnviarMail(string email, AsuntoMail asuntoMail, string body);
        string ValidarContraseñas(Usuario usuario, string contraseñaActual, string contraseñaNueva, string confirmarContraseña);
        int ModificarUsuario(Usuario usuario, Usuario userSession);
        void ModificarPermisoUsuario(Usuario usuario, Usuario userSession);
        string ObtenerAsuntoCorreo(AsuntoMail asuntoMail);
        string ObtenerCuerpoCorreo(AsuntoMail asuntoMail);
        List<Usuario> ObtenerUsuariosBloqueados();
    }
}