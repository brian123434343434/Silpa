using SILPA.Comun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.Tramites.BeneficioTributario.CertificadoUPME.Entidades
{
    [Serializable]
    public class proyectos : EntidadSerializable
    {
        public string id { get; set; }
        public string nombre { get; set; }
        public List<string> incentivos { get; set; }
        public List<incentivos> lstInsentivos { get; set; }
        public string etapa { get; set; }
        public decimal valor_inversion { get; set; }
        public string municipio { get; set; }
        public municipios objmunicipio { get; set; }
        public string usuario { get; set; }
        public usuarios objusuario { get; set; }
        public string estado { get; set; }
        public string radicado { get; set; }
        public string fecha_radicado { get; set; }
        public string carta { get; set; }
        public string formato { get; set; }
        public string certificado { get; set; }
        public string numero_certificado { get; set; }
        public List<string> solicitante_secundario { get; set; }
        public List<solicitantes_secundarios> lstSolicitanteSecundario { get; set; }
        public string generalidadesproyecto { get; set; }
        public generalidades objGeneralidadesproyecto { get; set; }
        public List<string> servicios { get; set; }
        public List<servicios> lstServicios { get; set; }
        public List<string> bienes { get; set; }
        public List<bien> lstBienes { get; set; }

    }
}
