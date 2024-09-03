using Aplication.Interfaces;
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

        public BitacoraService(IBitacoraDAO bitacoraDAO)
        {
            _bitacoraDAO = bitacoraDAO;
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
                    Bitacora bitacora = new Bitacora()
                    {
                        Id = int.Parse(evento["Id"].ToString()),
                        Email = evento["Email"].ToString(),
                        TipoUsuario = evento["TipoUsuario"].ToString(),
                        Descripcion = evento["Descripcion"].ToString(),
                        Fecha = DateTime.Parse(evento["Fecha"].ToString()),
                        Criticidad = evento["Criticidad"].ToString(),
                        DVH = int.Parse(evento["DVH"].ToString())
                    };

                    listaEventos.Add(bitacora);
                }

                return listaEventos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable ListarEventoBetween(string fecha_ini, string fecha_fin)
        {
            throw new NotImplementedException();
        }

        public DataTable ListarEventoBetweenCritic(string fecha_ini, string fecha_fin, string critic)
        {
            throw new NotImplementedException();
        }

        public DataTable ListarEventoBetweenUsuario(string fecha_ini, string fecha_fin, string nombre_usuario)
        {
            throw new NotImplementedException();
        }

        public DataTable ListarEventoCrit(string critic)
        {
            throw new NotImplementedException();
        }

        public DataTable ListarEventoCritUsu(string nombre_usuario, string crit)
        {
            throw new NotImplementedException();
        }

        public DataTable ListarEventoFechaUsuCrit(string fecha_ini, string fecha_fin, string nombre_usuario, string crit)
        {
            throw new NotImplementedException();
        }

        public DataTable ListarEventoUsuario(string nombre_usuario)
        {
            throw new NotImplementedException();
        }
    }
}
