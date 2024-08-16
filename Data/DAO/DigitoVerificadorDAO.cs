﻿using Data.Conexion;
using Data.Interfaces;
using System;
using System.Data;
using System.Text;

namespace Data.DAO
{
    public class DigitoVerificadorDAO : IDigitoVerificadorDAO
    {
        private readonly Acceso _acceso;

        public DigitoVerificadorDAO()
        {
            _acceso = Acceso.GetInstance;
        }

        public string Verificar_DV()
        {
            try
            {
                string mensaje = "";
                string consulta = "SELECT Nombre_Tabla as Tabla, DV as Valor_DV from DigitoVerificador";
                DataTable dt = _acceso.GenerarConsulta(consulta);
                foreach (DataRow fila in dt.Rows)
                {
                    int DV = int.Parse(fila[1].ToString());
                    string tabla = fila[0].ToString();

                    string consulta2 = "SELECT ISNULL(SUM(DVH),0) DVH FROM " + tabla;
                    DataTable dt2 = _acceso.GenerarConsulta(consulta2);
                    int DVH = int.Parse(dt2.Rows[0]["DVH"].ToString());

                    if (DV == DVH)
                    {
                        mensaje = "true";
                    }
                    else
                    {
                        mensaje = "false";
                        return mensaje;
                    }
                    string consulta3 = "SELECT * FROM " + tabla;
                    DataTable dt3 = _acceso.GenerarConsulta(consulta3);
                    int dvh_fila = 0;
                    foreach (DataRow fila1 in dt3.Rows)
                    {
                        foreach (DataColumn dc in dt3.Columns)
                        {
                            if (dc.ColumnName.ToString().Substring(0, 2).ToUpper() != "Id")
                            {
                                if (dc.ColumnName.ToString().ToUpper() != "DVH")
                                {
                                    string celda = fila1[dc].ToString();
                                    for (int i = 0; +i < fila1[dc].ToString().Length; i++)
                                    {
                                        byte[] bytes = Encoding.ASCII.GetBytes(fila1[dc].ToString().Substring(i, 1));
                                        int result = bytes[0];
                                        dvh_fila = dvh_fila + (result * (i + 1));
                                    }
                                }
                            }
                        }
                    }
                    if (dvh_fila == DV)
                    {
                        mensaje = "true";
                    }
                    else
                    {
                        mensaje = tabla;
                        return mensaje;
                    }

                }
                return mensaje;
            }
            catch
            {
                throw new Exception("Error en la base de datos. ");
            }
        }

        public bool Recalcular_DV()
        {
            try
            {
                bool mensaje = true;
                string consulta = "SELECT Nombre_Tabla as Tabla, DV as Valor_DV from DigitoVerificador";
                DataTable dt = _acceso.GenerarConsulta(consulta);
                foreach (DataRow fila in dt.Rows)
                {
                    string tabla = fila[0].ToString();
                    int DV = int.Parse(fila[1].ToString());
                    int x = 0;
                    int DV_tabla = 0;
                    string consulta2 = $@"SELECT * FROM {tabla}";
                    DataTable dt2 = _acceso.GenerarConsulta(consulta2);
                    foreach (DataRow fila1 in dt2.Rows)
                    {
                        int DVH_total = 0;
                        int dvh_fila = 0;
                        foreach (DataColumn dc in dt2.Columns)
                        {
                            if (dc.ColumnName.ToString().Substring(0, 2).ToUpper() != "Id")
                            {
                                if (dc.ColumnName.ToString().ToUpper() != "DVH")
                                {
                                    string celda = fila1[dc].ToString();
                                    for (int i = 0; i < fila1[dc].ToString().Length; i++)
                                    {
                                        byte[] bytes = Encoding.ASCII.GetBytes(fila1[dc].ToString().Substring(i, 1));
                                        int result = bytes[0];
                                        dvh_fila = dvh_fila + (result * (i + 1));
                                    }
                                }
                                else
                                {
                                    DVH_total = int.Parse(fila1[dc].ToString());
                                }
                            }
                        }
                        if (DVH_total != dvh_fila)
                        {
                            if (tabla == "Usuarios")
                            {
                                string consulta1 = $@"UPDATE {tabla} set DVH = '{dvh_fila}' WHERE Id = '{dt2.Rows[x]["Id"].ToString()}'";
                                _acceso.GenerarConsulta(consulta1);
                            }
                            else if (tabla == "Bitacora")
                            {
                                string consulta1 = $@"UPDATE {tabla} set DVH = '{dvh_fila}' WHERE Id = '{dt2.Rows[x]["Id"].ToString()}'";
                                _acceso.GenerarConsulta(consulta1);
                            }
                        }
                        x = x + 1;
                        DV_tabla = DV_tabla + dvh_fila;
                    }
                    string xxconsulta = $@"UPDATE DigitoVerificador set dv = {DV_tabla} WHERE Nombre_Tabla = '{tabla}'";
                    _acceso.GenerarConsulta(xxconsulta);

                }
                return mensaje;
            }
            catch
            {
                throw new Exception("Error en la base de datos. ");
            }
        }

        public DataTable ObtenerTabla(string tabla)
        {
            try
            {
                string consulta = $"SELECT * FROM {tabla}";
                DataTable dt = _acceso.GenerarConsulta(consulta);
                return dt;
            }
            catch (Exception)
            {
                throw new Exception("Error en la base de datos.");
            }
        }
    }
}