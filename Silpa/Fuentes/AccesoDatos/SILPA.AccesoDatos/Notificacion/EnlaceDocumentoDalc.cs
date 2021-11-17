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
    public class EnlaceDocumentoDalc
    {
        private Configuracion objConfiguracion;

        #region CReadoras
        
            /// <summary>
            /// Creadora
            /// </summary>
            public EnlaceDocumentoDalc()
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
            /// <returns>EnlaceDocumentoEntity con la información del enlace</returns>
            public EnlaceDocumentoEntity ConsultarEnlace(string p_strEnlaceID, string p_strLlave)
            {
                DbCommand objCommand = null;
                SqlDatabase objDataBase = null;
                DataSet objDatosEnlace = null;
                EnlaceDocumentoEntity objEnlace = null;

                try
                {
                    //Cargar conexion
                    objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                    //Cargar comando
                    objCommand = objDataBase.GetStoredProcCommand("NOT_CONSULTAR_ENLACE_DOCUMENTOS");
                    if (!string.IsNullOrEmpty(p_strEnlaceID))
                        objDataBase.AddInParameter(objCommand, "@P_ID_ENLACE_DOCUMENTOS", DbType.String, p_strEnlaceID);
                    if (!string.IsNullOrEmpty(p_strLlave))
                        objDataBase.AddInParameter(objCommand, "@P_LLAVE_ENVIADA", DbType.String, p_strLlave);

                    //Crear registro
                    objDatosEnlace = objDataBase.ExecuteDataSet(objCommand);

                    if (objDatosEnlace != null && objDatosEnlace.Tables.Count > 0 && objDatosEnlace.Tables[0].Rows.Count > 0)
                    {
                        
                        //Estado del flujo
                        objEnlace = new EnlaceDocumentoEntity
                        {
                            EnlaceID = objDatosEnlace.Tables[0].Rows[0]["ID_ENLACE_DOCUMENTOS"].ToString(),
                            AutoridadID = (objDatosEnlace.Tables[0].Rows[0]["AUT_ID"] != System.DBNull.Value ? Convert.ToInt32(objDatosEnlace.Tables[0].Rows[0]["AUT_ID"]) : -1),
                            ActoNotificacionID = Convert.ToInt64(objDatosEnlace.Tables[0].Rows[0]["ID_ACTO_NOTIFICACION"]),
                            PersonaID = Convert.ToInt64(objDatosEnlace.Tables[0].Rows[0]["ID_PERSONA"]),
                            EstadoPersonaID = Convert.ToInt64(objDatosEnlace.Tables[0].Rows[0]["ID_ESTADO_PERSONA"]),
                            LlaveEnviada = objDatosEnlace.Tables[0].Rows[0]["LLAVE_ENVIADA"].ToString(),
                            IncluirActoAdministrativo = Convert.ToBoolean(objDatosEnlace.Tables[0].Rows[0]["INCLUIR_ACTO_ADMINISTRATIVO"]),
                            IncluirConceptosActoAdministrativo = Convert.ToBoolean(objDatosEnlace.Tables[0].Rows[0]["INCLUIR_ACTO_CONCEPTOS_ACTO_ADMINISTRATIVO"]),
                            FechaCreacion = Convert.ToDateTime(objDatosEnlace.Tables[0].Rows[0]["FECHA_CREACION"]),
                            FechaVigencia = (objDatosEnlace.Tables[0].Rows[0]["FECHA_VIGENCIA"] != System.DBNull.Value ? Convert.ToDateTime(objDatosEnlace.Tables[0].Rows[0]["FECHA_VIGENCIA"]) : default(DateTime))
                        };  
                    }

                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "EnlaceDocumentoDalc :: ConsultarEnlace -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "EnlaceDocumentoDalc :: ConsultarEnlace -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

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
            /// <param name="p_objEnlace">EnlaceDocumentoEntity con la informcaion del enlace</param>
            public void CrearEnlace(EnlaceDocumentoEntity p_objEnlace)
            {
                DbCommand objCommand = null;
                SqlDatabase objDataBase = null;

                try
                {
                    //Cargar conexion
                    objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                    //Cargar comando
                    objCommand = objDataBase.GetStoredProcCommand("NOT_INSERTAR_ENLACE_DOCUMENTOS");
                    objDataBase.AddInParameter(objCommand, "@P_ID_ACTO_NOTIFICACION", DbType.Int64, p_objEnlace.ActoNotificacionID);
                    objDataBase.AddInParameter(objCommand, "@P_ID_PERSONA", DbType.Int64, p_objEnlace.PersonaID);
                    objDataBase.AddInParameter(objCommand, "@P_ID_ESTADO_PERSONA", DbType.Int64, p_objEnlace.EstadoPersonaID);
                    objDataBase.AddInParameter(objCommand, "@P_LLAVE_ENVIADA", DbType.String, p_objEnlace.LlaveEnviada);
                    objDataBase.AddInParameter(objCommand, "@P_INCLUIR_ACTO_ADMINISTRATIVO", DbType.Boolean, p_objEnlace.IncluirActoAdministrativo);
                    objDataBase.AddInParameter(objCommand, "@P_INCLUIR_ACTO_CONCEPTOS_ACTO_ADMINISTRATIVO", DbType.Boolean, p_objEnlace.IncluirConceptosActoAdministrativo);                    
                    if (p_objEnlace.FechaVigencia != default(DateTime))
                        objDataBase.AddInParameter(objCommand, "@P_FECHA_VIGENCIA", DbType.DateTime, p_objEnlace.FechaVigencia);

                    //Crear registro
                    objDataBase.ExecuteNonQuery(objCommand);

                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "EnlaceDocumentoDalc :: CrearEnlace -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "EnlaceDocumentoDalc :: CrearEnlace -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

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
