using Models;
using System.Collections.Generic;

namespace Aplication.Interfaces
{
    public interface ICompraService
    {
        int RealizarCompra(Compra compra, Usuario userSession);
        List<Compra> ObtenerCompras(int idUsuario);
        void GuardarDetalleCompra(DetalleCompra detalleCompra);
    }
}