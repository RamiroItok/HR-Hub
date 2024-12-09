using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Data.Conexion
{
    public class Acceso : Conexion
    {
        #region Singleton Implementation
        private static Acceso _instancia;
        private static readonly object _lock = new object();

        private Acceso() { }

        public static Acceso GetInstance
        {
            get
            {
                if (_instancia == null)
                {
                    lock (_lock)
                    {
                        if (_instancia == null)
                        {
                            _instancia = new Acceso();
                        }
                    }
                }
                return _instancia;
            }
        }
        #endregion

        #region Propiedades
        SqlConnection connection = new SqlConnection();

        private string _SelectCommandText;
        private string _executeCommandText;
        private SqlCommand _ExecuteParameters = new SqlCommand();
        internal readonly DbProviderFactory BaseFactory = DbProviderFactories.GetFactory("System.Data.SqlClient");

        public string SelectCommandText
        {
            get { return _SelectCommandText; }
            set { _SelectCommandText = value; }
        }
        public string ExecuteCommandText
        {
            get { return _executeCommandText; }
            set { _executeCommandText = value; }
        }
        public SqlCommand ExecuteParameters
        {
            get { return _ExecuteParameters; }
            set { _ExecuteParameters = value; }
        }
        #endregion

        #region Conexión
        private void Conectar()
        {
            connection.ConnectionString = conexion;
            connection.Open();
        }
        private void Desconectar()
        {
            connection.Close();
        }
        #endregion

        #region Métodos
        public virtual DataSet ExecuteNonReader()
        {
            if (this.SelectCommandText == "")
                throw new Exception("You must provide SelectCommandText first. Review Framework documentation.");

            using (connection)
            {
                DbDataAdapter da = this.BaseFactory.CreateDataAdapter();
                da.SelectCommand = this.BaseFactory.CreateCommand();
                da.SelectCommand.CommandText = this.SelectCommandText;
                da.SelectCommand.Connection = connection;

                DataSet ds = new DataSet();
                try
                {
                    Conectar();
                    da.Fill(ds);
                }
                catch (SqlException e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    Desconectar();
                }

                return ds;
            }
        }

        public DataTable GenerarConsulta(string Consulta)
        {
            SqlDataReader dr;
            DataTable dt = new DataTable();
            Conectar();
            SqlTransaction TR;
            SqlCommand Comando = new SqlCommand(Consulta, connection);
            TR = connection.BeginTransaction();

            try
            {
                Comando.Transaction = TR;
                dr = Comando.ExecuteReader();
                dt.Load(dr);
                TR.Commit();
            }
            catch
            {
                TR.Rollback();
            }

            Desconectar();
            return dt;
        }

        public virtual DataSet ExecuteStoredProcedureReader(string storedProcedureName, Dictionary<string, object> parameters = null)
        {
            DataSet ds = new DataSet();

            Conectar();

            SqlCommand command = new SqlCommand(storedProcedureName, connection);
            command.CommandType = CommandType.StoredProcedure;

            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    command.Parameters.AddWithValue(param.Key, param.Value);
                }
            }

            try
            {
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (SqlException exc)
            {
                throw new Exception("Ocurrió un error en BD: " + exc.Message);
            }
            catch (Exception exc2)
            {
                throw new Exception("Ocurrió un Error: " + exc2.Message);
            }
            finally
            {
                Desconectar();
            }

            return ds;
        }
        #endregion
    }
}
