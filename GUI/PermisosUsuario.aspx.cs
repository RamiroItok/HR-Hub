using Aplication.Interfaces;
using Models;
using Models.Composite;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unity;

namespace GUI
{
    public partial class PermisosUsuario : Page
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IPermisoService _permisoService;
        private static List<Models.Usuario> listaUsuarios = new List<Models.Usuario>();

        public PermisosUsuario()
        {
            _usuarioService = Global.Container.Resolve<IUsuarioService>();
            _permisoService = Global.Container.Resolve<IPermisoService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var usuario = Session["Usuario"] as Usuario;
            if(!_permisoService.TienePermiso(usuario, Permiso.PermisoUsuario))
            {
                Response.Redirect("AccesoDenegado.aspx");
                return;
            }

            if (!IsPostBack)
            {
                listaUsuarios = _usuarioService.ListarUsuarios();
                CargarUsuarioDefault();
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string searchTerm = txtBuscar.Text.Trim();

            if (string.IsNullOrEmpty(searchTerm))
            {
                lblMensaje.Text = "Por favor, ingrese un usuario en la búsqueda.";
                lblMensaje.Visible = true;
            }
            else
            {
                List<Models.Usuario> listaUsuariosFiltrados = new List<Models.Usuario>();

                listaUsuariosFiltrados = listaUsuarios
                                            .Where(x =>
                                                (string.IsNullOrEmpty(txtBuscar.Text) || x.Nombre.ToLower().Contains(txtBuscar.Text.ToLower())))
                                            .ToList();

                Session["UsuariosFiltrados"] = listaUsuariosFiltrados;
                lblMensaje.Visible = false;
                CargarGrilla(listaUsuariosFiltrados);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session["UsuariosFiltrados"] = null;
            txtBuscar.Text = string.Empty;
            lblMensaje.Visible = false;
            CargarUsuarioDefault();
        }

        protected void dataGridUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "VerMas")
            {
                int userId = Convert.ToInt32(e.CommandArgument);
                hiddenSelectedId.Value = userId.ToString();

                var usuario = _usuarioService.ObtenerUsuarioPorId(userId);
                var familia = _permisoService.ObtenerFamiliaUsuario(usuario.Id);
                var permisos = _permisoService.ObtenerPermisosAsignadosPorUsuario(usuario.Id);

                string script = $@"
                    document.getElementById('modalId').innerText = '{usuario.Id}';
                    document.getElementById('modalNombre').innerText = '{usuario.Nombre}';
                    document.getElementById('modalApellido').innerText = '{usuario.Apellido}';
                    document.getElementById('modalEmail').innerText = '{usuario.Email}';
                    document.getElementById('modalFamilia').innerText = '{familia.Rows[0]["Nombre"]}';

                    var permisosList = document.getElementById('modalPermisos');
                    permisosList.innerHTML = '';";

                foreach (var permiso in permisos)
                {
                    script += $@"
                var permisoItem = document.createElement('li');
                permisoItem.textContent = '{permiso.Nombre}';
                permisosList.appendChild(permisoItem);";
                }

                script += "$('#verMasModal').modal('show');";

                ScriptManager.RegisterStartupScript(this, GetType(), "ShowModalScript", script, true);
            }
        }

        #region Metodos prviados

        private void CargarUsuarioDefault()
        {
            CargarGrilla(_usuarioService.ListarUsuarios());
        }

        private void CargarGrilla(List<Models.Usuario> listadoUsuarios)
        {
            dataGridUsuarios.DataSource = listadoUsuarios;
            dataGridUsuarios.DataBind();
        }
        #endregion
    }
}