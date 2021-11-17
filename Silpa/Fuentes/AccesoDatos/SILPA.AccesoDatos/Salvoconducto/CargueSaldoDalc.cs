using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SILPA.Comun;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Salvoconducto
{
    public class CargueSaldoDalc
    {
        private Configuracion objConfiguracion;

        public CargueSaldoDalc()
        {
            objConfiguracion = new Configuracion();
        }

        public CargueSaldoIdentity ConsultaCargueSaldoTarea(int TareaID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SPSUN_CONSULTA_CARGUE_SALDO");
                db.AddInParameter(cmd, "P_TAR_ID", DbType.Int32, TareaID);
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    if (reader.Read())
                    {
                        return new CargueSaldoIdentity { TareaID = Convert.ToInt32(reader["TAR_ID"]), CargueID = Convert.ToInt32(reader["CARGUE_ID"]), TipoSaldoID = Convert.ToInt32(reader["TIPO_SALDO_ID"]) };
                    }
                }
                return null;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void InsertaCargueSaldoTarea(CargueSaldoIdentity vCargueSaldoIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SPSUN_INSERTA_CARGUE_SALDO");
                db.AddInParameter(cmd, "P_TAR_ID", DbType.Int32, vCargueSaldoIdentity.TareaID);
                db.AddInParameter(cmd, "P_TIPO_SALDO_ID", DbType.Int32, vCargueSaldoIdentity.TipoSaldoID);
                db.AddInParameter(cmd, "P_CARGUE_ID", DbType.Int32, vCargueSaldoIdentity.CargueID);
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
