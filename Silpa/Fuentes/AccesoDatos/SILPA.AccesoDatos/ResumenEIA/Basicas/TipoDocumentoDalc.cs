using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using SILPA.AccesoDatos;
using SILPA.AccesoDatos.Generico;
using System.Configuration;
using SILPA.Comun;
using System.Data.Common;
using System.Data.SqlClient;


namespace SILPA.AccesoDatos.ResumenEIA.Basicas
{
    public class TipoDocumentoDalc
    {
        private Configuracion objConfiguracion;

        public TipoDocumentoDalc()
        {
            objConfiguracion = new Configuracion();
        }

        /// <summary>
        /// Lista los tipos de contribuyente
        /// </summary>
        public List<TipoDocumentoEntity> Listar()
        {
            objConfiguracion = new Configuracion();
            IDataReader dr;
            SqlDatabase db;
            try
            {
                db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                TipoDocumentoEntity _objDatos;
                List<TipoDocumentoEntity> listaDatos = new List<TipoDocumentoEntity>();

                DbCommand cmd = db.GetStoredProcCommand("EIP_LISTA_TIPO_DOCUMENTO");                
                dr = db.ExecuteReader(cmd);
                while (dr.Read())
                {
                    _objDatos = new TipoDocumentoEntity();
                    _objDatos["ETD_ID"] = dr["ETD_ID"].ToString();
                    _objDatos["ETD_TIPO_DOCUMENTO"] = dr["ETD_TIPO_DOCUMENTO"].ToString();
                    listaDatos.Add(_objDatos);
                }
                return listaDatos;
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
            finally
            {
                dr = null;
                db = null;
            }

        }

    }
}
