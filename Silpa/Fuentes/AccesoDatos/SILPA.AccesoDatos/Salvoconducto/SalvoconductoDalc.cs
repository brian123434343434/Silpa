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

namespace SILPA.AccesoDatos.Salvoconducto
{
    public class SalvoconductoDalc
    {
        /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        private Configuracion objConfiguracion;

        public SalvoconductoDalc()
        {
            objConfiguracion = new Configuracion();
        }

        /// <summary>
        /// Método que inserta los datos de un salvoconducto en la base de datos
        /// </summary>
        /// <param name="objIdentity">Objeto con los datos del salvoconducto</param>
        public void InsertarSalvoconducto(ref SalvoconductoIdentity objIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[]
                       {                            
                            
                           objIdentity.idSalvoconducto, objIdentity.NumeroSilpa,
                           objIdentity.NumeroExpediente,
                           objIdentity.IdTipoSalvoconducto,objIdentity.NumeroSalvoconducto,
                           objIdentity.NumeroSalvoconductoAnterior,objIdentity.FechaDesde,
                           objIdentity.FechaHasta, 
                           objIdentity.IdTipoRecursoFlora == 0 ? null : objIdentity.IdTipoRecursoFlora,                           
                           objIdentity.RecursoRelacionado,
                           objIdentity.Ruta                          
                        };

                DbCommand cmd = db.GetStoredProcCommand("SUN_CREAR_SALVOCONDUCTO", parametros);
                db.ExecuteNonQuery(cmd);
                objIdentity.idSalvoconducto = Int32.Parse(cmd.Parameters["@P_SAV_ID"].Value.ToString());
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }


        public DataSet ListarSalvoconducto(int _idSalvoconducto, string _numeroSalvoconducto)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { _idSalvoconducto, _numeroSalvoconducto,null,null,null,null,null };
                DbCommand cmd = db.GetStoredProcCommand("SUN_LISTAR_SALVOCONDUCTO", parametros);
                DataSet ds_datos = new DataSet();
                ds_datos = db.ExecuteDataSet(cmd);
                return (ds_datos);

            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Constructor de la clase que a su vez carga los valores para una identidad de las personas
        /// cuyo valor del identificador corresponda con la BD
        /// </summary>
        /// <param name="objIdentity.Id">Valor del identificador del salvoconducto</param>
        public void ConsultarSalvoconducto(ref SalvoconductoIdentity objIdentity)
        {

            DataSet dsResultado = ListarSalvoconducto(objIdentity.idSalvoconducto, "");

            if (dsResultado.Tables[0].Rows.Count > 0)
            {                
                objIdentity.FechaDesde = Convert.ToDateTime(dsResultado.Tables[0].Rows[0]["SAV_FECHA_DESDE"]).ToShortDateString();
                objIdentity.FechaHasta = Convert.ToDateTime(dsResultado.Tables[0].Rows[0]["SAV_FECHA_HASTA"]).ToShortDateString();             
                objIdentity.IdTipoRecursoFlora = dsResultado.Tables[0].Rows[0]["RFL_ID"].ToString() == "" ? 0 : Convert.ToInt32(dsResultado.Tables[0].Rows[0]["RFL_ID"]);
                objIdentity.IdTipoSalvoconducto = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["TSA_ID"]);                
                objIdentity.NumeroExpediente = dsResultado.Tables[0].Rows[0]["EXP_NUMERO"].ToString();
                objIdentity.NumeroSalvoconducto = dsResultado.Tables[0].Rows[0]["SAV_NUMERO"].ToString();
                objIdentity.NumeroSalvoconductoAnterior = dsResultado.Tables[0].Rows[0]["SAV_NUMERO_ANTERIOR"].ToString();
                objIdentity.NumeroSilpa = dsResultado.Tables[0].Rows[0]["SAV_NUMERO_SILPA"].ToString();
                objIdentity.TipoRecursoFlora = dsResultado.Tables[0].Rows[0]["RFL_NOMBRE"].ToString();
                objIdentity.TipoSalvoconducto = dsResultado.Tables[0].Rows[0]["TSA_NOMBRE"].ToString();                
            }

        }

        public DataSet ListarSalvoconductoDetalles(Int64 idProcessInstance)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { idProcessInstance, };
                DbCommand cmd = db.GetStoredProcCommand("BPM_OBTENER_INFORMACION_FORMULARIO", parametros);
                DataSet ds_datos = new DataSet();
                ds_datos = db.ExecuteDataSet(cmd);
                return (ds_datos);

            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet ListarSalvoconductoEspecimen(Int64 idSalvoconducto)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { idSalvoconducto};
                DbCommand cmd = db.GetStoredProcCommand("SUN_LISTAR_SALVOCONDUCTO_ESPECIMEN", parametros);
                DataSet ds_datos = new DataSet();
                ds_datos = db.ExecuteDataSet(cmd);
                return (ds_datos);

            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public DataSet ListarSalvoconductoTransporte(Int64 idSalvoconducto)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { idSalvoconducto };
                DbCommand cmd = db.GetStoredProcCommand("SUN_LISTAR_SALVOCONDUCTO_TRANSPORTE", parametros);
                DataSet ds_datos = new DataSet();
                ds_datos = db.ExecuteDataSet(cmd);
                return (ds_datos);

            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet ListarSalvoconductoRuta(Int64 idSalvoconducto)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { idSalvoconducto };
                DbCommand cmd = db.GetStoredProcCommand("SUN_LISTAR_SALVOCONDUCTO_RUTA", parametros);
                DataSet ds_datos = new DataSet();
                ds_datos = db.ExecuteDataSet(cmd);
                return (ds_datos);

            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet ListarTipoSalvoconducto()
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());                
                DbCommand cmd = db.GetStoredProcCommand("SUN_LISTAR_TIPO_SALVOCONDUCTO");
                DataSet ds_datos = new DataSet();
                ds_datos = db.ExecuteDataSet(cmd);
                return (ds_datos);

            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet ListarSalvoconducto(int _idSalvoconducto, string _numSalvoconducto, DateTime? FechaInicial, DateTime? FechaFinal, int? tipoSalv, int? tipoAA, int? idUser)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { _idSalvoconducto, _numSalvoconducto, FechaInicial, FechaFinal, tipoSalv, tipoAA, idUser };
                DbCommand cmd = db.GetStoredProcCommand("SUN_LISTAR_SALVOCONDUCTO", parametros);
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
