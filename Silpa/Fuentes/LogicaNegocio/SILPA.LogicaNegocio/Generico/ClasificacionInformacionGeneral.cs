using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.AccesoDatos.Generico;
using SILPA.Comun;

namespace SILPA.LogicaNegocio.Generico
{
    public class ClasificacionInformacionGeneral
    {
        #region  Objetos

            private ClasificacionInformacionGeneralDalc _objClasificacionDalc;

        #endregion
        

        #region  Creadora

            /// <summary>
            /// Creadora
            /// </summary>
            public ClasificacionInformacionGeneral()
            {
                //Creary cargar configuración
                this._objClasificacionDalc = new ClasificacionInformacionGeneralDalc();
            }

        #endregion


        #region  Metodos Publicos

            /// <summary>
            /// Consultar las clasificaciones de informacion especificadas
            /// </summary>
            /// <param name="p_strDescripcion">string con la descripcion</param>
            /// <param name="p_blnActivo">bool que indica si solo se trae los activos</param>
            /// <returns>string con la informacion de las clasificaciones serializada</returns>
            public string ObtenerClasificaciones(string p_strDescripcion = "", bool? p_blnActivo = null)
            {
                List<ClasificacionInformacionGeneralIdentity> objLstClasificaciones = null;
                XmlSerializador objXmlSerializador = null;
                string strClasificaciones = "";

                //Consultar los datos
                objLstClasificaciones = this._objClasificacionDalc.ObtenerClasificaciones(p_strDescripcion, p_blnActivo);

                //Serializar datos
                objXmlSerializador = new XmlSerializador();
                strClasificaciones = objXmlSerializador.serializar(objLstClasificaciones);

                return strClasificaciones;
            }


            /// <summary>
            /// Obtiene la informacion de la clasificiacion especificada
            /// </summary>
            /// <param name="p_intClasificacionID">int con la identificacion de la clasificacion</param>
            /// <returns>string con la informacion de la clasificacion</returns>
            public string ObtenerClasificacionInformacionAdicional(int p_intClasificacionID)
            {
                ClasificacionInformacionGeneralIdentity objClasificacion = null;
                XmlSerializador objXmlSerializador = null;
                string strClasificaciones = "";

                //Consultar los datos
                objClasificacion = this._objClasificacionDalc.ObtenerClasificacionInformacionAdicional(p_intClasificacionID);

                //Serializar datos
                objXmlSerializador = new XmlSerializador();
                strClasificaciones = objXmlSerializador.serializar(objClasificacion);

                return strClasificaciones;
            }

        #endregion
    }
}
