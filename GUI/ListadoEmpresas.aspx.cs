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
    public partial class ListadoEmpresas : Page, IIdiomaService
    {
        private readonly IEmpresaService _empresaService;
        private readonly IPermisoService _permisoService;
        private readonly IdiomaService _idiomaService;

        public ListadoEmpresas()
        {
            _empresaService = Global.Container.Resolve<IEmpresaService>();
            _permisoService = Global.Container.Resolve<IPermisoService>();
            _idiomaService = Global.Container.Resolve<IdiomaService>();
            _idiomaService.Subscribe(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var usuario = Session["Usuario"] as Usuario;
            if(!_permisoService.TienePermiso(usuario, Permiso.ConfiguracionEmpresas))
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
            if (!(litPageTitle == null))
            {
                litPageTitle.Text = _idiomaService.GetTranslation("PageTitleListadoEmpresas");
                litTitle.Text = _idiomaService.GetTranslation("TituloListadoEmpresas");
                litModalTitle.Text = _idiomaService.GetTranslation("TituloModalModificarEmpresa");
                litNombreLabel.Text = _idiomaService.GetTranslation("LabelNombre");
                litURLLabel.Text = _idiomaService.GetTranslation("LabelURL");
                litLogoLabel.Text = _idiomaService.GetTranslation("LabelLogo");
                btnGuardarCambios.Text = _idiomaService.GetTranslation("BotonGuardarCambios");
                litCloseButton.Text = _idiomaService.GetTranslation("BotonCerrar");
            }
        }

        protected void rptEmpresas_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var btnModificar = (LinkButton)e.Item.FindControl("btnModificar");
                var litModificar = (Literal)e.Item.FindControl("litModificar");
                var btnEliminar = (Button)e.Item.FindControl("btnEliminar"); 

                if (btnModificar != null && litModificar != null)
                {
                    litModificar.Text = _idiomaService.GetTranslation("BotonModificar");

                    var dataItem = (DataRowView)e.Item.DataItem;
                    var id = dataItem["Id"].ToString();
                    var nombre = dataItem["Nombre"].ToString();
                    var url = dataItem["URLEmpresa"].ToString();

                    btnModificar.OnClientClick = $"openModal('{id}', '{nombre}', '{url}'); return false;";
                }

                if (btnEliminar != null)
                {
                    btnEliminar.Text = _idiomaService.GetTranslation("BotonEliminar");
                }
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
            try
            {
                if (e.CommandName == "Eliminar")
                {
                    int idEmpresa = Convert.ToInt32(e.CommandArgument);

                    Empresa empresa = _empresaService.ObtenerEmpresaPorId(idEmpresa);
                    var userSession = Session["Usuario"] as Usuario;

                    _empresaService.Eliminar(empresa, userSession);
                    CargarEmpresas();

                    lblMensaje.Text = _idiomaService.GetTranslation("MensajeEmpresaEliminada");
                    lblMensaje.CssClass = string.Empty;
                    lblMensaje.CssClass = "alert alert-success";
                    lblMensaje.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Visible = true;
                lblMensaje.CssClass = "text-danger";
                lblMensaje.Text = $"{_idiomaService.GetTranslation("MensajeErrorGeneral")}: {_idiomaService.GetTranslation(ex.Message)}";
            }
        }

        protected void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtNombreEmpresa.Text) || string.IsNullOrEmpty(txtURLEmpresa.Text))
                {
                    lblMensaje.Text = _idiomaService.GetTranslation("MensajeErrorCamposFaltantes");
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

                lblMensaje.Text = _idiomaService.GetTranslation("MensajeEmpresaModificada");
                lblMensaje.CssClass = string.Empty;
                lblMensaje.CssClass = "alert alert-success";
                lblMensaje.Visible = true;

                CargarEmpresas();
            }
            catch (Exception ex)
            {
                lblMensaje.Visible = true;
                lblMensaje.CssClass = "text-danger";
                lblMensaje.Text = $"{_idiomaService.GetTranslation("MensajeErrorGeneral")}: {_idiomaService.GetTranslation(ex.Message)}";
            }
        }

        protected string EnsureUrl(string url)
        {
            if (!url.StartsWith("http://", StringComparison.OrdinalIgnoreCase) && !url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                url = "http://" + url;
            }
            return url;
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
