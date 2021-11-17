using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.Comun;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using SILPA.AccesoDatos.Generico;
using SILPA.AccesoDatos.Encuestas.Encuesta.Entity;
using SoftManagement.Log;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace SILPA.AccesoDatos.Encuestas.Encuesta.Dalc
{
    public class AutoridadAmbientalEncuestasDalc
    {
        #region  Objetos

            private Configuracion _objConfiguracion;

        #endregion


        #region  Creadora

            /// <summary>
            /// Creadora
            /// </summary>
            public AutoridadAmbientalEncuestasDalc()
            {
                //Creary cargar configuración
                this._objConfiguracion = new Configuracion();
            }

        #endregion


        #region  Metodos Publicos

            /// <summary>
            /// Consultar el listado de autoridades ambientales
            /// </summary>
            /// <returns>List con la información de las autoridades ambientales</returns>
            public List<AutoridadAmbientalEncuestasEntity> ConsultarAutoridadAmbientales()
            {
                SqlDatabase objDataBase = null;
                DbCommand objCommand = null;
                DataSet objInformacion = null;
                List<AutoridadAmbientalEncuestasEntity> objLstAutoridades = null;
                AutoridadAmbientalEncuestasEntity objAutoridad = null;

                try
                {
                    //Cargar conexion
                    objDataBase = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());

                    //Cargar comando
                    objCommand = objDataBase.GetStoredProcCommand("ENC_LISTA_AUTORIDAD_AMBIENTAL");

                    //Consultar
                    objInformacion = objDataBase.ExecuteDataSet(objCommand);
                    if (objInformacion != null && objInformacion.Tables[0].Rows.Count > 0)
                    {
                        //Crear el listado
                        objLstAutoridades = new List<AutoridadAmbientalEncuestasEntity>();

                        //Ciclo que carga la información
                        foreach (DataRow objAutoridadInformacion in objInformacion.Tables[0].Rows)
                        {
                            //Crear objeto y cargar datos
                            objAutoridad = new AutoridadAmbientalEncuestasEntity
                            {
                                AutoridadID = Convert.ToInt32(objAutoridadInformacion["AUT_ID"]),
                                Autoridad = objAutoridadInformacion["AUT_NOMBRE"].ToString(),
                            };

                            //Adiciona al listado
                            objLstAutoridades.Add(objAutoridad);
                        }
                    }
                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "AutoridadAmbientalEncuestasDalc :: ConsultarAutoridades -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "AutoridadAmbientalEncuestasDalc :: ConsultarAutoridades -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw exc;
                }

                return objLstAutoridades;
            }

        #endregion

    }
}
