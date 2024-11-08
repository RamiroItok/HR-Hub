using Aplication.Interfaces;
using Aplication.Interfaces.Observer;
using Aplication.Services.Observer;
using Models;
using Models.Composite;
using System;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;
using Unity;

namespace GUI
{
    public partial class AltaProducto : System.Web.UI.Page, IIdiomaService
    {
        private readonly IProductoService _productoService;
        private readonly IEmpresaService _empresaService;
        private readonly IPermisoService _permisoService;
        private readonly IdiomaService _idiomaService;

        public AltaProducto()
        {
            _productoService = Global.Container.Resolve<IProductoService>();
            _empresaService = Global.Container.Resolve<IEmpresaService>();
            _permisoService = Global.Container.Resolve<IPermisoService>();
            _idiomaService = Global.Container.Resolve<IdiomaService>();
            _idiomaService.Subscribe(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var usuario = Session["Usuario"] as Usuario;
            if(!_permisoService.TienePermiso(usuario, Permiso.ConfiguracionProducto))
            {
                Response.Redirect("AccesoDenegado.aspx");
                return;
            }

            try
            {
                if (!IsPostBack)
                {
                    string selectedLanguage = Session["SelectedLanguage"] as string ?? "es";
                    ddlLanguage.SelectedValue = selectedLanguage;
                    _idiomaService.CurrentLanguage = selectedLanguage;
                    CargarEmpresas();
                    CargarTipoProducto();
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
                CargarTextos();
            }
        }

        private void CargarTextos()
        {
            if (!(tituloRegistro == null))
            {
                tituloRegistro.InnerText = _idiomaService.GetTranslation("TituloAltaProducto");
                Page.Title = _idiomaService.GetTranslation("PageTitleAltaProducto");
                lblNombreProducto.InnerText = _idiomaService.GetTranslation("LabelNombreProducto");
                txtNombreProducto.Attributes["placeholder"] = _idiomaService.GetTranslation("PlaceholderNombreProducto");
                lblEmpresa.Text = _idiomaService.GetTranslation("LabelEmpresa");
                lblImagenProducto.InnerText = _idiomaService.GetTranslation("LabelImagenProducto");
                lblDescripcion.InnerText = _idiomaService.GetTranslation("LabelDescripcion");
                txtDescripcion.Attributes["placeholder"] = _idiomaService.GetTranslation("PlaceholderDescripcion");
                lblTipo.Text = _idiomaService.GetTranslation("LabelTipo");
                lblCantidad.InnerText = _idiomaService.GetTranslation("LabelCantidad");
                txtCantidad.Attributes["placeholder"] = _idiomaService.GetTranslation("PlaceholderCantidad");
                lblPrecioUnitario.InnerText = _idiomaService.GetTranslation("LabelPrecioUnitario");
                txtPrecioUnitario.Attributes["placeholder"] = _idiomaService.GetTranslation("PlaceholderPrecioUnitario");
                btnSubmit.Text = _idiomaService.GetTranslation("ButtonRegistrar");
                btnCancel.Text = _idiomaService.GetTranslation("ButtonCancelar");
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
                        Empresa = new Empresa()
                            {
                                Id = int.Parse(DropDownEmpresa.Text)
                            },
                        Imagen = imagenBytes,
                        Descripcion = txtDescripcion.Text,
                        TipoProducto = new TipoProducto()
                            {
                                Id = int.Parse(DropDownTipoProducto.Text)
                            },
                        Cantidad = int.Parse(txtCantidad.Text),
                        PrecioUnitario = decimal.Parse(txtPrecioUnitario.Text)
                    };

                    var userSession = Session["Usuario"] as Usuario;
                    var id = _productoService.Registrar(producto, userSession);

                    lblMensaje.CssClass = "text-success";
                    lblMensaje.Text = _idiomaService.GetTranslation("MensajeRegistroExitosoProducto");
                    lblMensaje.Visible = true;
                    Limpiar();
                }
                else
                    throw new Exception("MensajeCamposIncompletos");
            }
            catch (Exception ex)
            {
                lblMensaje.CssClass = "text-danger";
                lblMensaje.Text = $"{_idiomaService.GetTranslation("MensajeErrorGeneral")}: {_idiomaService.GetTranslation(ex.Message)}";
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
            DropDownEmpresa.Items.Insert(0, new ListItem(_idiomaService.GetTranslation("DefaultSelectEmpresa"), ""));
        }

        private void CargarTipoProducto()
        {
            DropDownTipoProducto.DataSource = _productoService.ObtenerTipoProducto();
            DropDownTipoProducto.DataTextField = "Nombre";
            DropDownTipoProducto.DataValueField = "Id";
            DropDownTipoProducto.DataBind();
            DropDownTipoProducto.Items.Insert(0, new ListItem(_idiomaService.GetTranslation("DefaultSelectTipo"), ""));  
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

        public void UpdateLanguage(string language)
        {
            CargarTextos();
        }

        protected override void OnUnload(EventArgs e)
        {
            _idiomaService.Unsubscribe(this);
            base.OnUnload(e);
        }

        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedLanguage = ddlLanguage.SelectedValue;
            Session["SelectedLanguage"] = selectedLanguage;
            Response.Redirect(Request.RawUrl);
        }
    }
}