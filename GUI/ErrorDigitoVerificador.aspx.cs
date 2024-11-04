using Models;
using System;
using Aplication.Services.Observer;
using Unity;
using Aplication.Interfaces.Observer;

namespace GUI
{
    public partial class ErrorDigitoVerificador : System.Web.UI.Page, IIdiomaService
    {
        private readonly IdiomaService _idiomaService;

        public ErrorDigitoVerificador()
        {
            _idiomaService = Global.Container.Resolve<IdiomaService>();
            _idiomaService.Subscribe(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string selectedLanguage = Session["SelectedLanguage"] as string ?? "es";
                _idiomaService.CurrentLanguage = selectedLanguage;

                string tabla = Request.QueryString["mensaje"];
                lblErrorMessage.Text = !string.IsNullOrEmpty(tabla)
                    ? _idiomaService.GetTranslation("ErrorIngresarSistema")
                    : _idiomaService.GetTranslation("ErrorDesconocido");

                CargarTextos();
            }
        }

        private void CargarTextos()
        {
            if (!(litPageTitle == null))
            {
                litPageTitle.Text = _idiomaService.GetTranslation("PageTitleErrorVerificador");
                litErrorTitle.Text = _idiomaService.GetTranslation("TituloErrorVerificador");
                litContactMessage.Text = _idiomaService.GetTranslation("MensajeContactoAdmin");
                btnOk.Text = _idiomaService.GetTranslation("BotonAceptar");
            }
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            var usuario = Session["Usuario"] as Usuario;
            if (usuario != null && usuario.Puesto != Models.Enums.Puesto.WebMaster)
            {
                Session.Abandon();
                Response.Redirect("Home.aspx");
            }
            else
            {
                Response.Redirect("MenuPrincipal.aspx");
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
    }
}
