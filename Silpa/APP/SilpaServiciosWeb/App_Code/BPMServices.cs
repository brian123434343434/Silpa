using System.IO;
using System.Xml;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Collections.Generic;
using System;
using System.Web.UI.WebControls;
using SILPA.Servicios;
using SILPA.Servicios.SolicitudDAA;
using SILPA.Servicios.Generico;
using SILPA.Servicios.Generico.RadicarDocumento;
using SoftManagement.Log;
using SILPA.Servicios.CesionDerechos;
using Referenciador;
using SILPA.Servicios.ImpresionFUS;
using SILPA.Servicios.PINES;
using SILPA.LogicaNegocio.DAA; //jmartinez obtengo la libreria para radicar 


[WebService(Namespace = "http://www.workflowcolombia.com/eworkflow/Services/WorkFlowServices.asmx")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class GattacaBPMServices9000 : System.Web.Services.WebService
{

    /// <summary>
    /// Url de ubicacion del servicio.
    /// </summary>
    private string url;


    /// <summary>
    /// constructor
    /// </summary>
    public GattacaBPMServices9000()
    {
        BPMServices.GattacaBPMServices9000 service = new BPMServices.GattacaBPMServices9000();
        this.url = service.Url;
        //this.url =  System.Configuration.ConfigurationManager.AppSettings["BpmServices"].ToString();
    }

    /// <summary>
    /// Constructor que permite el cambio de url del servicio del BPM Gattaca
    /// </summary>
    /// <param name="strUrl">url del servico</param>
    public GattacaBPMServices9000(string strUrl)
    {
        this.url = strUrl;
    }


    /// <summary>
    /// Configura el url del servicio del BPM Gattaca
    /// </summary>
    /// <param name="obj"></param>
    private void SetUrlServicio(ref BPMServices.GattacaBPMServices9000 obj)
    {
        if (this.url != string.Empty && this.url != null)
        {
            ((BPMServices.GattacaBPMServices9000)obj).Url = this.url;
        }
    }
    
    [WebMethod(Description = "Retorna True si la conexion al WS esta correcta")]
    public bool TestTransmission()
    {
        try
        {
        BPMServices.GattacaBPMServices9000 objBpmServices = new BPMServices.GattacaBPMServices9000();
        SetUrlServicio(ref objBpmServices);
        return objBpmServices.TestTransmission();
        }
        catch(Exception ex)
        {
            SMLog.Escribir(Severidad.Informativo, "++++Falla Incia TestTransmission: " +  ex.ToString());
            throw;
        }

    }

    [WebMethod(Description = "Retorna XML con los Paquetes asociados al cliente")]
    public string GetPackages(string Client, Int64 UserId, bool IsOnlyEnabled)
    {
        
        try{
        BPMServices.GattacaBPMServices9000 objBpmServices = new BPMServices.GattacaBPMServices9000();
        SetUrlServicio(ref objBpmServices);
        return objBpmServices.GetPackages(Client, UserId, IsOnlyEnabled);
        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Informativo, "++++Falla Incia GetPackages: " + ex.ToString());
            throw;
        }
    }


    [WebMethod(Description = "Retorna XML con los Procesos asociados al cliente")]
    public string GetProcesses(string Client,Int64 UserID,bool IsOnlyEnabled)
    {
     
        try
       {
          BPMServices.GattacaBPMServices9000 objBpmServices = new BPMServices.GattacaBPMServices9000();
            SetUrlServicio(ref objBpmServices);
            return objBpmServices.GetProcesses(Client, UserID, IsOnlyEnabled);
        }
        catch(Exception ex)
        {
            SMLog.Escribir(Severidad.Informativo, "++++Falla Incia GetProcesses: " + ex.ToString());
            throw;
        }
    }


    [WebMethod(Description = "Retorna XML con los Procesos asociados al cliente")]
    public string GetProcessById(string Client, Int64 UserID, Int64 IdProcess)
    {
        try
        {    
            BPMServices.GattacaBPMServices9000 objBpmServices = new BPMServices.GattacaBPMServices9000();
            SetUrlServicio(ref objBpmServices);
            return objBpmServices.GetProcessById(Client, UserID, IdProcess);
        }
        catch(Exception ex)
        {
            SMLog.Escribir(Severidad.Informativo, "++++Falla Incia GetProcessById: "  + ex.ToString());
            throw;
        }
    }

    [WebMethod(Description = "Retorna XML con los Procesos asociados al cliente")]
    public string GetActivityInstanceByUserId(string Client, Int64 UserID, Int16 Status, Int64 Activity,Int64 IdProcessInstance, string OrderBy)
    {
     
        try
        {
        BPMServices.GattacaBPMServices9000 objBpmServices = new BPMServices.GattacaBPMServices9000();
        SetUrlServicio(ref objBpmServices);
        return objBpmServices.GetActivityInstanceByUserId(Client, UserID, Status, Activity, IdProcessInstance, OrderBy);
        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Informativo, "++++Falla Incia GetActivityInstanceByUserId: " +  ex.ToString());
            throw;
        }

    }

    [WebMethod(Description = "Retorna XML con los Procesos asociados al cliente")]
    public string GetActivityInstancesByUserId(string Client, Int64 UserID, Int16 Status,Int64 IdProcessInstance, Int16 MaxInstanceActivity, string OrderBy)
    {
        
        try
        {
        BPMServices.GattacaBPMServices9000 objBpmServices = new BPMServices.GattacaBPMServices9000();
        SetUrlServicio(ref objBpmServices);
        return objBpmServices.GetActivityInstancesByUserId(Client, UserID, Status,IdProcessInstance, MaxInstanceActivity, OrderBy);

        }
        catch(Exception ex)
        {
            SMLog.Escribir(Severidad.Informativo, "++++Falla Incia GetActivityInstancesByUserId: " +  ex.ToString());
            throw;
        }
    }


    [WebMethod(Description = "Retorna XML con los Procesos asociados al cliente")]
    public string GetProcessInstancesById(string Client, Int64 UserID, Int64 ProcessInstancesId)
    {
        
        try
       {
        BPMServices.GattacaBPMServices9000 objBpmServices = new BPMServices.GattacaBPMServices9000();
        SetUrlServicio(ref objBpmServices);
        return objBpmServices.GetProcessInstancesById(Client, UserID, ProcessInstancesId);
                }
       catch(Exception ex)
       {
           SMLog.Escribir(Severidad.Informativo, "++++Falla Incia GetProcessInstancesById: "  + ex.ToString());
           throw;
       }
    }



    [WebMethod(Description = "Crear un proceso por medio del Generador")]
    public string CreateProcessXML(string Client, string sXML)
    {
       
        try
        {
            BPMServices.GattacaBPMServices9000 objBpmServices = new BPMServices.GattacaBPMServices9000();
            SetUrlServicio(ref objBpmServices);
            return objBpmServices.CreateProcessXML(Client, sXML);
        }
        catch(Exception ex)
        {
            SMLog.Escribir(Severidad.Informativo, "++++Falla Incia CreateProcessXML: "  + ex.ToString());
            throw;
        }
    }


    [WebMethod(Description = "Crea una Instancia de Proceso")]
    public Int64 WMCreateProcessInstance(string Client, Int64 UserID, Int64 IDProcessCase, Int64 Sequence, string EntryDataType, string IDEntryData, string EntryData)
    {

        String strMensaje2 = "Client: " + Client + " UserID: " + UserID.ToString() + "  IDProcessCase: " + IDProcessCase.ToString() + " Sequence: " + Sequence.ToString() + " EntryDataType:" + EntryDataType + "IDEntryData: " + IDEntryData + " EntryData:" + EntryData;
        SMLog.Escribir(Severidad.Informativo, "++++ Incia WMCreateProcessInstance: " + strMensaje2);      
        
        try
        {
            BPMServices.GattacaBPMServices9000 objBpmServices = new BPMServices.GattacaBPMServices9000();
            SetUrlServicio(ref objBpmServices);
            Int64 resultado = objBpmServices.WMCreateProcessInstance(Client, UserID, IDProcessCase, Sequence, EntryDataType, IDEntryData, EntryData);
           
            ProcesoFachada proceso = new ProcesoFachada(Client, UserID, resultado);
            
            //imp.AdicionarProcesoImpresionFus((Int32)resultado);
            if (proceso.rad.objRadicacion._objRadDocIdentity.Id != 0)
            {
                ImpresionFUSFachada.GenerarFus(proceso.rad.objRadicacion._objRadDocIdentity.Id, (Int32)resultado, proceso.rad.objRadicacion._objRadDocIdentity.UbicacionDocumento, proceso.rad.objRadicacion._objRadDocIdentity.NumeroVITALCompleto, proceso.rad.objRadicacion._objRadDocIdentity.IdSolicitante);
                proceso.rad.objRadicacion._objRadDocIdentity.Leido = false;
                proceso.rad.objRadicacion.ActualizarEstadoRadicacion();                
            }


            int? IdAA = proceso.rad.objRadicacion._objRadDocIdentity.IdAA;

            if (IdAA == null)
            {
                IdAA = 0;
            }

            return resultado;
        }
        catch (Exception ex)
        {
            String strMensaje = "Client: " + Client + " UserID: " + UserID.ToString() + "  IDProcessCase: " + IDProcessCase.ToString() + " Sequence: " + Sequence.ToString() + " EntryDataType:" + EntryDataType + "IDEntryData: " + IDEntryData + " EntryData:" + EntryData;
            SMLog.Escribir(Severidad.Critico, "++++Falla Incia WMCreateProcessInstance: " +strMensaje + ex.ToString());
            throw;
        }
        
    }



    /// <summary>
    /// Instancia los procesos de silpa
    /// </summary>
    /// <param name="Client"></param>
    /// <param name="UserID"></param>
    /// <param name="IDProcessInstance"></param>
    /// <returns></returns>
    [WebMethod(Description = "Inicia la ejecución de un proceso")]
    public Int64 WMStartProcessInstance(string Client, Int64 UserID, Int64 IDProcessInstance)
    {
        BPMServices.GattacaBPMServices9000 objBpmServices = new BPMServices.GattacaBPMServices9000();

        SetUrlServicio(ref objBpmServices);
        string data = Client + " -  " + UserID.ToString() + " -  " + IDProcessInstance.ToString() + objBpmServices.Url.ToString();

        Int64 resultado=0;
        try
        {
            resultado = objBpmServices.WMStartProcessInstance(Client, UserID, IDProcessInstance);
            Alarmas clsAlarmas = new Alarmas();
            clsAlarmas.NotificarCrearcionActivityInstance(UserID, IDProcessInstance);
            return resultado;
        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Informativo, "++++Falla Incia WMStartProcessInstance: " + ex.ToString());
            return resultado;
        }
        
        
    }


    [WebMethod(Description = "Finaliza la ejecución de una tarea")]
    public string EndActivityInstance
        (
        string Client, Int64 UserID,Int64 activityInstanceID,Int64 processInstanceID,string selectedCondition,string comments,
        string outComments, string entryDataType, string entryData, string idEntryData
        )
    {

        String strMensaje = "Client: " + Client + " UserID: " + UserID.ToString() + " activityInstanceID:" + activityInstanceID.ToString() + "  IDProcessCase: " + processInstanceID.ToString() +
            " selectedCondition: " + selectedCondition + " comments:" + comments + "outComments: " + outComments + " entryDataType:" + entryDataType + " entryData:" + entryData + " idEntryData:" + idEntryData;
        SMLog.Escribir(Severidad.Informativo, "++++Inicia la finalizacion de la tarea: " + strMensaje);
        BPMServices.GattacaBPMServices9000 objBpmServices = new BPMServices.GattacaBPMServices9000();
        SetUrlServicio(ref objBpmServices);

        return objBpmServices.EndActivityInstance
            (Client, UserID, activityInstanceID, processInstanceID, selectedCondition, comments,
              outComments, entryDataType, entryData, idEntryData);
    }


    [WebMethod(Description = "Retorna las Condiciones validas para una instancia de actividad")]
    public BPMServices.ListItem[] GetConditionsByActivityInstance(string Client, Int64 UserID, Int64 ActivityInstanceID)
    {
        BPMServices.GattacaBPMServices9000 objBpmServices = new BPMServices.GattacaBPMServices9000();
        SetUrlServicio(ref objBpmServices);
        return objBpmServices.GetConditionsByActivityInstance(Client,UserID,ActivityInstanceID);
    }


    [WebMethod(Description = "Retorna el tipo de condición de salida para una instancia de actividad")]
    public string GetConditionsTypeActivityInstance(string Client, Int64 UserID, Int64 ActivityInstanceID)
    {
        BPMServices.GattacaBPMServices9000 objBpmServices = new BPMServices.GattacaBPMServices9000();
        SetUrlServicio(ref objBpmServices);
        return objBpmServices.GetConditionsTypeActivityInstance(Client, UserID, ActivityInstanceID);
    }


    [WebMethod(Description = "Retorna las Condiciones validas para una actividad")]
    public string GetConditions(string Client, Int64 UserID, Int64 ActivityInstanceID)
    {     
        BPMServices.GattacaBPMServices9000 objBpmServices = new BPMServices.GattacaBPMServices9000();
        SetUrlServicio(ref objBpmServices);
        return objBpmServices.GetConditions(Client, UserID, ActivityInstanceID);
    }

    /// <summary>
    /// Método donde se instancia la Solicitud DAA
    /// </summary>
    /// <param name="Client"></param>
    /// <param name="UserID"></param>
    /// <param name="activityInstanceID"></param>
    /// <param name="processInstanceID"></param>
    /// <param name="entryDataType"></param>
    /// <param name="entryData"></param>
    /// <param name="idEntryData"></param>
    /// <returns>bool: True-> Exito / False: Fracaso</returns>
    [WebMethod(Description = "Adjunta a una instancia de actividad una referencia de datos de una herramienta externa.")]
    public bool AttachDataToActivityInstance(string Client,Int64 UserID,Int64 activityInstanceID,
                                             Int64 processInstanceID,string entryDataType,
                                             string entryData,string idEntryData)
    {

        
        bool blnResult = false;
        string strMensaje = "Client: " + Client + " UserId: " + UserID + " activityInstanceID: " + activityInstanceID + " processInstanceID: " + processInstanceID + " entryDataType: " + entryDataType + " entryData: " + entryData + " idEntryData: " + idEntryData;
        SMLog.Escribir(Severidad.Informativo, "++++Incia AttachDataToActivityInstanc -- " + strMensaje);

        try
        {
            BPMServices.GattacaBPMServices9000 objBpmServices = new BPMServices.GattacaBPMServices9000();
            SetUrlServicio(ref objBpmServices);

            //SMLog.Escribir(Severidad.Informativo, "radicado desde:  AttachDataToActivityInstance A");

            RadicacionDocumentoFachada objRadFachada = new RadicacionDocumentoFachada();
            objRadFachada.EnviarDatosRadicacion(processInstanceID, entryData, idEntryData);

            blnResult = objBpmServices.AttachDataToActivityInstance(Client, UserID, activityInstanceID, processInstanceID, entryDataType,
                                                      entryData, idEntryData);

            // Se actualiza el participante de la forma
            // en el caso en que exista cesion de derechos..
            CesionFechada ces = new CesionFechada();
            ces.ActualizarParticipanteForma(processInstanceID);

            objRadFachada.VerificarActividadRadicable(Client, UserID, activityInstanceID, processInstanceID, entryDataType,
                                                      entryData, idEntryData);


            string str_mensaje1 = DateTime.Now.ToString() + "   ********   ";

            str_mensaje1 = "pasa VerificarActividadRadicable: " + objRadFachada.objRadicacion._objRadDocIdentity.IdAA.ToString() + " NumeroSilpa: " + objRadFachada.objRadicacion._objRadDocIdentity.NumeroSilpa;

     

            objRadFachada.ConsultarAutoridad(objRadFachada.objRadicacion._objRadDocIdentity.IdAA);

            string str_mensaje2 = DateTime.Now.ToString() + "   ********   ";

            str_mensaje2 = "pasa ConsultarAutoridad";



            string str_ruta = objRadFachada.objRadicacion._objRadDocIdentity.UbicacionDocumento;
            string str_AA = objRadFachada.objAutoridad.objAutoridadIdentity.Nombre;

        
            SILPA.LogicaNegocio.Generico.Formulario objFormulario = new SILPA.LogicaNegocio.Generico.Formulario();
            objFormulario.ConsultaDatosEnvioCorreoEE(idEntryData, UserID, entryData, str_ruta, str_AA);
        

            return blnResult;
        }
        catch (Exception ex)
        {
            
            string str_mensaje = DateTime.Now.ToString() + "   ********   " + ex.ToString();

            str_mensaje = str_mensaje + "  Client:" + Client + "   UserID:" + UserID.ToString() +
                "  activityInstanceID:" + activityInstanceID.ToString() +
                "   processInstanceID:" + processInstanceID.ToString() +
                "   entryDataType:" + entryDataType.ToString() +
                "   entryData:" + entryData +
                "   idEntryData:" + idEntryData;

            

            SMLog.Escribir(Severidad.Informativo, "++++Fallo Incia AttachDataToActivityInstanc Detalle:" + str_mensaje);         
            throw;
        }
        
    }


    [WebMethod(Description = "Adjunta a una instancia de actividad un archivo.")]
    public bool AttachFileToActivityInstance(string Client ,Int64 UserID,Int64 activityInstanceID,Int64 processInstanceID, string FullFileName)
    {

        BPMServices.GattacaBPMServices9000 objBpmServices = new BPMServices.GattacaBPMServices9000();
        SetUrlServicio(ref objBpmServices);
        return objBpmServices.AttachFileToActivityInstance(Client ,UserID,activityInstanceID,processInstanceID, FullFileName);
    }

	
    [WebMethod(Description = "Adjunta a un comentario a una instancia de proceso.")]
	public bool AttachCommentToProcessInstance(string Client,Int64 UserID,Int64 processInstanceID,string  Comment)
    {
     
        BPMServices.GattacaBPMServices9000 objBpmServices = new BPMServices.GattacaBPMServices9000();
        return objBpmServices.AttachCommentToProcessInstance(Client,UserID,processInstanceID,Comment);
    }
	


    [WebMethod(Description = "Retorna XML con los casos de procesos")]
    public string GetProcessCasesByProcess(string Client,Int64 UserID,Int64 IdProcess,bool IsOnlyEnabled)
    {
     
        BPMServices.GattacaBPMServices9000 objBpmServices = new BPMServices.GattacaBPMServices9000();
        SetUrlServicio(ref objBpmServices);
        return objBpmServices.GetProcessCasesByProcess(Client,UserID,IdProcess,IsOnlyEnabled);
    }


    [WebMethod(Description = "Retorna XML con los Id de los formularios que contiene un caso de proceso")]
    public string GetFormsByProcessCase(string Client,Int64 UserID,Int64 IdProcessCase)
    {
     
        try
        {
            BPMServices.GattacaBPMServices9000 objBpmServices = new BPMServices.GattacaBPMServices9000();
            SetUrlServicio(ref objBpmServices);
            return objBpmServices.GetFormsByProcessCase(Client,UserID,IdProcessCase);
        }
        catch(Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, "Error: GetFormsByProcessCase +++++ " + ex.ToString());  
            throw;
        }
    }


    [WebMethod(Description = "Obtener todos los atributos del proceso. Retorna [Id, IdProcess, Name]")]
    public BPMServices.ProcessAttributeEntity[] GetProcessAttribute(string Client, Int64 UserID, Int64 IdProcess)
    {
  
        try
        {
            BPMServices.GattacaBPMServices9000 objBpmServices = new BPMServices.GattacaBPMServices9000();
            SetUrlServicio(ref objBpmServices);
            return objBpmServices.GetProcessAttribute(Client,UserID,IdProcess);
        }
        catch(Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, "Error: GetProcessAttribute +++++ " + ex.ToString());
            throw;
        }
    }


    [WebMethod(Description = "Obtener todos los valores de los atributos del proceso. Retorna [Id, IdProcess, Name]")]
    public BPMServices.ProcessAttributeEntity[] GetProcessAttributeValue(string Client, Int64 UserID, Int64 IdProcessInstance)
    {
        
        try
        {
            BPMServices.GattacaBPMServices9000 objBpmServices = new BPMServices.GattacaBPMServices9000();
            SetUrlServicio(ref objBpmServices);
            return objBpmServices.GetProcessAttributeValue(Client, UserID, IdProcessInstance);
        }
        catch(Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, "Error: GetProcessAttributeValue +++++ " + ex.ToString());
            throw;
        }
    }


    [WebMethod(Description = "Obtener un valor en especifico de los atributos del proceso. Retorna un DataTable")]
    public BPMServices.ProcessAttributeEntity[] GetProcessAttributeValueById(string Client,Int64 UserID ,Int64 lIdProcessInstance,Int64 IdProcessAttribute)
    {
        
        try
        {
            BPMServices.GattacaBPMServices9000 objBpmServices = new BPMServices.GattacaBPMServices9000();
            SetUrlServicio(ref objBpmServices);
            return objBpmServices.GetProcessAttributeValueById(Client,UserID,lIdProcessInstance,IdProcessAttribute);
        }
        catch(Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, "Error: GetProcessAttributeValueById +++++ " + ex.ToString());          
            throw;
        }
    }


    [WebMethod(Description = "Actualizar un valor en especifico de los atributos del proceso, si este se ejecuto correctamente retorna True")]
    public bool UpdateProcessAttributeValue(string Client,Int64 UserID, Int64  IdProcessInstance, Int64 IdProcessAttribute, string Value)
    {
        
        try 
        {
            BPMServices.GattacaBPMServices9000 objBpmServices = new BPMServices.GattacaBPMServices9000();
            SetUrlServicio(ref objBpmServices);
            return objBpmServices.UpdateProcessAttributeValue(Client, UserID, IdProcessInstance, IdProcessAttribute, Value);
        }
        catch(Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, "Error: UpdateProcessAttributeValue +++++ " + ex.ToString());            
            throw;
        }
    }

    [WebMethod(Description = "Metodo de test del servicio")]
    public void Test()
    {
        return;
    }

    
    /// <summary>
    /// mascara que utililiza la impresion dinámica de FUS
    /// </summary>
    /// <param name="idProcessInstance">Objecto que representa el Id de la instancia del proceso</param>
    private void EjecucionImpresion(int idProcessInstance)
    {
        try{
            //WSIMP01 impresor = new WSIMP01();
            Referenciador.WSIMP01.WSIMP01 impresor = new Referenciador.WSIMP01.WSIMP01();
            impresor.AdicionarProcesoImpresionFus(idProcessInstance);   
         }
        catch(Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, "Error: EjecucionImpresion +++++ " + ex.ToString());            
            throw;
        }
    }
}
