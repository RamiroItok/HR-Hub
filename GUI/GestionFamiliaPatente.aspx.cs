using Aplication.Interfaces;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unity;

namespace GUI
{
    public partial class GestionFamiliaPatente : Page
    {
        private readonly IPermisoService _iPermiso;

        public GestionFamiliaPatente()
        {
            _iPermiso = Global.Container.Resolve<IPermisoService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarFamilias();
                CargarDataGrids();
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
                lblMessage.Text = "Debe seleccionar una familia";
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
                    lblMensajeAsignacion.Text = "No hay permisos para asignar";
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
                            }
                        }
                    }

                    Listar(familiaId);
                    if (isSelected)
                    {
                        lblMensajeAsignacion.Text = "Se asignó el permiso correctamente.";
                        lblMensajeAsignacion.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lblMensajeAsignacion.Text = "Debe seleccionar al menos un permiso para asignar.";
                        lblMensajeAsignacion.ForeColor = System.Drawing.Color.Red;
                    }
                }

            }
            catch (Exception ex)
            {
                lblMessage.Text = "Ocurrió un error al asignar permisos: " + ex.Message;
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
                    lblMensajeAsignacion.Text = "No hay permisos para quitar";
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
                            }
                        }
                    }

                    Listar(familiaId);
                    if (isSelected)
                    {
                        lblMensajeEliminar.Text = "Se asignó el permiso correctamente.";
                        lblMensajeEliminar.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lblMensajeEliminar.Text = "Debe seleccionar al menos un permiso para asignar.";
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

    }
}