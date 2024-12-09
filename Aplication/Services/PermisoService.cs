using Aplication.Interfaces;
using Models;
using Models.Composite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Aplication.Services
{
    public class PermisoService : IPermisoService
    {
        private readonly Data.Composite.PermisoDAO _permisoDAO;
        private readonly IDigitoVerificadorService _digitoVerificadorService;
        private readonly IBitacoraService _bitacoraService;

        public PermisoService(IDigitoVerificadorService digitoVerificadorService, IBitacoraService bitacoraService)
        { 
            _permisoDAO = new Data.Composite.PermisoDAO();
            _digitoVerificadorService = digitoVerificadorService;
            _bitacoraService = bitacoraService;
        }

        #region Metodos
        public void AsignarPermisoAFamilia(int padreId, int hijoId, Usuario usuario)
        {
            try
            {
                _permisoDAO.AsignarPermisoAFamilia(padreId, hijoId);
                _bitacoraService.AltaBitacora(usuario.Email, usuario.Puesto, "Asigna patente a familia", Models.Enums.Criticidad.ALTA);
                _digitoVerificadorService.CalcularDVTabla("FamiliaPatente");
            }
            catch (Exception ex) when (ex.Message.Contains("SQL") || ex.Message.Contains("BD"))
            {
                throw new Exception("ErrorBD");
            }
            catch (Exception)
            {
                throw new Exception("ErrorAsignarPermisoFamilia");
            }
        }

        public void AsignarPermisoAUsuario(int idUsuario, int idPatente, Usuario userSession)
        {
            try
            {
                _permisoDAO.AsignarPermisoAUsuario(idUsuario, idPatente);
                _bitacoraService.AltaBitacora(userSession.Email, userSession.Puesto, "Asigna patente a usuario", Models.Enums.Criticidad.ALTA);
                _digitoVerificadorService.CalcularDVTabla("UsuarioPermiso");
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

        public void QuitarPermisoAFamilia(int padreId, int hijoId, Usuario userSession)
        {
            try
            {
                _permisoDAO.QuitarPermisoAFamilia(padreId, hijoId);
                _bitacoraService.AltaBitacora(userSession.Email, userSession.Puesto, "Quita patente a familia", Models.Enums.Criticidad.ALTA);
                _digitoVerificadorService.CalcularDVTabla("FamiliaPatente");
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

        public void QuitarPermisoAUsuario(int idUsuario, int idPatente, Usuario userSession)
        {
            try
            {
                _permisoDAO.QuitarPermisoAUsuario(idUsuario, idPatente);
                _bitacoraService.AltaBitacora(userSession.Email, userSession.Puesto, "Quita patente a usuario", Models.Enums.Criticidad.ALTA);
                _digitoVerificadorService.CalcularDVTabla("UsuarioPermiso");
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

        public int AltaFamiliaPatente(Componente componente, bool familia, Usuario usuario)
        {
            try
            {
                _permisoDAO.AltaFamiliaPatente(componente, familia);
                _bitacoraService.AltaBitacora(usuario.Email, usuario.Puesto, "Da de alta una familia", Models.Enums.Criticidad.ALTA);
                _digitoVerificadorService.CalcularDVTabla("Permiso");
                return 1;
            }
            catch (Exception ex) when (ex.Message.Contains("SQL") || ex.Message.Contains("BD"))
            {
                throw new Exception("ErrorBD");
            }
            catch (Exception)
            {
                throw new Exception("ErrorAltaFamiliaPatente");
            }
        }

        public void GuardarUsuarioPermiso(int puestoId, int permisoId)
        {
            try
            {
                _permisoDAO.GuardarUsuarioPermiso(puestoId, permisoId);
                _digitoVerificadorService.CalcularDVTabla("UsuarioPermiso");
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

        public void EliminarPermisoUsuario(int puestoId, int permisoId)
        {
            try
            {
                _permisoDAO.EliminarUsuarioPermiso(puestoId, permisoId);
                _digitoVerificadorService.CalcularDVTabla("UsuarioPermiso");
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

        public IList<Familia> ObtenerFamilias()
        {
            try
            {
                IList<Familia> familias = _permisoDAO.ObtenerFamilias();
                return familias;
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

        public IList<Componente> ObtenerPermisosNoAsignados(int familiaId)
        {
            try
            {
                IList<Componente> permisosNoAsignados = _permisoDAO.ObtenerPermisosNoAsignados(familiaId);
                return permisosNoAsignados;
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

        public IList<Componente> ObtenerPermisosNoAsignadosPorUsuario(int idUsuario)
        {
            try
            {
                IList<Componente> permisosNoAsignados = _permisoDAO.ObtenerPermisosNoAsignadosPorUsuario(idUsuario);
                return permisosNoAsignados;
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

        public IList<Componente> ObtenerPermisosAsignadosPorUsuario(int idUsuario)
        {
            try
            {
                IList<Componente> permisosAsignados = _permisoDAO.ObtenerPermisosAsignadosPorUsuario(idUsuario);
                return permisosAsignados;
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

        public IList<Componente> ObtenerPermisosPorFamilia(int familiaId)
        {
            try
            {
                IList<Componente> permisosNoAsignados = _permisoDAO.ObtenerPermisosPorFamilia(familiaId);
                return permisosNoAsignados;
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

        public bool ExisteComponente(Componente componente, int Id)
        {
            bool existeComponente = false;

            if (componente.Id.Equals(Id))
                existeComponente = true;

            else
            {
                foreach (var item in componente.Hijos)
                {
                    existeComponente = ExisteComponente(item, Id);
                    if (existeComponente) return true;
                }
            }

            return existeComponente;
        }

        public void GetComponenteUsuario(Usuario usuario)
        {
            try
            {
                _permisoDAO.GetComponenteUsuario(usuario);
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

        public DataTable ObtenerFamiliaUsuario(int idUsuario)
        {
            try
            {
                var resultado = _permisoDAO.ObtenerFamiliaUsuario(idUsuario);
                return resultado.Tables[0];
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

        public void ActualizarFamiliaUsuario(Usuario usuario, int? puestoAnterior, Usuario userSession)
        {
            try
            {
                _permisoDAO.ActualizarFamiliaUsuario(usuario, puestoAnterior);
                _permisoDAO.InsertarFamiliaUsuario(usuario);
                _bitacoraService.AltaBitacora(userSession.Email, userSession.Puesto, $"Actualiza familia a usuario", Models.Enums.Criticidad.ALTA);
                _digitoVerificadorService.CalcularDVTabla("UsuarioPermiso");
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

        public bool TienePermiso(Usuario usuario, Permiso permiso)
        {
            if(!usuario.Permisos.Any(p => p.Permiso != null && p.Permiso.Equals(permiso)))
            {
                _bitacoraService.AltaBitacora(usuario.Email, usuario.Puesto, $"Acceso a pantalla {permiso} denegada", Models.Enums.Criticidad.ALTA);
                return false;
            }
            return true;
        }

        #endregion
    }
}