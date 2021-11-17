using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SILPA.Comun;
using System.Data.Common;
using System.Data.SqlClient;

namespace SILPA.AccesoDatos.Generico
{
    
    /// <summary>
    /// Calse que manipula los datos del Expediente
    /// </summary>
    public class ExpedienteDalc
    {
        private Configuracion objConfiguracion;
        public ExpedienteDalc() { objConfiguracion = new Configuracion(); }

        public DataSet ListaNumeroVitalPorUsuario(int usuarioId)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = { usuarioId };
                DbCommand cmd = db.GetStoredProcCommand("GEN_NUMERO_SILPA_EXPEDIENTES_SOLICITANTE", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);

            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }

        public DataSet ListaNumeroVitalPorUsuarioyAA(int usuarioId, int id_aa)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = { usuarioId, id_aa };
                DbCommand cmd = db.GetStoredProcCommand("GEN_NUMERO_SILPA_EXPEDIENTES_SOLICITANTE_AA", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);

            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }
        public DataSet ListaNumeroExpedientePorNumeroVITAL(string numeroSilpa)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = { numeroSilpa };
                DbCommand cmd = db.GetStoredProcCommand("GEN_LISTAR_NUMERO_EXPEDIENTE_POR_NUMERO_VITAL", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);

            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }

        public DataSet ListaNumeroExpedientePorUsuario(string idApplicationUser)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = { idApplicationUser };
                DbCommand cmd = db.GetStoredProcCommand("[BAS_LISTA_EXPEDIENTES_USUARIO]", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);

            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }

    }
}
