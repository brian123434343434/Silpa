using SILPA.AccesoDatos.Encuestas.Contingencias.Dalc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.HtmlControls;

namespace SILPA.LogicaNegocio.Encuestas.Contingencias
{
    public class GeneracionPDFContingencias
    {
        private GeneracionPDFContingenciasDalc _generacionPDFDalc;

        public GeneracionPDFContingencias()
        {
            _generacionPDFDalc = new GeneracionPDFContingenciasDalc();
            
        }

        public void GenerarDocumentoPDFInfoInicial(ref DataSet p_objDatosXml, int p_intSolicitudID)
        {
            try
            {
                // consulta los datos del formulario de cambios menores
                DataSet dtsInfoFormulario = _generacionPDFDalc.ConsultarDatosContingenciasInfoInicialSolicitudID(p_intSolicitudID);
                string idFormulario = p_objDatosXml.Tables["FORMULARIO"].Rows[0]["idFormulario"].ToString();
                //TODO: GENERACION DEL METODO PARA ARMAR EL HTML DEL FORMULARIO asignanos nombres a las tablas
                if (dtsInfoFormulario.Tables[0].Rows.Count > 0)
                {
                    dtsInfoFormulario.Tables[0].TableName = "DATOS_FORMULARIO";
                    dtsInfoFormulario.Tables[1].TableName = "DATOS_SECCIONES_SOLICITUD";
                    dtsInfoFormulario.Tables[2].TableName = "DATOS_PREGUNTAS_SECCIONES_SOLICITUD";
                    dtsInfoFormulario.Tables[3].TableName = "DATOS_RESPUESTA_PREGUNTA_SECCION_SOLICITUD";

                    //foreach (DataRow drDocumentoSolicitud in dtsInfoFormulario.Tables[4].Rows)
                    //{
                    //    drDocumentoSolicitud["IDFORMULARIO"] = idFormulario;
                    //}

                    foreach (DataRow drFormulario in dtsInfoFormulario.Tables["DATOS_FORMULARIO"].Rows)
                    {
                        drFormulario["IDFORMULARIO"] = idFormulario;
                    }

                    // validamos si viene alguna pregunta de tipo ubicacion geografica para obtener el municipio de la contingencia.
                    if (dtsInfoFormulario.Tables["DATOS_PREGUNTAS_SECCIONES_SOLICITUD"].AsEnumerable().Where(x => x.Field<Int32>("ENCTIPO_PREGUNTA_ID") == 3).Count() > 0)
                    {
                        DataRow dtrPreguntaUbicacion = dtsInfoFormulario.Tables["DATOS_PREGUNTAS_SECCIONES_SOLICITUD"].AsEnumerable().Where(x => x.Field<Int32>("ENCTIPO_PREGUNTA_ID") == 3).FirstOrDefault();
                        int intPreguntaUbicacion = Convert.ToInt32(dtrPreguntaUbicacion["ENCPREGUNTA_ID"]);

                        DataRow dtrRespuestaUbicacion = dtsInfoFormulario.Tables["DATOS_RESPUESTA_PREGUNTA_SECCION_SOLICITUD"].AsEnumerable().Where(x => x.Field<Int32>("PREGUNTA_ID") == intPreguntaUbicacion).FirstOrDefault();

                        string  strMunicipioUbicacion = dtrRespuestaUbicacion["ID_RESPUESTA"].ToString().Split('|')[1];
                        foreach (DataRow drFormulario in p_objDatosXml.Tables["FORMULARIO"].Rows)
                        {
                            drFormulario["municipio"] = strMunicipioUbicacion;
                        }
                    }
                }
                
                p_objDatosXml.Tables["DATOS_FORMULARIO"].Merge(dtsInfoFormulario.Tables[0]);
                p_objDatosXml.Tables["DATOS_SECCIONES_SOLICITUD"].Merge(dtsInfoFormulario.Tables["DATOS_SECCIONES_SOLICITUD"]);
                p_objDatosXml.Tables["DATOS_PREGUNTAS_SECCIONES_SOLICITUD"].Merge(dtsInfoFormulario.Tables["DATOS_PREGUNTAS_SECCIONES_SOLICITUD"]);
                p_objDatosXml.Tables["DATOS_RESPUESTA_PREGUNTA_SECCION_SOLICITUD"].Merge(dtsInfoFormulario.Tables["DATOS_RESPUESTA_PREGUNTA_SECCION_SOLICITUD"]);
            }
            catch (Exception)
            {
                
                throw;
            }
            

        }
        public void GenerarDocumentoPDFInfoParcial(ref DataSet p_objDatosXml, string numeroVITALInicial)
        {
            try
            {
                // consulta los datos del formulario de cambios menores
                DataSet dtsInfoFormulario = _generacionPDFDalc.ConsultaDatosContingenciaInfoInicialNumeroVITAL(numeroVITALInicial);
                if (dtsInfoFormulario.Tables[0].Rows.Count > 0)
                {
                    DataRow dtrRegistroContingenciaInicial = dtsInfoFormulario.Tables[0].AsEnumerable().FirstOrDefault();
                    foreach (DataRow drFormulario in p_objDatosXml.Tables["FORMULARIO"].Rows)
                    {
                        drFormulario["expediente"] = dtrRegistroContingenciaInicial["CODIGO_EXPEDIENTE"].ToString();
                        drFormulario["nombre_proyecto"] = dtrRegistroContingenciaInicial["NOMBRE_PROYECTO"].ToString();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
