using Aplication.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.Services;
using Unity;

namespace GUI.WebService
{
    /// <summary>
    /// Descripción breve de ReporteCompras
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class ReporteCompras : System.Web.Services.WebService
    {
        private readonly IProductoService _productoService;

        public ReporteCompras()
        {
            _productoService = Global.Container.Resolve<IProductoService>();
        }

        [WebMethod]
        public List<ProductoReporte> ObtenerProductosMasCompradosPorMesPorAnio()
        {
            try
            {
                var resultado = _productoService.ObtenerProductosMasCompradosPorMesPorAnio();
                if (resultado == null || resultado.Tables.Count == 0 || resultado.Tables[0].Rows.Count == 0)
                    return null;

                List<ProductoReporte> listaProductos = new List<ProductoReporte>();
                foreach (DataRow compra in resultado.Tables[0].Rows)
                {
                    int numeroMes = Convert.ToInt32(compra["Mes"]);
                    string nombreMes = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(numeroMes);

                    ProductoReporte productoReporte = new ProductoReporte()
                    {
                        Nombre = compra["Nombre"].ToString(),
                        VecesComprado = Convert.ToInt32(compra["VecesComprado"]),
                        Mes = nombreMes,
                        Anio = Convert.ToInt32(compra["Anio"])
                    };
                    listaProductos.Add(productoReporte);
                }

                return listaProductos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [WebMethod]
        public string ObtenerXMLReporte()
        {
            string path = HttpContext.Current.Server.MapPath("~/App_Data/ReporteProductosMasComprados.xml");
            if (File.Exists(path))
            {
                return File.ReadAllText(path);
            }
            return null;
        }
    }
}