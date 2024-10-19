using Aplication;
using Aplication.Interfaces;
using Models;
using System;
using System.Web.UI;
using Unity;

namespace GUI
{
    public partial class MenuPrincipal : Page
    {
        private readonly IBackUpService _backUpService;
        private readonly IDigitoVerificadorService _digitoVerificadorService;

        public MenuPrincipal()
        {
            _backUpService = Global.Container.Resolve<IBackUpService>();
            _digitoVerificadorService = Global.Container.Resolve<IDigitoVerificadorService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                VerificarDV();
                var usuario = Session["Usuario"] as Usuario;

                var nombre = EncriptacionService.Decrypt_AES(usuario.Nombre);
                var apellido = EncriptacionService.Decrypt_AES(usuario.Apellido);

                lblNombreUsuario.Text = $"{usuario.Puesto}, {nombre} {apellido}";
                lblNombreUsuarioProfile.Text = $"{nombre} {apellido}";
            }
        }

        private void VerificarDV()
        {
            try
            {
                if (Session["ErrorVerificacionDV"] == null)
                {
                    _backUpService.CrearBaseDeDatos();
                    string mensaje = _digitoVerificadorService.VerificarDV();
                    if (mensaje != "true")
                    {
                        Session["ErrorVerificacionDV"] = true;
                        Response.Redirect($"ErrorDigitoVerificador.aspx?mensaje={mensaje}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Se ha producido un error al verificar los digitos verificadores");
            }
        }
    }
}