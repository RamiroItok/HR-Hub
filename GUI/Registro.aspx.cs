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

        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                var puesto = DropDownPuesto.Text;
                puesto = "Lider";

                Usuario usuario = new Usuario()
                {
                    Nombre = txtNombre.Text,
                    Apellido = txtApellido.Text,
                    Email = txtEmail.Text,
                    Contraseña = txtContraseña.Text,
                    Puesto = (Puesto)Enum.Parse(typeof(Puesto), puesto)
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
            txtNombre.Text = String.Empty;
            txtApellido.Text = String.Empty;
            txtEmail.Text = String.Empty;
            txtContraseña.Text = String.Empty;
        }
    }
}