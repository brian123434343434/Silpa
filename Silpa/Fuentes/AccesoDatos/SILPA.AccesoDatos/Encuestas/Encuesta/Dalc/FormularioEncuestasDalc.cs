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
    public class FormularioEncuestasDalc
    {
        #region  Objetos

            private Configuracion _objConfiguracion;

        #endregion


        #region  Creadora

            /// <summary>
            /// Creadora
            /// </summary>
            public FormularioEncuestasDalc()
            {
                //Creary cargar configuración
                this._objConfiguracion = new Configuracion();
            }

        #endregion


        #region  Metodos Privados


            /// <summary>
            /// Cargar el listado de opciones de una pregunta
            /// </summary>
            /// <param name="p_objInformacionPreguntas">Lista con los datos de las opciones</param>
            /// <returns>List con la información cargada</returns>
            private List<OpcionPreguntaEncuestasEntity> CargarOpcionesPregunta(DataRow[] p_objInformacionOpciones)
            {
                List<OpcionPreguntaEncuestasEntity> objLstOpcionesEncuestasEntity = null;
                OpcionPreguntaEncuestasEntity objOpcionEncuestasEntity = null;

                try
                {
                    //Verificar que se tenga información
                    if (p_objInformacionOpciones != null && p_objInformacionOpciones.Length > 0)
                    {
                        //Crear el listado de preguntas
                        objLstOpcionesEncuestasEntity = new List<OpcionPreguntaEncuestasEntity>();

                        //Ciclo que carga los datos de las preguntas
                        foreach (DataRow objDatosOpcion in p_objInformacionOpciones)
                        {
                            //Cargar datos
                            objOpcionEncuestasEntity = new OpcionPreguntaEncuestasEntity
                            {
                                OpcionPreguntaID = Convert.ToInt32(objDatosOpcion["ENCOPCION_PREGUNTA_ID"]),
                                PreguntaID = Convert.ToInt32(objDatosOpcion["ENCPREGUNTA_ID"]),
                                TipoOpcion = new TipoOpcionPreguntaEncuestaEntity { TipoOpcionPreguntaID = Convert.ToInt32(objDatosOpcion["ENCTIPOOPCIONPREGUNTA_ID"]), TipoOpcionPregunta = objDatosOpcion["TIPO_OPCION_PREGUNTA"].ToString(), Activo = Convert.ToBoolean(objDatosOpcion["ACTIVO_TIPO_OPCION_PREGUNTA"]) },
                                TextoOpcion = objDatosOpcion["TEXTO_OPCION"].ToString(),
                                Orden = Convert.ToInt32(objDatosOpcion["ORDEN"]),
                                Activo = Convert.ToBoolean(objDatosOpcion["ACTIVO"])
                            };

                            //Adicionar a listado
                            objLstOpcionesEncuestasEntity.Add(objOpcionEncuestasEntity);

                        }
                    }
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "FormularioEncuestasDalc :: CargarOpcionesPregunta -> Error cargando opciones de una pregunta: " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw exc;
                }

                return objLstOpcionesEncuestasEntity;
            }


            /// <summary>
            /// Cargar el listado de opciones que habilitan una pregunta
            /// </summary>
            /// <param name="p_objInformacionPreguntas">Lista con los datos de las opciones</param>
            /// <returns>List con la información cargada</returns>
            private List<PreguntaHabilitaOpcionEncuestasEntity> CargarOpcionesHabilitanPregunta(DataRow[] p_objInformacionOpciones)
            {
                List<PreguntaHabilitaOpcionEncuestasEntity> objLstOpcionesEncuestasEntity = null;
                PreguntaHabilitaOpcionEncuestasEntity objOpcionEncuestasEntity = null;

                try
                {
                    //Verificar que se tenga información
                    if (p_objInformacionOpciones != null && p_objInformacionOpciones.Length > 0)
                    {
                        //Crear el listado de preguntas
                        objLstOpcionesEncuestasEntity = new List<PreguntaHabilitaOpcionEncuestasEntity>();

                        //Ciclo que carga los datos de las preguntas
                        foreach (DataRow objDatosOpcion in p_objInformacionOpciones)
                        {
                            //Cargar datos
                            objOpcionEncuestasEntity = new PreguntaHabilitaOpcionEncuestasEntity
                            {
                                PreguntaHabilitaOpcionID = Convert.ToInt32(objDatosOpcion["ENCPREGUNTA_HABILITAR_OPCION_ID"]),
                                PreguntaID = Convert.ToInt32(objDatosOpcion["ENCPREGUNTA_ID"]),
                                OpcionID = Convert.ToInt32(objDatosOpcion["ENCOPCION_PREGUNTA_ID"]),
                                EsOpcional = Convert.ToBoolean(objDatosOpcion["OPCIONAL"])
                            };

                            //Adicionar a listado
                            objLstOpcionesEncuestasEntity.Add(objOpcionEncuestasEntity);
                        }
                    }
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "FormularioEncuestasDalc :: CargarOpcionesHabilitanPregunta -> Error cargando opciones que habilitan pregunta: " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw exc;
                }

                return objLstOpcionesEncuestasEntity;
            }


            /// <summary>
            /// Cargar el listado de preguntas asociados a una seccion
            /// </summary>
            /// <param name="p_objInformacionPreguntas">Lista con los datos de las pregunatas</param>
            /// <returns>List con la información cargada</returns>
            private List<PreguntaEncuestasEntity> CargarPreguntasSeccion(int p_intSeccionID, DataSet p_objInformacionFormulario)
            {
                List<PreguntaEncuestasEntity> objLstPreguntaEncuestasEntity = null;
                PreguntaEncuestasEntity objPreguntaEncuestasEntity = null;
                DataRow[] objInformacionPreguntas = null;

                try
                {
                    //Cargar las filas asociadas a las preguntas
                    if (p_objInformacionFormulario.Tables[2].Rows.Count > 0)
                        objInformacionPreguntas = p_objInformacionFormulario.Tables[2].Select("ENCSECCION_ID = " + p_intSeccionID.ToString());

                    //Verificar que se tenga información
                    if (objInformacionPreguntas != null && objInformacionPreguntas.Length > 0)
                    {
                        //Crear el listado de preguntas
                        objLstPreguntaEncuestasEntity = new List<PreguntaEncuestasEntity>();

                        //Ciclo que carga los datos de las preguntas
                        foreach (DataRow objDatosPregunta in objInformacionPreguntas)
                        {
                            //Cargar datos
                            objPreguntaEncuestasEntity = new PreguntaEncuestasEntity
                            {
                                PreguntaID = Convert.ToInt32(objDatosPregunta["ENCPREGUNTA_ID"]),
                                TipoPregunta = new TipoPreguntaEncuestaEntity { TipoPreguntaID = Convert.ToInt32(objDatosPregunta["ENCTIPO_PREGUNTA_ID"]), TipoPregunta = objDatosPregunta["TIPO_PREGUNTA"].ToString(), Activo = Convert.ToBoolean(objDatosPregunta["ACTIVO_TIPO_PREGUNTA"]) },                                
                                SeccionID = Convert.ToInt32(objDatosPregunta["ENCSECCION_ID"]),
                                FormularioID = Convert.ToInt32(objDatosPregunta["ENCFORMULARIO_ID"]),
                                SectorID = Convert.ToInt32(objDatosPregunta["ENCSECTOR_ID"]),
                                PreguntaPadreID = (objDatosPregunta["ENCPREGUNTA_PADRE_ID"] != System.DBNull.Value ? Convert.ToInt32(objDatosPregunta["ENCPREGUNTA_PADRE_ID"]) : 0),
                                Pregunta = objDatosPregunta["TEXTO_PREGUNTA"].ToString(),
                                AyudaPregunta = ( objDatosPregunta["AYUDA_PREGUNTA"] != System.DBNull.Value ? objDatosPregunta["AYUDA_PREGUNTA"].ToString() : ""),
                                Obligatorio = Convert.ToBoolean(objDatosPregunta["OBLIGATORIO"]),
                                Orden = Convert.ToInt32(objDatosPregunta["ORDEN"]),
                                Numeral = (objDatosPregunta["NUMERAL"] != System.DBNull.Value ? objDatosPregunta["NUMERAL"].ToString() : ""),
                                Nivel = (objDatosPregunta["NIVEL"] != System.DBNull.Value ? Convert.ToInt32(objDatosPregunta["NIVEL"]) : 0),
                                Activo = Convert.ToBoolean(objDatosPregunta["ACTIVO"])
                            };

                            //Cargar el listado de opciones
                            if (p_objInformacionFormulario.Tables[3].Rows.Count > 0)
                                objPreguntaEncuestasEntity.OpcionesPregunta = this.CargarOpcionesPregunta(p_objInformacionFormulario.Tables[3].Select("ENCPREGUNTA_ID = " + objPreguntaEncuestasEntity.PreguntaID.ToString()));

                            //Cargar el listado de opciones que la activan
                            if (p_objInformacionFormulario.Tables[4].Rows.Count > 0)
                                objPreguntaEncuestasEntity.OpcionesHabilitanPregunta = this.CargarOpcionesHabilitanPregunta(p_objInformacionFormulario.Tables[4].Select("ENCPREGUNTA_ID = " + objPreguntaEncuestasEntity.PreguntaID.ToString()));

                            //Adicionar a listado
                            objLstPreguntaEncuestasEntity.Add(objPreguntaEncuestasEntity);

                        }
                    }
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "FormularioEncuestasDalc :: CargarPreguntasSeccion -> Error cargando datos de preguntas del formulario: " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw exc;
                }

                return objLstPreguntaEncuestasEntity;
            }


            /// <summary>
            /// Cargar la información basica correspondiente al formulario
            /// </summary>
            /// <param name="p_objInformacionFormulario">DataSet con la informacion del formulario</param>
            /// <param name="p_objFormulario">FormularioEncuestasEntity con la informacion del formulario formateada</param>
            private FormularioEncuestasEntity CargarDatosFormulario(DataSet p_objInformacionFormulario)
            {
                FormularioEncuestasEntity objFormularioEncuestasEntity = null;
                SeccionEncuestasEntity objSeccionEncuestasEntity = null;

                try
                {                    
                    //Verificar que la primera tabla contien datos
                    if (p_objInformacionFormulario.Tables[0].Rows.Count > 0)
                    {
                        //Cargar datos basicos
                        objFormularioEncuestasEntity = new FormularioEncuestasEntity
                        {
                            FormularioID = Convert.ToInt32(p_objInformacionFormulario.Tables[0].Rows[0]["ENCFORMULARIO_ID"]),
                            Formulario = p_objInformacionFormulario.Tables[0].Rows[0]["FORMULARIO"].ToString(),
                            Activo = Convert.ToBoolean(p_objInformacionFormulario.Tables[0].Rows[0]["ACTIVO"])
                        };

                        //Verificar que se tenga datos de secciones
                        if (p_objInformacionFormulario.Tables[1].Rows.Count > 0)
                        {
                            //Crear el listado de secciones
                            objFormularioEncuestasEntity.Secciones = new List<SeccionEncuestasEntity>();

                            //Ciclo que carga datos de secciones
                            foreach (DataRow objDatosSeccion in p_objInformacionFormulario.Tables[1].Rows)
                            {
                                //Cargar datos de la seccion
                                objSeccionEncuestasEntity = new SeccionEncuestasEntity
                                {
                                    SeccionID = Convert.ToInt32(objDatosSeccion["ENCSECCION_ID"]),
                                    Seccion = objDatosSeccion["SECCION"].ToString(),
                                    Activo = Convert.ToBoolean(objDatosSeccion["ACTIVO"])
                                };

                                //Cargar el listado de preguntas de la seccion
                                objSeccionEncuestasEntity.Preguntas = this.CargarPreguntasSeccion(objSeccionEncuestasEntity.SeccionID, p_objInformacionFormulario);

                                //Adicionar seccion al listado
                                objFormularioEncuestasEntity.Secciones.Add(objSeccionEncuestasEntity);
                            }
                        }
                    }                    
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "FormularioEncuestasDalc :: CargarDatosFormulario -> Error cargando datos del formulario: " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw exc;
                }

                return objFormularioEncuestasEntity;
            }

        #endregion


        #region  Metodos Publicos

            /// <summary>
            /// Obtener la información del formulario relacionado a un sector especifico
            /// </summary>
            /// <param name="p_intFormularioID">int con el identificador del formulario</param>
            /// <param name="p_intSectorID">int con el identificador del sector</param>
            /// <returns>FormularioEncuestasEntity con la información del formulario</returns>
            public FormularioEncuestasEntity ConsultarFormularioSector(int p_intFormularioID, int p_intSectorID)
            {
                SqlDatabase objDataBase = null;
                DbCommand objCommand = null;
                DataSet objInformacion = null;
                FormularioEncuestasEntity objFormularioEncuestasEntity = null;

                try
                {
                    //Cargar conexion
                    objDataBase = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());

                    //Cargar comando
                    objCommand = objDataBase.GetStoredProcCommand("ENC_CONSULTAR_FORMULARIO_SECTOR_ACTIVO");
                    objDataBase.AddInParameter(objCommand, "@P_ENCFORMULARIO_ID", DbType.Int32, p_intFormularioID);
                    objDataBase.AddInParameter(objCommand, "@P_ENCSECTOR_ID", DbType.Int32, p_intSectorID);

                    //Consultar
                    objInformacion = objDataBase.ExecuteDataSet(objCommand);
                    if (objInformacion != null && objInformacion.Tables.Count == 5)
                    {
                        //Cargar la información del formulario
                        objFormularioEncuestasEntity = this.CargarDatosFormulario(objInformacion);
                    }
                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "FormularioEncuestasDalc :: ConsultarFormularioSector -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "FormularioEncuestasDalc :: ConsultarFormularioSector -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw exc;
                }

                return objFormularioEncuestasEntity;
            }

        #endregion
    }
}
