using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.REDDS

{
    [Serializable]
    public class ReddsParticipante
    {
        public int ParticipanteID { get; set; }
        public int ReddsID { get; set; }
        public string NombreParticipante { get; set; }
    }
}
