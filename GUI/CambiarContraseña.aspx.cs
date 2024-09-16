using Aplication.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unity;

namespace GUI
{
    public partial class CambiarContraseña : Page
    {
        private readonly IUsuarioService _iUsuarioService;

        public CambiarContraseña()
        {
            _iUsuarioService = Global.Container.Resolve<IUsuarioService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCambiar_Click(object sender, EventArgs e)
        {
            var contraseñaActual = txtPasswordActual.Text;
            var contraseñaNueva = txtPasswordNueva.Text;
            var confirmarContraseña = txtPasswordConfirmar.Text;
            var usuario = Session["Usuario"] as Usuario;

            var esContraseñaValida = _iUsuarioService.ValidarContraseñas(usuario, contraseñaActual, contraseñaNueva, confirmarContraseña);

            if (esContraseñaValida != null)
            {
                lblMensaje.Text = esContraseñaValida;
                lblMensaje.CssClass = "message-label error";
                lblMensaje.Visible = true;
            }
            else
            {
                if (_iUsuarioService.ActualizarContraseña(usuario, contraseñaNueva, Models.Enums.TipoOperacionContraseña.Cambio))
                {
                    lblMensaje.Visible = true;
                    lblMensaje.CssClass = "message-label success";
                    lblMensaje.Text = "La contraseña se ha cambiado con éxito.";
                }
                else
                {
                    lblMensaje.Visible = true;
                    lblMensaje.CssClass = "message-label error";
                    lblMensaje.Text = "Hubo un problema al cambiar la contraseña.";
                }
            }
            
        }
    }
}