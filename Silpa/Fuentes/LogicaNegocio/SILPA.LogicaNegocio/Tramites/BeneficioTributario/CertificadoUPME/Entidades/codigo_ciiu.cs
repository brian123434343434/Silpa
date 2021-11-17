using SILPA.Comun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.Tramites.BeneficioTributario.CertificadoUPME.Entidades
{
    [Serializable]
    public class codigo_ciiu : EntidadSerializable
    {
        public string clase { get; set; }
        public string descripcion { get; set; }
        public string sector_productivo { get; set; }
        public sector_productivo objSectorProductivo { get; set; }

    }
}
