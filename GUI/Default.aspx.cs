using Models;
using System;
using System.Web.UI;

namespace GUI
{
    public partial class _Default : Page
    {
        public Usuario usuario { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                usuario = Session["Usuario"] as Usuario;

                if (usuario == null)
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }

        protected void btn_CerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }
    }
}