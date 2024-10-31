using Data.Conexion;
using Data.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace Data.DAO
{
    public class CompraDAO : ICompraDAO
    {
        private readonly Acceso _acceso;

        public CompraDAO()
        {
            _acceso = Acceso.GetInstance;
        }

        public void GuardarDetalleCompra(DetalleCompra detalleCompra)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@IdCompra", detalleCompra.IdCompra },
                    { "@IdProducto", detalleCompra.IdProducto },
                    { "@NombreProducto", detalleCompra.NombreProducto },
                    { "@Cantidad", detalleCompra.Cantidad },
                    { "@PrecioUnitario", detalleCompra.PrecioUnitario },
                    { "@Subtotal", detalleCompra.Subtotal }
                };

                _acceso.ExecuteStoredProcedureReader("sp_i_detalleCompra", parameters);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet ObtenerCompras(int idUsuario)
        {
            throw new NotImplementedException();
        }

        public int RealizarCompra(Compra compra)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@IdUsuario", compra.IdUsuario },
                    { "@FechaPago", compra.FechaPago },
                    { "@Total", compra.Total }
                };

                DataSet resultado = _acceso.ExecuteStoredProcedureReader("sp_i_compra", parameters);

                return Convert.ToInt32(resultado.Tables[0].Rows[0]["Id"]);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
