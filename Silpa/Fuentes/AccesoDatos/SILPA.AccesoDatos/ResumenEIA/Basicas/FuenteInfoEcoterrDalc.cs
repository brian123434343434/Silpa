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
    public class FuenteInfoEcoterrDalc
    {
        private Configuracion objConfiguracion;

        public FuenteInfoEcoterrDalc()
        {
            objConfiguracion = new Configuracion();
        }


        public List<FuenteInfoEcoterrEntity> Listar()
        {
            objConfiguracion = new Configuracion();
            IDataReader dr;
            SqlDatabase db;
            try
            {
                db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                FuenteInfoEcoterrEntity _objDatos;
                List<FuenteInfoEcoterrEntity> listaDatos = new List<FuenteInfoEcoterrEntity>();
                DbCommand cmd = db.GetStoredProcCommand("EIP_FUENT_INFO_ECOTERR");
                dr = db.ExecuteReader(cmd);
                while (dr.Read())
                {
                    _objDatos = new FuenteInfoEcoterrEntity();
                    _objDatos["EFI_ID"] = dr["EFI_ID"].ToString();
                    _objDatos["EFI_FUENT_INFO_ECOTERR"] = dr["EFI_FUENT_INFO_ECOTERR"].ToString();
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
