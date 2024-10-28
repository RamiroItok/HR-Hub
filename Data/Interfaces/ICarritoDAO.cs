using Models;
using System.Data;

namespace Data.Interfaces
{
    public interface ICarritoDAO
    {
        void InsertarCarrito(int idProducto, int idUsuario);
        DataSet ObtenerCarrito(int idUsuario);
        void EliminarCarrito(int idCarrito);
        void LimpiarCarrito(int idCarrito);
    }
}