using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.ReporteTramite
{
    public class DocumentacionSolicitudEntity : EntidadSerializable
    {


        private DateTime FechaSolicitudField;
        private string numeroSILPAField;
        private string DescripcionField;
        private string RutaField;
        private string NumeroExpedienteField; 
        

        /// <summary>
        /// Fecha de la solicitud
        /// </summary>
        public DateTime FechaSolicitud
        {
            get
            {
                return this.FechaSolicitudField;
            }
            set
            {
                this.FechaSolicitudField = value;
            }
        }
              
 
        /// <summary>
        /// Número SILPA asignado
        /// </summary>
        public string numeroSILPA
        {
            get
            {
                return this.numeroSILPAField;
            }
            set
            {
                this.numeroSILPAField = value;
            }
        }

        /// <summary>
        /// Descripcion del documento
        /// </summary>
        public string Descripcion
        {
            get
            {
                return this.DescripcionField;
            }
            set
            {
                this.DescripcionField = value;
            }
        }
             
        /// <summary>
        /// Ruta del documento
        /// </summary>
        public string Ruta
        {
            get
            {
                return this.RutaField;
            }
            set
            {
                this.RutaField = value;
            }
        }

        /// <summary>
        /// Numero acto administrativo
        /// </summary>
        public string NumeroExpediente
        {
            get
            {
                return this.NumeroExpedienteField;
            }
            set
            {
                this.NumeroExpedienteField = value;
            }
        }
       
    }
}
