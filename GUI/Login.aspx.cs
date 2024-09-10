using Aplication.Interfaces;
using Models.Enums;
using System;
using System.Web.UI;
using Unity;

namespace GUI
{
    public partial class Login : Page
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IBitacoraService _bitacoraService;

        public Login()
        {
            _usuarioService = Global.Container.Resolve<IUsuarioService>();
            _bitacoraService = Global.Container.Resolve<IBitacoraService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] != null)
                Response.Redirect("/Default.aspx");
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            var resultadoValidacion = _usuarioService.ValidarCampos(txtEmail.Text, txtPassword.Text);

            if (!string.IsNullOrEmpty(resultadoValidacion))
            {
                lblMensaje.Text = resultadoValidacion;
                lblMensaje.Visible = true;
            }
            else
            {
                var usuario = _usuarioService.ValidarUsuarioContraseña(txtEmail.Text, txtPassword.Text);

                if (usuario == null)
                {
                    lblMensaje.Text = "El email no coincide con la contraseña.";
                    lblMensaje.Visible = true;
                }
                else
                {
                    Session["Usuario"] = usuario;
                    _bitacoraService.AltaBitacora(usuario.Email, usuario.Puesto, "Inicio de sesion", Criticidad.MEDIA);
                    Response.Redirect("Default.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
        }
    }
}