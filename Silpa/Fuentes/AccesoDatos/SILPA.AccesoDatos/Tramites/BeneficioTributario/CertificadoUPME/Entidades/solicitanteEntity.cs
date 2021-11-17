using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Tramites.BeneficioTributario.CertificadoUPME.Entidades
{
    public class solicitanteEntity
    {
        public int certificadoID { get; set; }
        public int solicitanteID { get; set; }
        public string nombre { get; set; }
        public string sectorProductivo { get; set; }
        public string codigoCIIU { get; set; }
        public string tipoIdentificacion { get; set; }
        public string identificacion { get; set; }
        public string domicilio { get; set; }
        public string telefono { get; set; }
        public string direccion { get; set; }
        public string nombreContacto { get; set; }
        public string emailContacto { get; set; }
        public string telefonoContacto { get; set; }
        public enumTipoSolicitante TipoSolicitante { get; set; }
    }

    public enum enumTipoSolicitante
    {
        principal = 1,
        secundario = 2
    }
}
