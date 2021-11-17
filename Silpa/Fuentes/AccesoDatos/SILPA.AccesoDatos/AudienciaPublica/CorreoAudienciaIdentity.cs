using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.AudienciaPublica
{

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://tempuri.org/Sancionatorio.xsd")]
    [System.Xml.Serialization.XmlRootAttribute("CorreoAudiencia", Namespace = "http://tempuri.org/Sancionatorio.xsd", IsNullable = false)]


    public class CorreoAudienciaIdentity : EntidadSerializable
    {

        private string NumeroSilpaSolicitudField;
        private string numeroSilpaInscripcionField;
        private string NumeroAudienciaPublicaField;
        private string nombreArchivosField;

        private string mensajeField;
        private List<string> listaArchivosField;

      
        /// <summary>
        /// Numero silpa Solicitud audiencia pública
        /// </summary>
        public string NumeroSilpaSolicitud
        {
            get
            {
                return this.NumeroSilpaSolicitudField;
            }
            set
            {
                this.NumeroSilpaSolicitudField = value;
            }
        }



        /// <summary>
        /// Numero silpa Solicitud audiencia pública
        /// </summary>
        public string NumeroSilpaInscripcion
        {
            get
            {
                return this.numeroSilpaInscripcionField;
            }
            set
            {
                this.numeroSilpaInscripcionField = value;
            }
        }


        /// <summary>
        /// Numero de audiencia pública
        /// </summary>
        public string NumeroAudienciaPublica
        {
            get
            {
                return this.NumeroAudienciaPublicaField;
            }
            set
            {
                this.NumeroAudienciaPublicaField = value;
            }
        }


        /// <summary>
        /// Nombre de archivos para adjuntar
        /// </summary>
        public string nombreArchivos
        {
            get
            {
                return this.nombreArchivosField;
            }
            set
            {
                this.nombreArchivosField = value;
            }
        }

      
        /// <summary>
        /// Mensaje del correo a enviar
        /// </summary>
        public string Mensaje
        {
            get
            {
                return this.mensajeField;
            }
            set
            {
                this.mensajeField = value;
            }
        }


       /// <summary>
       /// Lista de archivos adjuntos
       /// </summary>
        public List<string> listaArchivos
        {
            get { return listaArchivosField; }
            set { listaArchivosField = value; }
        }

    }
}
