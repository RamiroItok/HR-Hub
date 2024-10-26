using Aplication.Interfaces;
using Data.Interfaces;
using Models;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoDAO _iProductoDAO;
        private readonly IDigitoVerificadorService _iDigitoVerificadorService;
        private readonly IBitacoraService _iBitacoraService;

        public ProductoService(IProductoDAO iProductoDAO, IDigitoVerificadorService digitoVerificadorService, IBitacoraService bitacoraService)
        {
            _iProductoDAO = iProductoDAO;
            _iDigitoVerificadorService = digitoVerificadorService;
            _iBitacoraService = bitacoraService;
        }

        public void Eliminar(Producto empresa, Usuario userSession)
        {
            throw new NotImplementedException();
        }

        public int Modificar(Producto empresa, Usuario userSession)
        {
            throw new NotImplementedException();
        }

        public DataTable ObtenerProductos()
        {
            throw new NotImplementedException();
        }

        public DataTable ObtenerTipoProducto()
        {
            try
            {
                var resultado = _iProductoDAO.ObtenerTipoProducto();

                return resultado.Tables[0];
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

        public int Registrar(Producto producto, Usuario userSession)
        {
            try
            {
                var id = _iProductoDAO.Registrar(producto);
                _iBitacoraService.AltaBitacora(userSession.Email, userSession.Puesto, $"Se registra el producto {producto.Nombre}", Criticidad.BAJA);
                _iDigitoVerificadorService.CalcularDVTabla("Producto");

                return id;
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
