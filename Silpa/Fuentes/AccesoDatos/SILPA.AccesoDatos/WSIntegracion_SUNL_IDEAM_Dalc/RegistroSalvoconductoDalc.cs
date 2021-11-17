using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SILPA.AccesoDatos.Generico;
using SILPA.Comun;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.WSIntegracion_SUNL_IDEAM_Dalc
{
    public class RegistroSalvoconductoDalc
    {

        private Configuracion objConfiguracion;

        public RegistroSalvoconductoDalc()
        {
            objConfiguracion = new Configuracion();
        }

        public DataSet ObtenerIdSalvoconductos(bool sn_diario)
        {
            DataSet ds = new DataSet();
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DbCommand cmd = db.GetStoredProcCommand("SSP_CONSULTAR_ID_SUNL_IDEAM");
            db.AddInParameter(cmd, "P_SN_DIARIO", DbType.Boolean, sn_diario);
            ds = db.ExecuteDataSet(cmd);
            return ds;
        }

        public int InsertarCabeceraLogConsumoWSIDEAM(DateTime? FechaDesde, DateTime? FechaHasta, string str_tipo_reporte)
        {
            int int_LogID = 0;
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DbCommand cmd = db.GetStoredProcCommand("SP_LOG_CONSUMO_WS_IDEAM_SUNL");
            db.AddInParameter(cmd, "P_FECHA_FILTRO_DESDE", DbType.DateTime, FechaDesde);
            db.AddInParameter(cmd, "P_FECHA_FILTRO_HASTA", DbType.DateTime, FechaHasta);
            db.AddInParameter(cmd, "P_TIPO_REPORTE", DbType.String, str_tipo_reporte);
            db.AddOutParameter(cmd, "P_LOG_ID", DbType.Int32, 0);
            db.ExecuteNonQuery(cmd);
            int_LogID = Convert.ToInt32(db.GetParameterValue(cmd, "P_LOG_ID"));
            return int_LogID;
        }

        public void InsertarDetalleLogConsumoWSIDEAM(int int_LogID, int SalvoconductoID, string str_Detalle, bool Exitoso)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DbCommand cmd = db.GetStoredProcCommand("SP_LOG_CONSUMO_WS_IDEAM_SUNL_DET");
            db.AddInParameter(cmd, "P_LOG_ID", DbType.Int32, int_LogID);
            db.AddInParameter(cmd, "P_SALVOCONDUCTO_ID", DbType.Int32, SalvoconductoID);
            db.AddInParameter(cmd, "P_DETALLE", DbType.String, str_Detalle);
            db.AddInParameter(cmd, "P_SN_EXITOSO", DbType.Boolean, Exitoso);
            db.ExecuteNonQuery(cmd);
        }


    }
}
