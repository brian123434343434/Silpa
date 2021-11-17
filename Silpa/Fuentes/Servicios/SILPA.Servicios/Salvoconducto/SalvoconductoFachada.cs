using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using SILPA.LogicaNegocio.Salvoconducto;
using SILPA.LogicaNegocio.Generico;
using SILPA.LogicaNegocio.Salvoconducto.Entidades;
using SoftManagement.Log;

namespace SILPA.Servicios.Salvoconducto
{
    public class SalvoconductoFachada
    {

        public SILPA.LogicaNegocio.Salvoconducto.Salvoconducto _objSalvoconducto;

        public SalvoconductoFachada()
        {
        }

        public string GuardarSalvoconducto(string xmlDatos)
        {
            _objSalvoconducto = new SILPA.LogicaNegocio.Salvoconducto.Salvoconducto();
            return _objSalvoconducto.InsertarSalvoconducto(xmlDatos);

            
        }

        public string GuardarRecursos(string datosSalvoconductoXML)
        {
            _objSalvoconducto = new SILPA.LogicaNegocio.Salvoconducto.Salvoconducto();
            return _objSalvoconducto.InsertarRecurso(datosSalvoconductoXML);

        }


        /// <summary>
        /// Verificar la existencia de una resolución
        /// </summary>
        /// <param name="p_strInformacionResolucion">string con la información de la resolución a validar</param>
        /// <param name="p_strRutaArchivosTemporales">string con la ruta donde se escriben los archivos temporales</param>
        /// <returns>string con el XML que contiene la información de la solicitud</returns>
        public string VerificarResolucionSUN(string p_strInformacionResolucion)
        {
            SUNRespuestaEntity objSUNRespuestaEntity = null;
            ResolucionEntity objResolucionEntity = null;
            XmlSerializador objSerializador = null;
            Resolucion objResolucion = null;
            bool blnExiste = false;

            try
            {
                //Verificar que el contenido no sea nulo
                if (!string.IsNullOrEmpty(p_strInformacionResolucion))
                {
                    //Cargar la informacion
                    objSerializador = new XmlSerializador();
                    objResolucionEntity = (ResolucionEntity)objSerializador.Deserializar(new ResolucionEntity(), p_strInformacionResolucion);

                    //Verificar datos de resolución
                    objResolucion = new Resolucion();
                    blnExiste = objResolucion.ExisteResolucion(objResolucionEntity);

                    //Verificar si existe
                    if (blnExiste)
                    {
                        //Cargar datos respuesta                  
                        objSUNRespuestaEntity = new SUNRespuestaEntity { Codigo = "0", Mensaje = "OK" };
                    }
                    else
                    {
                        objSUNRespuestaEntity = new SUNRespuestaEntity { Codigo = "1", Mensaje = "La resolución especificada no se encuentra relacionada" };
                    }
                }
                else
                {
                    objSUNRespuestaEntity = new SUNRespuestaEntity { Codigo = "3", Mensaje = "No se especifico información para ser validada" };
                }
            }
            catch (Exception exc)
            {
                //Cargar error
                objSUNRespuestaEntity = new SUNRespuestaEntity { Codigo = "4", Mensaje = "Se presento error durante la consulta" };

                //Escribir error
                SMLog.Escribir(Severidad.Critico, "SalvoconductoFachada :: VerificarResolucionSUN -> Error inesperado: " + exc.Message + " " + exc.StackTrace);
            }


            return objSUNRespuestaEntity.GetXml();
        }


    }
}
