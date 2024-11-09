using Aplication.Interfaces;
using Aplication.Interfaces.Observer;
using Aplication.Services.Observer;
using Models;
using Models.Composite;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unity;

namespace GUI
{
    public partial class MisDocumentos : Page, IIdiomaService
    {
        private readonly IDocumentoService _documentoService;
        private readonly IPermisoService _permisoService;
        private readonly IdiomaService _idiomaService;

        public MisDocumentos()
        {
            _documentoService = Global.Container.Resolve<IDocumentoService>();
            _permisoService = Global.Container.Resolve<IPermisoService>();
            _idiomaService = Global.Container.Resolve<IdiomaService>();
            _idiomaService.Subscribe(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var usuario = Session["Usuario"] as Usuario;

            if (!_permisoService.TienePermiso(usuario, Permiso.MisDocumentos))
            {
                Response.Redirect("AccesoDenegado.aspx");
                return;
            }

            if (!IsPostBack)
            {
                try
                {
                    if (Session["DocumentosLeidos"] == null)
                    {
                        Session["DocumentosLeidos"] = new HashSet<int>();
                    }

                    CargarDocumentos(false);

                    string selectedLanguage = Session["SelectedLanguage"] as string ?? "es";
                    ddlLanguage.SelectedValue = selectedLanguage;
                    _idiomaService.CurrentLanguage = selectedLanguage;

                    CargarTextos();
                }
                catch (Exception ex)
                {
                    lblMensaje.Text = _idiomaService.GetTranslation(ex.Message);
                    lblMensaje.Visible = true;
                    lblMensaje.CssClass = "text-danger";
                    CargarTextos();
                }
            }
            RegistrarEventosValidacion();
        }

        private void RegistrarEventosValidacion()
        {
            foreach (GridViewRow row in dataGridDocumentos.Rows)
            {
                Button btnLeer = row.FindControl("btnLeer") as Button;
                Button btnFirmar = row.FindControl("btnFirmar") as Button;

                if (btnLeer != null)
                    ClientScript.RegisterForEventValidation(btnLeer.UniqueID, btnLeer.CommandArgument);

                if (btnFirmar != null)
                    ClientScript.RegisterForEventValidation(btnFirmar.UniqueID, btnFirmar.CommandArgument);
            }
        }

        private void CargarTextos()
        {
            if (!(lblTituloMisDocumentos == null))
            {
                lblTituloMisDocumentos.Text = _idiomaService.GetTranslation("MisDocumentos");
                lblNoDocumentos.Text = _idiomaService.GetTranslation("NoHayDocumentos");
                chkFirmado.Text = _idiomaService.GetTranslation("Firmado");
                dataGridDocumentos.Columns[0].HeaderText = _idiomaService.GetTranslation("HeaderID");
                dataGridDocumentos.Columns[1].HeaderText = _idiomaService.GetTranslation("HeaderNombre");
                dataGridDocumentos.Columns[2].HeaderText = _idiomaService.GetTranslation("HeaderFechaEntrega");
                dataGridDocumentos.Columns[3].HeaderText = _idiomaService.GetTranslation("HeaderFechaFirma");
                dataGridDocumentos.Columns[4].HeaderText = _idiomaService.GetTranslation("HeaderFirmado");
                dataGridDocumentos.DataBind();

                foreach (GridViewRow row in dataGridDocumentos.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        Button btnLeer = (Button)row.FindControl("btnLeer");
                        Button btnFirmar = (Button)row.FindControl("btnFirmar");

                        if (btnLeer != null)
                            btnLeer.Text = _idiomaService.GetTranslation("ButtonLeerDocumento");

                        if (btnFirmar != null)
                            btnFirmar.Text = _idiomaService.GetTranslation("ButtonFirmarDocumento");
                    }
                }
            }
        }

        private void CargarDocumentos(bool firmado)
        {
            try
            {
                var userSession = Session["Usuario"] as Usuario;
                List<UsuarioDocumento> documentos = _documentoService.ObtenerDocumentosPorUsuario(firmado, userSession);
                dataGridDocumentos.DataSource = documentos;
                dataGridDocumentos.DataBind();

                lblNoDocumentos.Visible = documentos.Count == 0;
            }
            catch (Exception ex)
            {
                lblMensaje.Visible = true;
                lblMensaje.CssClass = "text-danger";
                lblMensaje.Text = _idiomaService.GetTranslation(ex.Message);
            }
        }

        protected void chkFirmado_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CargarDocumentos(chkFirmado.Checked);
                CargarTextos();
            }
            catch (Exception ex)
            {
                lblMensaje.Visible = true;
                lblMensaje.CssClass = "text-danger";
                lblMensaje.Text = _idiomaService.GetTranslation(ex.Message);
            }
        }

        protected void dataGridDocumentos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int documentoId = Convert.ToInt32(e.CommandArgument);

                if (e.CommandName == "Leer")
                {
                    MostrarDocumento(documentoId);

                    var documentosLeidos = Session["DocumentosLeidos"] as HashSet<int>;
                    if (!documentosLeidos.Contains(documentoId))
                    {
                        documentosLeidos.Add(documentoId);
                    }
                }
                else if (e.CommandName == "Firmar")
                {
                    var documentosLeidos = Session["DocumentosLeidos"] as HashSet<int>;
                    if (documentosLeidos.Contains(documentoId))
                    {
                        FirmarDocumento(documentoId);
                        CargarDocumentos(chkFirmado.Checked);

                        ScriptManager.RegisterStartupScript(this, GetType(), $"{_idiomaService.GetTranslation("TituloMisDocumentosConfirmacion")}", $"Swal.fire('Confirmación', '{_idiomaService.GetTranslation("DocumentoFirmadoExitosamente")}', 'success');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), $"{_idiomaService.GetTranslation("TituloMisDocumentosLeerAntes")}", $"Swal.fire('Advertencia', '{_idiomaService.GetTranslation("DocumentoDebeLeerAntes")}', 'warning');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                string titulo = _idiomaService.GetTranslation("MensajeErrorGeneral");
                string mensaje = _idiomaService.GetTranslation(ex.Message);
                ScriptManager.RegisterStartupScript(this, GetType(), $"{titulo}", $"Swal.fire('Advertencia', '{mensaje}', 'warning');", true);
            }
        }

        private void FirmarDocumento(int documentoId)
        {
            var userSession = Session["Usuario"] as Usuario;
            _documentoService.FirmarDocumento(documentoId, userSession);
        }

        private void MostrarDocumento(int documentoId)
        {
            byte[] contenido = _documentoService.ObtenerContenidoPorId(documentoId);
            if (contenido != null)
            {
                litDocumentoContenido.Text = $"<embed src='data:application/pdf;base64,{Convert.ToBase64String(contenido)}' width='100%' height='600px' type='application/pdf' />";
                
                ScriptManager.RegisterStartupScript(this, GetType(), "ShowModalScript", "$('#leerDocumentoModal').modal('show');", true);
            }
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

        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedLanguage = ddlLanguage.SelectedValue;
            Session["SelectedLanguage"] = selectedLanguage;
            _idiomaService.CurrentLanguage = selectedLanguage;
            CargarTextos();
            Response.Redirect(Request.RawUrl);
        }
    }
}