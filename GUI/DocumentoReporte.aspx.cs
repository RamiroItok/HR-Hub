using Aplication.Interfaces;
using Aplication.Interfaces.Observer;
using Aplication.Services.Observer;
using Aplication.Services.XML;
using System;
using System.Web.Script.Serialization;
using System.Web.UI;
using Unity;

namespace GUI
{
    public partial class DocumentoReporte : Page, IIdiomaService
    {
        private readonly IDocumentoService _documentoService;
        private readonly IdiomaService _idiomaService;

        public DocumentoReporte()
        {
            _documentoService = Global.Container.Resolve<IDocumentoService>();
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
            if(!(litReporteTitulo == null))
            {
                litReporteTitulo.Text = _idiomaService.GetTranslation("TituloReporteDocumentos");
                litReporteDescripcion.Text = _idiomaService.GetTranslation("DescripcionReporteDocumentos");
                btnGenerarReporte.Text = _idiomaService.GetTranslation("ButtonGenerarReporte");
                btnGenerarXML.Text = _idiomaService.GetTranslation("ButtonGenerarXML");
            }
        }

        protected void btnGenerarReporte_Click(object sender, EventArgs e)
        {
            try
            {
                WebService.DocumentosService servicio = new WebService.DocumentosService();
                var datos = servicio.ObtenerPorcentajeFirmasPorDocumento();

                if (datos != null && datos.Count > 0)
                {
                    JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                    string jsonData = jsSerializer.Serialize(datos);

                    string script = $"renderTortaGrafico({jsonData});";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "renderGrafico", script, true);
                }
                else
                {
                    string noDataScript = $"Swal.fire('{_idiomaService.GetTranslation("TextoSinDatos")}', '{_idiomaService.GetTranslation("SinDatosReporteDocumentos")}', 'warning');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "sinDatosReporte", noDataScript, true);
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Visible = true;
                lblMensaje.CssClass = "text-danger";
                lblMensaje.Text = $"{_idiomaService.GetTranslation("MensajeErrorGeneral")}: {_idiomaService.GetTranslation(ex.Message)}";
            }
        }

        protected void btnGenerarXML_Click(object sender, EventArgs e)
        {
            try
            {
                WebService.DocumentosService servicio = new WebService.DocumentosService();
                var datos = servicio.ObtenerPorcentajeFirmasPorDocumento();

                if (datos != null && datos.Count > 0)
                {
                    GenerarXML generadorXml = new GenerarXML();
                    generadorXml.GenerarXMLFirmasDocumentos(datos);

                    string successScript = $"Swal.fire('{_idiomaService.GetTranslation("XMLGenerado")}', '{_idiomaService.GetTranslation("MensajeReporteExitoso")}', 'success');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "xmlGenerado", successScript, true);
                }
                else
                {
                    string noDataScript = $"Swal.fire('{_idiomaService.GetTranslation("TextoSinDatos")}', '{_idiomaService.GetTranslation("NoDatosXML")}.', 'warning');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "sinDatosXML", noDataScript, true);
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Visible = true;
                lblMensaje.CssClass = "text-danger";
                lblMensaje.Text = $"{_idiomaService.GetTranslation("MensajeErrorGeneral")}: {_idiomaService.GetTranslation(ex.Message)}";
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

        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedLanguage = ddlLanguage.SelectedValue;
            Session["SelectedLanguage"] = selectedLanguage;
            CargarTextos();
            Response.Redirect(Request.RawUrl);
        }
    }
}