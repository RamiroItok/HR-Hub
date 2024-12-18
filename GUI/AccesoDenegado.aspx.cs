﻿using Aplication.Interfaces.Observer;
using Aplication.Services.Observer;
using System;
using System.Web.UI;
using Unity;

namespace GUI
{
    public partial class AccesoDenegado : Page, IIdiomaService
    {
        private readonly IdiomaService _idiomaService;

        public AccesoDenegado()
        {
            _idiomaService = Global.Container.Resolve<IdiomaService>();
            _idiomaService.Subscribe(this);
        }

        public void UpdateLanguage(string language)
        {
            CargarTextos();
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
            if (!(errorCode == null))
            {
                errorCode.InnerText = _idiomaService.GetTranslation("ErrorCode403");
                Page.Title = _idiomaService.GetTranslation("PageTitleAccessDenied");
                errorTitle.InnerText = _idiomaService.GetTranslation("ErrorTitleAccessDenied");
                errorMessage.InnerText = _idiomaService.GetTranslation("ErrorMessageAccessDenied");
                btnHome.InnerText = _idiomaService.GetTranslation("ButtonHome");
            }
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