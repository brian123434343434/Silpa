using SILPA.Comun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.Tramites.BeneficioTributario.CertificadoUPME.Entidades
{
    [Serializable]
    public class incentivos : EntidadSerializable
    {
        public int id { get; set; }
        public string incentivo { get; set; }

    }
}
