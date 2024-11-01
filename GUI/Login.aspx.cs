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

        public Login()
        {
            _usuarioService = Global.Container.Resolve<IUsuarioService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] != null)
                Response.Redirect("/MenuPrincipal.aspx");
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarEmailControl.EsEmailValido())
                {
                    if (PasswordValidator.IsValid)
                    {
                        var usuario = _usuarioService.ObtenerUsuarioPorEmail(ValidarEmailControl.Email);
                        string password = PasswordValidator.Password;

                        lblMensaje.Text = _usuarioService.ValidarUsuario(usuario, ValidarEmailControl.Email, password);

                        if (lblMensaje.Text != "")
                            lblMensaje.Visible = true;
                        else
                        {
                            Session["Usuario"] = usuario;
                            Response.Redirect("MenuPrincipal.aspx", false);
                            Context.ApplicationInstance.CompleteRequest();
                        }
                    }
                    else
                    {
                        throw new Exception("Contraseña no valida");
                    }
                }
                else
                {
                    throw new Exception("Email no valido");
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.Visible = true;
            }
        }
    }
}