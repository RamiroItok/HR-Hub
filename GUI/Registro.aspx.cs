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
        private readonly IBitacoraService _iBitacoraService;

        public Registro()
        {
            _usuarioService = Global.Container.Resolve<IUsuarioService>();
            _iBitacoraService = Global.Container.Resolve<IBitacoraService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    CargarPuestos();
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
                    Contraseña = txtContraseña.Text,
                    Puesto = (Puesto)Enum.Parse(typeof(Puesto), DropDownPuesto.Text),
                    Area = (Area)Enum.Parse(typeof(Area), DropDownArea.Text),
                    FechaNacimiento = DateTime.Parse(txtFechaNac.Value),
                    Genero = drpGenero.ToString(),
                    FechaIngreso = DateTime.Now
                };

                var esContraseñaValida = _usuarioService.ValidarFormatoContraseña(txtContraseña.Text);

                if (esContraseñaValida)
                {
                    var userSession = Session["Usuario"] as Usuario;
                    var id = _usuarioService.RegistrarUsuario(usuario, userSession);

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
            DropDownPuesto.SelectedIndex = 0;
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

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }
    }
}