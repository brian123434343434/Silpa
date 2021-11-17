using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.Comunicacion
{
    public class EsperarRespuestaIdentity
    {

        public EsperarRespuestaIdentity()
        {
        }

        private int idRespuestaField;

        private string numSilpaField;

        private string numExpedienteField;

        private DateTime fechaEnvioField;

        private DateTime fechaEsperaField;

        private bool envioCorreoField;

        private EsperarRespuestaEntidadIdentity[] esperarRespuestaEntidadField;

        /// <comentarios/>
        public int idRespuesta
        {
            get
            {
                return this.idRespuestaField;
            }
            set
            {
                this.idRespuestaField = value;
            }
        }
        /// <comentarios/>
        public string numSilpa
        {
            get
            {
                return this.numSilpaField;
            }
            set
            {
                this.numSilpaField = value;
            }
        }

        /// <comentarios/>
        public string numExpediente
        {
            get
            {
                return this.numExpedienteField;
            }
            set
            {
                this.numExpedienteField = value;
            }
        }

        /// <comentarios/>
        public DateTime fechaEnvio
        {
            get
            {
                return this.fechaEnvioField;
            }
            set
            {
                this.fechaEnvioField = value;
            }
        }

        /// <comentarios/>
        public DateTime fechaEspera
        {
            get
            {
                return this.fechaEsperaField;
            }
            set
            {
                this.fechaEsperaField = value;
            }
        }

        /// <comentarios/>
        public bool envioCorreo
        {
            get
            {
                return this.envioCorreoField;
            }
            set
            {
                this.envioCorreoField = value;
            }
        }

        /// <comentarios/>
        public EsperarRespuestaEntidadIdentity[] esperarRespuestaEntidad
        {
            get { return this.esperarRespuestaEntidadField; }
            set { this.esperarRespuestaEntidadField = value; }
        }

    }
}
