using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SILPA.Comun;
using System.Data.SqlClient;
using System.Data.Common;
using SoftManagement.Log;
using SILPA.AccesoDatos.Excepciones;
using System.Data;
using System.Data.OleDb;

namespace SILPA.AccesoDatos.EvaluacionREA.Dalc
{
    public class PDFSolicitudREADalc
    {
        #region  Objetos

            private Configuracion _objConfiguracion;

        #endregion


        #region  Creadora

            /// <summary>
            /// Creadora
            /// </summary>
            public PDFSolicitudREADalc()
            {
                //Crear y cargar configuración
                this._objConfiguracion = new Configuracion();
            }

        #endregion

        #region Metodos Publicos

            /// <summary>
            /// Consultar la información del registro para generación del pdf
            /// </summary>
            /// <param name="p_intREAID">int con el identificador del registro</param>
            /// <returns>DataSet con la informacion para la generación del PDF</returns>
            public DataSet ConsultarRegistroPDF(int p_intREAID)
            {
                DbCommand objCommand = null;
                SqlDatabase objDataBase = null;
                DataSet objInformacion = null;

                try
                {
                    //Cargar conexion
                    objDataBase = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());

                    //Ejecutar consulta
                    objCommand = objDataBase.GetStoredProcCommand("REASP_CONSULTAR_REGISTRO_PDF");
                    objDataBase.AddInParameter(objCommand, "@P_SOLICITUD_EVALUACION_REA_ID", DbType.Int32, p_intREAID);
                    objInformacion = objDataBase.ExecuteDataSet(objCommand);

                    //Dar nombre a las tablas
                    if (objInformacion != null && objInformacion.Tables.Count == 8)
                    {
                        objInformacion.Tables[0].TableName = "SOLICITUD";
                        objInformacion.Tables[1].TableName = "INSUMO_RECOLECCION";
                        objInformacion.Tables[2].TableName = "INSUMO_PRESERVACION";
                        objInformacion.Tables[3].TableName = "INSUMO_PROFESIONALES";
                        objInformacion.Tables[4].TableName = "INSUMO_COBERTURA";
                        objInformacion.Tables[5].TableName = "SOLICITANTE";
                        objInformacion.Tables[6].TableName = "REPRESENTANTE";
                        objInformacion.Tables[7].TableName = "APODERADO";
                    }
                    else
                    {
                        throw new Exception("Consulta no retorna información consistente");
                    }
                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "RegistroRIDDalc :: ConsultarRegistroPDF -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw new RIDDalcException("RegistroRIDDalc :: ConsultarRegistroPDF -> Error bd: " + sqle.Message, sqle.InnerException);
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "RegistroRIDDalc :: ConsultarRegistroPDF -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw new RIDDalcException("RegistroRIDDalc :: ConsultarRegistroPDF -> Error inesperado: " + exc.Message, exc.InnerException);
                }

                return objInformacion;
            }

        #endregion
    }
}
