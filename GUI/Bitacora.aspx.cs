using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aplication.Interfaces;
using Unity;

namespace GUI
{
    public partial class Bitacora : Page
    {
        private readonly IBitacoraService _iBitacoraService;
        private readonly IUsuarioService _iUsuarioService;
        private readonly IPermisoService _iPermisoService;
        private static List<Models.Bitacora> listaEventos = new List<Models.Bitacora>();

        public Bitacora()
        {
            _iBitacoraService = Global.Container.Resolve<IBitacoraService>();
            _iUsuarioService = Global.Container.Resolve<IUsuarioService>();
            _iPermisoService = Global.Container.Resolve<IPermisoService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                listaEventos = _iBitacoraService.ListarEventos();
                CargarEventosDefault();
                CargarUsuarios();
                CargarTipoUsuario();
                CargarCriticidad();
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
            drpUsuarios.Items.Insert(0, new ListItem("Seleccione un usuario", ""));
        }

        private void CargarTipoUsuario()
        {
            drpTipoUsuario.DataSource = _iPermisoService.ObtenerFamilias();
            drpTipoUsuario.DataTextField = "Nombre";
            drpTipoUsuario.DataValueField = "Id";
            drpTipoUsuario.DataBind();
            drpTipoUsuario.Items.Insert(0, new ListItem("Seleccione un tipo de usuario", ""));
        }

        private void CargarCriticidad()
        {
            List<string> items = new List<string> { "BAJA", "MEDIA", "ALTA" };
            drpCriticidad.DataSource = items;
            drpCriticidad.DataBind();
            drpCriticidad.Items.Insert(0, new ListItem("Seleccione una criticidad", ""));
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            DateTime? fechaDesde = string.IsNullOrEmpty(txtFechaDesde.Value) ? (DateTime?)null : DateTime.Parse(txtFechaDesde.Value);
            DateTime? fechaHasta = string.IsNullOrEmpty(txtFechaHasta.Value) ? (DateTime?)null : DateTime.Parse(txtFechaHasta.Value);


            if ((fechaDesde.HasValue && fechaHasta.HasValue) && fechaDesde.Value > fechaHasta.Value)
            {
                lblMensaje.CssClass = "text-danger";
                lblMensaje.Visible = true;
                lblMensaje.Text = "El campo 'Fecha Desde' no puede ser mayor que el campo 'Fecha Hasta'";
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
                                            .ToList();

                Session["EventosFiltrados"] = listaEventosFiltrados;

                CargarGrilla(listaEventosFiltrados);
            }
            
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Limpiar();
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
            txtFechaDesde.Value = String.Empty;
            txtFechaHasta.Value = String.Empty;
            drpUsuarios.SelectedIndex = 0;
            drpTipoUsuario.SelectedIndex = 0;
            drpCriticidad.SelectedIndex = 0;
            lblMensaje.Text = String.Empty;
            lblMensaje.Visible = false;
        }
    }
}
