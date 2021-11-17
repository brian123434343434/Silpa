using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.Notificacion
{
     [Serializable]
    public class PersonaNotExpedienteEntity : EntidadSerializable
    {
        public PersonaNotExpedienteEntity()
        {
           
        }


        public Int32 ID { get; set; }

        public String PERSONA_IDENTIFICACION { get; set; }

        public int PERSONA_TIPO_IDENTIFICACION { get; set; }

        public String PERSONA_NOMBRE_COMPLETO { get; set; }

        public int PERSONA_PER_ID { get; set; }

        public int PERSONA_AA { get; set; }

        public String PERSONA_NOMBRE_AA { get; set; }

        public DateTime Fecha { get; set; }

        public bool Activo { get; set; }

        public String SOL_NUMERO_SILPA_PROCESO { get; set; }

        public bool ES_NOTIFICACION_ELECTRONICA_X_EXP { get; set; }

        public bool ES_NOTIFICACION_ELECTRONICA { get; set; }

        public bool ES_NOTIFICACION_ELECTRONICA_AA { get; set; }

        public bool EsNotificacionElec { get; set; }

        public int EstadoNotificacion { get; set; }

        
    }
}
