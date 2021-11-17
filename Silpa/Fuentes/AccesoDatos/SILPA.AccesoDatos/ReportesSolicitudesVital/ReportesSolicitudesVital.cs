using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SoftManagement.Log;

namespace SILPA.AccesoDatos.ReportesSolicitudesVital
{
    public class ReportesSolicitudesVitalDalc
    {
        private Configuracion objConfiguracion;

        public ReportesSolicitudesVitalDalc()
        {
            objConfiguracion = new Configuracion();
        }

        public DataSet ListarParametrizacion(string campos, string tabla, string condicion, string orden)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SP_CONSULTA_VALORES_TABLA");
                db.AddInParameter(cmd, "@TXT_CAMPOS", DbType.String, campos);
                db.AddInParameter(cmd, "@TXT_TABLA", DbType.String, tabla);
                db.AddInParameter(cmd, "@TXT_WHERE", DbType.String, condicion);
                db.AddInParameter(cmd, "@TXT_ORDER_BY", DbType.String, orden);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet ListarSolicitudesVital(int Tramite,string NumeroVital, DateTime FechaSolicitudDesde, DateTime FechaSolicitudHasta, int IdSolicitante, int IdFormulario, int IdAutAmb, int TipoReporte, string Departmamento, string Municipio)
        {
            try
            {
                object[] parametros = new object[] { NumeroVital, FechaSolicitudDesde, FechaSolicitudHasta, IdSolicitante, IdFormulario, IdAutAmb, TipoReporte }; ;
                string ProcedimientoAlmacenado  = string.Empty;
                switch (Tramite)
                {
                    case 41:
                        parametros = new object[] { NumeroVital, FechaSolicitudDesde, FechaSolicitudHasta, IdSolicitante, IdFormulario, IdAutAmb, TipoReporte, Departmamento, Municipio }; ;
                        ProcedimientoAlmacenado = "SP_REP_REPORTE_CONTINGENCIAS";
                        break;
                    case 73:
                        ProcedimientoAlmacenado = "SP_REP_CONTING_PARCIAL_FINAL";
                        break;
                    case 74:
                        ProcedimientoAlmacenado = "SP_REP_ACTIVIDAD_RECUPERACION";
                        break;
                    default:
                        break;
                }
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand(ProcedimientoAlmacenado, parametros);
                DataSet ds_datos = new DataSet();
                ds_datos = db.ExecuteDataSet(cmd);

                return (ds_datos);

            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
