using Aplication.Interfaces;
using Models;
using Models.Enums;
using System;
using System.IO;
using Unity;

namespace GUI
{
    public partial class FalloIntegridad : System.Web.UI.Page
    {
        private readonly IBackUpService _backUpService;
        private readonly IDigitoVerificadorService _digitoVerificadorService;
        private readonly IBitacoraService _bitacoraService;

        public FalloIntegridad()
        {
            _backUpService = Global.Container.Resolve<IBackUpService>();
            _digitoVerificadorService = Global.Container.Resolve<IDigitoVerificadorService>();
            _bitacoraService = Global.Container.Resolve<IBitacoraService>();
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
                var usuario = Session["Usuario"] as Usuario;
                _digitoVerificadorService.RecalcularDV();
                _bitacoraService.AltaBitacora(usuario.Email, usuario.Puesto, "Se recalcularon los digitos verificadores", Models.Enums.Criticidad.ALTA);

                MostrarEstadoFallido(null);
                if (lblEstadoIntegridad.Text == "Estado: Saludable")
                {
                    lblMensajeRecalcular.Visible = true;
                    lblMensajeRecalcular.Text = "Se recalcularon correctamente los digitos verificadores del sistema";
                    lblMensajeRecalcular.CssClass = "fallo-text-success";
                }
            }
            catch
            {
                lblMensajeRecalcular.Text = "Se produjo un error al intentar recalcular los digitos verificadores";
            }
        }

        protected void btnRestore_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario usuario = Session["Usuario"] as Usuario;
                string rutaArchivo = Path.GetFileName(fileBackup.FileName);
                if (!string.IsNullOrEmpty(rutaArchivo))
                {
                    string rutaDestino = "D:/Backups/" + rutaArchivo;
                    fileBackup.SaveAs(rutaDestino);

                    var resultado = _backUpService.RealizarRestore(rutaDestino, usuario);
                    _bitacoraService.AltaBitacora(usuario.Email, usuario.Puesto, "Se realizó un restore.", Criticidad.ALTA);

                    lblMensajeRestore.Text = resultado;
                    lblMensajeRestore.Visible = true;
                    lblMensajeRestore.CssClass = "fallo-text-success";
                }
                else
                {
                    lblMensajeRestore.Text = "Debe seleccionar un archivo";
                    lblMensajeRestore.Visible = true;
                    lblMensajeRestore.CssClass = "fallo-text-failed";
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void MostrarEstadoFallido(Models.FalloIntegridad falloIntegridad)
        {
            if (falloIntegridad != null)
            {
                lblTablaFallo.Text = $"Fallo en tabla: {falloIntegridad.Tabla}";
                lblTablaFallo.CssClass = "fallo-text-failed";

                lblEstadoIntegridad.Text = "Estado: no saludable";
                lblEstadoIntegridad.CssClass = "fallo-text-failed";
            }
            else
            {
                lblEstadoIntegridad.Text = "Estado: Saludable";
                lblEstadoIntegridad.CssClass = "fallo-text-success";
                lblTablaFallo.Text = "Fallo en la tabla: -";
                lblTablaFallo.CssClass = "fallo-text-success";
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            fileBackup = null;
            lblMensajeRestore.Visible = false;
        }
    }
}