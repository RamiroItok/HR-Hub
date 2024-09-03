using Models;
using System;
using System.Web.UI;

namespace GUI.Controls
{
    public partial class NavBar : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var usuario = Session["Usuario"] as Usuario;
                if (usuario != null)
                {
                    loginLink.Visible = false;
                    logoutLink.Visible = true;
                    seguridadLink.Visible = true;
                    registroLink.Visible = true;
                    bitacoraLink.Visible = true;
                }
                else
                {
                    loginLink.Visible = true;
                    logoutLink.Visible = false;
                    seguridadLink.Visible = false;
                    registroLink.Visible = false;
                    bitacoraLink.Visible = false;
                }
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("Default.aspx");
        }
    }
}
