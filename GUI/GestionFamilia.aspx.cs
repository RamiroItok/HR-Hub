using Aplication.Interfaces;
using Models;
using Models.Composite;
using System;
using System.Linq;
using System.Web.UI.WebControls;
using Unity;

namespace GUI
{
    public partial class GestionFamilia : System.Web.UI.Page
    {
        private readonly IPermisoService _iPermiso;
        private readonly IBitacoraService _bitacoraService;
        public GestionFamilia()
        {
            _iPermiso = Global.Container.Resolve<IPermisoService>();
            _bitacoraService = Global.Container.Resolve<IBitacoraService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarFamilia();
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
                        throw new Exception("La familia ya existe");
                    }

                    Familia familia = new Familia()
                    {
                        Nombre = txtFamilia.Text,
                    };

                    _iPermiso.AltaFamiliaPatente(familia, true);

                    var usuario = Session["Usuario"] as Usuario;
                    _bitacoraService.AltaBitacora(usuario.Email, usuario.Puesto, "Da de alta una patente", Models.Enums.Criticidad.ALTA);

                    lblMessage.CssClass = "text-success";
                    lblMessage.Text = "Se ha dado de alta la familia correctamente.";
                    CargarFamilia();
                    Limpiar();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
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
    }
}