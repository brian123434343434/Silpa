using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Descripción breve de qrystring
/// </summary>
[Serializable]
public class qrystring
{
    public string qrys;
    public string idPersona = "-1";
    public string idEstado = "-1";
    public string numeroSilpa = string.Empty;
    public string expediente = string.Empty;
    public string idActoNot = "-1";
    public string fechaEstadoNotificado = DateTime.Now.ToString();

    public string FechaActo = DateTime.Now.ToString();
    public string TipoActo = string.Empty;
    public string NumeroActo = string.Empty;
    public string Usuario = string.Empty;
    public string Identificacion = string.Empty;
    public string DiasVence = string.Empty;
    public string EsPDI = string.Empty;
    public string IDProcesoNot = string.Empty;
    public string EstadoNotificado = string.Empty;
    public string Ubicacion = string.Empty;
    public string Autoridad = string.Empty;
    public string IdAutoridad = string.Empty;
}
