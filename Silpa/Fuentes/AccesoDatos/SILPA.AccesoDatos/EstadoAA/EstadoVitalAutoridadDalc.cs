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
    public class EstadoVitalAutoridadDalc
    {
        public static EstadoVitalAutoridadEntity BuscarEstadoVitalPorId(int id)
        {
            Configuracion objConfiguracion = new Configuracion();
            EstadoVitalAutoridadEntity objIdentity;
            //EST_LISTA_ESTADO_AUTORIDAD
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { id };
            try
            {
                DbCommand cmd = db.GetStoredProcCommand("EST_LISTA_ESTADO_AUTORIDAD", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                objIdentity = new EstadoVitalAutoridadEntity(); 
                
                if (dsResultado.Tables[0].Rows.Count > 0)
                { 
                    foreach(DataRow r in dsResultado.Tables[0].Rows ) 
                    {
                        objIdentity.IdEstadoAutoirdad = Convert.ToInt32(r["EAA_ID"]);
                        EstadoVitalEntity estadoVital = new EstadoVitalEntity();
                        estadoVital = EstadoVitalDalc.BuscarEstadoVitalId(Convert.ToInt32(r["EST_ID"]));
                        objIdentity.EstadoAAmbiental = estadoVital;
                        AutoridadAmbientalIdentity aa = new AutoridadAmbientalIdentity();
                        aa.IdAutoridad = Convert.ToInt32(r["AUT_ID"]);
                        AutoridadAmbientalDalc  aaDalc = new AutoridadAmbientalDalc(); 
                        aaDalc.ObtenerAutoridadById(ref aa);
                        //aaDalc.ObtenerAutoridadNoIntegradaById(ref aa);
                        
                        
                        objIdentity.Autoridad = aa;
                    }
                }
                return objIdentity; 
            }
            finally
            {
                if (objConfiguracion != null)
                    objConfiguracion = null;

            }
        }
    }
}

