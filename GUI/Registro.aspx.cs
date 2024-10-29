using Aplication.Interfaces;
using Models;
using Models.Enums;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unity;

namespace GUI
{
    public partial class Registro : Page
    {
        private readonly IUsuarioService _usuarioService;

        public Registro()
        {
            _usuarioService = Global.Container.Resolve<IUsuarioService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    CargarAreas();
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Visible = true;
                lblMensaje.CssClass = "text-danger";
                lblMensaje.Text = ex.Message;
            }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario usuario = new Usuario()
                {
                    Nombre = txtNombre.Text,
                    Apellido = txtApellido.Text,
                    Email = txtEmail.Text,
                    Contraseña = hiddenContraseña.Value,
                    Area = (Area)Enum.Parse(typeof(Area), DropDownArea.Text),
                    FechaNacimiento = DateTime.Parse(txtFechaNac.Value),
                    Genero = drpGenero.SelectedItem.Text,
                    FechaIngreso = DateTime.Now,
                    Direccion = txtDireccion.Text,
                    NumeroDireccion = int.Parse(txtNumeroDireccion.Text),
                    Departamento = txtDepartamento.Text,
                    CodigoPostal = txtCodigoPostal.Text,
                    Ciudad = txtCiudad.Text,
                    Provincia = txtProvincia.Text,
                    Pais = txtPais.Text
                };

                // TODO: --> VALIDAR CAMPOS

                var esContraseñaValida = _usuarioService.ValidarFormatoContraseña(usuario.Contraseña);
                //var esUsuarioValido = _usuarioService.ObtenerUsuarioPorEmail(usuario.Email);
                if (esContraseñaValida) //&& esUsuarioValido == null)
                {
                    var userSession = Session["Usuario"] as Usuario;
                    var id = _usuarioService.RegistrarUsuario(usuario, userSession);
                    _usuarioService.EnviarMail(usuario.Email, usuario.Contraseña, AsuntoMail.GeneracionContraseña);
                    lblMensaje.Visible = true;
                    lblMensaje.Text = "Se ha registrado el usuario correctamente";
                    Limpiar();
                }
                else
                {
                    lblMensaje.Text = "La contraseña debe tener al menos una mayuscula, una minuscula, un caracter especial, un numero, y debe ser de 8 caracteres en total.";
                }
                
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }
        }

        private void Limpiar()
        {
            txtNombre.Text = String.Empty;
            txtApellido.Text = String.Empty;
            txtEmail.Text = String.Empty;
            txtContraseña.Text = String.Empty;
            txtFechaNac.Value = String.Empty;
            DropDownArea.SelectedIndex = 0;
            drpGenero.SelectedIndex = 0;
            hiddenContraseña.Value = String.Empty;
            txtDireccion.Text = String.Empty;
            txtNumeroDireccion.Text = String.Empty;
            txtDepartamento.Text = String.Empty;
            txtCodigoPostal.Text = String.Empty;
            txtCiudad.Text = String.Empty;
            txtProvincia.Text = String.Empty;
            txtPais.Text = String.Empty;
        }

        private void CargarAreas()
        {
            DropDownArea.DataSource = _usuarioService.ObtenerAreas();
            DropDownArea.DataTextField = "Area";
            DropDownArea.DataValueField = "Id";
            DropDownArea.DataBind();
            DropDownArea.Items.Insert(0, new ListItem("Seleccione una Area", ""));
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        protected void btnGenerarPassword_Click(object sender, EventArgs e)
        {
            string nuevaContraseña = _usuarioService.GenerarContraseña();
            hiddenContraseña.Value = nuevaContraseña;
            txtContraseña.Text = new string('*', nuevaContraseña.Length);
        }
    }
}