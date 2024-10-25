using Aplication.Interfaces;
using Models;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unity;

namespace GUI
{
    public partial class PermisosUsuario : Page
    {
        private readonly IUsuarioService _usuarioService;
        private static List<Models.Usuario> listaUsuarios = new List<Models.Usuario>();

        public PermisosUsuario()
        {
            _usuarioService = Global.Container.Resolve<IUsuarioService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                listaUsuarios = _usuarioService.ListarUsuarios();
                CargarUsuarioDefault();
                CargarPuestos();
            }
            CargarCampos();
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
            LimpiarCampos();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            lblMensajeModificacion.Text = Validar();
            if (!string.IsNullOrEmpty(lblMensajeModificacion.Text))
            {
                lblMensajeModificacion.Visible = true;
                lblMensajeModificacion.CssClass = "validation-message-failed";
            }
            else
            {
                var usuario = CompletarUsuario();
                var userSession = Session["Usuario"] as Usuario;
                _usuarioService.ModificarPermisoUsuario(usuario, userSession);
                CargarUsuarioDefault();
                LimpiarCampos();

                lblMensajeModificacion.Text = "Se modificaron correctamente los datos";
                lblMensajeModificacion.Visible = true;
                lblMensajeModificacion.CssClass = "validation-message-success";
            }
        }

        protected void btnCancelarModificacion_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        #region Metodos prviados
        private void CargarUsuarioDefault()
        {
            CargarGrilla(_usuarioService.ListarUsuarios());
        }

        private void CargarGrilla(List<Models.Usuario> listadoBitacora)
        {
            dataGridUsuarios.DataSource = listadoBitacora;
            dataGridUsuarios.DataBind();
        }

        private void CargarPuestos()
        {
            DropDownPuesto.DataSource = _usuarioService.ObtenerPuestos();
            DropDownPuesto.DataTextField = "Nombre";
            DropDownPuesto.DataValueField = "Id";
            DropDownPuesto.DataBind();
            DropDownPuesto.Items.Insert(0, new ListItem("Seleccione un Puesto", ""));
        }

        private string Validar()
        {
            if (string.IsNullOrEmpty(hiddenApellido.Value) || string.IsNullOrEmpty(hiddenEmail.Value) || string.IsNullOrEmpty(hiddenNombre.Value))
                return "Hay campos sin completar";

            return null;
        }

        private Usuario CompletarUsuario()
        {
            Usuario usuario = new Usuario()
            {
                Email = hiddenEmail.Value,
                Puesto = (Puesto)Enum.Parse(typeof(Puesto), DropDownPuesto.Text),
            };
            return usuario;
        }

        private void CargarCampos()
        {
            txtApellido.Text = hiddenApellido.Value;
            txtNombre.Text = hiddenNombre.Value;
            txtEmail.Text = hiddenEmail.Value;
        }

        private void LimpiarCampos()
        {
            txtApellido.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtNombre.Text = string.Empty;
            DropDownPuesto.SelectedIndex = 0;
        }
        #endregion
    }
}