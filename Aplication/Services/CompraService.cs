using Aplication.Interfaces;
using Data.DAO;
using Data.Interfaces;
using Models;
using System;
using System.Collections.Generic;

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

        public List<Compra> ObtenerCompras(int idUsuario)
        {
            throw new NotImplementedException();
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
