using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SILPA.AccesoDatos;
using SILPA.AccesoDatos.Generico;
using System.Configuration;
using SILPA.Comun;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using SoftManagement.Log;

namespace SILPA.AccesoDatos.ImpresionesFus
{
    public class ImpresionArchivoFusDalc
    {


        public ImpresionArchivoFusDalc()
        {
            this.objConfiguracion = new Configuracion();
        }

        /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        //private Configuracion objConfiguracion = new Configuracion();
        private Configuracion objConfiguracion;

        public List<ImpresionArchivoFus> CrearArchivo(int ProcessInstance)
        {
            List<ImpresionArchivoFus> objLista = new List<ImpresionArchivoFus>();
            ImpresionArchivoFus objItemLista;

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { ProcessInstance };
            DbCommand cmd = db.GetStoredProcCommand("BPM_OBTENER_INFORMACION_FORMULARIO", parametros);            
            try
            {                
                IDataReader dr = db.ExecuteReader(cmd);
                while (dr.Read())
                {                 
                    objItemLista = new ImpresionArchivoFus();
                    objItemLista.strCampo = dr["VALORCAMPO"].ToString();
                    objItemLista.strValor = dr["VALOR"].ToString();
                    objLista.Add(objItemLista);                 
                }

                return objLista;
            }
            catch (Exception ex)
            {
                SMLog.Escribir(Severidad.Critico, "CrearArchivo FUS .... "+ex.ToString());
                return null;
            }            
        }


        /// <summary>
        /// hava:09-oct-10
        /// </summary>
        /// <param name="ProcessInstance">int: id process isntance</param>
        /// <param name="FormInstance">int: id  forminsntance</param>
        /// <returns></returns>
        public List<ImpresionArchivoFus> CrearArchivoInfoAdicional(int ProcessInstance, int FormInstance)
        {
            List<ImpresionArchivoFus> objLista = new List<ImpresionArchivoFus>();
            ImpresionArchivoFus objItemLista;

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { ProcessInstance, FormInstance };
            DbCommand cmd = db.GetStoredProcCommand("BPM_OBTENER_DATOS_FORMULARIO_INFORMACION_ADICIONAL", parametros);
            //SMLog.Escribir(Severidad.Informativo, "Fus_lectura_data -1");
            try
            {
                //SMLog.Escribir(Severidad.Informativo, "Fus_lectura_data 0");
                IDataReader dr = db.ExecuteReader(cmd);
                while (dr.Read())
                {                    
                    objItemLista = new ImpresionArchivoFus();
                    objItemLista.strCampo = dr["VALORCAMPO"].ToString();
                    objItemLista.strValor = dr["VALOR"].ToString();
                    objLista.Add(objItemLista);
                 
                }

                return objLista;
            }
            catch (Exception ex)
            {
                SMLog.Escribir(Severidad.Critico, "CrearArchivoInfoAdicional +++  " + ex.Message);
                throw new Exception(ex.Message);
            }
            finally
            {
                if (cmd != null)
                {
                    cmd = null;
                }
            }
        }


        public string NombreFormulario(int ProcessInstance)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DbCommand cmd = db.GetSqlStringCommand("SELECT DBO.F_NOMBRE_FORMULARIO(" + ProcessInstance.ToString() + ")");
            string salida = "";
            try
            {
                //SMLog.Escribir(Severidad.Informativo, "Se esta ejecutando el nombre del formulario para la instancia del proceso " + ProcessInstance.ToString()); 
                object result = db.ExecuteScalar(cmd);
                //salida = db.ExecuteScalar(cmd).ToString();

                if (result != null)
                {
                    salida = result.ToString();
                }
                
                return salida;
            }
            finally
            {
                cmd.Dispose();
                cmd = null;
                db = null;
            }
        }

        public string NombreProyecto(string NumeroVital)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DbCommand cmd = db.GetSqlStringCommand("SELECT DBO.F_NOMBRE_PROYECTO(SOL_IDPROCESSINSTANCE) as NOMBRE_PROYECTO FROM DAA_SOLICITUD WHERE SOL_NUMERO_SILPA='" + NumeroVital + "' ");
            string salida = "";
            try
            {
                //SMLog.Escribir(Severidad.Informativo, "Se esta ejecutando el nombre del formulario para la instancia del proceso " + ProcessInstance.ToString()); 
                object result = db.ExecuteScalar(cmd);
                if (result!=null) 
                {
                    salida = result.ToString();
                }
                
                return salida;
            }
            finally
            {
                cmd.Dispose();
                cmd = null;
                db = null;
            }
        }
    }
}
