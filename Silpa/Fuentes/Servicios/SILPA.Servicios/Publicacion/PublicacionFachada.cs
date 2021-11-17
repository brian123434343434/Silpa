using System;
using System.Collections.Generic;
using System.Text;
using SILPA.LogicaNegocio.Publicacion;
using SILPA.LogicaNegocio.Generico;
using SoftManagement.Log;
using SILPA.AccesoDatos.Publicacion; 


namespace SILPA.Servicios.Publicacion
{
    public class PublicacionFachada
    {

        public SILPA.LogicaNegocio.Publicacion.Publicacion _objPublicacion;

        /// <summary>
        /// Constructor
        /// </summary>
        public PublicacionFachada()
        {
        }

        /// <summary>
        /// Inserta una publicacion en la base de datos utilizando la clase Publicacion de la Logica de Negocio.
        /// </summary>
        /// <param name="strXmlDatos">Datos de la publicacion en formato XML</param>
        /// <returns>Informacion de la publicacion almacenada en formato XML</returns>
        public long InsertarPublicacion(PublicacionIdentity ObjPublicacionIdentity)
        {
            long lngPublicacionID = -1;

            try
            {
	            SMLog.Escribir(Severidad.Informativo, "ingresa a Insertar Publicacion");              
	            _objPublicacion = new SILPA.LogicaNegocio.Publicacion.Publicacion();
                if (ObjPublicacionIdentity.PublicacionID != null && ObjPublicacionIdentity.PublicacionID.Value > 0)
                {
                    _objPublicacion.EliminarPublicacion(ObjPublicacionIdentity.PublicacionID.Value);
                    lngPublicacionID = _objPublicacion.InsertarPublicacion(ObjPublicacionIdentity);
                }
                else
                {
                    lngPublicacionID = _objPublicacion.InsertarPublicacion(ObjPublicacionIdentity);
                }
	            SMLog.Escribir(Severidad.Informativo, "finaliza a Insertar Publicacion");
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al Insertar Publicación.";
                throw new Exception(strException, ex);
            }

            return lngPublicacionID;
        }


        public void InsertarPublicacion(string strXmlDatos, string listaDocumentos)
        {
            _objPublicacion = new SILPA.LogicaNegocio.Publicacion.Publicacion();
            //return _objPublicacion.InsertarPublicacion(strXmlDatos);
            _objPublicacion.InsertarPublicacion(strXmlDatos, listaDocumentos);
        }
        

        ///// <summary>
        ///// Inserta una publicacion en la base de datos utilizando la clase Publicacion de la Logica de Negocio.
        ///// </summary>
        ///// <param name="strXmlDatos">Datos de la publicacion en formato XML</param>
        ///// <returns>Informacion de la publicacion actualizada en formato XML</returns>
        //public string AclararPublicacion(string strXmlDatos)
        //{
        //    _objPublicacion = new SILPA.LogicaNegocio.Publicacion.Publicacion();
        //    return _objPublicacion.AclararPublicacion(strXmlDatos);
        //}

        /// <summary>
        /// Elimina una publicacion en la base de datos utilizando la clase Publicacion de la Logica de Negocio.
        /// </summary>
        /// <param name="strXmlDatos">Datos de la publicacion en formato XML</param>
        /// <returns>Informacion de la publicacion eliminada en formato XML</returns>
        public void EliminarPublicacion(long p_lngPublicacionID)
        {
            _objPublicacion = new SILPA.LogicaNegocio.Publicacion.Publicacion();
            _objPublicacion.EliminarPublicacion(p_lngPublicacionID);
        }

        /// <summary>
        /// Actualiza la fecha de desfijacion de la pubi=licacion actual
        /// </summary>
        /// <param name="p_lngPublicacionID">long con el identificador de la publicacion</param>
        /// <param name="p_objFechaDesfijacion">DateTime con la fecha de desfijacion</param>
        public void ActualizarDesfijarPublicacion(long p_lngPublicacionID, DateTime p_objFechaDesfijacion)
        {
            _objPublicacion = new SILPA.LogicaNegocio.Publicacion.Publicacion();
            _objPublicacion.ActualizarDesfijarPublicacion(p_lngPublicacionID, p_objFechaDesfijacion);
        }
    }
}
