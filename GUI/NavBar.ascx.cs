using Aplication.Interfaces;
using Models;
using System;
using System.Web.UI;
using Unity;

namespace GUI.Controls
{
    public partial class NavBar : UserControl
    {
        private readonly IBitacoraService _bitacoraService;
        public NavBar()
        {
            _bitacoraService = Global.Container.Resolve<IBitacoraService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
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
                    falloIntegridadSeguridadLink.Visible = false;
                }

                VisualizarMenu(usuario);
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

        private void VisualizarMenu(Usuario usuario)
        {
            if (Session["ErrorVerificacionDV"] != null && usuario != null && usuario.Puesto == Models.Enums.Puesto.WebMaster)
            {
                falloIntegridadLink.Visible = true;
                seguridadLink.Visible = false;
                restoreLink.Visible = false;
                registroLink.Visible = false;
                loginLink.Visible = false;
                contactoLink.Visible = false;
                miCuentaLink.Visible = false;
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
                    }
                }
            }
        }
    }
}
