using Aplication;
using Models;
using System;
using System.Data;
using System.Web.UI;

namespace GUI
{
    public partial class MenuPrincipal : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var usuario = Session["Usuario"] as Usuario;

                var nombre = EncriptacionService.Decrypt_AES(usuario.Nombre);
                var apellido = EncriptacionService.Decrypt_AES(usuario.Apellido);

                lblNombreUsuario.Text = $"{usuario.Puesto}, {nombre} {apellido}";
                lblNombreUsuarioProfile.Text = $"{nombre} {apellido}";
            }
        }
    }
}