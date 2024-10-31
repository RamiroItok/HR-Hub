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
    public class CarritoDAO : ICarritoDAO
    {
        private readonly Acceso _acceso;

        public CarritoDAO()
        {
            _acceso = Acceso.GetInstance;
        }

        public void EliminarProducto(int idCarrito)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@IdCarrito", idCarrito }
                };

                _acceso.ExecuteStoredProcedureReader("sp_d_productoCarrito", parameters);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void InsertarCarrito(int idProducto, int idUsuario, int? cantidad)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@IdProducto", idProducto },
                    { "@IdUsuario", idUsuario },
                    { "@Cantidad", cantidad },
                };

                _acceso.ExecuteStoredProcedureReader("sp_i_carrito", parameters);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void LimpiarCarrito(int idUsuario)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@IdUsuario", idUsuario }
                };

                _acceso.ExecuteStoredProcedureReader("sp_d_carrito", parameters);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet ObtenerCarrito(int idUsuario)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@IdUsuario", idUsuario }
                };

                return _acceso.ExecuteStoredProcedureReader("sp_s_carrito", parameters);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
