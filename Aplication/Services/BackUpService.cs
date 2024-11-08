using System;
using System.Configuration;
using Aplication.Interfaces;
using Data.DAO;
using Models;
using Models.Enums;

namespace Aplication.Services
{
    public class BackUpService : IBackUpService
    {
        private readonly BackUpDAO _backUpDAO;

        public BackUpService()
        {
            _backUpDAO = new BackUpDAO();
        }

        public string RealizarBackup(string ruta, string nombre, Usuario usuario)
        {
            try
            {
                if (string.IsNullOrEmpty(ruta) || string.IsNullOrEmpty(nombre))
                    throw new Exception("MensajeCamposIncompletos");
                else
                {
                    var resultado = _backUpDAO.RealizarBackup(ruta, nombre);
                    return resultado;
                }
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

        public string RealizarRestore(string archivo, Usuario usuario)
        {
            try
            {
                if (string.IsNullOrEmpty(archivo))
                    throw new Exception("MensajeArchivoNoSeleccionado");
                else
                {
                    var resultado = _backUpDAO.RealizarRestore(archivo);
                    return resultado;
                }
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

        public bool CrearBaseDeDatos()
        {
            try
            {
                string server = ConfigurationManager.AppSettings["server"];
                string nombreBase = ConfigurationManager.AppSettings["base"];

                bool resultado = _backUpDAO.CrearBaseDeDatos(server, nombreBase);
                return resultado;
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