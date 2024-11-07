using Aplication.Interfaces.Observer;
using Aplication.Services.Observer;
using System;
using Unity;

namespace GUI.Controls
{
    public partial class ValidarCantidad : System.Web.UI.UserControl, IIdiomaService
    {
        private readonly IdiomaService _idiomaService;

        public ValidarCantidad()
        {
            _idiomaService = Global.Container.Resolve<IdiomaService>();
            _idiomaService.Subscribe(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarTextos();
            }
        }

        private void CargarTextos()
        {
            litCantidadExcedidaTitulo.Text = _idiomaService.GetTranslation("CantidadExcedidaTitulo");
            litCantidadExcedidaTexto.Text = _idiomaService.GetTranslation("CantidadExcedidaTexto");
            litCantidadActualizadaTitulo.Text = _idiomaService.GetTranslation("CantidadActualizadaTitulo");
            litCantidadActualizadaTexto.Text = _idiomaService.GetTranslation("CantidadActualizadaTexto");
            litConfirmButtonText.Text = _idiomaService.GetTranslation("ConfirmButtonText");
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