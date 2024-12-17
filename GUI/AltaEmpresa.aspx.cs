using Aplication.Interfaces;
using Aplication.Interfaces.Observer;
using Aplication.Services.Observer;
using Models;
using Models.Composite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using Unity;

namespace GUI
{
    public partial class AltaEmpresa : Page, IIdiomaService
    {
        private readonly IEmpresaService _empresaService;
        private readonly IPermisoService _permisoService;
        private readonly IdiomaService _idiomaService;

        public AltaEmpresa()
        {
            _empresaService = Global.Container.Resolve<IEmpresaService>();
            _permisoService = Global.Container.Resolve<IPermisoService>();
            _idiomaService = Global.Container.Resolve<IdiomaService>();
            _idiomaService.Subscribe(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var usuario = Session["Usuario"] as Usuario;
            if(!_permisoService.TienePermiso(usuario, Permiso.ConfiguracionEmpresas))
            {
                Response.Redirect("AccesoDenegado.aspx");
                Context.ApplicationInstance.CompleteRequest();
                return;
            }
            if (!IsPostBack)
            {
                string selectedLanguage = Session["SelectedLanguage"] as string ?? "es";
                ddlLanguage.SelectedValue = selectedLanguage;
                _idiomaService.CurrentLanguage = selectedLanguage;
                CargarTextos();
            }
        }

        private void CargarTextos()
        {
            if (!(tituloRegistro == null))
            {
                tituloRegistro.InnerText = _idiomaService.GetTranslation("TituloRegistroEmpresas");
                Page.Title = _idiomaService.GetTranslation("PageTitleAltaEmpresa");
                lblNombreEmpresa.InnerText = _idiomaService.GetTranslation("LabelNombreEmpresa");
                txtNombreEmpresa.Attributes["placeholder"] = _idiomaService.GetTranslation("PlaceholderNombreEmpresa");
                lblLogoEmpresa.InnerText = _idiomaService.GetTranslation("LabelLogoEmpresa");
                lblURLEmpresa.InnerText = _idiomaService.GetTranslation("LabelURLEmpresa");
                txtURLEmpresa.Attributes["placeholder"] = _idiomaService.GetTranslation("PlaceholderURLEmpresa");
                btnSubmit.Text = _idiomaService.GetTranslation("ButtonRegistrar");
                btnCancel.Text = _idiomaService.GetTranslation("ButtonCancelar");
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (fileLogo.HasFile || !string.IsNullOrEmpty(txtNombreEmpresa.Text) || !string.IsNullOrEmpty(txtURLEmpresa.Text))
                {
                    if (!ValidarExtensionImagen(fileLogo.PostedFile))
                        throw new Exception("ExtensionImagen");

                    byte[] imagenBytes;
                    using (BinaryReader br = new BinaryReader(fileLogo.PostedFile.InputStream))
                    {
                        imagenBytes = br.ReadBytes(fileLogo.PostedFile.ContentLength);
                    }

                    Empresa empresa = new Empresa()
                    {
                        Nombre = txtNombreEmpresa.Text,
                        Logo = imagenBytes,
                        URL = txtURLEmpresa.Text
                    };

                    var userSession = Session["Usuario"] as Usuario;
                    var id = _empresaService.Registrar(empresa, userSession);

                    lblMensaje.CssClass = "text-success";
                    lblMensaje.Text = _idiomaService.GetTranslation("MensajeRegistroExitoso");
                    lblMensaje.Visible = true;
                    Limpiar();
                }
                else
                {
                    lblMensaje.CssClass = "text-danger";
                    lblMensaje.Text = _idiomaService.GetTranslation("MensajeCamposIncompletos");
                    lblMensaje.Visible = true; 
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = _idiomaService.GetTranslation("MensajeErrorGeneral") + ": " + _idiomaService.GetTranslation(ex.Message);
                lblMensaje.CssClass = "text-danger";
                lblMensaje.Visible = true;
            }
        }

        private bool ValidarExtensionImagen(HttpPostedFile file)
        {
            string extension = Path.GetExtension(file.FileName).ToLower();

            var extensionesPermitidas = new List<string> { ".png", ".jpg", ".jpeg" };

            return extensionesPermitidas.Contains(extension);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void Limpiar()
        {
            txtNombreEmpresa.Text = string.Empty;
            txtURLEmpresa.Text = string.Empty;
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