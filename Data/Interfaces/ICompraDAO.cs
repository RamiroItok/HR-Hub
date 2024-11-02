using Models;
using System.Data;

namespace Data.Interfaces
{
    public interface ICompraDAO
    {
        int RealizarCompra(Compra compra);
        DataSet ObtenerCompraPorId(int idCompra);
        DataSet ObtenerDetalleCompra(int idCompra);
        DataSet ObtenerComprasPorUsuario(int idUsuario);
        void GuardarDetalleCompra(DetalleCompra detalleCompra);
    }
}