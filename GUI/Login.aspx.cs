using Aplication.Interfaces;
using System;
using System.Web.UI;
using Unity;

namespace GUI
{
    public partial class Login : Page
    {
        private readonly IUsuarioService _usuarioService;

        public Login()
        {
            _usuarioService = Global.Container.Resolve<IUsuarioService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            var usuario = _usuarioService.ValidarUsuarioContraseña(txtEmail.Text, txtPassword.Text);

            if (usuario == null)
            {
                lblMensaje.Text = "El email no coincide con la contraseña.";
            }
            else
            {
                Session["Usuario"] = usuario;
                Response.Redirect("Default.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }
    }
}