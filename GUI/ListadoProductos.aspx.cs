using Aplication.Interfaces;
using Aplication.Interfaces.Observer;
using Aplication.Services.Observer;
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
    public partial class ListadoProductos : Page, IIdiomaService
    {
        private readonly IEmpresaService _empresaService;
        private readonly IProductoService _productoService;
        private readonly IPermisoService _permisoService;
        private readonly IdiomaService _idiomaService;

        public ListadoProductos()
        {
            _empresaService = Global.Container.Resolve<IEmpresaService>();
            _productoService = Global.Container.Resolve<IProductoService>();
            _permisoService = Global.Container.Resolve<IPermisoService>();
            _idiomaService = Global.Container.Resolve<IdiomaService>();
            _idiomaService.Subscribe(this);
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

                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-AR");

                if (!IsPostBack)
                {
                    string selectedLanguage = Session["SelectedLanguage"] as string ?? "es";
                    ddlLanguage.SelectedValue = selectedLanguage;
                    _idiomaService.CurrentLanguage = selectedLanguage;
                    CargarProductos();
                    CargarEmpresas();
                    CargarTiposProducto();
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Visible = true;
                lblMensaje.CssClass = "text-danger";
                lblMensaje.Text = $"{_idiomaService.GetTranslation("MensajeErrorGeneral")}: {_idiomaService.GetTranslation(ex.Message)}";
            }
            finally
            {
                if (!IsPostBack)
                    CargarTextos();
            }
        }

        private void CargarTextos()
        {
            if (!(litPageTitle == null))
            {
                litPageTitle.Text = _idiomaService.GetTranslation("PageTitleListadoProductos");
                litTitle.Text = _idiomaService.GetTranslation("TituloListadoProductos");
                litModalTitle.Text = _idiomaService.GetTranslation("TituloModalModificarProducto");
                litNombreLabel.Text = _idiomaService.GetTranslation("LabelNombre");
                litEmpresaLabel.Text = _idiomaService.GetTranslation("LabelEmpresa");
                litDescripcionLabel.Text = _idiomaService.GetTranslation("LabelDescripcion");
                litTipoProductoLabel.Text = _idiomaService.GetTranslation("LabelTipoProducto");
                litCantidadLabelModal.Text = _idiomaService.GetTranslation("LabelCantidad");
                litPrecioUnitarioLabelModal.Text = _idiomaService.GetTranslation("LabelPrecioUnitario");
                litCloseButton.Text = _idiomaService.GetTranslation("BotonCerrar");
                btnGuardarCambios.Text = _idiomaService.GetTranslation("BotonGuardarCambios");
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

                    lblMensaje.Text = _idiomaService.GetTranslation("MensajeProductoEliminado");
                    lblMensaje.CssClass = string.Empty;
                    lblMensaje.CssClass = "alert alert-success";
                    lblMensaje.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = _idiomaService.GetTranslation(ex.Message);
                lblMensaje.CssClass = "alert alert-danger";
                lblMensaje.Visible = true;
            }
        }

        protected void rptProductos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var precioUnitarioLabel = e.Item.FindControl("precioUnitarioLabel") as Literal;
                if (precioUnitarioLabel != null)
                {
                    precioUnitarioLabel.Text = _idiomaService.GetTranslation("LabelPrecioUnitario");
                }

                var btnModificar = (Button)e.Item.FindControl("btnModificar");
                var btnEliminar = (Button)e.Item.FindControl("btnEliminar");

                if (btnModificar != null)
                    btnModificar.Text = _idiomaService.GetTranslation("BotonModificar");

                if (btnEliminar != null)
                    btnEliminar.Text = _idiomaService.GetTranslation("BotonEliminar");
            }
        }

        protected void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtNombreProducto.Text) || string.IsNullOrEmpty(txtCantidad.Text) || string.IsNullOrEmpty(txtDescripcion.Text) || string.IsNullOrEmpty(txtPrecioUnitario.Text))
                {
                    throw new Exception(_idiomaService.GetTranslation("MensajeErrorCamposFaltantes"));
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

                lblMensaje.Text = _idiomaService.GetTranslation("MensajeProductoModificado");
                lblMensaje.CssClass = "alert alert-success";
                lblMensaje.Visible = true;

                CargarProductos();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = _idiomaService.GetTranslation(ex.Message);
                lblMensaje.CssClass = "alert alert-danger";
                lblMensaje.Visible = true;
            }
        }

        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedLanguage = ddlLanguage.SelectedValue;
            Session["SelectedLanguage"] = selectedLanguage;
            _idiomaService.CurrentLanguage = selectedLanguage;
            CargarTextos();
            Response.Redirect(Request.RawUrl);
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