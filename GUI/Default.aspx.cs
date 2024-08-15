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
    }
}