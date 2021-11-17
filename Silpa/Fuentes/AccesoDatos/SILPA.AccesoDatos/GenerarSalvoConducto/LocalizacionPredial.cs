using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.GenerarSalvoConducto
{
    public class LocalizacionPredial
    {

        public LocalizacionPredial()
        {
           
        }
        // Identificador del objeto
        public Int32? IDLocalizacionPredial { get; set; }
         // Identificador del objeto
        public Int32? ActoAdministrativo { get; set; }
        // Nombre del Predio
        public String NombrePredio { get; set; }
        // Identificador TipoPropiedadPredio
        public TipoPropiedadPredio TipoPropiedadPredio { get; set; }
        // Identificador OtrogaRegimenPropietario
        public String OtroRegimenPropietario { get; set; }
        // Identificador NumeroMatricula
        public String NumeroMatricula { get; set; }
        // Identificador Vereda
        public Vereda Vereda { get; set; }
        // Identificador Municipio
        public Int64? Municipio { get; set; }
        // Identificador Departamento
        public Int64? Departamento { get; set; }
        // Identificador FechaCreacion Registro
        public DateTime? FechaCreacion { get; set; }
    }
}
