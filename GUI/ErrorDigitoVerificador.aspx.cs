using Models;
using System;

namespace GUI
{
    public partial class ErrorDigitoVerificador : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string tabla = Request.QueryString["mensaje"];
                if (!string.IsNullOrEmpty(tabla))
                {
                    lblErrorMessage.Text = $"Se realizaron modificaciones de datos, de manera externa, en la tabla {tabla}";
                }
                else
                {
                    lblErrorMessage.Text = "Se ha producido un error desconocido.";
                }
            }
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            var usuario = Session["Usuario"] as Usuario;
            if (usuario != null && usuario.Puesto != Models.Enums.Puesto.WebMaster)
            {
                Session.Abandon();
                Response.Redirect("Home.aspx");
            }
            else
            {
                Response.Redirect("MenuPrincipal.aspx");
            }
        }
    }
}