using Models;
using System.Collections.Generic;

namespace Aplication.Interfaces
{
    public interface ICompraService
    {
        int RealizarCompra(Compra compra, Usuario userSession);
        Compra ObtenerCompra(int idCompra);
        List<DetalleCompra> ObtenerDetalleCompra(int idCompra);
        List<Compra> ObtenerComprasPorUsuario(int idUsuario);
        void GuardarDetalleCompra(DetalleCompra detalleCompra);
        string CrearMensajeResumenCompra(Models.Compra compra, Usuario usuario, List<DetalleCompra> detallesCompra);
    }
}