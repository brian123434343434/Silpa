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

namespace SILPA.AccesoDatos.ParametrizacionUrlSolicitudAutAmbDalc
{
    public class ParametrizacionUrlSolicitudAutAmbDalc
    {
        private Configuracion objConfiguracion;

        public ParametrizacionUrlSolicitudAutAmbDalc()
        {
            objConfiguracion = new Configuracion();
        }

        public void ActualizarUrlSolicitudXAutAmb(int ID_AA, int ID_TRAMITE, string URL, int ID_PARTICIPANTE)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SP_INSERTAR_URL_SOLICITUD_POR_AA");
                db.AddInParameter(cmd, "@ID_AA", DbType.Int32, ID_AA);
                db.AddInParameter(cmd, "@ID_TRAMITE", DbType.Int32, ID_TRAMITE);
                db.AddInParameter(cmd, "@URL", DbType.String, URL);
                db.AddInParameter(cmd, "@ID_PARTICIPANTE", DbType.Int32, ID_PARTICIPANTE);
                db.ExecuteNonQuery(cmd);

            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public void EliminarUrlSolicitudXAutAmb(int ID_AA, int ID_TRAMITE)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SP_ELIMINAR_URL_SOLICITUD_POR_AA");
                db.AddInParameter(cmd, "@ID_AA", DbType.Int32, ID_AA);
                db.AddInParameter(cmd, "@ID_TRAMITE", DbType.Int32, ID_TRAMITE);
                db.ExecuteNonQuery(cmd);

            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public DataSet ConsultarUrlSolicitudXAutAmb(int ID_AA, int ID_TRAMITE)
        {
            DataSet ds_datos = new DataSet();
            try
            {
                object[] parametros = new object[] {ID_AA, ID_TRAMITE}; ;
                string ProcedimientoAlmacenado = string.Empty;
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SP_CONSULTAR_URL_SOLICITUD_POR_AA", parametros);
                ds_datos = db.ExecuteDataSet(cmd);
                return (ds_datos);

            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
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

    }

}
