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
    public class RutaDalc
    {
        private Configuracion objConfiguracion;

        public RutaDalc()
        {
            objConfiguracion = new Configuracion();
        }

        public int InsertarOrigenDestinoSalvoconducto(RutaEntity pRutaOrigen, RutaEntity pRutaDestino, int pSalvoconductoID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SPSUN_INSERTA_RUTA");
                db.AddOutParameter(cmd, "P_RUTA_ID", DbType.Int32, 10);
                db.AddInParameter(cmd, "P_DEPTO_ORIGEN_ID", DbType.Int32, pRutaOrigen.DepartamentoID);
                db.AddInParameter(cmd, "P_MUNICIPIO_ORIGEN_ID", DbType.Int32, pRutaOrigen.MunicipioID);
                db.AddInParameter(cmd, "P_CORREGIMIENTO_ORIGEN", DbType.String, pRutaOrigen.Corregimiento);
                db.AddInParameter(cmd, "P_VEREDA_ORIGEN", DbType.String, pRutaOrigen.Vereda);
                db.AddInParameter(cmd, "P_DEPTO_DESTINO_ID", DbType.Int32, pRutaDestino.DepartamentoID);
                db.AddInParameter(cmd, "P_MUNICIPIO_DESTINO_ID", DbType.Int32, pRutaDestino.MunicipioID);
                db.AddInParameter(cmd, "P_SALVOCONDUCTO_ID", DbType.Int32, pSalvoconductoID);
                db.AddInParameter(cmd, "P_BARRIO_ORIGEN", DbType.String, pRutaOrigen.Barrio);
                db.AddInParameter(cmd, "P_BARRIO_DESTINO", DbType.String, pRutaDestino.Barrio);
                db.AddInParameter(cmd, "P_TIPO_RUTA_ID", DbType.Int32, pRutaOrigen.TipoRutaID);
                db.ExecuteNonQuery(cmd);
                return Convert.ToInt32(db.GetParameterValue(cmd, "@P_RUTA_ID"));
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void InsertarRutaDesplazamientoSalconducto(RutaEntity pRuta)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SPSUN_INSERTA_RUTA_DESPLAZAMIENTO");
                db.AddInParameter(cmd, "P_RUTA_ID", DbType.Int32, pRuta.RutaID);
                db.AddInParameter(cmd, "P_DEPTO_ID", DbType.Int32, pRuta.DepartamentoID);
                db.AddInParameter(cmd, "P_MUNICIPIO_ID", DbType.Int32, pRuta.MunicipioID);
                db.AddInParameter(cmd, "P_CORREGIMIENTO", DbType.String, pRuta.Corregimiento);
                db.AddInParameter(cmd, "P_VEREDA", DbType.String, pRuta.Vereda);
                db.AddInParameter(cmd, "P_ORDEN", DbType.Int32, pRuta.Orden);
                db.AddInParameter(cmd, "P_BARRIO", DbType.String, pRuta.Barrio);
                db.AddInParameter(cmd, "P_SN_BORRAR", DbType.Boolean, pRuta.Estado);
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<RutaEntity> ListaRutaDesplazamiento(int pSalvoconductoID)
        {
            try
            {
                List<RutaEntity> lstRutaDesplazamiento = new List<RutaEntity>();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SPSUN_CONSULTA_RUTA_DESPLAZAMIENTO");
                db.AddInParameter(cmd, "P_SALVOCONDUCTO_ID", DbType.Int32, pSalvoconductoID);
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        lstRutaDesplazamiento.Add(new RutaEntity { RutaID = Convert.ToInt32(reader["RUTA_ID"]), 
                            DepartamentoID = Convert.ToInt32(reader["DEPTO_ID"]), 
                            Departamento = reader["DEP_NOMBRE"].ToString(),
                            MunicipioID = Convert.ToInt32(reader["MUNICIPIO_ID"]), 
                            Municipio = reader["MUN_NOMBRE"].ToString(),
                            Corregimiento = reader["CORREGIMIENTO"].ToString(), 
                            Vereda = reader["VEREDA"].ToString(),
                            TipoRuta = reader["TIPO_RUTA"].ToString(),
                            TipoRutaID = Convert.ToInt32(reader["TIPO_RUTA_ID"]),
                            IdOrigenDestino = Convert.ToInt32(reader["ID_ORIGEN_DEST"]),
                            Barrio = reader["BARRIO"].ToString(),
                            Orden = Convert.ToInt32(reader["ORDEN"]) });
                    }
                }
                return lstRutaDesplazamiento;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<TipoRuta> ListaTipoRuta()
        {
            try
            {
                List<TipoRuta> lstTipoRuta = new List<TipoRuta>();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SPSUN_LISTA_TIPO_RUTA");
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        lstTipoRuta.Add(new TipoRuta
                        {
                            TipoRutaDescripcion = reader["TIPO_RUTA"].ToString(),
                            TipoRutaID = Convert.ToInt32(reader["TIPO_RUTA_ID"])
                        });
                    }
                }
                return lstTipoRuta;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
