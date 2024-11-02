using Models;
using System;

namespace Aplication.Services
{
    public abstract class CompraFactory
    {
        public abstract Compra CrearCompra(int idUsuario, decimal total);
    }

    public class CompraProductoFactory : CompraFactory
    {
        public override Compra CrearCompra(int idUsuario, decimal total)
        {
            return new CompraProducto
            {
                IdUsuario = idUsuario,
                FechaPago = DateTime.Now,
                Total = total
            };
        }
    }
}