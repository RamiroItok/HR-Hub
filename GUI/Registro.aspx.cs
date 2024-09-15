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
        private readonly IBitacoraService _iBitacoraService;

        public Registro()
        {
            _usuarioService = Global.Container.Resolve<IUsuarioService>();
            _iBitacoraService = Global.Container.Resolve<IBitacoraService>();
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

                var esContraseñaValida = _usuarioService.ValidarFormatoContraseña(txtContraseña.Text);

                if (esContraseñaValida)
                {
                    var id = _usuarioService.RegistrarUsuario(usuario);

                    RegistrarBitacora();

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
            txtArea.Text = String.Empty;
            DropDownPuesto.SelectedIndex = 0;
        }

        private void CargarPuestos()
        {
            var resultado = _usuarioService.ObtenerPuestos();
            DropDownPuesto.DataSource = resultado;
            DropDownPuesto.DataTextField = "Puesto";
            DropDownPuesto.DataValueField = "Id";
            DropDownPuesto.DataBind();
        }

        private void RegistrarBitacora()
        {
            var user = Session["Usuario"] as Usuario;
            _iBitacoraService.AltaBitacora(user.Email, user.Puesto, "Da de alta un usuario", Models.Enums.Criticidad.BAJA);
        }
    }
}