using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SILPA.AccesoDatos;
using SILPA.AccesoDatos.Generico;
using System.Configuration;
using SILPA.Comun;
using System.Data.SqlClient;
using SILPA.AccesoDatos.Notificacion;


namespace SILPA.AccesoDatos.Notificacion
{
    public class TipoPersonaNotificacionDalc
    {
        private Configuracion objConfiguracion;

        public TipoPersonaNotificacionDalc()
        {
            objConfiguracion = new Configuracion();
        }

        public List<TipoPersonaEntity> ListarTiposPersona()
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            TipoPersonaEntity tipo;
            DbCommand cmd = db.GetStoredProcCommand("NOT_LISTAR_TIPO_PERSONA");
            DataSet dsResultado = db.ExecuteDataSet(cmd);

            List<TipoPersonaEntity> lista;
            try
            {
                if (dsResultado.Tables[0].Rows.Count  > 0)
                {
                    lista = new List<TipoPersonaEntity>();
                    foreach (DataRow dt in dsResultado.Tables[0].Rows)
                    {
                        tipo = new TipoPersonaEntity(); 
                        tipo.ID = Convert.ToInt32(dt["ID_TIPO_PERSONA"]);
                        tipo.Nombre = dt["NTP_NOMBRE_TIPO"].ToString();
                        tipo.Codigo = dt["NTP_CODIGO_TIPO"].ToString();
                        lista.Add(tipo);
                    }
                    return lista;
                }
                return null;
            }
            finally
            {
                dsResultado.Dispose();
                dsResultado = null;
                cmd.Dispose();
                cmd = null;
                db = null;
            }
        }

        public TipoPersonaEntity ListarTipoPersona(object[] parametros)
        {
            objConfiguracion = new Configuracion();
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            TipoPersonaEntity tipo;
            DbCommand cmd = db.GetStoredProcCommand("NOT_LISTAR_TIPO_PERSONA_R", parametros);
            DataSet dsResultado = new DataSet();

            try
            {
                dsResultado = db.ExecuteDataSet(cmd);
                if (dsResultado.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dt in dsResultado.Tables[0].Rows)
                    {
                        tipo = new TipoPersonaEntity();
                        /*tipo.ID = Convert.ToInt32(dt["ID_TIPO_PERSONA"]);
                        tipo.Nombre = dt["NTP_NOMBRE_TIPO"].ToString();
                        tipo.Codigo = dt["NTP_CODIGO_TIPO"].ToString();
                        */
                        tipo.ID = Convert.ToInt32(dt["ID_TIPO_PERSONA"]);
                        tipo.Nombre = dt["NTP_NOMBRE_TIPO"].ToString();
                        tipo.Codigo = dt["NTP_CODIGO_TIPO"].ToString();

                        return tipo;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al Listar los Tipos de Persona.";
                throw new Exception(strException, ex);
            }
            finally
            {
                dsResultado.Dispose();
                dsResultado = null;
                cmd.Dispose();
                cmd = null;
                db = null;
            }
        }
    }
}
