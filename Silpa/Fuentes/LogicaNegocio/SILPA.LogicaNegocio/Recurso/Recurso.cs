using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.AccesoDatos.RecursoReposicion;
using System.Data;
using SILPA.Comun;
using SILPA.LogicaNegocio.ServiciosREST.Servicios.ANLA.Notificaciones;
using SILPA.LogicaNegocio.ServiciosREST.Servicios.ANLA.Notificaciones.Entidades;

namespace SILPA.LogicaNegocio.Recurso
{
    public class Recurso
    {

        /// <summary>
        /// Insertar un nuevo recurso de reposición
        /// </summary>
        /// <param name="objRecurso">RecursoEntity con la información del recurso</param>
        /// <param name="expedientePadre">string con el expediente</param>
        /// <param name="vitalAdicional">string con el numero vital adicional</param>
        /// <param name="vitalPadre">string con el numero vital original</param>
        /// <param name="vitalGenerado">string con el numero vital de radicdo de recurso</param>
        /// <param name="numeroActo">string con el numero de acto</param>
        public void InsertarRecursoExtendido(ref RecursoEntity objRecurso, string expedientePadre,
                                             string vitalAdicional, string vitalPadre, string vitalGenerado, string numeroActo)
        {
            RecursoDalc objRecursoDalc = new RecursoDalc();
            objRecursoDalc.InsertarRecursoExtendido(ref objRecurso, expedientePadre, vitalAdicional, vitalPadre, vitalGenerado, numeroActo);
        }


        /// <summary>
        /// Retornar el listado de actos administrativos para recurso de reposición para una persona que cumpla con los parametros de busqueda
        /// </summary>
        /// <param name="p_lngIDApplicationUser">long con el identificador del usuario que realiza la consulta</param>
        /// <param name="p_strNumeroVital">string con el numero VITAL. Opcional</param>
        /// <param name="p_strExpediente">string con el codigo del expediente. Opcional</param>
        /// <param name="p_strNumeroActo">string con el numero de acto. Opcional</param>
        /// <param name="p_intAutoridadAmbientalID">int con el identificador de la autoridad ambiental. Opcional</param>
        /// <param name="p_objFechaActoDesde">DateTime con la fecha inicial del rango</param>
        /// <param name="p_objFechaActoHasta">DateTime con la fecha final del rango</param>        
        /// <returns>Lista de objeto RecursoParaPresentarEntity List<RecursoParaPresentarEntity></returns>
        public List<ActoParaRecursoEntity> ObtenerListadoActosAdministrativosRecursoPersona(long p_lngIDApplicationUser, string p_strNumeroVital, string p_strExpediente,
                                                                        string p_strNumeroActo, int p_intAutoridadAmbientalID,
                                                                        DateTime p_objFechaActoDesde, DateTime p_objFechaActoHasta)
        {
            List<ActoParaRecursoEntity> lstActoParaRecursoEntity;
            lstActoParaRecursoEntity = new List<ActoParaRecursoEntity>();
            try
            {
                if (p_intAutoridadAmbientalID != (int)AutoridadesAmbientales.ANLA)
                {
                    RecursoDalc objRecursoDalc = new RecursoDalc();
                    lstActoParaRecursoEntity = objRecursoDalc.ObtenerListadoActosAdministrativosRecursoPersona(p_lngIDApplicationUser, p_strNumeroVital, p_strExpediente,
                                                                                           p_strNumeroActo, p_intAutoridadAmbientalID,
                                                                                           p_objFechaActoDesde, p_objFechaActoHasta);
                }
                else if (p_intAutoridadAmbientalID == (int)AutoridadesAmbientales.ANLA)
                {
                    RecursoReposicionService objRecursoReposicionService;
                    FormaConsultaActoParaRecursoEntity objFormaConsultaActoParaRecursoEntity;
                    List<RecursoParaPresentarEntity> lstRecursoParaPresentarEntity;
                    
                    lstRecursoParaPresentarEntity = new List<RecursoParaPresentarEntity>();
                    objRecursoReposicionService = new RecursoReposicionService();

                    objFormaConsultaActoParaRecursoEntity = new FormaConsultaActoParaRecursoEntity
                    {
                            IDApplicationUser = p_lngIDApplicationUser,
                            NumeroVITAL = p_strNumeroVital,
                            Expediente = p_strExpediente,
                            NumeroActoAdministrativo = p_strNumeroActo,
                            FechaActoDesde = p_objFechaActoDesde,
                            FechaActoHasta = p_objFechaActoHasta
                    };
                    lstRecursoParaPresentarEntity = objRecursoReposicionService.ObtenerListadoActosAdministrativosRecursoPersona(objFormaConsultaActoParaRecursoEntity);
                    foreach (RecursoParaPresentarEntity itemRecursoParaPresentarEntity in lstRecursoParaPresentarEntity)
                    {
                        lstActoParaRecursoEntity.Add(convertirRespuesta(itemRecursoParaPresentarEntity));
                    }
                }
                return lstActoParaRecursoEntity;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            
        }


        /// <summary>
        /// Obtener el documento correspondiente al acto administrativo sobre el cual se presenta el recurso de reposicion
        /// </summary>
        /// <param name="p_intActoNotificacionID">int con el identificador del acto administrativo</param>
        /// <returns>ArchivoRecursoEntity con la informacion del archivo</returns>
        public ArchivoRecursoEntity ObtenerArchivoRecursoANLA(int p_intActoNotificacionID)
        {
            RecursoReposicionService objRecursoReposicionService;
            ArchivoRecursoEntity objArchivoRecursoEntity = null;

            try
            {
                objRecursoReposicionService = new RecursoReposicionService();
                objArchivoRecursoEntity = objRecursoReposicionService.ObtenerDocumentoRecursoReposicion(p_intActoNotificacionID);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return objArchivoRecursoEntity;
        }

        #region Metodos privados
        /// <summary>
        /// Coniverte el objeto repuesta del consumo al objeto local recurso
        /// </summary>
        /// <param name="objRecursoParaPresentarEntity"></param>
        /// <returns></returns>
        private ActoParaRecursoEntity convertirRespuesta(RecursoParaPresentarEntity objRecursoParaPresentarEntity)
        {
            return new ActoParaRecursoEntity()
            {
                ActoNotificacionID = objRecursoParaPresentarEntity.ActoNotificacionID,
                Autoridad = objRecursoParaPresentarEntity.Autoridad,
                AutoridadID = objRecursoParaPresentarEntity.AutoridadID,
                EstadoActualID = objRecursoParaPresentarEntity.EstadoActualID,
                EstadoFuturoID = objRecursoParaPresentarEntity.EstadoFuturoID,
                Expediente = objRecursoParaPresentarEntity.Expediente,
                FechaActoAdministrativo = objRecursoParaPresentarEntity.FechaActoAdministrativo,
                FechaNotificacion = objRecursoParaPresentarEntity.FechaNotificacion,
                FlujoID = objRecursoParaPresentarEntity.FlujoID,
                IdentificacionUsuario = objRecursoParaPresentarEntity.IdentificacionUsuario,
                NumeroActoAdministrativo = objRecursoParaPresentarEntity.NumeroActoAdministrativo,
                NumeroVITAL = objRecursoParaPresentarEntity.NumeroVITAL,
                PersonaID = objRecursoParaPresentarEntity.PersonaID,
                RutaDocumento = objRecursoParaPresentarEntity.RutaDocumento,
                EstadoPersonaActoID = objRecursoParaPresentarEntity.EstadoPersonaActoID
            };
        }
        #endregion
        
        

        
    }
}
