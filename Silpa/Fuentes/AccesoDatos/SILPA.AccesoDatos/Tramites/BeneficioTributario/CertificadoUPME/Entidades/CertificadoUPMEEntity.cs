using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Tramites.BeneficioTributario.CertificadoUPME.Entidades
{
    public class CertificadoUPMEEntity
    {
        public string numeroSILPA { get; set; }
        public int certificadoID { get; set; }
        public string tipoIdentificacion { get; set; }
        public string numeroCertificado { get; set; }
        public string tipoCertificacion { get; set; }
        public string nombreProyecto { get; set; }
        public string ubicacionGeografia { get; set; }
        public string departamento { get; set; }
        public string municipio { get; set; }
        public decimal energiaAnualGenerada { get; set; }
        public string fuenteNoConvencional { get; set; }
        public int fuenteConvencionalSustituirID { get; set; }
        public string fuenteConvencionalSustituir { get; set; }
        public decimal emisionesCO2 { get; set; }
        public decimal calculoANLA { get; set; }
        public decimal valorTotalInversion { get; set; }
        public decimal valorIVA { get; set; }
        public List<bienesEntity> lstBienes { get; set; }
        public List<serviciosEntity> lstServicios { get; set; }
        public List<solicitanteEntity> lstSolicitanteSecundario { get; set; }
        public solicitanteEntity solicitantePrincial { get; set; }
        public decimal latitud { get; set; }
        public decimal longitud { get; set; }
        public string etapa { get; set; }
        public string rutaDescripcionProyecto { get; set; }
        public string rutaSoportePago { get; set; }
        public int subFuenteConvencionalSustituirID { get; set; }
        public string subFuenteConvencionalSustituir { get; set; }
        public string numeroReferenciaPago { get; set; }
        public CertificadoUPMEEntity()
        {
            lstBienes = new List<bienesEntity>();
            lstServicios = new List<serviciosEntity>();
            lstSolicitanteSecundario = new List<solicitanteEntity>();
        }
    }
}
