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
            CargarPuestos();
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario usuario = new Usuario()
                {
                    Nombre = txtNombre.Value,
                    Apellido = txtApellido.Value,
                    Email = txtEmail.Value,
                    Contraseña = txtContraseña.Value,
                    Puesto = (Puesto)Enum.Parse(typeof(Puesto), DropDownPuesto.Text)
                };

                var id = _usuarioService.RegistrarUsuario(usuario);

                lblResultado.Text = "Se ha registrado el usuario correctamente";
                Limpiar();
            }
            catch (Exception ex)
            {
                lblResultado.Text = ex.Message;
            }
        }

        private void Limpiar()
        {
            txtNombre.Value = String.Empty;
            txtApellido.Value = String.Empty;
            txtEmail.Value = String.Empty;
            txtContraseña.Value = String.Empty;
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