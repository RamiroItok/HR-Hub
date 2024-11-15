using Aplication.Interfaces.Observer;
using Aplication.Services.Observer;
using Aplication.Services.XML;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Unity;

namespace GUI
{
    public partial class ReporteCompras : Page, IIdiomaService
    {
        private readonly IdiomaService _idiomaService;

        public ReporteCompras()
        {
            _idiomaService = Global.Container.Resolve<IdiomaService>();
            _idiomaService.Subscribe(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarAnios();
                string selectedLanguage = Session["SelectedLanguage"] as string ?? "es";
                ddlLanguage.SelectedValue = selectedLanguage;
                _idiomaService.CurrentLanguage = selectedLanguage;
                CargarTextos();
            }
        }

        private void CargarTextos()
        {
            if (!(litTitle == null))
            {
                litTitle.Text = _idiomaService.GetTranslation("ProductosMasCompradosTitle");
                litDescription.Text = _idiomaService.GetTranslation("ProductosMasCompradosDescription");
                btnGenerarXML.Text = _idiomaService.GetTranslation("GenerarXMLButton");
                btnGenerarReporte.Text = _idiomaService.GetTranslation("GenerarReporteButton");
            }
        }

        private void CargarAnios()
        {
            ddlAnio.Items.Clear();
            ddlAnio.Items.Add(new ListItem("2024", "2024"));
            ddlAnio.Items.Add(new ListItem("2025", "2025"));
        }

        protected void btnGenerarXML_Click(object sender, EventArgs e)
        {
            try
            {
                WebService.ReporteCompras servicio = new WebService.ReporteCompras();
                List<ProductoReporte> productosMasComprados = servicio.ObtenerProductosMasCompradosPorMesPorAnio();

                if (productosMasComprados == null || productosMasComprados.Count == 0)
                {
                    string script = $"mostrarNotificacionSinDatos('{_idiomaService.GetTranslation("SinDatosTitle")}', '{_idiomaService.GetTranslation("SinDatosText")}');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "noDataNotification", script, true);
                    return;
                }

                GenerarXML generadorXml = new GenerarXML();
                generadorXml.GenerarXMLProductosPorMesYAnio(productosMasComprados);

                string successScript = $"mostrarNotificacionXMLGenerado('{_idiomaService.GetTranslation("ReporteGeneradoTitle")}', '{_idiomaService.GetTranslation("ReporteGeneradoText")}');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "xmlGeneratedNotification", successScript, true);
            }
            catch (Exception ex)
            {
                lblMensaje.Visible = true;
                lblMensaje.CssClass = "text-danger";
                lblMensaje.Text = _idiomaService.GetTranslation(ex.Message);
            }
        }

        protected void btnGenerarReporte_Click(object sender, EventArgs e)
        {
            try
            {
                string anioSeleccionado = ddlAnio.SelectedValue;
                WebService.ReporteCompras servicio = new WebService.ReporteCompras();
                List<ProductoReporte> productos = ParseXmlData(servicio.ObtenerXMLReporte())
                    .Where(p => p.Anio.ToString() == anioSeleccionado)
                    .ToList();

                if (productos.Count == 0)
                {
                    string script1 = $"mostrarNotificacionSinDatos('{_idiomaService.GetTranslation("SinDatosTitle")}', '{_idiomaService.GetTranslation("SinDatosText")}');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "noDataNotification", script1, true);
                    return;
                }

                var productosPorMes = productos
                    .GroupBy(p => p.Mes)
                    .ToDictionary(g => g.Key, g => g.ToList());

                JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                string jsonData = jsSerializer.Serialize(productosPorMes);

                string script = $"renderCharts({jsonData}, '{_idiomaService.GetTranslation("ProductosVendidosEn")}');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "renderChartsScript", script, true);
            }
            catch (Exception ex)
            {
                lblMensaje.Visible = true;
                lblMensaje.CssClass = "text-danger";
                lblMensaje.Text = _idiomaService.GetTranslation(ex.Message);
            }
        }

        private List<ProductoReporte> ParseXmlData(string xmlData)
        {
            var productos = new List<ProductoReporte>();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlData);

            foreach (XmlNode anioNode in xmlDoc.SelectNodes("//Anio"))
            {
                int anio = int.Parse(anioNode.Attributes["valor"].Value);

                foreach (XmlNode mesNode in anioNode.SelectNodes("Mes"))
                {
                    string mes = mesNode.Attributes["nombre"].Value;

                    foreach (XmlNode productoNode in mesNode.SelectNodes("Producto"))
                    {
                        ProductoReporte producto = new ProductoReporte
                        {
                            Nombre = productoNode["Nombre"].InnerText,
                            VecesComprado = int.Parse(productoNode["VecesComprado"].InnerText),
                            Mes = mes,
                            Anio = anio
                        };
                        productos.Add(producto);
                    }
                }
            }

            return productos;
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
