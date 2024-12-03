using Aplication.Interfaces;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;

namespace Aplication.Services
{
    public class DigitoVerificadorService : IDigitoVerificadorService
    {
        private readonly IDigitoVerificadorDAO _digitoVerificadorDAO;

        public DigitoVerificadorService()
        {
            _digitoVerificadorDAO = new Data.DAO.DigitoVerificadorDAO();
        }

        public List<string> VerificarDV()
        {
            try
            {
                return _digitoVerificadorDAO.Verificar_DV();
            }
            catch (Exception ex) when (ex.Message.Contains("SQL") || ex.Message.Contains("BD"))
            {
                throw new Exception("ErrorBD");
            }
            catch (Exception)
            {
                throw new Exception("ErrorVerificarDigitosVerificadores");
            }
        }

        public bool RecalcularDV()
        {
            try
            {
                _digitoVerificadorDAO.Recalcular_DV();
                return true;
            }
            catch (Exception ex) when (ex.Message.Contains("SQL") || ex.Message.Contains("BD"))
            {
                throw new Exception("ErrorBD");
            }
            catch (Exception)
            {
                throw new Exception("ErrorRecalcularDigitosVerificadores");
            }
        }

        public bool CalcularDVTabla(string tabla)
        {
            try
            {
                _digitoVerificadorDAO.CalcularDVTabla(tabla);
                return true;
            }
            catch (Exception ex) when (ex.Message.Contains("SQL") || ex.Message.Contains("BD"))
            {
                throw new Exception("ErrorBD");
            }
            catch (Exception)
            {
                throw new Exception("ErrorCalcularDigitoVerificadorTabla");
            }
        }
    }
}