using Aplication.Interfaces;
using Models;
using Models.Composite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unity;

namespace GUI
{
    public partial class Productos : Page
    {
        private readonly IProductoService _productoService;
        private readonly IEmpresaService _empresaService;
        private readonly ICarritoService _carritoService;
        private readonly IPermisoService _permisoService;
        private static List<Producto> listaProductos;

        public Productos()
        {
            _productoService = Global.Container.Resolve<IProductoService>();
            _empresaService = Global.Container.Resolve<IEmpresaService>();
            _carritoService = Global.Container.Resolve<ICarritoService>();
            _permisoService = Global.Container.Resolve<IPermisoService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var usuario = Session["Usuario"] as Usuario;
            if(!_permisoService.TienePermiso(usuario, Permiso.Productos))
            {
                Response.Redirect("AccesoDenegado.aspx");
                return;
            }

            if (!IsPostBack)
            {
                listaProductos = _productoService.ObtenerProductos().AsEnumerable().Select(row => new Producto
                {
                    Id = row.Field<int>("Id"),
                    Nombre = row.Field<string>("Nombre"),
                    Descripcion = row.Field<string>("Descripcion"),
                    Empresa = new Empresa()
                    {
                        Id = row.Field<int>("IdEmpresa"),
                        Nombre = row.Field<string>("NombreEmpresa"),
                    },
                    TipoProducto = new TipoProducto()
                    {
                        Id = row.Field<int>("IdTipoProducto"),
                        Nombre = row.Field<string>("NombreTipoProducto"),
                    },
                    Cantidad = row.Field<int>("Cantidad"),
                    PrecioUnitario = row.Field<decimal>("PrecioUnitario"),
                    Imagen = (byte[])row["Imagen"]
                }).ToList();

                CargarProductos(listaProductos);
                CargarTipoProducto();
                CargarEmpresas();
            }
        }

        private void CargarProductos(List<Producto> productosFiltrados)
        {
            ProductRepeater.DataSource = productosFiltrados;
            ProductRepeater.DataBind();
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            var productosFiltrados = listaProductos
                .Where(p =>
                    (string.IsNullOrEmpty(txtNombre.Text) || p.Nombre.ToLower().Contains(txtNombre.Text.ToLower())) &&
                    (string.IsNullOrEmpty(txtDescripcion.Text) || p.Descripcion.ToLower().Contains(txtDescripcion.Text.ToLower())) &&
                    (DropDownEmpresa.SelectedIndex == 0 || p.Empresa.Nombre.ToLower().Contains(DropDownEmpresa.SelectedItem.Text.ToLower())) &&
                    (DropDownTipoProducto.SelectedIndex == 0 || p.TipoProducto.Nombre.ToLower().Contains(DropDownTipoProducto.SelectedItem.Text.ToLower()))
                )
                .ToList();

            CargarProductos(productosFiltrados);
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
            DropDownTipoProducto.Items.Insert(0, new ListItem("Seleccione un tipo de producto", ""));
        }

        protected void AgregarAlCarrito_Command(object sender, CommandEventArgs e)
        {
            int idProducto = Convert.ToInt32(e.CommandArgument);
            var userSession = Session["Usuario"] as Usuario;
            _carritoService.InsertarCarrito(idProducto, userSession, null);
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "showCartModal", "showCartModal();", true);
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Limpiar();
            CargarProductos(listaProductos);
        }

        private void Limpiar()
        {
            txtDescripcion.Text = string.Empty;
            txtNombre.Text = string.Empty;
            DropDownEmpresa.SelectedIndex = 0;
            DropDownTipoProducto.SelectedIndex = 0;
        }
    }
}