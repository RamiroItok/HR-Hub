﻿using Aplication.Interfaces;
using Data.Interfaces;
using Models;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Data;

namespace Aplication.Services
{
    public class BitacoraService : IBitacoraService
    {
        private readonly IBitacoraDAO _bitacoraDAO;
        private readonly IDigitoVerificadorService _digitoVerificadorService;

        public BitacoraService(IBitacoraDAO bitacoraDAO, IDigitoVerificadorService digitoVerificadorService)
        {
            _bitacoraDAO = bitacoraDAO;
            _digitoVerificadorService = digitoVerificadorService;
        }
        public int AltaBitacora(string email, Puesto tipoUsuario, string descripcion, Criticidad criticidad)
        {
            try
            {
                Bitacora bitacora = new Bitacora()
                {
                    Email = email,
                    TipoUsuario = tipoUsuario.ToString(),
                    Descripcion = descripcion,
                    Fecha = DateTime.Now,
                    Criticidad = criticidad.ToString()
                };
                var resultado = _bitacoraDAO.RegistrarBitacora(bitacora);
                _digitoVerificadorService.CalcularDVTabla("Bitacora");
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int BajaBitacora(string fechaIni, string fechaFin)
        {
            throw new NotImplementedException();
        }

        public List<Bitacora> ListarEventos()
        {
            try
            {
                List<Bitacora> listaEventos = new List<Bitacora>();
                var resultado = _bitacoraDAO.ListarEventos();
                foreach (DataRow evento in resultado.Tables[0].Rows)
                {
                    var bitacora = CompletarBitacora(evento);

                    listaEventos.Add(bitacora);
                }

                return listaEventos;
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

        private Bitacora CompletarBitacora(DataRow evento)
        {
            Bitacora bitacora = new Bitacora()
            {
                Id = int.Parse(evento["Id"].ToString()),
                Email = EncriptacionService.Decrypt_AES(evento["Email"].ToString()),
                TipoUsuario = evento["TipoUsuario"].ToString(),
                Descripcion = evento["Descripcion"].ToString(),
                Fecha = DateTime.Parse(evento["Fecha"].ToString()),
                Criticidad = evento["Criticidad"].ToString(),
                DVH = int.Parse(evento["DVH"].ToString())
            };

            return bitacora;
        }
    }
}
