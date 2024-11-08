using Aplication.Interfaces;
using Aplication.Interfaces.Observer;
using Aplication.Services.Observer;
using System;
using System.Collections.Generic;
using System.Web.UI;
using Unity;

namespace GUI
{
    public partial class Login : Page, IIdiomaService
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IDigitoVerificadorService _digitoVerificadorService;
        private readonly IdiomaService _idiomaService;

        public Login()
        {
            _usuarioService = Global.Container.Resolve<IUsuarioService>();
            _digitoVerificadorService = Global.Container.Resolve<IDigitoVerificadorService>();
            _idiomaService = Global.Container.Resolve<IdiomaService>();
            _idiomaService.Subscribe(this);
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

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> listaMensajes = _digitoVerificadorService.VerificarDV();
                if (listaMensajes.Count > 0)
                {
                    Models.FalloIntegridad falloIntegridad = new Models.FalloIntegridad();
                    falloIntegridad.Tablas = listaMensajes;
                    falloIntegridad.Fallo = true;

                    var usuario = _usuarioService.ValidarUsuarioWebMaster(ValidarEmailControl.Email, PasswordValidator.Password);

                    if (usuario != null)
                        Session["Usuario"] = usuario;

                    Session["ErrorVerificacionDV"] = falloIntegridad;
                    Response.Redirect($"ErrorDigitoVerificador.aspx");
                    return;
                }

                if (ValidarEmailControl.EsEmailValido())
                {
                    if (PasswordValidator.IsValid)
                    {
                        var usuario = _usuarioService.ObtenerUsuarioPorEmail(ValidarEmailControl.Email);
                        string password = PasswordValidator.Password;

                        _usuarioService.ValidarUsuario(usuario, ValidarEmailControl.Email, password);

                        Session["Usuario"] = usuario;
                        Response.Redirect("MenuPrincipal.aspx", false);
                        Context.ApplicationInstance.CompleteRequest();
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
                lblMensaje.Text = _idiomaService.GetTranslation(ex.Message);
                lblMensaje.Visible = true;
            }
        }

        public void UpdateLanguage(string language)
        {
            CargarTextos();
        }

        protected override void OnUnload(EventArgs e)
        {
            _idiomaService.Unsubscribe(this);
            base.OnUnload(e);
        }

        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedLanguage = ddlLanguage.SelectedValue;
            Session["SelectedLanguage"] = selectedLanguage;
            Response.Redirect(Request.RawUrl);
        }
    }
}
