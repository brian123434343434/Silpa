using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.EvaluacionREA.Entity
{
    [Serializable]
    public class SolicitudREAEntity
    {
        public int SolicitudREAID { get; set; }
        public int SolicitanteID { get; set; }
        public string NumeroVITAL { get; set; }
        public int? AutoridadAmbientalID { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public int DuracionPermiso { get; set; }
        public List<InsumosGrupoBiologicoEntity> LstInsumosGrupoBilogocio { get; set; }
        public List<InsumoCoberturaEntity> LstIsnumosCobertura { get; set; }
        public List<DocumentoSolicitudREAEntity> LstDocumentos { get; set; }
    }
}
