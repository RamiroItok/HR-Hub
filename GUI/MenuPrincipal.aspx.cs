using Aplication;
using Aplication.Interfaces;
using Aplication.Interfaces.Observer;
using Aplication.Services.Observer;
using Models;
using System;
using System.Collections.Generic;
using System.Web.UI;
using Unity;

namespace GUI
{
    public partial class MenuPrincipal : Page, IIdiomaService
    {
        private readonly IBackUpService _backUpService;
        private readonly IDigitoVerificadorService _digitoVerificadorService;
        private readonly IdiomaService _idiomaService;

        public MenuPrincipal()
        {
            _backUpService = Global.Container.Resolve<IBackUpService>();
            _digitoVerificadorService = Global.Container.Resolve<IDigitoVerificadorService>();
            _idiomaService = Global.Container.Resolve<IdiomaService>();
            _idiomaService.Subscribe(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                VerificarDV();
                var usuario = Session["Usuario"] as Usuario;

                if (usuario != null)
                {
                    string idioma = Session["SelectedLanguage"] as string ?? "es";
                    _idiomaService.CurrentLanguage = idioma;

                    CargarTextoUsuario();

                    var nombre = EncriptacionService.Decrypt_AES(usuario.Nombre);
                    var apellido = EncriptacionService.Decrypt_AES(usuario.Apellido);

                    lblNombreUsuario.Text = lblNombreUsuario.Text + usuario.Puesto + ", " + nombre + " " + apellido;
                    lblNombreUsuarioProfile.Text = nombre + " " + apellido;
                }
                string selectedLanguage = Session["SelectedLanguage"] as string ?? "es";
                ddlLanguage.SelectedValue = selectedLanguage;
            }
        }

        private void CargarTextoUsuario()
        {
            if (!(lblNombreUsuario == null))
            {
                lblNombreUsuario.Text = _idiomaService.GetTranslation("TituloMensaje");
                litTituloPagina.Text = _idiomaService.GetTranslation("TituloPaginaMenu");
                litGestionaPerfil.Text = _idiomaService.GetTranslation("GestionaPerfil");
                litIrPerfil.Text = _idiomaService.GetTranslation("IrPerfil");
                litSolicitarDescanso.Text = _idiomaService.GetTranslation("SolicitarDescanso");

                litMisTareas.Text = _idiomaService.GetTranslation("MisTareas");
                litConsultaTareas.Text = _idiomaService.GetTranslation("ConsultaTareas");
                litVerTareas.Text = _idiomaService.GetTranslation("VerTareas");

                litMisDocumentos.Text = _idiomaService.GetTranslation("MisDocumentos");
                litAccedeDocumentos.Text = _idiomaService.GetTranslation("AccedeDocumentos");
                litVerDocumentos.Text = _idiomaService.GetTranslation("VerDocumentos");

                litFooterText.Text = _idiomaService.GetTranslation("TextoPiePagina");
            }
        }

        private void VerificarDV()
        {
            if (Session["ErrorVerificacionDV"] == null)
            {
                _backUpService.CrearBaseDeDatos();
                List<string> listaMensajes = _digitoVerificadorService.VerificarDV();
                if (listaMensajes.Count > 0)
                {
                    Models.FalloIntegridad falloIntegridad = new Models.FalloIntegridad();
                    falloIntegridad.Tablas = listaMensajes;
                    falloIntegridad.Fallo = true;

                    Session["ErrorVerificacionDV"] = falloIntegridad;
                    Response.Redirect($"ErrorDigitoVerificador.aspx");
                }
            }
        }

        public void UpdateLanguage(string newLanguage)
        {
            CargarTextoUsuario();
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