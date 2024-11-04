using Aplication.Interfaces;
using Aplication.Interfaces.Observer;
using Aplication.Services.Observer;
using Models;
using Models.Composite;
using Models.Enums;
using System;
using System.IO;
using Unity;

namespace GUI
{
    public partial class FalloIntegridad : System.Web.UI.Page, IIdiomaService
    {
        private readonly IBackUpService _backUpService;
        private readonly IDigitoVerificadorService _digitoVerificadorService;
        private readonly IBitacoraService _bitacoraService;
        private readonly IPermisoService _permisoService;
        private readonly IdiomaService _idiomaService;

        public FalloIntegridad()
        {
            _backUpService = Global.Container.Resolve<IBackUpService>();
            _digitoVerificadorService = Global.Container.Resolve<IDigitoVerificadorService>();
            _bitacoraService = Global.Container.Resolve<IBitacoraService>();
            _permisoService = Global.Container.Resolve<IPermisoService>();
            _idiomaService = Global.Container.Resolve<IdiomaService>();
            _idiomaService.Subscribe(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var usuario = Session["Usuario"] as Usuario;
            if(!_permisoService.TienePermiso(usuario, Permiso.FalloIntegridad))
            {
                Response.Redirect("AccesoDenegado.aspx");
                return;
            }

            if (!IsPostBack)
            {
                MostrarEstadoFallido(Session["ErrorVerificacionDV"] as Models.FalloIntegridad);
                string selectedLanguage = Session["SelectedLanguage"] as string ?? "es";
                _idiomaService.CurrentLanguage = selectedLanguage;
                CargarTextos();
            }
        }

        private void CargarTextos()
        {
            litPageTitle.Text = _idiomaService.GetTranslation("PageTitleFalloIntegridad");
            litRecalcularTitulo.Text = _idiomaService.GetTranslation("TituloRecalcularDV");
            litRestoreTitulo.Text = _idiomaService.GetTranslation("TituloRestore");
            litSeleccionarArchivo.Text = _idiomaService.GetTranslation("LabelSeleccionarArchivo");
            btnRecalcular.Text = _idiomaService.GetTranslation("BotonRecalcularDV");
            btnRestore.Text = _idiomaService.GetTranslation("BotonRealizarRestore");
            btnCancelar.Text = _idiomaService.GetTranslation("BotonCancelar");
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
                    lblMensajeRecalcular.Text = _idiomaService.GetTranslation("MensajeRecalculoExito");
                    lblMensajeRecalcular.CssClass = "fallo-text-success";
                }
            }
            catch
            {
                lblMensajeRecalcular.Text = _idiomaService.GetTranslation("MensajeErrorRecalculo");
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

                    lblMensajeRestore.Text = _backUpService.RealizarRestore(rutaDestino, usuario);
                    lblMensajeRestore.Visible = true;
                    lblMensajeRestore.CssClass = "fallo-text-success";
                }
                else
                {
                    throw new Exception(_idiomaService.GetTranslation("MensajeArchivoNoSeleccionado"));
                }
                
            }
            catch (Exception ex)
            {
                lblMensajeRestore.Text = _idiomaService.GetTranslation("MensajeErrorRestore") + ex.Message;
                lblMensajeRestore.Visible = true;
                lblMensajeRestore.CssClass = "fallo-text-failed";
            }
        }

        private void MostrarEstadoFallido(Models.FalloIntegridad falloIntegridad)
        {
            if (falloIntegridad != null)
            {
                lblTablaFallo.Text = _idiomaService.GetTranslation("LabelTablaFallo") + falloIntegridad.Tabla;
                lblTablaFallo.CssClass = "fallo-text-failed";

                lblEstadoIntegridad.Text = _idiomaService.GetTranslation("EstadoNoSaludable");
                lblEstadoIntegridad.CssClass = "fallo-text-failed";
            }
            else
            {
                lblEstadoIntegridad.Text = _idiomaService.GetTranslation("EstadoSaludable");
                lblEstadoIntegridad.CssClass = "fallo-text-success";
                lblTablaFallo.Text = _idiomaService.GetTranslation("LabelTablaFalloSaludable");
                lblTablaFallo.CssClass = "fallo-text-success";
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            fileBackup = null;
            lblMensajeRestore.Visible = false;
        }

        public void UpdateLanguage(string language)
        {
            CargarTextos();
        }

        protected override void OnUnload(EventArgs e)
        {
            _idiomaService.Unsubscribe(this);
            base.OnUnload(e);
        }
    }
}