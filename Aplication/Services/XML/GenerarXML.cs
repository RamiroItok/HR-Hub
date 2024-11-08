using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Aplication.Services.XML
{
    public class GenerarXML
    {
        public void GenerarXMLProductosPorMesYAnio(List<ProductoReporte> productos)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "ReporteProductosMasComprados.xml");
            Directory.CreateDirectory(Path.GetDirectoryName(path));

            using (XmlWriter writer = XmlWriter.Create(path, new XmlWriterSettings { Indent = true, Encoding = Encoding.UTF8 }))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("ReporteProductosMasComprados");

                var productosAgrupados = productos
                    .GroupBy(p => new { p.Anio, p.Mes })
                    .OrderByDescending(g => g.Key.Anio)
                    .ThenByDescending(g => g.Key.Mes);

                foreach (var grupo in productosAgrupados)
                {
                    writer.WriteStartElement("Anio");
                    writer.WriteAttributeString("valor", grupo.Key.Anio.ToString());

                    writer.WriteStartElement("Mes");
                    writer.WriteAttributeString("nombre", grupo.Key.Mes);

                    foreach (var producto in grupo)
                    {
                        writer.WriteStartElement("Producto");
                        writer.WriteElementString("Nombre", producto.Nombre);
                        writer.WriteElementString("VecesComprado", producto.VecesComprado.ToString());
                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement();
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        public void GenerarXMLBitacora(List<Bitacora> registros)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "ReporteBitacora.xml");
            Directory.CreateDirectory(Path.GetDirectoryName(path));

            using (XmlWriter writer = XmlWriter.Create(path, new XmlWriterSettings { Indent = true, Encoding = Encoding.UTF8 }))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Bitacora");

                foreach (var registro in registros)
                {
                    writer.WriteStartElement("Registro");
                    writer.WriteElementString("Id", registro.Id.ToString());
                    writer.WriteElementString("Email", registro.Email);
                    writer.WriteElementString("TipoUsuario", registro.TipoUsuario);
                    writer.WriteElementString("Descripcion", registro.Descripcion);
                    writer.WriteElementString("Fecha", registro.Fecha.ToString("yyyy-MM-dd HH:mm:ss"));
                    writer.WriteElementString("Criticidad", registro.Criticidad);
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        public void GenerarXMLFirmasDocumentos(List<DocumentoReporte> documentos)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "ReporteFirmasDocumentos.xml");
            Directory.CreateDirectory(Path.GetDirectoryName(path));

            using (XmlWriter writer = XmlWriter.Create(path, new XmlWriterSettings { Indent = true, Encoding = Encoding.UTF8 }))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("DocumentosFirmasReporte");

                foreach (var documento in documentos)
                {
                    writer.WriteStartElement("Documento");

                    writer.WriteElementString("IdDocumento", documento.IdDocumento.ToString());
                    writer.WriteElementString("NombreDocumento", documento.NombreDocumento);
                    writer.WriteElementString("TotalAsignados", documento.TotalAsignados.ToString());
                    writer.WriteElementString("TotalFirmados", documento.TotalFirmados.ToString());
                    writer.WriteElementString("TotalNoFirmados", documento.TotalNoFirmados.ToString());
                    writer.WriteElementString("PorcentajeFirmado", documento.PorcentajeFirmado.ToString("F2"));
                    writer.WriteElementString("PorcentajeNoFirmado", documento.PorcentajeNoFirmado.ToString("F2"));

                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }
    }
}