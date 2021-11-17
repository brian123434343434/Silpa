#region NameSpaces default de .NET

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

#endregion

#region NameSpaces Incluidos por el desarrollador

//using SILA.Administracion;
//using SILA.Seguridad;

#endregion

public partial class adm_parametros_tablas_basicas : System.Web.UI.Page
{
    #region Atributos

    //#region Documentación Atributo
    ///// <summary>
    /////     Objeto de configuracion
    ///// </summary>
    //#endregion
    //protected Configuracion oConfig;

    //#region Documentación Atributo
    ///// <summary>
    /////     Objeto que se encarga de manipular la bitacora de acciones
    ///// </summary>
    //#endregion
    //protected Bitacora oBita;

    //#region Documentación Atributo
    ///// <summary>
    /////     Objeto que se encarga de manipular los datos y el comportamiento del administrador
    ///// </summary>
    //#endregion
    //protected Administrador oAdmin;

    //#region Documentación Atributo
    ///// <summary>
    /////     Objeto que se encarga de manipular los datos y el comportamiento de las listas basicas
    ///// </summary>
    //#endregion
    //protected SILA.Administracion.ListasBasicas oLista;

    //#region Documentación Atributo
    ///// <summary>
    /////     Objeto que se encarga de manipular los datos y el comportamiento del sitio
    ///// </summary>
    //#endregion
    //protected Sitio oSitio;

    #endregion

    #region Carga de pagina

    #region Documentacion Metodo
    /// <summary>
    /// Este metodo se encarga de cargar la pagina en su estado inicial
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        this.iniciarPagina();

        if (!this.IsPostBack)
            this.cargaPagina();
    }

    #region Documentacion Metodo
    /// <summary>
    /// Metodo que asegura la persistencia de algunos de los datos y de 
    /// los procedimientos de la pagina
    /// </summary>
    #endregion
    private void iniciarPagina()
    {
        //this.oConfig = new Configuracion();
        //this.oConfig = (Configuracion)Application["oConfig"];
        //this.oBita = new Bitacora(this.oConfig);
        //this.oLista = new ListasBasicas(this.oConfig);
        //this.oSitio = new Sitio(this.oConfig);
        //this.oAdmin = new Administrador(this.oConfig, Profile.PerfilAdministrador.AdministradorID, true);
        //this.Session["rolID"] = null;

        //this.oBita.SitioID = Profile.PerfilAdministrador.SitioID;
        //this.oBita.AdministradorID = Profile.PerfilAdministrador.AdministradorID;
        //this.oBita.Modulo = Modulo.Roles;
        //this.oBita.IP = Request.UserHostName;
    }

    #region Documentacion Metodo
    /// <summary>
    /// Metodo que se encarga de cargar las listas basicas clasificadas por
    /// modulos.
    /// </summary>
    #endregion
    private void cargarListas()
    {
        //Label lbl_mod_id;        
        //GridView dgv_tablas_basicas;

        //this.rpt_modulos.DataSource = this.oSitio.listarModulos();
        //this.rpt_modulos.DataBind();

        //foreach (RepeaterItem rpti in this.rpt_modulos.Items)
        //{
        //    dgv_tablas_basicas = (GridView)rpti.FindControl("dgv_tablas_basicas");

        //    lbl_mod_id = (Label)rpti.FindControl("lbl_mod_id");            

        //    dgv_tablas_basicas.DataSource = this.oLista.listarListas((Modulo)Int32.Parse(lbl_mod_id.Text));
        //    dgv_tablas_basicas.DataBind();

        //    if (dgv_tablas_basicas.Rows.Count < 1)
        //        rpti.Visible = false;  
  

        //}
    }

    #region Documentacion Metodo
    /// <summary>
    /// Metodo que se encarga de cargar (solamente la primera vez)
    /// los contenidos de los controles de la pagina
    /// </summary>
    #endregion
    private void cargaPagina()
    {
        this.cargarListas();
        this.validarPermisos();
    }

    #region Documentacion Metodo
    /// <summary>
    /// Metodo que se encarga de comprobar los permisos del administrador en sesion.
    /// </summary>
    #endregion
    private void validarPermisos()
    {
        //GridView dgv_tablas_basicas;

        //Permiso oPer = new Permiso();
        //oPer = this.oAdmin.consultarPermiso((Int32)Modulo.Tablas_Basicas, Tipo_Permiso.Modulo);

        //foreach (RepeaterItem rpti in this.rpt_modulos.Items)
        //{
        //    dgv_tablas_basicas = (GridView)rpti.FindControl("dgv_tablas_basicas");
        //    dgv_tablas_basicas.Enabled = oPer.Consultar;
        //}
    }

    protected void btn_cargar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/adm_parametros/tablas_basicas_cargar.aspx");
    }
    #endregion
}
