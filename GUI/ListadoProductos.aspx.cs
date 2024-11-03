using Aplication.Interfaces;
using Models;
using Models.Composite;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unity;

namespace GUI
{
    public partial class ListadoProductos : Page
    {
        private readonly IEmpresaService _empresaService;
        private readonly IProductoService _productoService;
        private readonly IPermisoService _permisoService;

        public ListadoProductos()
        {
            _empresaService = Global.Container.Resolve<IEmpresaService>();
            _productoService = Global.Container.Resolve<IProductoService>();
            _permisoService = Global.Container.Resolve<IPermisoService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var usuario = Session["Usuario"] as Usuario;
                if (!_permisoService.TienePermiso(usuario, Permiso.ConfiguracionProducto))
                {
                    Response.Redirect("AccesoDenegado.aspx");
                    return;
                }

                if (!IsPostBack)
                {
                    CargarProductos();
                    CargarEmpresas();
                    CargarTiposProducto();
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = string.Empty;
                lblMensaje.CssClass = "alert alert-danger";
                lblMensaje.Visible = true;
            }
        }

        private void CargarEmpresas()
        {
            DropDownEmpresa.DataSource = _empresaService.ObtenerEmpresas();
            DropDownEmpresa.DataTextField = "Nombre";
            DropDownEmpresa.DataValueField = "Id";
            DropDownEmpresa.DataBind();
        }

        private void CargarTiposProducto()
        {
            DropDownTipoProducto.DataSource = _productoService.ObtenerTipoProducto();
            DropDownTipoProducto.DataTextField = "Nombre";
            DropDownTipoProducto.DataValueField = "Id";
            DropDownTipoProducto.DataBind();
        }

        private void CargarProductos()
        {
            DataTable productos = _productoService.ObtenerProductos();
            rptProductos.DataSource = productos;
            rptProductos.DataBind();
        }

        protected void btnEliminar_Command(object sender, CommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Eliminar")
                {
                    int idProducto = Convert.ToInt32(e.CommandArgument);

                    var producto = _productoService.ObtenerProductoPorId(idProducto);

                    var userSession = Session["Usuario"] as Usuario;

                    _productoService.Eliminar(producto, userSession);
                    CargarProductos();

                    lblMensaje.Text = "El producto ha sido eliminado correctamente.";
                    lblMensaje.CssClass = string.Empty;
                    lblMensaje.CssClass = "alert alert-success";
                    lblMensaje.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = string.Empty;
                lblMensaje.CssClass = "alert alert-danger";
                lblMensaje.Visible = true;
            }
        }

        protected void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtNombreProducto.Text) || string.IsNullOrEmpty(txtCantidad.Text) || string.IsNullOrEmpty(txtDescripcion.Text) || string.IsNullOrEmpty(txtPrecioUnitario.Text))
                {
                    throw new Exception("Hay campos sin completar");
                }

                byte[] imagen = null;

                if (fileImagenProducto.HasFile)
                {
                    using (BinaryReader br = new BinaryReader(fileImagenProducto.PostedFile.InputStream))
                    {
                        imagen = br.ReadBytes(fileImagenProducto.PostedFile.ContentLength);
                    }
                }

                Producto producto = new Producto()
                {
                    Id = Convert.ToInt32(IdProducto.Value),
                    Nombre = string.IsNullOrEmpty(txtNombreProducto.Text) ? string.Empty : txtNombreProducto.Text,
                    Empresa = new Empresa()
                    {
                        Id = (int)(string.IsNullOrEmpty(DropDownEmpresa.SelectedValue) ? (int?)null : int.Parse(DropDownEmpresa.SelectedValue))
                    },
                    Imagen = imagen,
                    Descripcion = string.IsNullOrEmpty(txtDescripcion.Text) ? string.Empty : txtDescripcion.Text,
                    TipoProducto = new TipoProducto()
                    {
                        Id = (int)(string.IsNullOrEmpty(DropDownTipoProducto.SelectedValue) ? (int?)null : int.Parse(DropDownTipoProducto.SelectedValue))
                    },
                    Cantidad = string.IsNullOrEmpty(txtCantidad.Text) ? (int?)null : int.Parse(txtCantidad.Text),
                    PrecioUnitario = string.IsNullOrEmpty(txtPrecioUnitario.Text) ? (decimal?)null : decimal.Parse(txtPrecioUnitario.Text)
                };

                var userSession = Session["Usuario"] as Usuario;
                _productoService.Modificar(producto, userSession);

                lblMensaje.Text = "El producto ha sido modificado correctamente";
                lblMensaje.CssClass = string.Empty;
                lblMensaje.CssClass = "alert alert-success";
                lblMensaje.Visible = true;

                CargarProductos();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = string.Empty;
                lblMensaje.CssClass = "alert alert-danger";
                lblMensaje.Visible = true;
            }
        }
    }
}