using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aplication.Interfaces;
using Aplication.Interfaces.Observer;
using Aplication.Services;
using Aplication.Services.Observer;
using Models;
using Models.Composite;
using Unity;

namespace GUI
{
    public partial class MisCompras : Page, IIdiomaService
    {
        private readonly ICompraService _compraService;
        private readonly IUsuarioService _usuarioService;
        private readonly IPermisoService _permisoService;
        private readonly IdiomaService _idiomaService;

        public MisCompras()
        {
            _compraService = Global.Container.Resolve<ICompraService>();
            _usuarioService = Global.Container.Resolve<IUsuarioService>();
            _permisoService = Global.Container.Resolve<IPermisoService>();
            _idiomaService = Global.Container.Resolve<IdiomaService>();
            _idiomaService.Subscribe(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var usuario = Session["Usuario"] as Usuario;
            if(!_permisoService.TienePermiso(usuario, Permiso.MisCompras))
            {
                Response.Redirect("AccesoDenegado.aspx");
                return;
            }

            if (!IsPostBack)
            {
                string selectedLanguage = Session["SelectedLanguage"] as string ?? "es";
                ddlLanguage.SelectedValue = selectedLanguage;
                _idiomaService.CurrentLanguage = selectedLanguage;
                CargarTextos();
                CargarCompras();
            }

            if (Request.QueryString["DownloadPdf"] == "true" && int.TryParse(Request.QueryString["idCompra"], out int idCompra))
            {
                DescargarPdf(idCompra);
            }
        }

        public string btnResumenText { get; set; }

        private void CargarTextos()
        {
            if (!(lblTituloMisCompras == null))
            {
                lblTituloMisCompras.Text = _idiomaService.GetTranslation("TituloMisCompras");
                lblModalTituloResumenCompra.Text = _idiomaService.GetTranslation("ModalTituloResumenCompra");
                lblLabelIDCompra.Text = _idiomaService.GetTranslation("LabelIDCompra");
                lblLabelFechaCompra.Text = _idiomaService.GetTranslation("LabelFechaCompra");
                lblLabelTotalCompra.Text = _idiomaService.GetTranslation("LabelTotalCompra");
                lblButtonCerrar.Text = _idiomaService.GetTranslation("ButtonCerrar");
                btnDescargarPdf.Text = _idiomaService.GetTranslation("ButtonDescargarPdf");
                btnResumenText = _idiomaService.GetTranslation("ButtonVerResumen");

                gvCompras.Columns[0].HeaderText = _idiomaService.GetTranslation("HeaderID");
                gvCompras.Columns[1].HeaderText = _idiomaService.GetTranslation("HeaderFechaCompra");
                gvCompras.Columns[2].HeaderText = _idiomaService.GetTranslation("HeaderTotal");
                gvDetallesCompra.Columns[0].HeaderText = _idiomaService.GetTranslation("HeaderProducto");
                gvDetallesCompra.Columns[1].HeaderText = _idiomaService.GetTranslation("HeaderCantidad");
                gvDetallesCompra.Columns[2].HeaderText = _idiomaService.GetTranslation("HeaderPrecioUnitario");
                gvDetallesCompra.Columns[3].HeaderText = _idiomaService.GetTranslation("HeaderSubtotal");
            }
        }

        private void CargarCompras()
        {
            var userSession = Session["Usuario"] as Usuario;

            if (userSession == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            List<Models.Compra> listadoCompras = _compraService.ObtenerComprasPorUsuario(userSession.Id);
            gvCompras.DataSource = listadoCompras;
            gvCompras.DataBind();
        }

        protected void gvCompras_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "VerResumen")
            {
                int idCompra = int.Parse(e.CommandArgument.ToString());
                MostrarResumenCompra(idCompra);
            }
        }

        private void MostrarResumenCompra(int idCompra)
        {
            var compra = _compraService.ObtenerCompra(idCompra);
            var usuario = _usuarioService.ObtenerUsuarioPorId(compra.IdUsuario);
            var detallesCompra = _compraService.ObtenerDetalleCompra(idCompra);

            lblIdCompra.Text = compra.Id.ToString();
            lblFechaCompra.Text = compra.FechaPago.ToString("dd/MM/yyyy");
            lblTotalCompra.Text = compra.Total.ToString("C");

            gvDetallesCompra.DataSource = detallesCompra;
            gvDetallesCompra.DataBind();

            ScriptManager.RegisterStartupScript(this, GetType(), "ShowModalScript", "showResumenCompraModal();", true);
        }

        private void DescargarPdf(int idCompra)
        {
            var compra = _compraService.ObtenerCompra(idCompra);
            var usuario = _usuarioService.ObtenerUsuarioPorId(compra.IdUsuario);
            var detallesCompra = _compraService.ObtenerDetalleCompra(idCompra);

            GenerarPDFService generarPDFService = new GenerarPDFService();
            byte[] pdfBytes = generarPDFService.GenerarPdf(compra, usuario, detallesCompra);

            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", $"attachment; filename=ResumenCompra_{idCompra}.pdf");
            Response.BinaryWrite(pdfBytes);
            Response.Flush();
            Response.End();
        }

        protected override void OnUnload(EventArgs e)
        {
            _idiomaService.Unsubscribe(this);
            base.OnUnload(e);
        }

        public void UpdateLanguage(string language)
        {
            CargarTextos();
        }

        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedLanguage = ddlLanguage.SelectedValue;
            ddlLanguage.SelectedValue = selectedLanguage;
            Session["SelectedLanguage"] = selectedLanguage;
            CargarTextos();
            Response.Redirect(Request.RawUrl);
        }
    }
}