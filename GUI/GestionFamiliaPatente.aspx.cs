using Aplication.Interfaces;
using Aplication.Interfaces.Observer;
using Aplication.Services.Observer;
using Models;
using Models.Composite;
using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unity;

namespace GUI
{
    public partial class GestionFamiliaPatente : Page, IIdiomaService
    {
        private readonly IPermisoService _iPermiso;
        private readonly IBitacoraService _iBitacoraService;
        private readonly IdiomaService _idiomaService;

        public GestionFamiliaPatente()
        {
            _iPermiso = Global.Container.Resolve<IPermisoService>();
            _iBitacoraService = Global.Container.Resolve<IBitacoraService>();
            _idiomaService = Global.Container.Resolve<IdiomaService>();
            _idiomaService.Subscribe(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var usuario = Session["Usuario"] as Usuario;
            if(!_iPermiso.TienePermiso(usuario, Permiso.GestionFamiliaPatente))
            {
                Response.Redirect("AccesoDenegado.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CargarFamilias();
                CargarDataGrids();
                string selectedLanguage = Session["SelectedLanguage"] as string ?? "es";
                ddlLanguage.SelectedValue = selectedLanguage;
                _idiomaService.CurrentLanguage = selectedLanguage;
                CargarTextos();
            }
        }

        private void CargarTextos()
        {
            if (!(litTituloPagina == null))
            {
                litTituloPagina.Text = _idiomaService.GetTranslation("GestionFamiliaPatenteTituloPagina");

                lblFamilia1.Text = _idiomaService.GetTranslation("LabelFamilia");
                btn_Listar.Text = _idiomaService.GetTranslation("ButtonListar");
                btn_AsignarPermiso.Text = _idiomaService.GetTranslation("ButtonAsignarPermiso");
                btn_EliminarPatente.Text = _idiomaService.GetTranslation("ButtonEliminarPatente");

                lblPermisosNoAsignadosTitle.Text = _idiomaService.GetTranslation("PermisosNoAsignados");
                lblPermisosAsignadosTitle.Text = _idiomaService.GetTranslation("PermisosAsignados");

                cmb_Familia1.Items[0].Text = _idiomaService.GetTranslation("SeleccionaUnaOpcion");

                lblMensajeAsignacion.Text = string.Empty;
                lblMensajeEliminar.Text = string.Empty;
                lblMessage.Text = string.Empty;

                gvPermisosNoAsignados.Columns[0].HeaderText = _idiomaService.GetTranslation("SeleccionarHeader");
                gvPermisosNoAsignados.Columns[1].HeaderText = _idiomaService.GetTranslation("ColumnId");
                gvPermisosNoAsignados.Columns[2].HeaderText = _idiomaService.GetTranslation("ColumnNombre");
                gvPermisosNoAsignados.Columns[3].HeaderText = _idiomaService.GetTranslation("ColumnPermiso");

                gvPermisosAsignados.Columns[0].HeaderText = _idiomaService.GetTranslation("SeleccionarHeader");
                gvPermisosAsignados.Columns[1].HeaderText = _idiomaService.GetTranslation("ColumnId");
                gvPermisosAsignados.Columns[2].HeaderText = _idiomaService.GetTranslation("ColumnNombre");
                gvPermisosAsignados.Columns[3].HeaderText = _idiomaService.GetTranslation("ColumnPermiso");

                gvPermisosNoAsignados.EmptyDataText = _idiomaService.GetTranslation("NoPermisosAsignar");
                gvPermisosAsignados.EmptyDataText = _idiomaService.GetTranslation("NoPermisosAsignados");

                gvPermisosNoAsignados.DataBind();
                gvPermisosAsignados.DataBind();
            }
        }

        private void CargarDataGrids()
        {
            Limpiar();
            DataTable dt = new DataTable();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("Permiso", typeof(string));
            gvPermisosNoAsignados.DataSource = dt;
            gvPermisosNoAsignados.DataBind();

            DataTable dt2 = new DataTable();
            dt2.Columns.Add("Id", typeof(int));
            dt2.Columns.Add("Nombre", typeof(string));
            dt2.Columns.Add("Permiso", typeof(string));
            gvPermisosAsignados.DataSource = dt2;
            gvPermisosAsignados.DataBind();
        }

        private void CargarFamilias()
        {
            cmb_Familia1.DataSource = _iPermiso.ObtenerFamilias();
            cmb_Familia1.DataTextField = "Nombre";
            cmb_Familia1.DataValueField = "Id";
            cmb_Familia1.DataBind();
        }

        protected void btn_Listar_Click(object sender, EventArgs e)
        {
            int familiaId;
            if (int.TryParse(cmb_Familia1.SelectedValue, out familiaId) && familiaId > 0)
            {
                Listar(familiaId);
            }
            else
            {
                CargarDataGrids();
                lblMessage.Text = _idiomaService.GetTranslation("SeleccionarFamilia");
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btn_AsignarPermiso_Click(object sender, EventArgs e)
        {
            try
            {
                int familiaId = int.Parse(cmb_Familia1.SelectedValue);
                bool isSelected = false;

                if (gvPermisosNoAsignados.Rows.Count == 0)
                {
                    Limpiar();
                    lblMensajeAsignacion.Text = _idiomaService.GetTranslation("NoPermisosParaAsignar");
                    lblMensajeAsignacion.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    foreach (GridViewRow row in gvPermisosNoAsignados.Rows)
                    {
                        CheckBox chk = (CheckBox)row.FindControl("chkSeleccionar");
                        if (chk != null && chk.Checked)
                        {
                            isSelected = true;

                            if (gvPermisosNoAsignados.DataKeys != null && row.RowIndex < gvPermisosNoAsignados.DataKeys.Count)
                            {
                                int permisoId = int.Parse(gvPermisosNoAsignados.DataKeys[row.RowIndex].Value.ToString());
                                _iPermiso.AsignarPermisoAFamilia(familiaId, permisoId);
                                _iPermiso.GuardarUsuarioPermiso(familiaId, permisoId);

                                var usuario = Session["Usuario"] as Models.Usuario;
                                _iPermiso.GetComponenteUsuario(usuario);
                            }
                        }
                    }

                    Listar(familiaId);
                    if (isSelected)
                    {
                        var usuario = Session["Usuario"] as Usuario;
                        _iBitacoraService.AltaBitacora(usuario.Email, usuario.Puesto, "Asigna patente a familia", Models.Enums.Criticidad.ALTA);

                        lblMensajeAsignacion.Text = _idiomaService.GetTranslation("OperacionExitosa");
                        lblMensajeAsignacion.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lblMensajeAsignacion.Text = _idiomaService.GetTranslation("SeleccionarPermisoParaAsignar");
                        lblMensajeAsignacion.ForeColor = System.Drawing.Color.Red;
                    }
                }

            }
            catch (Exception ex)
            {
                lblMessage.Text = _idiomaService.GetTranslation("ErrorAlAsignarPermisos") + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btn_EliminarPatente_Click(object sender, EventArgs e)
        {
            try
            {
                int familiaId = int.Parse(cmb_Familia1.SelectedValue);
                bool isSelected = false;

                if (gvPermisosAsignados.Rows.Count == 0)
                {
                    Limpiar();
                    lblMensajeAsignacion.Text = _idiomaService.GetTranslation("NoPermisosParaQuitar");
                    lblMensajeAsignacion.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    foreach (GridViewRow row in gvPermisosAsignados.Rows)
                    {
                        CheckBox chk = (CheckBox)row.FindControl("chkSeleccionar2");
                        if (chk != null && chk.Checked)
                        {
                            isSelected = true;
                            if (gvPermisosAsignados.DataKeys != null && row.RowIndex < gvPermisosAsignados.DataKeys.Count)
                            {
                                int permisoId = int.Parse(gvPermisosAsignados.DataKeys[row.RowIndex].Value.ToString());
                                _iPermiso.QuitarPermisoAFamilia(familiaId, permisoId);
                                _iPermiso.EliminarPermisoUsuario(familiaId, permisoId);

                                var usuario = Session["Usuario"] as Models.Usuario;
                                _iPermiso.GetComponenteUsuario(usuario);
                            }
                        }
                    }

                    Listar(familiaId);
                    if (isSelected)
                    {
                        lblMensajeEliminar.Text = _idiomaService.GetTranslation("OperacionExitosa");
                        lblMensajeEliminar.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lblMensajeEliminar.Text = _idiomaService.GetTranslation("SeleccionarPermisoParaAsignar");
                        lblMensajeEliminar.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void Listar(int familiaId)
        {
            Limpiar();
            gvPermisosNoAsignados.DataSource = _iPermiso.ObtenerPermisosNoAsignados(familiaId);
            gvPermisosNoAsignados.DataBind();

            gvPermisosAsignados.DataSource = _iPermiso.ObtenerPermisosPorFamilia(familiaId);
            gvPermisosAsignados.DataBind();
        }

        private void Limpiar()
        {
            lblMensajeAsignacion.Text = String.Empty;
            lblMensajeEliminar.Text = String.Empty;
            lblMessage.Text = String.Empty;
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