using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;

namespace SILPA.AccesoDatos.Generico
{
    public class PseDalc
    {
        private Configuracion _objConfiguracion;

        public PseDalc()
        {
            _objConfiguracion = new Configuracion();
        }

        public void ObtenerPseXAutAmbiental(ref PseIdentity objPse)
        {

            SqlDatabase db = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { objPse.Id_aut };
            DbCommand cmd = db.GetStoredProcCommand("PSE_LST_PSE_AUT", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);

            if (dsResultado.Tables[0].Rows.Count > 0)
            {
                objPse.Id = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["PSE_ID"].ToString());
                objPse.Id_aut = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["PSE_AA_ID"].ToString());
                objPse.Certificate_sub = dsResultado.Tables[0].Rows[0]["PSE_CERTIFICATE_SUB"].ToString();
                objPse.Url = dsResultado.Tables[0].Rows[0]["PSE_URL"].ToString();
                objPse.Code = dsResultado.Tables[0].Rows[0]["PSE_CODE"].ToString();
                objPse.Razon_social = dsResultado.Tables[0].Rows[0]["PSE_RAZON_SOCIAL"].ToString();
            }

        }

        public void InsertarPse(ref PseIdentity objPse)
        {

            SqlDatabase db = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { objPse.Id_aut, objPse.Certificate_sub, objPse.Code, objPse.Url, objPse.Razon_social };
            DbCommand cmd = db.GetStoredProcCommand("PSE_ADD_PSE_AUT", parametros);
            db.ExecuteNonQuery(cmd);
        }
    }
}
