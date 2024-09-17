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
        private readonly IBitacoraService _iBitacoraService;

        public BackUpService(IBitacoraService bitacoraService)
        {
            _backUpDAO = new BackUpDAO();
            _iBitacoraService = bitacoraService;
        }

        public string RealizarBackup(string ruta, string nombre, Usuario usuario)
        {
            try
            {
                if (string.IsNullOrEmpty(ruta) || string.IsNullOrEmpty(nombre))
                {
                    return "Hay campos sin completar";
                }
                else
                {
                    var resultado = _backUpDAO.RealizarBackup(ruta, nombre);
                    _iBitacoraService.AltaBitacora(usuario.Email, usuario.Puesto, "Se realizó una copia de seguridad", Criticidad.ALTA);
                    return resultado;
                }
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
                {
                    return "Debe seleccionar un archivo";
                }
                else
                {
                    var resultado = _backUpDAO.RealizarRestore(archivo);
                    _iBitacoraService.AltaBitacora(usuario.Email, usuario.Puesto, "Se realizó un restore.", Criticidad.ALTA);
                    return resultado;
                }
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}