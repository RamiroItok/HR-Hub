﻿using Data.Conexion;
using Data.Interfaces;
using Models;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Data.DAO
{
    public class DigitoVerificadorDAO : IDigitoVerificadorDAO
    {
        private readonly Acceso _acceso;
        private readonly IBitacoraDAO _bitacoraDAO;

        public DigitoVerificadorDAO()
        {
            _acceso = Acceso.GetInstance;
            _bitacoraDAO = new BitacoraDAO();
        }

        public List<string> Verificar_DV()
        {
            try
            {
                List<string> listaMensajes = new List<string>();
                string consulta = "SELECT NombreTabla as Tabla, DV as Valor_DV from DigitoVerificador";
                DataTable dt = _acceso.GenerarConsulta(consulta);
                foreach (DataRow fila in dt.Rows)
                {
                    int DV = int.Parse(fila[1].ToString());
                    string tabla = fila[0].ToString();

                    string consulta2 = "SELECT ISNULL(SUM(DVH),0) DVH FROM " + tabla;
                    DataTable dt2 = _acceso.GenerarConsulta(consulta2);
                    int DVH = int.Parse(dt2.Rows[0]["DVH"].ToString());

                    if (DV != DVH)
                    {
                        listaMensajes.Add(tabla);
                        RegistrarEnBitacoraSiNoExiste(tabla);
                        continue;
                    }
                    string consulta3 = "SELECT * FROM " + tabla;
                    DataTable dt3 = _acceso.GenerarConsulta(consulta3);
                    int dvh_fila = 0;
                    foreach (DataRow fila1 in dt3.Rows)
                    {
                        foreach (DataColumn dc in dt3.Columns)
                        {
                            if (dc.ColumnName.ToString().ToUpper() != "ID")
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
                    if (dvh_fila != DV)
                    {
                        listaMensajes.Add(tabla);
                        RegistrarEnBitacoraSiNoExiste(tabla);
                        continue;
                    }

                }
                return listaMensajes;
            }
            catch
            {
                throw new Exception("ErrorVerificarDigitosVerificadores");
            }
        }

        public bool Recalcular_DV()
        {
            try
            {
                TablaCorrupta.TablasRegistradasEnBitacora.Clear();
                bool mensaje = true;
                string consulta = "SELECT NombreTabla as Tabla, DV as Valor_DV from DigitoVerificador";
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
                            if (dc.ColumnName.ToString().ToUpper() != "ID")
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
                            if (tabla == "FamiliaPatente")
                            {
                                string consulta1 = $@"UPDATE {tabla} set DVH = '{dvh_fila}' WHERE PadreId = '{dt2.Rows[x]["PadreId"].ToString()}' and HijoId = '{dt2.Rows[x]["HijoId"].ToString()}'";
                                _acceso.GenerarConsulta(consulta1);
                            }
                            else if (tabla != "FamiliaPatente")
                            {
                                string consulta1 = $@"UPDATE {tabla} set DVH = '{dvh_fila}' WHERE Id = '{dt2.Rows[x]["Id"].ToString()}'";
                                _acceso.GenerarConsulta(consulta1);
                            }
                        }
                        x = x + 1;
                        DV_tabla = DV_tabla + dvh_fila;
                    }
                    string xxconsulta = $@"UPDATE DigitoVerificador set dv = {DV_tabla} WHERE NombreTabla = '{tabla}'";
                    _acceso.GenerarConsulta(xxconsulta);

                }
                return mensaje;
            }
            catch
            {
                throw new Exception("ErrorRecalcularDigitosVerificadores");
            }
        }

        public bool CalcularDVTabla(string tablaObjetivo)
        {
            try
            {
                bool mensaje = true;

                string consulta = $"SELECT NombreTabla as Tabla, DV as Valor_DV FROM DigitoVerificador WHERE NombreTabla = '{tablaObjetivo}'";
                DataTable dt = _acceso.GenerarConsulta(consulta);

                if (dt.Rows.Count == 0)
                {
                    throw new Exception("La tabla especificada no existe en la base de datos DigitoVerificador.");
                }

                string tabla = dt.Rows[0]["Tabla"].ToString();
                int DV_tabla = 0;
                string consulta2 = $@"SELECT * FROM {tabla}";
                DataTable dt2 = _acceso.GenerarConsulta(consulta2);
                int x = 0;

                foreach (DataRow fila1 in dt2.Rows)
                {
                    int DVH_total = 0;
                    int dvh_fila = 0;

                    foreach (DataColumn dc in dt2.Columns)
                    {
                        if (dc.ColumnName.ToString().ToUpper() != "ID")
                        {
                            if (dc.ColumnName.ToString().ToUpper() != "DVH")
                            {
                                string celda = fila1[dc].ToString();
                                for (int i = 0; i < celda.Length; i++)
                                {
                                    byte[] bytes = Encoding.ASCII.GetBytes(celda.Substring(i, 1));
                                    int result = bytes[0];
                                    dvh_fila += result * (i + 1);
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
                        string consultaUpdate = "";
                        string primaryKey = "Id";
                        if (tabla == "FamiliaPatente")
                        {
                            var padreId = fila1[0].ToString();
                            var hijoId = fila1[1].ToString();
                            consultaUpdate = $@"UPDATE {tabla} SET DVH = '{dvh_fila}' WHERE PadreId = {padreId} and HijoId = {hijoId}";
                        }
                        
                        if (tabla != "FamiliaPatente")
                        {
                            consultaUpdate = $@"UPDATE {tabla} SET DVH = '{dvh_fila}' WHERE {primaryKey} = '{dt2.Rows[x][primaryKey].ToString()}'";
                        }

                        _acceso.GenerarConsulta(consultaUpdate);
                    }

                    x += 1;
                    DV_tabla += dvh_fila;
                }

                string xxconsulta = $@"UPDATE DigitoVerificador SET dv = {DV_tabla} WHERE NombreTabla = '{tabla}'";
                _acceso.GenerarConsulta(xxconsulta);

                return mensaje;
            }
            catch (Exception)
            {
                throw new Exception("ErrorCalcularDigitoVerificadorTabla");
            }
        }

        private void RegistrarEnBitacoraSiNoExiste(string tabla)
        {
            if (!TablaCorrupta.TablasRegistradasEnBitacora.Contains(tabla))
            {
                var bitacora = CrearBitacora(tabla);
                _bitacoraDAO.RegistrarBitacora(bitacora);
                TablaCorrupta.TablasRegistradasEnBitacora.Add(tabla);

                if(tabla != "Bitacora")
                    CalcularDVTabla("Bitacora");
            }
        }

        private Bitacora CrearBitacora(string tabla)
        {
            return new Bitacora()
            {
                Email = "gAWVEPDybxsYLBQsi0Mvz/BpqNmDIbk3al8RaZMYch4=",
                TipoUsuario = Puesto.WebMaster.ToString(),
                Descripcion = $"La tabla {tabla} esta corrupta",
                Fecha = DateTime.Now,
                Criticidad = Criticidad.ALTA.ToString()
            };
        }
    }
}
