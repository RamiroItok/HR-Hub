using System;
using System.Web.UI;
using Aplication.Services.Observer;
using Unity;

namespace GUI
{
    public partial class Home : Page
    {
        private readonly IdiomaService _languageService;

        public Home()
        {
            _languageService = Global.Container.Resolve<IdiomaService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string selectedLanguage = Session["SelectedLanguage"] as string ?? "es";
                ddlLanguage.SelectedValue = selectedLanguage;
                _languageService.CurrentLanguage = selectedLanguage;

                CargarTextos();
            }
        }

        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedLanguage = ddlLanguage.SelectedValue;
            Session["SelectedLanguage"] = selectedLanguage;
            Response.Redirect(Request.RawUrl);
        }

        private void CargarTextos()
        {
            if (!(litWelcomeTitle == null))
            {
                litWelcomeTitle.Text = _languageService.GetTranslation("TituloBienvenida");
                litWelcomeDescription.Text = _languageService.GetTranslation("DescripcionBienvenida");

                hlLogin.Text = _languageService.GetTranslation("BotonIniciarSesion");
                btnInfo.Text = _languageService.GetTranslation("BotonInformacion");
                hlContact.Text = _languageService.GetTranslation("BotonContacto");

                litPillarsTitle.Text = _languageService.GetTranslation("TituloPilares");
                litPillar1Title.Text = _languageService.GetTranslation("TituloPilar1");
                litPillar1Description.Text = _languageService.GetTranslation("DescripcionPilar1");
                litPillar2Title.Text = _languageService.GetTranslation("TituloPilar2");
                litPillar2Description.Text = _languageService.GetTranslation("DescripcionPilar2");
                litPillar3Title.Text = _languageService.GetTranslation("TituloPilar3");
                litPillar3Description.Text = _languageService.GetTranslation("DescripcionPilar3");

                litServicesTitle.Text = _languageService.GetTranslation("TituloServicios");
                litService1Title.Text = _languageService.GetTranslation("TituloServicio1");
                litService1Description.Text = _languageService.GetTranslation("DescripcionServicio1");
                litService2Title.Text = _languageService.GetTranslation("TituloServicio2");
                litService2Description.Text = _languageService.GetTranslation("DescripcionServicio2");
                litService3Title.Text = _languageService.GetTranslation("TituloServicio3");
                litService3Description.Text = _languageService.GetTranslation("DescripcionServicio3");
                litService4Title.Text = _languageService.GetTranslation("TituloServicio4");
                litService4Description.Text = _languageService.GetTranslation("DescripcionServicio4");
                litService5Title.Text = _languageService.GetTranslation("TituloServicio5");
                litService5Description.Text = _languageService.GetTranslation("DescripcionServicio5");
                litService6Title.Text = _languageService.GetTranslation("TituloServicio6");
                litService6Description.Text = _languageService.GetTranslation("DescripcionServicio6");

                litMoreServices.Text = _languageService.GetTranslation("MasServicios");
                litFooterText.Text = _languageService.GetTranslation("TextoPiePagina");
            }
        }
    }
}
