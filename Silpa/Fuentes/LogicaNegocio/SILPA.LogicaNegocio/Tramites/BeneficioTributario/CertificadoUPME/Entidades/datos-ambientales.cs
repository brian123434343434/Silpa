using SILPA.Comun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.Tramites.BeneficioTributario.CertificadoUPME.Entidades
{
    [Serializable]
    public class datos_ambientales : EntidadSerializable
    {
        public string id { get; set; }
        public decimal? emisiones_generadas_sin { get; set; }
        public decimal? emisiones_generadas_con { get; set; }
    }
}
