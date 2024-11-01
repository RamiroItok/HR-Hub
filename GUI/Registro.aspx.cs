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
                if (CamposLlenos())
                {
                    Usuario usuario = new Usuario()
                    {
                        Nombre = txtNombre.Text,
                        Apellido = txtApellido.Text,
                        Email = ValidarEmail.Email,
                        Contraseña = hiddenContraseña.Value,
                        Area = (Area)Enum.Parse(typeof(Area), DropDownArea.Text),
                        FechaNacimiento = DateTime.Parse(txtFechaNac.Value),
                        Genero = drpGenero.SelectedItem.Text,
                        FechaIngreso = DateTime.Now,

                        Direccion = ValidarRegistroUsuarioDatosControl.Direccion,
                        NumeroDireccion = ValidarRegistroUsuarioDatosControl.NumeroDireccion,
                        Departamento = ValidarRegistroUsuarioDatosControl.Departamento,
                        CodigoPostal = ValidarRegistroUsuarioDatosControl.CodigoPostal,
                        Ciudad = ValidarRegistroUsuarioDatosControl.Ciudad,
                        Provincia = ValidarRegistroUsuarioDatosControl.Provincia,
                        Pais = ValidarRegistroUsuarioDatosControl.Pais
                    };

                    var esContraseñaValida = _usuarioService.ValidarFormatoContraseña(usuario.Contraseña);
                    if (esContraseñaValida)
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
                        throw new Exception("La contraseña debe tener al menos una mayúscula, una minúscula, un carácter especial, un número, y debe ser de 8 caracteres en total.");
                    }
                }
                else
                {
                    throw new Exception("Hay campos sin completar");
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Visible = true;
                lblMensaje.CssClass = "text-danger";
                lblMensaje.Text = ex.Message;
            }
        }

        private bool CamposLlenos()
        {
            if (string.IsNullOrWhiteSpace(txtApellido.Text) || string.IsNullOrWhiteSpace(hiddenContraseña.Value) || string.IsNullOrWhiteSpace(ValidarEmail.Email) || string.IsNullOrWhiteSpace(txtFechaNac.Value) || 
                string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(ValidarRegistroUsuarioDatosControl.Pais) || string.IsNullOrWhiteSpace(ValidarRegistroUsuarioDatosControl.Departamento) || 
                string.IsNullOrWhiteSpace(ValidarRegistroUsuarioDatosControl.Ciudad) || string.IsNullOrWhiteSpace(ValidarRegistroUsuarioDatosControl.CodigoPostal) || 
                string.IsNullOrWhiteSpace(ValidarRegistroUsuarioDatosControl.Direccion) || string.IsNullOrWhiteSpace(ValidarRegistroUsuarioDatosControl.NumeroDireccion.ToString()))
                return false;

            return true;
        }

        private void Limpiar()
        {
            txtNombre.Text = String.Empty;
            txtApellido.Text = String.Empty;
            ValidarEmail.Email = String.Empty;
            txtContraseña.Text = String.Empty;
            txtFechaNac.Value = String.Empty;
            DropDownArea.SelectedIndex = 0;
            drpGenero.SelectedIndex = 0;
            hiddenContraseña.Value = String.Empty;

            ValidarRegistroUsuarioDatosControl.Limpiar();
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
