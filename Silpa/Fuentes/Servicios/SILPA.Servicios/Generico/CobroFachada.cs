using System;
using System.Collections.Generic;
using System.Text;
using SILPA.LogicaNegocio.Generico;
using System.Data;
using SILPA.AccesoDatos.DAA;
using SILPA.AccesoDatos.Generico;
using Silpa.Workflow;
using SILPA.Comun;
using SoftManagement.Log;

namespace SILPA.Servicios.Generico
{
    public class CobroFachada
    {
        public void CrearCobro(string xmlDatosCobro, string usuario)
        {
            try
            {
	            Cobro _objCobro = new Cobro();
	            //xmlDatosCobro = _objCobro.cargarPrueba();
	            string strNumeroSilpa = _objCobro.CrearCobro(xmlDatosCobro);
	            //SolicitudDAAEIAIdentity _solicitud = new SolicitudDAAEIAIdentity();
	            //SolicitudDAAEIADalc _solicitudDalc = new SolicitudDAAEIADalc();
	            //BpmParametros _bpmParametrosDalc = new BpmParametros();
	            //SILPA.Servicios.BPMServices.GattacaBPMServices9000 _servicioBPM = new SILPA.Servicios.BPMServices.GattacaBPMServices9000();
	            //Proceso _objProceso = new Proceso();
	
	            //DataSet dsResultadoBPMParametro = new DataSet();
	            //dsResultadoBPMParametro = _bpmParametrosDalc.ListarBmpParametros(18);
	           // _solicitud = _solicitudDalc.ObtenerSolicitud(null, null, strNumeroSilpa);
	            //_objProceso.DeterminarActividad(_solicitud.IdProcessInstance);
	            //si hay resultados
	           // if (dsResultadoBPMParametro.Tables[0].Rows.Count > 0)
	           // {
	                //3B Se busca la condición para finalizar la actividad actual de bpm siempre y cuando la actividad siguiente posea un 
	                // formulario igual al formulario asociado a la actividad encontrada en el paso 3A
	               // int tipo = Convert.ToInt32(dsResultadoBPMParametro.Tables[0].Rows[0]["TIPO"]);
	               // string nombre = Convert.ToString(dsResultadoBPMParametro.Tables[0].Rows[0]["NOMBRE"]);
	
	                //switch (tipo)
	                //{
	                //    case 3:
	                //        _objProceso.CondicionSiguientePorActividad(_objProceso.IdActiviteInstance, nombre);
	                //        break;
	                //}
	
	                //Quiere decir que se encontró la condición que finaliza dicha actividad y avanza a la actividad escojida
	                //if (_objProceso.IdCondicion != string.Empty)
	                //{
	                //    //3C se finaliza la actividad
	                //    //PENDIENTE: no es siempre 1
	                //    _servicioBPM.EndActivityInstance("SoftManagement", 1, _objProceso.IdActiviteInstance,
	                //        _solicitud.IdProcessInstance, _objProceso.IdCondicion, "Actividad Finalizada desde Liquidación", "", "0", "0", "0");
	                //}
	
	
	                //Comun.XmlSerializador _objSerialcobro = new SILPA.Comun.XmlSerializador();
	                //CobroType cobrotype = new CobroType();
	                //cobrotype = (CobroType)_objSerialcobro.Deserializar(new CobroType(), xmlDatosCobro);
	
	                //string condicion = string.Empty;
	
	                //if(cobrotype.IdTipoDocumento!=null)
	                //{
	                //    TipoDocumentoDalc tipoDocDalc = new TipoDocumentoDalc();
	                //    condicion  = tipoDocDalc.ObtenerCondicionTipoDocumento(cobrotype.IdTipoDocumento);
	                //}
	
	
	                //if (cobrotype.IdTipoDocumento == (int)TipoDocumentoLiquidacionEvaluacion.OficioCobroEvaluacion)
	                //{
	                //    condicion = "VITALCONPRO02LA-05";
	                //}
	
	                //if (cobrotype.IdTipoDocumento == (int)TipoDocumentoLiquidacionEvaluacion.OficioSolicitudRequerimientosLiquidaciónEvaluacion)
	                //{
	                //    condicion = "VITALCONPRO02LA-02";  // VITALCONPRO02LA-02
	                //}
	
	                //condicion = "VITALCONPRO02LA-05";
	
	               // SMLog.Escribir(Severidad.Informativo, " Avanzar Tarea 756: Processinstance:" + _solicitud.IdProcessInstance.ToString() + " Activida:"  +  ActividadSilpa.RegistrarInformacionDocumento.ToString() + " Condicion:" + condicion + "  Usuario:"+usuario);
	                
	               // ServicioWorkflow servicioWorkflow = new ServicioWorkflow();
	               // servicioWorkflow.ValidarActividadActual(_solicitud.IdProcessInstance, ActividadSilpa.RegistrarInformacionDocumento);
	              //  servicioWorkflow.FinalizarTarea(_solicitud.IdProcessInstance, ActividadSilpa.RegistrarInformacionDocumento, usuario, condicion);
	
	           // }
	            ///SILPA.Servicios.ArctividadBPMFachada.DeterminarAvanceActividad(strNumeroSilpa);
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al recibir los datos del cobro entregados por la Autoridad Ambiental para generar el formulario de cobro.";
                throw new Exception(strException, ex);
            }
        }

        
        /// <summary>
        /// método que determina si la actividad actual en AttachDataToActivityInstance
        /// es una actividad que genera cobro o no.
        /// </summary>
        /// <param name="IdActivityInstance"></param>
        /// <param name="?"></param>
        public void CrearCobro(Int64 activityInstanceID, Int64 processInstanceID, string entryDataType,string entryData, string idEntryData)
        {
            Cobro _objCobro = new Cobro();
            /// se deben obtener los datos de cobro guardados en el Formbuilder
            /// carga de prueba:            
            SILPA.LogicaNegocio.DAA.SolicitudDAAEIA solDaa = new SILPA.LogicaNegocio.DAA.SolicitudDAAEIA();
            solDaa.ConsultarSolicitudByProcessInstance(processInstanceID);
            
            string xmlDatosCobro = _objCobro.cargarPrueba(solDaa.Identity.NumeroSilpa);
            //System.IO.File.WriteAllText(@"c:\temp\xmlDatosCobro.xml", xmlDatosCobro);

            string strNumeroSilpa = _objCobro.CrearCobro(xmlDatosCobro);
        }


        public string ConsultarCobro(string IdExpediente)
        {
            Cobro _objCobro = new Cobro();
            /// se deben obtener los datos de cobro guardados en el Formbuilder
            /// carga de prueba:            
            List<CobroPSE> objCobro= _objCobro.ConsultarDatosPagoPSE(IdExpediente);
            
            XmlSerializador ser = new XmlSerializador();
            return ser.serializar(objCobro);            
    

        }
    }
}
