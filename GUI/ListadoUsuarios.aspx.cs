using Aplication.Interfaces;
using Aplication.Interfaces.Observer;
using Aplication.Services.Observer;
using Models;
using Models.Composite;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unity;

namespace GUI
{
    public partial class ListadoUsuarios : Page, IIdiomaService
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IPermisoService _permisoService;
        private readonly IDocumentoService _documentoService;
        private readonly IdiomaService _idiomaService;
        private static List<Models.Usuario> listaUsuarios = new List<Models.Usuario>();

        public ListadoUsuarios()
        {
            _usuarioService = Global.Container.Resolve<IUsuarioService>();
            _permisoService = Global.Container.Resolve<IPermisoService>();
            _documentoService = Global.Container.Resolve<IDocumentoService>();
            _idiomaService = Global.Container.Resolve<IdiomaService>();
            _idiomaService.Subscribe(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var usuario = Session["Usuario"] as Usuario;
            if (!_permisoService.TienePermiso(usuario, Permiso.ListarUsuarios))
            {
                Response.Redirect("AccesoDenegado.aspx");
                return;
            }

            if (!IsPostBack)
            {
                string selectedLanguage = Session["SelectedLanguage"] as string ?? "es";
                ddlLanguage.SelectedValue = selectedLanguage;
                _idiomaService.CurrentLanguage = selectedLanguage;
                listaUsuarios = _usuarioService.ListarUsuarios();
                CargarUsuarioDefault();
                CargarAreas();
                CargarPuestos();
                CargarTextos();
                CargarHeadersGridView();
            }
            CargarCampos();
        }

        private void CargarTextos()
        {
            if (!(litPageTitle == null))
            {
                litPageTitle.Text = _idiomaService.GetTranslation("PageTitleListadoUsuarios");
                litTitle.Text = _idiomaService.GetTranslation("TituloListadoUsuarios");
                btnBuscar.Text = _idiomaService.GetTranslation("Buscar");
                btnCancelar.Text = _idiomaService.GetTranslation("Cancelar");
                lblMensaje.Text = _idiomaService.GetTranslation("MensajeNoResultados");
                txtBuscar.Attributes["placeholder"] = _idiomaService.GetTranslation("BuscarUsuarioPlaceholder");

                litFormTitle.Text = _idiomaService.GetTranslation("ModificarDatosUsuario");
                btnGuardar.Text = _idiomaService.GetTranslation("Guardar");
                btnCancelarModificacion.Text = _idiomaService.GetTranslation("Cancelar");

                litNombreLabel.Text = _idiomaService.GetTranslation("Nombre");
                litApellidoLabel.Text = _idiomaService.GetTranslation("Apellido");
                litEmailLabel.Text = _idiomaService.GetTranslation("Email");
                litFechaIngresoLabel.Text = _idiomaService.GetTranslation("FechaIngreso");
                litPuestoLabel.Text = _idiomaService.GetTranslation("Puesto");
                litAreaLabel.Text = _idiomaService.GetTranslation("Area");
                litFechaNacimientoLabel.Text = _idiomaService.GetTranslation("FechaNacimiento");
                litDireccionLabel.Text = _idiomaService.GetTranslation("Direccion");
                litNumeroDireccionLabel.Text = _idiomaService.GetTranslation("NumeroDireccion");
                litDepartamentoLabel.Text = _idiomaService.GetTranslation("Departamento");
                litCodigoPostalLabel.Text = _idiomaService.GetTranslation("CodigoPostal");
                litCiudadLabel.Text = _idiomaService.GetTranslation("Ciudad");
                litProvinciaLabel.Text = _idiomaService.GetTranslation("Provincia");
                litPaisLabel.Text = _idiomaService.GetTranslation("Pais");
                lblGenero.Text = _idiomaService.GetTranslation("LabelGenero");
                drpGenero.Items.Clear();
                drpGenero.Items.Add(new ListItem(_idiomaService.GetTranslation("OptionGeneroDefault"), "0"));
                drpGenero.Items.Add(new ListItem(_idiomaService.GetTranslation("OptionGeneroMasculino"), "1"));
                drpGenero.Items.Add(new ListItem(_idiomaService.GetTranslation("OptionGeneroFemenino"), "2"));
                drpGenero.Items.Add(new ListItem(_idiomaService.GetTranslation("OptionGeneroNoEspecifica"), "3"));
            }
        }

        private void CargarHeadersGridView()
        {
            if (!(dataGridUsuarios.Columns[1].HeaderText == null))
            {
                dataGridUsuarios.Columns[1].HeaderText = _idiomaService.GetTranslation("Nombre");
                dataGridUsuarios.Columns[2].HeaderText = _idiomaService.GetTranslation("Apellido");
                dataGridUsuarios.Columns[3].HeaderText = _idiomaService.GetTranslation("Email");
                dataGridUsuarios.Columns[4].HeaderText = _idiomaService.GetTranslation("FechaIngreso");
                dataGridUsuarios.Columns[5].HeaderText = _idiomaService.GetTranslation("Puesto");
                dataGridUsuarios.Columns[6].HeaderText = _idiomaService.GetTranslation("Area");
                dataGridUsuarios.Columns[7].HeaderText = _idiomaService.GetTranslation("FechaNacimiento");
                dataGridUsuarios.Columns[8].HeaderText = _idiomaService.GetTranslation("Genero");
                dataGridUsuarios.Columns[9].HeaderText = _idiomaService.GetTranslation("Direccion");
                dataGridUsuarios.Columns[10].HeaderText = _idiomaService.GetTranslation("NumeroDireccion");
                dataGridUsuarios.Columns[11].HeaderText = _idiomaService.GetTranslation("Departamento");
                dataGridUsuarios.Columns[12].HeaderText = _idiomaService.GetTranslation("CodigoPostal");
                dataGridUsuarios.Columns[13].HeaderText = _idiomaService.GetTranslation("Ciudad");
                dataGridUsuarios.Columns[14].HeaderText = _idiomaService.GetTranslation("Provincia");
                dataGridUsuarios.Columns[15].HeaderText = _idiomaService.GetTranslation("Pais");
                dataGridUsuarios.DataBind();
            }
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
            try
            {
                lblMensajeModificacion.Text = Validar();
                int? puestoAnterior = null;
                if (!string.IsNullOrEmpty(hiddenDropDownPuesto.Value))
                    puestoAnterior = int.Parse(hiddenDropDownPuesto.Value);
                
                lblMensajeModificacion.Text = _idiomaService.GetTranslation("DatosModificadosExitosamente");
                lblMensajeModificacion.Visible = true;
                lblMensajeModificacion.CssClass = "validation-message-success";

                var usuario = CompletarUsuario();
                var userSession = Session["Usuario"] as Usuario;
                _usuarioService.ModificarUsuario(usuario, userSession);
                _permisoService.ActualizarFamiliaUsuario(usuario, puestoAnterior);

                if (usuario.Puesto == Puesto.Lider)
                    _documentoService.AsignarDocumentosAUsuario(usuario.Id);
                if (puestoAnterior == (int)Puesto.Lider)
                    _documentoService.QuitarDocumentosAUsuario(usuario.Id);

                CargarUsuarioDefault();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                lblMensajeModificacion.Visible = true;
                lblMensajeModificacion.CssClass = "validation-message-failed";
                lblMensajeModificacion.Text = ex.Message;
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
                lblMensaje.Text = _idiomaService.GetTranslation("IngreseUsuarioBusqueda");
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
            DropDownPuesto.DataTextField = "Nombre";
            DropDownPuesto.DataValueField = "Id";
            DropDownPuesto.DataBind();
            DropDownPuesto.Items.Insert(0, new ListItem(_idiomaService.GetTranslation("SeleccionePuesto"), ""));
        }

        private void CargarAreas()
        {
            DropDownArea.DataSource = _usuarioService.ObtenerAreas();
            DropDownArea.DataTextField = "Area";
            DropDownArea.DataValueField = "Id";
            DropDownArea.DataBind();
            DropDownArea.Items.Insert(0, new ListItem(_idiomaService.GetTranslation("SeleccioneArea"), ""));
        }

        private string Validar()
        {
            if (string.IsNullOrEmpty(hiddenApellido.Value) || string.IsNullOrEmpty(txtCiudad.Text) || string.IsNullOrEmpty(txtCodigoPostal.Text) || string.IsNullOrEmpty(txtDepartamento.Text) || string.IsNullOrEmpty(txtDireccion.Text)
                || string.IsNullOrEmpty(hiddenEmail.Value) || string.IsNullOrEmpty(hiddenFechaIngreso.Value) || string.IsNullOrEmpty(hiddenFechaNacimiento.Value) || string.IsNullOrEmpty(hiddenNombre.Value)
                || string.IsNullOrEmpty(txtNumeroDireccion.Text) || string.IsNullOrEmpty(txtPais.Text) || string.IsNullOrEmpty(txtProvincia.Text))
                throw new Exception(_idiomaService.GetTranslation("MensajeCamposIncompletos"));

            return null;
        }

        private Usuario CompletarUsuario()
        {
            var thisUsuario = _usuarioService.ObtenerUsuarioPorEmail(hiddenEmail.Value);
            Usuario usuario = new Usuario()
            {
                Id = thisUsuario.Id,
                Email = hiddenEmail.Value,
                Puesto = (Puesto)Enum.Parse(typeof(Puesto), DropDownPuesto.Text),
                Area = (Area)Enum.Parse(typeof(Area), DropDownArea.Text),
                Genero = Enum.GetName(typeof(Genero), int.Parse(drpGenero.Text)),
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
            txtFechaNacimiento.Text = string.Empty;
            drpGenero.SelectedIndex = 0;
            txtNombre.Text = string.Empty;
            txtNumeroDireccion.Text = string.Empty;
            txtPais.Text = string.Empty;
            txtProvincia.Text = string.Empty;
            DropDownPuesto.SelectedIndex = 0;
            DropDownArea.SelectedIndex = 0;
        }

        public void UpdateLanguage(string language)
        {
            CargarTextos();
            CargarHeadersGridView();
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
            _idiomaService.CurrentLanguage = selectedLanguage;
            CargarTextos();
            CargarHeadersGridView();
            Response.Redirect(Request.RawUrl);
        }
    }
}