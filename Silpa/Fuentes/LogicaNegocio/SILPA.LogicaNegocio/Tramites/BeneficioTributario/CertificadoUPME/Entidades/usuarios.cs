using SILPA.Comun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.Tramites.BeneficioTributario.CertificadoUPME.Entidades
{
    [Serializable]
    public class usuarios : EntidadSerializable
    {
        public string username { get; set; }
        public string email { get; set; }
        public string first_name { get; set; }
        public int id { get; set; }
        public List<string> solicitante_principal { get; set; }
        public List<solicitantes_principales> lstSolicitantePrincipal { get; set; }
    }
}
