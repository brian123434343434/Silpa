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
    public class HistoriaCambioEstadoDalc
    {
        //EST_INSERT_EST_HISTORIA_CAMBIO_ESTADO
        public static void InsertarCambioHistoria(DateTime fecha, string numeroVital, string numeroExpediente, int estadoNuevo, int autoridadAmbiental)
        { 
            Configuracion objConfiguracion = new Configuracion();
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { fecha, numeroVital, numeroExpediente, estadoNuevo, autoridadAmbiental };
            try
            {
                DbCommand cmd = db.GetStoredProcCommand("EST_INSERT_EST_HISTORIA_CAMBIO_ESTADO", parametros);
                cmd.ExecuteNonQuery(); 
            }
            finally
            {
                db = null;
            }
        }

        public static List<HistoriaCambioEstadoEntity> BuscarCambioEstado(string numeroVital, int numeroExpediente)
        {
            Configuracion objConfiguracion = new Configuracion();
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DataSet dts = new DataSet(); 
            object[] parametros = new object[] { numeroVital, numeroExpediente};
            List<HistoriaCambioEstadoEntity> lista = new List<HistoriaCambioEstadoEntity>();
            try
            {
                DbCommand cmd = db.GetStoredProcCommand("EST_LISTAR_EST_HISTORIA_CAMBIO_ESTADO", parametros);
                dts = db.ExecuteDataSet(cmd);
                foreach (DataRow r in dts.Tables[0].Rows)
                {
                    HistoriaCambioEstadoEntity his = new HistoriaCambioEstadoEntity();
                    his.IdHistoriaCambio =  Convert.ToInt32(r["ECE_ID"]);
                    his.FechaRegistro = Convert.ToDateTime(r["ECE_FECHA_REGISTRO"]);
                    his.NumeroVital = r["ECE_NUMERO_VITAL"].ToString(); 
                    his.ValorExpediente =  Convert.ToInt32(r["ECE_NUMERO_EXPEDIENTE"]);
                    his.EstadoNuevo = EstadoVitalDalc.BuscarEstadoVitalId(Convert.ToInt32(r["ECE_ESTADO_NUEVO"]));
                    AutoridadAmbientalIdentity ent = new AutoridadAmbientalIdentity();
                    AutoridadAmbientalDalc entDalc = new AutoridadAmbientalDalc();
                    ent.IdAutoridad = Convert.ToInt32(r["AUT_ID"]);
                    
                    entDalc.ObtenerAutoridadById(ref ent);
                    //entDalc.ObtenerAutoridadNoIntegradaById(ref ent);
                    his.Autoridad = ent;

                    lista.Add(his);  
                }
                return lista;
            }
            finally
            {
                db = null;
            }
            
        }
    }
}
