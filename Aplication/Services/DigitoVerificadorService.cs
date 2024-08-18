using Aplication.Interfaces;
using Data.Interfaces;
using System;
using System.Data;

namespace Aplication.Services
{
    public class DigitoVerificadorService : IDigitoVerificadorService
    {
        private readonly IDigitoVerificadorDAO _digitoVerificadorDAO;

        public DigitoVerificadorService()
        {
            _digitoVerificadorDAO = new Data.DAO.DigitoVerificadorDAO();;
        }

        public string VerificarDV()
        {
            try
            {
                return _digitoVerificadorDAO.Verificar_DV();
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
            catch
            {
                throw new Exception("Error al listar usuarios");
            }
        }
    }
}