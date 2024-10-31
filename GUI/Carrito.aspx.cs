using Aplication.Interfaces;
using Models;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unity;

namespace GUI
{
    public partial class Carrito : Page
    {
        private readonly ICarritoService _carritoService;
        public Carrito()
        {
            _carritoService = Global.Container.Resolve<ICarritoService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-AR");
            if (!IsPostBack)
            {
                CargarCarrito();
            }
        }

        private void CargarCarrito()
        {
            var userSession = Session["Usuario"] as Usuario;
            var carrito = _carritoService.ObtenerCarrito(userSession.Id);
            gvCarrito.DataSource = carrito;
            gvCarrito.DataBind();
            CalcularTotalCarrito();
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
    }
}