using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data.SqlClient;

namespace SILPA.AccesoDatos.ResumenEIA.Generacion
{
    public class Procedimientos
    {
        Configuracion objConfiguracion;
        public Procedimientos()
        {
            objConfiguracion = new Configuracion();
        }

        public DataSet ProcFuentesAguasVertimientos(string evtID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { evtID };
                DbCommand cmd = db.GetStoredProcCommand("SP_EIA_EIH_FUENTES_AGUA_VERTIMIENTO", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);

            }
            catch (SqlException sqle)
            {
                return null;
            }

        }

        public DataSet ProcFuentesAguasVertimientosSuelos(string evtID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { evtID };
                DbCommand cmd = db.GetStoredProcCommand("SP_EIH_PARAM_MUEST_SUELO_VERT", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);

            }
            catch (SqlException sqle)
            {
                return null;
            }

        }

        public DataSet ProcTiposTratamientosVertimientos(string evtID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { evtID };
                DbCommand cmd = db.GetStoredProcCommand("SP_EIH_TRAT_TIPO_VERT_TIPO_TRAT", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);

            }
            catch (SqlException sqle)
            {
                return null;
            }

        }


        public string ProcExisteRelaciionTipoTratamiento(string edvId, object etvId)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { edvId, etvId };
                DbCommand cmd = db.GetStoredProcCommand("SP_EIH_TRAT_TIPO_VERT_TIPO_TRAT_VAL", parametros);
                string strResultado = db.ExecuteScalar(cmd).ToString();
                return (strResultado);

            }
            catch (SqlException sqle)
            {
                return null;
            }
        }

        public DataSet ProcCalidadFisicoquimicas(string evtID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { evtID };
                DbCommand cmd = db.GetStoredProcCommand("SP_EIH_CALIDAD_ESP_VERTIMIENTO", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);

            }
            catch (SqlException sqle)
            {
                return null;
            }

        }

        public DataSet ProcCalidadFuentesSuperf(string eipID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { eipID };
                DbCommand cmd = db.GetStoredProcCommand("SP_EIH_CALIDAD_FUENTES_AGUA_SUPRF", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);

            }
            catch (SqlException sqle)
            {
                return null;
            }

        }
        public DataSet ProcCalidadFuentesSubt(string eipID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { eipID };
                DbCommand cmd = db.GetStoredProcCommand("SP_EIH_CALIDAD_FUENTES_AGUA_SUBT", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);

            }
            catch (SqlException sqle)
            {
                return null;
            }

        }

        public DataSet ProcCalidadSitioAire(string eipID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { eipID };
                DbCommand cmd = db.GetStoredProcCommand("SP_EIH_SITIOS_MONIT_CALIDAD_AIRE", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);
            }
            catch (SqlException sqle)
            {
                return null;
            }

        }

        public DataSet ProcCalidadSitioRuido(string eipID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { eipID };
                DbCommand cmd = db.GetStoredProcCommand("SP_EIH_SITIOS_MONIT_RUIDO_AMB", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);
            }
            catch (SqlException sqle)
            {
                return null;
            }

        }
        

        
    }
}
