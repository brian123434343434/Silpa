using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.Comun;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace SILPA.AccesoDatos.Aprovechamiento
{
    public class TipoAprovechamientoDalc
    {
        private Configuracion objConfiguracion;

        public TipoAprovechamientoDalc()
        {
            objConfiguracion = new Configuracion();
        }

        public List<TipoAprovechamientoIdentity> ListaTipoDocumento()
        {
            try
            {
                List<TipoAprovechamientoIdentity> LstTipoAprovechamientoIdentity = new List<TipoAprovechamientoIdentity>();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SSP_LISTAR_TIPO_DOCUMENTO");
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        LstTipoAprovechamientoIdentity.Add(new TipoAprovechamientoIdentity() { TipoAprovechamientoID = Convert.ToInt32(reader["TIPO_APROVECHAMIENTO_ID"]), TipoAprovechamiento = reader["TIPO_APROVECHAMIENTO"].ToString() });
                    }
                }
                return LstTipoAprovechamientoIdentity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public List<TipoAprovechamientoIdentity> ListaTipoAprovechamiento()
        {
            try
            {
                List<TipoAprovechamientoIdentity> LstTipoAprovechamientoIdentity = new List<TipoAprovechamientoIdentity>();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SSP_LISTAR_TIPO_APROVECHAMIENTO");
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        LstTipoAprovechamientoIdentity.Add(new TipoAprovechamientoIdentity() { TipoAprovechamientoID = Convert.ToInt32(reader["TIPO_APROVECHAMIENTO_ID"]), TipoAprovechamiento = reader["TIPO_APROVECHAMIENTO"].ToString() });
                    }
                }
                return LstTipoAprovechamientoIdentity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
