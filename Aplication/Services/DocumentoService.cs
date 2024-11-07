﻿using Aplication.Interfaces;
using Data.Interfaces;
using Models;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Services
{
    public class DocumentoService : IDocumentoService
    {
        private readonly IDocumentoDAO _documentoDAO;
        private readonly IDigitoVerificadorService _iDigitoVerificadorService;
        private readonly IBitacoraService _iBitacoraService;
        private readonly IUsuarioService _usuarioService;

        public DocumentoService(IDocumentoDAO documentoDAO, IDigitoVerificadorService digitoVerificadorService, IBitacoraService bitacoraService, IUsuarioService usuarioService)
        {
            _documentoDAO = documentoDAO;
            _iDigitoVerificadorService = digitoVerificadorService;
            _iBitacoraService = bitacoraService;
            _usuarioService = usuarioService;
        }

        public void AsignarDocumento(int idDocumento)
        {
            try
            {
                List<Usuario> lista = _usuarioService.ObtenerEmpleados();
                foreach(Usuario usuario in lista)
                {
                    _documentoDAO.AsignarDocumento(idDocumento, usuario.Id);
                }

                _iDigitoVerificadorService.CalcularDVTabla("UsuarioDocumento");
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

        public int CargarDocumento(Documento documento, Usuario userSession)
        {
            try
            {
                var id = _documentoDAO.CargarDocumento(documento);
                _iBitacoraService.AltaBitacora(userSession.Email, userSession.Puesto, $"Cargo el archivo {documento.Nombre}", Criticidad.BAJA);
                _iDigitoVerificadorService.CalcularDVTabla("Documentos");
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

        public void FirmarDocumento(int idDocumento, Usuario userSession)
        {
            try
            {

                _documentoDAO.FirmarDocumento(idDocumento, userSession.Id);
                _iBitacoraService.AltaBitacora(userSession.Email, userSession.Puesto, "Firmo un documento", Criticidad.BAJA);
                _iDigitoVerificadorService.CalcularDVTabla("UsuarioDocumento");
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

        public byte[] ObtenerContenidoPorId(int idDocumento)
        {
            try
            {
                var resultado = _documentoDAO.ObtenerContenidoPorId(idDocumento);
                return (byte[])resultado.Tables[0].Rows[0]["Contenido"];
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

        public List<UsuarioDocumento> ObtenerDocumentosPorUsuario(bool firmado, Usuario userSession)
        {
            try
            {
                var resultado = _documentoDAO.ObtenerDocumentosPorUsuario(firmado, userSession.Id);

                List<UsuarioDocumento> listadoDocumentos = new List<UsuarioDocumento>();

                foreach (DataRow row in resultado.Tables[0].Rows)
                {
                    UsuarioDocumento usuarioDocumento = new UsuarioDocumento()
                    {
                        Documento = new Documento()
                        {
                            Id = Convert.ToInt32(row["IdDocumento"]),
                            Nombre = row["Nombre"].ToString()
                        },
                        Usuario = new Usuario()
                        {
                            Id = userSession.Id
                        },
                        FechaEntrega = (DateTime)row["FechaEntrega"],
                        FechaFirma = row["FechaFirma"] == DBNull.Value ? (DateTime?)null : (DateTime)row["FechaFirma"],
                        Firmado = (EstadoFirma)Convert.ToInt32(row["Firmado"])
                    };

                    listadoDocumentos.Add(usuarioDocumento);
                }

                return listadoDocumentos;
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