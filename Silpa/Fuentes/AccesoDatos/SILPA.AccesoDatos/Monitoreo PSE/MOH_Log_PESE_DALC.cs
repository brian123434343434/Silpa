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

namespace SILPA.AccesoDatos.Monitoreo_PSE
{
    public class MOH_Log_PESE_DALC
    {
          private Configuracion objConfiguracion;

          public MOH_Log_PESE_DALC()
        {
            objConfiguracion = new Configuracion();
        }


        public DataTable ConsultaLogDALC(Int32 iNumTransaccion, string sFechaIniTransaccion,
                                          string sFechaFinTransaccion, string sCodBanco, int iAutoridadAmbiental,
                                          string sNumSilpa, string sNumExpediente, string sNumReferencia, 
                                          string sFechaIniExpedicion, string sFechaFinExpedicion,
                                          string sFechaIniVencimiento, string sFechaFinVencimiento,
                                          Int32 iCandidato,Int32 sValor)
        {
           try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                object[] parametros = new object[] { iNumTransaccion, sFechaIniTransaccion, sFechaFinTransaccion, 
                                                     sCodBanco, iAutoridadAmbiental, sNumSilpa, sNumExpediente,
                                                     sNumReferencia, sFechaIniExpedicion, sFechaFinExpedicion,
                                                     sFechaIniVencimiento, sFechaFinVencimiento, iCandidato, sValor};
                
                DbCommand cmd = db.GetStoredProcCommand("MON_LISTA_PSE", parametros);

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
