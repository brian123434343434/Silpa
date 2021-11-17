using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using SILPA.AccesoDatos.Liquidacion.Dalc;
using SILPA.AccesoDatos.Liquidacion.Entity;
using SILPA.AccesoDatos.Generico;
using SILPA.LogicaNegocio.Liquidacion.Entidades;
using SILPA.LogicaNegocio.Excepciones;
using SoftManagement.Log;
using SILPA.LogicaNegocio.Liquidacion.Enum;
using SILPA.AccesoDatos.BPMProcess;
using System.Xml.Serialization;
using SILPA.LogicaNegocio.BPMProcessL;
using SILPA.Comun;
using System.Configuration;
using SILPA.LogicaNegocio.Generico;
using SILPA.LogicaNegocio.Formularios;
using System.Globalization;

namespace SILPA.LogicaNegocio.Liquidacion
{
    public class Autoliquidacion
    {
        #region Objetos

            private AutoliquidacionDalc _objAutoliquidacionDalc;        

        #endregion


        #region Metodos Privados

            #region Radicacion VITAL

                /// <summary>
                /// Crear objeto identity para cargar en XML
                /// </summary>
                /// <param name="pintId">int con el ID</param>
                /// <param name="p_strGrupo">string con el nombre del grupo</param>
                /// <param name="p_strValor">string con el valor</param>
                /// <param name="p_intOrden">int con el orden</param>
                /// <param name="p_objArchivo">Arreglo de bytes con archivo</param>
                /// <returns></returns>
                private ValoresIdentity CargarValores(int pintId, string p_strGrupo, string p_strValor, int p_intOrden, Byte[] p_objArchivo)
                {
                    ValoresIdentity objValores = new ValoresIdentity();
                    objValores.Id = pintId;
                    objValores.Grupo = p_strGrupo;
                    objValores.Valor = p_strValor;
                    objValores.Orden = p_intOrden;
                    objValores.Archivo = p_objArchivo;
                    return objValores;
                }


                /// <summary>
                /// Crear el XML requerido para crear formulario en Vital
                /// </summary>
                /// <param name="p_intSolicitanteID">int con el identificador del solicitante</param>
                /// <param name="p_intSolicitudLiquidacionID">int con el identificador de la solicitud</param>
                /// <param name="p_intAutoridadID">int con el identificador de la auqtoridad ambiental</param>
                /// <returns>string con el XML</returns>
                private string CrearXmlVital(int p_intSolicitanteID, int p_intSolicitudLiquidacionID, int p_intAutoridadID)
                {
                    List<ValoresIdentity> objValoresList = new List<ValoresIdentity>();
                    objValoresList.Add(CargarValores(1, "Bas", p_intSolicitudLiquidacionID.ToString(), 1, new Byte[1]));
                    objValoresList.Add(CargarValores(2, "Bas", p_intSolicitanteID.ToString(), 1, new Byte[1]));
                    objValoresList.Add(CargarValores(3, "Bas", p_intAutoridadID.ToString(), 1, new Byte[1]));
                    MemoryStream memoryStream = new MemoryStream();
                    XmlSerializer serializador = new XmlSerializer(typeof(List<ValoresIdentity>));
                    serializador.Serialize(memoryStream, objValoresList);
                    string xml = System.Text.UTF8Encoding.UTF8.GetString(memoryStream.ToArray());
                    return xml;
                }


                /// <summary>
                /// Realizar el registro de la solicitud en VITAL
                /// </summary>
                /// <param name="p_intFortmularioID">int con el identificador del formulario</param>
                /// <param name="p_intSolicitanteID">int con el identificador del solicitante</param>
                /// <param name="p_intSolicitudLiquidacionID">int con el identificador de la solicitud</param>
                /// <param name="p_intAutoridadID">int con el identificador de la auqtoridad ambiental</param>
                /// <returns>string con el numero vital</returns>
                private string RegistroVital(long p_intFortmularioID, int p_intSolicitanteID, int p_intSolicitudLiquidacionID, int p_intAutoridadID)
                {
                    BpmProcessLn objProceso = new BpmProcessLn();
                    string strNumeroVital = "";

                    try
                    {
                        //Crear proceso
                        objProceso = new BpmProcessLn();

                        //TODO Verificar en pruebas
                        strNumeroVital = objProceso.crearProceso(ConfigurationManager.AppSettings["BPMAutoliquidacionClientID"].ToString(),
                                                                p_intFortmularioID,
                                                                p_intSolicitanteID,
                                                                this.CrearXmlVital(p_intSolicitanteID, p_intSolicitudLiquidacionID, p_intAutoridadID));
                        //strNumeroVital = "760000801507" + (new Random().Next(6000000, 6099999)).ToString(); 
                        //strNumeroVital = "0700900745219815001";
                        //Verificar si se obtuvo el número vital
                        if (string.IsNullOrEmpty(strNumeroVital))
                        {
                            throw new Exception("No se obtuvo número vital para proceso de autoliquidación");
                        }
                    }                    
                    catch (BPMException exc)
                    {
                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "Autoliquidacion :: RegistroVital -> Error realizando la radicación: " + exc.Message + " " + exc.StackTrace);

                        //Escalar error
                        throw new RadicacionAutoliquidacionException("Autoliquidacion :: RegistroVital -> Error Realizando radicación: " + exc.Message, exc.InnerException);
                    }
                    catch (Exception exc)
                    {
                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "Autoliquidacion :: RegistroVital -> Error Inesperado: " + exc.Message + " " + exc.StackTrace);

                        //Escalar error
                        throw new RadicacionAutoliquidacionException("Autoliquidacion :: RegistroVital -> Error Inesperado: " + exc.Message, exc.InnerException);
                    }

                    return strNumeroVital;
                }
        

                /// <summary>
                /// Enviar correo de solicitud de liquidación en VITAL
                /// </summary>
                /// <param name="p_objSolicitudLiquidacion">SolicitudLiquidacionEntity con la información de la solicitud</param>
                private void EnviarCorreoRadicacionSolicitud(SolicitudLiquidacionEntity p_objSolicitudLiquidacion)
                {
                    SILPA.LogicaNegocio.ICorreo.Correo objCorreo = null;
                    Persona objPersona = null;
                    string strNombreSOlicitante = "";
                    string strFechaRadicacion = "";

                    try
                    {
                        //Obtener informacion del solicitante
                        objPersona = new Persona();
                        objPersona.ObternerPersonaByUserIdApp(p_objSolicitudLiquidacion.SolicitanteID.ToString());

                        if (objPersona.Identity != null)
                        {
                            //Verificar si la persona tiene registrado correo electronico
                            if (!string.IsNullOrEmpty(objPersona.Identity.CorreoElectronico))
                            {

                                //Cargar el nombre del solicitante
                                if (!string.IsNullOrEmpty(objPersona.Identity.RazonSocial))
                                {
                                    strNombreSOlicitante = objPersona.Identity.RazonSocial;
                                }
                                else
                                {
                                    strNombreSOlicitante = objPersona.Identity.PrimerNombre + " " +
                                                           objPersona.Identity.SegundoNombre + " " +
                                                           objPersona.Identity.PrimerApellido + " " +
                                                           objPersona.Identity.SegundoApellido;
                                }

                                //Cargar la fecha de radicacion
                                strFechaRadicacion = p_objSolicitudLiquidacion.FechaRadicacionVITAL.ToString("dd") + " de " + p_objSolicitudLiquidacion.FechaRadicacionVITAL.ToString("MMMM", CultureInfo.CreateSpecificCulture("es-CO")) + " de " + p_objSolicitudLiquidacion.FechaRadicacionVITAL.ToString("yyyy");

                                //Enviar correo
                                objCorreo = new ICorreo.Correo();
                                objCorreo.EnviarCorreoRegistroSolicitudAutoliquidacion(strNombreSOlicitante.Trim(), objPersona.Identity.CorreoElectronico,
                                                                                       p_objSolicitudLiquidacion.ClaseSolicitud.ClaseSolicitud, p_objSolicitudLiquidacion.TipoSolicitud.TipoSolicitud,
                                                                                       p_objSolicitudLiquidacion.AutoridadAmbiental.Nombre, strFechaRadicacion, p_objSolicitudLiquidacion.NumeroVITAL);
                            }
                            else
                            {
                                throw new Exception("No se encontro correo para el usuario " + p_objSolicitudLiquidacion.SolicitanteID);
                            }
                        }
                        else
                        {
                            throw new Exception("No se encontro información para el usuario " + p_objSolicitudLiquidacion.SolicitanteID);
                        }
                    }
                    catch (Exception exc)
                    {
                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "Autoliquidacion :: EnviarCorreoRadicacionSolicitud -> Error Inesperado: " + exc.Message + " " + exc.StackTrace);
                    }
                }

            #endregion

        #endregion


        #region Metodos Publicos


            #region Consultas

                /// <summary>
                /// Consultar el identificador correspondiente a la descripcion
                /// </summary>
                /// <param name="p_intDesplegable">int con el identificador del deplegable</param>
                /// <param name="p_strDescripcion">string con la descripción</param>
                /// <param name="p_strDescripcionPadre">string con la descripcion padre a buscar</param>
                /// <returns>int con el identificador</returns>
                public int ConsultarIdentificadoDesplegable(int p_intDesplegable, string p_strDescripcion, string p_strDescripcionPadre = "")
                {
                    this._objAutoliquidacionDalc = new AutoliquidacionDalc();
                    return this._objAutoliquidacionDalc.ConsultarIdentificadoDesplegable(p_intDesplegable, p_strDescripcion, p_strDescripcionPadre);
                }


                /// <summary>
                /// Obtener la información de la solicitud identificada por el número VITAL que ingresa
                /// </summary>
                /// <param name="p_strNumeroVital">string con el número vital</param>
                /// <returns>
                /// DataSet con la información de la radicación en las siguientes tablas:
                /// - Radicacion
                /// - Solicitante
                /// - Representantes
                /// </returns>
                public DataSet ConsultarInformacionRadicacionNumeroVital(string p_strNumeroVital)
                {
                    this._objAutoliquidacionDalc = new AutoliquidacionDalc();
                    return this._objAutoliquidacionDalc.ConsultarInformacionRadicacionNumeroVital(p_strNumeroVital);
                }


                /// <summary>
                /// Obtener la autoridad(es) ambiental(es) correspondiente a la solicitud de liquidación realizada
                /// </summary>
                /// <param name="p_intTipoSolicitudID">int con el identificador del tipo de solicitud que se esta realizando</param>
                /// <param name="p_intTramiteID">int con el identificador del tramite que se encuentra realizando</param>
                /// <param name="p_intTipoProyectoID">int con el identificador el tipo de proyecto o actividad</param>
                /// <param name="p_lstTipoProyectoID">List con el listado de identificadores de municipios donde se llevara a capo los proyectos</param>
                /// <returns>List con la(s) autoridad(es) ambiental(es) a las cuales se direccione la solicitud</returns>
                public List<AutoridadAmbientalIdentity> ConsultarAutoridadAmbientalSolicitud(int p_intTipoSolicitudID, int p_intTramiteID, int p_intTipoProyectoID, List<int> p_lstTipoProyectoID)
                {
                    this._objAutoliquidacionDalc = new AutoliquidacionDalc();
                    return this._objAutoliquidacionDalc.ConsultarAutoridadAmbientalSolicitud(p_intTipoSolicitudID, p_intTramiteID, p_intTipoProyectoID, p_lstTipoProyectoID);
                }
        

                /// <summary>
                /// Consultar la información de la solicitud de la liquidación
                /// </summary>
                /// <param name="p_SolicitudLiquidacionID">int con el identificador de la solicitud</param>
                /// <returns>SolicitudLiquidacionEntity con la información de la solicitud. En caso de no encontrar información retorna null</returns>
                public SolicitudLiquidacionEntity ConsultarSolicitudLiquidacion(int p_intSolicitudLiquidacionID)
                {
                    this._objAutoliquidacionDalc = new AutoliquidacionDalc();
                    return this._objAutoliquidacionDalc.ConsultarSolicitudLiquidacion(p_intSolicitudLiquidacionID);
                }


                /// <summary>
                /// Consultar la información de la solicitud de la liquidación para generación del documento de radicación
                /// </summary>
                /// <param name="p_intSolicitudLiquidacionID">int con el identificador de la solicitud</param>
                /// <returns>
                /// DataSet con la información de la solicitud en las siguientes tablas:
                /// - Solicitud
                /// - Permisos
                /// - Regiones
                /// - Ubicaciones
                /// - Rutas
                /// - Campos_Complementarios
                /// - Cobros_Relacionados
                /// </returns>
                public DataSet ConsultarSolicitudLiquidacionDocumentoRadicacion(int p_intSolicitudLiquidacionID)
                {
                    this._objAutoliquidacionDalc = new AutoliquidacionDalc();
                    return this._objAutoliquidacionDalc.ConsultarSolicitudLiquidacionDocumentoRadicacion(p_intSolicitudLiquidacionID);
                }


                /// <summary>
                /// Consultar el listado de solicitudes que cumplen con los parametros de busqueda especificados
                /// </summary>
                /// <param name="p_intSolicitanteID">int con el identificador del solicitante</param>
                /// <param name="p_intAutoridadID">int con el identificador de la autoridad ambiental. Opcional.</param>
                /// <param name="p_intTipoSolicitudID">int con el identificador del tipo de solicitud. Opcional.</param>
                /// <param name="p_intClaseSolicitudID">int con la clase de solicitud. Opcional</param>
                /// <param name="p_strNombreProyecto">string con el nombre del proyecto. Opcional.</param>
                /// <param name="p_strNumeroVital">string con el numero vital. Opcional.</param>
                /// <param name="p_intEstadoSolicitudID">int con el identificador del estado de la solicitud. Opcional.</param>
                /// <param name="p_objFechaCreacionInicio">DataTime con la fecha de creación. Rango Inicial. Opcional.</param>
                /// <param name="p_objFechaCreacionFin">DataTime con la fecha de creación. Rango Final. Opcional.</param>
                /// <returns>List con la información de las solicitudes</returns>
                public List<SolicitudLiquidacionEntity> ConsultarListadoSolicitudesLiquidacion(int p_intSolicitanteID, int p_intAutoridadID = -1, int p_intTipoSolicitudID = -1,
                                                                                               int p_intClaseSolicitudID = -1, string p_strNombreProyecto = "", string p_strNumeroVital = "",
                                                                                               int p_intEstadoSolicitudID = -1, DateTime p_objFechaCreacionInicio = default(DateTime), DateTime p_objFechaCreacionFin = default(DateTime))
                {
                    this._objAutoliquidacionDalc = new AutoliquidacionDalc();
                    return this._objAutoliquidacionDalc.ConsultarListadoSolicitudesLiquidacion(p_intSolicitanteID, p_intAutoridadID, p_intTipoSolicitudID,
                                                                                               p_intClaseSolicitudID, p_strNombreProyecto, p_strNumeroVital,
                                                                                               p_intEstadoSolicitudID, p_objFechaCreacionInicio, p_objFechaCreacionFin);
                }


                /// <summary>
                /// Cargar la información contenida en el datatable en el listado de cobros relacionados
                /// </summary>
                /// <param name="p_objDatosCobros">DataTable con la información de los cobros</param>
                /// <param name="p_objSolicitudLiquidacion">SolicitudLiquidacionEntity objeto en el cual se cargara los datos de los cobros</param>
                public List<CobroSolicitudLiquidacionEntity> ObtenerCobrosSolicitudLiquidacion(int p_intSolicitudLiquidacionID)
                {
                    this._objAutoliquidacionDalc = new AutoliquidacionDalc();
                    return this._objAutoliquidacionDalc.ObtenerCobrosSolicitudLiquidacion(p_intSolicitudLiquidacionID);
                }

        
            #endregion


            #region Insertar


                /// <summary>
                /// Generar la liquidación correspondiente a la solicitud de liquidación
                /// </summary>
                /// <param name="p_objSolicitudLiquidacion">SolicitudLiquidacionEntity con la información de la solicitud</param>
                /// <returns>int con el identificador de la solicitud generada</returns>
                public int RadicarSolicitudLiquidacion(SolicitudLiquidacionEntity p_objSolicitudLiquidacion)
                {
                    int intIdentificadorSolicitudID = 0;
                    string strNumeroVital = "";                    
                    TipoSolicitudLiquidacionDalc objTipoSolicitudDalc = null;

                    try
                    {
                        //Se asigna estado de pendiente de radicación
                        p_objSolicitudLiquidacion.EstadoSolicitud = new EstadoSolicitudLiquidacionEntity { EstadoSolicitudID = (int)EstadoSolicitudEnum.Solicitud_Pendiente_Radicacion_Solicitud };

                        //Grabar la solicitud en la base de datos
                        this._objAutoliquidacionDalc = new AutoliquidacionDalc();
                        intIdentificadorSolicitudID = this._objAutoliquidacionDalc.InsertarSolicitudAutoliquidacion(p_objSolicitudLiquidacion);

                        //Verificar que se obtenga el identificador de la solicitud
                        if (intIdentificadorSolicitudID > 0)
                        {
                            //Consultar configuración de tipo de solicitud
                            objTipoSolicitudDalc = new TipoSolicitudLiquidacionDalc();
                            p_objSolicitudLiquidacion.TipoSolicitud = objTipoSolicitudDalc.ConsultarTipoSolicitud(p_objSolicitudLiquidacion.TipoSolicitud.TipoSolicitudID);

                            //Radicar solicitud en VITAL
                            strNumeroVital = this.RegistroVital(p_objSolicitudLiquidacion.TipoSolicitud.FormularioID, p_objSolicitudLiquidacion.SolicitanteID, p_objSolicitudLiquidacion.SolicitudLiquidacionID, p_objSolicitudLiquidacion.AutoridadAmbiental.IdAutoridad);

                            //Actualizar el número vital en la solicitud
                            this._objAutoliquidacionDalc.ModificarNumeroVitalSolicitudLiquidacion(p_objSolicitudLiquidacion.SolicitudLiquidacionID, strNumeroVital);

                            //Actualizar estado
                            this._objAutoliquidacionDalc.ModificarEstadoSolicitudLiquidacion(p_objSolicitudLiquidacion.SolicitudLiquidacionID, (int)EstadoSolicitudEnum.Solicitud_Radicada);
                            
                            //Enviar correo de solicitud
                            this.EnviarCorreoRadicacionSolicitud(p_objSolicitudLiquidacion);
                        }
                        else
                        {
                            throw new Exception("No se pudo obtener el identificador de la solicitud de radicación");
                        }
                    }
                    catch (RadicacionAutoliquidacionException exc)
                    {
                        //Escalar excepción
                        throw exc;
                    }
                    catch (Exception exc)
                    {
                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "Autoliquidacion :: RadicarSolicitudLiquidacion -> Error bd: " + exc.Message + " - " + exc.StackTrace);

                        //Escalar error
                        throw new AutoliquidacionException("Se presento un error no esperado durante el proceso de generación de la solicitud. " + exc.Message, exc);
                    }

                    return intIdentificadorSolicitudID;
                }

            #endregion


            #region Modificar

                /// <summary>
                /// Reenviar la solicitud de liquidación indicado para realizar las tareas que se encuentren penidentes por realizar
                /// </summary>
                /// <param name="p_intSolicitudLiquidacion">int con el identificador de la solicitud a reenviar</param>
                public void ReenviarSolicitudLiquidacion(int p_intSolicitudLiquidacion)
                {
                    string strNumeroVital = "";
                    SolicitudLiquidacionEntity objSolicitudLiquidacionEntity = null;
                    TipoSolicitudLiquidacionDalc objTipoSolicitudDalc = null;

                    try
                    {
                        //Se consulta solicitud de liquidacion
                        objSolicitudLiquidacionEntity = this.ConsultarSolicitudLiquidacion(p_intSolicitudLiquidacion);

                        //Verificar que se obtenga información de la solicitud de liquidación
                        if (objSolicitudLiquidacionEntity != null && objSolicitudLiquidacionEntity.SolicitudLiquidacionID > 0)
                        {
                            //Verificar si el estado pendiente es registro
                            if (objSolicitudLiquidacionEntity.EstadoSolicitud.EstadoSolicitudID == (int)EstadoSolicitudEnum.Solicitud_Pendiente_Radicacion_Solicitud)
                            {

                                //Consultar configuración de tipo de solicitud
                                objTipoSolicitudDalc = new TipoSolicitudLiquidacionDalc();
                                objSolicitudLiquidacionEntity.TipoSolicitud = objTipoSolicitudDalc.ConsultarTipoSolicitud(objSolicitudLiquidacionEntity.TipoSolicitud.TipoSolicitudID);

                                //Radicar solicitud en VITAL
                                strNumeroVital = this.RegistroVital(objSolicitudLiquidacionEntity.TipoSolicitud.FormularioID, objSolicitudLiquidacionEntity.SolicitanteID, objSolicitudLiquidacionEntity.SolicitudLiquidacionID, objSolicitudLiquidacionEntity.AutoridadAmbiental.IdAutoridad);

                                //Actualizar el número vital en la solicitud
                                this._objAutoliquidacionDalc.ModificarNumeroVitalSolicitudLiquidacion(objSolicitudLiquidacionEntity.SolicitudLiquidacionID, strNumeroVital);

                                //Actualizar estado
                                this._objAutoliquidacionDalc.ModificarEstadoSolicitudLiquidacion(objSolicitudLiquidacionEntity.SolicitudLiquidacionID, (int)EstadoSolicitudEnum.Solicitud_Radicada);

                                //Enviar correo de solicitud
                                this.EnviarCorreoRadicacionSolicitud(objSolicitudLiquidacionEntity);
                            }                            
                        }
                        else
                        {
                            throw new Exception("No se pudo obtener información de la solicitud de liquidación " + p_intSolicitudLiquidacion.ToString());
                        }
                    }
                    catch (RadicacionAutoliquidacionException exc)
                    {
                        //Escalar excepción
                        throw exc;
                    }                    
                    catch (Exception exc)
                    {
                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "Autoliquidacion :: ReenviarSolicitudLiquidacion -> Error bd: " + exc.Message + " - " + exc.StackTrace);

                        //Escalar error
                        throw new AutoliquidacionException("Se presento un error no esperado durante el proceso de reenvío de la solicitud. " + exc.Message, exc);
                    }
                }


            #endregion


        #endregion

    }
}
