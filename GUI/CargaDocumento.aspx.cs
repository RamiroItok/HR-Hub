using Aplication.Interfaces;
using Aplication.Interfaces.Observer;
using Aplication.Services.Observer;
using Models;
using Models.Composite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unity;

namespace GUI
{
    public partial class CargaDocumento : Page, IIdiomaService
    {
        private readonly IDocumentoService _documentoService;
        private readonly IPermisoService _permisoService;
        private readonly IdiomaService _idiomaService;

        public CargaDocumento()
        {
            _documentoService = Global.Container.Resolve<IDocumentoService>();
            _permisoService = Global.Container.Resolve<IPermisoService>();
            _idiomaService = Global.Container.Resolve<IdiomaService>();
            _idiomaService.Subscribe(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var usuario = Session["Usuario"] as Usuario;
            if(!_permisoService.TienePermiso(usuario, Permiso.CargaDocumentos))
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
            if (!(litPageTitle == null))
            {
                litPageTitle.Text = _idiomaService.GetTranslation("PageTitleCargaDocumento");
                lblTitulo.Text = _idiomaService.GetTranslation("TituloCargaDocumento");
                lblNombreArchivo.Text = _idiomaService.GetTranslation("LabelNombreArchivo");
                lblArchivo.Text = _idiomaService.GetTranslation("LabelArchivo");
                btnCargar.Text = _idiomaService.GetTranslation("ButtonCargarArchivo");
                btnCancelar.Text = _idiomaService.GetTranslation("ButtonCancelar");
                lblPopupTitulo.Text = _idiomaService.GetTranslation("PopupTitulo");
                txtNombreArchivo.Attributes["placeholder"] = _idiomaService.GetTranslation("PlaceholderNombreArchivo");
            }
        }

        protected void BtnCargar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!fileUpload.HasFile)
                {
                    throw new Exception(_idiomaService.GetTranslation("MensajeSeleccioneArchivo"));
                }

                string nombreArchivo = txtNombreArchivo.Text;
                string tipoArchivo = fileUpload.PostedFile.ContentType;
                byte[] contenidoArchivo;

                using (var stream = fileUpload.PostedFile.InputStream)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        stream.CopyTo(memoryStream);
                        contenidoArchivo = memoryStream.ToArray();
                    }
                }

                Documento documento = new Documento()
                {
                    Nombre = nombreArchivo,
                    TipoArchivo = tipoArchivo,
                    Contenido = contenidoArchivo,
                    FechaCarga = DateTime.Now
                };

                var userSession = Session["Usuario"] as Usuario;

                var id = _documentoService.CargarDocumento(documento, userSession);
                _documentoService.AsignarDocumento(id);

                Limpiar();
                ScriptManager.RegisterStartupScript(this, GetType(), "mostrarPopup", $"mostrarPopup('{_idiomaService.GetTranslation("MensajeExitoCarga").Replace("'", "\\'")}', 'success');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "mostrarPopup", $"mostrarPopup('{_idiomaService.GetTranslation(ex.Message).Replace("'", "\\'")}', 'error');", true);
            }       
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void Limpiar()
        {
            txtNombreArchivo.Text = string.Empty;
            fileUpload = null;
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
            _idiomaService.CurrentLanguage = selectedLanguage;
            CargarTextos();
        }
    }
}