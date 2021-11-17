using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using SILPA.LogicaNegocio.BPMProcessL;
using SILPA.LogicaNegocio.Generico;
using SILPA.Servicios.BPMProcess.Entidades;
using SILPA.LogicaNegocio.DAA;
using SILPA.AccesoDatos.DAA;
using SoftManagement.Log;
using Silpa.Workflow;
using Silpa.Workflow.Entidades;
using SILPA.Servicios.SolicitudDAA;

namespace SILPA.Servicios.BPMProcess
{
    public class BPMProcess
    {
        //private TipoUsuarioIdentity Identity;
        //private TipoUsuarioDalc objTipoUsuarioDalc;
        //public TipoUsuarioIdentity TipousuarioIdentity
        //{
        //    get { return this.Identity; }
        //}
        //public List<TipoUsuarioIdentity> CargarTipoUsuarios()
        //{
        //    this.objTipoUsuarioDalc = new TipoUsuarioDalc();
        //    return objTipoUsuarioDalc.ObtenerUsuarios();
        //}

        public string CrearProcesoBPM(string ClientId,Int64 FormId,Int64 UserID,string ValoresXML)
        {
            BpmProcessLn objBPMProcess = new BpmProcessLn();
            return objBPMProcess.crearProceso(ClientId,FormId,UserID,ValoresXML);
            //BPMServices.GattacaBPMServices9000 objBPMServices = new BPMServices();
            //Int64 idProcessCase = objBPMServices.GetIdProcessCase(FormId);
            //Int64 processinstance=objBPMServices.WMCreateProcessInstance(ClientId,UserID,idProcessCase,0,"VBFormBuilder",forminstance.ToString(),FormId.ToString());
            //Int64 activityInstance = objBPMServices.WMStartProcessInstance(ClientId, UserID, processinstance);            
            //if (objBPMServices.AttachDataToActivityInstance(ClientId, UserID, activityInstance, processinstance, "VBFormBuilder", forminstance.ToString(), FormId.ToString()))
            //{
            //    return objBPMProcess.NumeroSilpaxIdProcessInstance(processinstance);
            //}

            //return "Error al crear el proceso";
        }

        public string CrearProcesoAutoridad(Int64 TramiteId, Int64 PerId,Int64 AA, string ValoresXML)
        {
            try
            {
                BpmProcessLn objBPMProcess = new BpmProcessLn();
                return objBPMProcess.crearProcesoAutoridad(TramiteId, PerId, AA, ValoresXML);
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al Crear Proceso de Autoridad.";
                throw new Exception(strException, ex);
            }
        }
        public string ObtenerFormularios()
        {
            BpmProcessLn objBPMProcess = new BpmProcessLn();
            return objBPMProcess.ObtenerFormularios();
        }


        public string ObtenerCampos(Int64 idForm)
        {
            BpmProcessLn objBPMProcess = new BpmProcessLn();
            return objBPMProcess.ObtenerCampos(idForm);
        }

        //private BpmProcess BpmProcess()
        //{
        //    throw new Exception("The method or operation is not implemented.");
        //}


        /// <summary>
        /// Registrar un formulario en el sistema de VITAL. La autoridad se calcula de manera automatica de acuerdo a los datos de la entidad
        /// </summary>
        /// <param name="p_strUsarioTransaccion">string con el usuario gattaca de transaccion</param>
        /// <param name="p_lngFormularioID">long con el identificador del formulario a radicar</param>
        /// <param name="p_lngUsuarioID">long con el identificador del usuario</param>
        /// <param name="p_xmlFormulario">string con los datos del formulario a radicar</param>
        /// <returns>string con la infromacion producto del registro del proceso</returns>
        public string RegistrarFormularioProceso(string p_strUsarioTransaccion, long p_lngFormularioID, long p_lngUsuarioID, string p_xmlFormulario)
        {
            BpmProcessLn objProceso = null; 
            RadicadoSolicitudTramiteEntity objRadicado = new RadicadoSolicitudTramiteEntity();
            RadicacionDocumento objRadicadoDocumento = null;
            SolicitudDAAEIA objSolicitud = null;
            Configuracion objConfiguracion = null;

            try
            {
                //Crear proceso de proceso
                objProceso = new BpmProcessLn();
                objRadicado.NumeroVital = objProceso.crearProceso(p_strUsarioTransaccion, p_lngFormularioID, p_lngUsuarioID, p_xmlFormulario);

                //Verificar si se genero numero vital
                if (string.IsNullOrEmpty(objRadicado.NumeroVital) || objRadicado.NumeroVital.ToLower().Contains("error"))
                {
                    throw new Exception("No se genero el numero VITAL durante el proceso de radicacion");
                }
                else
                {
                    //Obtener informacion de la solicitud
                    objSolicitud = new SolicitudDAAEIA();
                    objSolicitud.ConsultarSolicitudNumeroSILPA(objRadicado.NumeroVital);

                    if (objSolicitud.Identity != null)
                    {

                        //Obtener la informacion de la radicacion
                        objRadicadoDocumento = new RadicacionDocumento();
                        objRadicadoDocumento.ObtenerRadicacionNumeroVital(objRadicado.NumeroVital);

                        if (objRadicadoDocumento._objRadDocIdentity != null)
                        {
                            //Cargar los datos
                            objRadicado.SolicitudID = objSolicitud.Identity.IdSolicitud;
                            objRadicado.TramiteID = objSolicitud.Identity.IdTipoTramite;
                            objRadicado.NumeroSilpa = objRadicadoDocumento._objRadDocIdentity.NumeroSilpa;
                            objRadicado.RadicadoID = objRadicadoDocumento._objRadDocIdentity.Id;
                            objRadicado.DescripcionRadicado = objRadicadoDocumento._objRadDocIdentity.DescRadicacion;
                            objRadicado.PathDocumentos = objRadicadoDocumento._objRadDocIdentity.UbicacionDocumento;

                            //Quitar ruta de referencia FILETRAFFIC
                            objConfiguracion = new Configuracion();
                            objRadicado.PathDocumentos = objRadicado.PathDocumentos.Replace(objConfiguracion.FileTraffic, "");
                        }
                        else 
                        {
                            throw new Exception("No se obtuvo informacion del radicado generado");
                        }
                    }
                    else
                    {
                        throw new Exception("No se obtuvo informacion de la solicitud generada");
                    }

                }

            }
            catch (Exception exc)
            {
                SMLog.Escribir(Severidad.Critico, "Se presento un error durante la radicacion del formulario. Datos:" +
                                                  "p_strUsarioTransaccion: " + (p_strUsarioTransaccion ?? "null") + " - p_lngFormularioID: " + p_lngFormularioID.ToString() +
                                                  " -- p_lngUsuarioID: " + p_lngUsuarioID.ToString() + " -- p_xmlFormulario: " + (p_xmlFormulario ?? "null") + " ----- ERRROR: " + exc.ToString() + " -- " + exc.StackTrace.ToString() );
                throw exc;
            }

            return objRadicado.GetXml();
        }

        /// <summary>
        /// Finaliza el proceso activo relacionado al numero VITAL
        /// </summary>
        /// <param name="p_strNumeroVITAL">string con el numero VITAL especificado</param>
        /// <returns>string con la informacion del registro de radicacion generado en la finalizacion del proceso</returns>
        public string FinalizarProcesoNumeroVITAL(string p_strNumeroVITAL)
        {
            BpmProcessLn objProceso = null;
            RadicadoSolicitudTramiteEntity objRadicado = new RadicadoSolicitudTramiteEntity();
            SolicitudDAAEIA objSolicitud = null;
            ServicioWorkflow objServicioWorkflow;
            ActividadInfo objActividadInfo;
            RadicacionDocumento objRadicadoDocumento = null;
            Configuracion objConfiguracion = null;
            SolicitudFachada objSolicitudFachada = null;

            try
            {
                //Obtener informacion de la solicitud
                objSolicitud = new SolicitudDAAEIA();
                objSolicitud.ConsultarSolicitudNumeroSILPA(p_strNumeroVITAL);

                //Verificar si se obtuvo informacion de la solicitud
                if (objSolicitud.Identity != null)
                {
                    objServicioWorkflow = new ServicioWorkflow();
                    objActividadInfo = objServicioWorkflow.ConsultarActividadActual(objSolicitud.Identity.IdProcessInstance);

                    //Verificar que se obtenga proceso
                    if (objActividadInfo != null)
                    {
                        //Se genera la radicacion del proceso
                        objSolicitudFachada = new SolicitudFachada();
                        objSolicitudFachada.RadicarSolicitud(objSolicitud.Identity.IdProcessInstance, objSolicitud.Identity.IdAutoridadAmbiental, objSolicitud.Identity.IdSolicitante);
                        
                        //Cerrar actividad
                        objProceso = new BpmProcessLn();
                        objProceso.CerrarActividad(Convert.ToInt32(objSolicitud.Identity.IdSolicitante), objActividadInfo.ActivityInstance.Value, Convert.ToInt32(objSolicitud.Identity.IdProcessInstance));

                        //Obtener la informacion de la radicacion
                        objRadicadoDocumento = new RadicacionDocumento();
                        objRadicadoDocumento.ObtenerRadicacionNumeroVital(p_strNumeroVITAL);

                        if (objRadicadoDocumento._objRadDocIdentity != null)
                        {
                            //Cargar los datos
                            objRadicado.SolicitudID = objSolicitud.Identity.IdSolicitud;
                            objRadicado.TramiteID = objSolicitud.Identity.IdTipoTramite;
                            objRadicado.NumeroSilpa = objRadicadoDocumento._objRadDocIdentity.NumeroSilpa;
                            objRadicado.RadicadoID = objRadicadoDocumento._objRadDocIdentity.Id;
                            objRadicado.DescripcionRadicado = objRadicadoDocumento._objRadDocIdentity.DescRadicacion;
                            objRadicado.PathDocumentos = objRadicadoDocumento._objRadDocIdentity.UbicacionDocumento;

                            //Quitar ruta de referencia FILETRAFFIC
                            objConfiguracion = new Configuracion();
                            objRadicado.PathDocumentos = objRadicado.PathDocumentos.Replace(objConfiguracion.FileTraffic, "");
                        }
                        else 
                        {
                            throw new Exception("No se obtuvo informacion del radicado generado");
                        }
                    }
                }
                else
                {
                    throw new Exception("No se existe informacion relacionada al numero VITAL");
                }

            }
            catch (Exception exc)
            {
                SMLog.Escribir(Severidad.Critico, "Se presento un error durante el cierre de actividades. Datos:" +
                                                  "p_strNumeroVITAL: " + (p_strNumeroVITAL ?? "null"));
                throw exc;
            }

            return objRadicado.GetXml();
        }


        /// <summary>
        /// Obtiene el listado de los registros que se encuentran pendientes de radicación en SIGPRO - (12082020 - FRAMIREZ)
        /// </summary>
        /// <param name="identificadorAutoridadAmbiental">int con el identificador de la autoridad ambiental</param>
        /// <param name="fechaInicial">string con la fecha inicial de busqueda</param>
        /// <param name="fechaFinal">string con la fecha final de busqueda</param>
        /// <returns>dataset con la informacion de los registros que se encuentran pendientes de radicar en SIGPRO</returns>

        public List<RegistroRadicarSigproEntity> ObtenerRegistrosPendientesRadicacionSigpro(int identificadorAutoridadAmbiental, string fechaInicial, string fechaFinal)
        {
            var listaRegistros = new List<RegistroRadicarSigproEntity>();
           
            try
            {
                var FechaInicio = new DateTime();
                var FechaFin = new DateTime(); 
                try
                {
                    FechaInicio = DateTime.ParseExact(fechaInicial, "yyyy-MM-dd HH:mm:ss", null); ;
                    FechaFin = DateTime.ParseExact(fechaFinal, "yyyy-MM-dd HH:mm:ss", null);                    
                }
                catch
                {
                    throw new Exception("Formato de fechas no valido, verifique que se envíen en el formato yyyy-MM-dd HH:mm:ss");
                }


                if (FechaFin < FechaInicio)
                {
                    throw new Exception("La fecha inicial debe ser menor a la fecha final");
                }

                DAA daa = new DAA();
                listaRegistros = (List<RegistroRadicarSigproEntity>)daa.ObtenerRegistrosPendientesRadicacionSigpro(identificadorAutoridadAmbiental, fechaInicial, fechaFinal);

                // Verificar si la lista tiene registros
                if (listaRegistros.Count > 0)
                {
                    // Recorrer listado para busqueda de rutas de archivos a copiar, almacenar en la propiedad (RutasArchivosCopiar)
                    foreach (var registro in listaRegistros)
                    {
                       var listasArchivos = daa.RetornarListadoArchivosRadicacionSigpro(registro.PathDocumento, registro.TramiteVitalId, registro.NumeroSilpa);
                       registro.RutasArchivosTodosCopiar = listasArchivos[0];
                       registro.RutasArchivosBaseCopiar = listasArchivos[1];
                       registro.RutasArchivosAdicionalesCopiar = listasArchivos[2];
                    }
                }                
            }
            catch (Exception exc)
            {
                SMLog.Escribir(Severidad.Critico, "Error en la consulta de registros pendientes por radicar en SIGPRO.");
                throw exc;
            }
            return listaRegistros;
        }

    }
}
