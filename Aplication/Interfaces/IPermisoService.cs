using Models;
using Models.Composite;
using System;
using System.Collections.Generic;
using System.Data;

namespace Aplication.Interfaces
{
    public interface IPermisoService
    {
        int AltaFamiliaPatente(Componente componente, bool familia);
        IList<Familia> ObtenerFamilias();
        bool ExisteComponente(Componente componente, int Id);
        void GetComponenteUsuario(Usuario usuario);
        void GuardarUsuarioPermiso(int puestoId, int permisoId);
        void EliminarPermisoUsuario(int puestoId, int permisoId);
        IList<Componente> ObtenerPermisosNoAsignados(int familiaId);
        IList<Componente> ObtenerPermisosNoAsignadosPorUsuario(int idUsuario);
        IList<Componente> ObtenerPermisosAsignadosPorUsuario(int idUsuario);
        IList<Componente> ObtenerPermisosPorFamilia(int familiaId);
        void AsignarPermisoAFamilia(int padreId, int hijoId);
        void QuitarPermisoAFamilia(int padreId, int hijoId);
        void AsignarPermisoAUsuario(int idUsuario, int idPatente, Usuario userSession);
        void QuitarPermisoAUsuario(int idUsuario, int idPatente, Usuario userSession);
        DataTable ObtenerFamiliaUsuario(int idUsuario);
        void ActualizarFamiliaUsuario(Usuario usuario, int? puestoAnterior);
        bool TienePermiso(Usuario usuario, Permiso permiso);
    }
}