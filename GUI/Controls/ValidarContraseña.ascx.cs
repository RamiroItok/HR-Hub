using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GUI.Controls
{
    public partial class ValidarContraseña : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            cvPassword.ErrorMessage = "";
        }

        protected void ValidatePassword(object source, ServerValidateEventArgs args)
        {
            string password = args.Value;

            var regex = new Regex(@"^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
            
            if (regex.IsMatch(password))
            {
                args.IsValid = true;
                cvPassword.ErrorMessage = "";
            }
            else
            {
                args.IsValid = false;
                cvPassword.ErrorMessage = "La contraseña debe tener al menos 8 caracteres, una mayúscula, un número y un carácter especial.";
            }
        }

        public string Password
        {
            get { return txtPassword.Text; }
            set { txtPassword.Text = value; }
        }
    }
}