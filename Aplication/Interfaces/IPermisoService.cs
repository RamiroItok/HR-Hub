using Models;
using Models.Composite;
using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Data;

namespace Aplication.Interfaces
{
    public interface IPermisoService
    {
        int AltaFamiliaPatente(Componente componente, bool familia);
        void GuardarFamiliaCreada(Familia familia);
        IList<Familia> ObtenerFamilias();
        IList<Patente> ObtenerPatentes();
        IList<Componente> TraerFamiliaPatentes(int familiaId);
        Array TraerPermisos();
        bool ExisteComponente(Componente componente, int Id);
        void GetComponenteUsuario(Usuario usuario);
        void GetComponenteFamilia(Familia familia);
        void GuardarUsuarioPermiso(int puestoId, int permisoId);
        void EliminarPermisoUsuario(int puestoId, int permisoId);
        void PrimerRegistroGuardarPermiso(int idUsuario, int idPatente);
        IList<Familia> GetFamiliasValidacion(int familiaId);
        Componente ObtenerFamiliaArbol(int familiaId, Componente componenteOriginal, Componente componenteAgregar);
        Componente GetUsuarioArbol(int usuarioId, Componente componenteOriginal, Componente componenteAgregar);
        IList<Componente> ObtenerPermisosNoAsignados(int familiaId);
        IList<Componente> ObtenerPermisosNoAsignadosPorUsuario(int idUsuario);
        IList<Componente> ObtenerPermisosAsignadosPorUsuario(int idUsuario);
        IList<Componente> ObtenerPermisosPorFamilia(int familiaId);
        void AsignarPermisoAFamilia(int padreId, int hijoId);
        void QuitarPermisoAFamilia(int padreId, int hijoId);
        void AsignarPermisoAUsuario(int idUsuario, int idPatente, Usuario userSession);
        void QuitarPermisoAUsuario(int idUsuario, int idPatente, Usuario userSession);
        DataTable ObtenerFamiliaUsuario(int idUsuario);
    }
}