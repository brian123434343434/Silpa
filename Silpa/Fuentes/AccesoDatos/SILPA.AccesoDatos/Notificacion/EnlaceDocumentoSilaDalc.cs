using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SoftManagement.Log;

namespace SILPA.AccesoDatos.Notificacion
{
    public class EnlaceDocumentoSilaDalc
    {
        private Configuracion objConfiguracion;

        #region CReadoras
        
            /// <summary>
            /// Creadora
            /// </summary>
            public EnlaceDocumentoSilaDalc()
            {
                this.objConfiguracion = new Configuracion();
            }

        #endregion


        #region Metodos Publicos


            /// <summary>
            /// Consultar la información de un enlace
            /// </summary>
            /// <param name="p_strEnlaceID">string con la información del enlace</param>
            /// <param name="p_strLlave">string con la llave</param>
            /// <returns>EnlaceDocumentoSilaEntity con la información del enlace</returns>
            public EnlaceDocumentoSilaEntity ConsultarEnlace(string p_strEnlaceID, string p_strLlave)
            {
                DbCommand objCommand = null;
                SqlDatabase objDataBase = null;
                DataSet objDatosEnlace = null;
                EnlaceDocumentoSilaEntity objEnlace = null;

                try
                {
                    //Cargar conexion
                    objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                    //Cargar comando
                    objCommand = objDataBase.GetStoredProcCommand("NOT_CONSULTAR_ENLACE_DOCUMENTOS_SILA");
                    if (!string.IsNullOrEmpty(p_strEnlaceID))
                        objDataBase.AddInParameter(objCommand, "@P_ID_ENLACE_DOCUMENTOS", DbType.String, p_strEnlaceID);
                    if (!string.IsNullOrEmpty(p_strLlave))
                        objDataBase.AddInParameter(objCommand, "@P_LLAVE", DbType.String, p_strLlave);

                    //Crear registro
                    objDatosEnlace = objDataBase.ExecuteDataSet(objCommand);

                    if (objDatosEnlace != null && objDatosEnlace.Tables.Count > 0 && objDatosEnlace.Tables[0].Rows.Count > 0)
                    {
                        
                        //Estado del flujo
                        objEnlace = new EnlaceDocumentoSilaEntity
                        {
                            EnlaceID = objDatosEnlace.Tables[0].Rows[0]["ID_ENLACE_DOCUMENTOS_SILA"].ToString(),
                            ActoNotificacionID = Convert.ToInt64(objDatosEnlace.Tables[0].Rows[0]["ID_ACTO_NOTIFICACION"]),
                            PersonaID = Convert.ToInt64(objDatosEnlace.Tables[0].Rows[0]["ID_PERSONA"]),
                            EstadoPersonaID = Convert.ToInt64(objDatosEnlace.Tables[0].Rows[0]["ID_ESTADO_PERSONA"]),
                            DocumentoID = Convert.ToInt32(objDatosEnlace.Tables[0].Rows[0]["DOC_ID"]),
                            Llave = objDatosEnlace.Tables[0].Rows[0]["LLAVE"].ToString(),
                            FechaCreacion = Convert.ToDateTime(objDatosEnlace.Tables[0].Rows[0]["FECHA_CREACION"]),
                            FechaVigencia = (objDatosEnlace.Tables[0].Rows[0]["FECHA_VIGENCIA"] != System.DBNull.Value ? Convert.ToDateTime(objDatosEnlace.Tables[0].Rows[0]["FECHA_VIGENCIA"]) : default(DateTime))
                        };  
                    }

                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "EnlaceDocumentoSilaDalc :: ConsultarEnlace -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "EnlaceDocumentoSilaDalc :: ConsultarEnlace -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw exc;
                }
                finally
                {
                    if (objCommand != null)
                    {
                        objCommand.Dispose();
                        objCommand = null;
                    }
                    if (objDataBase != null)
                        objDataBase = null;
                }

                return objEnlace;
            }


            /// <summary>
            /// Crear un nuevo enlace
            /// </summary>
            /// <param name="p_objEnlace">EnlaceDocumentoSilaEntity con la informcaion del enlace</param>
            public void CrearEnlace(EnlaceDocumentoSilaEntity p_objEnlace)
            {
                DbCommand objCommand = null;
                SqlDatabase objDataBase = null;

                try
                {
                    //Cargar conexion
                    objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                    //Cargar comando
                    objCommand = objDataBase.GetStoredProcCommand("NOT_INSERTAR_ENLACE_DOCUMENTOS_SILA");
                    objDataBase.AddInParameter(objCommand, "@P_ID_ENLACE_DOCUMENTOS_SILA", DbType.String, p_objEnlace.EnlaceID);
                    objDataBase.AddInParameter(objCommand, "@P_ID_ACTO_NOTIFICACION", DbType.Int64, p_objEnlace.ActoNotificacionID);
                    objDataBase.AddInParameter(objCommand, "@P_ID_PERSONA", DbType.Int64, p_objEnlace.PersonaID);
                    if (p_objEnlace.EstadoPersonaID > 0)
                        objDataBase.AddInParameter(objCommand, "@P_ID_ESTADO_PERSONA", DbType.Int64, p_objEnlace.EstadoPersonaID);
                    objDataBase.AddInParameter(objCommand, "@P_DOC_ID", DbType.Int32, p_objEnlace.DocumentoID);                    
                    objDataBase.AddInParameter(objCommand, "@P_LLAVE", DbType.String, p_objEnlace.Llave);
                    if (p_objEnlace.FechaVigencia != default(DateTime))
                        objDataBase.AddInParameter(objCommand, "@P_FECHA_VIGENCIA", DbType.DateTime, p_objEnlace.FechaVigencia);

                    //Crear registro
                    objDataBase.ExecuteNonQuery(objCommand);

                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "EnlaceDocumentoSilaDalc :: CrearEnlace -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "EnlaceDocumentoSilaDalc :: CrearEnlace -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw exc;
                }
                finally
                {
                    if (objCommand != null)
                    {
                        objCommand.Dispose();
                        objCommand = null;
                    }
                    if (objDataBase != null)
                        objDataBase = null;
                }
            }

        #endregion

    }
}
