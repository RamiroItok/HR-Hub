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

        public void EliminarCarrito(int idCarrito)
        {
            throw new NotImplementedException();
        }

        public void InsertarCarrito(int idProducto, int idUsuario)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@IdProducto", idProducto },
                    { "@IdUsuario", idUsuario }
                };

                _acceso.ExecuteStoredProcedureReader("sp_i_carrito", parameters);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void LimpiarCarrito(int idCarrito)
        {
            throw new NotImplementedException();
        }

        public DataSet ObtenerCarrito(int idUsuario)
        {
            throw new NotImplementedException();
        }
    }
}
