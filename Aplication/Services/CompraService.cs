using Aplication.Interfaces;
using Data.DAO;
using Data.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace Aplication.Services
{
    public class CompraService : ICompraService
    {
        private readonly ICompraDAO _compraDAO;
        private readonly IDigitoVerificadorService _digitoVerificadorService;
        private readonly IBitacoraService _bitacoraService;

        public CompraService(IDigitoVerificadorService digitoVerificadorService, IBitacoraService bitacoraService)
        {
            _compraDAO = new CompraDAO();
            _digitoVerificadorService = digitoVerificadorService;
            _bitacoraService = bitacoraService;
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
                throw new Exception("Se ha perdido la conexión con la base de datos. Vuelva a intentar en unos minutos");
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
                var resultado = _compraDAO.ObtenerCompras(idCompra);
                if (resultado == null)
                    return null;

                return CompletarCompra(resultado);
            }
            catch (Exception ex) when (ex.Message.Contains("SQL") || ex.Message.Contains("BD"))
            {
                throw new Exception("Se ha perdido la conexión con la base de datos. Vuelva a intentar en unos minutos");
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
                    detallesCompra.Add(detalle);
                }

                return detallesCompra;
            }
            catch (Exception ex) when (ex.Message.Contains("SQL") || ex.Message.Contains("BD"))
            {
                throw new Exception("Se ha perdido la conexión con la base de datos. Vuelva a intentar en unos minutos");
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
                NombreProducto = row["NombreProducto"].ToString(),
                Cantidad = Convert.ToInt32(row["Cantidad"]),
                PrecioUnitario = Convert.ToDecimal(row["PrecioUnitario"]),
                Subtotal = Convert.ToDecimal(row["Subtotal"])
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
                throw new Exception("Se ha perdido la conexión con la base de datos. Vuelva a intentar en unos minutos");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
