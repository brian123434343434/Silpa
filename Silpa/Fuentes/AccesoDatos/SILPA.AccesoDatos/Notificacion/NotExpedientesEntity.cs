using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.Notificacion
{
     [Serializable]
    public class NotExpedientesEntity : EntidadSerializable
    {

        public NotExpedientesEntity()
        {
           
        }


        public int IDTIPNOT { get; set; }

        public int ID_SOLICITUD_EXPEDIENTE{get; set;}

        public string ID_SOLICITUD { get; set; }

        public String ID_EXPEDIENTE { get; set; }

        public String DESC_EXPEDIENTE { get; set; }

        public int SOL_ID_AA { get; set; }

        public String DESC_SOL_ID_AA { get; set; }

        public int SOL_ID_SOLICITANTE { get; set; }

        public String DESC_SOL_ID_SOLICITANTE { get; set; }

        public String SOL_NUMERO_SILPA { get; set; }

        public bool ACTIVO { get; set; }

       

    }
}
