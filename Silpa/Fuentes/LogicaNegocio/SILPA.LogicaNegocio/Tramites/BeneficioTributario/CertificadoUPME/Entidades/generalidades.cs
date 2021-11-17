using SILPA.Comun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.Tramites.BeneficioTributario.CertificadoUPME.Entidades
{
    [Serializable]
    public class generalidades : EntidadSerializable
    {
        public string id { get; set; }
        public string sector_socioeconomico { get; set; }
        public string tipo { get; set; }
        public string tipo_generador { get; set; }
        public string tipo_fnce { get; set; }
        public string recurso_energetico { get; set; }
        public string recurso_energetico_otro { get; set; }
        public string tecnologia { get; set; }
        public string tecnologias_otro { get; set; }
        public string datosambientales { get; set; }
        public datos_ambientales _objdatosambientales { get; set; }
        public string ubicaciongeneralidades { get; set; }
        public ubicaciones _objUbicaciones { get; set; }
        public string datostecnicos { get; set; }
        public datostecnicos _objdatostecnicos { get; set; }
        public string descripcion_proyecto { get; set; }


    }
}
