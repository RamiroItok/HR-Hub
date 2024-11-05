using Aplication.Interfaces.Observer;
using Aplication.Services.Observer;
using System;
using System.Web.UI;
using Unity;

namespace GUI.Controls
{
    public partial class ValidarRegistroUsuarioDatos : UserControl, IIdiomaService
    {

        private readonly IdiomaService _idiomaService;

        public ValidarRegistroUsuarioDatos()
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
            if (!(lblDireccion == null))
            {
                lblDireccion.Text = _idiomaService.GetTranslation("LabelDireccion");
                txtDireccion.Attributes["placeholder"] = _idiomaService.GetTranslation("PlaceholderDireccion");

                lblNumeroDireccion.Text = _idiomaService.GetTranslation("LabelNumeroDireccion");
                txtNumeroDireccion.Attributes["placeholder"] = _idiomaService.GetTranslation("PlaceholderNumeroDireccion");

                lblDepartamento.Text = _idiomaService.GetTranslation("LabelDepartamento");
                txtDepartamento.Attributes["placeholder"] = _idiomaService.GetTranslation("PlaceholderDepartamento");

                lblCodigoPostal.Text = _idiomaService.GetTranslation("LabelCodigoPostal");
                txtCodigoPostal.Attributes["placeholder"] = _idiomaService.GetTranslation("PlaceholderCodigoPostal");

                lblCiudad.Text = _idiomaService.GetTranslation("LabelCiudad");
                txtCiudad.Attributes["placeholder"] = _idiomaService.GetTranslation("PlaceholderCiudad");

                lblProvincia.Text = _idiomaService.GetTranslation("LabelProvincia");
                txtProvincia.Attributes["placeholder"] = _idiomaService.GetTranslation("PlaceholderProvincia");

                lblPais.Text = _idiomaService.GetTranslation("LabelPais");
                txtPais.Attributes["placeholder"] = _idiomaService.GetTranslation("PlaceholderPais");

                ErrorDireccion = _idiomaService.GetTranslation("ErrorDireccion");
                ErrorNumeroDireccion = _idiomaService.GetTranslation("ErrorNumeroDireccion");
                ErrorDepartamento = _idiomaService.GetTranslation("ErrorDepartamento");
                ErrorCodigoPostal = _idiomaService.GetTranslation("ErrorCodigoPostal");
                ErrorCiudad = _idiomaService.GetTranslation("ErrorCiudad");
                ErrorProvincia = _idiomaService.GetTranslation("ErrorProvincia");
                ErrorPais = _idiomaService.GetTranslation("ErrorPais");
            }
        }

        public string ErrorDireccion { get; private set; }
        public string ErrorNumeroDireccion { get; private set; }
        public string ErrorDepartamento { get; private set; }
        public string ErrorCodigoPostal { get; private set; }
        public string ErrorCiudad { get; private set; }
        public string ErrorProvincia { get; private set; }
        public string ErrorPais { get; private set; }

        public string Direccion
        {
            get { return txtDireccion.Text; }
            set { txtDireccion.Text = value; }
        }

        public int NumeroDireccion
        {
            get
            {
                int numero;
                int.TryParse(txtNumeroDireccion.Text, out numero);
                return numero;
            }
            set { txtNumeroDireccion.Text = value.ToString(); }
        }

        public string Departamento
        {
            get { return txtDepartamento.Text; }
            set { txtDepartamento.Text = value; }
        }

        public string CodigoPostal
        {
            get { return txtCodigoPostal.Text; }
            set { txtCodigoPostal.Text = value; }
        }

        public string Ciudad
        {
            get { return txtCiudad.Text; }
            set { txtCiudad.Text = value; }
        }

        public string Provincia
        {
            get { return txtProvincia.Text; }
            set { txtProvincia.Text = value; }
        }

        public string Pais
        {
            get { return txtPais.Text; }
            set { txtPais.Text = value; }
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

        public void Limpiar()
        {
            Direccion = string.Empty;
            NumeroDireccion = 0;
            Departamento = string.Empty;
            CodigoPostal = string.Empty;
            Ciudad = string.Empty;
            Provincia = string.Empty;
            Pais = string.Empty;
        }
    }
}
