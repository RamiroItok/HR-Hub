using Data.Conexion;
using Data.Tools;
using Models;
using Models.Composite;
using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Data.Composite
{
    public class PermisoDAO
    {
        private readonly Fill _fill;
        private readonly Acceso _acceso;
        public PermisoDAO()
        {
            _fill = new Fill();
            _acceso = Acceso.GetInstance;
        }

        public int AltaFamiliaPatente(Componente componente, bool familia)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@parNombre", componente.Nombre }
                };

                if (familia)
                    parameters.Add("@parPermiso", DBNull.Value);
                else 
                    parameters.Add("@parPermiso", componente.Permiso.ToString());

                DataSet resultado = _acceso.ExecuteStoredProcedureReader("sp_i_permiso", parameters);

                return Convert.ToInt32(resultado.Tables[0].Rows[0]["Id"]);
            }
            catch (Exception)
            {
                throw new Exception("Error en la base de datos.");
            }
        }

        public void GuardarFamiliaCreada(Familia familia)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@parId", familia.Id }
                };

                _acceso.ExecuteStoredProcedureReader("sp_d_familiaPatente", parameters);

                foreach (Componente item in familia.Hijos)
                {
                    Dictionary<string, object> parameteros = new Dictionary<string, object>
                    {
                        { "@parPadreId", familia.Id },
                        { "@parHijoId", item.Id }
                    };

                    _acceso.ExecuteStoredProcedureReader("sp_i_familiaPatente", parameteros);
                }
            }
            catch (Exception)
            {
                throw new Exception("Error en la base de datos.");
            }
        }

        public void AsignarPermisoAFamilia(int padreId, int hijoId)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@parPadreId", padreId },
                    { "@parHijoId", hijoId }
                };

                _acceso.ExecuteStoredProcedureReader("sp_i_familiaPatente", parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al asignar el permiso a la familia.", ex);
            }
        }

        public void ActualizarFamiliaUsuario(Usuario usuario, int puestoAnterior)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@IdUsuario", usuario.Id },
                    { "@PuestoAnterior", puestoAnterior }
                };

                _acceso.ExecuteStoredProcedureReader("sp_d_usuarioPermisoActualizacion", parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar permisos anteriores.", ex);
            }
        }

        public void InsertarFamiliaUsuario(Usuario usuario)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@IdPuesto", (int)usuario.Puesto }
                };

                var resultado = _acceso.ExecuteStoredProcedureReader("sp_s_familiaPatente_porPuesto", parameters);
                DataTable dt = resultado.Tables[0];

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow rows in dt.Rows)
                    {
                        Dictionary<string, object> parametros = new Dictionary<string, object>
                        {
                            { "@IdPermiso", int.Parse(rows["IdPermiso"].ToString()) },
                            { "@IdUsuario", usuario.Id}
                        };

                        _acceso.ExecuteStoredProcedureReader("sp_i_UsuarioPermiso", parametros);
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("Error en la base de datos.");
            }
        }

        public void AsignarPermisoAUsuario(int idUsuario, int idPatente)
        {
            try
            {
                Dictionary<string, object> parametros = new Dictionary<string, object>
                {
                    { "@IdPermiso", idPatente },
                    { "@IdUsuario", idUsuario }
                };

                _acceso.ExecuteStoredProcedureReader("sp_i_UsuarioPermiso", parametros);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al asignar el permiso al usuario.", ex);
            }
        }

        public void QuitarPermisoAFamilia(int padreId, int hijoId)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@parPadreId", padreId },
                    { "@parHijoId", hijoId }
                };

                _acceso.ExecuteStoredProcedureReader("sp_d_familiaPatente", parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al quitar el permiso a la familia.", ex);
            }
        }

        public void QuitarPermisoAUsuario(int idUsuario, int idPatente)
        {
            try
            {
                Dictionary<string, object> parametros = new Dictionary<string, object>
                {
                    { "@parUsuarioId", idUsuario },
                    { "@parPermisoId", idPatente }
                };

                _acceso.ExecuteStoredProcedureReader("sp_d_UsuarioPermiso", parametros);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al quitar el permiso al usuario.", ex);
            }
        }

        public void GuardarUsuarioPermiso(int puestoId, int permisoId)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@parPuestoId", puestoId }
                };

                var resultado = _acceso.ExecuteStoredProcedureReader("sp_s_Usuario_porPuesto", parameters);
                DataTable dt = resultado.Tables[0];

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow rows in dt.Rows)
                    {
                        Dictionary<string, object> parametros = new Dictionary<string, object>
                        {
                            { "@IdPermiso", permisoId },
                            { "@IdUsuario", int.Parse(rows["ID"].ToString()) }
                        };

                        _acceso.ExecuteStoredProcedureReader("sp_i_UsuarioPermiso", parametros);
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("Error en la base de datos.");
            }
        }

        public void EliminarUsuarioPermiso(int puestoId, int permisoId)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@parPuestoId", puestoId }
                };

                var resultado = _acceso.ExecuteStoredProcedureReader("sp_s_Usuario_porPuesto", parameters);
                DataTable dt = resultado.Tables[0];

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow rows in dt.Rows)
                    {
                        Dictionary<string, object> parametros = new Dictionary<string, object>
                        {
                            { "@parUsuarioId", int.Parse(rows["ID"].ToString()) },
                            { "@parPermisoId", permisoId }
                        };

                        _acceso.ExecuteStoredProcedureReader("sp_d_UsuarioPermiso", parametros);
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("Error en la base de datos.");
            }
        }

        public void PrimerRegistroGuardarPermiso(int idUsuario, int idPatente)
        {
            try
            {
                Dictionary<string, object> parameteros = new Dictionary<string, object>
                {
                    { "@parUsuarioId", idUsuario },
                    { "@parPatenteId", idPatente },
                    { "@parDVH", 0 }
                };

                _acceso.ExecuteStoredProcedureReader("sp_i_usuarioPermiso", parameteros);
            }
            catch (Exception)
            {
                throw new Exception("Error en la base de datos.");
            }
        }

        public IList<Familia> ObtenerFamilias()
        {
            try
            {
                var ds = _acceso.ExecuteStoredProcedureReader("sp_s_permiso", null);

                IList<Models.Composite.Familia> familias = new List<Models.Composite.Familia>();

                if (ds.Tables[0].Rows.Count > 0)
                    familias = _fill.FillListFamilia(ds);

                return familias;
            }
            catch (Exception)
            {
                throw new Exception("Error en la base de datos.");
            }
        }

        public IList<Patente> ObtenerPatentes()
        {
            try
            {
                var ds = _acceso.ExecuteStoredProcedureReader("sp_s_permiso_notNull", null);

                IList<Models.Composite.Patente> patentes = new List<Models.Composite.Patente>();

                if (ds.Tables[0].Rows.Count > 0)
                    patentes = _fill.FillListPatente(ds);

                return patentes;
            }
            catch (Exception)
            {
                throw new Exception("Error en la base de datos.");
            }
        }

        public IList<Models.Composite.Componente> TraerFamiliaPatentes(int Id_Familia)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@idFamilia", Id_Familia }
                };

                var resultado = _acceso.ExecuteStoredProcedureReader("sp_s_familiaPatente", parameters);
                DataTable dt = resultado.Tables[0];

                List<Componente> componentes = new List<Componente>();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow rows in dt.Rows)
                    {
                        int padreId = 0;
                        if (rows["PadreId"] != DBNull.Value)
                        {
                            padreId = int.Parse(rows["PadreId"].ToString());
                        }

                        int Id = int.Parse(rows["PermisoId"].ToString());
                        string nombre = rows["Nombre"].ToString();
                        string permiso = string.Empty;
                        if (rows["Permiso"] != DBNull.Value) permiso = rows["Permiso"].ToString();

                        Componente componente;
                        if (string.IsNullOrEmpty(permiso)) componente = new Familia();
                        else componente = new Patente();

                        componente.Id = Id;
                        componente.Nombre = nombre;
                        if (!string.IsNullOrEmpty(permiso)) componente.Permiso = (Models.Composite.Permiso)Enum.Parse(typeof(Models.Composite.Permiso), permiso);

                        Componente padre = GetComponente(padreId, componentes);
                        if (padre == null) componentes.Add(componente);
                        else padre.AgregarHijo(componente);
                    }
                }
                return componentes;
            }
            catch (Exception)
            {
                throw new Exception("Error en la base de datos.");
            }
        }

        public Componente ObtenerFamiliaArbol(int familiaId, Componente componenteOriginal, Componente componenteAgregar)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@idFamilia", familiaId }
                };

                var resultado = _acceso.ExecuteStoredProcedureReader("sp_s_permiso_familiaPatente", parameters);
                DataTable dt = resultado.Tables[0];

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow rows in dt.Rows)
                    {
                        int Id = int.Parse(rows["PermisoId"].ToString());
                        string nombre = rows["Nombre"].ToString();
                        string permiso = string.Empty;
                        if (rows["Permiso"] != DBNull.Value) permiso = rows["Permiso"].ToString();

                        Componente componente;
                        if (string.IsNullOrEmpty(permiso)) componente = new Familia();
                        else componente = new Patente();

                        componente.Id = Id;
                        componente.Nombre = nombre;
                        if (!string.IsNullOrEmpty(permiso)) componente.Permiso = (Models.Composite.Permiso)Enum.Parse(typeof(Models.Composite.Permiso), permiso);

                        if (componenteAgregar != null)
                        {
                            if (componente.GetType() == typeof(Patente)) componenteAgregar.AgregarHijo(componente);
                            else if (componente.GetType() == typeof(Familia)) LlenarComponenteFamilia(componente, componenteOriginal, componenteAgregar);
                        }
                        else
                        {
                            if (componente.GetType() == typeof(Patente)) componenteOriginal.AgregarHijo(componente);
                            else if (componente.GetType() == typeof(Familia)) LlenarComponenteFamilia(componente, componenteOriginal, componenteOriginal);
                        }
                    }
                }

                return componenteOriginal;
            }
            catch (Exception)
            {
                throw new Exception("Error en la base de datos.");
            }
        }

        public Componente GetUsuarioArbol(int usuarioId, Componente componenteOriginal, Componente componenteAgregar)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@idUsuario", usuarioId }
                };

                var resultado = _acceso.ExecuteStoredProcedureReader("sp_s_permiso_usuarioPermiso", parameters);
                DataTable dt = resultado.Tables[0];

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow rows in dt.Rows)
                    {
                        int Id = int.Parse(rows["PermisoId"].ToString());
                        string nombre = rows["Nombre"].ToString();
                        string permiso = string.Empty;
                        if (rows["Permiso"] != DBNull.Value) permiso = rows["Permiso"].ToString();

                        Componente componente;
                        if (string.IsNullOrEmpty(permiso)) componente = new Familia();
                        else componente = new Patente();

                        componente.Id = Id;
                        componente.Nombre = nombre;
                        if (!string.IsNullOrEmpty(permiso)) componente.Permiso = (Models.Composite.Permiso)Enum.Parse(typeof(Models.Composite.Permiso), permiso);

                        if (componenteAgregar != null)
                        {
                            if (componente.GetType() == typeof(Patente)) componenteAgregar.AgregarHijo(componente);
                            else if (componente.GetType() == typeof(Familia)) LlenarComponenteFamilia(componente, componenteOriginal, componenteAgregar);
                        }
                        else
                        {
                            if (componente.GetType() == typeof(Patente)) componenteOriginal.AgregarHijo(componente);
                            else if (componente.GetType() == typeof(Familia)) LlenarComponenteFamilia(componente, componenteOriginal, componenteOriginal);
                        }
                    }
                }

                return componenteOriginal;
            }
            catch (Exception)
            {
                throw new Exception("Error en la base de datos.");
            }
        }

        public IList<Familia> GetFamiliasValidacion(int familiaId)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@familiaId", familiaId }
                };

                var resultado = _acceso.ExecuteStoredProcedureReader("sp_s_familia_validacion", parameters);

                IList<Familia> familias = new List<Familia>();

                if (resultado.Tables[0].Rows.Count > 0)
                    familias = _fill.FillListFamilia(resultado);

                return familias;
            }
            catch (Exception)
            {
                throw new Exception("Error en la base de datos.");
            }
        }

        private Componente GetComponente(int id, IList<Componente> lista)
        {
            try
            {
                Componente componente = lista != null ? lista.Where(i => i.Id.Equals(id)).FirstOrDefault() : null;

                if (componente == null && lista != null)
                {
                    foreach (var item in lista)
                    {
                        var _lista = GetComponente(id, item.Hijos);
                        if (_lista != null && _lista.Id == id) return _lista;
                        else if (_lista != null) return GetComponente(id, _lista.Hijos);
                    }
                }
                return componente;
            }
            catch (Exception) { throw new Exception("Error al obtener los componentes."); }
        }

        public void GetComponenteUsuario(Usuario usuario)
        {
            try
            {
                usuario.Permisos.Clear();
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@idUsuario", usuario.Id }
                };

                var resultado = _acceso.ExecuteStoredProcedureReader("sp_s_usuarioPermiso_permiso", parameters);
                DataTable dt = resultado.Tables[0];

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow rows in dt.Rows)
                    {
                        int id = int.Parse(rows["Id"].ToString());
                        string nombre = rows["Nombre"].ToString();
                        string permiso = String.Empty;
                        if (rows["Permiso"].ToString() != String.Empty) permiso = rows["Permiso"].ToString();

                        Componente componente;
                        if (!String.IsNullOrEmpty(permiso))
                        {
                            componente = new Patente();
                            componente.Id = id;
                            componente.Nombre = nombre;
                            componente.Permiso = (Models.Composite.Permiso)Enum.Parse(typeof(Models.Composite.Permiso), permiso);
                            usuario.Permisos.Add(componente);
                        }
                        else
                        {
                            componente = new Familia();
                            componente.Id = id;
                            componente.Nombre = nombre;

                            var familia = TraerFamiliaPatentes(id);
                            foreach (var f in familia)
                            {
                                componente.AgregarHijo(f);
                            }

                            usuario.Permisos.Add(componente);
                        }
                    }
                }
            }
            catch (Exception) { throw new Exception("Hubo un error al obtener los permisos del usuario."); }
        }

        public void GetComponenteFamilia(Familia familia)
        {
            familia.VaciarHijos();
            foreach (Componente item in TraerFamiliaPatentes(familia.Id))
            {
                familia.AgregarHijo(item);
            }
        }

        public DataSet ObtenerFamiliaUsuario(int idUsuario)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@IdUsuario", idUsuario }
            };

            return _acceso.ExecuteStoredProcedureReader("sp_s_familiaUsuario", parameters);
        }

        private void LlenarComponenteFamilia(Componente componente, Componente componenteOriginal, Componente componenteRaiz)
        {
            Componente familia = new Familia();
            familia = componente;

            componenteRaiz.AgregarHijo(familia);

            ObtenerFamiliaArbol(componente.Id, componenteOriginal, familia);
        }

        public List<Componente> ObtenerPermisosNoAsignados(int familiaId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@FamiliaId", familiaId }
            };

            var resultado = _acceso.ExecuteStoredProcedureReader("sp_ObtenerPermisosNoAsignados", parameters);
            DataTable dt = resultado.Tables[0];

            List<Componente> permisosNoAsignados = new List<Componente>();

            foreach (DataRow row in dt.Rows)
            {
                string permisoTexto = row["Permiso"].ToString();
                Componente componente;

                if (string.IsNullOrEmpty(permisoTexto))
                {
                    componente = new Familia();
                }
                else
                {
                    componente = new Patente();
                    componente.Permiso = (Permiso)Enum.Parse(typeof(Permiso), permisoTexto);
                }
                componente.Id = Convert.ToInt32(row["Id"]);
                componente.Nombre = row["Nombre"].ToString();

                permisosNoAsignados.Add(componente);
            }

            return permisosNoAsignados;
        }

        public List<Componente> ObtenerPermisosNoAsignadosPorUsuario(int idUsuario)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@IdUsuario", idUsuario }
            };

            var resultado = _acceso.ExecuteStoredProcedureReader("sp_ObtenerPermisosNoAsignadosPorUsuario", parameters);
            DataTable dt = resultado.Tables[0];

            List<Componente> permisosNoAsignados = new List<Componente>();

            foreach (DataRow row in dt.Rows)
            {
                string permisoTexto = row["Permiso"].ToString();
                Componente componente;

                if (string.IsNullOrEmpty(permisoTexto))
                {
                    componente = new Familia();
                }
                else
                {
                    componente = new Patente();
                    componente.Permiso = (Permiso)Enum.Parse(typeof(Permiso), permisoTexto);
                }
                componente.Id = Convert.ToInt32(row["Id"]);
                componente.Nombre = row["Nombre"].ToString();

                permisosNoAsignados.Add(componente);
            }

            return permisosNoAsignados;
        }

        public List<Componente> ObtenerPermisosAsignadosPorUsuario(int idUsuario)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@IdUsuario", idUsuario }
            };

            var resultado = _acceso.ExecuteStoredProcedureReader("sp_ObtenerPermisosAsignadosPorUsuario", parameters);
            DataTable dt = resultado.Tables[0];

            List<Componente> permisosAsignados = new List<Componente>();

            foreach (DataRow row in dt.Rows)
            {
                string permisoTexto = row["Permiso"].ToString();
                Componente componente;

                if (string.IsNullOrEmpty(permisoTexto))
                {
                    componente = new Familia();
                }
                else
                {
                    componente = new Patente();
                    componente.Permiso = (Permiso)Enum.Parse(typeof(Permiso), permisoTexto);
                }
                componente.Id = Convert.ToInt32(row["Id"]);
                componente.Nombre = row["Nombre"].ToString();

                permisosAsignados.Add(componente);
            }

            return permisosAsignados;
        }

        public List<Componente> ObtenerPermisosPorFamilia(int idFamilia)
        {
            List<Componente> permisosNoAsignados = new List<Componente>();

            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@idFamilia", idFamilia }
                };

                var resultado = _acceso.ExecuteStoredProcedureReader("sp_s_permiso_familiaPatente", parameters);
                DataTable dt = resultado.Tables[0];

                foreach (DataRow row in dt.Rows)
                {
                    Componente componente;
                    string permisoString = row["Permiso"] != DBNull.Value ? row["Permiso"].ToString() : null;

                    if (string.IsNullOrEmpty(permisoString))
                    {
                        componente = new Familia
                        {
                            Id = Convert.ToInt32(row["PermisoId"]),
                            Nombre = row["Nombre"].ToString()
                        };
                    }
                    else
                    {
                        componente = new Patente
                        {
                            Id = Convert.ToInt32(row["PermisoId"]),
                            Nombre = row["Nombre"].ToString(),
                            Permiso = (Permiso)Enum.Parse(typeof(Permiso), permisoString)
                        };
                    }

                    permisosNoAsignados.Add(componente);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los permisos no asignados para la familia.", ex);
            }

            return permisosNoAsignados;
        }

    }
}
