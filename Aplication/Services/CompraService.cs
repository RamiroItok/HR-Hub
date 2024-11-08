using Aplication.Interfaces;
using Data.DAO;
using Data.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;

namespace Aplication.Services
{
    public class CompraService : ICompraService
    {
        private readonly ICompraDAO _compraDAO;
        private readonly IDigitoVerificadorService _digitoVerificadorService;
        private readonly IBitacoraService _bitacoraService;
        private readonly IProductoService _productoService;

        public CompraService(IDigitoVerificadorService digitoVerificadorService, IBitacoraService bitacoraService, IProductoService productoService)
        {
            _compraDAO = new CompraDAO();
            _digitoVerificadorService = digitoVerificadorService;
            _bitacoraService = bitacoraService;
            _productoService = productoService;
        }

        public void GuardarDetalleCompra(DetalleCompra detalleCompra)
        {
            try
            {
                _compraDAO.GuardarDetalleCompra(detalleCompra);
                _digitoVerificadorService.CalcularDVTabla("DetalleCompra");
                _digitoVerificadorService.CalcularDVTabla("Producto");
            }
            catch (Exception ex) when (ex.Message.Contains("SQL") || ex.Message.Contains("BD"))
            {
                throw new Exception("ErrorBD");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Compra ObtenerCompra(int idCompra)
        {
            try
            {
                var resultado = _compraDAO.ObtenerCompraPorId(idCompra);
                if (resultado == null)
                    return null;

                return CompletarCompra(resultado);
            }
            catch (Exception ex) when (ex.Message.Contains("SQL") || ex.Message.Contains("BD"))
            {
                throw new Exception("ErrorBD");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<DetalleCompra> ObtenerDetalleCompra(int idCompra)
        {
            try
            {
                var resultado = _compraDAO.ObtenerDetalleCompra(idCompra);
                if (resultado == null || resultado.Tables.Count == 0 || resultado.Tables[0].Rows.Count == 0)
                    return null;

                List<DetalleCompra> detallesCompra = new List<DetalleCompra>();
                foreach (DataRow compra in resultado.Tables[0].Rows)
                {
                    DetalleCompra detalle = CompletarDetalleCompra(compra);
                    var prod = _productoService.ObtenerProductoPorId(detalle.IdProducto);
                    detalle.NombreProducto = prod.Nombre;
                    detallesCompra.Add(detalle);
                }

                return detallesCompra;
            }
            catch (Exception ex) when (ex.Message.Contains("SQL") || ex.Message.Contains("BD"))
            {
                throw new Exception("ErrorBD");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Compra> ObtenerComprasPorUsuario(int idUsuario)
        {
            try
            {
                var resultado = _compraDAO.ObtenerComprasPorUsuario(idUsuario);
                if (resultado == null || resultado.Tables.Count == 0 || resultado.Tables[0].Rows.Count == 0)
                    return null;

                List<Compra> listaCompras = new List<Compra>();
                foreach (DataRow compra in resultado.Tables[0].Rows)
                {
                    Compra detalle = CompletarCompra(compra);
                    listaCompras.Add(detalle);
                }

                return listaCompras;
            }
            catch (Exception ex) when (ex.Message.Contains("SQL") || ex.Message.Contains("BD"))
            {
                throw new Exception("ErrorBD");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private DetalleCompra CompletarDetalleCompra(DataRow row)
        {
            return new DetalleCompra()
            {
                IdCompra = Convert.ToInt32(row["IdCompra"]),
                IdProducto = Convert.ToInt32(row["IdProducto"]),
                Cantidad = Convert.ToInt32(row["Cantidad"]),
                PrecioUnitario = Convert.ToDecimal(row["PrecioUnitario"]),
                Subtotal = Convert.ToDecimal(row["Subtotal"])
            };
        }

        private Compra CompletarCompra(DataRow row)
        {
            return new Compra()
            {
                Id = Convert.ToInt32(row["Id"]),
                IdUsuario = (int)row["IdUsuario"],
                FechaPago = (DateTime)row["FechaPago"],
                Total = (decimal)row["Total"]
            };
        }

        private Compra CompletarCompra(DataSet tabla)
        {
            var usuario = new Compra()
            {
                Id = (int)tabla.Tables[0].Rows[0]["Id"],
                IdUsuario = (int)tabla.Tables[0].Rows[0]["IdUsuario"],
                FechaPago = (DateTime)tabla.Tables[0].Rows[0]["FechaPago"],
                Total = (decimal)tabla.Tables[0].Rows[0]["Total"]
            };

            return usuario;
        }

        public int RealizarCompra(Compra compra, Usuario userSession)
        {
            try
            {
                var resultado = _compraDAO.RealizarCompra(compra);
                _digitoVerificadorService.CalcularDVTabla("Compra");
                _bitacoraService.AltaBitacora(userSession.Email, userSession.Puesto, "El usuario realizo una compra", Models.Enums.Criticidad.ALTA);

                return resultado;
            }
            catch (Exception ex) when (ex.Message.Contains("SQL") || ex.Message.Contains("BD"))
            {
                throw new Exception("ErrorBD");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string CrearMensajeResumenCompra(Compra compra, Usuario usuario, List<DetalleCompra> detallesCompra)
        {
            string templatePath = HttpContext.Current.Server.MapPath("~/Templates/ResumenCompraTemplate.html");
            string templateContent = File.ReadAllText(templatePath);

            templateContent = templateContent.Replace("{{ID_COMPRA}}", compra.Id.ToString());
            templateContent = templateContent.Replace("{{ID_USUARIO}}", compra.IdUsuario.ToString());
            templateContent = templateContent.Replace("{{NOMBRE}}", $"{usuario.Nombre} {usuario.Apellido}");
            templateContent = templateContent.Replace("{{FECHA_COMPRA}}", compra.FechaPago.ToString("dd/MM/yyyy"));
            templateContent = templateContent.Replace("{{TOTAL_COMPRA}}", compra.Total.ToString("C"));

            string detallesHtml = "";
            foreach (var detalle in detallesCompra)
            {
                var producto = _productoService.ObtenerProductoPorId(detalle.IdProducto);
                detallesHtml += "<tr>";
                detallesHtml += $"<td>{producto.Nombre}</td>";
                detallesHtml += $"<td>{detalle.Cantidad}</td>";
                detallesHtml += $"<td>{detalle.PrecioUnitario:C}</td>";
                detallesHtml += $"<td>{detalle.Subtotal:C}</td>";
                detallesHtml += "</tr>";
            }

            templateContent = templateContent.Replace("{{DETALLES_COMPRA}}", detallesHtml);

            return templateContent;
        }
    }
}
