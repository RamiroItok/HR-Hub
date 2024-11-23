using Aplication.Interfaces;
using Aplication.Interfaces.Observer;
using Aplication.Services.Observer;
using Models;
using Models.Composite;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unity;

namespace GUI
{
    public partial class Backup : Page, IIdiomaService
    {
        private readonly IBackUpService _iBackupService;
        private readonly IBitacoraService _iBitacoraService;
        private readonly IPermisoService _permisoService;
        private readonly IdiomaService _idiomaService;

        public Backup()
        {
            _iBackupService = Global.Container.Resolve<IBackUpService>();
            _iBitacoraService = Global.Container.Resolve<IBitacoraService>();
            _permisoService = Global.Container.Resolve<IPermisoService>();
            _idiomaService = Global.Container.Resolve<IdiomaService>();
            _idiomaService.Subscribe(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            var usuario = Session["Usuario"] as Usuario;
            if(!_permisoService.TienePermiso(usuario, Permiso.BackUp))
            {
                Response.Redirect("AccesoDenegado.aspx");
                return;
            }
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
            if (!(lblTituloBackup == null))
            {
                lblTituloBackup.Text = _idiomaService.GetTranslation("TituloBackup");
                Page.Title = _idiomaService.GetTranslation("PageTitleBackup");
                lblBackup.Text = _idiomaService.GetTranslation("RutaBackup");
                lblNombreBackup.Text = _idiomaService.GetTranslation("LabelNombreBackup");
                txtNombre.Attributes["placeholder"] = _idiomaService.GetTranslation("PlaceholderNombreBackup");
                btnBackup.Text = _idiomaService.GetTranslation("ButtonRealizarBackup");
                btnCancelar.Text = _idiomaService.GetTranslation("ButtonCancelar");
            }
        }

        protected void btnBackup_Click(object sender, EventArgs e)
        {
            Usuario usuario = Session["Usuario"] as Usuario;
            var nombre = txtNombre.Text;

            try
            {
                var ruta = ConfigurationManager.AppSettings["RutaBackup"];
                var resultado = _iBackupService.RealizarBackup(ruta, nombre, usuario);
                _iBitacoraService.AltaBitacora(usuario.Email, usuario.Puesto, "Se realizó una copia de seguridad", Criticidad.ALTA);

                lblMensaje.Text = _idiomaService.GetTranslation("MensajeExitoBackup");
                lblMensaje.CssClass = "text-success";
            }
            catch (Exception ex)
            {
                lblMensaje.Text = $"{_idiomaService.GetTranslation("MensajeErrorGeneral")}: {_idiomaService.GetTranslation(ex.Message)}";
                lblMensaje.CssClass = "text-danger";
            }
            finally
            {
                lblMensaje.Visible = true;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            txtNombre.Text = string.Empty;
            lblMensaje.Visible = false;
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