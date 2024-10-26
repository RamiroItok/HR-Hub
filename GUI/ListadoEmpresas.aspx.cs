using Aplication.Interfaces;
using Models;
using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unity;

namespace GUI
{
    public partial class ListadoEmpresas : Page
    {
        private readonly IEmpresaService _empresaService;

        public ListadoEmpresas()
        {
            _empresaService = Global.Container.Resolve<IEmpresaService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarEmpresas();
            }
        }

        private void CargarEmpresas()
        {
            DataTable empresas = _empresaService.ObtenerEmpresas();
            rptEmpresas.DataSource = empresas;
            rptEmpresas.DataBind();
        }

        protected void btnEliminar_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                int idEmpresa = Convert.ToInt32(e.CommandArgument);

                Empresa empresa = _empresaService.ObtenerEmpresaPorId(idEmpresa);
                var userSession = Session["Usuario"] as Usuario;

                _empresaService.Eliminar(empresa, userSession);
                CargarEmpresas();

                lblMensaje.Text = "La empresa ha sido eliminada exitosamente.";
                lblMensaje.CssClass = string.Empty;
                lblMensaje.CssClass = "alert alert-success";
                lblMensaje.Visible = true;
            }
        }

        protected void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombreEmpresa.Text) || string.IsNullOrEmpty(txtURLEmpresa.Text))
            {
                lblMensaje.Text = "Error: Hay campos que faltan completar.";
                lblMensaje.CssClass = string.Empty;
                lblMensaje.CssClass = "alert alert-danger";
                lblMensaje.Visible = true;
                return;
            }

            byte[] logo = null;

            if (fileLogoEmpresa.HasFile)
            {
                using (BinaryReader br = new BinaryReader(fileLogoEmpresa.PostedFile.InputStream))
                {
                    logo = br.ReadBytes(fileLogoEmpresa.PostedFile.ContentLength);
                }
            }

            Empresa empresa = new Empresa()
            {
                Id = Convert.ToInt32(hfEmpresaId.Value),
                Nombre = string.IsNullOrEmpty(txtNombreEmpresa.Text) ? string.Empty : txtNombreEmpresa.Text,
                Logo = logo,
                URL = string.IsNullOrEmpty(txtURLEmpresa.Text) ? string.Empty : txtURLEmpresa.Text
            };

            var userSession = Session["Usuario"] as Usuario;
            _empresaService.Modificar(empresa, userSession);

            lblMensaje.Text = "La empresa ha sido modificada exitosamente.";
            lblMensaje.CssClass = string.Empty;
            lblMensaje.CssClass = "alert alert-success";
            lblMensaje.Visible = true;

            CargarEmpresas();
        }

        protected string EnsureUrl(string url)
        {
            if (!url.StartsWith("http://", StringComparison.OrdinalIgnoreCase) && !url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                url = "http://" + url;
            }
            return url;
        }
    }
}
