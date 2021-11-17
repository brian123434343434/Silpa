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
    public class TipoPtoAguaDalc
    {
        private Configuracion objConfiguracion;

        public TipoPtoAguaDalc()
        {
            objConfiguracion = new Configuracion();
        }
        public List<TipoPtoAguaEntity> Listar()
        {
            objConfiguracion = new Configuracion();
            IDataReader dr;
            SqlDatabase db;
            try
            {
                db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                TipoPtoAguaEntity _objDatos;
                List<TipoPtoAguaEntity> listaDatos = new List<TipoPtoAguaEntity>();
                DbCommand cmd = db.GetStoredProcCommand("EIP_LISTA_PTO_AGUA");
                dr = db.ExecuteReader(cmd);
                while (dr.Read())
                {
                    _objDatos = new TipoPtoAguaEntity();
                    _objDatos["ETP_ID"] = dr["ETP_ID"].ToString();
                    _objDatos["ETP_TIPO_PTO_AGUA"] = dr["ETP_TIPO_PTO_AGUA"].ToString();
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
