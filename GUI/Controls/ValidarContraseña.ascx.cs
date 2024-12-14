using Aplication.Interfaces.Observer;
using Aplication.Services.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unity;

namespace GUI.Controls
{
    public partial class ValidarContraseña : System.Web.UI.UserControl, IIdiomaService
    {
        private readonly IdiomaService _idiomaService;

        public ValidarContraseña()
        {
            _idiomaService = Global.Container.Resolve<IdiomaService>();
            _idiomaService.Subscribe(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CargarTextos();
            cvPassword.ErrorMessage = "";
        }

        protected void ValidatePassword(object source, ServerValidateEventArgs args)
        {
            string password = args.Value;
            
            var regex = new Regex(@"^(?=.*[A-Z])(?=.*\d)(?=.*[.@$!%*?&#\-_+)\(])[A-Za-z\d.@$!%*?&#\-_+)\(]{8,}$");
            
            if (regex.IsMatch(password))
            {
                args.IsValid = true;
                cvPassword.ErrorMessage = "";
            }
            else
            {
                args.IsValid = false;
                cvPassword.ErrorMessage = _idiomaService.GetTranslation("ErrorPassword");
            }
        }

        public bool IsValid
        {
            get
            {
                var args = new ServerValidateEventArgs(Password, true);
                ValidatePassword(this, args);
                return args.IsValid;
            }
        }

        public string Password
        {
            get { return txtPassword.Text; }
            set { txtPassword.Text = value; }
        }

        public void LimpiarPassword()
        {
            Password = string.Empty;
        }

        public void UpdateLanguage(string language)
        {
            CargarTextos();
        }

        private void CargarTextos()
        {
            if (!(lblPassword == null))
            {
                lblPassword.Text = _idiomaService.GetTranslation("LabelPassword");
                txtPassword.Attributes["placeholder"] = _idiomaService.GetTranslation("PlaceholderPassword");
                cvPassword.ErrorMessage = _idiomaService.GetTranslation("ErrorPassword");
            }
        }

        protected override void OnUnload(EventArgs e)
        {
            _idiomaService.Unsubscribe(this);
            base.OnUnload(e);
        }
    }
}