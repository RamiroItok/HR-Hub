using Aplication.Interfaces;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unity;

namespace GUI
{
    public partial class RecuperarContraseña : Page
    {
        private readonly IUsuarioService _usuarioService;

        public RecuperarContraseña()
        {
            _usuarioService = Global.Container.Resolve<IUsuarioService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRecuperar_Click(object sender, EventArgs e)
        {
            var email = txtEmail.Text;
            var usuario = _usuarioService.ObtenerUsuarioPorEmail(email);

            if(usuario == null)
            {
                lblMensaje.Text = "El usuario no existe";
                lblMensaje.Visible = true;
            }
            else
            {
                string nuevaContraseña = _usuarioService.GenerarContraseña();
                if(_usuarioService.ActualizarContraseña(usuario, nuevaContraseña, Models.Enums.TipoOperacionContraseña.Recuperacion))
                {
                    string body = _usuarioService.ObtenerCuerpoCorreo(AsuntoMail.RecuperacionContraseña);
                    body = body.Replace("{{CONTRASEÑA}}", nuevaContraseña);

                    _usuarioService.EnviarMail(email, AsuntoMail.RecuperacionContraseña, body);
                    lblMensaje.Text = "Se ha enviado una nueva contraseña a su correo electrónico.";
                    lblMensaje.CssClass = "message-label success";
                }
                else
                {
                    lblMensaje.Text = "Hubo un error al actualizar la contraseña. Intente nuevamente.";
                    lblMensaje.CssClass = "message-label error";
                }
                lblMensaje.Visible = true;
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    }
}