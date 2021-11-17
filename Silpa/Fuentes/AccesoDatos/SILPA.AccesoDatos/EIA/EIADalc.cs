using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Configuration;

namespace SILPA.AccesoDatos.EIA
{
    public class EIADalc
    {
        private string silpaConnection;

        /// <summary>
        /// Contructor de  la clases
        /// </summary>
        public EIADalc()
        { 
                silpaConnection = ConfigurationManager.ConnectionStrings["SILPAConnectionString"].ConnectionString;
        }

        public DataSet ConsultarFormularios(string numeroVital, int userId, DateTime? fechaDesde, DateTime? fechaHasta)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(silpaConnection);
                object[] parametros = new object[] { numeroVital, userId, fechaDesde, fechaHasta };
                DbCommand cmd = db.GetStoredProcCommand("EIA_CONSULTAR_FORMULARIO",parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return dsResultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public DataSet ConsultarListaNumeroVital(int userId)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(silpaConnection);
                object[] parametros = new object[] { userId };
                DbCommand cmd = db.GetStoredProcCommand("EIA_CONSULTAR_NUMERO_VITAL_X_USER",parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return dsResultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
