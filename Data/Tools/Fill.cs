using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Data.Tools
{
    public class Fill
    {
        #region Usuario
        public Models.Usuario FillObjectUsuario(DataRow dr)
        {
            Models.Usuario usuario = new Models.Usuario();
            try
            {
                if (dr.Table.Columns.Contains("Id") && !Convert.IsDBNull(dr["Id"]))
                    usuario.Id = Convert.ToInt32(dr["Id"]);

                if(dr.Table.Columns.Contains("Nombre") && !Convert.IsDBNull(dr["Nombre"]))
                    usuario.Nombre = Convert.ToString(dr["Nombre"]);

                if (dr.Table.Columns.Contains("Apellido") && !Convert.IsDBNull(dr["Apellido"]))
                    usuario.Apellido = Convert.ToString(dr["Apellido"]);

                if (dr.Table.Columns.Contains("Email") && !Convert.IsDBNull(dr["Email"]))
                    usuario.Email = Convert.ToString(dr["Email"]);

                if (dr.Table.Columns.Contains("Contraseña") && !Convert.IsDBNull(dr["Contraseña"]))
                    usuario.Contraseña = Convert.ToString(dr["Contraseña"]);

                if (dr.Table.Columns.Contains("IdPuesto") && !Convert.IsDBNull(dr["IdPuesto"]))
                    usuario.Puesto = ((Models.Enums.Puesto)dr["IdPuesto"]);

                if (dr.Table.Columns.Contains("Area") && !Convert.IsDBNull(dr["Area"]))
                    usuario.Area = (Models.Enums.Area)(dr["Area"]);

                if(dr.Table.Columns.Contains("FechaIngreso") && !Convert.IsDBNull(dr["FechaIngreso"]))
                    usuario.FechaIngreso = ((DateTime)dr["FechaIngreso"]);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en el método FillObject, " + ex.Message);
            }
            return usuario;
        }

        public List<Models.Usuario> FillListUsuario(DataSet ds)
        {
            return (from DataRow dr in ds.Tables[0].Rows select (new Fill()).FillObjectUsuario(dr)).ToList();
        }

        public UsuarioDTO FillObjectUsuarioDTO(DataRow dr)
        {
            UsuarioDTO usuario = new UsuarioDTO();
            try
            {
                if(dr.Table.Columns.Contains("Id") && !Convert.IsDBNull(dr["Id"]))
                    usuario.Id = Convert.ToInt32(dr["Id"]);

                if (dr.Table.Columns.Contains("Nombre") && !Convert.IsDBNull(dr["Nombre"]))
                    usuario.Nombre = Convert.ToString(dr["Nombre"]);

                if (dr.Table.Columns.Contains("Apellido") && !Convert.IsDBNull(dr["Apellido"]))
                    usuario.Apellido = Convert.ToString(dr["Apellido"]);

                if (dr.Table.Columns.Contains("Email") && !Convert.IsDBNull(dr["Email"]))
                    usuario.Email = Convert.ToString(dr["Email"]);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en el método FillObject, " + ex.Message);
            }
            return usuario;
        }

        public UsuarioDTO FillObjectUsuarioDesencriptado(DataRow dr)
        {
            UsuarioDTO usuario = new UsuarioDTO();

            try
            {
                if (dr.Table.Columns.Contains("Id") && !Convert.IsDBNull(dr["Id"]))
                    usuario.Id = Convert.ToInt32(dr["Id"]);

                if (dr.Table.Columns.Contains("Nombre") && !Convert.IsDBNull(dr["Nombre"]))
                    usuario.Nombre = Convert.ToString(dr["Nombre"]);

                if (dr.Table.Columns.Contains("Apellido") && !Convert.IsDBNull(dr["Apellido"]))
                    usuario.Apellido = Convert.ToString(dr["Apellido"]);

                if (dr.Table.Columns.Contains("Email") && !Convert.IsDBNull(dr["Email"]))
                    usuario.Email = Convert.ToString(dr["Email"]);

            }
            catch (Exception ex)
            {
                throw new Exception("Error en el método FillObject, " + ex.Message);
            }
            return usuario;
        }

        public List<Models.DTOs.UsuarioDTO> FillListUsuarioDTO(DataSet ds)
        {
            return (from DataRow dr in ds.Tables[0].Rows select (new Fill()).FillObjectUsuarioDTO(dr)).ToList();
        }

        public List<Models.DTOs.UsuarioDTO> FillListBloqueado(DataSet ds)
        {
            return (from DataRow dr in ds.Tables[0].Rows select (new Fill()).FillObjectUsuarioDTO(dr)).ToList();
        }


        #endregion Usuario

        #region Familia
        public Models.Composite.Familia FillObjectFamilia(DataRow dr)
        {
            Models.Composite.Familia familia = new Models.Composite.Familia();

            try
            {
                if (dr.Table.Columns.Contains("Id") && !Convert.IsDBNull(dr["Id"]))
                    familia.Id = Convert.ToInt32(dr["Id"]);


                if (dr.Table.Columns.Contains("Nombre") && !Convert.IsDBNull(dr["Nombre"]))
                    familia.Nombre = Convert.ToString(dr["Nombre"]);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en el método FillObject, " + ex.Message);
            }

            return familia;
        }

        public List<Models.Composite.Familia> FillListFamilia(DataSet ds)
        {
            return (from DataRow dr in ds.Tables[0].Rows select (new Fill()).FillObjectFamilia(dr)).ToList();
        }
        #endregion

        #region Patente
        public Models.Composite.Patente FillObjectPatente(DataRow dr)
        {
            Models.Composite.Patente patente = new Models.Composite.Patente();

            try
            {
                if (dr.Table.Columns.Contains("Id") && !Convert.IsDBNull(dr["Id"]))
                    patente.Id = Convert.ToInt32(dr["Id"]);


                if (dr.Table.Columns.Contains("Nombre") && !Convert.IsDBNull(dr["Nombre"]))
                    patente.Nombre = Convert.ToString(dr["Nombre"]);

                if (dr.Table.Columns.Contains("Permiso") && !Convert.IsDBNull(dr["Permiso"]))
                    patente.Permiso = (Models.Composite.Permiso)Enum.Parse(typeof(Models.Composite.Permiso), dr["Permiso"].ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("Error en el método FillObject, " + ex.Message);
            }

            return patente;
        }

        public List<Models.Composite.Patente> FillListPatente(DataSet ds)
        {
            return (from DataRow dr in ds.Tables[0].Rows select (new Fill()).FillObjectPatente(dr)).ToList();
        }
        #endregion
    }
}
