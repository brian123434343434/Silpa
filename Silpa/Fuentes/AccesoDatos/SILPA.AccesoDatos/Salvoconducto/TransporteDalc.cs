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
    public class TransporteDalc
    {
        private Configuracion objConfiguracion;

        public TransporteDalc()
        {
            objConfiguracion = new Configuracion();
        }
        public void InsertarTransporteSalvoconducto(TransporteNewIdentity pTransporteNewIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SPSUN_INSERTA_TRANSPORTE");
                db.AddInParameter(cmd, "P_SALVOCONDUCTO_ID", DbType.Int32, pTransporteNewIdentity.SalvoconductoID);
                db.AddInParameter(cmd, "P_TIPO_TRANSPORTE_ID", DbType.Int32, pTransporteNewIdentity.TipoTransporteID);
                db.AddInParameter(cmd, "P_MODO_TRANSPORTE_ID", DbType.Int32, pTransporteNewIdentity.ModoTransporteID);
                db.AddInParameter(cmd, "P_EMPRESA", DbType.String, pTransporteNewIdentity.Empresa);
                db.AddInParameter(cmd, "P_NUMERO_IDENTIFICACION", DbType.String, pTransporteNewIdentity.NumeroIdentificacionMedioTransporte);
                db.AddInParameter(cmd, "P_NOMBRE_TRANSPORTADOR", DbType.String, pTransporteNewIdentity.NombreTransportador);
                db.AddInParameter(cmd, "P_TIPO_IDENTIFICACION_TRANSPORTADOR_ID", DbType.Int32, pTransporteNewIdentity.TipoIdentificacionTransportadorID);
                db.AddInParameter(cmd, "P_NUMERO_IDENTIFICACION_TRANSPORTADOR", DbType.String, pTransporteNewIdentity.NumeroIdentificacionTransportador);
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public TransporteNewIdentity ConsultaTransporte(int pSalvoconductoID)
        {
            try
            {
                TransporteNewIdentity vTransporteNewIdentity = new TransporteNewIdentity();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SPSUN_CONSULTA_TRANSPORTE");
                db.AddInParameter(cmd, "P_SALVOCONDUCTO_ID", DbType.Int32, pSalvoconductoID);
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    if (reader.Read())
                    {
                        vTransporteNewIdentity.SalvoconductoID = Convert.ToInt32(reader["SALVOCONDUCTO_ID"]);
                        vTransporteNewIdentity.TipoTransporteID = Convert.ToInt32(reader["TIPO_TRANSPORTE_ID"]);
                        vTransporteNewIdentity.TipoTransporte = reader["TIPO_TRANSPORTE"].ToString();
                        vTransporteNewIdentity.ModoTransporteID = Convert.ToInt32(reader["MODO_TRANSPORTE_ID"]);
                        vTransporteNewIdentity.ModoTransporte = reader["MODO_TRANSPORTE"].ToString();
                        vTransporteNewIdentity.Empresa = reader["EMPRESA"].ToString();
                        vTransporteNewIdentity.NumeroIdentificacionMedioTransporte = reader["NUMERO_IDENTIFICACION"].ToString();
                        vTransporteNewIdentity.NombreTransportador = reader["NOMBRE_TRANSPORTADOR"].ToString();
                        vTransporteNewIdentity.TipoIdentificacionTransportadorID = Convert.ToInt32(reader["TIPO_IDENTIFICACION_TRANSPORTADOR_ID"]);
                        vTransporteNewIdentity.TipoIdentificacionTransportador = reader["TIPO_IDENTIFICACION_TRANSPORTADOR"].ToString();
                        vTransporteNewIdentity.NumeroIdentificacionTransportador = reader["NUMERO_IDENTIFICACION_TRANSPORTADOR"].ToString();
                    }
                }
                return vTransporteNewIdentity;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<TransporteNewIdentity> ListaTransporteSalvoconducto(int pSalvoconductoID)
        {
            try
            {
                List<TransporteNewIdentity> LstTransporte = new List<TransporteNewIdentity>();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SSP_CONSULTAR_TRANSPORTE_SALVOCONDUCTO");
                db.AddInParameter(cmd, "P_SALVOCONDUCTO_ID", DbType.Int32, pSalvoconductoID);
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        LstTransporte.Add(new TransporteNewIdentity
                        {
                            TransporteSalvoconductoID = Convert.ToInt32(reader["TRANSPORTE_SALV_ID"]),
                            ModoTransporteID = Convert.ToInt32(reader["MODO_TRANSPORTE_ID"]),
                            ModoTransporte = reader["MODO_TRANSPORTE"].ToString(),
                            TipoTransporteID = Convert.ToInt32(reader["TIPO_TRANSPORTE_ID"]),
                            TipoTransporte = reader["TIPO_TRANSPORTE"].ToString(),
                            Empresa = reader["EMPRESA"].ToString(),
                            NumeroIdentificacionMedioTransporte = reader["NUMERO_IDENTIFICACION"].ToString(),
                            NombreTransportador = reader["NOMBRE_TRANSPORTADOR"].ToString(),
                            NumeroIdentificacionTransportador = reader["NUMERO_IDENTIFICACION_TRANSPORTADOR"].ToString(),
                            //jmartinez salvoconducto fase 2
                            CodigoIdeamTipoTransporte = reader["CODIGO_IDEAM_TIPO_TRANSPORTE"].ToString(),
                            TipoIdentificacionTransportadorIDEAM = reader["CODIGO_IDEAM_TIPO_IDENT_TRANSPORTADOR"].ToString(),
                            CodigoIdeamModoTransporte = reader["CODIGO_IDEAM_MODO_TRANSPORTE"].ToString()
                        });
                    }
                }
                return LstTransporte;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
