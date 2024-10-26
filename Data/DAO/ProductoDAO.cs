using Data.Conexion;
using Data.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
                    { "@IdEmpresa", producto.IdEmpresa },
                    { "@Imagen", producto.Imagen },
                    { "@Descripcion", producto.Descripcion },
                    { "@IdTipoProducto", producto.IdTipoProducto },
                    { "@Cantidad", producto.Cantidad },
                    { "@PrecioUnitario", producto.PrecioUnitario }
                };

                DataSet resultado = _acceso.ExecuteStoredProcedureReader("sp_u_producto", parameters);

                return Convert.ToInt32(resultado.Tables[0].Rows[0]["Id"]);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet ObtenerProductos()
        {
            try
            {
                var resultado = _acceso.ExecuteStoredProcedureReader("sp_s_producto", null);
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet ObtenerTipoProducto()
        {
            try
            {
                DataSet resultado = _acceso.ExecuteStoredProcedureReader("sp_s_tipoProducto", null);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
                    { "@IdEmpresa", producto.IdEmpresa },
                    { "@IdTipoProducto", producto.IdTipoProducto },
                    { "@Cantidad", producto.Cantidad },
                    { "@PrecioUnitario", producto.PrecioUnitario },
                    { "@DVH", 0 }
                };

                DataSet resultado = _acceso.ExecuteStoredProcedureReader("sp_i_producto", parameters);

                return Convert.ToInt32(resultado.Tables[0].Rows[0]["Id"]);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
