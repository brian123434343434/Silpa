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
using SILPA.LogicaNegocio.GrupoYUsuarios;
using System.Xml;
using SILPA.AccesoDatos.GrupoYUsuarios;
using SoftManagement.Log;

public partial class GrupoYUsuarios_EdicionMenu :System.Web.UI.Page {

    public string strTableMenu = "";
    private DataSet dsMenu = new DataSet( );
    private string strColumnaHijo = "";
    private string strColumnaPadre = "";
    private string strTextField = "TextField";
    private string strValueField = "ValueField";
    private string strTargetField = "TargetField";
    private string strUrlField = "NavigateUrlField";
    private int intMCurrentId = 0;
    public string strGrupos = "";
    string selectedMenu = "";

    protected void Page_Load( object sender, EventArgs e ) {

        if( !this.IsPostBack ){
            loadCombos( );
            loadMenu( );

            btnGuardarMenu.Attributes.Add( "onclick", "return validateSave();" );
            btnSaveMenu1.Attributes.Add( "onclick", "return validateSave();" );
        }

    }

    private bool existsMenu( string grupos, string findValue ){
        grupos = "," + grupos + ",";

        if( grupos.IndexOf("," + findValue + ",") > -1 )
            return true;

        return false;
    }

    private void loadCombos( ) {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".loadCombos.Inicio");
            GrupoYMenu menu = new GrupoYMenu();
            selectedMenu = Request.Params["menu"];
            int intSelectedMenu = 0;

            if (!(selectedMenu != "" && int.TryParse(selectedMenu, out intSelectedMenu)))
            {
                if (txtMenuId.Text != "" && selectedMenu != "0")
                {
                    selectedMenu = txtMenuId.Text;
                }
                else
                {
                    txtSelectedMenu.Text = "0";
                    txtNombreMenu.Text = "Menu nuevo";
                    txtMenuId.Text = "0";
                    selectedMenu = "0";
                }
            }
            else if (selectedMenu == "0")
            {
                txtSelectedMenu.Text = "0";
                txtNombreMenu.Text = "Menu nuevo";
                txtMenuId.Text = "0";
                selectedMenu = "0";
            }

            dsMenu = menu.ConsultarGrupoMenuOpcion("-1", "-1", "", selectedMenu);
            DataTable dtGrupos = Utilidades.arrayToDataTable(GrupoYMenu.ConsultarGrupo().ToArray());
            strGrupos = "<table id='tblGroups'><tr>";
            int colsCount = 4;

            string grupos = "";

            if (dsMenu.Tables.Count > 0 && dsMenu.Tables[0].Rows.Count > 0 && selectedMenu != "0")
                grupos = dsMenu.Tables[0].Rows[0]["GMO_GRUPO"].ToString();

            for (int i = 0; i < dtGrupos.Rows.Count; i++)
            {

                strGrupos += "<td>";
                strGrupos += "<input type='checkbox' onclick='updateGroups()' value='" + dtGrupos.Rows[i]["ID"].ToString() + "'";
                strGrupos += (existsMenu(grupos, dtGrupos.Rows[i]["ID"].ToString()) ? " checked " : "");
                strGrupos += " >";
                strGrupos += "<span>" + dtGrupos.Rows[i]["Name"].ToString() + "</span>";
                strGrupos += "</td>";

                if (((i + 1) % colsCount) == 0)
                    strGrupos += "</tr><tr>";

            }
            if (dsMenu.Tables.Count > 0 && dsMenu.Tables[0].Rows.Count > 0 && selectedMenu != "0")
            {
                txtNombreMenu.Text = dsMenu.Tables[0].Rows[0]["GMO_NOMBRE_MENU"].ToString();
                txtSelectedMenu.Text = dsMenu.Tables[0].Rows[0]["GMO_NOMBRE_XML"].ToString();
                txtMenuId.Text = dsMenu.Tables[0].Rows[0]["GMO_ID"].ToString();
                txtGrupos.Text = dsMenu.Tables[0].Rows[0]["GMO_GRUPO"].ToString();
            }
            else
            {
                txtNombreMenu.Text = "Menu nuevo";
                txtSelectedMenu.Text = "";
                txtMenuId.Text = "";
                txtGrupos.Text = "";
            }

            strGrupos += "</tr></table>";
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".loadCombos.Finalizo");
        }
    }

    private void loadMenu( ) {

        strTableMenu = "";
        string strFileMenu = ConfigurationManager.AppSettings[ "RUTA_XML" ] + txtSelectedMenu.Text;

        if( txtSelectedMenu.Text == "" )
            return;
            //strFileMenu = ConfigurationManager.AppSettings[ "RUTA_XML" ] + "mMenu.xml";

        if( !System.IO.File.Exists( strFileMenu ) ) {
            string xmlObject = "<?xml version=\"1.0\" encoding=\"iso-8859-1\" ?><menu></menu>";
            XmlDocument xmlDoc = new XmlDocument( );
            xmlDoc.LoadXml( xmlObject );
            xmlDoc.Save( strFileMenu );
        }

        dsMenu = new DataSet( );
        dsMenu.ReadXml( strFileMenu );

        if( dsMenu.Relations.Count == 0 ){
            string fileMenuSTD = ConfigurationManager.AppSettings[ "RUTA_XML" ] + "mMenu.xml";
            dsMenu = new DataSet( );
            dsMenu.ReadXml( fileMenuSTD );

            dsMenu.EnforceConstraints = false;
            dsMenu.Tables[ 0 ].Rows.Clear( );
            dsMenu.EnforceConstraints = true;

            DataRow dr = null;
            DataSet dsOriginal = new DataSet( );
            dsOriginal.ReadXml( strFileMenu );

            if( dsOriginal.Tables.Count > 0 && dsOriginal.Tables[ 0 ].Rows.Count > 0 ){
                foreach( DataRow drOriginal in dsOriginal.Tables[ 0 ].Rows ) {
                    dr = dsMenu.Tables[ 0 ].NewRow( );
                    foreach( DataColumn dcOriginal in dsOriginal.Tables[ 0 ].Columns )
                        dr[ dcOriginal.ColumnName ] = drOriginal[ dcOriginal.ColumnName ];

                    dsMenu.Tables[ 0 ].Rows.Add( dr );
                }
            }

        }

        /*Se asume q solo hay una restriccion (el id del menu)*/
        strColumnaHijo = dsMenu.Relations[ 0 ].ChildColumns[ 0 ].ColumnName;
        strColumnaPadre = dsMenu.Relations[ 0 ].ParentColumns[ 0 ].ColumnName;

        for( int i = 0; i < dsMenu.Tables[ 0 ].Rows.Count; i++ ){

            if( dsMenu.Tables[ 0 ].Rows[ i ][ strColumnaHijo ].ToString( ).Equals( "" ) ) {

                strTableMenu += "<tr><td><div><table class='itemMenu'><tr><td class='imgItemMenu'>" +
                    "<img src='../images/Knob_Add.jpg' onclick='addMenu(this)' /><br>" +
                    "<img src='../images/Knob_Cancel.jpg' onclick='deleteMenu(this)'  />" +
                    "</td><td>";

                strTableMenu += "<fieldset>" +
                                " <legend>Menú</legend>" +
                                " <table>" +
                                "    <tr>" +
                                "        <td>Texto:</td>" +
                                "        <td><input type='text' value='" + dsMenu.Tables[ 0 ].Rows[ i ][ strTextField ] + "'/></td>" +
                                "        <td>Valor:</td>" +
                                "        <td><input type='text' value='" + dsMenu.Tables[ 0 ].Rows[ i ][ strValueField ] + "'/></td>" +
                                "    </tr>" +
                                "    <tr>" +
                                "        <td>Url:</td>" +
                                "        <td><input type='text' value='" + dsMenu.Tables[ 0 ].Rows[ i ][ strUrlField ] + "'/></td>" +
                                "        <td>Target:</td>" +
                                "        <td><input type='text' value='" + dsMenu.Tables[ 0 ].Rows[ i ][ strTargetField ] + "'/></td>" +
                                "    </tr>" +
                                " </table>" +
                                "</fieldset>";

                strTableMenu += "<br><div>";
                strTableMenu += loadMenuChilds( dsMenu.Tables[ 0 ].Rows[ i ][ strColumnaPadre ].ToString( ) );
                strTableMenu += "</div></td></tr></table></div></td></tr>";

            }

        }
    }

    private string loadMenuChilds( string padreId ){

        string strLCurrentMenu = "";
        
        if( padreId.Equals( "" ) )
            return "";

        for( int i = 0; i < dsMenu.Tables[ 0 ].Rows.Count; i++ ) {
            if( dsMenu.Tables[ 0 ].Rows[ i ][ strColumnaHijo ].ToString( ) == padreId ) {

                strLCurrentMenu += "<div>" + ( strLCurrentMenu.Equals( "" ) ? "" : "<br>" );
                strLCurrentMenu += "<table class='itemMenu'><tr><td class='imgItemMenu'>" +
                    "<img src='../images/Knob_Add.jpg' onclick='addMenu(this)' /><br>" +
                    "<img src='../images/Knob_Cancel.jpg' onclick='deleteMenu(this)' />" +
                    "</td><td>";

                strLCurrentMenu += "<fieldset>" +
                                " <legend>Menú</legend>" +
                                " <table>" +
                                "    <tr>" +
                                "        <td>Texto:</td>" +
                                "        <td><input type='text' value='" + dsMenu.Tables[ 0 ].Rows[ i ][ strTextField ] + "'/></td>" +
                                "        <td>Valor:</td>" +
                                "        <td><input type='text' value='" + dsMenu.Tables[ 0 ].Rows[ i ][ strValueField ] + "'/></td>" +
                                "    </tr>" +
                                "    <tr>" +
                                "        <td>Url:</td>" +
                                "        <td><input type='text' value='" + dsMenu.Tables[ 0 ].Rows[ i ][ strUrlField ] + "'/></td>" +
                                "        <td>Target:</td>" +
                                "        <td><input type='text' value='" + dsMenu.Tables[ 0 ].Rows[ i ][ strTargetField ] + "'/></td>" +
                                "    </tr>" +
                                " </table>" +
                                "</fieldset>";

                strLCurrentMenu += "<br/><div>";
                strLCurrentMenu += loadMenuChilds( dsMenu.Tables[ 0 ].Rows[ i ][ strColumnaPadre ].ToString( ) );
                strLCurrentMenu += "</div></td></tr></table></div>";

            }
        }


        return strLCurrentMenu;

    }

    protected void btnCancelarMenu_Click( object sender, EventArgs e ) {
        Response.Redirect( "MenuGrupo.aspx" );
    }
    
    protected void btnGuardarMenu_Click( object sender, EventArgs e ) {

        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".btnGuardarMenu_Click.Inicio");
            GrupoYMenu grupoMenu = new GrupoYMenu();
            GrupoMenuOpcionEntity _grupoMenu = new GrupoMenuOpcionEntity();

            DataSet tmpDs = grupoMenu.ConsultarGrupoMenuOpcion(txtGrupos.Text, "");
            if (tmpDs != null && tmpDs.Tables.Count > 0 && tmpDs.Tables[0].Rows.Count > 0)
            {
                Mensaje.MostrarMensaje(this, "La combinación seleccionada ya existe.");
                return;
            }

            #region Save XML

            int currentMenu = 0;
            if (txtSelectedMenu.Text == "")
            {
                txtSelectedMenu.Text = "mMenu" + txtGrupos.Text.Replace(",", "-") + ".xml";
                XmlDocument xmlDoc = new XmlDocument();

                string pathXml = ConfigurationManager.AppSettings["RUTA_XML"];
                string xmlObject = "<?xml version=\"1.0\" encoding=\"iso-8859-1\" ?><menu></menu>";
                xmlDoc.LoadXml(xmlObject);
                xmlDoc.Save(pathXml + txtSelectedMenu.Text);
            }

            string strFileMenu = ConfigurationManager.AppSettings["RUTA_XML"] + "mMenu.xml";
            strFileMenu = ConfigurationManager.AppSettings["RUTA_XML"] + txtSelectedMenu.Text;
            DataRow drMenu = null;

            dsMenu.ReadXml(strFileMenu);

            if (dsMenu.Relations.Count == 0)
            {
                string fileMenuSTD = ConfigurationManager.AppSettings["RUTA_XML"] + "mMenu.xml";
                dsMenu = new DataSet();
                dsMenu.ReadXml(fileMenuSTD);

                dsMenu.EnforceConstraints = false;
                dsMenu.Tables[0].Rows.Clear();
                dsMenu.EnforceConstraints = true;

                DataRow dr = null;
                DataSet dsOriginal = new DataSet();
                dsOriginal.ReadXml(strFileMenu);

                if (dsOriginal.Tables.Count > 0 && dsOriginal.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drOriginal in dsOriginal.Tables[0].Rows)
                    {
                        dr = dsMenu.Tables[0].NewRow();
                        foreach (DataColumn dcOriginal in dsOriginal.Tables[0].Columns)
                            dr[dcOriginal.ColumnName] = drOriginal[dcOriginal.ColumnName];

                        dsMenu.Tables[0].Rows.Add(dr);
                    }
                }
            }

            dsMenu.EnforceConstraints = false;
            dsMenu.Tables[0].Rows.Clear();
            dsMenu.EnforceConstraints = true;

            strColumnaHijo = dsMenu.Relations[0].ChildColumns[0].ColumnName;
            strColumnaPadre = dsMenu.Relations[0].ParentColumns[0].ColumnName;

            while (Request.Params["menu0_" + currentMenu.ToString() + "_text"] != null)
            {
                drMenu = dsMenu.Tables[0].NewRow();

                for (int i = 0; i < dsMenu.Tables[0].Columns.Count; i++)
                {
                    if (dsMenu.Tables[0].Columns[i].ColumnName.ToLower().Equals(strColumnaPadre.ToLower()))
                        continue;
                    if (dsMenu.Tables[0].Columns[i].ColumnName.ToLower().Equals(strColumnaHijo.ToLower()))
                        continue;

                    drMenu[dsMenu.Tables[0].Columns[i].ToString()] = (dsMenu.Tables[0].Columns[i].DataType.FullName.Contains("String") ? "" : "0");
                }

                intMCurrentId++;
                drMenu[strColumnaPadre] = intMCurrentId;
                drMenu[strTextField] = Request.Params["menu0_" + currentMenu.ToString() + "_text"];
                drMenu[strValueField] = Request.Params["menu0_" + currentMenu.ToString() + "_valor"];
                drMenu[strTargetField] = Request.Params["menu0_" + currentMenu.ToString() + "_target"];
                drMenu[strUrlField] = Request.Params["menu0_" + currentMenu.ToString() + "_url"];

                dsMenu.Tables[0].Rows.Add(drMenu);

                if (Request.Params["menu0_" + currentMenu.ToString() + "_0_text"] != null)
                    addChildMenus("menu0_" + currentMenu.ToString(), intMCurrentId);

                currentMenu++;
            }

            string strMenu = dsMenu.GetXml();


            dsMenu.WriteXml(strFileMenu);

            #endregion

            #region Save data to database

            if (this.txtMenuId.Text != string.Empty && txtMenuId.Text != null && txtMenuId.Text != "" && txtMenuId.Text != "0")
            {


                _grupoMenu.Id = Convert.ToInt32(this.txtMenuId.Text);
                _grupoMenu.NombreXMLMenu = "mMenu" + txtGrupos.Text.Replace(",", "-") + ".xml";
                _grupoMenu.GrupoId = txtGrupos.Text;
                _grupoMenu.NombreMenu = txtNombreMenu.Text;
                _grupoMenu.GrupoId = txtGrupos.Text;

                string strOldFileMenu = ConfigurationManager.AppSettings["RUTA_XML"] + _grupoMenu.NombreXMLMenu;

                GrupoYMenu menu = new GrupoYMenu();
                DataSet dsOldMenu = menu.ConsultarGrupoMenuOpcion("-1", "-1", "", _grupoMenu.Id.ToString());

                try
                {
                    System.IO.File.Move(strFileMenu, strOldFileMenu);
                }
                catch { }

                grupoMenu.ActualizarGrupoMenuOpcion(_grupoMenu);
            }
            else
            {
                _grupoMenu.Id = 0;
                _grupoMenu.NombreXMLMenu = "mMenu" + txtGrupos.Text.Replace(",", "-") + ".xml";
                _grupoMenu.GrupoId = txtGrupos.Text;
                _grupoMenu.MenuId = "0";
                _grupoMenu.NombreMenu = txtNombreMenu.Text;

                this.txtMenuId.Text = grupoMenu.InsertarGrupoMenuOpcion(_grupoMenu).ToString();
                selectedMenu = this.txtMenuId.Text;
            }

            #endregion

            loadCombos();
            loadMenu();
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".btnGuardarMenu_Click.Finalizo");
        }

        Mensaje.MostrarMensaje( this, "Menu guardado correctamente." );
    }

    private void addChildMenus( string tagmenu, int padreId ){

        DataRow drMenu = null;
        int currentMenu = 0;
        while( Request.Params[ tagmenu + "_" + currentMenu.ToString( ) + "_text" ] != null ){
            drMenu = dsMenu.Tables[ 0 ].NewRow( );
            
            for( int i = 0; i < dsMenu.Tables[ 0 ].Columns.Count; i++ ) {
                if( dsMenu.Tables[ 0 ].Columns[ i ].ColumnName.ToLower( ).Equals( strColumnaPadre.ToLower( ) ) )
                    continue;
                if( dsMenu.Tables[ 0 ].Columns[ i ].ColumnName.ToLower( ).Equals( strColumnaHijo.ToLower( ) ) )
                    continue;

                drMenu[ dsMenu.Tables[ 0 ].Columns[ i ].ToString( ) ] = ( dsMenu.Tables[ 0 ].Columns[ i ].DataType.FullName.Contains( "String" ) ? "" : "0" );
            }

            intMCurrentId++;
            drMenu[ strColumnaPadre ] = intMCurrentId;
            drMenu[ strColumnaHijo ] = padreId;
            drMenu[ strTextField ] = Request.Params[ tagmenu + "_" + currentMenu.ToString( ) + "_text" ];
            drMenu[ strValueField ] = Request.Params[ tagmenu + "_" + currentMenu.ToString( ) + "_valor" ];
            drMenu[ strTargetField ] = Request.Params[ tagmenu + "_" + currentMenu.ToString( ) + "_target" ];
            drMenu[ strUrlField ] = Request.Params[ tagmenu + "_" + currentMenu.ToString( ) + "_url" ];

            dsMenu.Tables[ 0 ].Rows.Add( drMenu );

            if( Request.Params[ tagmenu + "_" + currentMenu.ToString() + "_text" ] != null )
                addChildMenus( tagmenu + "_" + currentMenu.ToString( ), intMCurrentId );
            
            currentMenu++;
        }
    }

    protected void ddlMenus_SelectedIndexChanged( object sender, EventArgs e ) {
        loadMenu( );
    }
}
