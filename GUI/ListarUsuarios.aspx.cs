using Aplication.Interfaces;
using System;
using Unity;

namespace GUI
{
    public partial class ListarUsuarios : System.Web.UI.Page
    {
        private readonly IUsuarioService _usuarioService;

        public ListarUsuarios()
        {
            _usuarioService = Global.Container.Resolve<IUsuarioService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarUsuarios();
            }
        }

        public void CargarUsuarios()
        {
            var usuarios = _usuarioService.ListarUsuarios();
            dataGridUsuarios.DataSource = usuarios;
            dataGridUsuarios.DataBind();
        }
    }
}