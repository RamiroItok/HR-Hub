using Aplication.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unity;

namespace GUI
{
    public partial class Compra : Page
    {
        private readonly ICarritoService _carritoService;
        private readonly ICompraService _compraService;
        protected static List<Models.Carrito> carritoItems;

        public Compra()
        {
            _carritoService = Global.Container.Resolve<ICarritoService>();
            _compraService = Global.Container.Resolve<ICompraService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCarrito();
            }
        }

        private void CargarCarrito()
        {
            var userSession = Session["Usuario"] as Usuario;
            carritoItems = _carritoService.ObtenerCarrito(userSession.Id);
            CargarCarritoEnPantalla();
            CalcularTotalCarrito();
        }

        private void CargarCarritoEnPantalla()
        {
            gvCarrito.DataSource = carritoItems;
            gvCarrito.DataBind();
        }

        private void CalcularTotalCarrito()
        {
            decimal totalCarrito = 0;

            foreach (var item in carritoItems)
            {
                totalCarrito += item.Subtotal;
            }

            lblTotalCompra.Text = totalCarrito.ToString("C");
        }

        protected void btnPagar_Click(object sender, EventArgs e)
        {
            try
            {
                var usuario = Session["Usuario"] as Usuario;
                Models.Compra compra = new Models.Compra()
                {
                    IdUsuario = usuario.Id,
                    FechaPago = DateTime.Now,
                    Total = decimal.Parse(lblTotalCompra.Text, NumberStyles.Currency, CultureInfo.CurrentCulture)
                };

                var idCompra = _compraService.RealizarCompra(compra, usuario);

                foreach(var item in carritoItems)
                {
                    DetalleCompra detalleCompra = new DetalleCompra()
                    {
                        IdCompra = idCompra,
                        IdProducto = item.IdProducto,
                        NombreProducto = item.Producto.Nombre,
                        Cantidad = item.Cantidad,
                        PrecioUnitario = (decimal)item.Producto.PrecioUnitario,
                        Subtotal = (decimal)(item.Cantidad * item.Producto.PrecioUnitario)
                    };

                    _compraService.GuardarDetalleCompra(detalleCompra);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}