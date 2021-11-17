using SILPA.Comun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.Tramites.BeneficioTributario.CertificadoUPME.Entidades
{
    [Serializable]
    public class solicitantes_secundarios : EntidadSerializable
    {
        public string id { get; set; }
        public string nombre { get; set; }
        public string tipo_identificacion { get; set; }
        public string identificacion { get; set; }
        public string telefono { get; set; }
        public string direccion { get; set; }
        public string nombre_representante { get; set; }
        public string email_representante { get; set; }
        public string telefono_representante { get; set; }
        public string nombre_contacto { get; set; }
        public string email_contacto { get; set; }
        public string telefono_contacto { get; set; }
        public string codigo_ciiu { get; set; }
        public string municipio { get; set; }
        public codigo_ciiu objCodigoCIIU { get; set; }
        public municipios objMunicipios { get; set; }
    }
}
