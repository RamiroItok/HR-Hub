﻿using Aplication.Interfaces;
using Models;
using Models.Composite;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unity;

namespace GUI
{
    public partial class Backup : Page
    {
        private readonly IBackUpService _iBackupService;
        private readonly IBitacoraService _iBitacoraService;
        private readonly IPermisoService _permisoService;

        public Backup()
        {
            _iBackupService = Global.Container.Resolve<IBackUpService>();
            _iBitacoraService = Global.Container.Resolve<IBitacoraService>();
            _permisoService = Global.Container.Resolve<IPermisoService>();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            var usuario = Session["Usuario"] as Usuario;
            if(!_permisoService.TienePermiso(usuario, Permiso.BackUp))
            {
                Response.Redirect("AccesoDenegado.aspx");
                return;
            }
        }

        protected void btnBackup_Click(object sender, EventArgs e)
        {
            Usuario usuario = Session["Usuario"] as Usuario;
            var ruta = txtRuta.Text;
            var nombre = txtNombre.Text;

            try
            {
                var resultado = _iBackupService.RealizarBackup(ruta, nombre, usuario);
                _iBitacoraService.AltaBitacora(usuario.Email, usuario.Puesto, "Se realizó una copia de seguridad", Criticidad.ALTA);

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
            txtNombre.Text = string.Empty;
            txtRuta.Text = string.Empty;
        }
    }
}