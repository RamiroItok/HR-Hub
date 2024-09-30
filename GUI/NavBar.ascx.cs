using Aplication.Interfaces;
using Models;
using System;
using System.Web.UI;
using Unity;

namespace GUI.Controls
{
    public partial class NavBar : UserControl
    {
        private readonly IBitacoraService _bitacoraService;
        public NavBar()
        {
            _bitacoraService = Global.Container.Resolve<IBitacoraService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var usuario = Session["Usuario"] as Usuario;
                if (usuario != null)
                {
                    loginLink.Visible = false;
                    logoutLink.Visible = true;
                    seguridadLink.Visible = false;
                    registroLink.Visible = false;
                    bitacoraLink.Visible = true;
                    miCuentaLink.Visible = false;
                }
                else
                {
                    loginLink.Visible = true;
                    logoutLink.Visible = false;
                    seguridadLink.Visible = false;
                    registroLink.Visible = false;
                    bitacoraLink.Visible = false;
                    miCuentaLink.Visible = false;
                }
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            var usuario = Session["Usuario"] as Usuario;

            _bitacoraService.AltaBitacora(usuario.Email, usuario.Puesto, "Cierra sesion", Models.Enums.Criticidad.BAJA);

            Session.Abandon();
            Response.Redirect("Home.aspx");
        }

        protected void btnInicio_Click(object sender, EventArgs e)
        {
            var usuario = Session["Usuario"] as Usuario;

            if (usuario != null)
            {
                Response.Redirect("Inicio.aspx");
            }
            else{
                Response.Redirect("Home.aspx");
            }
        }
    }
}
