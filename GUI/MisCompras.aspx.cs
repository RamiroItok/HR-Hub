using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aplication.Interfaces;
using Aplication.Services;
using Models;
using Models.Composite;
using Unity;

namespace GUI
{
    public partial class MisCompras : Page
    {
        private readonly ICompraService _compraService;
        private readonly IUsuarioService _usuarioService;
        private readonly IPermisoService _permisoService;

        public MisCompras()
        {
            _compraService = Global.Container.Resolve<ICompraService>();
            _usuarioService = Global.Container.Resolve<IUsuarioService>();
            _permisoService = Global.Container.Resolve<IPermisoService>();
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
                CargarCompras();
            }

            if (Request.QueryString["DownloadPdf"] == "true" && int.TryParse(Request.QueryString["idCompra"], out int idCompra))
            {
                DescargarPdf(idCompra);
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
    }
}