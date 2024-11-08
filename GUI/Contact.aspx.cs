using Aplication.Interfaces.Observer;
using Aplication.Services.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unity;

namespace GUI
{
    public partial class Contact : Page, IIdiomaService
    {
        private readonly IdiomaService _idiomaService;

        public Contact()
        {
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
            if (!(litContactTitle == null))
            {
                litContactTitle.Text = _idiomaService.GetTranslation("ContactTitle");
                litAddress.Text = _idiomaService.GetTranslation("Address") + ": El Salvador 5847";
                litCity.Text = _idiomaService.GetTranslation("City") + ": Buenos Aires";
                litProvince.Text = _idiomaService.GetTranslation("Province") + ": Buenos Aires";
                litCountry.Text = _idiomaService.GetTranslation("Country") + ": Argentina";
                litPhone.Text = _idiomaService.GetTranslation("Phone") + ": +54 11 1234-5678";
                litEmailLabel.Text = _idiomaService.GetTranslation("Email");
            }
        }

        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedLanguage = ddlLanguage.SelectedValue;
            Session["SelectedLanguage"] = selectedLanguage;
            Response.Redirect(Request.RawUrl);
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