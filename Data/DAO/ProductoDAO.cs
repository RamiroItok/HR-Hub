using Data.Conexion;
using Data.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace Data.DAO
{
    public class ProductoDAO : IProductoDAO
    {
        private readonly Acceso _acceso;

        public ProductoDAO()
        {
            _acceso = Acceso.GetInstance;
        }

        public void Eliminar(int idProducto)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@Id", idProducto }
                };

                _acceso.ExecuteStoredProcedureReader("sp_d_producto", parameters);
            }
            catch (Exception)
            {
                throw new Exception("ErrorEliminarProducto");
            }
        }

        public int Modificar(Producto producto)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@Id", producto.Id },
                    { "@Nombre", producto.Nombre },
                    { "@IdEmpresa", producto.Empresa.Id },
                    { "@Imagen", producto.Imagen },
                    { "@Descripcion", producto.Descripcion },
                    { "@IdTipoProducto", producto.TipoProducto.Id },
                    { "@Cantidad", producto.Cantidad },
                    { "@PrecioUnitario", producto.PrecioUnitario }
                };

                DataSet resultado = _acceso.ExecuteStoredProcedureReader("sp_u_producto", parameters);

                return Convert.ToInt32(resultado.Tables[0].Rows[0]["Id"]);
            }
            catch (Exception)
            {
                throw new Exception("ErrorModificarProducto");
            }
        }

        public DataSet ObtenerProductos()
        {
            try
            {
                var resultado = _acceso.ExecuteStoredProcedureReader("sp_s_producto", null);
                return resultado;
            }
            catch (Exception)
            {
                throw new Exception("ErrorObtenerProductos");
            }
        }

        public DataSet ObtenerProductoPorId(int id)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@Id", id }
                };

                var resultado = _acceso.ExecuteStoredProcedureReader("sp_s_producto_porId", parameters);
                return resultado;
            }
            catch (Exception)
            {
                throw new Exception("ErrorObtenerProductoPorId");
            }
        }

        public DataSet ObtenerTipoProducto()
        {
            try
            {
                DataSet resultado = _acceso.ExecuteStoredProcedureReader("sp_s_tipoProducto", null);

                return resultado;
            }
            catch (Exception)
            {
                throw new Exception("ErrorObtenerTipoProductos");
            }
        }

        public DataSet ObtenerProductosMasCompradosPorMesPorAnio()
        {
            try
            {
                return _acceso.ExecuteStoredProcedureReader("sp_ObtenerProductosMasCompradosPorMesYAnio", null);
            }
            catch (Exception)
            {
                throw new Exception("ErrorObtenerProductosMasComprados");
            }
        }

        public int Registrar(Producto producto)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@Nombre", producto.Nombre },
                    { "@Descripcion", producto.Descripcion },
                    { "@Imagen", producto.Imagen },
                    { "@IdEmpresa", producto.Empresa.Id },
                    { "@IdTipoProducto", producto.TipoProducto.Id },
                    { "@Cantidad", producto.Cantidad },
                    { "@PrecioUnitario", producto.PrecioUnitario },
                    { "@DVH", 0 }
                };

                DataSet resultado = _acceso.ExecuteStoredProcedureReader("sp_i_producto", parameters);

                return Convert.ToInt32(resultado.Tables[0].Rows[0]["Id"]);
            }
            catch (Exception)
            {
                throw new Exception("ErrorRegistrarProducto");
            }
        }
    }
}
