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
    public class TipoContribuyenteDalc
    {
        private Configuracion objConfiguracion;

        public TipoContribuyenteDalc()
        {
            objConfiguracion = new Configuracion();
        }

        /// <summary>
        /// Lista los tipos de contribuyente
        /// </summary>
        public List<TipoContribuyenteEntity> Listar()
        {
            objConfiguracion = new Configuracion();
            IDataReader dr;
            SqlDatabase db;
            try
            {
                db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                TipoContribuyenteEntity _objDatos;
                List<TipoContribuyenteEntity> listaDatos = new List<TipoContribuyenteEntity>();

                DbCommand cmd = db.GetStoredProcCommand("EIP_LISTA_TIPO_CONTRIBUYENTE");                
                dr = db.ExecuteReader(cmd);
                while (dr.Read())
                {
                    _objDatos = new TipoContribuyenteEntity();
                    _objDatos["ETC_ID"] = dr["ETC_ID"].ToString();
                    _objDatos["ETC_TIPO_CONTRIBUYENTE"] = dr["ETC_TIPO_CONTRIBUYENTE"].ToString();
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
