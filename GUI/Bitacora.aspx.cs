using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Aplication.Interfaces;
using Unity;

namespace GUI
{
    public partial class Bitacora : Page
    {
        private readonly IBitacoraService _iBitacoraService;
        private static List<Models.Bitacora> listaEventos = new List<Models.Bitacora>();

        public Bitacora()
        {
            _iBitacoraService = Global.Container.Resolve<IBitacoraService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarEventosDefault();
            }
        }

        private void CargarEventosDefault()
        {
            listaEventos = _iBitacoraService.ListarEventos();
            CargarGrilla(listaEventos);
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            DateTime? fechaDesde = string.IsNullOrEmpty(txtFechaDesde.Text) ? (DateTime?)null : DateTime.Parse(txtFechaDesde.Text);
            DateTime? fechaHasta = string.IsNullOrEmpty(txtFechaHasta.Text) ? (DateTime?)null : DateTime.Parse(txtFechaHasta.Text);

            if(txtSearch.Text != string.Empty || fechaDesde != null || fechaHasta != null)
            {
                var eventoFiltrado = listaEventos
                    .Where(x => (x.Descripcion.ToLower().Contains(txtSearch.Text.ToLower()) ||
                            x.TipoUsuario.ToLower().Contains(txtSearch.Text.ToLower())) &&
                            (!fechaDesde.HasValue || x.Fecha >= fechaDesde.Value) &&
                            (!fechaHasta.HasValue || x.Fecha <= fechaHasta.Value))
                    .OrderByDescending(x => x.Fecha)
                    .ToList();

                CargarGrilla(eventoFiltrado);
            }

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Limpiar();
            CargarGrilla(listaEventos);
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
        }
    }
}