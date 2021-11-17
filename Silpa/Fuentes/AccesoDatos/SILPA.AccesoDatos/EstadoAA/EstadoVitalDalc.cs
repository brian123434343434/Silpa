using System;
using System.IO;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SILPA.AccesoDatos;
using SILPA.AccesoDatos.Generico;
using System.Configuration;
using SILPA.Comun;
using System.Data.SqlClient;
namespace SILPA.AccesoDatos.EstadoAA
{
    public class EstadoVitalDalc
    {
        public static EstadoVitalEntity BuscarEstadoVitalId(int id)
        {
            Configuracion objConfiguracion = new Configuracion();
            EstadoVitalEntity objIdentity = null;
            //EST_LISTA_ESTADO_AUTORIDAD
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { id };
            try
            {
                DbCommand cmd = db.GetStoredProcCommand("EST_LISTAR_ESTADO_VITAL", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                foreach (DataRow r in dsResultado.Tables[0].Rows)
                {
                    objIdentity = new EstadoVitalEntity();

                    objIdentity.EstId = Convert.ToInt32(r["EST_ID"]);
                    objIdentity.EstNombre = r["EST_NOMBRE"].ToString();
                    objIdentity.EstActivo = Convert.ToInt32(r["EST_ACTIVO"]);
                    //return objIdentity;
                }
                return objIdentity;
            }
            
            finally
            {
                db = null;
            }
        }
    }
}
