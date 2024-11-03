using Aplication.Services.XML;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace GUI
{
    public partial class ReporteCompras : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarAnios();
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
            WebService.ReporteCompras servicio = new WebService.ReporteCompras();
            List<ProductoReporte> productosMasComprados = servicio.ObtenerProductosMasCompradosPorMesPorAnio();

            if (productosMasComprados == null || productosMasComprados.Count == 0)
            {
                string noDataScript = "Swal.fire('Sin datos', 'No hay datos de productos para el reporte.', 'error');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "noDataScript", noDataScript, true);
                return;
            }

            GenerarXML generadorXml = new GenerarXML();
            generadorXml.GenerarXMLProductosPorMesYAnio(productosMasComprados);

            string successScript = "Swal.fire('Reporte Generado', 'El reporte XML ha sido generado exitosamente.', 'success');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "successScript", successScript, true);
        }

        protected void btnGenerarReporte_Click(object sender, EventArgs e)
        {
            string anioSeleccionado = ddlAnio.SelectedValue;
            WebService.ReporteCompras servicio = new WebService.ReporteCompras();
            List<ProductoReporte> productos = ParseXmlData(servicio.ObtenerXMLReporte())
                .Where(p => p.Anio.ToString() == anioSeleccionado)
                .ToList();

            if (productos.Count == 0)
            {
                string noDataScript = "Swal.fire('Sin datos', 'No hay datos para el año seleccionado.', 'error');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "noDataScript", noDataScript, true);
                return;
            }

            var productosPorMes = productos
                .GroupBy(p => p.Mes)
                .ToDictionary(g => g.Key, g => g.ToList());

            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            string jsonData = jsSerializer.Serialize(productosPorMes);

            string script = $"renderCharts({jsonData});";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "renderChartsScript", script, true);
        }

        private List<ProductoReporte> ParseXmlData(string xmlData)
        {
            var productos = new List<ProductoReporte>();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlData);

            // Iterar sobre cada nodo <Anio> en el XML
            foreach (XmlNode anioNode in xmlDoc.SelectNodes("//Anio"))
            {
                int anio = int.Parse(anioNode.Attributes["valor"].Value);

                // Dentro de cada <Anio>, iterar sobre cada nodo <Mes>
                foreach (XmlNode mesNode in anioNode.SelectNodes("Mes"))
                {
                    string mes = mesNode.Attributes["nombre"].Value;

                    // Dentro de cada <Mes>, iterar sobre cada nodo <Producto>
                    foreach (XmlNode productoNode in mesNode.SelectNodes("Producto"))
                    {
                        ProductoReporte producto = new ProductoReporte
                        {
                            Nombre = productoNode["Nombre"].InnerText,
                            VecesComprado = int.Parse(productoNode["VecesComprado"].InnerText),
                            Mes = mes,  // Guardamos el nombre del mes como string
                            Anio = anio // Guardamos el valor del año como int
                        };
                        productos.Add(producto);
                    }
                }
            }

            return productos;
        }
    }
}
