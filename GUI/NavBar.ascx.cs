using Aplication.Interfaces;
using Aplication.Interfaces.Observer;
using Aplication.Services.Observer;
using Models;
using System;
using System.Web.UI;
using Unity;

namespace GUI.Controls
{
    public partial class NavBar : UserControl, IIdiomaService
    {
        private readonly IBitacoraService _bitacoraService;
        private readonly IdiomaService _idiomaService;
        public NavBar()
        {
            _bitacoraService = Global.Container.Resolve<IBitacoraService>();
            _idiomaService = Global.Container.Resolve<IdiomaService>();
            _idiomaService.Subscribe(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                VisualizarMenu();
                CargarTextos();
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            var usuario = Session["Usuario"] as Usuario;

            _bitacoraService.AltaBitacora(usuario.Email, usuario.Puesto, "Cierra sesion", Models.Enums.Criticidad.BAJA);

            Session.Abandon();
            Response.Redirect("Home.aspx");
        }

        protected void btnInicio_Click(object sender, EventArgs e)
        {
            var usuario = Session["Usuario"] as Usuario;

            if ((usuario != null && Session["ErrorVerificacionDV"] == null) || (Session["ErrorVerificacionDV"] != null && usuario.Puesto == Models.Enums.Puesto.WebMaster))
            {
                Response.Redirect("MenuPrincipal.aspx");
            }
            else
            {
                Session.Abandon();
                Response.Redirect("Home.aspx");
            }
        }

        private void VisualizarMenu()
        {
            var usuario = Session["Usuario"] as Usuario;
            if (usuario == null)
            {
                loginLink.Visible = true;
                logoutLink.Visible = false;
                seguridadLink.Visible = false;
                registroLink.Visible = false;
                bitacoraLink.Visible = false;
                miCuentaLink.Visible = false;
                falloIntegridadLink.Visible = false;
                configuracionEmpresaLink.Visible = false;
                configuracionProductosLink.Visible = false;
                productosLink.Visible = false;
                carritoLink.Visible = false;
                misComprasLink.Visible = false;
                reportesLink.Visible = false;
                reporteComprasLink.Visible = false;
                desbloquearUsuariosLink.Visible = false;
                documentosLink.Visible = false;
                cargaDocumentosLink.Visible = false;
                misDocumentosLink.Visible = false;
            }

            if (Session["ErrorVerificacionDV"] != null && usuario != null && usuario.Puesto == Models.Enums.Puesto.WebMaster)
            {
                falloIntegridadLink.Visible = true;
                bitacoraLink.Visible = true;
                seguridadLink.Visible = false;
                restoreLink.Visible = false;
                registroLink.Visible = false;
                loginLink.Visible = false;
                contactoLink.Visible = false;
                miCuentaLink.Visible = false;
                configuracionEmpresaLink.Visible = false;
                productosLink.Visible = false;
                carritoLink.Visible = false;
                misComprasLink.Visible = false;
                reportesLink.Visible = false;
                reporteComprasLink.Visible = false;
                desbloquearUsuariosLink.Visible = false;
                documentosLink.Visible = false;
                cargaDocumentosLink.Visible = false;
                misDocumentosLink.Visible = false;
                return;
            }
            else if (Session["ErrorVerificacionDV"] != null && usuario != null && usuario.Puesto != Models.Enums.Puesto.WebMaster)
            {
                seguridadLink.Visible = false;
                registroLink.Visible = false;
                loginLink.Visible = false;
                contactoLink.Visible = false;
                miCuentaLink.Visible = false;
                falloIntegridadLink.Visible = false;
                configuracionEmpresaLink.Visible = false;
                productosLink.Visible = false;
                carritoLink.Visible = false;
                misComprasLink.Visible = false;
                reportesLink.Visible = false;
                reporteComprasLink.Visible = false;
                desbloquearUsuariosLink.Visible = false;
                documentosLink.Visible = false;
                cargaDocumentosLink.Visible = false;
                misDocumentosLink.Visible = false;
                return;
            }
            
            if (usuario != null)
            {
                loginLink.Visible = false;
                foreach (var permiso in usuario.Permisos)
                {
                    switch (permiso.Permiso)
                    {
                        case Models.Composite.Permiso.Gestion_Usuarios:
                            registroLink.Visible = true;
                            break;
                        case Models.Composite.Permiso.ListarUsuarios:
                            listarUsuariosLink.Visible = true;
                            break;
                        case Models.Composite.Permiso.RegistroUsuario:
                            registroUsuarioLink.Visible = true;
                            break;
                        case Models.Composite.Permiso.Bitacora:
                            bitacoraLink.Visible = true;
                            break;
                        case Models.Composite.Permiso.Seguridad:
                            seguridadLink.Visible = true;
                            break;
                        case Models.Composite.Permiso.BackUp:
                            backUpLink.Visible = true;
                            break;
                        case Models.Composite.Permiso.Restore:
                            restoreLink.Visible = true;
                            break;
                        case Models.Composite.Permiso.GestionFamilia:
                            gestionFamiliaLink.Visible = true;
                            break;
                        case Models.Composite.Permiso.GestionFamiliaPatente:
                            gestionFamiliaPatenteLink.Visible = true;
                            break;
                        case Models.Composite.Permiso.FalloIntegridad:
                            falloIntegridadSeguridadLink.Visible = true;
                            break;
                        case Models.Composite.Permiso.PermisoUsuario:
                            permisosUsuarioLink.Visible = true;
                            break;
                        case Models.Composite.Permiso.ConfiguracionEmpresas:
                            configuracionEmpresaLink.Visible = true;
                            break;
                        case Models.Composite.Permiso.ConfiguracionProducto:
                            configuracionProductosLink.Visible = true;
                            break;
                        case Models.Composite.Permiso.Productos:
                            productosLink.Visible = true;
                            break;
                        case Models.Composite.Permiso.Carrito:
                            carritoLink.Visible = true;
                            break;
                        case Models.Composite.Permiso.GestionPermisosUsuario:
                            gestionPermisosUsuarioLink.Visible = true;
                            break;
                        case Models.Composite.Permiso.MisCompras:
                            misComprasLink.Visible = true;
                            break;
                        case Models.Composite.Permiso.MiCuenta:
                            miCuentaLink.Visible = true;
                            break;
                        case Models.Composite.Permiso.Reportes:
                            reportesLink.Visible = true;
                            break;
                        case Models.Composite.Permiso.ReporteCompras:
                            reporteComprasLink.Visible = true;
                            break;
                        case Models.Composite.Permiso.DesbloquearUsuario:
                            desbloquearUsuariosLink.Visible = true;
                            break;
                        case Models.Composite.Permiso.Documentos:
                            documentosLink.Visible = true;
                            break;
                        case Models.Composite.Permiso.CargaDocumentos:
                            cargaDocumentosLink.Visible = true;
                            break;
                        case Models.Composite.Permiso.MisDocumentos:
                            misDocumentosLink.Visible = true;
                            break;
                    }
                }
            }
        }
        private void CargarTextos()
        {
            if (!(litHRHub == null))
            {
                litHRHub.Text = _idiomaService.GetTranslation("HRHub");
                litRegistro.Text = _idiomaService.GetTranslation("Registro");
                litListarUsuarios.Text = _idiomaService.GetTranslation("ListarUsuarios");
                litRegistrarUsuario.Text = _idiomaService.GetTranslation("RegistrarUsuario");
                litSeguridad.Text = _idiomaService.GetTranslation("Seguridad");
                litGestionFamilia.Text = _idiomaService.GetTranslation("GestionFamilia");
                litGestionFamiliaPatente.Text = _idiomaService.GetTranslation("GestionFamiliaPatente");
                litGestionPermisosUsuario.Text = _idiomaService.GetTranslation("GestionPermisosUsuario");
                litPermisosUsuario.Text = _idiomaService.GetTranslation("PermisosUsuario");
                litBackup.Text = _idiomaService.GetTranslation("Backup");
                litRestore.Text = _idiomaService.GetTranslation("Restore");
                litFalloIntegridad1.Text = _idiomaService.GetTranslation("FalloIntegridad");
                litFalloIntegridad.Text = _idiomaService.GetTranslation("FalloIntegridad");
                litConfigEmpresa.Text = _idiomaService.GetTranslation("ConfiguracionEmpresas");
                litAltaEmpresa.Text = _idiomaService.GetTranslation("AltaEmpresa");
                litListadoEmpresas.Text = _idiomaService.GetTranslation("ListadoEmpresas");
                litConfigProductos.Text = _idiomaService.GetTranslation("ConfiguracionProductos");
                litAltaProducto.Text = _idiomaService.GetTranslation("AltaProducto");
                litListadoProductos.Text = _idiomaService.GetTranslation("ListadoProductos");
                litReportes.Text = _idiomaService.GetTranslation("Reportes");
                litReporteCompras.Text = _idiomaService.GetTranslation("ReporteCompras");
                litBitacora.Text = _idiomaService.GetTranslation("Bitacora");
                litProductos.Text = _idiomaService.GetTranslation("Productos");
                litCarrito.Text = _idiomaService.GetTranslation("Carrito");
                litContacto.Text = _idiomaService.GetTranslation("Contacto");
                litLogin.Text = _idiomaService.GetTranslation("Login");
                litLogout.Text = _idiomaService.GetTranslation("CerrarSesion");
                litMiCuenta.Text = _idiomaService.GetTranslation("MiCuenta");
                litMisCompras.Text = _idiomaService.GetTranslation("MisCompras");
                litCambiarContraseña.Text = _idiomaService.GetTranslation("CambiarContrasena");
                litDesbloquearUsuarios.Text = _idiomaService.GetTranslation("DesbloquearUsuarios");
                litMisDocumentos.Text = _idiomaService.GetTranslation("MisDocumentos");
            }
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
