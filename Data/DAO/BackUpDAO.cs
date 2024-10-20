using Data.Conexion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace Data.DAO
{
    public class BackUpDAO
    {
        private readonly Acceso _acceso;

        public BackUpDAO()
        {
            _acceso = Acceso.GetInstance;
        }

        #region SCRIPT INICIAL DE BASE DE DATOS
        private const string CREAR_BASE_DE_DATOS = "";
        #endregion

        public string RealizarBackup(string ruta, string nombre)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@ruta", ruta },
                    { "@nombre", nombre }
                };

                _acceso.ExecuteStoredProcedureReader("sp_RealizarBackup", parameters);

                return "La copia se ha creado correctamente en la ruta en " + ruta + ".";
            }
            catch
            {
                throw new Exception("Error al realizar la copia.");
            }
        }

        public string RealizarRestore(string ruta)
        {
            try
            {
                _acceso.SelectCommandText = $@"USE[master]; ALTER DATABASE[HrHub] SET SINGLE_USER WITH ROLLBACK IMMEDIATE USE MASTER RESTORE DATABASE[HrHub] FROM DISK = '{ruta}' WITH REPLACE ALTER DATABASE [HrHub] SET MULTI_USER";
                _acceso.ExecuteNonReader();

                return "Se restauró el sistema de manera correcta. Debes volver a iniciar sesion";
            }
            catch
            {
                throw new Exception("Error al realizar el restore.");
            }
        }

        public bool CrearBaseDeDatos(string server, string nombreBase)
        {
            try
            {
                bool existeBD = true;
                existeBD = _acceso.VerificarExistenciaBaseDeDatos(server, nombreBase);

                if (existeBD == false)
                {
                    string script = CREAR_BASE_DE_DATOS;
                    IEnumerable<string> query = FormatearQueryScript(script);
                    _acceso.ExecuteNonQueryCreateDB(server, query);
                }
                return existeBD;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private IEnumerable<string> FormatearQueryScript(string script)
        {
            script = Regex.Replace(script, @"(\r\n|\n\r|\n|\r)", "\n");

            string[] condiciones = Regex.Split(
                    script,
                    @"^[\t ]*GO[\t ]*\d*[\t ]*(?:--.*)?$",
                    RegexOptions.Multiline |
                    RegexOptions.IgnorePatternWhitespace |
                    RegexOptions.IgnoreCase);

            return condiciones
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => x.Trim(' ', '\n'));
        }
    }
}