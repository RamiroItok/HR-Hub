using Aplication.Interfaces;
using Models;
using Models.Composite;
using System;
using System.IO;
using System.Linq;
using System.Web.UI;
using Unity;

namespace GUI
{
    public partial class AltaEmpresa : Page
    {
        private readonly IEmpresaService _empresaService;
        private readonly IPermisoService _permisoService;

        public AltaEmpresa()
        {
            _empresaService = Global.Container.Resolve<IEmpresaService>();
            _permisoService = Global.Container.Resolve<IPermisoService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var usuario = Session["Usuario"] as Usuario;
            if(!_permisoService.TienePermiso(usuario, Permiso.ConfiguracionEmpresas))
            {
                Response.Redirect("AccesoDenegado.aspx");
                return;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (fileLogo.HasFile || !string.IsNullOrEmpty(txtNombreEmpresa.Text) || !string.IsNullOrEmpty(txtURLEmpresa.Text))
                {
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
                    lblMensaje.Text = "Empresa registrada con éxito!";
                    lblMensaje.Visible = true;
                    Limpiar();
                }
                else
                {
                    lblMensaje.CssClass = "text-danger"; 
                    lblMensaje.Text = "Hay campos sin completar";
                    lblMensaje.Visible = true; 
                }
            }
            catch (Exception ex)
            {
                lblMensaje.CssClass = "text-danger";
                lblMensaje.Text = ex.Message;
                lblMensaje.Visible = true;
            }
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
    }
}