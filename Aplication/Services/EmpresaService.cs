using Aplication.Interfaces;
using Data.Interfaces;
using Models;
using Models.Enums;
using System;

namespace Aplication.Services
{
    public class EmpresaService : IEmpresaService
    {
        private readonly IEmpresaDAO _empresaDAO;
        private readonly IDigitoVerificadorService _iDigitoVerificadorService;
        private readonly IBitacoraService _iBitacoraService;

        public EmpresaService(IEmpresaDAO empresaDAO, IDigitoVerificadorService digitoVerificadorService, IBitacoraService bitacoraService)
        {
            _empresaDAO = empresaDAO;
            _iDigitoVerificadorService = digitoVerificadorService;
            _iBitacoraService = bitacoraService;
        }

        public int Registrar(Empresa empresa, Usuario userSession)
        {
            try
            {
                var id = _empresaDAO.Registrar(empresa);
                _iBitacoraService.AltaBitacora(userSession.Email, userSession.Puesto, $"Se registra la empresa {empresa.Nombre}", Criticidad.BAJA);
                _iDigitoVerificadorService.CalcularDVTabla("Empresa");

                return id;
            }
            catch (Exception ex) when (ex.Message.Contains("SQL") || ex.Message.Contains("BD"))
            {
                throw new Exception("Se ha perdido la conexión con la base de datos. Vuelva a intentar en unos minutos");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int Modificar(Empresa empresa, Usuario userSession)
        {
            try
            {
                var id = _empresaDAO.Modificar(empresa);
                _iBitacoraService.AltaBitacora(userSession.Email, userSession.Puesto, $"Se modifican los datos de la empresa {empresa.Nombre}", Criticidad.MEDIA);
                _iDigitoVerificadorService.CalcularDVTabla("Empresa");

                return id;
            }
            catch (Exception ex) when (ex.Message.Contains("SQL") || ex.Message.Contains("BD"))
            {
                throw new Exception("Se ha perdido la conexión con la base de datos. Vuelva a intentar en unos minutos");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Eliminar(Empresa empresa, Usuario userSession)
        {
            try
            {
                _empresaDAO.Eliminar(empresa.Id);
                _iBitacoraService.AltaBitacora(userSession.Email, userSession.Puesto, $"Se da de baja la empresa {empresa.Nombre}", Criticidad.MEDIA);
                _iDigitoVerificadorService.CalcularDVTabla("Empresa");
            }
            catch (Exception ex) when (ex.Message.Contains("SQL") || ex.Message.Contains("BD"))
            {
                throw new Exception("Se ha perdido la conexión con la base de datos. Vuelva a intentar en unos minutos");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}