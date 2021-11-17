using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SILPA.Comun;
using SILPA.LogicaNegocio.Salvoconducto.Entidades;
using SILPA.LogicaNegocio.WSSUN;
using SILPA.LogicaNegocio.Excepciones;
using SoftManagement.Log;

namespace SILPA.LogicaNegocio.Salvoconducto
{
    public class ServicioSalvoConducto
    {
        #region Metodos Publicos


            /// <summary>
            /// Verificar la existencia de una resolución
            /// </summary>
            /// <param name="p_intAutoridadAmbiental">int con el identificador de la autoridad ambiental</param>
            /// <param name="p_intSolicitanteID">int con el identificador del solicitante</param>
            /// <param name="p_strNumeroResolucion">string con el número de resolución</param>
            /// <param name="p_objFechaResolucion">DateTime con la fecha de resolución</param>
            /// <returns></returns>
            public string VerificarResolucionAprovechamiento(int p_intAutoridadAmbiental, int p_intSolicitanteID, string p_strNumeroResolucion, DateTime p_objFechaResolucion)
            {
                XmlSerializador objSerializador = null;
                WSSUN.WSSUN objWSSUN = null;
                ResolucionEntity objResolucionEntity = null;
                SUNRespuestaEntity objSUNRespuestaEntity = null;
                string strResolucion = "";
                string strRespuesta = "";
                string strMensajeRespuesta = "";

                try
                {
                    //Crear objeto de serializacion
                    objSerializador = new XmlSerializador();

                    //Cargar datos de objeto
                    objResolucionEntity = new ResolucionEntity
                    {
                        AutoridadAmbientalID = p_intAutoridadAmbiental,
                        SolicitanteID = p_intSolicitanteID,
                        NumeroResolucion = p_strNumeroResolucion,
                        FechaResolucion = p_objFechaResolucion
                    };
                    strResolucion = objSerializador.serializar(objResolucionEntity);


                    //Generar liquidación y cobro
                    objWSSUN = new WSSUN.WSSUN();
                    objWSSUN.Url = SILPA.Comun.DireccionamientoWS.UrlWS("WSSUN");
                    strRespuesta = objWSSUN.VerificarResolucionSUN(strResolucion);

                    //Serializar y obtener información de la liquidación                    
                    objSUNRespuestaEntity = (SUNRespuestaEntity)objSerializador.Deserializar(new SUNRespuestaEntity(), strRespuesta);

                    if (objSUNRespuestaEntity.Codigo == "1")
                    {
                        strMensajeRespuesta = "No se encontro información asociada a la resolución " + p_strNumeroResolucion + " del " + p_objFechaResolucion.ToString("dd/MM/yyyy") + " la cual otorgue permiso de aprovechamiento.";
                    }
                    else if (objSUNRespuestaEntity.Codigo != "0" && objSUNRespuestaEntity.Codigo != "1")
                    {
                        throw new Exception(objSUNRespuestaEntity.Mensaje);
                    }
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "ServicioSalvoConducto :: GenerarLiquidacionAutoliquidacionSILA -> Error Inesperado: " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw new SUNServicioException("ServicioSalvoConducto :: GenerarLiquidacionAutoliquidacionSILA -> Error Inesperado: " + exc.Message, exc.InnerException);
                }

                return strMensajeRespuesta;
            }


        #endregion

    }
}
