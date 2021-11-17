using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SILPA.LogicaNegocio.Generico;
using SILPA.AccesoDatos.Notificacion;
using SILPA.AccesoDatos.DAA;
using SILPA.AccesoDatos.Generico;
using SILPA.Comun;
using Silpa.Workflow;
using SoftManagement.Log;
using SILPA.AccesoDatos.Publicacion;
using SILPA.LogicaNegocio.Notificacion;


namespace SILPA.Servicios
{
    public class NotificacionFachada
    {

        /// <summary>
        /// Consulta un estado de notificación específico llamado desde la AA
        /// </summary>
        /// <param name="xmlDatosConsulta"></param>
        /// <returns></returns>
        public string ConsultarNotificacion(string xmlDatosConsulta)
        {
            SILPA.LogicaNegocio.Notificacion.Notificacion notnot = new SILPA.LogicaNegocio.Notificacion.Notificacion();
            SILPA.AccesoDatos.Notificacion.PersonaNotificarEntity persona = new PersonaNotificarEntity();
            persona.PrimerNombre = "Juan";
            persona.PrimerApellido = "Sanchez";
            return "*";
            //notnot.GetXMLGELEstadoNotificacionSalida(xmlDatosConsulta, ref persona);


        }

        //Modificar el método ActualizarProcesos en NotificacionFachada.cs
        /// <summary>
        /// Actualiza todos los procesos Pendientes
        /// </summary>
        /// <remarks>Este método se ejecuta a través de un servicio de Windows o un servicio que tiene un llamado constante en un horario específico</remarks>
        public List<string> ActualizarProcesos(bool p_blnLogActivo = false )
        {
            if (p_blnLogActivo)
                SMLog.Escribir(Severidad.Informativo, "----Inicia el proceso de ActualizarProcesos");

            //1. Consultar La lista de Actos cuyo estado sea: 1, 2, 9, 10
            SILPA.Servicios.BPMServices.GattacaBPMServices9000 _servicioBPM = new SILPA.Servicios.BPMServices.GattacaBPMServices9000();

            Proceso _objProceso = new Proceso();
            List<NotificacionEntity> listaNotificacion = new List<NotificacionEntity>();
            NotificacionDalc _objNotificacionDalc = new NotificacionDalc();
            EstadoNotificacionEntity estado = new EstadoNotificacionEntity();
            EstadoNotificacionDalc _estadoDalc = new EstadoNotificacionDalc();
            PersonaNotificarDalc _personaDalc = new PersonaNotificarDalc();
            //Consulta todos los estados que no sean EJECUTORIADO
            ParametroEntity _parametro = new ParametroEntity();
            ParametroDalc _parametroDalc = new ParametroDalc();

            _parametro.IdParametro = -1;
            _parametro.NombreParametro = "EJECUTORIADA";
            _parametroDalc.obtenerParametros(ref _parametro);

            estado.ID = Convert.ToInt32(_parametro.Parametro);
            listaNotificacion = _objNotificacionDalc.ObtenerActosParaConsultarPDI(estado);
            SolicitudDAAEIAIdentity _solicitud = new SolicitudDAAEIAIdentity();
            SolicitudDAAEIADalc _solicitudDalc = new SolicitudDAAEIADalc();
            BpmParametros _bpmParametrosDalc = new BpmParametros();
            SILPA.LogicaNegocio.Notificacion.Notificacion consultaNotificacion = new SILPA.LogicaNegocio.Notificacion.Notificacion();
            List<PersonaNotificarEntity> listaPersonas;
            List<string> listaConsultas = new List<string>();
            bool _todosNotificados = false;



            for (int indiceActual = 0; indiceActual < listaNotificacion.Count; indiceActual++)
            {
                NotificacionEntity not = listaNotificacion[indiceActual];

                this.ConsultarTodosPublicables(not);

                string mensaje = "";
                try
                {
                    listaPersonas = new List<PersonaNotificarEntity>();
                    listaPersonas = _personaDalc.ObtenerPersonasEstado(not, estado);

                    foreach (PersonaNotificarEntity per in listaPersonas)
                    {
                        mensaje = string.Format("persona {0} estado {1}", per.NumeroIdentificacion.ToString(), per.EstadoNotificado.Estado);
                        consultaNotificacion.ActualizarEstado(per, not);
                    }

                    string condicion = string.Empty;


                    _todosNotificados = _objNotificacionDalc.ExistenPendientesNotificarNumeroSilpa(estado, not.NumeroSILPA);
                    if (!_todosNotificados)
                    {

                        TipoDocumentoDalc tipoDocDalc = new TipoDocumentoDalc();
                        condicion = tipoDocDalc.ObtenerCondicionTipoDocumento(not.IdTipoActo.ID);

                        _solicitud = _solicitudDalc.ObtenerSolicitud(null, null, not.NumeroSILPA);

                        string user = System.Configuration.ConfigurationManager.AppSettings["userFinaliza"].ToString();

                        ServicioWorkflow servicioWorkflow = new ServicioWorkflow();
                        mensaje = servicioWorkflow.ValidarActividadActual(_solicitud.IdProcessInstance, user, (long)ActividadSilpa.RegistrarInformacionDocumento);
                        if (mensaje=="")
                            servicioWorkflow.FinalizarTarea(_solicitud.IdProcessInstance, ActividadSilpa.RegistrarInformacionDocumento, user, condicion);
                        else
                            //JNS 20190822 se escribe mensaje de error
                            SMLog.Escribir(SoftManagement.Log.Severidad.Informativo, "BPM - Validacion :: NotificacionFachada::ActualizarProcesos - NumeroSILPA: " + (!string.IsNullOrEmpty(not.NumeroSILPA) ? not.NumeroSILPA : "null") +
                                                                                     " - CodigoExpediente: " + (!string.IsNullOrEmpty(not.CodigoExpediente) ? not.CodigoExpediente : "null") + 
                                                                                     " - NumeroActoAdministrativo: " + (!string.IsNullOrEmpty(not.NumeroActoAdministrativo) ? not.NumeroActoAdministrativo : "null") +
                                                                                     "\n\n Error: " + mensaje, "BPM_VAL_CON");
                    }
                }
                catch (Exception ex)
                {
                     mensaje = string.Format("----Error en NotificacionFachada.ActualizarProceso para el registro IdActoNotificacion:'{0}' not.NumeroSILPA:'{1}' not.IdTipoActo:'{2}' Error: {3}", not.IdActoNotificacion, not.NumeroSILPA, not.IdTipoActo, ex.ToString());
                    SMLog.Escribir(Severidad.Critico, mensaje);
                }
            }

            return listaConsultas;
        }


        //Modificar el método ActualizarProcesos en NotificacionFachada.cs
        /// <summary>
        /// Actualiza todos los procesos Pendientes
        /// </summary>
        /// <remarks>Este método se ejecuta a través de un servicio de Windows o un servicio que tiene un llamado constante en un horario específico</remarks>
        public List<string> ActualizarProcesos(int idEstado, string strEstado, string esPDI, long idActoNot, long idPersona)
        {
            SILPA.Servicios.BPMServices.GattacaBPMServices9000 _servicioBPM = new SILPA.Servicios.BPMServices.GattacaBPMServices9000();

            Proceso _objProceso = new Proceso();
            List<NotificacionEntity> listaNotificacion = new List<NotificacionEntity>();
            NotificacionDalc _objNotificacionDalc = new NotificacionDalc();
            EstadoNotificacionEntity estado = new EstadoNotificacionEntity();
            EstadoNotificacionDalc _estadoDalc = new EstadoNotificacionDalc();
            PersonaNotificarDalc _personaDalc = new PersonaNotificarDalc();
            //Consulta todos los estados que no sean EJECUTORIADO
            ParametroEntity _parametro = new ParametroEntity();
            ParametroDalc _parametroDalc = new ParametroDalc();

            _parametro.IdParametro = -1;
            _parametro.NombreParametro = "EJECUTORIADA";
            _parametroDalc.obtenerParametros(ref _parametro);

            estado.ID = Convert.ToInt32(_parametro.Parametro);

            listaNotificacion = _objNotificacionDalc.ObtenerActosParaConsultarPDIPorActoPorPersona(estado, idActoNot, idPersona);

            SolicitudDAAEIAIdentity _solicitud = new SolicitudDAAEIAIdentity();
            SolicitudDAAEIADalc _solicitudDalc = new SolicitudDAAEIADalc();
            BpmParametros _bpmParametrosDalc = new BpmParametros();
            //Variables para PDI
            SILPA.LogicaNegocio.Notificacion.Notificacion consultaNotificacion = new SILPA.LogicaNegocio.Notificacion.Notificacion();
            List<PersonaNotificarEntity> listaPersonas;
            List<string> listaConsultas = new List<string>();

            bool _todosNotificados = false;


            for (int indiceActual = 0; indiceActual < listaNotificacion.Count; indiceActual++)
            {
                NotificacionEntity not = listaNotificacion[indiceActual];
                string strMensajeValBPM = "";
                string mensaje = "";

                this.ConsultarTodosPublicables(not);

                try
                {
                    listaPersonas = new List<PersonaNotificarEntity>();
                    listaPersonas = _personaDalc.ObtenerPersonasEstado(not, estado, idPersona);


                    foreach (PersonaNotificarEntity per in listaPersonas)
                    {
                        mensaje = string.Format("persona {0} estado {1}", per.NumeroIdentificacion.ToString(), per.EstadoNotificado.Estado);
                        consultaNotificacion.ActualizarEstado(per, not, idEstado, strEstado, esPDI);
                    }

                    string condicion = string.Empty;
                    SMLog.Escribir(Severidad.Informativo, "Va ha consultar si todos notificados (" + not.IdActoNotificacion + ")");

                    //_todosNotificados = _objNotificacionDalc.ExistePendientesFinalizarActo(idActoNot);
                    _todosNotificados = _objNotificacionDalc.ExistePendientesFinalizarActoExcluyenteNotifComuni(idActoNot);
                    // _todosNotificados = !_objNotificacionDalc.ExistenPendientesActoAdministrativo((int)not.IdActoNotificacion);

                    if (!_todosNotificados)
                    {
                        SMLog.Escribir(Severidad.Informativo, "Estan Todos Notificados, se actualiza fecha de fijaciòn en la publicaciòn");
                        // JM - 15/04/2013
                        // Actualiza la Fecha_Fijacion de la Publicaciones cuando no existan notificaciones pendientes
                        _objNotificacionDalc.ActualizarFechaFijacionPublicacion(not);

                        TipoDocumentoDalc tipoDocDalc = new TipoDocumentoDalc();
                        condicion = tipoDocDalc.ObtenerCondicionTipoDocumento(not.IdTipoActo.ID);

                        _solicitud = _solicitudDalc.ObtenerSolicitud(null, null, not.NumeroSILPA);

                        string user = System.Configuration.ConfigurationManager.AppSettings["userFinaliza"].ToString();

                        //Consultar si la condición es especial
                        SILPA.LogicaNegocio.AdmTablasBasicas.TipoTramite objTablasBasicas = new SILPA.LogicaNegocio.AdmTablasBasicas.TipoTramite();
                        System.Data.DataTable dtCondicion = objTablasBasicas.ListarCondicionesEspeciales(null, condicion, null);

                        //Si la condición no es especial realizar proceso de avance del flujo
                        if (dtCondicion == null || dtCondicion.Rows.Count == 0)
                        {
                            //Verificar si existe error para avnzar
                            ServicioWorkflow servicioWorkflow = new ServicioWorkflow();
                            strMensajeValBPM = servicioWorkflow.ValidarActividadActual(_solicitud.IdProcessInstance, user, (long)ActividadSilpa.RegistrarInformacionDocumento);

                            //JNS 2019/08/22 Se realiza ajuste para que en caso de que no se cumpla condición se reporte en log
                            //Si no existe error para avanzar se finaliza la tarea, sino se reporta en log
                            if (string.IsNullOrEmpty(strMensajeValBPM))
                                servicioWorkflow.FinalizarTarea(_solicitud.IdProcessInstance, ActividadSilpa.RegistrarInformacionDocumento, user, condicion);
                            else
                                SMLog.Escribir(Severidad.Informativo, "BPM - Validacion :: NotificacionFachada::ActualizarProcesos - idActoNot: " + idActoNot.ToString() + " - idPersona: " + idPersona.ToString() + " - Error: " + strMensajeValBPM, "BPM_VAL_CON");
                        }
                    }

                }
                catch (Exception ex)
                {
                    mensaje = string.Format("----Error en NotificacionFachada.ActualizarProceso para el registro IdActoNotificacion:'{0}' not.NumeroSILPA:'{1}' not.IdTipoActo:'{2}' Error: {3}", not.IdActoNotificacion, not.NumeroSILPA, not.IdTipoActo, ex.ToString());
                    SMLog.Escribir(Severidad.Critico, "Adelantar Tramite" + ex.ToString());
                }
            }

            return listaConsultas;
        }


        //Modificar el método ActualizarProcesos en NotificacionFachada.cs
        /// <summary>
        /// Actualiza todos los procesos Pendientes
        /// </summary>
        /// <remarks>Este método se ejecuta a través de un servicio de Windows o un servicio que tiene un llamado constante en un horario específico</remarks>
        public List<string> ActualizarProcesosCorporaciones(int idEstado, string strEstado, string esPDI, long idActoNot, long idPersona)
        {
            SILPA.Servicios.BPMServices.GattacaBPMServices9000 _servicioBPM = new SILPA.Servicios.BPMServices.GattacaBPMServices9000();

            Proceso _objProceso = new Proceso();
            List<NotificacionEntity> listaNotificacion = new List<NotificacionEntity>();
            NotificacionDalc _objNotificacionDalc = new NotificacionDalc();
            EstadoNotificacionEntity estado = new EstadoNotificacionEntity();
            EstadoNotificacionDalc _estadoDalc = new EstadoNotificacionDalc();
            PersonaNotificarDalc _personaDalc = new PersonaNotificarDalc();
            //Consulta todos los estados que no sean EJECUTORIADO
            ParametroEntity _parametro = new ParametroEntity();
            ParametroDalc _parametroDalc = new ParametroDalc();

            _parametro.IdParametro = -1;
            _parametro.NombreParametro = "EJECUTORIADA";
            _parametroDalc.obtenerParametros(ref _parametro);

            estado.ID = Convert.ToInt32(_parametro.Parametro);
            
            listaNotificacion = _objNotificacionDalc.ObtenerActosParaConsultarPDIPorActoPorPersona(estado, idActoNot, idPersona);

            SolicitudDAAEIAIdentity _solicitud = new SolicitudDAAEIAIdentity();
            SolicitudDAAEIADalc _solicitudDalc = new SolicitudDAAEIADalc();
            BpmParametros _bpmParametrosDalc = new BpmParametros();
            //Variables para PDI
            SILPA.LogicaNegocio.Notificacion.Notificacion consultaNotificacion = new SILPA.LogicaNegocio.Notificacion.Notificacion();
            List<PersonaNotificarEntity> listaPersonas;
            List<string> listaConsultas = new List<string>();

            bool _todosNotificados = false;


            for (int indiceActual = 0; indiceActual < listaNotificacion.Count; indiceActual++)
            {
                NotificacionEntity not = listaNotificacion[indiceActual];

                this.ConsultarTodosPublicables(not);

                string mensaje = "";
                try
                {
                    listaPersonas = new List<PersonaNotificarEntity>();
                    listaPersonas = _personaDalc.ObtenerPersonasEstado(not, estado, idPersona);


                    foreach (PersonaNotificarEntity per in listaPersonas)
                    {
                        mensaje = string.Format("persona {0} estado {1}", per.NumeroIdentificacion.ToString(), per.EstadoNotificado.Estado);
                        consultaNotificacion.ActualizarEstado(per, not, idEstado, strEstado, esPDI);
                    }

                    string condicion = string.Empty;
                    SMLog.Escribir(Severidad.Critico, "Va ha consultar si todos notificados ("+not.IdActoNotificacion+")");

                    _todosNotificados = _objNotificacionDalc.ExistePendientesFinalizarActoCorporaciones(idActoNot);
                    // _todosNotificados = !_objNotificacionDalc.ExistenPendientesActoAdministrativo((int)not.IdActoNotificacion);
                    
                    if (!_todosNotificados)
                    {
                        SMLog.Escribir(Severidad.Critico, "Estan Todos Notificados, se actualiza fecha de fijaciòn en la publicaciòn");
                        // JM - 15/04/2013
                        // Actualiza la Fecha_Fijacion de la Publicaciones cuando no existan notificaciones pendientes
                        _objNotificacionDalc.ActualizarFechaFijacionPublicacion(not);
                                        
                        TipoDocumentoDalc tipoDocDalc = new TipoDocumentoDalc();
                        condicion = tipoDocDalc.ObtenerCondicionTipoDocumento(not.IdTipoActo.ID);

                        _solicitud = _solicitudDalc.ObtenerSolicitud(null, null, not.NumeroSILPA);

                        string user = System.Configuration.ConfigurationManager.AppSettings["userFinaliza"].ToString();

                        
                        ServicioWorkflow servicioWorkflow = new ServicioWorkflow();
                        string strMensaje = servicioWorkflow.ValidarActividadActual(_solicitud.IdProcessInstance, user,(long)ActividadSilpa.RegistrarInformacionDocumento);
                        if (string.IsNullOrEmpty(strMensaje))
                            servicioWorkflow.FinalizarTarea(_solicitud.IdProcessInstance, ActividadSilpa.RegistrarInformacionDocumento, user, condicion);
                        else
                            //JNS 20190822 se escribe mensaje de error
                            SMLog.Escribir(SoftManagement.Log.Severidad.Informativo, "BPM - Validacion :: NotificacionFachada::ActualizarProcesos - idActoNot: " + idActoNot.ToString() +
                                                                                     " - idPersona: " + idPersona.ToString() +
                                                                                     " - idEstado: " + idEstado.ToString() +
                                                                                     "\n\n Error: " + strMensaje, "BPM_VAL_CON");
                     }
                    
                }
                catch (Exception ex)
                {
                    mensaje = string.Format("----Error en NotificacionFachada.ActualizarProceso para el registro IdActoNotificacion:'{0}' not.NumeroSILPA:'{1}' not.IdTipoActo:'{2}' Error: {3}", not.IdActoNotificacion, not.NumeroSILPA, not.IdTipoActo, ex.ToString());
                    SMLog.Escribir(Severidad.Critico,"Adelantar Tramite"+ ex.ToString());
                }
            }

            return listaConsultas;
        }

        /// <summary>
        /// Método que permite determinar si las personas notificadas se encuentran en los estados apropiados para
        /// permitir una publicación si esrequerida para el tipo de documento
        /// </summary>
        /// <returns></returns>
        public void ConsultarTodosPublicables(NotificacionEntity not)
        {
            if (not != null)
            {

                // si requiere publicación
                if (not.RequierePublicacion)
                {
                    bool _todosNotificados = false;
                    //Se determina si cada persona cumple con los estados para publicar
                    //not.IdTipoActo.ID
                    NotificacionDalc dalc = new NotificacionDalc();
                    EstadoNotificacionEntity estado = new EstadoNotificacionEntity();
                    estado.ID = -1;
                    // int result = dalc.ConsultarTodosPublicacion(not.IdActoNotificacion, not.IdTipoActo.ID);
                    _todosNotificados = dalc.ExistenPendientesNotificarNumeroSilpa(estado, not.NumeroSILPA);

                    //Si Los estados de todas las personas a notificar se encuentran dentro de las posibles para publicar entonces 
                    //actualizamos la publicación
                    if (!_todosNotificados)
                    {
                        PublicacionDalc pubDalc = new PublicacionDalc();
                        pubDalc.ActualizarPublicacionPorNotificacion(not.IdActoNotificacion);
                    }
                }
            }
        }

        /*
        /// <summary>
        /// Actualiza todos los procesos Pendientes
        /// </summary>
        /// <remarks>Este método se ejecuta a través de un servicio de Windows o un servicio que tiene un llamado constante en un horario específico</remarks>
        public List<string> ActualizarProcesos()
        {
            //SMLog.Escribir(Severidad.Informativo, "----Inicia el proceso de ActualizarProcesos"); 
            //1. Consultar La lista de Actos cuyo estado sea: 1, 2, 9, 10
            SILPA.Servicios.BPMServices.GattacaBPMServices9000 _servicioBPM = new SILPA.Servicios.BPMServices.GattacaBPMServices9000();
            Proceso _objProceso = new Proceso();
            List<NotificacionEntity> listaNotificacion = new List<NotificacionEntity>();
            NotificacionDalc _objNotificacionDalc = new NotificacionDalc();
            EstadoNotificacionEntity estado = new EstadoNotificacionEntity();
            EstadoNotificacionDalc _estadoDalc = new EstadoNotificacionDalc();
            PersonaNotificarDalc _personaDalc = new PersonaNotificarDalc();
            //Consulta todos los estados que no sean EJECUTORIADO
            ParametroEntity _parametro = new ParametroEntity();
            ParametroDalc _parametroDalc = new ParametroDalc();
            
            _parametro.IdParametro = -1;
            _parametro.NombreParametro = "EJECUTORIADA";
            _parametroDalc.obtenerParametros(ref _parametro);

            estado.ID = Convert.ToInt32(_parametro.Parametro);
            listaNotificacion = _objNotificacionDalc.ObtenerActosParaConsultarPDI(estado);
            SolicitudDAAEIAIdentity _solicitud = new SolicitudDAAEIAIdentity();
            SolicitudDAAEIADalc _solicitudDalc = new SolicitudDAAEIADalc();
            BpmParametros _bpmParametrosDalc = new BpmParametros();
            //Variables para PDI
            SILPA.LogicaNegocio.Notificacion.Notificacion consultaNotificacion = new SILPA.LogicaNegocio.Notificacion.Notificacion();
            List<PersonaNotificarEntity> listaPersonas;
            List<string> listaConsultas = new List<string>();
            //Temporal...
            //estado = _estadoDalc.ListarEstadoNotificacion(new object[] { 7 });
            //NotificacionEntity tempnot;
            
            /// Variable que permite conocer si todos las personas estan notificadas
            bool _todosNotificados = false;

            //2. Recorro la lista y actualizo por cada uno de ellos, el estado en NOT_ACTO

            //foreach (NotificacionEntity not in listaNotificacion)
            for (int indiceActual = 0; indiceActual < listaNotificacion.Count; indiceActual++)
            {
                NotificacionEntity not = listaNotificacion[indiceActual];

                // Consultar el Estado en PDI
                listaPersonas = new List<PersonaNotificarEntity>();
                listaPersonas = _personaDalc.ObtenerPersonasEstado(not, estado);

                foreach (PersonaNotificarEntity per in listaPersonas)
                {
                    consultaNotificacion.ActualizarEstado(per, not);
                    // listaConsultas.Add(consultaNotificacion.SetXMLGELEstadoNotificacionEntrada(per, not));
                }
                /// se determina si todas las personas se encuentran en estado notificada
                PersonaNotificarEntity p = null;

                p = listaPersonas.Find(delegate(PersonaNotificarEntity persona) { return persona.EstadoNotificado.ID != (int)EstadoNotificacion.NOTIFICADA; });

                //SMLog.Escribir(Severidad.Informativo, "----Revision " + indiceActual.ToString());
                //SMLog.Escribir(Severidad.Informativo, "----not.NumeroSILPA " + not.NumeroSILPA);
                if (p == null)
                {
                    //SMLog.Escribir(Severidad.Informativo, "----P es null");
                }
                else
                {
                    //SMLog.Escribir(Severidad.Informativo, "----P tiene datos " + p.NumeroIdentificacion);
                }

                if (p == null)
                {
                    //Es necesario verificar además que este código SILPA esté más adelante
                    bool pendienteNotificacionAdicional = false;
                    for (int siguiente = indiceActual + 1; siguiente < listaNotificacion.Count; siguiente++)
                    {
                        NotificacionEntity notSiguiente = listaNotificacion[siguiente];

                        //SMLog.Escribir(Severidad.Informativo, "----notSiguiente.NumeroSILPA " + notSiguiente.NumeroSILPA + " " + siguiente.ToString());
                        if (notSiguiente.NumeroSILPA == not.NumeroSILPA)
                        {
                            pendienteNotificacionAdicional = true;
                            break;
                        }
                    }
                    _todosNotificados = !pendienteNotificacionAdicional;
                    //SMLog.Escribir(Severidad.Informativo, "----_todosNotificados " + _todosNotificados);
                }
                else _todosNotificados = false;

                string condicion = string.Empty;
                if (_todosNotificados)
                {

                    ///  VITALCONPRO01-10 |  VITALCONPRO01-13 |VITALCONPRO01-14 | VITALCONPRO01-11 -- para DAA
                    //if (not.IdTipoActo.ID != null)
                    //{
                    TipoDocumentoDalc tipoDocDalc = new TipoDocumentoDalc();
                    condicion = tipoDocDalc.ObtenerCondicionTipoDocumento(not.IdTipoActo.ID);
                    //}

                    _solicitud = _solicitudDalc.ObtenerSolicitud(null, null, not.NumeroSILPA);

                    //PersonaDalc personaDalc = new PersonaDalc();
                    //PersonaIdentity persona = personaDalc.BuscarPersonaByUserId(System.Configuration.ConfigurationManager.AppSettings["userFinaliza"].ToString());

                    string user = System.Configuration.ConfigurationManager.AppSettings["userFinaliza"].ToString();

                    //Avanza la tarea
                    ////SMLog.Escribir(Severidad.Informativo, "Notificados: Todos  --->>   ProcessInstance:" + _solicitud.IdProcessInstance.ToString() + "  Condicion: " + condicion + " user:" + persona.Username + " Tipo de acto:" + not.IdTipoActo.ID.ToString());

                    ///System.IO.File.WriteAllText(@"c:\TMP\COCHINADA_3.TXT", "Notificados: Todos  --->>   ProcessInstance:" + _solicitud.IdProcessInstance.ToString() + "  Condicion: " + condicion + " user:" + user + " Tipo de acto:" + not.IdTipoActo.ID.ToString());

                    ServicioWorkflow servicioWorkflow = new ServicioWorkflow();
                    //servicioWorkflow.ValidarActividadActual(_solicitud.IdProcessInstance, ActividadSilpa.ValidarRespuestaAutoridadAmbiental);
                    //SMLog.Escribir(Severidad.Informativo, "----not.IdTipoActo.ID" + not.IdTipoActo.ID.ToString());
                    //SMLog.Escribir(Severidad.Informativo, "----Condicion: " + condicion);
                    //SMLog.Escribir(Severidad.Informativo, "----user: " + user);
                    //SMLog.Escribir(Severidad.Informativo, "----_solicitud.IdProcessInstance.ToString" + _solicitud.IdProcessInstance.ToString());
                    servicioWorkflow.ValidarActividadActual(_solicitud.IdProcessInstance, ActividadSilpa.RegistrarInformacionDocumento);
                    servicioWorkflow.FinalizarTarea(_solicitud.IdProcessInstance, ActividadSilpa.RegistrarInformacionDocumento, user, condicion);
                }
            }
            
            return listaConsultas;
        }
        */
        /// <summary>
        /// Crea un Proceso de Notificación desde la Autoridad Ambiental
        /// </summary>
        /// <param name="xmlDatos"></param>
        //public string CrearProceso(string xmlDatos)
        //{
        //    string salida = "";
        //    SILPA.LogicaNegocio.Notificacion.Notificacion _objNotificacion = new SILPA.LogicaNegocio.Notificacion.Notificacion();
        //    salida = _objNotificacion.CrearProceso(xmlDatos);
        //    return salida;

        //    //Pasar lo de actividad avance a CrearProceso en lógica de negocio?
        //    //SILPA.Servicios.ArctividadBPMFachada.DeterminarAvanceActividad(_numeroSILPA);
        //}

        public string ComponenteNot(string metodo, string datos, bool p_blnLogActivo = false)
        {
            if (p_blnLogActivo)
                SMLog.Escribir(Severidad.Informativo, "Entra ComponenteNot");
            
            string respuesta = string.Empty;
            Random e = new Random(100);
            string idTransaccion = DateTime.Now.ToString("yyyyMMddhhmmss") + "_" + e.Next(999);
            ParametroDalc parDalc = new ParametroDalc();
            ParametroEntity parEntity = new ParametroEntity();
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            try
            {
                parEntity.IdParametro = -1;
                parEntity.NombreParametro = SILPA.Comun.DatoComponenteNotificacion.ruta;
                parDalc.obtenerParametros(ref parEntity);
                string ruta = parEntity.Parametro + idTransaccion + ".txt";

                //SMLog.Escribir(Severidad.Informativo, "Notificacion: " + metodo.ToString() + ";Archivo Creado en " + ruta);       
                System.IO.File.WriteAllText(ruta, datos);

                parEntity = new ParametroEntity();
                parEntity.IdParametro = -1;
                parEntity.NombreParametro = Comun.DatoComponenteNotificacion.componente;
                parDalc.obtenerParametros(ref parEntity);


                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.FileName = parEntity.Parametro;
                p.StartInfo.Arguments = ruta + " " + idTransaccion + " " + metodo;

                
                //SMLog.Escribir(Severidad.Informativo, "Notificacion: " + ";Ejecucion " + parEntity.Parametro + " " +  p.StartInfo.Arguments);       

                //-----
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.RedirectStandardError = true;
                p.Start(); // Do not wait for the child process to exit before 
                // reading to the end of its redirected stream. 
                // p.WaitForExit(); 
                // Read the output stream first and then wait. string output = p.StandardOutput.ReadToEnd(); p.WaitForExit();
                if (p_blnLogActivo)
                    SMLog.Escribir(Severidad.Informativo, "entra p.WaitForExit()");
                p.WaitForExit();
                //SMLog.Escribir(Severidad.Informativo, "Notificacion: " + ";Termino el proceso " + p.StartInfo.Arguments);       
                if (p_blnLogActivo)
                    SMLog.Escribir(Severidad.Informativo, "sale  p.WaitForExit()");
                string strRuta = ruta.Replace("txt", "") + "_" + idTransaccion + "_" + SILPA.Comun.DatoComponenteNotificacion.respuesta;
                
                if(System.IO.File.Exists(strRuta))
                {
                    respuesta = System.IO.File.ReadAllText(strRuta);
                }
                else
                {
                    WSRespuesta xmlRespuesta = new WSRespuesta();
                    xmlRespuesta.CodigoMensaje = "No existe respuesta desde Notificacion PDI.";
                    xmlRespuesta.Mensaje = "No existe respuesta desde Notificacion PDI.";
                    xmlRespuesta.IdExterno = "-1";
                    xmlRespuesta.IdSilpa = "-1";
                    xmlRespuesta.Exito = false;
                    respuesta = xmlRespuesta.GetXml();
                }

                if (p_blnLogActivo)
                    SMLog.Escribir(Severidad.Informativo, "Sale ComponenteNot");

                //respuesta = System.IO.File.ReadAllText(ruta.Replace("txt", "") + "_" + idTransaccion + "_" + SILPA.Comun.DatoComponenteNotificacion.respuesta);

                return respuesta;
            }
            catch (Exception ex)
            {
                //SMLog.Escribir(Severidad.Critico, "Notificacion: " + ";error el proceso " + p.StartInfo.Arguments + " " + ex.Message);       
                p.Dispose();
                p = null;
                return "error " + ex.Message;
            }


        }

        /// <summary>
        /// Hava: 19-Nov-10
        /// LLamado manual Notificación electrónica
        /// </summary>
        /// <param name="idEstado"></param>
        /// <param name="strEstado"></param>
        /// <param name="esPDI"></param>
        /// <param name="idActoNot"></param>
        /// <returns></returns>
        public string ComponenteNotManual(string idEstado, string strEstado, string esPDI, long idActoNot, string metodo, long idPersona)
        {            

            WSRespuesta xmlRespuesta = new WSRespuesta();

            string respuesta = string.Empty;
            Random e = new Random(100);
            string idTransaccion = DateTime.Now.ToString("yyyyMMddhhmmss") + "_" + e.Next(999);
            ParametroDalc parDalc = new ParametroDalc();         
            
            ParametroEntity parEntity = new ParametroEntity();
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            try
            {
                parEntity.IdParametro = -1;
                parEntity.NombreParametro = SILPA.Comun.DatoComponenteNotificacion.ruta;
                parDalc.obtenerParametros(ref parEntity);
                string ruta = parEntity.Parametro + idTransaccion + ".txt";

                parEntity = new ParametroEntity();
                parEntity.IdParametro = -1;
                parEntity.NombreParametro = Comun.DatoComponenteNotificacion.componente;
                parDalc.obtenerParametros(ref parEntity);

                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.FileName = parEntity.Parametro;                
                p.StartInfo.Arguments = idEstado + " " + strEstado + " " + esPDI + " " + idActoNot.ToString() + " " + idTransaccion + " " + ruta + " " + metodo + " " + idPersona.ToString();
                
                SMLog.Escribir(Severidad.Informativo, "Intermedio ComNotmanual" + idEstado + " " + strEstado + " " + esPDI + " " + idActoNot + " " + metodo);
                //-----
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.RedirectStandardError = true;
                p.Start(); // Do not wait for the child process to exit before 
                // reading to the end of its redirected stream. 
                // p.WaitForExit(); 
                // Read the output stream first and then wait. string output = p.StandardOutput.ReadToEnd(); p.WaitForExit();
                p.WaitForExit();
                //SMLog.Escribir(Severidad.Informativo, "Notificacion: " + ";Termino el proceso " + p.StartInfo.Arguments);       

                SMLog.Escribir(Severidad.Informativo, "final ComNotmanual" + idEstado + " " + strEstado + " " + esPDI + " " + idActoNot + " " + metodo);

                string strRuta = ruta.Replace("txt", "") + "_" + idTransaccion + "_" + SILPA.Comun.DatoComponenteNotificacion.respuesta;

                if (System.IO.File.Exists(strRuta))
                {
                    respuesta = System.IO.File.ReadAllText(strRuta);
                    xmlRespuesta.CodigoMensaje = respuesta;
                }
                else
                {
                    //WSRespuesta xmlRespuesta = new WSRespuesta();
                    xmlRespuesta.CodigoMensaje = "No se pudo efectuar el procedimiento manual";
                    xmlRespuesta.Mensaje = "No se pudo efectuar el procedimiento manual";
                    xmlRespuesta.IdExterno = "-1";
                    xmlRespuesta.IdSilpa = "-1";
                    xmlRespuesta.Exito = false;
                    respuesta = xmlRespuesta.GetXml();
                }

                //respuesta = System.IO.File.ReadAllText(ruta.Replace("txt", "") + "_" + idTransaccion + "_" + SILPA.Comun.DatoComponenteNotificacion.respuesta);

                return respuesta;
            }
            catch (Exception ex)
            {
                SMLog.Escribir(Severidad.Critico, "Notificacion: " + ";error el proceso " + p.StartInfo.Arguments + " " + ex.Message);       
                p.Dispose();
                p = null;
                return "error " + ex.Message;
            }
        }


        /// <summary>
        /// Obtener el listado de direcciones de una persona
        /// </summary>
        /// <param name="p_strNumeroIdentificacion">string con el numero identificacion de una persona</param>
        /// <returns>string con el XML que contiene el listado de direcciones de una persona</returns>
        public string ObtenerDireccionesNumeroIdentificacion(string p_strNumeroIdentificacion)
        {
            PersonaNotificar objPersonaNotificar;
            List<DireccionNotificacionEntity> objLstDirecciones;
            XmlSerializador objXmlSerializador;
            string strListadoDirecciones = "";

            try
            {
                //Obtener el listado de direcciones
                objPersonaNotificar = new PersonaNotificar();
                objLstDirecciones = objPersonaNotificar.ObtenerListadoDireccionesNotificarNumeroIdentificacion(p_strNumeroIdentificacion);

                //Serializar objeto
                objXmlSerializador = new XmlSerializador();
                strListadoDirecciones = objXmlSerializador.serializar(objLstDirecciones);
            }
            catch (Exception ex)
            {
                SMLog.Escribir(Severidad.Critico, "Notificacion :: ObtenerDireccionesNumeroIdentificacion : " + ";error el obteniendo direcciones p_strNumeroIdentificacion: " + (p_strNumeroIdentificacion ?? "null") + " " + ex.Message + " - " + ex.InnerException.ToString());
                return "Error " + ex.Message;
            }

            return strListadoDirecciones;
        }


        /// <summary>
        /// Obtener la información de la dirección de la persona
        /// </summary>
        /// <param name="p_lngDIreccionID">long con el identificador de la direccion</param>
        /// <returns>string con la información de la dirección</returns>
        public string ObtenerInformacionDireccionPersona(long p_lngDIreccionID)
        {
            PersonaNotificar objPersonaNotificar;
            DireccionNotificacionEntity objDireccionNotificacionEntity;
            XmlSerializador objXmlSerializador;
            string strInformacionPersona = "";

            try
            {
                //Obtener el listado de direcciones
                objPersonaNotificar = new PersonaNotificar();
                objDireccionNotificacionEntity = objPersonaNotificar.ObtenerInformacionDireccionPersona(p_lngDIreccionID);

                //Serializar objeto
                objXmlSerializador = new XmlSerializador();
                strInformacionPersona = objXmlSerializador.serializar(objDireccionNotificacionEntity);
            }
            catch (Exception ex)
            {
                SMLog.Escribir(Severidad.Critico, "Notificacion :: ObtenerInformacionDireccionPersona: " + ";error obteniendo informacion de direccion p_lngDIreccionID: " + p_lngDIreccionID.ToString() + " " + ex.Message + " - " + ex.InnerException.ToString());
                return "Error " + ex.Message;
            }

            return strInformacionPersona;
        }


        /// <summary>
        /// Obtener el listado de correos de una persona
        /// </summary>
        /// <param name="p_strNumeroIdentificacion">string con el numero de identificacion</param>
        /// <returns>List con la información de los correos</returns>
        public List<string> ObtenerListadoCorreosNotificarNumeroIdentificacion(string p_strNumeroIdentificacion)
        {
            PersonaNotificar objPersonaNotificar;
            List<string> objLstDirecciones;

            try
            {
                //Obtener el listado de direcciones
                objPersonaNotificar = new PersonaNotificar();
                objLstDirecciones = objPersonaNotificar.ObtenerListadoCorreosNotificarNumeroIdentificacion(p_strNumeroIdentificacion);
            }
            catch (Exception ex)
            {
                SMLog.Escribir(Severidad.Critico, "Notificacion :: ObtenerListadoCorreosNotificarNumeroIdentificacion: " + ";error obteniendo informacion de correos p_strNumeroIdentificacion: " + (p_strNumeroIdentificacion ?? "null") + " " + ex.Message + " - " + ex.InnerException.ToString());
                throw ex;
            }

            return objLstDirecciones;
        }


        public int DiasDiferencia(DateTime fechaEstado, DateTime fechaHoy)
        {
            NotificacionDalc notdalc = new NotificacionDalc();
            return notdalc.DiasDiferencia(fechaEstado, fechaHoy);
        }

        public void ProcesarNotificacionesPendientesBPM()
        {
            NotificacionDalc _objNotificacionDalc = new NotificacionDalc();
            DataTable dtRegistros = new DataTable();
            dtRegistros = _objNotificacionDalc.listaNotificacionesPorAdelantarBPM();
            if (dtRegistros.Rows.Count > 0)
            {
                foreach (DataRow dtRow in dtRegistros.Rows)
                {
                    if (dtRow["ID_ACTO_NOTIFICACION"].ToString() != string.Empty && dtRow["ID_PERSONA"].ToString() != string.Empty && dtRow["ID_ESTADO"].ToString() != string.Empty && dtRow["NOMBRE_ESTADO"].ToString() != string.Empty && dtRow["ES_PDI"].ToString() != string.Empty)
                    {
                        try
                        {
                            ActualizarProcesos(Convert.ToInt32(dtRow["ID_ESTADO"].ToString()), dtRow["NOMBRE_ESTADO"].ToString(), dtRow["ES_PDI"].ToString(), Convert.ToInt64(dtRow["ID_ACTO_NOTIFICACION"].ToString()), Convert.ToInt64(dtRow["ID_PERSONA"].ToString()));
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
            }
        }

    }
}


