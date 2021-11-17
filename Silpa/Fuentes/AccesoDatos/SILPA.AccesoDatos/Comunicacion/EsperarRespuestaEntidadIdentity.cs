using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.Comunicacion
{
  public class EsperarRespuestaEntidadIdentity
    {
      public EsperarRespuestaEntidadIdentity()
        {
        }

        private int idRespuestaField;

        private int idRespuestaEntidadField;

        private string nombreField;
   
        private string correoElectronicoField;
   
        private DateTime fechaRespuestaField;

      //07-jul-2010 - aegb
      private int idEntidadField;
      private string numSilpaField;

         /// <comentarios/>
       public int idRespuestaEntidad
        {
            get
            {
                return this.idRespuestaEntidadField;
            }
            set
            {
                this.idRespuestaEntidadField = value;
            }
        }
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
        public string nombre
        {
            get
            {
                return this.nombreField;
            }
            set
            {
                this.nombreField = value;
            }
        }
      

        /// <comentarios/>
        public string correoElectronico
        {
            get
            {
                return this.correoElectronicoField;
            }
            set
            {
                this.correoElectronicoField = value;
            }
        }

        /// <comentarios/>
        public DateTime fechaRespuesta
        {
            get
            {
                return this.fechaRespuestaField;
            }
            set
            {
                this.fechaRespuestaField = value;
            }
        }

        //07-jul-2010 - aegb
        /// <comentarios/>
        public int IdEntidad
        {
            get
            {
                return this.idEntidadField;
            }
            set
            {
                this.idEntidadField = value;
            }
        }
        /// <comentarios/>
        public string NumSilpa
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

    }
}
