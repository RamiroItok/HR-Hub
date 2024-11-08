using Aplication.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using Unity;

namespace GUI.WebService
{
    /// <summary>
    /// Descripción breve de DocumentosService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class DocumentosService : System.Web.Services.WebService
    {
        private readonly IDocumentoService _documentoService;

        public DocumentosService()
        {
            _documentoService = Global.Container.Resolve<IDocumentoService>();
        }

        [WebMethod]
        public List<Models.DocumentoReporte> ObtenerPorcentajeFirmasPorDocumento()
        {
            DataSet ds = _documentoService.ObtenerPorcentajeFirmasPorDocumento();

            List<Models.DocumentoReporte> reporte = new List<Models.DocumentoReporte>();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Models.DocumentoReporte documento = new Models.DocumentoReporte
                    {
                        IdDocumento = Convert.ToInt32(row["IdDocumento"]),
                        NombreDocumento = row["NombreDocumento"].ToString(),
                        TotalAsignados = Convert.ToInt32(row["TotalAsignados"]),
                        TotalFirmados = Convert.ToInt32(row["TotalFirmados"]),
                        TotalNoFirmados = Convert.ToInt32(row["TotalNoFirmados"]),
                        PorcentajeFirmado = Convert.ToDecimal(row["PorcentajeFirmado"]),
                        PorcentajeNoFirmado = Convert.ToDecimal(row["PorcentajeNoFirmado"])
                    };

                    reporte.Add(documento);
                }
            }

            return reporte;
        }
    }
}
