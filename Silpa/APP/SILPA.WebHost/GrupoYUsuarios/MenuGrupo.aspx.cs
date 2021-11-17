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
using System.Collections.Generic;
using SILPA.AccesoDatos.GrupoYUsuarios;
using SILPA.LogicaNegocio.GrupoYUsuarios;
using SILPA.LogicaNegocio.Generico;
using SILPA.Comun;
using SILPA.AccesoDatos.Generico;
using SoftManagement.Log;

public partial class GrupoYUsuarios_MenuGrupo :System.Web.UI.Page {
    
    protected void Page_Load( object sender, EventArgs e ) {
        if( !this.IsPostBack )
            llenarGrilla( );
    }

    private void llenarGrilla(){
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".llenarGrilla.Inicio");
            GrupoYMenu menu = new GrupoYMenu();
            DataSet dsMenu = menu.ConsultarGrupoMenuOpcion("-1", "-1", txtNombreMenu.Text);

            grdMenus.DataSource = dsMenu;
            grdMenus.DataBind();
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".llenarGrilla.Finalizo");
        }
    }

    protected void grdMenus_rowcommand( Object sender, GridViewCommandEventArgs e ) {

        //Label lblMenuId = ( Label )( (GridView)sender ).Rows[ int.Parse(e.CommandArgument.ToString( ) ) ].Cells[ 0 ].Controls[ 0 ];
        Label lblMenuId = (Label)((GridView)sender).Rows[int.Parse(e.CommandArgument.ToString())].FindControl("lblMenuId");

        if ( e.CommandName == "edit" ){
            Response.Redirect( "EdicionMenu.aspx?menu=" + lblMenuId.Text );
        }
        else if( e.CommandName == "desactivate" ) {
            GrupoYMenu menu = new GrupoYMenu( );
            menu.ActivarGrupoMenuOpcion( int.Parse( lblMenuId.Text ), false );
            DataSet dsMenu = menu.ConsultarGrupoMenuOpcion( "-1", "-1", "", lblMenuId.Text );

            if( dsMenu != null && dsMenu.Tables.Count > 0 && dsMenu.Tables[ 0 ].Rows.Count > 0 ){
                string fileName = ConfigurationManager.AppSettings[ "RUTA_XML" ] + dsMenu.Tables[ 0 ].Rows[ 0 ][ "GMO_NOMBRE_XML" ];

                if( System.IO.File.Exists( fileName ) )
                    System.IO.File.Move( fileName, fileName + ".desactivated" );
            }
        }
        else if( e.CommandName == "activate" ) {

            GrupoYMenu menu = new GrupoYMenu( );
            menu.ActivarGrupoMenuOpcion( int.Parse( lblMenuId.Text ), true );
            DataSet dsMenu = menu.ConsultarGrupoMenuOpcion( "-1", "-1", "", lblMenuId.Text );

            if( dsMenu != null && dsMenu.Tables.Count > 0 && dsMenu.Tables[ 0 ].Rows.Count > 0 ) {
                string fileName = ConfigurationManager.AppSettings[ "RUTA_XML" ] + dsMenu.Tables[ 0 ].Rows[ 0 ][ "GMO_NOMBRE_XML" ];

                if( System.IO.File.Exists( fileName + ".desactivated" ) )
                    System.IO.File.Move( fileName + ".desactivated", fileName.Replace( ".desactivated", "" ) );
            }
        }

        llenarGrilla( );

    }

    protected void btnBuscar_Click( object sender, EventArgs e ) {
        llenarGrilla( );
    }

    protected void btnNuevo_Click( object sender, EventArgs e ) {
        Response.Redirect( "EdicionMenu.aspx?menu=0" );
    }

    protected void grdMenus_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.grdMenus.PageIndex = e.NewPageIndex;
        llenarGrilla();
    }
}
