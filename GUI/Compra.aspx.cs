using Aplication.Interfaces;
using Aplication.Interfaces.Observer;
using Aplication.Services;
using Aplication.Services.Observer;
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
    public partial class Compra : Page, IIdiomaService
    {
        private readonly ICarritoService _carritoService;
        private readonly ICompraService _compraService;
        private readonly IPermisoService _permisoService;
        private readonly IdiomaService _idiomaService;
        private readonly EnviarMail _enviarMailService;
        protected static List<Models.Carrito> carritoItems;

        public Compra()
        {
            _carritoService = Global.Container.Resolve<ICarritoService>();
            _compraService = Global.Container.Resolve<ICompraService>();
            _permisoService = Global.Container.Resolve<IPermisoService>();
            _idiomaService = Global.Container.Resolve<IdiomaService>();
            _idiomaService.Subscribe(this);
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
                string selectedLanguage = Session["SelectedLanguage"] as string ?? "es";
                _idiomaService.CurrentLanguage = selectedLanguage;
                CargarTextos();
            }
        }

        private void CargarTextos()
        {
            Page.Title = _idiomaService.GetTranslation("PageTitleCompra");
            lblCompraResumenTitle.Text = _idiomaService.GetTranslation("CompraResumenTitle");
            lblTotalCompraLabel.Text = _idiomaService.GetTranslation("TotalCompraLabel");
            lblPaymentDetailsTitle.Text = _idiomaService.GetTranslation("PaymentDetailsTitle");
            lblCardNumberLabel.Text = _idiomaService.GetTranslation("CardNumberLabel");
            lblCardHolderLabel.Text = _idiomaService.GetTranslation("CardHolderLabel");
            lblExpiryDateLabel.Text = _idiomaService.GetTranslation("ExpiryDateLabel");
            lblCVCLabel.Text = _idiomaService.GetTranslation("CVCLabel");
            btnPagar.Text = _idiomaService.GetTranslation("ButtonPay");
            txtNumeroTarjeta.Attributes["placeholder"] = _idiomaService.GetTranslation("PlaceholderCardNumber");
            txtNombreTitular.Attributes["placeholder"] = _idiomaService.GetTranslation("PlaceholderCardHolder");
            txtFechaVencimiento.Attributes["placeholder"] = _idiomaService.GetTranslation("PlaceholderExpiryDate");
            txtCVC.Attributes["placeholder"] = _idiomaService.GetTranslation("PlaceholderCVC");

            gvCarrito.Columns[0].HeaderText = _idiomaService.GetTranslation("ColumnProduct");
            gvCarrito.Columns[1].HeaderText = _idiomaService.GetTranslation("ColumnPrice");
            gvCarrito.Columns[2].HeaderText = _idiomaService.GetTranslation("ColumnQuantity");
            gvCarrito.Columns[3].HeaderText = _idiomaService.GetTranslation("ColumnSubtotal");

            gvCarrito.DataBind();
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
                        Cantidad = item.Cantidad,
                        PrecioUnitario = (decimal)item.Producto.PrecioUnitario,
                        Subtotal = (decimal)(item.Cantidad * item.Producto.PrecioUnitario)
                    };

                    _compraService.GuardarDetalleCompra(detalleCompra);
                }

                _enviarMailService.EnviarResumenCompraPorEmail(idCompra);
                _carritoService.LimpiarCarrito(usuario, false);

                MostrarNotificacionPagoExitoso();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void MostrarNotificacionPagoExitoso()
        {
            string title = _idiomaService.GetTranslation("PaymentSuccessTitle");
            string message = _idiomaService.GetTranslation("PaymentSuccessMessage");
            string confirmButton = _idiomaService.GetTranslation("ConfirmButtonText");

            string script = $@"
                Swal.fire({{
                    icon: 'success',
                    title: '{title}',
                    text: '{message}',
                    confirmButtonText: '{confirmButton}',
                    customClass: {{ popup: 'animated fadeInDown' }}
                }}).then((result) => {{ if (result.isConfirmed) {{ window.location.href = '/Carrito.aspx'; }} }});";

            ScriptManager.RegisterStartupScript(this, GetType(), "showPaymentSuccess", script, true);
        }

        public void UpdateLanguage(string language)
        {
            CargarTextos();
        }

        protected override void OnUnload(EventArgs e)
        {
            _idiomaService.Unsubscribe(this);
            base.OnUnload(e);
        }
    }
}