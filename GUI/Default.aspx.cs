using Models;
using System;
using System.Web.UI;

namespace GUI
{
    public partial class _Default : Page
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
                
                lblBienvenido.Text = $"Bienvenido {usuario.Puesto}, {usuario.Nombre} {usuario.Apellido}!";
                lblBienvenido.Visible = true;
            }
            else
            {
                lblBienvenido.Visible = false;
            }
        }
    }
}