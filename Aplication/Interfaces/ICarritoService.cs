using Models;
using System.Collections.Generic;

namespace Aplication.Interfaces
{
    public interface ICarritoService
    {
        void InsertarCarrito(int idProducto, Usuario usuario, int? cantidad);
        List<Carrito> ObtenerCarrito(int idUsuario);
        void EliminarProducto(int idCarrito, Usuario userSession);
        void LimpiarCarrito(Usuario userSession, bool compra);
    }
}