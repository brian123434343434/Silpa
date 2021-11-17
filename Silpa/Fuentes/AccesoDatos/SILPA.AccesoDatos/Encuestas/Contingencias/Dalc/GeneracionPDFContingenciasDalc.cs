using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SILPA.Comun;

namespace SILPA.AccesoDatos.Encuestas.Contingencias.Dalc
{
    public class GeneracionPDFContingenciasDalc
    {
        private Configuracion objConfiguracion;

        public GeneracionPDFContingenciasDalc()
        {
            objConfiguracion = new Configuracion();
        }

        /// <summary>
        /// Consulta la informacion de la solicitud de cambios menores
        /// </summary>
        /// <param name="numeroVITAL">numero vital de la solicitud</param>
        /// <returns></returns>
        public DataSet ConsultaDatosContingenciaInfoInicialNumeroVITAL(string numeroVITAL)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("ENC_CONSULTA_SOLICITUD_CONTINGENCIAS_NUMERO_VITAL");
                db.AddInParameter(cmd, "P_NUMERO_VITAL", DbType.String, numeroVITAL);
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Consulta la informacion de la solicitud de cambios menores
        /// </summary>
        /// <param name="p_intSolicitudID">Identificador de la solicitud</param>
        /// <returns></returns>
        public DataSet ConsultarDatosContingenciasInfoInicialSolicitudID(int p_intSolicitudID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("ENC_CONSULTA_SOLICITUD_CONTINGENCIAS");
                db.AddInParameter(cmd, "P_ENCSOLICITUDCONTINGENCIA_ID", DbType.Int32, p_intSolicitudID);
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
