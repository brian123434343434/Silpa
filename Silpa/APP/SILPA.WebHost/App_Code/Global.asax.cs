#region NameSpaces fijos

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;

#endregion

#region NameSpaces incluidos por el desarrollador



#endregion

#region Documentación Clase
/// <summary>
/// Clase que manipula los metodos globales de la aplicacion
/// </summary>
/// <remarks>
///		<strong>Control de Versiones</strong>
///		<list type="table">
///			<listheader>
///				<term>Autor</term>
///				<term>Fecha De Modificación</term>
///				<term>Version</term>
///				<term>Observaciones</term>
///			</listheader>
///			<item>
///				<term>Jorge A. Barrera</term>
///				<term>2006/05/18</term>
///				<term>1.0</term>
///				<term></term>
///			</item>
///		</list>
/// </remarks>
#endregion
public class Global : HttpApplication
{
    public Global()
    {
    }

    #region Documentacion
    /// <summary>
    /// Metodo de inicializacion de aplicacion
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    #endregion
    protected void Application_Start(object sender, EventArgs e)
    {
        //this.Server.ClearError(); 
        /*Hashtable hsh_packs = new Hashtable();

        Configuracion oConfig = new Configuracion();

        // Conexión a BD SILA
        Parametros_Conexion oParBD = new Parametros_Conexion();
        oParBD.TipoProveedor = (Tipo_Proveedor)Int32.Parse(ConfigurationManager.AppSettings["int_tipoProveedor"]);
        oParBD.Servidor = ConfigurationManager.AppSettings["str_nombreServidor"];
        oParBD.BaseDatos = ConfigurationManager.AppSettings["str_nombreBaseDeDatos"];
        oParBD.Usuario = ConfigurationManager.AppSettings["str_usuario"];
        oParBD.Password = ConfigurationManager.AppSettings["str_password"];
        oParBD.Pool = bool.Parse(ConfigurationManager.AppSettings["b_poolActivo"]);
        oParBD.PoolSize = Int32.Parse(ConfigurationManager.AppSettings["int_poolSize"]);

Application["oParBD"] = oParBD;
        oConfig.ParametrosConexionSILA = oParBD;

        oConfig.PrefijoParametrosBD = ConfigurationManager.AppSettings["str_prefijo_parametros_bd"];

        hsh_packs.Add("Seguridad", ConfigurationManager.AppSettings["str_nombre_pack_seguridad"]);
        hsh_packs.Add("Administracion", ConfigurationManager.AppSettings["str_nombre_pack_administracion"]);
        hsh_packs.Add("Parametros", ConfigurationManager.AppSettings["str_nombre_pack_parametros"]);
        hsh_packs.Add("Expedientes", ConfigurationManager.AppSettings["str_nombre_pack_expedientes"]);

        oConfig.Packs = hsh_packs;

        oConfig.URLAdminBase = ConfigurationManager.AppSettings["str_url_base"];
        oConfig.DirDocumentos = ConfigurationManager.AppSettings["str_dir_documentos"];
        oConfig.DirImagenes = ConfigurationManager.AppSettings["str_dir_imagenes"];
        oConfig.DirImagenesDinamicas = ConfigurationManager.AppSettings["str_dir_imagenes_dinamicas"];
        oConfig.DirPlantillas = ConfigurationManager.AppSettings["str_dir_plantillas"];
        oConfig.DataGridPageSize = Int32.Parse(ConfigurationManager.AppSettings["int_datagrid_pagesize"]);
        oConfig.ServidorCorreo = ConfigurationManager.AppSettings["str_servidor_correo"];
        oConfig.SenderCorreo = ConfigurationManager.AppSettings["str_sender_correo"];
        oConfig.ServidorReportes = ConfigurationManager.AppSettings["str_servidor_reportes"];
        oConfig.ReportesSILA = ConfigurationManager.AppSettings["str_reportes_sila"];
        oConfig.ReportesFinanciero = ConfigurationManager.AppSettings["str_reportes_financiero"];
        oConfig.ReportesCITES = ConfigurationManager.AppSettings["str_reportes_cites"];
        oConfig.UsuarioCorreo = ConfigurationManager.AppSettings["str_usuario_correo"];
        oConfig.ClaveCorreo = ConfigurationManager.AppSettings["str_clave_correo"];

        oConfig.DirMinambiente = ConfigurationManager.AppSettings["str_dir_minambiente"];
        oConfig.DirSILA = ConfigurationManager.AppSettings["str_dir_SILA"];
        oConfig.DirCITES = ConfigurationManager.AppSettings["str_dir_CITES"];
        oConfig.DirADHOC = ConfigurationManager.AppSettings["str_dir_ADHOC"];

        oConfig.UrlLogout = ConfigurationManager.AppSettings["str_url_logout"];*/
        
        /*Propiedades para el archivo de log de errores*/
        /*oConfig.Nombre_Archivo_Log = ConfigurationManager.AppSettings["str_nombre_archivo_log"];
        oConfig.Extension_Archivo_Log = ConfigurationManager.AppSettings["str_extension_archivo_log"];
        oConfig.Ruta_Archivo_Log = ConfigurationManager.AppSettings["str_ruta_archivo_log"];
        oConfig.File_Propiedades = ConfigurationManager.AppSettings["str_file_xml_propiedades"];

        Session["oConfig"] = oConfig;*/

        // Licencias y Permisos
        /*Session["int_tra_cites"] = Int32.Parse(ConfigurationManager.AppSettings["int_tra_cites"]);
        Session["int_tra_cites2"] = Int32.Parse(ConfigurationManager.AppSettings["int_tra_cites2"]);
        Session["int_tra_no_cites"] = Int32.Parse(ConfigurationManager.AppSettings["int_tra_no_cites"]);
        Session["int_colombia_id"] = Int32.Parse(ConfigurationManager.AppSettings["int_colombia_id"]);
        Session["int_tra_lic_ambiental"] = Int32.Parse(ConfigurationManager.AppSettings["int_tra_lic_ambiental"]);
        Session["int_anno_inicial"] = Int32.Parse(ConfigurationManager.AppSettings["int_anno_inicial"]);
        Session["str_cod_correspondencia"] = ConfigurationManager.AppSettings["str_cod_correspondencia"].ToString();
        //Cobros por autoliquidacion
        Session["int_ref_proceso"] = Int32.Parse(ConfigurationManager.AppSettings["int_ref_proceso"]);
        Session["int_ref_documento"] = Int32.Parse(ConfigurationManager.AppSettings["int_ref_documento"]);
        //Cobros de autoliquidacion por permisos
        Session["int_ref_proceso_per"] = Int32.Parse(ConfigurationManager.AppSettings["int_ref_proceso_per"]);
        Session["int_ref_documento_per"] = Int32.Parse(ConfigurationManager.AppSettings["int_ref_documento_per"]);
        //Cobros coactivos
        Session["str_ref_documento_coa"] = ConfigurationManager.AppSettings["str_ref_documento_coa"];

        Session["int_hora_inicio"] = Int32.Parse(ConfigurationManager.AppSettings["int_hora_inicio"]);
        Session["int_hora_salida"] = Int32.Parse(ConfigurationManager.AppSettings["int_hora_salida"]);*/
    }

    protected void Session_Start(Object sender, EventArgs e)
    {
      //  this.Server.ClearError(); 
    }

 
     
     private void Application_Error(object sender, EventArgs e) 
     {
         if (IsMaxRequestExceededEexception(this.Server.GetLastError())) 
        { 
         this.Server.ClearError();         
         Session["ErrorArchivo"] = "ErrorArchivo";
         this.Server.Transfer(this.Request.Url.AbsolutePath);
         return;
        } 
     }
	const int TimedOutExceptionCode = -2147467259; 
    public static bool IsMaxRequestExceededEexception(Exception e) 
    {  // unhandeled errors = caught at global.ascx level  
        // http exception = caught at page level   
        Exception main;
        try
        {
            HttpUnhandledException unhandeled = (HttpUnhandledException)e;
            if (unhandeled != null && unhandeled.ErrorCode == TimedOutExceptionCode)
            {
                main = unhandeled.InnerException;
            }
            else
            {
                main = e;
            }
            HttpException http = (HttpException)main;
            if (http != null && http.ErrorCode == TimedOutExceptionCode)
            {   // hack: no real method of identifing if the error is max request exceeded as   
                // it is treated as a timeout exception   
                if (http.StackTrace.Contains("GetEntireRawContent"))
                {
                    // MAX REQUEST HAS BEEN EXCEEDED    
                    return true;
                }
            }
        }
        catch
        {
            return false; 
        }
        return false; 
    } 
}