using Aplication.Interfaces;
using Models;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unity;

namespace GUI
{
    public partial class Restore : Page
    {
        private readonly IBackUpService _iBackUpService;
        private readonly IBitacoraService _iBitacoraService;

        public Restore()
        {
            _iBackUpService = Global.Container.Resolve<IBackUpService>();
            _iBitacoraService = Global.Container.Resolve<IBitacoraService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRestore_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario usuario = Session["Usuario"] as Usuario;
                string rutaArchivo = Path.GetFileName(fileBackup.FileName);

                string rutaDestino = "D:/Backups/" + rutaArchivo;
                fileBackup.SaveAs(rutaDestino);

                var resultado = _iBackUpService.RealizarRestore(rutaDestino, usuario);
                _iBitacoraService.AltaBitacora(usuario.Email, usuario.Puesto, "Se realizó un restore.", Criticidad.ALTA);

                lblMensaje.Text = resultado;
                lblMensaje.Visible = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {

        }
    }
}