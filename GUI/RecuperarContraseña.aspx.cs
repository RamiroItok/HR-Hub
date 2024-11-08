using Aplication.Interfaces;
using Aplication.Interfaces.Observer;
using Aplication.Services.Observer;
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
    public partial class RecuperarContraseña : Page, IIdiomaService
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IdiomaService _idiomaService;

        public RecuperarContraseña()
        {
            _usuarioService = Global.Container.Resolve<IUsuarioService>();
            _idiomaService = Global.Container.Resolve<IdiomaService>();
            _idiomaService.Subscribe(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string selectedLanguage = Session["SelectedLanguage"] as string ?? "es";
                ddlLanguage.SelectedValue = selectedLanguage;
                _idiomaService.CurrentLanguage = selectedLanguage;
                CargarTextos();
            }
        }

        private void CargarTextos()
        {
            lblTitulo.Text = _idiomaService.GetTranslation("TituloRecuperarContrasena");
            lblEmail.Text = _idiomaService.GetTranslation("LabelEmail");
            btnRecuperar.Text = _idiomaService.GetTranslation("BotonRecuperarContrasena");
            btnVolver.Text = _idiomaService.GetTranslation("BotonVolver");
        }

        protected void btnRecuperar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtEmail.Text))
                    throw new Exception("MensajeCamposIncompletos");

                var email = txtEmail.Text;
                var usuario = _usuarioService.ObtenerUsuarioPorEmail(email);

                if (usuario == null)
                    throw new Exception("MensajeUsuarioNoExiste");
                else
                {
                    string nuevaContraseña = _usuarioService.GenerarContraseña();
                    if (_usuarioService.ActualizarContraseña(usuario, nuevaContraseña, Models.Enums.TipoOperacionContraseña.Recuperacion))
                    {
                        string body = _usuarioService.ObtenerCuerpoCorreo(AsuntoMail.RecuperacionContraseña);
                        body = body.Replace("{{CONTRASEÑA}}", nuevaContraseña);

                        _usuarioService.EnviarMail(email, AsuntoMail.RecuperacionContraseña, body);
                        lblMensaje.Text = _idiomaService.GetTranslation("MensajeContraseñaEnviada");
                        lblMensaje.CssClass = "message-label success";
                    }
                    else
                        throw new Exception("MensajeErrorActualizarContraseña");

                    lblMensaje.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = _idiomaService.GetTranslation(ex.Message);
                lblMensaje.Visible = true;
                lblMensaje.CssClass = "message-label error";
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        public void UpdateLanguage(string newLanguage)
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