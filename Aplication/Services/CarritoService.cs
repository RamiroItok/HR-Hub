using Aplication.Interfaces;
using Data.Interfaces;
using Models;
using System;
using System.Collections.Generic;

namespace Aplication.Services
{
    public class CarritoService : ICarritoService
    {
        private readonly ICarritoDAO _carritoDAO;
        private readonly IBitacoraService _bitacoraService;
        private readonly IDigitoVerificadorService _digitoVerificadorService;

        public CarritoService(ICarritoDAO carritoDAO, IDigitoVerificadorService digitoVerificadorService, IBitacoraService bitacoraService)
        {
            _carritoDAO = carritoDAO;
            _digitoVerificadorService = digitoVerificadorService;
            _bitacoraService = bitacoraService;
        }

        public void InsertarCarrito(int idProducto, Usuario userSession)
        {
            try
            {
                _carritoDAO.InsertarCarrito(idProducto, userSession.Id);
                _bitacoraService.AltaBitacora(userSession.Email, userSession.Puesto, "Agrego un producto al carrito", Models.Enums.Criticidad.BAJA);
                _digitoVerificadorService.CalcularDVTabla("Carrito");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Carrito> ObtenerCarrito(int idUsuario)
        {
            throw new NotImplementedException();
        }
    }
}