using SILPA.AccesoDatos.Generico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Aprovechamiento
{
    [Serializable]
    public class AprovechamientoIdentity
    {
        public int AprovechamientoID { get; set; }
        public int TipoAprovechamientoID { get; set; }
        public string TipoAprovechamiento { get;set;}
        public string Numero { get; set; }
        public string Detalle { get; set; }
        public DateTime? FechaExpedicion { get; set; }
        public DateTime? FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }
        public int ClaseRecursoId { get; set; }
        public string ClaseRecurso { get; set; }
        public int? ModoAdquisicionRecursoID { get; set; }
        public string ModoAdquisicionRecurso { get; set; }
        public int DepartamentoProcedenciaID { get; set; }
        public string DepartamentoProcedencia{ get; set; }
        public int MunicipioProcedenciaID { get; set; }
        public string MunicipioProcedencia { get; set; }
        public string CorregimientoProcedencia { get; set; }
        public string VeredaProcedencia { get; set; }
        public string Predio { get; set; }
        public int? AutoridadEmisoraID { get; set; }
        public string AutoridadEmisora { get; set; }
        public int? AutoridadOtorgaID { get; set; }
        public string AutoridadOtorga { get; set; }
        public int? FormatOtorgamientoID { get; set; }
        public string FormaOtorgamiento { get; set; }
        public int? SolicitanteID { get; set; }
        public PersonaIdentity Solicitante { get; set; }
        public int? SolicitanteOtorgaID { get; set; }
        public PersonaIdentity SolicitanteOtorga { get; set; }
        public string NumeroDocOtorga { get; set; }
        public DateTime? FechaDocOtorga { get; set; }
        public string PaisProcedencia { get; set; }
        public string EstablecimientoProcedencia { get; set; }
        public int? FinalidadID { get; set; }
        public string Finalidad { get; set; }
        public string RutaArchivo { get; set; }
        public List<EspecieAprovechamientoIdentity> LstEspecies { get; set; }
        public List<CoordenadaAprovechamientoIndentity> LstCoordenadas { get; set; }
        public double AreaTotalAutorizada { get; set; }
        public string UsuarioRegistra { get; set; }
        //jmartinez Salvoconducto Fase 2 Adiciono el Campo del nombre Comun Editable para las especies
        public DateTime? FechaFinalizacion { get; set; }
        public string CodigoIDEAMFormaOtorgamiento { get; set; }
        public string CodigoIDEAMClaseRecurso { get; set; }
        public string CodigoIDEAMModoAdquisicion { get; set; }

        public int CodigoUbicacionArbolAislado { get; set; }
        public string CodigoIDEAMUbicacionArbolAislado { get; set; }

    }
}
