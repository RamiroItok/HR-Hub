using Models;
using System.Data;

namespace Data.Interfaces
{
    public interface IProductoDAO
    {
        int Registrar(Producto producto);
        int Modificar(Producto producto);
        void Eliminar(Producto producto);
        DataSet ObtenerProductos();
        DataSet ObtenerTipoProducto();
    }
}