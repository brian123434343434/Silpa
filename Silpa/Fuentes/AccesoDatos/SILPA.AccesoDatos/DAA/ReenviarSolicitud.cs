using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.DAA
{
    public partial class ReenviarSolicitud : EntidadSerializable
    {

        private string numRadicacionField;

        /// <summary>
        /// Identificador de la autoridad nueva que recibirá la solicitud de radicación
        /// </summary>
        private int autIdAsignadaField;

        /// <summary>
        /// Identificador de la autoridad antigua que cede la solicitud de radicación
        /// </summary>
        private int autIdEntregaField;

        /// <summary>
        /// Número silpa completo ubicado en el expediente
        /// </summary>
        private string numeroSilpaField;

        private documentoAdjuntoType[] documentoAdjuntoField;


        /// <summary>
        /// Identifica el tipo documento vital
        /// </summary>
        private int idTipoDocumentoVital;
        public int IdTipoDocumentoVital
        {
            get { return idTipoDocumentoVital; }
            set { idTipoDocumentoVital = value; }
        }


        /// <comentarios/>
        public string numRadicacion
        {
            get
            {
                return this.numRadicacionField;
            }
            set
            {
                this.numRadicacionField = value;
            }
        }

        /// <comentarios/>
        public int autIdAsignada
        {
            get
            {
                return this.autIdAsignadaField;
            }
            set
            {
                this.autIdAsignadaField = value;
            }
        }

        /// <comentarios/>
        public int autIdEntrega
        {
            get
            {
                return this.autIdEntregaField;
            }
            set
            {
                this.autIdEntregaField = value;
            }
        }

        public string NumeroSilpa
        {
            get { return this.numeroSilpaField; }
            set { this.numeroSilpaField = value; }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute("documentoAdjunto")]
        public documentoAdjuntoType[] documentoAdjunto
        {
            get
            {
                return this.documentoAdjuntoField;
            }
            set
            {
                this.documentoAdjuntoField = value;
            }
        }
    }

}
