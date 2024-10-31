using Aplication.Interfaces;
using Data.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Data;

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

        public void EliminarProducto(int idCarrito, Usuario userSession)
        {
            try
            {
                _carritoDAO.EliminarProducto(idCarrito);
                _bitacoraService.AltaBitacora(userSession.Email, userSession.Puesto, "Elimino un producto del carrito", Models.Enums.Criticidad.BAJA);
                _digitoVerificadorService.CalcularDVTabla("Carrito");
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

        public void InsertarCarrito(int idProducto, Usuario userSession, int? cantidad)
        {
            try
            {
                _carritoDAO.InsertarCarrito(idProducto, userSession.Id, cantidad);
                _bitacoraService.AltaBitacora(userSession.Email, userSession.Puesto, "Agrego un producto al carrito", Models.Enums.Criticidad.BAJA);
                _digitoVerificadorService.CalcularDVTabla("Carrito");
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

        public void LimpiarCarrito(Usuario userSession, bool noEsCompra)
        {
            try
            {
                _carritoDAO.LimpiarCarrito(userSession.Id);
                if(noEsCompra)
                    _bitacoraService.AltaBitacora(userSession.Email, userSession.Puesto, "Se limpio el carrito", Models.Enums.Criticidad.BAJA);

                _digitoVerificadorService.CalcularDVTabla("Carrito");
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

        public List<Carrito> ObtenerCarrito(int idUsuario)
        {
            try
            {
                var resultado = _carritoDAO.ObtenerCarrito(idUsuario);
                List<Carrito> productosCarrito = new List<Carrito>();

                foreach (DataRow row in resultado.Tables[0].Rows)
                {
                    Carrito carrito = new Carrito()
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        Producto = new Producto()
                            {
                                Id = Convert.ToInt32(row["IdProducto"]),
                                Nombre = row["NombreProducto"].ToString(),
                                Imagen = (byte[])row["Imagen"],
                                Cantidad = Convert.ToInt32(row["CantidadProducto"]),
                                PrecioUnitario = Convert.ToDecimal(row["PrecioUnitario"])
                            },
                        Usuario = new Usuario()
                            {
                                Id = Convert.ToInt32(row["IdUsuario"])
                            },
                        Cantidad = Convert.ToInt32(row["Cantidad"])
                    };

                    productosCarrito.Add(carrito);
                }
                return productosCarrito;
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