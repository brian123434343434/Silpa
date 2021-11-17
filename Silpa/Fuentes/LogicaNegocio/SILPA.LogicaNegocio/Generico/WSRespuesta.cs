using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.LogicaNegocio.Generico
{
    /// <summary>
    /// clase que contiene los datos de respuesta de los servicios web de silpa
    /// </summary>
    public class WSRespuesta:EntidadSerializable
    {
        
        /// <summary>
        /// Constructor sin parametros
        /// </summary>
        public WSRespuesta(){}

        /// <summary>
        /// Constructor que recibe parametros
        /// </summary>
        /// <param name="strIdSilpa"></param>
        /// <param name="strIdExterno"></param>
        /// <param name="strCodigoMensaje"></param>
        /// <param name="strMensaje"></param>
        /// <param name="blnExito"></param>
        public WSRespuesta
            (
                string strIdSilpa, 
                string strIdExterno,
                string strCodigoMensaje,
                string strMensaje,
                bool blnExito
            )
        {
                this._idSilpa = strIdSilpa;
                this._idExterno = strIdExterno;
                this._codigoMensaje = strCodigoMensaje;
                this._mensaje = strMensaje;
                this._exito = blnExito;
        }


        /// <summary>
        /// algún identificador del sistema silpa generado en algún proceso 
        /// util para ligar con datos de sistemas externos
        /// </summary>
        private string _idSilpa;

        /// <summary>
        /// Identificador proveniente de algún sistema externo
        /// </summary>
        private string _idExterno;

        /// <summary>
        /// Coigo del mensaje generado desde el servicio
        /// </summary>
        private string _codigoMensaje;

        /// <summary>
        /// Mensaje generado en el servicio
        /// </summary>
        private string _mensaje;

        /// <summary>
        /// Estado de exito o fracaso de la operación realizada por el servicio
        /// </summary>
        private bool _exito;

        public string IdSilpa { get { return this._idSilpa; } set { this._idSilpa = value; } }
        public string IdExterno { get { return this._idExterno; } set { this._idExterno = value; } }
        public string CodigoMensaje { get { return this._codigoMensaje; } set { this._codigoMensaje = value; } }
        public string Mensaje { get { return this._mensaje; } set { this._mensaje = value; } }
        public bool Exito { get { return this._exito; } set { this._exito = value; } }
    }
}

