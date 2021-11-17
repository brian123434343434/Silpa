using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.GenerarSalvoConducto
{
    public class SalActoAdministrativo
    {

        public SalActoAdministrativo()
        {
           
        }

        // Identificador del objeto
        public Int32? IDSalActoAdministrativo { get; set; }
        // Identificador del NumeroActo
        public String NumeroActo { get; set; }
        // Identificador del NumeroVital
        public String NumeroVital { get; set; }
        // Identificador del AutoridadAmbiental
        public Int64? TipoIdentificacionSolicitante { get; set; }
        // Identificador del AutoridadAmbiental
        public String NumeroIdentificacionSolicitante { get; set; }
        // Identificador del AutoridadAmbiental
        public Int64? AutoridadAmbiental { get; set; }
        // Identificador del FormaOtorgamiento
        public FormaOtorgamiento FormaOtorgamiento { get; set; }
        // Identificador del Fecha
        public DateTime? Fecha { get; set; }
        // Identificador del FechaInicioVigencia
        public String FechaInicioVigencia { get; set; }
        // Identificador del FechaFinalVigencia
        public String FechaFinalVigencia { get; set; }
        // Identificador del Fecha Creacion Registro
        public String FechaCreacion { get; set; }
        // Identificador del Tarea
        public bool? EsTramitadoSila { get; set; }
        // Identificador del Tarea
        public int? ResolucionSancionatoria { get; set; }
        // Identificador del Tarea
        public int? Tarea { get; set; }
        // Numero de Expediente
        public String Expediente { get; set; }
        // Lista ListaIdentificacionRecurso
        public List<IdentificacionRecurso> ListaIdentificacionRecurso { get; set; }
        // Lista ListaLocalizacionPredial
        public List<LocalizacionPredial> ListaLocalizacionPredial { get; set; }
        // Nombre Archivo NombreArchivo
        public String NombreArchivo { get; set; }
        // Identificador del NumeroActoRelacionado
        public String NumeroActoRelacionado { get; set; }
        // Identificador del NumeroSalvconducto
        public String NumeroSalvconducto { get; set; }

        // Byte de Archivo Salvoconducto
        [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")]
        public byte[] DatosArchivo { get; set; }
    }
}
