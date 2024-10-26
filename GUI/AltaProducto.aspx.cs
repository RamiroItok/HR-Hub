using Aplication.Interfaces;
using Models;
using System;
using System.IO;
using System.Web.UI.WebControls;
using Unity;

namespace GUI
{
    public partial class AltaProducto : System.Web.UI.Page
    {
        private readonly IProductoService _productoService;
        private readonly IEmpresaService _empresaService;

        public AltaProducto()
        {
            _productoService = Global.Container.Resolve<IProductoService>();
            _empresaService = Global.Container.Resolve<IEmpresaService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    CargarEmpresas();
                    CargarTipoProducto();
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Visible = true;
                lblMensaje.CssClass = "text-danger";
                lblMensaje.Text = ex.Message;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (fileProducto.HasFile || !string.IsNullOrEmpty(txtNombreProducto.Text) || !string.IsNullOrEmpty(txtDescripcion.Text) || !string.IsNullOrEmpty(txtCantidad.Text) || !string.IsNullOrEmpty(txtPrecioUnitario.Text))
                {
                    byte[] imagenBytes;
                    using (BinaryReader br = new BinaryReader(fileProducto.PostedFile.InputStream))
                    {
                        imagenBytes = br.ReadBytes(fileProducto.PostedFile.ContentLength);
                    }

                    Producto producto = new Producto()
                    {
                        Nombre = txtNombreProducto.Text,
                        IdEmpresa = int.Parse(DropDownEmpresa.Text),
                        Imagen = imagenBytes,
                        Descripcion = txtDescripcion.Text,
                        IdTipoProducto = int.Parse(DropDownTipoProducto.Text),
                        Cantidad = int.Parse(txtCantidad.Text),
                        PrecioUnitario = decimal.Parse(txtPrecioUnitario.Text)
                    };

                    var userSession = Session["Usuario"] as Usuario;
                    var id = _productoService.Registrar(producto, userSession);

                    lblMensaje.CssClass = "text-success";
                    lblMensaje.Text = "Producto registrado con éxito!";
                    lblMensaje.Visible = true;
                    Limpiar();
                }
                else
                {
                    lblMensaje.CssClass = "text-danger"; 
                    lblMensaje.Text = "Hay campos sin completar";
                    lblMensaje.Visible = true; 
                }
            }
            catch (Exception ex)
            {
                lblMensaje.CssClass = "text-danger";
                lblMensaje.Text = ex.Message;
                lblMensaje.Visible = true;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void CargarEmpresas()
        {
            DropDownEmpresa.DataSource = _empresaService.ObtenerEmpresas();
            DropDownEmpresa.DataTextField = "Nombre";
            DropDownEmpresa.DataValueField = "Id";
            DropDownEmpresa.DataBind();
            DropDownEmpresa.Items.Insert(0, new ListItem("Seleccione una empresa", ""));
        }

        private void CargarTipoProducto()
        {
            DropDownTipoProducto.DataSource = _productoService.ObtenerTipoProducto();
            DropDownTipoProducto.DataTextField = "Nombre";
            DropDownTipoProducto.DataValueField = "Id";
            DropDownTipoProducto.DataBind();
            DropDownTipoProducto.Items.Insert(0, new ListItem("Seleccione un tipo", ""));
        }

        private void Limpiar()
        {
            txtCantidad.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtNombreProducto.Text = string.Empty;
            txtPrecioUnitario.Text = string.Empty;
            DropDownEmpresa.SelectedIndex = 0;
            DropDownTipoProducto.SelectedIndex = 0;
        }
    }
}