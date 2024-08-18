using Models.Composite;
using Models.DTOs;
using System;
using System.Collections.Generic;

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
        void GetComponenteUsuario(UsuarioDTO usuario);
        void GetComponenteFamilia(Familia familia);
        void GuardarPermiso(UsuarioDTO usuario);
        void PrimerRegistroGuardarPermiso(int idUsuario, int idPatente);
        IList<Familia> GetFamiliasValidacion(int familiaId);
        Componente ObtenerFamiliaArbol(int familiaId, Componente componenteOriginal, Componente componenteAgregar);
        Componente GetUsuarioArbol(int usuarioId, Componente componenteOriginal, Componente componenteAgregar);
    }
}