using Aplication.Interfaces;
using Aplication.Services;
using System;
using System.Web.Services;
using Unity;

namespace GUI.WebService
{
    /// <summary>
    /// Descripción breve de EnviarMail
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class EnviarMail : System.Web.Services.WebService
    {
        private readonly ICompraService _compraService;
        private readonly IUsuarioService _usuarioService;

        public EnviarMail()
        {
            _compraService = Global.Container.Resolve<ICompraService>();
            _usuarioService = Global.Container.Resolve<IUsuarioService>();
        }

        [WebMethod]
        public string EnviarResumenCompraPorEmail(int idCompra)
        {
            try
            {
                var compra = _compraService.ObtenerCompra(idCompra);
                if (compra == null) throw new Exception("Compra no encontrada.");

                var usuario = _usuarioService.ObtenerUsuarioPorId(compra.IdUsuario);
                if (usuario == null) throw new Exception("Usuario no encontrado.");

                var detallesCompra = _compraService.ObtenerDetalleCompra(idCompra);
                if (detallesCompra == null || detallesCompra.Count == 0)
                    throw new Exception("La compra no tiene productos.");

                string body = _compraService.CrearMensajeResumenCompra(compra, usuario, detallesCompra);
                GenerarPDFService generarPDFService = new GenerarPDFService();
                byte[] pdfBytes = generarPDFService.GenerarPdf(compra, usuario, detallesCompra);

                _usuarioService.EnviarMail(usuario.Email, Models.Enums.AsuntoMail.CompraProductos, body, pdfBytes);
                return "Correo enviado exitosamente a: " + usuario.Email;
            }
            catch (Exception ex)
            {
                return "Error al enviar el correo: " + ex.Message;
            }
        }
    }
}