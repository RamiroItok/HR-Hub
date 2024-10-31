using Models;
using System.Collections.Generic;

namespace Aplication.Interfaces
{
    public interface ICompraService
    {
        int RealizarCompra(Compra compra, Usuario userSession);
        Compra ObtenerCompra(int idCompra);
        List<DetalleCompra> ObtenerDetalleCompra(int idCompra);
        void GuardarDetalleCompra(DetalleCompra detalleCompra);
    }
}