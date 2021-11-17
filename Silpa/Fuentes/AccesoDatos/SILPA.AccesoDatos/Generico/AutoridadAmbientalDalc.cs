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


namespace SILPA.AccesoDatos.Generico
{
    
    /// <summary>
    /// clase que accede a los datos de Autoridad Ambiental
    /// </summary>
    public class AutoridadAmbientalDalc
    {

        Configuracion objConfiguracion;

        public AutoridadAmbientalDalc() 
        {
            objConfiguracion = new Configuracion();
        }

        
        /// <summary>
        /// Obtiene los datos de la autoridad 
        /// </summary>
        /// <param name="objIdentity.IdAutoridad">Instancia de la clase indentity de AutoridadAmbiental, 
        /// contiene su propiedad IdAutoridad como parametro de consulta </param>
        public void ObtenerAutoridadById(ref AutoridadAmbientalIdentity objIdentity)
        {
            try
            {
                /// se hace la conexión a la base de datos SILA:
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { objIdentity.IdAutoridad };
                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_AUT_AMB", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                if (dsResultado != null)
                {
                    if (dsResultado.Tables[0].Rows.Count > 0)
                    {
                        this.CargarObjeto(ref objIdentity, dsResultado.Tables[0]);

                        if (dsResultado.Tables[0].Rows[0]["AUT_CORREO_CORRESPONDENCIA"] != DBNull.Value)
                        {
                            objIdentity.EmailCorrespondencia = dsResultado.Tables[0].Rows[0]["AUT_CORREO_CORRESPONDENCIA"].ToString();
                        }
                    }
                }

                //???
                //if (dsResultado.Tables[0].Rows[0]["AUT_SERVICIO_RADICACION"] != null) { objIdentity.ServicioRadicacion = dsResultado.Tables[0].Rows[0]["AUT_SERVICIO_RADICACION"].ToString(); }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        /// <summary>
        /// Obtiene el correo de correspondencia de la autoridad ambiental
        /// no filtra si esta o no integrada a la ventanilla.
        /// </summary>
        /// <param name="objIdentity">Instancia de la clase indentity de AutoridadAmbiental, 
        /// contiene su propiedad IdAutoridad como parametro de consulta </param>
        public void ObtenerAutoridadNoIntegradaById(ref AutoridadAmbientalIdentity objIdentity)
        {
            try
            {
                /// se hace la conexión a la base de datos SILA:
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { objIdentity.IdAutoridad };
                DbCommand cmd = db.GetStoredProcCommand("BAS_OBTENER_AUT_AMB", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                if (dsResultado != null)
                {
                    if (dsResultado.Tables[0].Rows.Count > 0)
                    {
                        this.CargarObjeto(ref objIdentity, dsResultado.Tables[0]);

                        if (dsResultado.Tables[0].Rows[0]["AUT_CORREO_CORRESPONDENCIA"] != DBNull.Value)
                        {
                            objIdentity.EmailCorrespondencia = dsResultado.Tables[0].Rows[0]["AUT_CORREO_CORRESPONDENCIA"].ToString();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objIdentity"></param>
        /// <param name="dtDatos"></param>
        public void CargarObjeto(ref AutoridadAmbientalIdentity objIdentity, DataTable dtDatos)
        {
            if (dtDatos.Rows[0]["AUT_NOMBRE"] != null) { objIdentity.Nombre = dtDatos.Rows[0]["AUT_NOMBRE"].ToString(); }
            if (dtDatos.Rows[0]["AUT_ACTIVO"] != null) { objIdentity.Activo = Convert.ToBoolean(dtDatos.Rows[0]["AUT_ACTIVO"].ToString()); }
            if (dtDatos.Rows[0]["AUT_DIRECCION"] != null) { objIdentity.Direccion = dtDatos.Rows[0]["AUT_DIRECCION"].ToString(); }
            if (dtDatos.Rows[0]["AUT_TELEFONO"] != null) { objIdentity.Telefono = dtDatos.Rows[0]["AUT_TELEFONO"].ToString(); }
            if (dtDatos.Rows[0]["AUT_FAX"] != null) { objIdentity.Fax = dtDatos.Rows[0]["AUT_FAX"].ToString(); }
            if (dtDatos.Rows[0]["AUT_CORREO"] != null) { objIdentity.Email = dtDatos.Rows[0]["AUT_CORREO"].ToString(); }
            if (dtDatos.Rows[0]["AUT_NIT"] != null) { objIdentity.NIT = dtDatos.Rows[0]["AUT_NIT"].ToString(); }
            if (dtDatos.Rows[0]["AUT_CARGUE"] != null) { objIdentity.Cargue = Convert.ToBoolean(dtDatos.Rows[0]["AUT_CARGUE"].ToString()); }
            if (dtDatos.Rows[0]["AUT_BASE"] != null) { objIdentity.Base = Convert.ToBoolean(dtDatos.Rows[0]["AUT_BASE"].ToString()); }
            if (dtDatos.Rows[0]["AUT_APLICA_RADICACION"] != null) { objIdentity.RadicacionAutomatica = Convert.ToBoolean(dtDatos.Rows[0]["AUT_APLICA_RADICACION"].ToString()); }
            if (dtDatos.Rows[0]["AUT_GS1_CODE"] != null) { objIdentity.Gs1_Code = dtDatos.Rows[0]["AUT_GS1_CODE"].ToString(); }
            if (dtDatos.Rows[0]["AUT_CORREO_CORRESPONDENCIA"] != null) { objIdentity.CorreoCorrespondencia = dtDatos.Rows[0]["AUT_CORREO_CORRESPONDENCIA"].ToString(); }
            if (dtDatos.Rows[0]["AUT_DESCRIPCION"] != null) { objIdentity.Nombre_Largo = dtDatos.Rows[0]["AUT_DESCRIPCION"].ToString();}
            //Datos adicionales de autoridad ambiental
            if (dtDatos.Rows[0]["AAE_PPE_CERTIFICATE_SUB"] != null) { objIdentity.Ppe_certificate_sub = dtDatos.Rows[0]["AAE_PPE_CERTIFICATE_SUB"].ToString(); }
            if (dtDatos.Rows[0]["AAE_PPE_CODE"] != null) { objIdentity.Ppe_code = dtDatos.Rows[0]["AAE_PPE_CODE"].ToString(); }
            if (dtDatos.Rows[0]["AAE_PPE_URL"] != null) { objIdentity.Ppe_url = dtDatos.Rows[0]["AAE_PPE_URL"].ToString(); }
            if (dtDatos.Rows[0]["AAE_RAZON_SOCIAL"] != null) { objIdentity.Razon_social = dtDatos.Rows[0]["AAE_RAZON_SOCIAL"].ToString(); }
            if (dtDatos.Rows[0]["AAE_NOMBRE_BANCO"] != null) { objIdentity.NombreBanco = dtDatos.Rows[0]["AAE_NOMBRE_BANCO"].ToString(); }
            if (dtDatos.Rows[0]["AAE_NUMERO_CUENTA"] != null) { objIdentity.NumeroCuneta = dtDatos.Rows[0]["AAE_NUMERO_CUENTA"].ToString(); }

            if (dtDatos.Rows[0]["AAE_TIPO_CUENTA"] != null) { objIdentity.TipoCuenta = dtDatos.Rows[0]["AAE_TIPO_CUENTA"].ToString(); }
            if (dtDatos.Rows[0]["AAE_NIT_TITULAR_CUENTA"] != null) { objIdentity.NitTitularCuenta = dtDatos.Rows[0]["AAE_NIT_TITULAR_CUENTA"].ToString(); }
            if (dtDatos.Rows[0]["AAE_TITULAR_CUENTA"] != null) { objIdentity.TitularCuenta = dtDatos.Rows[0]["AAE_TITULAR_CUENTA"].ToString(); }
            if (dtDatos.Rows[0]["AUT_CORREO_SALVOCONDUCTO"] != null) { objIdentity.CorreoSalvoconducto = dtDatos.Rows[0]["AUT_CORREO_SALVOCONDUCTO"].ToString(); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objIdentity"></param>
        /// <param name="dtDatos"></param>
        public void CargarExtObjeto(ref AutoridadAmbientalIdentity objIdentity, DataTable dtDatos)
        {
//            AAE_ID
//AUT_ID
//AAE_CONSECUTIVO
//AAE_ANIO
//AAE_PPE_CERTIFICATE_SUB
//AAE_PPE_URL
//AAE_PPE_CODE
//AAE_RAZON_SOCIAL

        }

        /// <summary>
        /// Lista las autoridades ambientales en la BD. Pueden listarse todas o una en particular.
        /// </summary>
        /// <param name="IntId" >Con este valor se lista las autoridades ambientales con el identificador, si es null se listan todas</param>
        /// <returns>DataSet con los registros y las siguientes columnas: AUT_ID, AUT_NOMBRE, AUT_DIRECCION, AUT_TELEFONO, AUT_FAX, 
        /// AUT_CORREO, AUT_NIT, AUT_CARGUE, APLICA_RADICACION, AUT_ACTIVO</returns>
        public DataSet ListarAutoridadAmbiental(Nullable<int> IntId)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { IntId };
                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_AUT_AMB", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);
            }
            catch (SqlException sqle)
            {
                return null;
            }
        }

        public DataSet ListarAutoridadAmbientalSalvoConducto(Nullable<int> IntId)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { IntId };
                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_AUT_AMB_SALV", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);
            }
            catch (SqlException sqle)
            {
                return null;
            }
        }

        /// <summary>
        /// Lista las autoridades ambientales que se relacionan a permisos
        /// </summary>
        /// <returns>DataSet con los registros y las siguientes columnas: AUT_ID, AUT_NOMBRE, AUT_DIRECCION, AUT_TELEFONO, AUT_FAX, 
        /// AUT_CORREO, AUT_NIT, AUT_CARGUE, APLICA_RADICACION, AUT_ACTIVO</returns>
        public DataSet ListarAutoridadAmbientalPermisos()
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_AUT_AMB_PERMISOS");
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);
            }
            catch (SqlException sqle)
            {
                return null;
            }
        }


        public DataSet ListarAutoridadAmbientalSUNL(Nullable<int> IntId)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { IntId };
                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_AUT_AMB_SUNL", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);
            }
            catch (SqlException sqle)
            {
                return null;
            }
        }

        public DataSet ListarAutoridadAmbientalRegistroMinero(Nullable<int> IntId)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { IntId };
                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_AUT_AMB_REGISTRO_MINERO", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);
            }
            catch (SqlException sqle)
            {
                return null;
            }
        }

        

        /// <summary>
        /// Metodo que lista las autoridades ambientales activas
        /// </summary>
        /// <returns></returns>
        public DataSet ListarAutoridadAmbientalActiva()
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_AUT_AMB_ACT");
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);
            }
            catch (SqlException sqle)
            {
                return null;
            }
        }

        /// <summary>
        /// Lista las autoridades ambientales en la BD con jurisdiccion en la ubicacion definida en el proceso
        /// </summary>
        /// <param name="idProcessInstance">Identificador de la instacia del proceso</param>
        /// <returns>AUT_ID, AUT_NOMBRE</returns>
        public DataSet ListarAAXUbicacion(Int64 idProcessInstance)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { idProcessInstance };
                //archivo("Cadena de conexion " + objConfiguracion.SilpaCnx.ToString());  
                //archivo("Numero de itms en los parametros " + parametros.Length.ToString());
                //archivo("Valor de item 0 " + parametros[0].ToString());  
                DbCommand cmd = db.GetStoredProcCommand("BPM_LISTA_AUT_AMB_X_JUR", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                //archivo("Numero de registros encontrados " + dsResultado.Tables[0].Rows.Count.ToString());  
                return (dsResultado);  
            }
            catch (SqlException sqle)
            {
                  
                return null;
            }
        }


        /// <summary>
        /// Idnetificador del municipio
        /// </summary>
        /// <param name="_idMunicipio">Conjunto de datos de la jurisdicción: [MUN_ID] - [AUT_ID] - [AUT_NOMBRE]</param>
        public DataSet ListarAAXJurisdiccion(Nullable<int> _idMunicipio)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { _idMunicipio };
                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_AA_X_JURIS", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);
            }
            catch (SqlException sqle)
            {
                return null;
            }
        }

        /// <summary>
        /// Lista las autoridades ambientales en la BD con jurisdiccion en la ubicacion definida en el proceso
        /// </summary>
        /// <param name="idProcessInstance">Identificador de la instacia del proceso</param>
        /// <returns>AUT_ID, AUT_NOMBRE</returns>
        /// <remarks>Solo aplica para procesos diferentes a DAA y que aplica AA</remarks>
        public DataSet ListarAAXUbicacionOtros(Int64 idProcessInstance)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { idProcessInstance };
                //archivo("Cadena de conexion " + objConfiguracion.SilpaCnx.ToString());  
                //archivo("Numero de itms en los parametros " + parametros.Length.ToString());
                //archivo("Valor de item 0 " + parametros[0].ToString());  
                DbCommand cmd = db.GetStoredProcCommand("BPM_LISTA_AUT_AMB_X_JUR_OTROS", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                //archivo("Numero de registros encontrados " + dsResultado.Tables[0].Rows.Count.ToString());  
                return (dsResultado);
            }
            catch (SqlException sqle)
            {

                return null;
            }
        }

        /// <summary>
        /// retorna o asigna la autoridad ambiental dependiendo si el usuario pertenece a una corporacion, 
        /// si es corporacion se asigna la solicitud a la ANLA de lo contrario se asigna la que se asigna previamente
        /// </summary>
        /// <param name="Id_AutAmbiental"></param>
        /// <param name="Id_Solicitante"></param>
        /// <param name="Id_TipoTramite"></param>
        /// <returns>AutoridadAmbiental</returns>
        #region jmartinez 14-12-2017 retorna o asigna la autoridad ambiental dependiendo si el usuario pertenece a una corporacion, si es corporacion se asigna la solicitud a la ANLA de lo contrario se asigna la que se asigna previamente
        public int ValidarUsuarioCorporacion(int? Id_AutAmbiental, long Id_Solicitante, int Id_TipoTramite)
        {
            int Int_AutoridadAmbiental = 0;

            try
            {
                /// se hace la conexión a la base de datos SILA:
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] {
                                                     Id_AutAmbiental,
                                                     Id_Solicitante,
                                                     Id_TipoTramite
                                                   };
                DbCommand cmd = db.GetStoredProcCommand("SP_VERIFICA_USUARIO_CORPORACION", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                if (dsResultado != null)
                {
                    if (dsResultado.Tables[0].Rows.Count > 0)
                    {
                        if (dsResultado.Tables[0].Rows[0]["ID_AUT_AMB_OUT"] != DBNull.Value)
                        {
                            Int_AutoridadAmbiental = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["ID_AUT_AMB_OUT"]);
                        }
                    }
                }
                return Int_AutoridadAmbiental;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        #endregion

        /// <summary>
        /// Obtiene el identificadot de la Autoridad Ambiental MVADT
        /// </summary>
        /// <param name="idParametro">int: Identificador de AA MAVDT en la tabla SILPA_PRE.DBO.GEN_PARAMETRO</param>
        /// <returns>int: </returns>
        public int ObtenerIdAutoridadMAVDT(int idParametro) 
        {
            try
            {
                /// se hace la conexión a la base de datos SILA:
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { idParametro,string.Empty };
                DbCommand cmd = db.GetStoredProcCommand("BAS_OBTENER_VALOR_PARAMETRO", parametros);
                int i = db.ExecuteNonQuery(cmd);
                int result = -1;
                if (db.GetParameterValue(cmd, "VALOR")!=null) 
                {
                    result = int.Parse(db.GetParameterValue(cmd, "VALOR").ToString());
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet ListarAutoridadAmbientalXNumeroVital(String str_NumeroVital)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object [] { str_NumeroVital };
            DbCommand cmd = db.GetStoredProcCommand("BPM_LISTA_AUT_AMB_X_NUMERO_VITAL", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);
            return (dsResultado );
        }
        public DataSet ListatAutoridadAmbiental()
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("BPM_LISTA_AUT_AMB");
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);
            }
            catch (SqlException sqle)
            {
                return null;
            }
        }


        public DataSet AutoridadAmbientalDetalle(int AUT_ID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { AUT_ID };
                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_AUT_AMB_ACT_DETALLE", parametros);
                DataSet ds_datos = db.ExecuteDataSet(cmd);
                return ds_datos;
            }
            catch (SqlException sql)
            {
                throw new Exception(sql.Message);
            }
        }

    }
}

