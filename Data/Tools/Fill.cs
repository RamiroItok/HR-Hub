using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Data.Tools
{
    public class Fill
    {
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
