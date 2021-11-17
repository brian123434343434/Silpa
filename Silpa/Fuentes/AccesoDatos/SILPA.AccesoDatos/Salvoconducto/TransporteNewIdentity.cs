using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Salvoconducto
{
    [Serializable]
    public class TransporteNewIdentity
    {
        public int TransporteSalvoconductoID { get; set; }
        public int SalvoconductoID { get; set; }
        public int TipoTransporteID { get; set; }
        public string TipoTransporteOtro { get; set; }
        public string TipoTransporte { get; set; }
        public int ModoTransporteID { get; set; }
        public string ModoTransporte { get; set; }
        public string Empresa { get; set; }
        public string NumeroIdentificacionMedioTransporte { get; set; }
        public string NombreTransportador { get; set; }
        public string NumeroIdentificacionTransportador { get; set; }
        public int TipoIdentificacionTransportadorID { get; set; }
        public string TipoIdentificacionTransportador { get; set; }
        //jmartinez salvoconducto fase 2
        public string CodigoIdeamTipoTransporte { get; set; }
        public string TipoIdentificacionTransportadorIDEAM { get; set; }
        public string CodigoIdeamModoTransporte { get; set; }

    }
}
