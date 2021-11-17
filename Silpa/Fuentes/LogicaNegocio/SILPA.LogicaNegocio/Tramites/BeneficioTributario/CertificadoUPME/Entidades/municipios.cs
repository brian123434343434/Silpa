using SILPA.Comun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.Tramites.BeneficioTributario.CertificadoUPME.Entidades
{
    [Serializable]
    public class municipios : EntidadSerializable
    {
        public int id { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public string departamento { get; set; }
        public departamentos objdepartamento { get; set; }
    }
}
