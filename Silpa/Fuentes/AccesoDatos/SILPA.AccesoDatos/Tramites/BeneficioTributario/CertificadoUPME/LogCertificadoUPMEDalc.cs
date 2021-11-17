using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SILPA.Comun;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Tramites.BeneficioTributario.CertificadoUPME
{
    
    public class LogCertificadoUPMEDalc
    {
        private Configuracion objConfiguracion;

        public LogCertificadoUPMEDalc()
        {
            objConfiguracion = new Configuracion();
        }

        public void InsertarLog(string strMetodoServicio, string strDatosEnviados, string strResultado)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SP_FOREST_INSERTA_LOG_WS");
                db.AddInParameter(cmd, "P_NOMBRE_METODO_SERVICIO", DbType.String, strMetodoServicio);
                db.AddInParameter(cmd, "P_DATOS_ENVIADOS", DbType.String, strDatosEnviados);
                db.AddInParameter(cmd, "P_RESULTADO", DbType.String, strResultado);
                db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
