using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using SILPA.LogicaNegocio.AudienciaPublica;
using SoftManagement.Log;

public partial class Informacion_Publicaciones : System.Web.UI.Page
{   
    #region "rutinas"
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {           
            procesarQueryString(); 
        }
    }
    
    /// <summary>
    /// Boton para ingresar al formulario de inscripción a audiencia pública
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cmdInscripcion_Click(object sender, EventArgs e)
    {
        
        Response.Redirect("../AudienciaPublica/InscripcionAudienciaPublica_II.aspx?Numero_AuP=" + lblNoAUP.Text.Trim()+"&&"+"Numero_SILPA="+lblNumeroSILPA.Text.Trim()); 
            
    }
    #endregion

    #region "eventos"

    /// <summary>
    /// Obtener el Número SILPA
    /// </summary>
    protected void procesarQueryString()
    {
        string strNumeroSILPA = "";
        int? intAutoridadAmbiental = null;

        if (Request.QueryString["Numero_Silpa"] != null)
        {
            strNumeroSILPA = Request.QueryString["Numero_Silpa"];

            consultarAudiencia(strNumeroSILPA,intAutoridadAmbiental);
        }
        else { strNumeroSILPA = ""; }

    }

    /// <summary>
    /// Función para consultar el listado de audiencias a celebrar
    /// </summary>
    /// <param name="strNumSILPA"></param>
    /// <param name="strAutoridadAmbiental"></param>
    /// <param name="strNombreProyecto"></param>
    /// <param name="intNumeroExpediente"></param>
    /// <param name="dateFechaReunion"></param>
    /// <param name="dateFechaAudiencia"></param>
    protected void consultarAudiencia(string strNumSILPA,Nullable<int> intAutoridadAmbiental )
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".consultarAudiencia.Inicio");
            AudienciaPublica objAudienciaPublicaNegocio = new AudienciaPublica();
            DataTable dtAudiencias = new DataTable();

            //Ejecutar consulta de listado de adiencias a celebrar
            dtAudiencias = objAudienciaPublicaNegocio.ListarAudienciasPublicas(
                                                        strNumSILPA, intAutoridadAmbiental, " ", " ", null, null);

            for (int i = 0; i < dtAudiencias.Rows.Count; i++)
            {

                lblNumeroSILPA.Text = dtAudiencias.Rows[i]["NUMERO_SILPA"].ToString();
                lblProyecto.Text = dtAudiencias.Rows[i]["NOMBRE_PROYECTO"].ToString();
                lblNumExpediente.Text = dtAudiencias.Rows[i]["NUMERO_EXPEDIENTE"].ToString();
                lblFechaReunion.Text = dtAudiencias.Rows[i]["FECHA_HORA_REUNION_INFORMATIVA"].ToString();
                lblFechaAudiencia.Text = dtAudiencias.Rows[i]["FECHA_HORA_CELEBRACION_AUDIENCIA"].ToString();
                lblComunidad.Text = dtAudiencias.Rows[i]["ENTIDADES_COMUNIDADES"].ToString();
                lblLugarAudiencia.Text = dtAudiencias.Rows[i]["LUGAR_CELEBRACION_AUDIENCIA_PUBLICA"].ToString();
                lblLugarPonentes.Text = dtAudiencias.Rows[i]["LUGAR_INSCRIPCION_PONENTES"].ToString();
                lblLugarEstudios.Text = dtAudiencias.Rows[i]["LUGAR_ESTUDIOS_AMBIENTALES"].ToString();
                lblLugarReunion.Text = dtAudiencias.Rows[i]["LUGAR_REUNION_INFORMATIVA"].ToString();
                lblconvocatoria.Text = dtAudiencias.Rows[i]["CONVOCATORIA"].ToString();

                if (dtAudiencias.Rows[i]["RUTA_EDICTO"].ToString() != null && dtAudiencias.Rows[i]["RUTA_EDICTO"].ToString() != "null")
                {
                    lblEdicto.Text = dtAudiencias.Rows[i]["RUTA_EDICTO"].ToString();
                    lnkDescargar.Visible = true;
                }
                else
                {
                    lblEdicto.Text = "";
                    lnkDescargar.Visible = false;
                }

                if (lblEdicto.Text != "")
                    lnkDescargar.Visible = true;
                else
                    lnkDescargar.Visible = false;

                lblNoAUP.Text = dtAudiencias.Rows[i]["ID_AUDIENCIA_PUBLICA"].ToString();

            }
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".consultarAudiencia.Finalizo");
        }
    }

    /// <summary>
    /// Función para abrir o descargar archivos 
    /// </summary>
    public void descargarArchivos( string strRutaAdjuntos )
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".descargarArchivos.Inicio");
            //Abrir o Guardar el documento en la ruta especificada
            System.IO.FileInfo targetFile = new System.IO.FileInfo(strRutaAdjuntos);

            if (targetFile != null || targetFile.ToString() != null)
            {
                this.Response.Clear();
                this.Response.AddHeader("Content-Disposition", "attachment; filename=" + targetFile.Name);
                this.Response.AddHeader("Content-Length", targetFile.Length.ToString());
                this.Response.ContentType = "application/octet-stream";
                this.Response.ContentType = "application/base64";
                this.Response.WriteFile(targetFile.FullName);
                this.Response.WriteFile(strRutaAdjuntos);
            }
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);        
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".descargarArchivos.Finalizo");
        }
    }

    #endregion 
       
    protected void lnkDescargar_Click(object sender, EventArgs e)
    {
        if (lblEdicto.Text != "")
        {
            //descargarArchivos(lblEdicto.Text.Trim());
            Response.Redirect("../AudienciaPublica/ListarEdictoInscripcionAudienciaPublica.aspx?nsilpa="+lblNumeroSILPA.Text.Trim()+"&"+"ubi="+lblEdicto.Text.Trim());

            

        }
    }
    protected void lnkDescargar_Command(object sender, CommandEventArgs e)
    {

    }
    protected void btnRegresar1_Click(object sender, EventArgs e)
    {
        Page.Response.Redirect("MenuAudienciaPublica.aspx");
    }
}
