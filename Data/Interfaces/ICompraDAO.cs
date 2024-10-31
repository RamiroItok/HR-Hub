using Models;
using System.Data;

namespace Data.Interfaces
{
    public interface ICompraDAO
    {
        int RealizarCompra(Compra compra);
        DataSet ObtenerCompras(int idUsuario);
        void GuardarDetalleCompra(DetalleCompra detalleCompra);
    }
}