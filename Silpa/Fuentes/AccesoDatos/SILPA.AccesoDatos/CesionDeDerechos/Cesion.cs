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
namespace SILPA.AccesoDatos.CesionDeDerechos
{
    /// <summary>
    /// Clase de acceso a datos que permite la ejecucion del proceso de 
    /// Cesion de derechos a partir de el Numeri silpa o vital, la identificacion del
    /// usuario antoguio y la identificacion del nuevo usuario
    /// </summary>
    public class Cesion
    {
        /// <summary>
        /// Objeto configuracion: con la capacidad de conectarse a la configuracion
        /// consignada en el archivo de configuración de la aplicacion.
        /// </summary>
        private Configuracion objConfiguracion;
        
        /// <summary>
        /// Constructor por default
        /// </summary>
        public Cesion()
        {
            objConfiguracion = new Configuracion(); 
        }

        /// <summary>
        /// Procedimiento con la capacidad de invocar el SP de cesion_derechos
        /// </summary>
        /// <param name="vital">Numero SILPA o VITAL (Varchar)</param>
        /// <param name="usuarioQuitar">Identificacion de la persona a la cual se va a quitar del proceso </param>
        /// <param name="usuarioNuevo">Identificacion de la persona a la cual se va a agregar con cesionario del proceso</param>
        public void EjecutarCesion(string vital, string  usuarioQuitar, string usuarioNuevo)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DbCommand cmd;
            object[] obj = new object[]{vital, usuarioQuitar, usuarioNuevo};
            cmd = db.GetStoredProcCommand("CESION_DERECHOS", obj);
            try
            {
                DataSet dsResultado = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al Ejecutar Cesión.";
                throw new Exception(strException, ex);
            }
            finally 
            {
                if (cmd != null)
                    cmd.Dispose();
                cmd = null;
                db = null;
            }
        }

        public string ConsultarIdPorSilpa(string numeroSilpa)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DbCommand cmd;
            object[] obj = new object[] { numeroSilpa };
            cmd = db.GetStoredProcCommand("IDENTIFICACION_SILA", obj);
            try
            {
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return dsResultado.Tables[0].Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al consultar IdUsuario por VITAL.";
                throw new Exception(strException, ex);
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                cmd = null;
                db = null;
            }

        }

        
        /// <summary>
        /// HAVA:02-SEP-2010
        /// </summary>
        /// <param name="idPersona">long: identificador del solicitante</param>
        /// <param name="idAutoridad">int: identificador de la autoridad ambiental</param>
        /// <returns>List:PersonaCesionIdentity</returns>
        public List<PersonaCesionIdentity> ListarNumeroVitalPersonaCesion(long idPersona, int idAutoridad) 
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DbCommand cmd;
            object[] parametros = new object[] { idPersona, idAutoridad };
            cmd = db.GetStoredProcCommand("LISTAR_NUMERO_VITAL_PERSONA_CESION", parametros);
            try
            {
                DataSet dsResultado = db.ExecuteDataSet(cmd);

                List<PersonaCesionIdentity> resultList =  new List<PersonaCesionIdentity>();

                foreach (DataRow dr in dsResultado.Tables[0].Rows)
                {
                    PersonaCesionIdentity objPerCes =  new PersonaCesionIdentity();
                    objPerCes.IdPersona  = Convert.ToInt64(dr["PER_ID"]);
                    objPerCes.NumeroVital = dr["SOL_NUMERO_SILPA"].ToString();;
                    objPerCes.NumeroIdentificacion = dr["PER_NUMERO_IDENTIFICACION"].ToString();
                    resultList.Add(objPerCes);
                }

                return resultList;

            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                cmd = null;
                db = null;
            }

        }



    }
}
