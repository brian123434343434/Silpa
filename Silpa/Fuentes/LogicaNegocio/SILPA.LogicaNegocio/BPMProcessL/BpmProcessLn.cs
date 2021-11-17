using System;
using System.Collections.Generic;
using System.Text;
using SILPA.AccesoDatos.BPMProcess;
//using System.Windows.Documents;
using SILPA.Comun;
using System.Xml.Serialization;
using SoftManagement.Log;
using System.IO;
using System.Data;

namespace SILPA.LogicaNegocio.BPMProcessL
{
    public class BpmProcessLn
    {
       
        private Configuracion objConfiguracion;

        public BpmProcessLn()
        {
            objConfiguracion = new Configuracion();
        }
        

        public string crearProceso(string ClientId,long FormId, long UserID, string ValoresXML)
        {
            List<ValoresIdentity> objValoresIdentity = new List<ValoresIdentity>();
            XmlSerializador _ser = new XmlSerializador();
            objValoresIdentity = (List<ValoresIdentity>)_ser.Deserializar(new List<ValoresIdentity>(), ValoresXML);
         
            //objValoresIdentity = (ValoresIdentityList)objValoresIdentity.Deserializar(ValoresXML);
            string CadenaValores = "";
            ProcessDalc objProcess = new ProcessDalc();
            List<CampoIdentity> arrayCampos = objProcess.ObtenerCampos(FormId);
            foreach (ValoresIdentity objValores in objValoresIdentity)
            {
                if ((arrayCampos[objValores.Id - 1].Tipo == "DropDownList"))
                {
                    if (string.IsNullOrEmpty(objValores.Valor) || objValores.Valor=="-1")
                    {
                        //Valor por defecto en FormBulder cuando no se selecciona Archivo
                        objValores.Valor = "-2147483648";
                    }
                }

                
                CadenaValores = CadenaValores + "{" + objValores.Id.ToString() + "," + objValores.Grupo + "," + objValores.Valor.Replace(","," ") + "," + objValores.Orden.ToString() + "}";
            }
            ProcessIdentity pro = new ProcessIdentity();
            pro.IdForm = FormId;
            pro.IdUser = UserID;
            pro.Cadena= CadenaValores;
            objProcess.CrearProceso(ref pro);            
            if (pro.Resp.Contains("ERROR"))
            {
                SMLog.Escribir(Severidad.Critico, pro.Resp);
                return pro.Resp;
            }
            foreach (ValoresIdentity objValores in objValoresIdentity)
            {
                if (arrayCampos[objValores.Id - 1].Tipo == "FileUpLoad")
                {
                    if (objValores.Archivo.Length > 1)
                    {
                        CargarArchivo(objValores.Valor, objValores.Archivo);
                    }
                    else
                    {
                        SMLog.Escribir(Severidad.Informativo, "Archivo Formulario Manual Longitud 1: " + objValores.Valor);
                    }
                }

            }        
            bpmServices.GattacaBPMServices9000 objBPMServices = new bpmServices.GattacaBPMServices9000();
            objBPMServices.Url = SILPA.Comun.DireccionamientoWS.UrlWS("bpmServices");
            objBPMServices.Credentials = Credenciales();
            Int64 idProcessCase = GetIdProcessCase(FormId);
            Int64 processinstance = objBPMServices.WMCreateProcessInstance(ClientId, UserID, idProcessCase, 0, "VBFormBuilder", pro.IdFormInstance.ToString(), FormId.ToString());
            Int64 activityInstance = objBPMServices.WMStartProcessInstance(ClientId, UserID, processinstance);
     
            if (objBPMServices.AttachDataToActivityInstance(ClientId, UserID, activityInstance, processinstance, "VBFormBuilder", pro.IdFormInstance.ToString(), FormId.ToString()))
            {
                return NumeroSilpaxIdProcessInstance(processinstance);
            }
          
            //Guardar Archivos
                  
            return "Error al crear la solicitud";
        }


        public string crearProcesoAutoridad(Int64 TramiteId, Int64 PerId,Int64 AA, string ValoresXML)
        {
            try
            {
                string ClientId;
                Int64 FormId;
                Int64 UserId;

                ProcessDalc objProceso = new ProcessDalc();
                DataSet ParametrosFormulario = objProceso.ObtenerParametrosPorTramite(TramiteId, PerId);
                if (ParametrosFormulario.Tables.Count > 0)
                {
                    ClientId = ParametrosFormulario.Tables[0].Rows[0]["CLIENT_ID"].ToString();
                    FormId = Int64.Parse(ParametrosFormulario.Tables[0].Rows[0]["FORM_ID"].ToString());
                    UserId = Int64.Parse(ParametrosFormulario.Tables[0].Rows[0]["APPLICATION_USER"].ToString());
                    
                    return crearProcesoAutoridad(ClientId, FormId, UserId, AA, ValoresXML);
                }
                else
                    return "No Existen Parametros para este trámite y esta persona. ";
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al Crear Proceso de Autoridad.";
                throw new Exception(strException, ex);
            }
        }

        public string crearProcesoAutoridad(string ClientId, long FormId, long UserID, Int64 AA, string ValoresXML)
        {
            int intIdMaximo = 0;

            try
			{
	            List<ValoresIdentity> objValoresIdentity = new List<ValoresIdentity>();
	            XmlSerializador _ser = new XmlSerializador();
	            objValoresIdentity = (List<ValoresIdentity>)_ser.Deserializar(new List<ValoresIdentity>(), ValoresXML);
	
	            //objValoresIdentity = (ValoresIdentityList)objValoresIdentity.Deserializar(ValoresXML);
	            string CadenaValores = "";
	            ProcessDalc objProcess = new ProcessDalc();
	            List<CampoIdentity> arrayCampos = objProcess.ObtenerCampos(FormId);
	            foreach (ValoresIdentity objValores in objValoresIdentity)
	            {
	                if (arrayCampos[objValores.Id - 1].Tipo == "DropDownList")
	                {
	                    if (string.IsNullOrEmpty(objValores.Valor))
	                    {
	                        //Valor por defecto en FormBulder cuando no se selecciona Archivo
	                        objValores.Valor = "-2147483648";
	                    }
	                }
	                CadenaValores = CadenaValores + "{" + objValores.Id.ToString() + "," + objValores.Grupo + "," + objValores.Valor + "," + objValores.Orden.ToString() + "}";

                    if (intIdMaximo < objValores.Id)
                        intIdMaximo = objValores.Id;
	            }
	            //Se quema la informacion del departamento
	            //Ajuste para generar la cantidad de parametros equivalente al proceso
	            SILPA.AccesoDatos.Generico.ParametroDalc _parametroDalc = new SILPA.AccesoDatos.Generico.ParametroDalc();
	            SILPA.AccesoDatos.Generico.ParametroEntity _parametro = new SILPA.AccesoDatos.Generico.ParametroEntity();
	            SILPA.AccesoDatos.Generico.ParametroEntity _parametro2 = new SILPA.AccesoDatos.Generico.ParametroEntity();
	            SILPA.AccesoDatos.Generico.ParametroEntity _parametro3 = new SILPA.AccesoDatos.Generico.ParametroEntity();
	            _parametro.IdParametro = -1;
	            _parametro.NombreParametro = "Form_Com_EE";
	            _parametroDalc.obtenerParametros(ref _parametro);
	
	            _parametro2.IdParametro = -1;
	            _parametro2.NombreParametro = "Form_Sol_Info_Add";
	            _parametroDalc.obtenerParametros(ref _parametro2);
	
	            _parametro3.IdParametro = -1;
	            _parametro3.NombreParametro = "ID_Formulario_Cobro";
	            _parametroDalc.obtenerParametros(ref _parametro3);

                if ((arrayCampos.Count - objValoresIdentity.Count) == 7 && FormId.ToString() != _parametro.Parametro.ToString() && FormId.ToString() != _parametro2.Parametro.ToString() && FormId.ToString() != _parametro3.Parametro.ToString())
	            {
	                DataTable dtDeptoMun = objProcess.ConsultarAADepto(AA).Tables[0];
                    CadenaValores = CadenaValores + "{" + (intIdMaximo > 0 ? intIdMaximo + 1 : 3).ToString() + ",List1," + dtDeptoMun.Rows[0]["DEP_ID"] + ",1}";
                    CadenaValores = CadenaValores + "{" + (intIdMaximo > 0 ? intIdMaximo + 2 : 4).ToString() + ",List1," + dtDeptoMun.Rows[0]["MUN_ID"] + ",1}";
                    CadenaValores = CadenaValores + "{" + (intIdMaximo > 0 ? intIdMaximo + 3 : 5).ToString() + ",List1,-2147483648,1}";
                    CadenaValores = CadenaValores + "{" + (intIdMaximo > 0 ? intIdMaximo + 4 : 6).ToString() + ",List1,-2147483648,1}";
                    CadenaValores = CadenaValores + "{" + (intIdMaximo > 0 ? intIdMaximo + 5 : 7).ToString() + ",List1,-2147483648,1}";
                    CadenaValores = CadenaValores + "{" + (intIdMaximo > 0 ? intIdMaximo + 6 : 8).ToString() + ",List1,-2147483648,1}";
                    CadenaValores = CadenaValores + "{" + (intIdMaximo > 0 ? intIdMaximo + 7 : 9).ToString() + ",List1,-2147483648,1}";
	            }
	       
	            ProcessIdentity pro = new ProcessIdentity();
	            pro.IdForm = FormId;
	            pro.IdUser = UserID;

	            pro.Cadena = CadenaValores;
	            objProcess.CrearProceso(ref pro);
	            if (pro.Resp.Contains("ERROR"))
	            {
	                SMLog.Escribir(Severidad.Critico, pro.Resp);
	                return pro.Resp;
	            }
	            SMLog.Escribir(Severidad.Critico, "Ingreso ProcessService");
	            foreach (ValoresIdentity objValores in objValoresIdentity)
	            {
	                SMLog.Escribir(Severidad.Critico, " Tipo " + arrayCampos[objValores.Id - 1].Tipo.ToString());
	                if (arrayCampos[objValores.Id - 1].Tipo == "FileUpLoad")
	                {
                        //Verificar que el archivo contenga información
                        if (objValores.Archivo.Length > 1)
                        {
                            SMLog.Escribir(Severidad.Critico, "Sin entro" + objValores.Valor);
                            CargarArchivo(objValores.Valor, objValores.Archivo);
                        }
                        else
                        {
                            SMLog.Escribir(Severidad.Informativo, "Archivo Formulario Manual Longitud 1: " + objValores.Valor);
                        }
	                }
	            }
	
	            bpmServices.GattacaBPMServices9000 objBPMServices = new bpmServices.GattacaBPMServices9000();
	            objBPMServices.Url = SILPA.Comun.DireccionamientoWS.UrlWS("bpmServices");
	            objBPMServices.Credentials = Credenciales();
	            Int64 idProcessCase = GetIdProcessCase(FormId);
	            Int64 processinstance = objBPMServices.WMCreateProcessInstance(ClientId, UserID, idProcessCase, 0, "VBFormBuilder", pro.IdFormInstance.ToString(), FormId.ToString());
	            Int64 activityInstance = objBPMServices.WMStartProcessInstance(ClientId, UserID, processinstance);

	            /**/
	            if (objBPMServices.AttachDataToActivityInstance(ClientId, UserID, activityInstance, processinstance, "VBFormBuilder", pro.IdFormInstance.ToString(), FormId.ToString()))
	            {
	                string strNumeroSilpa = NumeroSilpaxIdProcessInstance(processinstance);
	
	                /* Se ingresa la relación de números silpa de padre e hijo */
	                if(objValoresIdentity.Count > 0)
	                {
	                    objProcess.CrearRelacionSilpaPadreHijo(objValoresIdentity[0].Valor, strNumeroSilpa);
	                }
	                return strNumeroSilpa;
	            }
	            //Guardar Archivos

	            return "Error al crear la solicitud";
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al Crear Proceso de Autoridad.";
                throw new Exception(strException, ex);
            }
        }


        /// <summary>
        /// Método especial para crear proceso al momento de comunicación EE
        /// </summary>
        /// <param name="ClientId"></param>
        /// <param name="FormId"></param>
        /// <param name="UserID"></param>
        /// <param name="AA"></param>
        /// <param name="ValoresXML"></param>
        /// <returns></returns>
        public string crearProcesoAutoridadComEE(string ClientId, long FormId, long UserID, Int64 AA, string ValoresXML)
        {
            List<ValoresIdentity> objValoresIdentity = new List<ValoresIdentity>();
            XmlSerializador _ser = new XmlSerializador();
            objValoresIdentity = (List<ValoresIdentity>)_ser.Deserializar(new List<ValoresIdentity>(), ValoresXML);

            //objValoresIdentity = (ValoresIdentityList)objValoresIdentity.Deserializar(ValoresXML);
            string CadenaValores = "";
            ProcessDalc objProcess = new ProcessDalc();
            List<CampoIdentity> arrayCampos = objProcess.ObtenerCampos(FormId);
            foreach (ValoresIdentity objValores in objValoresIdentity)
            {
                if (arrayCampos[objValores.Id - 1].Tipo == "DropDownList")
                {
                    if (string.IsNullOrEmpty(objValores.Valor))
                    {
                        //Valor por defecto en FormBulder cuando no se selecciona Archivo
                        objValores.Valor = "-2147483648";
                    }
                }
                CadenaValores = CadenaValores + "{" + objValores.Id.ToString() + "," + objValores.Grupo + "," + objValores.Valor + "," + objValores.Orden.ToString() + "}";
            }
            //Se quema la informacion del departamento
            DataTable dtDeptoMun = objProcess.ConsultarAADepto(AA).Tables[0];
            CadenaValores = CadenaValores + "{3,List1," + dtDeptoMun.Rows[0]["DEP_ID"] + ",1}";
            CadenaValores = CadenaValores + "{4,List1," + dtDeptoMun.Rows[0]["MUN_ID"] + ",1}";
            CadenaValores = CadenaValores + "{5,List1,-2147483648,1}";
            CadenaValores = CadenaValores + "{6,List1,-2147483648,1}";
            CadenaValores = CadenaValores + "{7,List1,-2147483648,1}";
            CadenaValores = CadenaValores + "{8,List1,-2147483648,1}";
            CadenaValores = CadenaValores + "{9,List1,-2147483648,1}";
            ProcessIdentity pro = new ProcessIdentity();
            pro.IdForm = FormId;
            pro.IdUser = UserID;
            pro.Cadena = CadenaValores;
            objProcess.CrearProceso(ref pro);
            if (pro.Resp.Contains("ERROR"))
            {
                SMLog.Escribir(Severidad.Critico, pro.Resp);
                return pro.Resp;
            }
            //SMLog.Escribir(Severidad.Critico, "Ingreso consultar Archivo");
            foreach (ValoresIdentity objValores in objValoresIdentity)
            {
                //SMLog.Escribir(Severidad.Critico, " Tipo " + arrayCampos[objValores.Id - 1].Tipo.ToString());
                if (arrayCampos[objValores.Id - 1].Tipo == "FileUpLoad")
                {
                   //SMLog.Escribir(Severidad.Critico, "Sin entro" + objValores.Valor);
                    CargarArchivo(objValores.Valor, objValores.Archivo);
                }
            }

            bpmServices.GattacaBPMServices9000 objBPMServices = new bpmServices.GattacaBPMServices9000();
            objBPMServices.Url = SILPA.Comun.DireccionamientoWS.UrlWS("bpmServices");
            Int64 idProcessCase = GetIdProcessCase(FormId);
            Int64 processinstance = objBPMServices.WMCreateProcessInstance(ClientId, UserID, idProcessCase, 0, "VBFormBuilder", pro.IdFormInstance.ToString(), FormId.ToString());
            
            //Este es un proceso de comunicacion EE,  asi que la AA relacionada a la tabla DAA_Solicitud queda vacía o no corresponde 
            //a la AA destino de la solicitud ,asi que se setea el dato proveniente desde el xml. 
            objProcess.ActualizarAAProcesoSolicitudComEE(processinstance, Convert.ToInt32(AA));
            
            Int64 activityInstance = objBPMServices.WMStartProcessInstance(ClientId, UserID, processinstance);

            if (objBPMServices.AttachDataToActivityInstance(ClientId, UserID, activityInstance, processinstance, "VBFormBuilder", pro.IdFormInstance.ToString(), FormId.ToString()))
            {
                return NumeroSilpaxIdProcessInstance(processinstance);
            }

            //Guardar Archivos

            return "Error al crear la solicitud";
        }

        private void CargarArchivo(string nombreArchivo, Byte[] strArchivoByte)
        {
            try
            {
                if (string.IsNullOrEmpty(nombreArchivo))
                    return ;
                else
                    if (strArchivoByte.Length==0)
                    {
                        SMLog.Escribir(Severidad.Critico,"Crear Proceso : Envio Nombre de archivo pero no envio archivo string to Byte[]");
                        return;
                    }
                
                string ruta = objConfiguracion.FileTraffic;
                string e = Path.GetExtension(nombreArchivo);
                SMLog.Escribir(Severidad.Informativo, "Adjutar Archivo : " + nombreArchivo + "| Ruta:" + ruta);
                //string prueba = nombreArchivo + e;
                nombreArchivo = nombreArchivo.Replace(e, "");
                int i=1;
                while (File.Exists(ruta + "/" + nombreArchivo + e))
                {
                    nombreArchivo += nombreArchivo + i.ToString();
                    i += 1;
                }
                nombreArchivo = nombreArchivo + e;
                TraficoDocumento objFileTraffic = new TraficoDocumento();
                List<String> lstStrNombreDocumento = new List<String>();
                lstStrNombreDocumento.Add(nombreArchivo);
                List<string> lstNombres = lstStrNombreDocumento;
                List<Byte[]> lstBytesDocumento=new List<Byte[]>();                
                lstBytesDocumento.Add(strArchivoByte);
              
                //bool ResCargue = objFileTraffic.RecibirDocumento(numerosilpa, clientId, lstBytesDocumento, ref lstStrNombreDocumento, ref ruta);
                System.IO.File.WriteAllBytes(objConfiguracion.DirectorioGattaca  + nombreArchivo, strArchivoByte);
            }
            catch (Exception ex)
            {
                SMLog.Escribir(Severidad.Critico, "Crear Proceso : Error al cargar archivo");

                string strException = "Validar los pasos efectuados al cargar archivo.";
                throw new Exception(strException, ex);

                return;
            }
        }

        /// <summary>
        /// Realizar el cierre de una actividad
        /// </summary>
        /// <param name="p_intUserID">int con el Identificador del usuario al cual pertenece la actividad</param>
        /// <param name="p_intIdActivityInstance">int con el Identificador de la instancia de la actividad</param>
        /// <param name="p_intIdProcessInstace">int con el id del proceso</param>
        public void CerrarActividad(int p_intUserID, int p_intIdActivityInstance, int p_intIdProcessInstace)
        {
            //Crear objeto gattaca que realizará operación
            bpmServices.GattacaBPMServices9000 objBPMServices = new bpmServices.GattacaBPMServices9000();
            objBPMServices.Url = SILPA.Comun.DireccionamientoWS.UrlWS("bpmServices");
            objBPMServices.Credentials = Credenciales();

            //Cerrar la actividad
            bpmServices.ListItem[] condiciones = objBPMServices.GetConditionsByActivityInstance("SoftManagement", p_intUserID, p_intIdActivityInstance);
            objBPMServices.EndActivityInstance("SoftManagement", p_intUserID, p_intIdActivityInstance, p_intIdProcessInstace, condiciones[0].Value, "", "", "", "", "");
        }


        public string NumeroSilpaxIdProcessInstance(Int64 processinstance)
        {
            try
            {
	            ProcessDalc objProcess = new ProcessDalc();
	            return objProcess.GetNumeroSilpa(processinstance);
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al consultar el Número VITAL asociado a un IdProcessInstance.";
                throw new Exception(strException, ex);
            }
        }

        public Int64 GetIdProcessCase(Int64 FormId)
        {
            try
            {
	            ProcessDalc objProcess = new ProcessDalc();
	            return objProcess.GetProcessCase(FormId);
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al consultar el número VITAL después de ejecutar STARPROCESINSTANCE.";
                throw new Exception(strException, ex);
            }
        }
        public string ObtenerFormularios()
        {
            try
            {
                ProcessDalc objProcess = new ProcessDalc();
                List<FormularioIdentity> arrayFormularios = objProcess.ObtenerFormularios();
                MemoryStream memoryStream = new MemoryStream();
                XmlSerializer serializador = new XmlSerializer(typeof(List<FormularioIdentity>));
                serializador.Serialize(memoryStream, arrayFormularios);

                return System.Text.UTF8Encoding.UTF8.GetString(memoryStream.ToArray());
            }
            catch (Exception ex)
            {
                SMLog.Escribir(ex);
                throw;
            }
        }
        public string ObtenerCampos(Int64 idForm)
        {
            try
            {
                ProcessDalc objProcess = new ProcessDalc();
                List<CampoIdentity> arrayCampos= objProcess.ObtenerCampos(idForm);
                MemoryStream memoryStream = new MemoryStream();
                XmlSerializer serializador = new XmlSerializer(typeof(List<CampoIdentity>));
                serializador.Serialize(memoryStream, arrayCampos);

                return System.Text.UTF8Encoding.UTF8.GetString(memoryStream.ToArray());
            }
            catch (Exception ex)
            {
                SMLog.Escribir(ex);
                throw;
            }
        }


        /// <summary>
        /// HAVA:05-ABR-2011
        /// Crea las credenciales para los servicios.
        /// </summary>
        /// <param name="user">usuario</param>
        /// <param name="password">clave</param>
        /// <returns></returns>
        public static System.Net.NetworkCredential Credenciales()
        {
            try
            {
	            string user = System.Configuration.ConfigurationManager.AppSettings["usuario_servicio"].ToString();
	            string pass = System.Configuration.ConfigurationManager.AppSettings["clave_servicio"].ToString();
	            System.Net.NetworkCredential credencial = new System.Net.NetworkCredential(user, pass);
	            return credencial;
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al crear las credenciales para los servicios.";
                throw new Exception(strException, ex);
            }
        }

    }
}
