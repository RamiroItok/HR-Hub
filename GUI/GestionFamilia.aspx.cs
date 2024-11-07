﻿using Aplication.Interfaces;
using Aplication.Interfaces.Observer;
using Aplication.Services.Observer;
using Models;
using Models.Composite;
using System;
using System.Linq;
using System.Web.UI.WebControls;
using Unity;

namespace GUI
{
    public partial class GestionFamilia : System.Web.UI.Page, IIdiomaService
    {
        private readonly IPermisoService _iPermiso;
        private readonly IBitacoraService _bitacoraService;
        private readonly IPermisoService _permisoService;
        private readonly IdiomaService _idiomaService;

        public GestionFamilia()
        {
            _iPermiso = Global.Container.Resolve<IPermisoService>();
            _bitacoraService = Global.Container.Resolve<IBitacoraService>();
            _permisoService = Global.Container.Resolve<IPermisoService>();
            _idiomaService = Global.Container.Resolve<IdiomaService>();
            _idiomaService.Subscribe(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var usuario = Session["Usuario"] as Usuario;
            if(!_permisoService.TienePermiso(usuario, Permiso.GestionFamilia))
            {
                Response.Redirect("AccesoDenegado.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CargarFamilia();
                string selectedLanguage = Session["SelectedLanguage"] as string ?? "es";
                ddlLanguage.SelectedValue = selectedLanguage;
                _idiomaService.CurrentLanguage = selectedLanguage;
                CargarTextos();
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtFamilia.Text))
                {
                    bool existeFamilia = _iPermiso.ObtenerFamilias().Where(x => x.Nombre.ToUpper() == txtFamilia.Text.ToUpper()).Any();
                    if (existeFamilia == true)
                    {
                        Limpiar();
                        throw new Exception(_idiomaService.GetTranslation("FamiliaYaExiste"));
                    }

                    Familia familia = new Familia()
                    {
                        Nombre = txtFamilia.Text,
                    };

                    _iPermiso.AltaFamiliaPatente(familia, true);

                    var usuario = Session["Usuario"] as Usuario;
                    _bitacoraService.AltaBitacora(usuario.Email, usuario.Puesto, "Da de alta una patente", Models.Enums.Criticidad.ALTA);

                    lblMessage.CssClass = "text-success";
                    lblMessage.Text = _idiomaService.GetTranslation("FamiliaAltaExitosa");
                    CargarFamilia();
                    Limpiar();
                }
                else
                {
                    throw new Exception(_idiomaService.GetTranslation("MensajeCamposIncompletos"));
                }
            }
            catch (Exception ex)
            {
                lblMessage.CssClass = "text-danger";
                lblMessage.Text = ex.Message;
            }
            finally
            {
                lblMessage.Visible = true;
            }
        }

        private void CargarTextos()
        {
            if (!(litTituloPagina == null))
            {
                litTituloPagina.Text = _idiomaService.GetTranslation("GestionFamiliaTituloPagina");

                lblNombreFamilia.Text = _idiomaService.GetTranslation("NombreFamilia");
                lblListadoFamilias.Text = _idiomaService.GetTranslation("ListadoFamilias");
                btnAceptar.Text = _idiomaService.GetTranslation("ButtonAlta");
                btnCancelar.Text = _idiomaService.GetTranslation("ButtonCancelar");

                lblMessage.Text = _idiomaService.GetTranslation("FamiliaAltaExitosa");

                gridFamilia.Columns[0].HeaderText = _idiomaService.GetTranslation("ColumnNombre");
                gridFamilia.Columns[1].HeaderText = _idiomaService.GetTranslation("ColumnHijos");
                gridFamilia.Columns[2].HeaderText = _idiomaService.GetTranslation("ColumnPermiso");

                gridFamilia.DataBind();
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void CargarFamilia()
        {
            var familias = _iPermiso.ObtenerFamilias();

            gridFamilia.DataSource = familias;
            gridFamilia.DataBind();

            foreach (DataControlField column in gridFamilia.Columns)
            {
                if (column.HeaderText == "Hijos" || column.HeaderText == "Permiso")
                {
                    column.Visible = false;
                }
            }

            gridFamilia.AutoGenerateSelectButton = false;
            gridFamilia.AllowPaging = false;
            gridFamilia.RowStyle.Wrap = false;
            gridFamilia.Attributes["tabindex"] = "-1";
            gridFamilia.Enabled = false;
        }

        private void Limpiar()
        {
            txtFamilia.Text = String.Empty;
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