using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;

namespace SILPA.AccesoDatos.Generico
{
    /// <summary>
    /// Clase pais
    /// </summary>
    public class ParametroDalc
    {
        /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        private Configuracion objConfiguracion;


        public ParametroDalc()
        {
            objConfiguracion = new Configuracion();
        }

        public List<ParametroEntity> obtenerParametros()
        {

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            //listamos todos los parametros
            object[] parametros = new object[] {-1};
            DbCommand cmd = db.GetStoredProcCommand("GEN_LISTAR_PARAMETRO", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);
            List<ParametroEntity> lista = new List<ParametroEntity>();

            ParametroEntity entity;
            try{

                if (dsResultado.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dt in dsResultado.Tables[0].Rows)
                    {
                        entity = new ParametroEntity();
                        entity.IdParametro = Convert.ToInt32(dt["ID_PARAMETRO"]);   
                        entity.IdTipoDato = Convert.ToInt32(dt["TIPO_DATO"]);
                        entity.NombreParametro = dt["NOMBRE_PARAMETRO"].ToString();
                        entity.Parametro = dt["PARAMETRO"].ToString();
                        lista.Add(entity);
                        
                    }
                    return lista;
                }
                return null;

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

        public void obtenerParametros(ref ParametroEntity _entity)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            //listamos todos los parametros
            object[] parametros = new object[] { _entity.IdParametro, _entity.NombreParametro };
            DbCommand cmd = db.GetStoredProcCommand("GEN_LISTAR_PARAMETROS", parametros);
            DataSet dsResultado = new DataSet();
            try
            {
                dsResultado = db.ExecuteDataSet(cmd);
                if (dsResultado.Tables[0].Rows.Count > 0)
                {
                    _entity.IdParametro = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["ID_PARAMETRO"]);
                    _entity.IdTipoDato = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["TIPO_DATO"]);
                    _entity.NombreParametro = dsResultado.Tables[0].Rows[0]["NOMBRE_PARAMETRO"].ToString();
                    _entity.Parametro = dsResultado.Tables[0].Rows[0]["PARAMETRO"].ToString(); 
                }
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al obtener Parametros.";
                throw new Exception(strException, ex);
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

        public int obtenerDiasHabilesEntre(DateTime? ini, DateTime fin)
        {

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { ini, fin};
            DbCommand cmd = db.GetStoredProcCommand("F_CONSULTAR_DIAS_HABILES", parametros);
            Object ret ;
            int numeroDias = 0; 
            try
            {
                ret = db.ExecuteScalar(cmd);
                numeroDias = Convert.ToInt32(ret);
            }
            finally
            {

                cmd.Dispose();
                cmd = null;
                db = null;
            }
            return numeroDias;
            
        }
        public void actualizarParametro(ParametroEntity _entity)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            //listamos todos los parametros
            object[] parametros = new object[] { _entity.IdParametro, _entity.IdTipoDato, _entity.Parametro, 'S' };
            DbCommand cmd = db.GetStoredProcCommand("GEN_UPDATE_PARAMETRO", parametros);
            try
            {
                db.ExecuteNonQuery(cmd);
            }
            finally
            {
                cmd.Dispose();
                cmd = null;
                db = null;
            }
        }
    }
}