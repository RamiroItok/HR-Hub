using Aplication.Interfaces;
using Aplication.Interfaces.Observer;
using Aplication.Services.Observer;
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
    public partial class PermisosUsuario : Page, IIdiomaService
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IPermisoService _permisoService;
        private readonly IdiomaService _idiomaService;
        private static List<Models.Usuario> listaUsuarios = new List<Models.Usuario>();

        public PermisosUsuario()
        {
            _usuarioService = Global.Container.Resolve<IUsuarioService>();
            _permisoService = Global.Container.Resolve<IPermisoService>();
            _idiomaService = Global.Container.Resolve<IdiomaService>();
            _idiomaService.Subscribe(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var usuario = Session["Usuario"] as Usuario;
            if(!_permisoService.TienePermiso(usuario, Permiso.PermisoUsuario))
            {
                Response.Redirect("AccesoDenegado.aspx");
                return;
            }

            try
            {
                if (!IsPostBack)
                {
                    listaUsuarios = _usuarioService.ListarUsuarios();
                    CargarUsuarioDefault();
                    string selectedLanguage = Session["SelectedLanguage"] as string ?? "es";
                    ddlLanguage.SelectedValue = selectedLanguage;
                    _idiomaService.CurrentLanguage = selectedLanguage;
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Visible = true;
                lblMensaje.CssClass = "text-danger";
                lblMensaje.Text = _idiomaService.GetTranslation(ex.Message);
            }
            finally
            {
                CargarTextos();
            }
        }

        private void CargarTextos()
        {
            litPageTitle.Text = _idiomaService.GetTranslation("TituloPaginaPermisos");
            lblMensaje.Text = _idiomaService.GetTranslation("MensajeBusquedaVacia");
            litTitle.Text = _idiomaService.GetTranslation("TituloListado");
            btnBuscar.Text = _idiomaService.GetTranslation("BotonBuscar");
            btnCancelar.Text = _idiomaService.GetTranslation("BotonCancelar");

            txtBuscar.Attributes["placeholder"] = _idiomaService.GetTranslation("PlaceholderBuscarUsuario");

            foreach (GridViewRow row in dataGridUsuarios.Rows)
            {
                Button btnVerMas = (Button)row.FindControl("btnVerMas");
                if (btnVerMas != null)
                {
                    btnVerMas.Text = _idiomaService.GetTranslation("BotonVerMas");
                }
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "SetModalText", $@"
                document.getElementById('verMasModalLabel').innerText = '{_idiomaService.GetTranslation("ModalTituloDetalles")}';
                document.querySelector('#verMasModal .btn-secondary').innerText = '{_idiomaService.GetTranslation("ModalCerrar")}';
                document.querySelector('#modalId').previousElementSibling.innerText = '{_idiomaService.GetTranslation("ModalID")}:'; 
                document.querySelector('#modalNombre').previousElementSibling.innerText = '{_idiomaService.GetTranslation("ModalNombre")}:'; 
                document.querySelector('#modalApellido').previousElementSibling.innerText = '{_idiomaService.GetTranslation("ModalApellido")}:'; 
                document.querySelector('#modalEmail').previousElementSibling.innerText = '{_idiomaService.GetTranslation("ModalEmail")}:'; 
                document.querySelector('#modalFamilia').previousElementSibling.innerText = '{_idiomaService.GetTranslation("ModalFamilia")}:'; 
                document.querySelector('#modalPermisos').previousElementSibling.innerText = '{_idiomaService.GetTranslation("ModalPermisos")}:'; 
            ", true);
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string searchTerm = txtBuscar.Text.Trim();

            if (string.IsNullOrEmpty(searchTerm))
            {
                lblMensaje.Text = _idiomaService.GetTranslation("MensajeBusquedaVacia");
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
            try
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