using Aplication.Interfaces;
using System;
using Unity;

namespace GUI
{
    public partial class FalloIntegridad : System.Web.UI.Page
    {
        private readonly IBackUpService _backUpService;
        private readonly IDigitoVerificadorService _digitoVerificadorService;

        public FalloIntegridad()
        {
            _backUpService = Global.Container.Resolve<IBackUpService>();
            _digitoVerificadorService = Global.Container.Resolve<IDigitoVerificadorService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MostrarEstadoFallido(Session["ErrorVerificacionDV"] as Models.FalloIntegridad);
            }
        }

        protected void btnRecalcular_Click(object sender, EventArgs e)
        {
            try
            {
                _digitoVerificadorService.RecalcularDV();
                MostrarEstadoFallido(null);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Se produjo un error al intentar recalcular los digitos verificadores";
            }
        }

        protected void btnRestore_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private void MostrarEstadoFallido(Models.FalloIntegridad falloIntegridad)
        {
            if (falloIntegridad != null)
            {
                lblTablaFallo.Text = $"Fallo en tabla: {falloIntegridad.Tabla}";

                lblEstadoIntegridad.Text = "Estado: no saludable";
                lblEstadoIntegridad.CssClass = "fallo-status-label fallo-status-fallo";
            }
            else
            {
                lblEstadoIntegridad.Text = "Estado: Saludable";
                lblEstadoIntegridad.CssClass = "fallo-status-label fallo-status-saludable";
                lblTablaFallo.Text = "Fallo en la tabla: -";
                lblTablaFallo.CssClass = "fallo-text-success";
            }
        }
    }
}