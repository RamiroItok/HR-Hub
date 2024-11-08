using Data.Conexion;
using Data.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace Data.DAO
{
    public class EmpresaDAO : IEmpresaDAO
    {
        private readonly Acceso _acceso;

        public EmpresaDAO()
        {
            _acceso = Acceso.GetInstance;
        }

        public int Registrar(Empresa empresa)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@Nombre", empresa.Nombre },
                    { "@Logo", empresa.Logo },
                    { "@URL", empresa.URL },
                    { "@DVH", 0 }
                };

                DataSet resultado = _acceso.ExecuteStoredProcedureReader("sp_i_empresa", parameters);

                return Convert.ToInt32(resultado.Tables[0].Rows[0]["Id"]);
            }
            catch (Exception)
            {
                throw new Exception("ErrorRegistrarEmpresa");
            }
        }

        public int Modificar(Empresa empresa)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@Id", empresa.Id },
                    { "@Nombre", empresa.Nombre },
                    { "@Logo", empresa.Logo },
                    { "@URL", empresa.URL }
                };

                DataSet resultado = _acceso.ExecuteStoredProcedureReader("sp_u_empresa", parameters);

                return Convert.ToInt32(resultado.Tables[0].Rows[0]["Id"]);
            }
            catch (Exception)
            {
                throw new Exception("ErrorModificarEmpresa");
            }
        }

        public void Eliminar(int idEmpresa)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@Id", idEmpresa }
                };

                _acceso.ExecuteStoredProcedureReader("sp_d_empresa", parameters);
            }
            catch (Exception)
            {
                throw new Exception("ErrorEliminarEmpresa");
            }
        }

        public DataSet ObtenerEmpresas()
        {
            try
            {
                var resultado = _acceso.ExecuteStoredProcedureReader("sp_s_empresa", null);
                return resultado;
            }
            catch (Exception)
            {
                throw new Exception("ErrorObtenerEmpresas");
            }
        }

        public DataSet ObtenerEmpresaPorId(int id)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@IdEmpresa", id }
                };

                return _acceso.ExecuteStoredProcedureReader("sp_s_empresa_porId", parameters);
            }
            catch (Exception)
            {
                throw new Exception("ErrorObtenerEmpresaPorId");
            }
        }
    }
}