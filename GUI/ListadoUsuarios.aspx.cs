using Aplication.Interfaces;
using Models;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unity;

namespace GUI
{
    public partial class ListadoUsuarios : Page
    {
        private readonly IUsuarioService _usuarioService;
        private static List<Models.Usuario> listaUsuarios = new List<Models.Usuario>();

        public ListadoUsuarios()
        {
            _usuarioService = Global.Container.Resolve<IUsuarioService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                listaUsuarios = _usuarioService.ListarUsuarios();
                CargarUsuarioDefault();
                CargarAreas();
                CargarPuestos();
            }
            CargarCampos();
        }

        private void CargarUsuarioDefault()
        {
            CargarGrilla(_usuarioService.ListarUsuarios());
        }

        private void CargarGrilla(List<Models.Usuario> listadoBitacora)
        {
            dataGridUsuarios.DataSource = listadoBitacora;
            dataGridUsuarios.DataBind();
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
                lblMensajeModificacion.Text = "Se modificaron correctamente los datos";
                lblMensajeModificacion.Visible = true;
                lblMensajeModificacion.CssClass = "validation-message-success";

                var usuario = CompletarUsuario();
                var userSession = Session["Usuario"] as Usuario;
                _usuarioService.ModificarUsuario(usuario, userSession);
                CargarUsuarioDefault();
                LimpiarCampos();
            }
        }

        protected void btnCancelarModificacion_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
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
                                                (string.IsNullOrEmpty(txtBuscar.Text) || x.Nombre.ToLower().Contains(txtBuscar.Text.ToLower()))
                                            )
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

        private void CargarPuestos()
        {
            DropDownPuesto.DataSource = _usuarioService.ObtenerPuestos();
            DropDownPuesto.DataTextField = "Puesto";
            DropDownPuesto.DataValueField = "Id";
            DropDownPuesto.DataBind();
            DropDownPuesto.Items.Insert(0, new ListItem("Seleccione un Puesto", ""));
        }

        private void CargarAreas()
        {
            DropDownArea.DataSource = _usuarioService.ObtenerAreas();
            DropDownArea.DataTextField = "Area";
            DropDownArea.DataValueField = "Id";
            DropDownArea.DataBind();
            DropDownArea.Items.Insert(0, new ListItem("Seleccione una Area", ""));
        }

        private string Validar()
        {
            if (string.IsNullOrEmpty(hiddenApellido.Value) || string.IsNullOrEmpty(txtCiudad.Text) || string.IsNullOrEmpty(txtCodigoPostal.Text) || string.IsNullOrEmpty(txtDepartamento.Text) || string.IsNullOrEmpty(txtDireccion.Text)
                || string.IsNullOrEmpty(hiddenEmail.Value) || string.IsNullOrEmpty(hiddenFechaIngreso.Value) || string.IsNullOrEmpty(hiddenFechaNacimiento.Value) || string.IsNullOrEmpty(txtGenero.Text) || string.IsNullOrEmpty(hiddenNombre.Value)
                || string.IsNullOrEmpty(txtNumeroDireccion.Text) || string.IsNullOrEmpty(txtPais.Text) || string.IsNullOrEmpty(txtProvincia.Text))
                return "Hay campos sin completar";

            return null;
        }

        private Usuario CompletarUsuario()
        {
            Usuario usuario = new Usuario()
            {
                Email = hiddenEmail.Value,
                Puesto = (Puesto)Enum.Parse(typeof(Puesto), DropDownPuesto.Text),
                Area = (Area)Enum.Parse(typeof(Area), DropDownArea.Text),
                Genero = txtGenero.Text,
                Direccion = txtDireccion.Text,
                NumeroDireccion = int.Parse(txtNumeroDireccion.Text),
                Departamento = txtDepartamento.Text,
                CodigoPostal = txtCodigoPostal.Text,
                Ciudad = txtCiudad.Text,
                Provincia = txtProvincia.Text,
                Pais = txtPais.Text
            };
            return usuario;
        }

        private void CargarCampos()
        {
            txtApellido.Text = hiddenApellido.Value;
            txtNombre.Text = hiddenNombre.Value;
            txtEmail.Text = hiddenEmail.Value;
            txtFechaIngreso.Text = hiddenFechaIngreso.Value;
            txtFechaNacimiento.Text = hiddenFechaNacimiento.Value;
        }

        private void LimpiarCampos()
        {
            txtApellido.Text = string.Empty;
            txtCiudad.Text = string.Empty;
            txtCodigoPostal.Text = string.Empty;
            txtDepartamento.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtFechaIngreso.Text = string.Empty;
            txtFechaNacimiento.Text= string.Empty;
            txtGenero.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtNumeroDireccion.Text = string.Empty;
            txtPais.Text = string.Empty;
            txtProvincia.Text = string.Empty;
            DropDownPuesto.SelectedIndex = 0;
            DropDownArea.SelectedIndex = 0;
        }
    }
}