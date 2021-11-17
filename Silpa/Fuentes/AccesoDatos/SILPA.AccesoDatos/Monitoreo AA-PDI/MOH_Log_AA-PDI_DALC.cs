using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace SILPA.AccesoDatos.Monitoreo_AA_PDI
{
    public class MOH_Log_AA_PDI_DALC
    {
         private Configuracion objConfiguracion;

         public MOH_Log_AA_PDI_DALC()
        {
            objConfiguracion = new Configuracion();
        }

         public DataTable ConsultaLogDALC(string sFechaIni, string sFechaFin, string sNombreWS, string sNomMetodo, int iSeveridad, string sMensaje, string sNoVital,int iAA, int iIdPadre)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { Formatfecha(sFechaIni), Formatfecha(sFechaFin), sNombreWS, sNomMetodo, iSeveridad, sMensaje, sNoVital, iAA, iIdPadre };
                DbCommand cmd = db.GetStoredProcCommand("SMH_CONSULTA_LOG_AA", parametros);

                cmd.CommandTimeout = 3000;
                DataSet ds_datos = db.ExecuteDataSet(cmd);
                return (ds_datos.Tables[0]);

            }
            catch (SqlException sql)
            {
                throw new Exception(sql.Message);
            }
        }

       
        private String Formatfecha(string sFecha)
        {
            if (sFecha.Trim() != "" && sFecha != null)
            {
                DateTime DTfecha = DateTime.Parse(sFecha);
                sFecha = DTfecha.Year.ToString() + (DTfecha.Month.ToString().Length == 1 ? "0" + DTfecha.Month.ToString() : DTfecha.Month.ToString()) + (DTfecha.Day.ToString().Length == 1 ? "0" + DTfecha.Day.ToString() : DTfecha.Day.ToString());
            }
            else
            {
                sFecha = "";
            }

            return sFecha;
        }

    }
}
