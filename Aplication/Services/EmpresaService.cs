using Aplication.Interfaces;
using Data.Interfaces;
using Models;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

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
                bool existeEmpresa = ObtenerListaDeEmpresas().Where(x => x.Nombre.ToUpper() == empresa.Nombre.ToUpper()).Any();

                if (existeEmpresa)
                    throw new Exception("EmpresaExistente");

                empresa.Nombre = EncriptacionService.Encriptar_AES(empresa.Nombre);
                
                var id = _empresaDAO.Registrar(empresa);
                _iBitacoraService.AltaBitacora(userSession.Email, userSession.Puesto, $"Se registra la empresa {EncriptacionService.Decrypt_AES(empresa.Nombre)}", Criticidad.BAJA);
                _iDigitoVerificadorService.CalcularDVTabla("Empresa");

                return id;
            }
            catch (Exception ex) when (ex.Message.Contains("SQL") || ex.Message.Contains("BD"))
            {
                throw new Exception("ErrorBD");
            }
            catch (Exception)
            {
                throw new Exception("ErrorRegistrarEmpresa");
            }
        }

        public int Modificar(Empresa empresa, Usuario userSession)
        {
            try
            {
                empresa.Nombre = EncriptacionService.Encriptar_AES(empresa.Nombre);

                var id = _empresaDAO.Modificar(empresa);
                _iBitacoraService.AltaBitacora(userSession.Email, userSession.Puesto, $"Se modifican los datos de la empresa {EncriptacionService.Decrypt_AES(empresa.Nombre)}", Criticidad.MEDIA);
                _iDigitoVerificadorService.CalcularDVTabla("Empresa");

                return id;
            }
            catch (Exception ex) when (ex.Message.Contains("SQL") || ex.Message.Contains("BD"))
            {
                throw new Exception("ErrorBD");
            }
            catch (Exception)
            {
                throw new Exception("ErrorModificarEmpresa");
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
                throw new Exception("ErrorBD");
            }
            catch (Exception)
            {
                throw new Exception("ErrorEliminarEmpresa");
            }
        }

        public DataTable ObtenerEmpresas()
        {
            try
            {
                var resultado = _empresaDAO.ObtenerEmpresas();
                var empresasTable = resultado.Tables[0];

                foreach (DataRow row in empresasTable.Rows)
                {
                    string nombreEncriptado = row["nombre"].ToString();
                    string nombreDesencriptado = EncriptacionService.Decrypt_AES(nombreEncriptado);

                    row["nombre"] = nombreDesencriptado;
                }

                return empresasTable;
            }
            catch (Exception ex) when (ex.Message.Contains("SQL") || ex.Message.Contains("BD"))
            {
                throw new Exception("ErrorBD");
            }
            catch (Exception)
            {
                throw new Exception("ErrorObtenerEmpresas");
            }
        }

        public Empresa ObtenerEmpresaPorId(int id)
        {
            try
            {
                var resultado = _empresaDAO.ObtenerEmpresaPorId(id);

                Empresa empresa = new Empresa()
                {
                    Id = (int)resultado.Tables[0].Rows[0]["Id"],
                    Nombre = EncriptacionService.Decrypt_AES(resultado.Tables[0].Rows[0]["Nombre"].ToString())
                };               

                return empresa;
            }
            catch (Exception ex) when (ex.Message.Contains("SQL") || ex.Message.Contains("BD"))
            {
                throw new Exception("ErrorBD");
            }
            catch (Exception)
            {
                throw new Exception("ErrorObtenerEmpresaPorId");
            }
        }

        private List<Empresa> ObtenerListaDeEmpresas()
        {
            var dataTable = ObtenerEmpresas();

            return ConvertirDataTableALista(dataTable);
        }

        private List<Empresa> ConvertirDataTableALista(DataTable dataTable)
        {
            var listaEmpresas = new List<Empresa>();

            foreach (DataRow row in dataTable.Rows)
            {
                listaEmpresas.Add(new Empresa
                {
                    Id = row.Table.Columns.Contains("id") ? Convert.ToInt32(row["id"]) : 0,
                    Nombre = row.Table.Columns.Contains("nombre") ? row["nombre"].ToString() : string.Empty,
                    Logo = row.Table.Columns.Contains("logo") && row["logo"] != DBNull.Value ? (byte[])row["logo"] : null,
                    URL = row.Table.Columns.Contains("url") ? row["url"].ToString() : string.Empty
                });
            }

            return listaEmpresas;
        }
    }
}