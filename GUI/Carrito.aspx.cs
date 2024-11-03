using Aplication.Interfaces;
using Models;
using Models.Composite;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unity;

namespace GUI
{
    public partial class Carrito : Page
    {
        private readonly ICarritoService _carritoService;
        private readonly IPermisoService _permisoService;
        private readonly IProductoService _productoService;
        public Carrito()
        {
            _carritoService = Global.Container.Resolve<ICarritoService>();
            _permisoService = Global.Container.Resolve<IPermisoService>();
            _productoService = Global.Container.Resolve<IProductoService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var usuario = Session["Usuario"] as Usuario;
            if(!_permisoService.TienePermiso(usuario, Permiso.Carrito))
            {
                Response.Redirect("AccesoDenegado.aspx");
                return;
            }

            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-AR");
            if (!IsPostBack)
            {
                CargarCarrito();
            }
        }

        protected void CargarCarrito()
        {
            var userSession = Session["Usuario"] as Usuario;
            var carrito = _carritoService.ObtenerCarrito(userSession.Id);

            if (carrito == null || carrito.Count == 0)
            {
                pnlCarritoVacio.Visible = true;
                gvCarrito.Visible = false;
                lblTotalCarrito.Text = "$0.00";
                btnLimpiarCarrito.Visible = false;
                btnFinalizarCompra.Visible = false;
            }
            else
            {
                pnlCarritoVacio.Visible = false;
                gvCarrito.Visible = true;
                btnLimpiarCarrito.Visible = true;
                btnFinalizarCompra.Visible = true;

                gvCarrito.DataSource = carrito;
                gvCarrito.DataBind();

                foreach (GridViewRow row in gvCarrito.Rows)
                {
                    var txtCantidad = (TextBox)row.FindControl("txtCantidad");
                    var productoId = (int)gvCarrito.DataKeys[row.RowIndex].Value;

                    var producto = _productoService.ObtenerProductoPorId(productoId);

                    txtCantidad.Attributes["data-stock"] = producto.Cantidad.ToString();
                }

                CalcularTotalCarrito();
            }
        }

        protected void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            TextBox txtCantidad = (TextBox)sender;
            GridViewRow row = (GridViewRow)txtCantidad.NamingContainer;

            int idProducto = Convert.ToInt32(gvCarrito.DataKeys[row.RowIndex].Value);

            var userSession = Session["Usuario"] as Usuario;

            _carritoService.InsertarCarrito(idProducto, userSession, int.Parse(txtCantidad.Text));

            CargarCarrito();

            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "showModificacionPopup", "mostrarNotificacionModificacion();", true);
        }

        protected void gvCarrito_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int cantidad = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Cantidad"));
                decimal precioUnitario = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Producto.PrecioUnitario"));
                decimal subtotal = cantidad * precioUnitario;

                Label lblSubtotal = (Label)e.Row.FindControl("lblSubtotal");
                lblSubtotal.Text = subtotal.ToString("C");
            }
        }

        private void CalcularTotalCarrito()
        {
            decimal totalCarrito = 0;
            foreach (GridViewRow row in gvCarrito.Rows)
            {
                Label lblSubtotal = (Label)row.FindControl("lblSubtotal");
                if (lblSubtotal != null)
                {
                    totalCarrito += decimal.Parse(lblSubtotal.Text, System.Globalization.NumberStyles.Currency);
                }
            }
            lblTotalCarrito.Text = totalCarrito.ToString("C");
        }

        protected void gvCarrito_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                var userSession = Session["Usuario"] as Usuario;
                int idCarrito = Convert.ToInt32(e.CommandArgument);
                _carritoService.EliminarProducto(idCarrito, userSession);
                CargarCarrito();

                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "showEliminacionPopup", "mostrarNotificacionEliminacion();", true);
            }
        }

        public string ConvertirImagenABase64(byte[] imagen)
        {
            if (imagen == null || imagen.Length == 0)
                return "";

            return "data:image/png;base64," + Convert.ToBase64String(imagen);
        }

        protected void btnFinalizarCompra_Click(object sender, EventArgs e)
        {
            var userSession = Session["Usuario"] as Usuario;
            Response.Redirect($"Compra.aspx?carritoId={userSession.Id}");
        }

        protected void btnLimpiarCarrito_Click(object sender, EventArgs e)
        {
            try
            {
                var userSession = Session["Usuario"] as Usuario;
                _carritoService.LimpiarCarrito(userSession, true);

                CargarCarrito();

                ScriptManager.RegisterStartupScript(this, GetType(), "carritoVaciado", "mostrarNotificacionCarritoVacio();", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "errorLimpiarCarrito", $"alert('Error al limpiar el carrito: {ex.Message}');", true);
            }
        }
    }
}