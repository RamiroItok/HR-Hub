using Aplication.Interfaces;
using Models.Enums;
using System;
using System.Web.UI;
using Unity;

namespace GUI
{
    public partial class Login : Page
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IBitacoraService _bitacoraService;

        public Login()
        {
            _usuarioService = Global.Container.Resolve<IUsuarioService>();
            _bitacoraService = Global.Container.Resolve<IBitacoraService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] != null)
                Response.Redirect("/Default.aspx");
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                var usuario = _usuarioService.ObtenerUsuarioPorEmail(txtEmail.Text);

                lblMensaje.Text = _usuarioService.ValidarUsuario(usuario, txtEmail.Text, txtPassword.Text);

                if (lblMensaje.Text != "")
                    lblMensaje.Visible = true;
                else
                {
                    Session["Usuario"] = usuario;
                    Response.Redirect("Default.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("SQL"))
                {
                    lblMensaje.Text = "Se ha perdido la conexión con la base de datos. Vuelva a intentar en unos minutos";
                    lblMensaje.Visible = true;
                }
            }
        }
    }
}