using SILPA.AccesoDatos.DAA;
using SILPA.AccesoDatos.PINES;
using SILPA.LogicaNegocio.DAA;
using SILPA.LogicaNegocio.Enumeracion;
using SILPA.LogicaNegocio.Generico;
using SoftManagement.ServicioCorreoElectronico;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;


namespace SILPA.LogicaNegocio.PINES
{
    public class Alarmas
    {
        public PINESDALC vPINESDALC;
        public Alarmas()
        {
            vPINESDALC = new PINESDALC();
        }
        public void NotificarCrearcionActivityInstance(Int64 UserID, Int64 IDProcessInstance)
        {
            try
            {
                // consultamos los responsables de las actividades pendientes para el processInstance. Solo se tiene en cuenta el proceso de PINES
                DataTable dtParticipantes = vPINESDALC.ParticipantesActividadPendiente(IDProcessInstance);
                if (dtParticipantes.Rows.Count > 0) // esto nos indica que se encontraron responsables para las actividades del proceso de PINES
                {
                    // hacemos un distinct por id de actividad con el objetivo de identificar si la actividad tiene mas de un interviniente
                    var activitys = dtParticipantes.AsEnumerable().GroupBy(x => new { IDACTIVITY = x.Field<int>("ID") }).Select(g => new { IDACTIVITY = g.Key.IDACTIVITY }).ToList();
                    // consultamos por cada actividad si existe como actividad con varios intervinientes
                    PINES clsPines = new PINES();
                    List<int> LstActividades = clsPines.ListaActividadesVariosIntervinientes();
                    List<int> LstAutoridadAmbiental = clsPines.ListaAutoridadesSolicitudId(Convert.ToInt32(IDProcessInstance),Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["IDFIELD_FORM_PINES_MUNICIPIO"].ToString()));
                    // Consultamos la solicitud
                    SolicitudDAAEIA clsSolicitudDAAEIA = new SolicitudDAAEIA();
                    clsSolicitudDAAEIA.ConsultarSolicitudByProcessInstance(IDProcessInstance);
                    // consultamos el solicitante de la solicitud
                    Persona clsPersona = new Persona();
                    clsPersona.ObternerPersonaByIdSolicitante(clsSolicitudDAAEIA.Identity.IdSolicitante.ToString());

                    DataTable dtFormularioProcessInstance = vPINESDALC.ConsultarDatosFormProcessInstance(IDProcessInstance);
                    

                    foreach (var item in activitys)
                    {
                        if (LstActividades.Contains(item.IDACTIVITY)) // si la actividad tiene varios intervinientes se debe notificar solo a los pertenencientes a las autoridades de la solicitud de pintes
                        {
                            // consultamos los responsable de la actividad por cada autoridad ambiental
                            foreach (var autoridadID in LstAutoridadAmbiental)
                            {
                                // una vez tengamos los responsable procedemos ha generar el correo para cada unos de los responsables de la autoridad
                                var participantesAutoridad = dtParticipantes.AsEnumerable().Where(x => x.Field<string>("CODE") == autoridadID.ToString() && x.Field<int>("ID") == item.IDACTIVITY).Select(g=> g);
                                foreach (var participante in participantesAutoridad)
	                            {
		                            ServicioCorreoElectronico clsServicioCorreoElectronico = new ServicioCorreoElectronico();
                                    clsServicioCorreoElectronico.Para.Add(participante.Field<string>("EMAIL"));
                                    clsServicioCorreoElectronico.Tokens.Add("ENTIDAD", participante.Field<string>("ENTIDAD"));
                                    clsServicioCorreoElectronico.Tokens.Add("SOLICITANTE", string.Format("{0} {1}", clsPersona.Identity.PrimerNombre, clsPersona.Identity.PrimerApellido));
                                    clsServicioCorreoElectronico.Tokens.Add("TIPO_IDENTIFICACION", clsPersona.Identity.TipoDocumentoIdentificacion.Sigla);
                                    clsServicioCorreoElectronico.Tokens.Add("NUMERO_IDENTIFICACION", clsPersona.Identity.NumeroIdentificacion);
                                    clsServicioCorreoElectronico.Tokens.Add("ACTIVIDAD", participante.Field<string>("ACTIVITYNAME"));
                                    clsServicioCorreoElectronico.Tokens.Add("FECHA_VENCIMIENTO_ACTIVIDAD", participante.Field<DateTime>("FECHA_VENCIMIENTO_ACTIVIDAD").ToShortDateString());
                                    clsServicioCorreoElectronico.Tokens.Add("NRO_VITAL", clsSolicitudDAAEIA.Identity.NumeroSilpa);
                                    var tipoProyecto = dtFormularioProcessInstance.AsEnumerable().Where(x=> x.Field<int>("IDFIELD") == Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["IDFIELD_FORM_PINES_TIPO_PROYECTO"].ToString())).Select(g => new {VALUE = g.Field<string>("VALUE")}).ToList().First();
                                    clsServicioCorreoElectronico.Tokens.Add("TIPO_PROYECTO", tipoProyecto.VALUE);
                                    clsServicioCorreoElectronico.Tokens.Add("FECHA_ENVIO", DateTime.Now.ToString());
                                    int plantillaID = (int)EnumPlantillaCorreo.IniciaTareaProcesoPINES;
                                    clsServicioCorreoElectronico.Enviar(plantillaID);
	                            }
                            }
                        }
                        else
                        {
                            foreach (var autoridadID in LstAutoridadAmbiental)
                            {
                                // una vez tengamos los responsable procedemos ha generar el correo para cada unos de los responsables de la autoridad
                                var participantesAutoridad = dtParticipantes.AsEnumerable().Where(x => x.Field<string>("CODE") == autoridadID.ToString() && x.Field<int>("ID") == item.IDACTIVITY).Select(g => g);
                                foreach (var participante in participantesAutoridad)
                                {
                                    ServicioCorreoElectronico clsServicioCorreoElectronico = new ServicioCorreoElectronico();
                                    clsServicioCorreoElectronico.Para.Add(participante.Field<string>("EMAIL"));
                                    clsServicioCorreoElectronico.Tokens.Add("ENTIDAD", participante.Field<string>("ENTIDAD"));
                                    clsServicioCorreoElectronico.Tokens.Add("SOLICITANTE", string.Format("{0} {1}", clsPersona.Identity.PrimerNombre, clsPersona.Identity.PrimerApellido));
                                    clsServicioCorreoElectronico.Tokens.Add("TIPO_IDENTIFICACION", clsPersona.Identity.TipoDocumentoIdentificacion.Sigla);
                                    clsServicioCorreoElectronico.Tokens.Add("NUMERO_IDENTIFICACION", clsPersona.Identity.NumeroIdentificacion);
                                    clsServicioCorreoElectronico.Tokens.Add("ACTIVIDAD", participante.Field<string>("ACTIVITYNAME"));
                                    clsServicioCorreoElectronico.Tokens.Add("FECHA_VENCIMIENTO_ACTIVIDAD", participante.Field<DateTime>("FECHA_VENCIMIENTO_ACTIVIDAD").ToShortDateString());
                                    clsServicioCorreoElectronico.Tokens.Add("NRO_VITAL", clsSolicitudDAAEIA.Identity.NumeroSilpa);
                                    var tipoProyecto = dtFormularioProcessInstance.AsEnumerable().Where(x => x.Field<int>("IDFIELD") == Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["IDFIELD_FORM_PINES_TIPO_PROYECTO"].ToString())).Select(g => new { VALUE = g.Field<string>("VALUE") }).ToList().First();
                                    clsServicioCorreoElectronico.Tokens.Add("TIPO_PROYECTO", tipoProyecto.VALUE);
                                    clsServicioCorreoElectronico.Tokens.Add("FECHA_ENVIO", DateTime.Now.ToString());
                                    int plantillaID = (int)EnumPlantillaCorreo.IniciaTareaProcesoPINES;
                                    clsServicioCorreoElectronico.Enviar(plantillaID);
                                }
                            }
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {
                
                throw ex;
            }

        }
        public void NotificarActividadesProximasAVencerce(int diaAlarmaVencimiento)
        {

            try
            {
                // consultamos los responsables de las actividades pendientes para el processInstance. Solo se tiene en cuenta el proceso de PINES
                DataTable dtParticipantes = vPINESDALC.ParticipantesActividadPendiente(null);
                if (dtParticipantes.Rows.Count > 0) // esto nos indica que se encontraron responsables para las actividades del proceso de PINES
                {
                    var tareasProximasAVencerce = dtParticipantes.AsEnumerable().Where(x => x.Field<int>("DIAS_A_VENCERCE") <= diaAlarmaVencimiento);
                    if(tareasProximasAVencerce.Count() > 0)
                    {
                        PINES clsPines = new PINES();
                        List<int> LstActividades = clsPines.ListaActividadesVariosIntervinientes();
                        var processInstances = tareasProximasAVencerce.GroupBy(x => new { IDPROCESSINSTANCE = x.Field<int>("IDPROCESSINSTANCE") });
                        foreach (var procesInstance in processInstances)
                        {
                            List<int> LstAutoridadAmbiental = clsPines.ListaAutoridadesSolicitudId(procesInstance.Key.IDPROCESSINSTANCE, Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["IDFIELD_FORM_PINES_MUNICIPIO"].ToString()));
                            SolicitudDAAEIA clsSolicitudDAAEIA = new SolicitudDAAEIA();
                            clsSolicitudDAAEIA.ConsultarSolicitudByProcessInstance(procesInstance.Key.IDPROCESSINSTANCE);
                            // consultamos el solicitante de la solicitud
                            Persona clsPersona = new Persona();
                            clsPersona.ObternerPersonaByIdSolicitante(clsSolicitudDAAEIA.Identity.IdSolicitante.ToString());

                            DataTable dtFormularioProcessInstance = vPINESDALC.ConsultarDatosFormProcessInstance(procesInstance.Key.IDPROCESSINSTANCE);
                            var actividadesProximasAVencerceProcessInstance = tareasProximasAVencerce.Where(x => x.Field<int>("IDPROCESSINSTANCE") == procesInstance.Key.IDPROCESSINSTANCE);
                            var distincActivityProcessInstance = actividadesProximasAVencerceProcessInstance.GroupBy(x => new { ACTIVITY = x.Field<int>("ID") });

                            foreach (var item in distincActivityProcessInstance)
                            {
                                int idActivity = item.Key.ACTIVITY;
                                if (LstActividades.Contains(idActivity)) // si la actividad tiene varios intervinientes se debe notificar solo a los pertenencientes a las autoridades de la solicitud de pintes
                                {
                                    // consultamos los responsable de la actividad por cada autoridad ambiental
                                    foreach (var autoridadID in LstAutoridadAmbiental)
                                    {
                                        // una vez tengamos los responsable procedemos ha generar el correo para cada unos de los responsables de la autoridad
                                        var participantesAutoridad = actividadesProximasAVencerceProcessInstance.Where(x => x.Field<string>("CODE") == autoridadID.ToString() && x.Field<int>("ID") == idActivity).Select(g => g);
                                        foreach (var participante in participantesAutoridad)
	                                    {
		                                    ServicioCorreoElectronico clsServicioCorreoElectronico = new ServicioCorreoElectronico();
                                            clsServicioCorreoElectronico.Para.Add(participante.Field<string>("EMAIL"));
                                            clsServicioCorreoElectronico.Tokens.Add("ENTIDAD", participante.Field<string>("ENTIDAD"));
                                            clsServicioCorreoElectronico.Tokens.Add("SOLICITANTE", string.Format("{0} {1}", clsPersona.Identity.PrimerNombre, clsPersona.Identity.PrimerApellido));
                                            clsServicioCorreoElectronico.Tokens.Add("TIPO_IDENTIFICACION", clsPersona.Identity.TipoDocumentoIdentificacion.Sigla);
                                            clsServicioCorreoElectronico.Tokens.Add("NUMERO_IDENTIFICACION", clsPersona.Identity.NumeroIdentificacion);
                                            clsServicioCorreoElectronico.Tokens.Add("ACTIVIDAD", participante.Field<string>("ACTIVITYNAME"));
                                            clsServicioCorreoElectronico.Tokens.Add("FECHA_VENCIMIENTO_ACTIVIDAD", participante.Field<DateTime>("FECHA_VENCIMIENTO_ACTIVIDAD").ToShortDateString());
                                            clsServicioCorreoElectronico.Tokens.Add("NRO_VITAL", clsSolicitudDAAEIA.Identity.NumeroSilpa);
                                            var tipoProyecto = dtFormularioProcessInstance.AsEnumerable().Where(x=> x.Field<int>("IDFIELD") == Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["IDFIELD_FORM_PINES_TIPO_PROYECTO"].ToString())).Select(g => new {VALUE = g.Field<string>("VALUE")}).ToList().First();
                                            clsServicioCorreoElectronico.Tokens.Add("TIPO_PROYECTO", tipoProyecto.VALUE);
                                            clsServicioCorreoElectronico.Tokens.Add("FECHA_ENVIO", DateTime.Now.ToString());
                                            int plantillaID = (int)EnumPlantillaCorreo.TareaProximaAVencercePINES;
                                            clsServicioCorreoElectronico.Enviar(plantillaID);
	                                    }
                                    }
                                }
                                else
                                {
                                    foreach (var autoridadID in LstAutoridadAmbiental)
                                    {
                                        // una vez tengamos los responsable procedemos ha generar el correo para cada unos de los responsables de la autoridad
                                        var participantesAutoridad = actividadesProximasAVencerceProcessInstance.Where(x => x.Field<string>("CODE") == autoridadID.ToString() && x.Field<int>("ID") == idActivity).Select(g => g);
                                        foreach (var participante in participantesAutoridad)
                                        {
                                            ServicioCorreoElectronico clsServicioCorreoElectronico = new ServicioCorreoElectronico();
                                            clsServicioCorreoElectronico.Para.Add(participante.Field<string>("EMAIL"));
                                            clsServicioCorreoElectronico.Tokens.Add("ENTIDAD", participante.Field<string>("ENTIDAD"));
                                            clsServicioCorreoElectronico.Tokens.Add("SOLICITANTE", string.Format("{0} {1}", clsPersona.Identity.PrimerNombre, clsPersona.Identity.PrimerApellido));
                                            clsServicioCorreoElectronico.Tokens.Add("TIPO_IDENTIFICACION", clsPersona.Identity.TipoDocumentoIdentificacion.Sigla);
                                            clsServicioCorreoElectronico.Tokens.Add("NUMERO_IDENTIFICACION", clsPersona.Identity.NumeroIdentificacion);
                                            clsServicioCorreoElectronico.Tokens.Add("ACTIVIDAD", participante.Field<string>("ACTIVITYNAME"));
                                            clsServicioCorreoElectronico.Tokens.Add("FECHA_VENCIMIENTO_ACTIVIDAD", participante.Field<DateTime>("FECHA_VENCIMIENTO_ACTIVIDAD").ToShortDateString());
                                            clsServicioCorreoElectronico.Tokens.Add("NRO_VITAL", clsSolicitudDAAEIA.Identity.NumeroSilpa);
                                            var tipoProyecto = dtFormularioProcessInstance.AsEnumerable().Where(x => x.Field<int>("IDFIELD") == Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["IDFIELD_FORM_PINES_TIPO_PROYECTO"].ToString())).Select(g => new { VALUE = g.Field<string>("VALUE") }).ToList().First();
                                            clsServicioCorreoElectronico.Tokens.Add("TIPO_PROYECTO", tipoProyecto.VALUE);
                                            clsServicioCorreoElectronico.Tokens.Add("FECHA_ENVIO", DateTime.Now.ToString());
                                            int plantillaID = (int)EnumPlantillaCorreo.TareaProximaAVencercePINES;
                                            clsServicioCorreoElectronico.Enviar(plantillaID);
                                        }
                                    }
                                }
                            }

                        }
                        

                    }
                }
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
    }
}
