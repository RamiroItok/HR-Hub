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

        protected string SelectCommandText
        {
            get { return _SelectCommandText; }
            set { _SelectCommandText = value; }
        }
        protected string ExecuteCommandText
        {
            get { return _executeCommandText; }
            set { _executeCommandText = value; }
        }
        protected SqlCommand ExecuteParameters
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
        public virtual void ExecuteNonQuery()
        {
            Conectar();

            SqlTransaction TR = connection.BeginTransaction();
            SqlCommand command = new SqlCommand(ExecuteCommandText, connection, TR);

            command.CommandType = CommandType.Text;
            command.Parameters.Clear();

            foreach (SqlParameter p in ExecuteParameters.Parameters)
            {
                command.Parameters.AddWithValue(p.ParameterName, p.SqlValue);
            }

            try
            {
                command.ExecuteNonQuery();
                TR.Commit();
            }
            catch (SqlException exc)
            {
                TR.Rollback();
                throw new Exception("Ocurrió un error en BD: " + exc.Message);
            }
            catch (Exception exc2)
            {
                TR.Rollback();
                throw new Exception("Ocurrió un Error: " + exc2.Message);
            }
            finally
            {
                Desconectar();
            }
        }

        public virtual int ExecuteNonEscalar()
        {
            Conectar();
            SqlTransaction transaction = connection.BeginTransaction();
            SqlCommand command = new SqlCommand(ExecuteCommandText, connection, transaction);

            command.CommandType = CommandType.Text;
            command.Parameters.Clear();

            foreach (SqlParameter p in ExecuteParameters.Parameters)
            {
                command.Parameters.AddWithValue(p.ParameterName, p.SqlValue);
            }

            SqlParameter sp_return = new SqlParameter();
            sp_return.Direction = ParameterDirection.ReturnValue;
            command.Parameters.Add(sp_return);

            int outputId = 0;

            try
            {
                outputId = (int)command.ExecuteScalar();
                transaction.Commit();
            }
            catch (SqlException exc)
            {
                transaction.Rollback();
                throw new Exception("Ocurrió un error en BD: " + exc.Message);
            }
            catch (Exception exc2)
            {
                transaction.Rollback();
                throw new Exception("Ocurrió un error: " + exc2.Message);
            }
            finally
            {
                Desconectar();
            }

            return outputId;
        }

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

        // TO REVIEW
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
            catch (Exception Ex)
            {
                TR.Rollback();
            }

            Desconectar();
            return dt;
        }

        public virtual bool VerificarExistenciaBaseDeDatos(string server, string nombreBase)
        {
            string query;
            bool existeBD = false;

            try
            {
                connection = new SqlConnection($"server={server};Trusted_Connection=yes");
                query = string.Format("SELECT database_id FROM sys.databases WHERE Name = '{0}'", nombreBase);

                using (connection)
                {
                    using (SqlCommand sqlCmd = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        object resultObj = sqlCmd.ExecuteScalar();

                        int databaseID = 0;

                        if (resultObj != null)
                        {
                            int.TryParse(resultObj.ToString(), out databaseID);
                        }

                        connection.Close();

                        existeBD = (databaseID > 0);
                    }
                }
            }
            catch (Exception)
            {
                existeBD = false;
            }

            return existeBD;
        }

        public virtual void ExecuteNonQueryCreateDB(string server, IEnumerable<string> script)
        {
            try
            {
                connection = new SqlConnection($"server={server};Trusted_Connection=yes");

                using (connection)
                {
                    connection.Open();
                    foreach (var query in script)
                    {
                        using (SqlCommand sqlCmd = new SqlCommand(query, connection))
                        {
                            sqlCmd.ExecuteNonQuery();
                        }
                    }
                }
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
                connection.Close();
            }
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
