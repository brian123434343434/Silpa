using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.Comun
{
    /// <summary>
    /// Enumeración ùtil para determinar el tipo de radicación
    /// </summary>
   public enum TipoRadicacion
    {
        Sincronica = 0,
        Asincronica = 1

    }

    /// <summary>
    /// Enumeración útil para determinar el estado de la solicitud de credenciales
    /// </summary>
    public enum EstadoSolicitudCredencial
    {
        NoExiste = -1, // No existe la solicitud.
        Aprobado  = 0, 
        EnProceso = 1,
        Rechazado = 2
    }



    /// <summary>
    /// Enumeración ùtil para determinar el tipo de solicitante ()
    /// </summary>
    public enum TipoSolicitante
    {
       Solicitante = 1,
       RepresentanteLegal = 2,
       Apoderado = 3,
       TerceroInterviniente = 4,
       Audiencia = 5
    }

    /// <summary>
    /// Enumeración ùtil para determinar el tipo de persona ()
    /// </summary>
    public enum TipoPersona
    {
        Natural = 1,
        JuridicaPublica = 2,
        JuridicaPrivada = 3,
        Funcionario = 4,
        Entidad = 5,
        RepresentanteLegal = 8,
        Apoderado = 9
    }

    /// <summary>
    /// Enumeración ùtil para determinar el tipo de oficio ()
    /// </summary>
    public enum TipoOficio
    {
        Tipo_Uno = 0,
        Tipo_Dos = 1
    }

    /// <summary>
    /// Tipo de acto administrativo
    /// </summary>
    public enum TipoActo
    {
        Concepto = 0,
        Auto = 1,
        Resolucion = 2,
        Oficio = 3
    }

    /// <summary>
    /// Tipo de Documento.
    /// </summary>
    public enum TipoDocumento
    {
        Acto = 0,
        Oficio = 1,
        Concepto = 2,
        Resolucion = 3,
        Auto = 0
    }

    /// <summary>
    /// Tipo de Documento para liquidacion de evaluación
    /// </summary>
    public enum TipoDocumentoLiquidacionEvaluacion
    {
        OficioCobroEvaluacion = 7,
        OficioSolicitudRequerimientosLiquidaciónEvaluacion = 48
    }

    /// <summary>
    /// Tipo de identificacion
    /// </summary>
    public enum TipoIdentificacion
    {
        Cedula = 1,  // CC
        Nit = 2,
        CedulaExtranjeria=3//CE
    }

    /// <summary>
    /// 
    /// </summary>
    public enum Orientacion
    {
        DERECHA = 0,
        IZQUIERDA = 1
    }

    /// <summary>
    /// Listado de los estados del proceso Solicitud DAA
    /// </summary>
    public enum EstadoProcesoDAA
    {
        ///  Estados para el proceso de DAA
        Enviar_TDR_DAA =1, 
        Radicar_Solicitud_Manualmente = 3,
        Radicar_Solicitud_Auto = 9,
        Conflicto_Requiere_DAA = 10,
        Conflicto_EIA = 11,
        Enviar_EIA=12,
        Pendiente_Radicacion=17,
        Radicado=18

    }

    public enum AutoridadesAmbientales
    {
        //Autoridades - IDs para insertar en procesos
        MAVDT=62,
        ANLA=144
    }

    public enum IDFormularios
    {
        //Autoridades - IDs para insertar en procesos
        Radicacion = 2,
        SolicitudDAA = 1
    }

    public enum ProcesosHomologados
    {
        //Procesos Homologados
        SolicitudDAA = 1
    }

    /// <summary>
    /// Tipos de dirección
    /// </summary>
    public enum TipoDireccion
    {
        Domicilio = 1, Correspondencia = 2, Expedicion_Documento = 3
    }

    /// <summary>
    /// Tipo de sancion
    /// </summary>
    public class TipoSancion
    {
        public const int Principal = 1;
        public const int Accesoria = 2;
    }

    public static class DatoComponenteNotificacion
    {
        public const string crear="crear";
        public const string consultar="consultar";
        public const string ejecutoriar="ejecutoriar";
        public const string respuesta="respuesta.txt";
        public const string ruta="ruta_transacciones";
        public const string componente = "componente_notificacion";

    }


    /// <summary>
    /// Tipos de Notificación
    /// </summary>
    public enum TipoNotificacion
    {
        NOTIFICACION = 1,
        COMUNICACION = 2,
        CUMPLASE = 3
    }

    /// <summary>
    /// Flujos de Notificación
    /// </summary>
    public enum FlujoNotificacion
    {
        NORMAL = 1,
        SIN_RECURSO = 2,
        COMUNICAR = 3,
        CUMPLASE = 4
    }


    /// <summary>
    /// Estados de notificación 
    /// </summary>
    public enum EstadoNotificacion 
    { 
        PENDIENTE_DE_ACUSE_DE_NOTIFICACION = 1,
        NOTIFICADA = 2,
        EN_EDICTO = 3,
        CON_RECURSO_INTERPUESTO = 4,
        EJECUTORIADA = 5,
        CON_RENUNCIA_A_TERMINOS = 6,
        SUSPENDIDO = 7,
        REVOCADO = 8,
        NO_EXISTE = 9,
        CON_ERROR = 10,
        FINALIZADA = 18
    }

    /// <summary>
    /// Caso Proceso
    /// </summary>
    public enum CasoProceso
    {
        DAA = 59,
        INCO = 61,
        AUD1 = 62,
        LIQLA = 66,	
        TER = 67,	
        LIC = 68,  
        SAN = 69,
        PER = 70,
        SUN = 71,	
        PRU = 72,	
        LIQPA = 73,
        CES = 74,	
        SUN3 = 76,	
        SUN2 = 77,	
        AUDCI = 80,	
        PRU2 = 83,	
        DIAN = 84	
    }

    /// <summary>
    /// Enumeraciones útiles para pago electronico.
    /// </summary>
    public enum PagoElectronico { btnPago = 1, btnImpresion = 2 }


    /// <summary>
    /// 
    /// </summary>
    public enum DocumentosComunicacionEE 
    { 
        OficioSolicitudInformacionEELicencia = 64,
        OficioSolicitudInformacionEEPermiso = 67,
        OficioRespuestaSolicitudInformación = 93
    }


    /// <summary>
    /// Contiene los identificadores de la tabla GEN_PARAMETRO, para ubicar su valor
    /// mediante el sp: BAS_OBTENER_VALOR_PARAMETRO
    /// </summary>
    public enum GenParametro 
    {
        AutoridadAmbientalMAVDT = 95
    }

    /// <summary>
    /// Idiomas soportados por el aplicativo
    /// </summary>
    public enum Idiomas
    {
        Espanol = 1,
        Ingles = 2
    }

    public enum NOTPlantilla
    {
        FormularioNotificacionNoFirmaANLA,
        FormularioNotificacionANLA,
        FormularioNotificacionNoFirma,
        FormularioNotificacion,
        FormularioNotificacionANLAFirmaIzquierda
    }

    public enum NOTPlantillaMarcadoresSigpro
    {
        Notificacion = 1,
        Pie = 2
    }

    public enum NOTEstadosActo
    {
        Sin_Verificar = 1,
        Verificado_NO_Liberado = 2,
        Verificado_Liberado_Parcialmente = 3,
        Verificado_Liberado = 4,
        Bloqueada_Solicitud_Anulacion = 5,
        Bloqueada_Anulacion = 6
    }

    public enum NOTEstadosActoPersona
    {
        Sin_Verificar = 1,
        Verificado = 2
    }

    public enum CobroEstados
    {
        OK = 1,
        PENDING = 2,
        NOT_AUTHORIZED = 3,
        FAILED = 4,
        IMPRESION = 5,
        AVANZADO = 6
    }

	public enum NOTTipoAnexo
    {
        Adjunto = 1,
        Enlace = 2
    }

    public enum BUCTipoCampo
    {
        Nombre_Completo_Persona = 1,
        Tipo_de_Documento_Persona = 2,
        Numero_Documeto = 3,
        Tarjeta_Profesional = 4,
        Direccion = 5,
        Telefono = 6,
        Celular = 7,
        Correo_Electronico = 8,
        Autoridad = 9,
        Código_Expediente = 10,
        Area_Persona = 11,
        Cargo_Persona = 12
    }

    /// <summary>
    /// Origen de las transacciones hacia SIGPRO
    /// </summary>
    public enum OrigenSIGPROTransacciones
    {
            NOTIFICACION = 1
    }

    /// <summary>
    /// Enum para definir los distintos tipos de origenes existentes para la consulta publica de tramites
    /// </summary>
    public enum OrigenConsultaPublica
    {
        VITAL = 1,
        SILA = 2,
        SILAMC = 3,
		PUBLICACION = 4,
        ANLA = 5,
        EIA = 6
    }

	/// <summary>
    /// Tipo de solicitud de autoliquidación realizado
    /// </summary>
    public enum AutoliquidacionTipoSolicitud
    {
        LICENCIA_AMBIENTAL = 1,
        PERMISO = 2,
        OTROS_INSTRUMENTOS = 3
    }

    /// <summary>
    /// Solicitud de autoliquidacion realizada
    /// </summary>
    public enum AutoliquidacionSolicitud
    {
        Evaluacion = 1,
        Modificacion = 2,
        Cesion = 3,
        Integracion = 4,
        Seguimiento = 7,
        DAA = 8
    }

    /// <summary>
    /// Solicitud de autoliquidacion realizada
    /// </summary>
    public enum AutoliquidacionResoluciones
    {
        Ley_633 = 1,
        Resolucion_0324 = 2
    }


    /// <summary>
    /// Metodo de conecion REST
    /// </summary>
    public enum MetodosConexionREST
    {
        GET = 1,
        POST = 2
    }

    /// <summary>
    /// Codigos de respuesta REST
    /// </summary>
    public enum CodigosRespuestaREST
    {
        OK = 200,
        ERROR = 400,
        NO_AUTORIZADO = 401,
        NO_EXISTE = 404
    }

    /// <summary>
    /// Estados de las solicitudes de reasignación
    /// </summary>
    public enum EstadoSolicitudReasignacion
    {
        Solicitado = 1,
        Aprobado = 2,
        Rechazado = 3,
        Reasignado = 4
    }

    /// <summary>
    /// Paises
    /// </summary>
    public enum PaisEnum
    {
        COLOMBIA = 49
    }
}