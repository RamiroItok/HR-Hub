using Aplication.Interfaces.Observer;
using Aplication.Services.Observer;
using System;
using System.Text.RegularExpressions;
using System.Web.UI;
using Unity;

namespace GUI.Controls
{
    public partial class ValidarEmail : UserControl, IIdiomaService
    {
        private readonly IdiomaService _idiomaService;

        public ValidarEmail()
        {
            _idiomaService = Global.Container.Resolve<IdiomaService>();
            _idiomaService.Subscribe(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CargarTextos();
        }
        public string Email
        {
            get { return txtEmail.Text; }
            set { txtEmail.Text = value; }
        }

        public bool EsEmailValido()
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(Email, pattern);
        }

        public void UpdateLanguage(string language)
        {
            CargarTextos();
        }

        private void CargarTextos()
        {
            if (!(lblEmail == null))
            {
                lblEmail.Text = _idiomaService.GetTranslation("LabelEmail");
                lblEmailError.Text = _idiomaService.GetTranslation("ErrorFormatoEmail");
                txtEmail.Attributes["placeholder"] = _idiomaService.GetTranslation("PlaceholderEmail");
            }
        }

        protected override void OnUnload(EventArgs e)
        {
            _idiomaService.Unsubscribe(this);
            base.OnUnload(e);
        }
    }
}
