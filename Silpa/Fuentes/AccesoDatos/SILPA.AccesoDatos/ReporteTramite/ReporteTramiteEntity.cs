using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.ReporteTramite
{    
    public partial class ReporteTramiteEntity : EntidadSerializable
    {
        private DateTime FechaCreacionField;
        private int idTipoTramiteField;
        private string nombreTipoTramiteField;
        private int autIDField;
        private string autNombreField;
        private int idSolicitanteField;
        private int idProcessInstanceField;
        private string numeroSILPAField;

        /// <summary>
        /// Fecha de la solicitud
        /// </summary>
        public DateTime FechaCreacion
        {
            get
            {
                return this.FechaCreacionField;
            }
            set
            {
                this.FechaCreacionField = value;
            }
        }

        /// <summary>
        /// Código del tipo de tramite
        /// </summary>
        public int idTipoTramite
        {
            get
            {
                return this.idTipoTramiteField;
            }
            set
            {
                this.idTipoTramiteField = value;
            }
        }

        /// <summary>
        /// Nombre del tipo de tramite
        /// </summary>
        public string nombreTipoTramite
        {
            get
            {
                return this.nombreTipoTramiteField;
            }
            set
            {
                this.nombreTipoTramiteField = value;
            }
        }


        /// <summary>
        /// Código del la autoridad ambiental 
        /// </summary>
        public int autID
        {
            get
            {
                return this.autIDField;
            }
            set
            {
                this.autIDField = value;
            }
        }



        /// <summary>
        /// Nombre de la autoridad ambiental
        /// </summary>
    
        public string autNombre
        {
            get
            {
                return this.autNombreField;
            }
            set
            {
                this.autNombreField = value;
            }
        }

        /// <summary>
        /// Código del solicitante
        /// </summary>
        public int idSolicitante
        {
            get
            {
                return this.idSolicitanteField;
            }
            set
            {
                this.idSolicitanteField = value;
            }
        }

        /// <summary>
        /// Código del formulario de proceso
        /// </summary>
        public int idProcessInstance
        {
            get
            {
                return this.idProcessInstanceField;
            }
            set
            {
                this.idProcessInstanceField= value;
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
    }   
}
