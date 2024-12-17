using Aplication.Interfaces;
using Aplication.Interfaces.Observer;
using Aplication.Services.Observer;
using Models;
using Models.Composite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unity;

namespace GUI
{
    public partial class GestionPermisosUsuario : Page, IIdiomaService
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IPermisoService _permisoService;
        private readonly IdiomaService _idiomaService;

        public GestionPermisosUsuario()
        {
            _usuarioService = Global.Container.Resolve<IUsuarioService>();
            _permisoService = Global.Container.Resolve<IPermisoService>();
            _idiomaService = Global.Container.Resolve<IdiomaService>();
            _idiomaService.Subscribe(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var usuario = Session["Usuario"] as Usuario;
            if(!_permisoService.TienePermiso(usuario, Permiso.GestionPermisosUsuario))
            {
                Response.Redirect("AccesoDenegado.aspx");
                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            try
            {
                if (!IsPostBack)
                {
                    CargarUsuarios();
                    string selectedLanguage = Session["SelectedLanguage"] as string ?? "es";
                    ddlLanguage.SelectedValue = selectedLanguage;
                    _idiomaService.CurrentLanguage = selectedLanguage;
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
                litPageTitle.Text = _idiomaService.GetTranslation("PageTitleGestionPermisosUsuario");
                litTitle.Text = _idiomaService.GetTranslation("TituloGestionPermisosUsuario");
                litSelectUserLabel.Text = _idiomaService.GetTranslation("LabelSeleccionarUsuario");
                litUnassignedPermissionsTitle.Text = _idiomaService.GetTranslation("TituloPermisosNoAsignados");
                litAssignedPermissionsTitle.Text = _idiomaService.GetTranslation("TituloPermisosAsignados");
                lblNoPermisosNoAsignados.Text = _idiomaService.GetTranslation("MensajeNoPermisosNoAsignados");
                lblNoPermisosAsignados.Text = _idiomaService.GetTranslation("MensajeNoPermisosAsignados");
                btnAsignarPermisos.Text = _idiomaService.GetTranslation("BotonAsignarPermisos");
                btnQuitarPermisos.Text = _idiomaService.GetTranslation("BotonQuitarPermisos");
                lblNoPermisosNoAsignados.Visible = true;
                lblNoPermisosAsignados.Visible = true;
            }
        }

        private void CargarUsuarios()
        {
            drpUsuarios.DataSource = _usuarioService.ListarUsuarios();
            drpUsuarios.DataTextField = "Email";
            drpUsuarios.DataValueField = "Id";
            drpUsuarios.DataBind();
            drpUsuarios.Items.Insert(0, new ListItem(_idiomaService.GetTranslation("DropdownSeleccionarUsuario"), "0"));
        }

        protected void drpUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idUsuario = int.Parse(drpUsuarios.SelectedValue);
                if (idUsuario == 0)
                {
                    lblNoPermisosNoAsignados.Visible = true;
                    lblNoPermisosAsignados.Visible = true;
                    gvPermisosNoAsignados.Visible = false;
                    gvPermisosAsignados.Visible = false;
                }
                else
                {
                    CargarPermisosNoAsignados(idUsuario);
                    CargarPermisosAsignados(idUsuario);
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Visible = true;
                lblMensaje.CssClass = "text-danger";
                lblMensaje.Text = _idiomaService.GetTranslation(ex.Message);
            }
        }

        private void CargarPermisosNoAsignados(int idUsuario)
        {
            var permisosNoAsignados = _permisoService.ObtenerPermisosNoAsignadosPorUsuario(idUsuario);
            gvPermisosNoAsignados.DataSource = permisosNoAsignados;
            gvPermisosNoAsignados.DataBind();

            lblNoPermisosNoAsignados.Visible = permisosNoAsignados.Count == 0;
            gvPermisosNoAsignados.Visible = permisosNoAsignados.Count > 0;
        }

        private void CargarPermisosAsignados(int idUsuario)
        {
            var permisosAsignados = _permisoService.ObtenerPermisosAsignadosPorUsuario(idUsuario);
            gvPermisosAsignados.DataSource = permisosAsignados;
            gvPermisosAsignados.DataBind();

            lblNoPermisosAsignados.Visible = permisosAsignados.Count == 0;
            gvPermisosAsignados.Visible = permisosAsignados.Count > 0;
        }

        protected void btnAsignarPermisos_Click(object sender, EventArgs e)
        {
            try
            {
                var userSession = Session["Usuario"] as Usuario;
                int idUsuario = int.Parse(drpUsuarios.SelectedValue);

                foreach (GridViewRow row in gvPermisosNoAsignados.Rows)
                {
                    CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                    if (chkSelect != null && chkSelect.Checked)
                    {
                        int idPermiso = Convert.ToInt32(gvPermisosNoAsignados.DataKeys[row.RowIndex].Value);
                        _permisoService.AsignarPermisoAUsuario(idUsuario, idPermiso, userSession);
                    }
                }

                CargarPermisosNoAsignados(idUsuario);
                CargarPermisosAsignados(idUsuario);

                ScriptManager.RegisterStartupScript(this, GetType(), "PermisosAsignados", "mostrarMensaje('asignado');", true);
            }
            catch (Exception ex)
            {
                lblMensaje.Visible = true;
                lblMensaje.CssClass = "text-danger";
                lblMensaje.Text = _idiomaService.GetTranslation(ex.Message);
            }
        }

        protected void btnQuitarPermisos_Click(object sender, EventArgs e)
        {
            try
            {
                var userSession = Session["Usuario"] as Usuario;
                int idUsuario = int.Parse(drpUsuarios.SelectedValue);

                foreach (GridViewRow row in gvPermisosAsignados.Rows)
                {
                    CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                    if (chkSelect != null && chkSelect.Checked)
                    {
                        int idPermiso = Convert.ToInt32(gvPermisosAsignados.DataKeys[row.RowIndex].Value);
                        _permisoService.QuitarPermisoAUsuario(idUsuario, idPermiso, userSession);
                    }
                }

                CargarPermisosNoAsignados(idUsuario);
                CargarPermisosAsignados(idUsuario);

                ScriptManager.RegisterStartupScript(this, GetType(), "PermisosQuitados", "mostrarMensaje('quitado');", true);
            }
            catch (Exception ex)
            {
                lblMensaje.Visible = true;
                lblMensaje.CssClass = "text-danger";
                lblMensaje.Text = _idiomaService.GetTranslation(ex.Message);
            }
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