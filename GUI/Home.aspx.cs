using Aplication;
using Models;
using System;
using System.Web.UI;

namespace GUI
{
    public partial class Home : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarUsuario();
            }
        }

        private void CargarUsuario()
        {
            if (Session["Usuario"] != null)
            {
                Usuario usuario = (Usuario)Session["Usuario"];

                var nombre = EncriptacionService.Decrypt_AES(usuario.Nombre);
                var apellido = EncriptacionService.Decrypt_AES(usuario.Apellido);
                
                lblBienvenido.Text = $"Bienvenido {usuario.Puesto}, {nombre} {apellido}!";
                lblBienvenido.Visible = true;
            }
            else
            {
                lblBienvenido.Visible = false;
            }
        }
    }
}