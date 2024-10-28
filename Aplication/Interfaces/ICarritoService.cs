using Models;
using System.Collections.Generic;

namespace Aplication.Interfaces
{
    public interface ICarritoService
    {
        void InsertarCarrito(int idProducto, Usuario usuario);
        List<Carrito> ObtenerCarrito(int idUsuario);
    }
}