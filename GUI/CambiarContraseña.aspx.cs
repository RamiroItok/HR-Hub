﻿using Aplication.Interfaces;
using Aplication.Interfaces.Observer;
using Aplication.Services.Observer;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unity;

namespace GUI
{
    public partial class CambiarContraseña : Page, IIdiomaService
    {
        private readonly IUsuarioService _iUsuarioService;
        private readonly IdiomaService _idiomaService;

        public CambiarContraseña()
        {
            _iUsuarioService = Global.Container.Resolve<IUsuarioService>();
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

        protected void btnCambiar_Click(object sender, EventArgs e)
        {
            var contraseñaActual = txtPasswordActual.Text;
            var contraseñaNueva = txtPasswordNueva.Text;
            var confirmarContraseña = txtPasswordConfirmar.Text;
            var usuario = Session["Usuario"] as Usuario;

            var esContraseñaValida = _iUsuarioService.ValidarContraseñas(usuario, contraseñaActual, contraseñaNueva, confirmarContraseña);

            if (esContraseñaValida != null)
            {
                lblMensaje.Text = _idiomaService.GetTranslation("ErrorContrasenaInvalida");
                lblMensaje.CssClass = "message-label error";
                lblMensaje.Visible = true;
            }
            else
            {
                if (_iUsuarioService.ActualizarContraseña(usuario, contraseñaNueva, Models.Enums.TipoOperacionContraseña.Cambio))
                {
                    lblMensaje.Visible = true;
                    lblMensaje.CssClass = "message-label success";
                    lblMensaje.Text = _idiomaService.GetTranslation("SuccessContrasenaCambiada");
                }
                else
                {
                    lblMensaje.Visible = true;
                    lblMensaje.CssClass = "message-label error";
                    lblMensaje.Text = _idiomaService.GetTranslation("ErrorCambioContrasena");
                }
            }
            
        }

        public void UpdateLanguage(string language)
        {
            CargarTextos();
        }

        private void CargarTextos()
        {
            Title = _idiomaService.GetTranslation("PageTitleChangePassword");
            litTitulo.Text = _idiomaService.GetTranslation("TituloCambiarContrasena");
            lblPasswordActual.Text = _idiomaService.GetTranslation("LabelPasswordActual");
            lblPasswordNueva.Text = _idiomaService.GetTranslation("LabelPasswordNueva");
            lblPasswordConfirmar.Text = _idiomaService.GetTranslation("LabelPasswordConfirmar");
            btnCambiar.Text = _idiomaService.GetTranslation("ButtonCambiarContrasena");
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