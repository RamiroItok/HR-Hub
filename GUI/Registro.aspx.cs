using Aplication.Interfaces;
using Models;
using Models.Enums;
using System;
using System.Web.UI;
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
            if (!IsPostBack)
            {
                CargarPuestos();
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
                    Area = txtArea.Text,
                    FechaIngreso = DateTime.Now
                };

                var id = _usuarioService.RegistrarUsuario(usuario);

                lblMensaje.Visible = true;
                lblMensaje.Text = "Se ha registrado el usuario correctamente";
                Limpiar();
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
        }

        private void CargarPuestos()
        {
            var resultado = _usuarioService.ObtenerPuestos();
            DropDownPuesto.DataSource = resultado;
            DropDownPuesto.DataTextField = "Puesto";
            DropDownPuesto.DataValueField = "Id";
            DropDownPuesto.DataBind();
        }
    }
}