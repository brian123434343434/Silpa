using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Auditoria;

/// <summary>
/// Descripción breve de GenerarLogAuditoria
/// </summary>
public class GenerarLogAuditoria
{
    public GenerarLogAuditoria()
    {

    }

    public void Insertar(string strModulo,int iCodigo,string strDetalle)
    {
        Log LogAuditoria = new Log();

        //Este bloque de codigo se debe cambiar de acuerdo al usuario logueado.
        LogAuditoria.Login_Usuario = DatosSesion.Usuario; // "usuario 34423";
        LogAuditoria.Identificacion_Usuario = "";
        LogAuditoria.Nombre_Usuario = DatosSesion.DatosUsuario.NombreUsuario; //"pepito perez ";
        LogAuditoria.Autoridad_Ambiental = "";

        LogAuditoria.Modulo = strModulo.Trim();
        LogAuditoria.Accion_Realizada = iCodigo;
        LogAuditoria.Detalle_Accion_Realizada = strDetalle.Trim();
        LogAuditoria.Almacenar();
    }






}
