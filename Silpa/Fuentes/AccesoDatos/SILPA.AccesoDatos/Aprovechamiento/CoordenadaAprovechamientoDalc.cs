using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SILPA.Comun;
using System.Data;

namespace SILPA.AccesoDatos.Aprovechamiento
{
    public class CoordenadaAprovechamientoDalc
    {
        private Configuracion objConfiguracion;
        public CoordenadaAprovechamientoDalc() 
        {
            objConfiguracion = new Configuracion();
        }
        public void CrearCoordenadaAprovechamiento(CoordenadaAprovechamientoIndentity vCoordenadaAprovechamientoIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SSP_INSERTAR_COORDENADA_APROVECHAMIENTO");
                db.AddInParameter(cmd, "P_APROVECHAMIENTO_ID", DbType.Int32, vCoordenadaAprovechamientoIdentity.AprovechamientoID);
                db.AddInParameter(cmd, "P_NORTE", DbType.Double, vCoordenadaAprovechamientoIdentity.Norte);
                db.AddInParameter(cmd, "P_ESTE", DbType.Double, vCoordenadaAprovechamientoIdentity.Este);
                db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<CoordenadaAprovechamientoIndentity> ListaCoordenadasAprovechamiento(int intAprovechamientoID)
        {
            try
            {
                List<CoordenadaAprovechamientoIndentity> LsCoordenadas = new List<CoordenadaAprovechamientoIndentity>();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SSP_CONSULTAR_COORDENADAS_APROVECHAMIENTO");
                db.AddInParameter(cmd, "P_APROVECHAMIENTO_ID", DbType.Int32, intAprovechamientoID);
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        LsCoordenadas.Add(new CoordenadaAprovechamientoIndentity
                        {
                            Este = Convert.ToDouble(reader["ESTE"]),
                            Norte = Convert.ToDouble(reader["NORTE"])
                           
                        });
                    }
                }
                return LsCoordenadas;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
    }
}
