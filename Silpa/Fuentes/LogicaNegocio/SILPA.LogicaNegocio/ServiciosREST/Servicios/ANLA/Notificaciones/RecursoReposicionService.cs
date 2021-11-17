using SILPA.Comun;
using SILPA.LogicaNegocio.ServiciosREST.Entidades;
using SILPA.LogicaNegocio.ServiciosREST.Enum;
using SILPA.LogicaNegocio.ServiciosREST.Servicios.ANLA.Notificaciones.Entidades;
using SoftManagement.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.ServiciosREST.Servicios.ANLA.Notificaciones
{
    public class RecursoReposicionService
    {
        #region Objetos

            /// <summary>
            /// Informacion del servicio
            /// </summary>
            private ServicioIntegracionRest _objServicioIntegracion;

        #endregion


        #region Creadora

        /// <summary>
        /// Creadora
        /// </summary>
        public RecursoReposicionService()
        {
            this._objServicioIntegracion = ServicioIntegracionRest.GetInstance((int)ServiciosIntegradosEnum.ANLA_RecursoReposicion);
        }

        #endregion


        #region Metodos Publicos

            /// <summary>
            /// Registra un proceso de notificaciones
            /// </summary>
            /// <param name="p_objFormaREgistroNotificaciones">FormaRegistroNotificacionEntity con la informacion del proceso de notificacion</param>
            /// <returns>string con mensaje de error en caso de que se presente</returns>
            public List<RecursoParaPresentarEntity> ObtenerListadoActosAdministrativosRecursoPersona(FormaConsultaActoParaRecursoEntity p_objFormaREgistroNotificaciones)
            {
                ServicioREST objServicio = null;
                RespuestaServicioEntity objRespuestaServicio = null;
                List<RecursoParaPresentarEntity> objetorespuesta = new List<RecursoParaPresentarEntity>() ;

                try
                {
                    //Generar token
                    objServicio = new ServicioREST();
                    objRespuestaServicio = objServicio.Ejecutar<List<RecursoParaPresentarEntity>>(MetodosConexionREST.POST, this._objServicioIntegracion.Servicio.URLServicio + "ObtenerListadoActosAdministrativosRecursoPersona", this._objServicioIntegracion.TokenServicio, p_objFormaREgistroNotificaciones);
                    
                    //Verificar que el proceso finalice correctamente
                    if (objRespuestaServicio != null && objRespuestaServicio.CodigoRespuesta == CodigosRespuestaREST.OK)
                    {
                        objetorespuesta = (List<RecursoParaPresentarEntity>)objRespuestaServicio.ObjetoRespuesta;
                    }
                    else
                    {
                        throw new Exception("Error durante el proceso de Consulta de actos administrativos recurso. Codigo: " + (objRespuestaServicio != null ? objRespuestaServicio.CodigoRespuesta.ToString() : "null"));
                    }

                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "RecursoReposicionService :: ObtenerListadoActosAdministrativosRecursoPersona -> Error obteniendo listado de actos administrativos recurso: " + exc.Message + " " + exc.StackTrace);

                    throw exc;
                }

                return objetorespuesta;
            }


            /// <summary>
            /// Registra un proceso de notificaciones
            /// </summary>
            /// <param name="p_objFormaREgistroNotificaciones">FormaRegistroNotificacionEntity con la informacion del proceso de notificacion</param>
            /// <returns>string con mensaje de error en caso de que se presente</returns>
            public ArchivoRecursoEntity ObtenerDocumentoRecursoReposicion(int p_intActoNotificacionID)
            {
                ServicioREST objServicio = null;
                RespuestaServicioEntity objRespuestaServicio = null;
                ArchivoRecursoEntity objArchivoRecursoEntity;

                try
                {
                    //Generar token
                    objServicio = new ServicioREST();
                    objRespuestaServicio = objServicio.Ejecutar<ArchivoRecursoEntity>(MetodosConexionREST.POST, this._objServicioIntegracion.Servicio.URLServicio + "ObtenerDocumentoRecursoReposicion", this._objServicioIntegracion.TokenServicio, p_intActoNotificacionID);

                    //Verificar que el proceso finalice correctamente
                    if (objRespuestaServicio != null && objRespuestaServicio.CodigoRespuesta == CodigosRespuestaREST.OK)
                    {
                        objArchivoRecursoEntity = (ArchivoRecursoEntity)objRespuestaServicio.ObjetoRespuesta;
                    }
                    else
                    {
                        throw new Exception("Error durante el proceso de consulta del archivo. Codigo: " + (objRespuestaServicio != null ? objRespuestaServicio.CodigoRespuesta.ToString() : "null"));
                    }

                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "RecursoReposicionService :: ObtenerDocumentoRecursoReposicion -> Error obteniendo documento: " + exc.Message + " " + exc.StackTrace);

                    throw exc;
                }

                return objArchivoRecursoEntity;
            }


            /// <summary>
            /// Obtener informacion del acto sobre el cual se presenta recurso
            /// </summary>
            /// <param name="p_intActoNotificacionID">int con el id del acto</param>
            /// <param name="p_intPersonaID">int con el id de la persona</param>
            /// <returns>ActoPersonaRecursoEntity con la informacion</returns>
            public ActoPersonaRecursoEntity ObtenerActoAdministrativoRecursoPersona(int p_intActoNotificacionID, int p_intPersonaID)
            {
                ServicioREST objServicio = null;
                RespuestaServicioEntity objRespuestaServicio = null;
                FormaObtenerActoRecursoPersonaEntity objFormaObtenerActoRecursoPersonaEntity;
                ActoPersonaRecursoEntity objActoPersonaRecursoEntity;

                try
                {
                    //Cargar datos
                    objFormaObtenerActoRecursoPersonaEntity = new FormaObtenerActoRecursoPersonaEntity
                    {
                        ActoNotificacionID = p_intActoNotificacionID,
                        PersonaID = p_intPersonaID
                    };

                    //Generar token
                    objServicio = new ServicioREST();
                    objRespuestaServicio = objServicio.Ejecutar<ActoPersonaRecursoEntity>(MetodosConexionREST.POST, this._objServicioIntegracion.Servicio.URLServicio + "ObtenerActoAdministrativoRecursoPersona", this._objServicioIntegracion.TokenServicio, objFormaObtenerActoRecursoPersonaEntity);

                    //Verificar que el proceso finalice correctamente
                    if (objRespuestaServicio != null && objRespuestaServicio.CodigoRespuesta == CodigosRespuestaREST.OK)
                    {
                        objActoPersonaRecursoEntity = (ActoPersonaRecursoEntity)objRespuestaServicio.ObjetoRespuesta;
                    }
                    else
                    {
                        throw new Exception("Error durante el proceso de consulta del acto administrativo. Codigo: " + (objRespuestaServicio != null ? objRespuestaServicio.CodigoRespuesta.ToString() : "null"));
                    }

                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "RecursoReposicionService :: ObtenerActoAdministrativoRecursoPersona -> Error obteniendo datos del acto administrativo: " + exc.Message + " " + exc.StackTrace);

                    throw exc;
                }

                return objActoPersonaRecursoEntity;
            }


            /// <summary>
            /// Registra el acto para presentar recurso de reposicion
            /// </summary>
            /// <param name="p_objFormaPresentarRecursoReposicionEntity"></param>
            public string PresentarRecursoReposicion(FormaPresentarRecursoReposicionEntity p_objFormaPresentarRecursoReposicionEntity)
            {
                ServicioREST objServicio = null;
                RespuestaServicioEntity objRespuestaServicio = null;
                string respuesta = string.Empty;
                try
                {
                    //Generar token
                    objServicio = new ServicioREST();
                    objRespuestaServicio = objServicio.Ejecutar(MetodosConexionREST.POST, this._objServicioIntegracion.Servicio.URLServicio + "PresentarActoParaRecursoResposicion", this._objServicioIntegracion.TokenServicio, p_objFormaPresentarRecursoReposicionEntity);

                    //Verificar que el proceso finalice correctamente
                    if (objRespuestaServicio != null && objRespuestaServicio.CodigoRespuesta == CodigosRespuestaREST.OK)
                    {
                        respuesta = (string)objRespuestaServicio.ObjetoRespuesta;
                    }
                    else
                    {
                        throw new Exception("Error durante el proceso de Consulta de actos administrativos recurso. Codigo: " + (objRespuestaServicio != null ? objRespuestaServicio.CodigoRespuesta.ToString() : "null"));
                    }
                    return respuesta;

                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "RecursoReposicionService :: ObtenerListadoActosAdministrativosRecursoPersona -> Error obteniendo listado de actos administrativos recurso: " + exc.Message + " " + exc.StackTrace);

                    throw exc;
                }
            }

        #endregion

    }
}
