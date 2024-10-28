using Models;
using System.Data;

namespace Data.Interfaces
{
    public interface ICarritoDAO
    {
        void InsertarCarrito(int idProducto, int idUsuario, int? cantidad);
        DataSet ObtenerCarrito(int idUsuario);
        void EliminarProducto(int idCarrito);
        void LimpiarCarrito(int idCarrito);
    }
}