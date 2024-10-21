using Aplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unity;

namespace GUI
{
    public partial class ListadoUsuarios : Page
    {
        private readonly IUsuarioService _usuarioService;
        private static List<Models.Usuario> listaUsuarios = new List<Models.Usuario>();

        public ListadoUsuarios()
        {
            _usuarioService = Global.Container.Resolve<IUsuarioService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                listaUsuarios = _usuarioService.ListarUsuarios();
                CargarUsuarioDefault();
            }
        }

        private void CargarUsuarioDefault()
        {
            CargarGrilla(listaUsuarios);
        }

        private void CargarGrilla(List<Models.Usuario> listadoBitacora)
        {
            dataGridUsuarios.DataSource = listadoBitacora;
            dataGridUsuarios.DataBind();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string searchTerm = txtBuscar.Text.Trim();

            if (string.IsNullOrEmpty(searchTerm))
            {
                lblMensaje.Text = "Por favor, ingrese un usuario en la búsqueda.";
                lblMensaje.Visible = true;
            }
            else
            {
                List<Models.Usuario> listaUsuariosFiltrados = new List<Models.Usuario>();

                listaUsuariosFiltrados = listaUsuarios
                                            .Where(x =>
                                                (string.IsNullOrEmpty(txtBuscar.Text) || x.Nombre.ToLower().Contains(txtBuscar.Text.ToLower()))
                                            )
                                            .ToList();

                Session["UsuariosFiltrados"] = listaUsuariosFiltrados;
                lblMensaje.Visible = false;
                CargarGrilla(listaUsuariosFiltrados);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session["UsuariosFiltrados"] = null;
            txtBuscar.Text = string.Empty;
            lblMensaje.Visible = false;
            CargarUsuarioDefault();
        }
    }
}