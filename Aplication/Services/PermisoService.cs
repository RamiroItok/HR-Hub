﻿using Aplication.Interfaces;
using Models.Composite;
using Models.DTOs;
using System;
using System.Collections.Generic;

namespace Aplication.Services
{
    public class PermisoService : IPermisoService
    {
        private readonly Data.Composite.PermisoDAO _permisoDAO;
        private readonly IDigitoVerificadorService _digitoVerificadorService;

        public PermisoService()
        { 
            _permisoDAO = new Data.Composite.PermisoDAO();
            _digitoVerificadorService = new DigitoVerificadorService();
        }

        #region Metodos
        public void GuardarFamiliaCreada(Familia familia)
        {
            try
            {
                _permisoDAO.GuardarFamiliaCreada(familia);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int AltaFamiliaPatente(Componente componente, bool familia)
        {
            try
            {
                _permisoDAO.AltaFamiliaPatente(componente, familia);
                return 1;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void GuardarPermiso(UsuarioDTO usuario)
        {
            try
            {
                _permisoDAO.GuardarPermiso(usuario);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void PrimerRegistroGuardarPermiso(int idUsuario, int idPatente)
        {
            try
            {
                _permisoDAO.PrimerRegistroGuardarPermiso(idUsuario, idPatente);
                _digitoVerificadorService.RecalcularDV();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IList<Componente> TraerFamiliaPatentes(int familiaId)
        {
            try
            {
                IList<Componente> componentes = _permisoDAO.TraerFamiliaPatentes(familiaId);
                return componentes;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public Componente ObtenerFamiliaArbol(int familiaId, Componente componenteOriginal, Componente componenteAgregar)
        {
            try
            {
                Componente comp = _permisoDAO.ObtenerFamiliaArbol(familiaId, componenteOriginal, componenteAgregar);
                return comp;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public Componente GetUsuarioArbol(int usuarioId, Componente componenteOriginal, Componente componenteAgregar)
        {
            try
            {
                Componente comp = _permisoDAO.GetUsuarioArbol(usuarioId, componenteOriginal, componenteAgregar);
                return comp;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public IList<Familia> ObtenerFamilias()
        {
            try
            {
                IList<Familia> familias = _permisoDAO.ObtenerFamilias();
                return familias;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public IList<Patente> ObtenerPatentes()
        {
            try
            {
                IList<Patente> patentes = _permisoDAO.ObtenerPatentes();
                return patentes;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public Array TraerPermisos()
        {
            try
            {
                return Enum.GetValues(typeof(Models.Composite.Permiso));
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public IList<Familia> GetFamiliasValidacion(int familiaId)
        {
            try
            {
                IList<Familia> familias = _permisoDAO.GetFamiliasValidacion(familiaId);
                return familias;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
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

        public void GetComponenteUsuario(UsuarioDTO usuario)
        {
            try
            {
                _permisoDAO.GetComponenteUsuario(usuario);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void GetComponenteFamilia(Familia familia)
        {
            try
            {
                _permisoDAO.GetComponenteFamilia(familia);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Tool
        public static bool TienePermiso(UsuarioDTO usuario, Models.Composite.Permiso permiso)
        {
            bool existePermiso = false;

            foreach (Componente item in usuario.Permisos)
            {
                if (item.Permiso.Equals(permiso)) return true;

                else
                {
                    existePermiso = EstaPermisoEnFamilia(item, permiso, existePermiso);
                    if (existePermiso) return true;
                }
            }

            return existePermiso;
        }

        private static bool EstaPermisoEnFamilia(Componente c, Models.Composite.Permiso permiso, bool existePermiso)
        {
            if (c.Permiso.Equals(permiso)) existePermiso = true;

            else
            {
                foreach (var item in c.Hijos)
                {
                    existePermiso = EstaPermisoEnFamilia(item, permiso, existePermiso);
                    if (existePermiso) return true;
                }
            }

            return existePermiso;
        }

        /*public static void HabilitarMenu(UsuarioDTO usuario, ToolStripMenuItem menu)
        {
            foreach (ToolStripMenuItem item in menu.DropDownItems)
            {
                string Nombre = item.Tag.ToString().Replace("menu_", "");
                Models.Composite.Permiso permiso = (Models.Composite.Permiso)Enum.Parse(typeof(Models.Composite.Permiso), Nombre);

                bool tienePermiso = TienePermiso(usuario, permiso);
                item.Enabled = tienePermiso;
                item.Visible = tienePermiso;
            }
        }*/
        #endregion
    }
}