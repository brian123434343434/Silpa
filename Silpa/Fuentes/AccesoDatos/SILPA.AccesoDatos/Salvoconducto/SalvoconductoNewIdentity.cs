using SILPA.AccesoDatos.Generico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SILPA.AccesoDatos.Salvoconducto
{
    public class NumeracionSalvoconducto
    {
        public int CONSECUTIVO { get; set; }
        public String MENSAJE { get; set; }
    }
    [Serializable]
    public class SalvoconductoNewIdentity
    {
        public int SalvoconductoID { get; set; }
        public int? TipoSalvoconductoID { get; set; }
        public string TipoSalvoconducto { get; set; }
        public int ClaseRecursoID { get; set; }
        public string ClaseRecurso { get; set; }
        public int Vigencia { get; set; }
        public string Numero { get; set; }
        public string CodigoSeguridad { get; set; }
        public string NumeroVitalTramite { get; set; }
        public int? AprovechamientoID { get; set; }
        public int DepartamentoProcedenciaID { get; set; }
        public string DepartamentoProcedencia { get; set; }
        public int MunicipioProcedenciaID { get; set; }
        public string MunicipioProcedencia { get; set; }
        public string CorregimientoProcedencia { get; set; }
        public string VeredaProcedencia { get; set; }
        public string EstablecimientoProcedencia { get; set; }
        public Aprovechamiento.AprovechamientoIdentity Aprovechamiento { get; set; }
        public int EstadoID { get; set; }
        public string Estado { get; set; }
        public DateTime FechaExpedicion { get; set; }
        public string Archivo { get; set; }
        public DateTime? FechaInicioVigencia { get; set; }
        public DateTime? FechaFinalVigencia { get; set; }
        public List<EspecimenNewIdentity> LstEspecimen { get; set; }
        public string Observacion { get; set; }
        public List<RutaEntity> LstRuta { get; set; }
        public TransporteNewIdentity Transporte { get; set; }
        public int? FormatOtorgamientoID { get; set; }
        public string FormatOtorgamiento { get; set; }
        public int? ModoAdquisicionRecursoID { get; set; }
        public string ModoAdquisicionRecurso { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public int? FinalidadID { get; set; }
        public string Finalidad { get; set; }
        public string UsuarioCargue { get; set; }
        public int AutoridadEmisoraID { get; set; }
        public string AutoridadEmisora { get; set; }
        public int? AutoridadCargueID { get; set; }
        public int? AutoridadOtorgaID { get; set; }
        public string NumeroDocOtorga { get; set; }
        public DateTime? FechaDocOtorga { get; set; }
        public int? SolicitanteOtorgaID { get; set; }
        public PersonaIdentity SolicitanteTitularPersonaIdentity { get; set; }
        public int SolicitanteID { get; set; }
        public string Solicitante { get; set; }
        public string NumeroSUNAnterior { get; set; }
        public int consecutivo { get; set; }
        public string Detalle { get; set; }
        public List<SalvoconductoAnterior> LstSalvoconductoAnterior { get; set; }
        public int DepartamentoOrigenID { get; set; }
        public int MunicipioOrigenID { get; set; }
        public string MotivoRechazo { get; set; }
        public int IdTipoBloqueo { get; set; }
        public string MotivoBloqueo { get; set; }
        public List<AprovechamientoOrigen> LstAprovechamientoOrigen { get; set; }
        public string TitularSalvoconducto { get; set; }
        public string IdentificacionTitularSalvocoducto { get; set; }
        public DataTable SalvoconductoAsociados { get; set; }
        public string NombreImagenAutoridad { get; set; }
        public List<TransporteNewIdentity> LstTransporte { get; set; }
        public int? AutoridadCambiaEstado { get; set; }

        //jmartinez salvoconducto Fase 2
        public string CodigoIdeamTipoSalvoconducto { get; set; }

        public string CodigoIdeamFinalidadRecurso { get; set; }
        //bosques.tic SUNL2020 campos aprovechamiento sunl preimpresos
        public string TitularAprovechamientoSUNLPreimpreso { get; set; }
        public string IdentificacionTitularAprovechamientoSUNLPreimpreso { get; set; }
        public string ActoAdministrativoAprovechamientoSUNLPreimpreso { get; set; }
        public DateTime FechaActoAdministrativoAprovechamientoSUNLPreimpreso { get; set; }
        public int AutoridadIdCargue { get; set; }
    }
    [Serializable]
    public class SalvoconductoAnterior
    {
        public int SalvoconductoID { get; set; }
        public string Detalle { get; set; }
        //jmartinez salvoconducto fase 2 
        public string Numero { get; set; }
    }
    [Serializable]
    public class AprovechamientoOrigen
    {
        public int AprovechamientoID { get; set; }
        public string Detalle { get; set; }
        public string numeroAprovechamiento { get; set; }
    }
    public enum EstadoSalvoconducto
    {
        Solicitud = 1,
        Emitido = 2,
        Negado = 3, 
        Cargado = 4
    }
}
