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
using SILPA.AccesoDatos;
using System.Collections.Generic;
using SILPA.AccesoDatos.GrupoYUsuarios;
using SILPA.LogicaNegocio.GrupoYUsuarios;
using SILPA.LogicaNegocio.Generico;
using SILPA.Comun;
using SILPA.AccesoDatos.Generico;
using System.Xml;
using SoftManagement.Log;

public partial class GrupoYUsuarios_CreacionMenuGrupo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        Mensaje.LimpiarMensaje(this);
       

        if (IsPostBack == false)
        {
            if (Request.QueryString["id"] != null)
                this.lblId.Text = Request.QueryString["id"].ToString();

            CrearCombos();
            CrearTablasDatos();          
        }
    }

    private void CrearTablasDatos()
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".CrearTablasDatos.Inicio");
            if (this.lblId.Text != string.Empty)
            {
                GrupoYMenu grupoMenu = new GrupoYMenu();
                DataSet _grupoMenu = grupoMenu.ConsultarGrupoMenuOpcion(Convert.ToInt32(this.lblId.Text), "-1");
                if (_grupoMenu.Tables[0].Rows.Count > 0)
                {
                    //Para los grupos               
                    this.lstGrupos.DataSource = grupoMenu.ConsultarGrupo(_grupoMenu.Tables[0].Rows[0]["GMO_GRUPO"].ToString());
                    this.lstGrupos.DataValueField = "ID";
                    this.lstGrupos.DataTextField = "Name";
                    this.lstGrupos.DataBind();

                    //Para los menus
                    string _menus = string.Empty;
                    string _opciones = string.Empty;
                    string[] colMenu = _grupoMenu.Tables[0].Rows[0]["GMO_MENU"].ToString().Split(',');
                    foreach (string fila in colMenu)
                    {
                        int opcion = fila.IndexOf("_");
                        if (opcion > 0)
                        {
                            _menus = _menus + fila.Substring(0, opcion) + ",";
                            _opciones = _opciones + fila.Substring(opcion + 1) + ",";
                        }
                        else
                            _menus = _menus + fila + ",";
                    }
                    //Se quita la coma del final
                    _menus = _menus.Substring(0, _menus.Length - 1);
                    if (_opciones != string.Empty)
                        _opciones = _opciones.Substring(0, _opciones.Length - 1);
                    else
                        _opciones = "0";

                    this.lstMenus.DataSource = grupoMenu.ConsultarMenuOpcion(_menus, _opciones);
                    this.lstMenus.DataValueField = "ID";
                    this.lstMenus.DataTextField = "NOMBRE";
                    this.lstMenus.DataBind();

                    this.btnAdicionarG.Enabled = false;
                    this.btnQuitarG.Enabled = false;
                }
            }
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".CrearTablasDatos.Finalizo");
        }
    }

    private void CrearCombos()
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".CrearCombos.Inicio");
            cboGrupos.DataSource = GrupoYMenu.ConsultarGrupo();
            cboGrupos.DataValueField = "ID";
            cboGrupos.DataTextField = "Name";
            cboGrupos.DataBind();
            cboGrupos.Items.Insert(0, new ListItem("Seleccione...", "-1"));

            //cboMenus.DataSource = GrupoYMenu.ListarMenuOpcion(-1, -1);
            //cboMenus.DataValueField = "ID";
            //cboMenus.DataTextField = "NOMBRE";
            //cboMenus.DataBind();
            //cboMenus.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            MenuDalc menu = new MenuDalc();
            cboMenus.DataSource = menu.ConsultarMenu();
            cboMenus.DataValueField = "MENU_ID";
            cboMenus.DataTextField = "TITULO_MENU";
            cboMenus.DataBind();
            cboMenus.Items.Insert(0, new ListItem("Seleccione...", "-1"));
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".CrearCombos.Finalizo");
        }
    }

    private string shortMenus( string menus ){
        string sortedMenu = "";
        string[] arrMenus = menus.Split( ",".ToCharArray( ) );
        string tmpMenu = "";

        for( int i = 0; i < arrMenus.Length; i++ ){
            for( int j = 0; j < arrMenus.Length; j++ ){

                if( int.Parse(arrMenus[ i ]) < int.Parse(arrMenus[ j ]) ){
                    tmpMenu = arrMenus[ j ];
                    arrMenus[ j ] = arrMenus[ i ];
                    arrMenus[ i ] = tmpMenu;
                }

            }
        }

        for( int i = 0; i < arrMenus.Length; i++ )
            sortedMenu += ( i > 0 ? "," : "" ) + arrMenus[ i ];

        return sortedMenu;
    }

    protected void btnAceptar_Click(object sender, EventArgs e)
    {
        if (this.lstGrupos.Items.Count > 0)
        {
            GrupoYMenu grupoMenu = new GrupoYMenu();
            GrupoMenuOpcionEntity _grupoMenu = new GrupoMenuOpcionEntity( );
            string pathXml = ConfigurationManager.AppSettings[ "RUTA_XML" ];
            string xmlObject = "<?xml version=\"1.0\" encoding=\"iso-8859-1\" ?><menu></menu>";
            string _grupo = string.Empty;
            XmlDocument xmlDoc = new XmlDocument( );
            string _menu = string.Empty;
            string _opciones = string.Empty;
            string _menus = string.Empty;

            _grupoMenu.NombreMenu = txtNombreMenu.Text;

            foreach (ListItem li in this.lstGrupos.Items)
                _grupo = _grupo + ( _grupo.Equals( "" ) ? "" : "," ) + li.Value;

            _grupo = shortMenus( _grupo );
            
            if (_opciones != string.Empty)
                _opciones = _opciones.Substring(0, _opciones.Length - 1);
            else
                _opciones = "0";
          
            _grupoMenu.GrupoId = _grupo;
            _grupoMenu.MenuId = "-1";

            if (this.lblId.Text != string.Empty){

                _grupoMenu.Id = Convert.ToInt32( this.lblId.Text );
                _grupoMenu.NombreXMLMenu = "mMenu" + _grupo.Replace( ",", "-" ) + ".xml";
                grupoMenu.ActualizarGrupoMenuOpcion(_grupoMenu);

            }
            else{
                if( grupoMenu.ConsultarGrupoMenuOpcion( _grupo ) ){
                    Mensaje.MostrarMensaje(this, "El grupo elegido ya tiene asignado un menú en el sistema.");
                    return;
                }
                _grupoMenu.Id = 0;

                _grupoMenu.NombreXMLMenu = "mMenu" + _grupo.Replace( ",", "-" ) + ".xml";
                this.lblId.Text = grupoMenu.InsertarGrupoMenuOpcion(_grupoMenu).ToString();
            }


            xmlDoc.LoadXml( xmlObject );
            xmlDoc.Save( pathXml + _grupoMenu.NombreXMLMenu );

            //grupoMenu.GenerarArchivoXml(_menus, _opciones, _grupo, pathXml);

            Mensaje.MostrarMensaje(this, "El menú y el grupo elegido ha sido creado en el sistema!.");
            this.btnAdicionarG.Enabled = false;
            this.btnQuitarG.Enabled = false;
        }
        else
            Mensaje.MostrarMensaje(this, "Debe seleccionar al menos un (1) grupo.");

    }
       
    /// <summary>
    /// Evento click sobre el boton adicionar
    /// </summary>
    protected void btnAdicionarM_Click(object sender, EventArgs e)
    {
        if (this.cboMenus.SelectedValue != "-1")
        {
            if (this.ElementoDuplicadoLista(this.lstMenus.Items, this.cboMenus.SelectedValue))
                this.lstMenus.Items.Add(this.cboMenus.SelectedItem);         
            else
                Mensaje.MostrarMensaje(this, "Este item ya ha sido adicionado.");
        }
        // Focus
        this.SetFocusControl(this.lstMenus.ClientID);
    }

    /// <summary>
    /// Evento de click sobre el boton de retirar
    /// </summary>
    protected void btnQuitarM_Click(object sender, EventArgs e)
    {
        if (this.lstMenus.SelectedIndex >= 0)
            this.lstMenus.Items.RemoveAt(this.lstMenus.SelectedIndex);
        // Focus
        this.SetFocusControl(this.lstMenus.ClientID);
    }  

    /// <summary>
    /// Evento click sobre el boton adicionar
    /// </summary>
    protected void btnAdicionarG_Click(object sender, EventArgs e)
    {
        if (this.cboGrupos.SelectedValue != "-1")
        {
            if (this.ElementoDuplicadoLista(this.lstGrupos.Items, this.cboGrupos.SelectedValue))
                this.lstGrupos.Items.Add(this.cboGrupos.SelectedItem);
            else
                Mensaje.MostrarMensaje(this, "Este item ya ha sido adicionado.");
        }
        // Focus
        this.SetFocusControl(this.lstGrupos.ClientID);
    }

    /// <summary>
    /// Evento de click sobre el boton de retirar
    /// </summary>
    protected void btnQuitarG_Click(object sender, EventArgs e)
    {
        if (this.lstGrupos.SelectedIndex >= 0)
            this.lstGrupos.Items.RemoveAt(this.lstGrupos.SelectedIndex);
        // Focus
        this.SetFocusControl(this.lstGrupos.ClientID);
    }

    //Se valida si existe el item en la lista
    protected bool ElementoDuplicadoLista(ListItemCollection lista, string valor)
    {
        try
        {
            bool validar = true;
            ListItem item = null;
            item = lista.FindByValue(valor);
            if (item != null)
                validar = false;

            return validar;
        }
        catch (Exception ex)
        {

            Mensaje.ErrorCritico(this, ex);
            return false;
        }
    }   

    /// <summary>
    /// Establece focus sobre un control
    /// </summary>
    private void SetFocusControl(string ControlName)
    {
        string script = "<script language=\"javascript\">var control = document.getElementById(\"" + ControlName + "\"); if( control != null ){control.focus();}</script>";
        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "Focus", script);
    }
   
}
