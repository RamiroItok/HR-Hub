using Models;
using System.Collections.Generic;
using System.Data;

namespace Aplication.Interfaces
{
    public interface IProductoService
    {
        int Registrar(Producto producto, Usuario userSession);
        int Modificar(Producto producto, Usuario userSession);
        void Eliminar(Producto producto, Usuario userSession);
        DataTable ObtenerProductos();
        Producto ObtenerProductoPorId(int id);
        DataTable ObtenerTipoProducto();
        DataSet ObtenerProductosMasCompradosPorMesPorAnio();
    }
}