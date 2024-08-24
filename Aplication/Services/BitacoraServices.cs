using Aplication.Interfaces;
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
    public class BitacoraServices : IBitacoraServices
    {
        private readonly IBitacoraDAO _bitacoraDAO;

        public BitacoraServices(IBitacoraDAO bitacoraDAO)
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

        public List<Bitacora> ListarEventos()
        {
            throw new NotImplementedException();
        }

        public DataTable ListarEventoUsuario(string nombre_usuario)
        {
            throw new NotImplementedException();
        }
    }
}
