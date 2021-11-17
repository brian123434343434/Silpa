using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.REDDS
{
    [Serializable]
    public class Redds
    {
        public int ReddsID { get; set; }
        public string NombreRazonSocial { get; set; }
        public string NombreIniciativa { get; set; }
        public int EstadoAvanceIniciativa { get; set; }
        public double? CostoEstimadoFormulacion { get; set; }
        public DateTime FechaInicioImplementacion { get; set; }
        public DateTime FechaFinImplementacion { get; set; }
        public string EstandarMercadeo { get; set; }
        public string MetodologiaEstandarMercadeo { get; set; }
        public double AreaInfluencia { get; set; }
        public string DocumentoDiseño { get; set; }
        public string ArchivosShape { get; set; }
        public string NumeroVital { get; set; }
        public int? RelacionJuridicaID { get; set; }
        public string DescRelacionJuridica { get; set; }
        public string MyProperty { get; set; }
        public List<ReddsParticipante> LstParticipante { get; set; }
        public List<ReddsEstimadoReduccionEmisiones> LstEstimadoReduccionEmisiones { get; set; }
        public List<ReddsEstimadoReduccionDeforestacion> LstEstimadoReduccionDeforestacion { get; set; }
        public List<ReddsActividad> LstActividad { get; set; }
        public List<ReddsCompartimientoCarbono> LstCompartimentoCarbono { get; set; }
        public List<ReddsAutoridadAmbiental> LstAutoridades { get; set; }
        public List<ReddsDeptoMunicipio> LstDeptoMunicipio { get; set; }
        public List<ReddsArchivos> LstArchivo { get; set; }
        public List<ReddsLocalizacion> LstLocalizacion { get; set; }
        public List<ReddsEstandarMercado> LstEstandarMercado { get; set; }
    }
}
