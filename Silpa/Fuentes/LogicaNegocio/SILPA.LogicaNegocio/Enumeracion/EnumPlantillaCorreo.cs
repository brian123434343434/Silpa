using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.LogicaNegocio.Enumeracion
{
    public enum EnumPlantillaCorreo
    {
        //Para aprobacion de usuarios
        AprobacionUsuarioAdmin = 1,
        AprobacionUsuario = 2,
        RechazoUsuario = 3,
        ComunicacionVisita = 4,
        ComunicacionCobroVencimiento = 5,
        AcuseDeEnvio = 6,
        FalloEnEnvio = 7,
        ComunicacionCobroVencimientoAA = 8,
        ComunicacionRecordatorio = 9,
        ComunicacionSolicitud = 10,
        ComunicacionSolicitudE = 11,
        Salvoconducto = 12,
        CorreoRuia = 13,
        CorreoSancionatorio = 14,
        CorreoAudiencia = 15,
        EnviarRadicacion = 16,
        EnviarOficio = 17,
        EnviarOficioAA = 18,
        CorrespondenciaAA = 19,
        CorrespondenciaCiudadano = 20,
        ComunicacionEE = 21,
        RadicacionAA = 22,
        SolicitudCredenciales = 23,
        RespuestaComunicacionSolicitudE = 24,
        HabilitarDeshabilitarUsuarios = 25,
        UsuarioInactivo = 26,
        ReestablecerContraseña = 27,
        FinalizarAudiencia = 35,
        RespuestaEE = 37,
        CambioEstadoNotificacionPersona = 41,
        FallaComunicacionPDI = 43,
        RecursoInterposicion = 47,
        CorreoComunicador = 48,
        SolNotificacionElectronica=49,
        IniciaTareaProcesoPINES=52,
        TareaProximaAVencercePINES=53,
        ComunicacionReunion = 54,
        
        AlertaContigencia = 62,
        NuevaSolicitudSalvoconductoAA = 67,
        NuevaSolicitudSalvoconductoSolicitante = 68,
        EmisionSalvoconducto = 69,
        RechazoSalvoconducto = 70,
        BloqueoSalvoconducto = 71,
        NotificacionEntidades = 72,
 		VencimientoSerieNumeracionSUNL = 75, //jmartinez
		RegistroAutoliquidacionVITAL = 76,
        ReasignacionRegistro = 88,
        ReasignacionRespuesta = 89,
        RegistroUsuarioConfirmacionCorreo = 90
    }
}
