using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aplication.Interfaces;
using Aplication.Interfaces.Observer;
using Aplication.Services.Observer;
using Aplication.Services.XML;
using Models;
using Models.Composite;
using Unity;

namespace GUI
{
    public partial class Bitacora : Page, IIdiomaService
    {
        private readonly IBitacoraService _iBitacoraService;
        private readonly IUsuarioService _iUsuarioService;
        private readonly IPermisoService _iPermisoService;
        private readonly IdiomaService _idiomaService;
        private static List<Models.Bitacora> listaEventos = new List<Models.Bitacora>();

        public Bitacora()
        {
            _iBitacoraService = Global.Container.Resolve<IBitacoraService>();
            _iUsuarioService = Global.Container.Resolve<IUsuarioService>();
            _iPermisoService = Global.Container.Resolve<IPermisoService>();
            _idiomaService = Global.Container.Resolve<IdiomaService>();
            _idiomaService.Subscribe(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var usuario = Session["Usuario"] as Usuario;
            if(!_iPermisoService.TienePermiso(usuario, Permiso.Bitacora))
            {
                Response.Redirect("AccesoDenegado.aspx");
                return;
            }
            try
            {
                if (!IsPostBack)
                {
                    string selectedLanguage = Session["SelectedLanguage"] as string ?? "es";
                    ddlLanguage.SelectedValue = selectedLanguage;
                    _idiomaService.CurrentLanguage = selectedLanguage;
                    CargarEventosInicio();
                    CargarUsuarios();
                    CargarTipoUsuario();
                    CargarCriticidad();
                }
            }
            catch (Exception ex)
            {
                lblMensaje.CssClass = "text-danger";
                lblMensaje.Visible = true;
                lblMensaje.Text = _idiomaService.GetTranslation(ex.Message);
            }
            finally
            {
                if (!IsPostBack)
                    CargarTextos();
            }
        }

        private void CargarEventosInicio()
        {
            listaEventos = _iBitacoraService.ListarEventos();
            CargarEventosDefault();
        }

        private void CargarTextos()
        {
            if (!(litTituloBitacora == null))
            {
                litTituloBitacora.Text = _idiomaService.GetTranslation("TituloBitacora");
                Page.Title = _idiomaService.GetTranslation("PageTitleBitacora");
                lblSearch.Text = _idiomaService.GetTranslation("LabelTextoBusqueda");
                txtSearch.Attributes["placeholder"] = _idiomaService.GetTranslation("PlaceholderTextoBusqueda");
                lblUsuario.Text = _idiomaService.GetTranslation("LabelUsuario");
                lblTipoUsuario.Text = _idiomaService.GetTranslation("LabelTipoUsuario");
                lblCriticidad.Text = _idiomaService.GetTranslation("LabelCriticidad");
                lblFechaDesde.Text = _idiomaService.GetTranslation("LabelFechaDesde");
                lblFechaHasta.Text = _idiomaService.GetTranslation("LabelFechaHasta");
                btnBuscar.Text = _idiomaService.GetTranslation("ButtonBuscar");
                btnCancelar.Text = _idiomaService.GetTranslation("ButtonCancelar");
                btnExportarExcel.Text = _idiomaService.GetTranslation("ExportarExcel");
                btnGenerarXML.Text = _idiomaService.GetTranslation("GenerarXML");
                btnBajaBitacora.Text = _idiomaService.GetTranslation("BajaBitacora");
            }
        }

        protected void gvBitacora_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvBitacora.PageIndex = e.NewPageIndex;

            if (Session["EventosFiltrados"] != null)
            {
                var eventosFiltrados = (List<Models.Bitacora>)Session["EventosFiltrados"];
                CargarGrilla(eventosFiltrados);
            }
            else
            {
                CargarEventosDefault();
            }
        }

        private void CargarEventosDefault()
        {
            var resultado = listaEventos
                            .OrderByDescending(p => p.Id)
                            .Take(50)
                            .ToList();
            CargarGrilla(resultado);
        }

        private void CargarUsuarios()
        {
            drpUsuarios.DataSource = _iUsuarioService.ListarUsuarios();
            drpUsuarios.DataTextField = "Email";
            drpUsuarios.DataValueField = "Id";
            drpUsuarios.DataBind();
            drpUsuarios.Items.Insert(0, new ListItem(_idiomaService.GetTranslation("SeleccioneUsuario"), ""));
        }

        private void CargarTipoUsuario()
        {
            drpTipoUsuario.DataSource = _iPermisoService.ObtenerFamilias();
            drpTipoUsuario.DataTextField = "Nombre";
            drpTipoUsuario.DataValueField = "Id";
            drpTipoUsuario.DataBind();
            drpTipoUsuario.Items.Insert(0, new ListItem(_idiomaService.GetTranslation("SeleccioneTipoUsuario"), ""));
        }

        private void CargarCriticidad()
        {
            List<string> items = new List<string> { "BAJA", "MEDIA", "ALTA" };
            drpCriticidad.DataSource = items;
            drpCriticidad.DataBind();
            drpCriticidad.Items.Insert(0, new ListItem(_idiomaService.GetTranslation("SeleccioneCriticidad"), ""));
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            DateTime? fechaDesde = string.IsNullOrEmpty(txtFechaDesde.Text) ? (DateTime?)null : DateTime.Parse(txtFechaDesde.Text);
            DateTime? fechaHasta = string.IsNullOrEmpty(txtFechaHasta.Text) ? (DateTime?)null : DateTime.Parse(txtFechaHasta.Text);


            if ((fechaDesde.HasValue && fechaHasta.HasValue) && fechaDesde.Value > fechaHasta.Value)
            {
                lblMensaje.CssClass = "text-danger";
                lblMensaje.Visible = true;
                lblMensaje.Text = _idiomaService.GetTranslation("ErrorFecha");
            }
            else
            {
                List<Models.Bitacora> listaEventosFiltrados = new List<Models.Bitacora>();

                listaEventosFiltrados = listaEventos
                                            .Where(x =>
                                                (string.IsNullOrEmpty(drpUsuarios.Text) || x.Email == drpUsuarios.SelectedItem.Text)
                                                && (string.IsNullOrEmpty(drpCriticidad.Text) || x.Criticidad == drpCriticidad.SelectedItem.Text)
                                                && (string.IsNullOrEmpty(drpTipoUsuario.Text) || x.TipoUsuario == drpTipoUsuario.SelectedItem.Text)
                                                && (string.IsNullOrEmpty(txtSearch.Text) || x.Descripcion.ToLower().Contains(txtSearch.Text.ToLower()))
                                                && (!fechaDesde.HasValue || x.Fecha >= fechaDesde)
                                                && (!fechaHasta.HasValue || x.Fecha <= fechaHasta)
                                            )
                                            .OrderByDescending(x => x.Id)
                                            .Take(50)
                                            .ToList();

                Session["EventosFiltrados"] = listaEventosFiltrados;
                lblMensaje.Visible = false;
                CargarGrilla(listaEventosFiltrados);
            }
            
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Limpiar();
            gvBitacora.PageIndex = 0;
            CargarEventosDefault();

            Session["EventosFiltrados"] = null;
        }

        private void CargarGrilla(List<Models.Bitacora> listadoBitacora)
        {
            gvBitacora.DataSource = listadoBitacora;
            gvBitacora.DataBind();
        }

        private void Limpiar()
        {
            txtSearch.Text = String.Empty;
            txtFechaDesde.Text = String.Empty;
            txtFechaHasta.Text = String.Empty;
            drpUsuarios.SelectedIndex = 0;
            drpTipoUsuario.SelectedIndex = 0;
            drpCriticidad.SelectedIndex = 0;
            lblMensaje.Text = String.Empty;
            lblMensaje.Visible = false;
        }

        protected void btnGenerarXML_Click(object sender, EventArgs e)
        {
            var registrosBitacora = listaEventos;

            if (registrosBitacora == null || !registrosBitacora.Any())
            {
                string noDataScript = $"Swal.fire('{_idiomaService.GetTranslation("TextoSinDatos")}', '{_idiomaService.GetTranslation("NoDatosXML")}', 'error');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "noDataScript", noDataScript, true);
                return;
            }

            GenerarXML generadorXml = new GenerarXML();
            generadorXml.GenerarXMLBitacora(registrosBitacora);

            string successScript = $"Swal.fire('{_idiomaService.GetTranslation("XMLGenerado")}', '{_idiomaService.GetTranslation("MensajeReporteExitoso")}', 'success');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "successScript", successScript, true);
        }

        protected void btnExportarExcel_Click(object sender, EventArgs e)
        {
            if (Session["EventosFiltrados"] != null)
            {
                gvBitacora.DataSource = (List<Models.Bitacora>)Session["EventosFiltrados"];
            }
            else
            {
                gvBitacora.DataSource = listaEventos
                            .OrderByDescending(p => p.Id)
                            .Take(50)
                            .ToList();
            } 

            gvBitacora.AllowPaging = false;
            gvBitacora.DataBind();

            string nombreArchivo = $"Bitacora - {DateTime.Now}.xls";

            Response.Clear();
            Response.Buffer = true;

            Response.AddHeader("content-disposition", $"attachment;filename={nombreArchivo}");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";

            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    string style = @"<style> .textmode { } </style>";
                    Response.Write(style);

                    gvBitacora.RenderControl(hw);

                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }

            gvBitacora.AllowPaging = true;
            CargarEventosDefault();
        }

        protected void btnBajaBitacora_Click(object sender, EventArgs e)
        {
            try
            {
                List<Models.Bitacora> eventosParaBaja;

                if (Session["EventosFiltrados"] != null)
                {
                    eventosParaBaja = (List<Models.Bitacora>)Session["EventosFiltrados"];
                }
                else
                {
                    eventosParaBaja = listaEventos;
                }

                var userSession = Session["Usuario"] as Usuario;
                _iBitacoraService.BajaBitacora(eventosParaBaja, userSession);

                gvBitacora.DataSource = eventosParaBaja;
                gvBitacora.AllowPaging = false;
                gvBitacora.DataBind();

                string nombreArchivo = $"Bitacora - {DateTime.Now:yyyyMMddHHmmss}.xls";

                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", $"attachment;filename={nombreArchivo}");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";

                using (StringWriter sw = new StringWriter())
                {
                    using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                    {
                        string style = @"<style> .textmode { } </style>";
                        Response.Write(style);

                        gvBitacora.RenderControl(hw);

                        Response.Output.Write(sw.ToString());
                        Response.Flush();
                        Response.End();
                    }
                }

                gvBitacora.AllowPaging = true;
                CargarEventosInicio();
            }
            catch (Exception ex)
            {
                string successScript = $"Swal.fire('{_idiomaService.GetTranslation("MensajeErrorGeneral")}', '{_idiomaService.GetTranslation(ex.Message)}', 'error');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "successScript", successScript, true);
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
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
            Response.Redirect(Request.RawUrl);
        }
    }
}
