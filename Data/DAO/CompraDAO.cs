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

        public DataSet ObtenerCompraPorId(int idCompra)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@Id", idCompra }
                };

                return _acceso.ExecuteStoredProcedureReader("sp_s_compraId", parameters);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet ObtenerDetalleCompra(int idCompra)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@IdCompra", idCompra }
                };

                return _acceso.ExecuteStoredProcedureReader("sp_s_detalleCompraIdCompra", parameters);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet ObtenerComprasPorUsuario(int idUsuario)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@IdUsuario", idUsuario }
                };

                return _acceso.ExecuteStoredProcedureReader("sp_s_compras_porUsuario", parameters);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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
