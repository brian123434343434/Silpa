using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SILPA.Comun;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Salvoconducto
{
    public class PuntoControlDalc
    {
        private Configuracion objConfiguracion;

        public PuntoControlDalc()
        {
            objConfiguracion = new Configuracion();
        }
        /// <summary>
        /// Retorna la lista de los puntos de control registrados para el salvoconducto
        /// </summary>
        /// <param name="pSalvoconductoID">SalvoconductoID</param>
        /// <returns></returns>
        public List<PuntoControlIdentity> ListaPuntosControl(int pSalvoconductoID)
        {
            try
            {
                List<PuntoControlIdentity> LstPuntos = new List<PuntoControlIdentity>();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SPSUN_LISTA_PUNTO_CONTROL");
                db.AddInParameter(cmd, "P_SALVOCONDUCTO_ID", DbType.Int32, pSalvoconductoID);
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        LstPuntos.Add(new PuntoControlIdentity
                        {
                            PuntoControlID = Convert.ToInt32(reader["PUNTO_CONTROL_ID"]),
                            LogID = Convert.ToInt32(reader["LOG_ID"]),
                            Latitud = Convert.ToDecimal(reader["LATITUD"]),
                            Longitud = Convert.ToDecimal(reader["LONGITUD"]),
                            Orden = Convert.ToInt32(reader["ORDEN"]),
                            FechaRegistro = Convert.ToDateTime(reader["FECHA_REGISTRO"]).ToString("dd/MM/yyyy HH:mm:ss"),
                            Nombre = reader["NOMBRE_REVISOR"].ToString(),
                            Identificacion = reader["IDENTIFICACION_REVISOR"].ToString(),
                            Autoridad = reader["AUTORIDAD"].ToString(),
                            Munpio = reader["MUNICIPIO"].ToString(),
                            Depto = reader["DEPARTAMENTO"].ToString()
                        });
                    }
                }
                return LstPuntos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void InsertarPuntoControl(int logID, decimal latitud, decimal longitud, int orden)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DbCommand cmd = db.GetStoredProcCommand("SPSUN_INSERTA_PUNTO_CONTROL");
            db.AddInParameter(cmd, "P_LOG_ID", DbType.Int32, logID);
            db.AddInParameter(cmd, "P_LATITUD", DbType.Decimal, latitud);
            db.AddInParameter(cmd, "P_LONGITUD", DbType.Decimal, longitud);
            db.AddInParameter(cmd, "P_ORDEN", DbType.Int32, orden);
            db.ExecuteNonQuery(cmd);
        }
    }
}
