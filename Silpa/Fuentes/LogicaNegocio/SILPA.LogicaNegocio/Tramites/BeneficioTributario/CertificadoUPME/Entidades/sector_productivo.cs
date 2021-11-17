using SILPA.Comun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.Tramites.BeneficioTributario.CertificadoUPME.Entidades
{
    [Serializable]
    public class sector_productivo : EntidadSerializable
    {
        public string division { get; set; }
        public string descripcion { get; set; }
    }
}
