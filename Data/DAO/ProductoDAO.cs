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

        public void Eliminar(Producto producto)
        {
            throw new NotImplementedException();
        }

        public int Modificar(Producto producto)
        {
            throw new NotImplementedException();
        }

        public DataSet ObtenerProductos()
        {
            throw new NotImplementedException();
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
