using Aplication.Interfaces;
using Aplication.Interfaces.Observer;
using Aplication.Services.Observer;
using Models;
using Models.Composite;
using Models.Enums;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unity;

namespace GUI
{
    public partial class Registro : Page, IIdiomaService
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IPermisoService _permisoService;
        private readonly IdiomaService _idiomaService;

        public Registro()
        {
            _usuarioService = Global.Container.Resolve<IUsuarioService>();
            _permisoService = Global.Container.Resolve<IPermisoService>();
            _idiomaService = Global.Container.Resolve<IdiomaService>();
            _idiomaService.Subscribe(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var usuario = Session["Usuario"] as Usuario;
            if(!_permisoService.TienePermiso(usuario, Permiso.RegistroUsuario))
            {
                Response.Redirect("AccesoDenegado.aspx");
                return;
            }

            try
            {
                if (!IsPostBack)
                {
                    CargarAreas();
                    string selectedLanguage = Session["SelectedLanguage"] as string ?? "es";
                    ddlLanguage.SelectedValue = selectedLanguage;
                    _idiomaService.CurrentLanguage = selectedLanguage;
                    CargarTextos();
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Visible = true;
                lblMensaje.CssClass = "text-danger";
                lblMensaje.Text = ex.Message;
            }
        }

        public string MensajeRegistroExitoTitulo { get; set; }
        public string MensajeRegistroExitoTexto { get; set; }
        public string MensajeRegistroExitoBoton { get; set; }

        private void CargarTextos()
        {
            if (!(litPageTitle == null))
            {
                litPageTitle.Text = _idiomaService.GetTranslation("PageTitleRegistro");
                litTituloRegistro.Text = _idiomaService.GetTranslation("RegistroTitulo");

                lblNombre.Text = _idiomaService.GetTranslation("LabelNombre");
                txtNombre.Attributes["placeholder"] = _idiomaService.GetTranslation("PlaceholderNombre");

                lblApellido.Text = _idiomaService.GetTranslation("LabelApellido");
                txtApellido.Attributes["placeholder"] = _idiomaService.GetTranslation("PlaceholderApellido");

                lblContraseña.Text = _idiomaService.GetTranslation("LabelContrasena");
                btnGenerarPassword.Text = _idiomaService.GetTranslation("ButtonGenerarPassword");

                lblGenero.Text = _idiomaService.GetTranslation("LabelGenero");
                litLabelArea.Text = _idiomaService.GetTranslation("LabelArea");

                drpGenero.Items.Clear();
                drpGenero.Items.Add(new ListItem(_idiomaService.GetTranslation("OptionGeneroDefault"), "0"));
                drpGenero.Items.Add(new ListItem(_idiomaService.GetTranslation("OptionGeneroMasculino"), "1"));
                drpGenero.Items.Add(new ListItem(_idiomaService.GetTranslation("OptionGeneroFemenino"), "2"));
                drpGenero.Items.Add(new ListItem(_idiomaService.GetTranslation("OptionGeneroNoEspecifica"), "3"));

                btnRegistrar.Text = _idiomaService.GetTranslation("ButtonRegistrar");
                btnCancelar.Text = _idiomaService.GetTranslation("ButtonCancelar");

                litLabelFechaNacimiento.Text = _idiomaService.GetTranslation("LabelFechaNacimiento");
                txtFechaNac.Attributes["placeholder"] = _idiomaService.GetTranslation("PlaceholderFechaNacimiento");

                MensajeRegistroExitoTitulo = _idiomaService.GetTranslation("RegistroExitoTitulo");
                MensajeRegistroExitoTexto = _idiomaService.GetTranslation("RegistroExitoTexto");
                MensajeRegistroExitoBoton = _idiomaService.GetTranslation("RegistroExitoBoton");
            }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            CargarTextos();
            try
            {
                if (CamposLlenos())
                {
                    bool registroExitoso = true;
                    Usuario usuario = new Usuario()
                    {
                        Nombre = txtNombre.Text,
                        Apellido = txtApellido.Text,
                        Email = ValidarEmail.Email,
                        Contraseña = hiddenContraseña.Value,
                        Area = (Area)Enum.Parse(typeof(Area), DropDownArea.Text),
                        FechaNacimiento = DateTime.Parse(txtFechaNac.Value),
                        Genero = drpGenero.SelectedItem.Text,
                        FechaIngreso = DateTime.Now,

                        Direccion = ValidarRegistroUsuarioDatosControl.Direccion,
                        NumeroDireccion = ValidarRegistroUsuarioDatosControl.NumeroDireccion,
                        Departamento = ValidarRegistroUsuarioDatosControl.Departamento,
                        CodigoPostal = ValidarRegistroUsuarioDatosControl.CodigoPostal,
                        Ciudad = ValidarRegistroUsuarioDatosControl.Ciudad,
                        Provincia = ValidarRegistroUsuarioDatosControl.Provincia,
                        Pais = ValidarRegistroUsuarioDatosControl.Pais
                    };

                    var esContraseñaValida = _usuarioService.ValidarFormatoContraseña(usuario.Contraseña);
                    if (esContraseñaValida)
                    {
                        var userSession = Session["Usuario"] as Usuario;
                        var id = _usuarioService.RegistrarUsuario(usuario, userSession);

                        var body = _usuarioService.ObtenerCuerpoCorreo(AsuntoMail.GeneracionContraseña);

                        body = body.Replace("{{CONTRASEÑA}}", usuario.Contraseña);

                        _usuarioService.EnviarMail(usuario.Email, AsuntoMail.GeneracionContraseña, body);
                        string script = $@"
                            <script>
                                Swal.fire({{
                                    title: '{MensajeRegistroExitoTitulo}',
                                    text: '{MensajeRegistroExitoTexto}',
                                    icon: 'success',
                                    confirmButtonText: '{MensajeRegistroExitoBoton}'
                                }});
                            </script>";

                        ClientScript.RegisterStartupScript(this.GetType(), "registroExitoso", script);
                        Limpiar();
                    }
                    else
                    {
                        throw new Exception(_idiomaService.GetTranslation("ErrorFormatoContraseña"));
                    }
                }
                else
                {
                    throw new Exception(_idiomaService.GetTranslation("ErrorCamposIncompletos"));
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Visible = true;
                lblMensaje.CssClass = "text-danger";
                lblMensaje.Text = ex.Message;
            }
        }

        private bool CamposLlenos()
        {
            if (string.IsNullOrWhiteSpace(txtApellido.Text) || string.IsNullOrWhiteSpace(hiddenContraseña.Value) || string.IsNullOrWhiteSpace(ValidarEmail.Email) || string.IsNullOrWhiteSpace(txtFechaNac.Value) || 
                string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(ValidarRegistroUsuarioDatosControl.Pais) || string.IsNullOrWhiteSpace(ValidarRegistroUsuarioDatosControl.Departamento) || 
                string.IsNullOrWhiteSpace(ValidarRegistroUsuarioDatosControl.Ciudad) || string.IsNullOrWhiteSpace(ValidarRegistroUsuarioDatosControl.CodigoPostal) || 
                string.IsNullOrWhiteSpace(ValidarRegistroUsuarioDatosControl.Direccion) || string.IsNullOrWhiteSpace(ValidarRegistroUsuarioDatosControl.NumeroDireccion.ToString()))
                return false;

            return true;
        }

        private void Limpiar()
        {
            txtNombre.Text = String.Empty;
            txtApellido.Text = String.Empty;
            ValidarEmail.Email = String.Empty;
            txtContraseña.Text = String.Empty;
            txtFechaNac.Value = String.Empty;
            DropDownArea.SelectedIndex = 0;
            drpGenero.SelectedIndex = 0;
            hiddenContraseña.Value = String.Empty;
            
            ValidarRegistroUsuarioDatosControl.Limpiar();
        }

        private void CargarAreas()
        {
            DropDownArea.DataSource = _usuarioService.ObtenerAreas();
            DropDownArea.DataTextField = "Area";
            DropDownArea.DataValueField = "Id";
            DropDownArea.DataBind();
            DropDownArea.Items.Insert(0, new ListItem(_idiomaService.GetTranslation("PlaceholderArea"), ""));
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        protected void btnGenerarPassword_Click(object sender, EventArgs e)
        {
            string nuevaContraseña = _usuarioService.GenerarContraseña();
            hiddenContraseña.Value = nuevaContraseña;
            txtContraseña.Text = new string('*', nuevaContraseña.Length);
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
            CargarTextos();
            Response.Redirect(Request.RawUrl);
        }
    }
}
