using System;
using Aplication.Interfaces;
using Data.DAO;
using Models;

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
            catch (Exception)
            {
                throw new Exception("ErrorCopiaSeguridad");
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
                    TablaCorrupta.TablasRegistradasEnBitacora.Clear();
                    return resultado;
                }
            }
            catch (Exception ex) when (ex.Message.Contains("SQL") || ex.Message.Contains("BD"))
            {
                throw new Exception("ErrorBD");
            }
            catch (Exception ex)
            {
                throw new Exception("ErrorRestore");
            }
        }
    }
}