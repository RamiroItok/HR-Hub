﻿using Aplication.Interfaces;
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

        public void Eliminar(Producto producto, Usuario userSession)
        {
            try
            {
                _iProductoDAO.Eliminar(producto.Id);
                _iBitacoraService.AltaBitacora(userSession.Email, userSession.Puesto, $"Se da de baja el producto {producto.Nombre}", Criticidad.MEDIA);
                _iDigitoVerificadorService.CalcularDVTabla("Producto");
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

        public int Modificar(Producto producto, Usuario userSession)
        {
            try
            {
                var id = _iProductoDAO.Modificar(producto);
                _iBitacoraService.AltaBitacora(userSession.Email, userSession.Puesto, $"Se modifican los datos del producto {producto.Nombre}", Criticidad.MEDIA);
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

        public DataTable ObtenerProductos()
        {
            try
            {
                var resultado = _iProductoDAO.ObtenerProductos();

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

        public Producto ObtenerProductoPorId(int id)
        {
            try
            {
                var resultado = _iProductoDAO.ObtenerProductoPorId(id);

                if (resultado != null && resultado.Tables.Count > 0 && resultado.Tables[0].Rows.Count > 0)
                {
                    var fila = resultado.Tables[0].Rows[0];

                    Producto producto = new Producto()
                    {
                        Id = (int)fila["Id"],
                        Nombre = (string)fila["Nombre"],
                        Empresa = new Empresa()
                            {
                                Id = (int)fila["IdEmpresa"],
                                Nombre = (string)fila["NombreEmpresa"]
                            },
                        Imagen = (byte[])fila["Imagen"],
                        Descripcion = (string)fila["Descripcion"],
                        TipoProducto = new TipoProducto()
                            {
                                Id = (int)fila["IdTipoProducto"],
                                Nombre = (string)fila["NombreTipoProducto"]
                            },
                        Cantidad = (int?)fila["Cantidad"],
                        PrecioUnitario = (decimal?)fila["PrecioUnitario"]
                    };

                    return producto;
                }

                return null;
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
