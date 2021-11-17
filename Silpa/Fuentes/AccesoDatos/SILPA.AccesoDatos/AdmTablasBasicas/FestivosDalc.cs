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

namespace SILPA.AccesoDatos.AdmTablasBasicas
{
    public class FestivosDalc
    {
         private Configuracion objConfiguracion;

         public FestivosDalc()
        {
            objConfiguracion = new Configuracion();
        }
         #region "FESTIVOS"

         /// <summary>
         /// Listar la informacion de los festivos registrados en el sistema
         /// </summary>
         /// <param name=""></param>
         /// <returns></returns>
         public DataTable Listar_Festivos()
         {
             try
             {
                 SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                 DbCommand cmd = db.GetStoredProcCommand("SMB_LISTA_FESTIVOS");
                 DataSet ds_datos = db.ExecuteDataSet(cmd);
                 return (ds_datos.Tables[0]);
             }
             catch (SqlException sql)
             {
                 throw new Exception(sql.Message);
             }
         }

         /// <summary>
         /// Registro en nuevo festivo en el sistema
         /// </summary>
         /// <param name="Fecha">Fecha</param>
         public void Insertar_Festivo(DateTime Fecha)
         {
             try
             {
                 SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                 object[] parametros = new object[] { Fecha };
                 DbCommand cmd = db.GetStoredProcCommand("SMB_INSERTAR_FESTIVO", parametros);
                 db.ExecuteNonQuery(cmd);
             }
             catch (Exception sql)
             {
                 throw new Exception(sql.Message);
             }

         }

        
         /// <summary>
         /// Elimina el registro del festivo en el sistema
         /// </summary>
         /// <param name="iID">Id del Festivo</param>
         public void Eliminar_Festivo(int iID)
         {
             try
             {
                 SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                 object[] parametros = new object[] { iID };
                 DbCommand cmd = db.GetStoredProcCommand("SMB_ELIMINAR_FESTIVO", parametros);
                 db.ExecuteNonQuery(cmd);
             }
             catch (Exception sql)
             {
                 throw new Exception(sql.Message);
             }
         }
         #endregion
    }
}
