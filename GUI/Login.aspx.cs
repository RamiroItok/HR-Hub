using Aplication.Interfaces;
using Aplication.Services.Observer;
using System;
using System.Web.UI;
using Unity;

namespace GUI
{
    public partial class Login : Page
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IdiomaService _idiomaService;

        public Login()
        {
            _usuarioService = Global.Container.Resolve<IUsuarioService>();
            _idiomaService = Global.Container.Resolve<IdiomaService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Usuario"] != null)
                {
                    Response.Redirect("/MenuPrincipal.aspx");
                }

                string selectedLanguage = Session["SelectedLanguage"] as string ?? "es";
                ddlLanguage.SelectedValue = selectedLanguage;
                _idiomaService.CurrentLanguage = selectedLanguage;

                CargarTextos();
            }
        }

        private void CargarTextos()
        {
            litTituloPagina.Text = _idiomaService.GetTranslation("TituloPaginaLogin");
            litTituloFormulario.Text = _idiomaService.GetTranslation("TituloFormularioLogin");
            btnLogin.Text = _idiomaService.GetTranslation("BotonIniciarSesion");
            litRecuperarContraseña.Text = _idiomaService.GetTranslation("EnlaceRecuperarContrasenia");
        }

        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedLanguage = ddlLanguage.SelectedValue;
            Session["SelectedLanguage"] = selectedLanguage;
            Response.Redirect(Request.RawUrl);
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarEmailControl.EsEmailValido())
                {
                    if (PasswordValidator.IsValid)
                    {
                        var usuario = _usuarioService.ObtenerUsuarioPorEmail(ValidarEmailControl.Email);
                        string password = PasswordValidator.Password;

                        lblMensaje.Text = _usuarioService.ValidarUsuario(usuario, ValidarEmailControl.Email, password);

                        if (!string.IsNullOrEmpty(lblMensaje.Text))
                        {
                            lblMensaje.Visible = true;
                        }
                        else
                        {
                            Session["Usuario"] = usuario;
                            Response.Redirect("MenuPrincipal.aspx", false);
                            Context.ApplicationInstance.CompleteRequest();
                        }
                    }
                    else
                    {
                        lblMensaje.Text = _idiomaService.GetTranslation("ErrorContrasenaNoValida");
                        lblMensaje.Visible = true;
                    }
                }
                else
                {
                    lblMensaje.Text = _idiomaService.GetTranslation("ErrorEmailNoValido");
                    lblMensaje.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.Visible = true;
            }
        }
    }
}
