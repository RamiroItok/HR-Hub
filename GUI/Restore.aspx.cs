using Aplication.Interfaces;
using Aplication.Interfaces.Observer;
using Aplication.Services.Observer;
using Models;
using Models.Composite;
using Models.Enums;
using System;
using System.IO;
using System.Web.UI;
using Unity;

namespace GUI
{
    public partial class Restore : Page, IIdiomaService
    {
        private readonly IBackUpService _iBackUpService;
        private readonly IBitacoraService _iBitacoraService;
        private readonly IPermisoService _permisoService;
        private readonly IdiomaService _idiomaService;

        public Restore()
        {
            _iBackUpService = Global.Container.Resolve<IBackUpService>();
            _iBitacoraService = Global.Container.Resolve<IBitacoraService>();
            _permisoService = Global.Container.Resolve<IPermisoService>();
            _idiomaService = Global.Container.Resolve<IdiomaService>();
            _idiomaService.Subscribe(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var usuario = Session["Usuario"] as Usuario;
            if(!_permisoService.TienePermiso(usuario, Permiso.Restore))
            {
                Response.Redirect("AccesoDenegado.aspx");
                Context.ApplicationInstance.CompleteRequest();
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
            if (!(lblTituloRestore == null))
            {
                lblTituloRestore.Text = _idiomaService.GetTranslation("TituloRestore");
                litPageTitle.Text = _idiomaService.GetTranslation("PageTitleRestore");
                lblDescripcionRestore.Text = _idiomaService.GetTranslation("DescripcionRestore");
                lblSeleccionarArchivo.Text = _idiomaService.GetTranslation("LabelSeleccionarArchivo");
                btnRestore.Text = _idiomaService.GetTranslation("ButtonRealizarRestore");
                btnCancelar.Text = _idiomaService.GetTranslation("ButtonCancelar");
            }
        }

        protected void btnRestore_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario usuario = Session["Usuario"] as Usuario;
                if (!string.IsNullOrEmpty(fileBackup.FileName))
                {
                    string rutaArchivo = Path.GetFileName(fileBackup.FileName);

                    string rutaDestino = "D:/Backups/" + rutaArchivo;
                    fileBackup.SaveAs(rutaDestino);

                    var resultado = _iBackUpService.RealizarRestore(rutaDestino, usuario);
                    _iBitacoraService.AltaBitacora(usuario.Email, usuario.Puesto, "Se realizó un restore.", Criticidad.ALTA);
                    
                    lblMensaje.Text = _idiomaService.GetTranslation("RestoreExitoso");
                }
                else
                {
                    throw new Exception("MensajeCamposIncompletos");
                }
            }
            catch (Exception ex)
            {
                lblMensaje.CssClass = "text-danger";
                lblMensaje.Text = _idiomaService.GetTranslation(ex.Message);
            }
            finally
            {
                lblMensaje.Visible = true;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            fileBackup = null;
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