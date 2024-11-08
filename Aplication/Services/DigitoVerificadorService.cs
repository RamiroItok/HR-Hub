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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable ObtenerTabla(string tabla)
        {
            try
            {
                DataTable dt = _digitoVerificadorDAO.ObtenerTabla(tabla);
                return dt;
            }
            catch (Exception ex) when (ex.Message.Contains("SQL") || ex.Message.Contains("BD"))
            {
                throw new Exception("ErrorBD");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}