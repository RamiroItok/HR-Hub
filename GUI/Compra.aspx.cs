using Aplication.Interfaces;
using Aplication.Services;
using GUI.WebService;
using Models;
using Models.Composite;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unity;

namespace GUI
{
    public partial class Compra : Page
    {
        private readonly ICarritoService _carritoService;
        private readonly ICompraService _compraService;
        private readonly IPermisoService _permisoService;
        private readonly EnviarMail _enviarMailService;
        protected static List<Models.Carrito> carritoItems;

        public Compra()
        {
            _carritoService = Global.Container.Resolve<ICarritoService>();
            _compraService = Global.Container.Resolve<ICompraService>();
            _permisoService = Global.Container.Resolve<IPermisoService>();
            _enviarMailService = new EnviarMail();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var usuario = Session["Usuario"] as Usuario;
            if(!_permisoService.TienePermiso(usuario, Permiso.Carrito))
            {
                Response.Redirect("AccesoDenegado.aspx");
                return;
            }
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

                CompraFactory compraFactory = new CompraProductoFactory();

                Models.Compra compra = compraFactory.CrearCompra(usuario.Id, decimal.Parse(lblTotalCompra.Text, NumberStyles.Currency));

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

                _enviarMailService.EnviarResumenCompraPorEmail(idCompra);
                _carritoService.LimpiarCarrito(usuario, false);

                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "showPaymentSuccess",
                    "Swal.fire({ " +
                    "  icon: 'success', " +
                    "  title: 'Pago realizado', " +
                    "  text: '¡El pago se ha realizado correctamente!', " +
                    "  confirmButtonText: 'Aceptar', " +
                    "  customClass: { popup: 'animated fadeInDown' } " +
                    "}).then((result) => { if (result.isConfirmed) { window.location.href = '/Carrito.aspx'; }});",
                    true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}