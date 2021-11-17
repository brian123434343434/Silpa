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
    public class FormaOtorgamientoDalc
    {
        private Configuracion objConfiguracion;

        public FormaOtorgamientoDalc()
        {
            objConfiguracion = new Configuracion();
        }
        public List<FormaOtorgamientoIdentity> ListaFormaOtorgamiento(int claseRecursoID,bool esSalvoconducto)
        {
            try
            {
                List<FormaOtorgamientoIdentity> ListFormaOtorgamiento = new List<FormaOtorgamientoIdentity>();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SPSUN_LISTA_FORMAOTORGAMIENTO");
                db.AddInParameter(cmd, "P_CLASE_RECURSO_ID", DbType.Int32, claseRecursoID);
                db.AddInParameter(cmd, "P_SALVOCONDUCTO", DbType.Boolean, esSalvoconducto);
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        ListFormaOtorgamiento.Add(new FormaOtorgamientoIdentity { FormaOtorgamientoId = Convert.ToInt32(reader["FORMA_OTORGAMIENTO_ID"]), FormaOtorgamiento = reader["FORMA_OTORGAMIENTO"].ToString() });
                    }
                }
                return ListFormaOtorgamiento;

            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
