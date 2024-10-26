using Models;
using System.Data;

namespace Aplication.Interfaces
{
    public interface IProductoService
    {
        int Registrar(Producto empresa, Usuario userSession);
        int Modificar(Producto empresa, Usuario userSession);
        void Eliminar(Producto empresa, Usuario userSession);
        DataTable ObtenerProductos();
        DataTable ObtenerTipoProducto();
    }
}