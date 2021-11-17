using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SILPA.Comun;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Contingencias
{
    public class ContingenciasDalc
    {
        /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        private Configuracion objConfiguracion;

        public ContingenciasDalc()
        {
            objConfiguracion = new Configuracion();
        }
        public DataTable obtenerDestinadariosNivelEmergencia(string strMunicipio, int intNivelEmergencia)
        {

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            //listamos todos los parametros
            object[] parametros = new object[] { strMunicipio, intNivelEmergencia };
            DbCommand cmd = db.GetStoredProcCommand("SSP_LISTA_DESTINATARIO_NIVEL_EMERGENCIA", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);
            try
            {

                return dsResultado.Tables[0];

            }
            finally
            {
                dsResultado.Dispose();
                dsResultado = null;
                cmd.Dispose();
                cmd = null;
                db = null;
            }
        }
    }
}
