using Models;
using System.Data;

namespace Data.Interfaces
{
    public interface IProductoDAO
    {
        int Registrar(Producto producto);
        int Modificar(Producto producto);
        void Eliminar(int idProducto);
        DataSet ObtenerProductos();
        DataSet ObtenerProductoPorId(int id);
        DataSet ObtenerTipoProducto();

    }
}