using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using SoftManagement.Log;
using System.Data;
using SoftManagement.Persistencia;
using System.Data.SqlClient;
using SILPA.AccesoDatos.ResumenEIA.Basicas;

public partial class ResumenEIA_ctrFicha2 : System.Web.UI.UserControl
{

    //private int iDProyecto=1;

    public int IDProyecto
    {
        get
        {
            if (ViewState["IdProyecto"] != null)
                return (int)ViewState["IdProyecto"];
            return -1;
        }
        set { ViewState["IdProyecto"] = value; }

    }

    public DateTime fecha
    {
        get { return new DateTime(2010, 11, 25, 14, 31, 50, 767); }
    }
       

    DataSet dsInformacionProyecto;
    DataSet dsVLocalizaciones;
    DataSet dsLocalizaciones;

  
     private void cargarFormulario()
     {
      
         DataSet ds = new DataSet();
         List<SqlParameter> Parametros = new List<SqlParameter>();
         SqlParameter par = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
         par.Value = IDProyecto;
         Parametros.Add(par);
         Contexto.cargarTabla(Parametros, ds, "EIH_INFORMACION_PROYECTO");
         if (ds.Tables[0].Rows.Count > 0)
         {
             DataRow dr = ds.Tables[0].Rows[0];

             txtNombreProyecto.Text = dr["EIP_NOMBRE_PROYECTO"].ToString();
             if(dr["ESP_ID"].ToString()!="")
                cboSectoProductivo.SelectedValue = dr["ESP_ID"].ToString();
             txtNombreBloqueHidro.Text = dr["EIP_NOMBRE_BLOQUE"].ToString();
             cboUnidadAreaH.SelectedValue = dr["EOP_ID"].ToString();
             txtAreaTotalH.Text = dr["EIP_AREA_TOTAL"].ToString();
             txtAreaIntervH.Text = dr["EIP_AREA_A_INTERVENIR"].ToString();
             txtDireccionProyecto.Text = dr["EIP_DIRECCION_PROYECTO"].ToString();
             if(dr["EOP_ID"].ToString()!="")
                cboOrigenCoordenadas.SelectedValue = dr["EOP_ID"].ToString();
             if(dr["EET_ID"].ToString()!="")
                cboEscalaTrabajoCoord.SelectedValue = dr["EET_ID"].ToString();
             txtVolTotCorteMovTierra.Text = dr["EIP_VOL_TOTAL_CORTE"].ToString();
             txtVolTotRellenoMovTierra.Text = dr["EIP_VOL_TOTAL_RELLENO"].ToString();
             txtVolTotSobranteMovTierra.Text = dr["EIP_MAT_TOTAL_SOBRANTE"].ToString();
             txtCantManoObraCalf.Text = dr["EIP_CANT_MANO_OBRA_CALIFICADA"].ToString();
             txtCantManoObraNoCalf.Text = dr["EIP_CANT_MANO_OBRA_NOCALIFICADA"].ToString();
             txtCostoProyecto.Text = dr["EIP_COSTO_PROYECTO"].ToString();
             txtDuracionProyecto.Text = dr["EIP_DURACION_PROYECTO"].ToString();
             if(dr["EUT_ID"].ToString()!="")
                cboUnidadDurProyecto.SelectedValue = dr["EUT_ID"].ToString();
             txtCostoBaseInversion.Text = dr["EIP_MONTO_BASE_INVERSION"].ToString();
             if(dr["EET_ID"].ToString()!="")
                this.cboEscalaTrabajoCoord.SelectedValue = dr["EET_ID"].ToString();

             if (cboSectoProductivo.SelectedValue != "-1")
             {
                 cargarComboInfraestructuraLineal(cboSectoProductivo.SelectedValue);
                 cargarComboInfraestructuraNoLineal(cboSectoProductivo.SelectedValue);
             }
             else
             {
                 cargarComboInfraestructuraLineal("0");
                 cargarComboInfraestructuraNoLineal("0");
             }

         }

     }
     private void cargarGrvLocalizaciones()
     {
         dsVLocalizaciones = new DataSet();
         List<SqlParameter> Parametros = new List<SqlParameter>();
         SqlParameter par = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
         par.Value = IDProyecto;
         Parametros.Add(par);        
         Contexto.cargarTabla(Parametros, dsVLocalizaciones, "EIV_LOCALIZACION_PROYECTO");
         this.grvLocalizacionProyecto.DataSource = dsVLocalizaciones.Tables[0];
         this.grvLocalizacionProyecto.DataBind();

     }
     private void cargardsInformacionProyecto()
     {
         List<SqlParameter> Parametros = new List<SqlParameter>();
         SqlParameter par = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
         par.Value = IDProyecto; 
         Parametros.Add(par);
         Contexto.cargarTabla(Parametros, dsInformacionProyecto, "EIH_INFORMACION_PROYECTO");
     }
     private void cargardslocalizaciones()
     {
         dsLocalizaciones = new DataSet();
         List<SqlParameter> Parametros = new List<SqlParameter>();
         SqlParameter par = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
         par.Value = IDProyecto;
         Parametros.Add(par);
         Contexto.cargarTabla(Parametros, dsLocalizaciones, "EIh_LOCALIZACION_PROYECTO");
     }
     private void agregarLocalizacion()
     {
         cargardslocalizaciones();
         DataRow dr = this.dsLocalizaciones.Tables[0].NewRow();
         dr["EIP_ID"] = this.IDProyecto; 
         dr["DEP_ID"] = cboDeptoLocalizacion.SelectedValue;
         dr["MUN_ID"] = cboMunicipioLocalizacion.SelectedValue;
         dr["EPA_ID"] = cboUnidadPoliticoAdmin.SelectedValue;
         dr["ELP_NOMBRE_UNIDAD_POL_ADMIN"] = txtNombreUPoliticoAdmin.Text;
         dr["ELP_PREDIO"] = txtNombrePredio.Text;
         dsLocalizaciones.Tables[0].Rows.Add(dr);
         Contexto.guardarTabla(dsLocalizaciones, "EIh_LOCALIZACION_PROYECTO");    
     }
     private void cargarGrvAutoridadAmbiental()
    {
        DataSet ds= new DataSet ();
        List<SqlParameter> Parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
        par.Value = IDProyecto;
        Parametros.Add(par);
        Contexto.cargarTabla(Parametros,ds, "EIV_AUTORI_AMBIEN_JURISDIC_PROY");
        this.grvAutoridadesAmbientales.DataSource = ds.Tables[0];
        this.grvAutoridadesAmbientales.DataBind();        
    }
     private void cargarGrvInfraestructuraLineal()
     { 
      DataSet ds= new DataSet ();
        List<SqlParameter> Parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
        par.Value = IDProyecto;
        Parametros.Add(par);
        Contexto.cargarTabla(Parametros,ds, "EIV_INFRA_LINEAL_PROYECTO");
        this.grvInfracLinealProy .DataSource = ds.Tables[0];
        this.grvInfracLinealProy .DataBind();     
     }
     private void confirmacionNoLineal()
     {
         DataSet ds = new DataSet();
         List<SqlParameter> parametros = new List<SqlParameter>();
         SqlParameter par = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
         par.Value = IDProyecto;
         parametros.Add(par);
         Contexto.cargarTabla(parametros, ds, "EIV_INFRA_NO_LINEAL_PROYECTO");
         this.grvInfracNoLinealProy.DataSource = ds.Tables[0];
         this.grvInfracNoLinealProy.DataBind();
     }
     private void cargarGrvNolineal()
     {
         DataSet ds = new DataSet();
         List<SqlParameter> parametros = new List<SqlParameter>();
         SqlParameter par = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
         par.Value = IDProyecto;
         parametros.Add(par);
         Contexto.cargarTabla(parametros, ds, "EIV_INFRA_NO_LINEAL_PROYECTO");
         this.grvInfracNoLinealProy.DataSource = ds.Tables[0];
         this.grvInfracNoLinealProy.DataBind();


     }
     private void cargarGrvMovTierraInfracLinealProy()
     {
         DataSet ds = new DataSet();
         List<SqlParameter> parametros = new List<SqlParameter>();
         SqlParameter par = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
         par.Value = IDProyecto;
         parametros.Add(par);
         Contexto.cargarTabla(parametros, ds, "EIV_MOV_TIERRAS_INFRA_LINEAL");
         grvMovTierraInfracLinealProy.DataSource = ds.Tables[0];
         grvMovTierraInfracLinealProy.DataBind();
     }
     private void cargarGrvMovTierraInfracNoLinealProy()
     {
         DataSet ds = new DataSet();
         List<SqlParameter> parametros = new List<SqlParameter>();
         SqlParameter par = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
         par.Value = IDProyecto;
         parametros.Add(par);
         Contexto.cargarTabla(parametros, ds, "EIV_MOV_TIERRAS_INFRA_NOLINEAL");
         grvMovTierraInfracNoLinealProy.DataSource = ds.Tables[0];
         grvMovTierraInfracNoLinealProy.DataBind();


     }

     
    protected void Page_Load(object sender, EventArgs e)
    {
    
        if (!IsPostBack)
        {     
          
        }
    }
    public void CargarTodaInfo()
    {
        cargarCombos();
        cargarFormulario();
        cargarGrvLocalizaciones();
        cargarGrvAutoridadAmbiental();
        cargarGrvMovTierraInfracLinealProy();
        cargarGrvMovTierraInfracNoLinealProy();
        cargarGrvInfracIntercProy();
        cargarGrvAsentInfrac();
        cargarGrvAreasDispoMatProy();
        cargarGrvInfraestructuraLineal();
        cargarGrvInfraNoLineal();
    }

    #region Métodos de inicialización

    //protected void cargarControlCoordenadas()
    //{
    //    Control ctrl = Page.LoadControl("~/ResumenEIA/Controles/ctrCoordenadas.ascx");
    //    plhControlCoordUbicacion.Controls.Add(ctrl);
    //}



    protected void cargarCombos()
    {
        cargarComboSectorProductivo();
        cargarComboMunicipios();
        cargarComboOrigen();
        cargarComboUnidadPoliticoAdministrativa();
        CargarEscalaTrabajo();
        cargarComboAutoridadAmbiental();
        cargarComboUnidadLongitud();
        cargarComboUnidadArea();
        cargarComboInfraNoLinealAsoc();
        cargarComboInfraLinealAsoc();
        cargarCombocTipoInfracIntercProy();
        CargarComboUnidLongAsentInfrac();
        cargarComboUnidadDurProyecto();

    }

    private void cargarComboUnidadDurProyecto()
    {
        DataSet ds= new DataSet ();
        Contexto.cargarTabla(ds, "EIB_UNIDAD_TIEMPO");
        cboUnidadDurProyecto.DataSource = ds.Tables[0];
        cboUnidadDurProyecto.DataValueField = "EUT_ID";
        cboUnidadDurProyecto.DataTextField = "EUT_UNIDAD_TIEMPO";
        cboUnidadDurProyecto.DataBind();
        cboUnidadDurProyecto.Items.Insert(0, new ListItem("Seleccione...", "-1")); 
    
    }
        
    private void cargarCombocTipoInfracIntercProy(){
        DataSet ds= new DataSet ();
        Contexto.cargarTabla(ds, "EIB_TIPO_INFRA_INTER_PROY");
        this.cboTipoInfracIntercProy.DataSource = ds.Tables[0];
        cboTipoInfracIntercProy.DataTextField = "EII_TIPO_INFRAC_INTERCEP";
        cboTipoInfracIntercProy.DataValueField = "EII_ID";
        cboTipoInfracIntercProy.DataBind();


        
    }

    private void CargarComboUnidLongAsentInfrac()
    { 
        DataSet ds= new DataSet ();
        Contexto .cargarTabla(ds,"EIB_UNIDAD_LONGITUD");
        cboUnidLongAsentInfrac.DataSource = ds.Tables[0];
        cboUnidLongAsentInfrac.DataTextField = "EUL_UNIDAD_LONGITUD";
        cboUnidLongAsentInfrac.DataValueField = "EUL_ID";
        cboUnidLongAsentInfrac.DataBind();
        cboUnidLongAsentInfrac.Items.Insert(0, new ListItem("Seleccione...", "-1")); 
    }

    private void cargarComboInfraNoLinealAsoc()
    { 
        DataSet ds = new DataSet();
        Contexto.cargarTabla(ds, "EIB_INFRAC_ASOC_NOLINEAL_MOV_TIERRA");
        cboInfracAsocMovTierNoLineal.DataSource = ds.Tables[0];
        cboInfracAsocMovTierNoLineal.DataTextField = "EIN_TIPO_INFRAC_ASOC_NL";
        cboInfracAsocMovTierNoLineal.DataValueField = "EIN_ID";
        cboInfracAsocMovTierNoLineal.DataBind();
        cboInfracAsocMovTierNoLineal.Items.Insert(0, new ListItem("Seleccione...", "-1")); 
   
    }

    private void cargarComboInfraLinealAsoc()
    {
        DataSet ds = new DataSet();
        Contexto.cargarTabla(ds, "EIB_INFRAC_ASOC_LINEAL_MOV_TIERRA");
        cboInfracAsocMovTierLineal.DataSource = ds.Tables[0];
        cboInfracAsocMovTierLineal.DataTextField = "EIL_TIPO_INFRAC_ASOC_L";
        cboInfracAsocMovTierLineal.DataValueField = "EIL_ID";
        cboInfracAsocMovTierLineal.DataBind();
        cboInfracAsocMovTierLineal.Items.Insert(0, new ListItem("Seleccione...", "-1")); 

    }

    private void cargarComboOrigen() {
        DataSet ds = new DataSet();
        Contexto.cargarTabla(ds, "EIB_ORIGEN_PRE_AREA_INFLU_DIREC");
        cboOrigenCoordenadas.DataSource = ds.Tables[0];
        cboOrigenCoordenadas.DataTextField = "EOP_ORIGEN";
        cboOrigenCoordenadas.DataValueField = "EOP_ID";
        cboOrigenCoordenadas.DataBind();
        cboOrigenCoordenadas.Items.Insert(0, new ListItem("Seleccione...", "-1"));    
    }
    private void cargarComboAutoridadAmbiental()
    {
        DataSet ds = new DataSet();
        Contexto.cargarTabla (ds,"EIB_AUTORIDAD_AMBIENTAL");
        cboAutoridadAmbiental.DataSource = ds.Tables[0];
        cboAutoridadAmbiental.DataTextField = "EAA_AUTORIDAD_AMBIENTAL";
        cboAutoridadAmbiental.DataValueField = "EAA_ID";
        cboAutoridadAmbiental.DataBind();
        cboAutoridadAmbiental.Items.Insert(0, new ListItem("Seleccione...", "-1"));        
    }
    private void cargarComboUnidadPoliticoAdministrativa()
    {
        DataSet ds= new DataSet ();
        Contexto.cargarTabla(ds, "EIB_UNIDAD_POL_ADMIN");
        this.cboUnidadPoliticoAdmin.DataSource = ds.Tables[0];
        this.cboUnidadPoliticoAdmin.DataTextField = "EPA_UNIDAD_POL_ADMIN";
        this.cboUnidadPoliticoAdmin.DataValueField = "EPA_ID";
        cboUnidadPoliticoAdmin.DataBind();
        cboUnidadPoliticoAdmin.Items.Insert(0, new ListItem("Seleccione...", "-1"));


    }
    private void cargarComboMunicipio()
    {
        int idDepto = -1;
        int.TryParse(cboDeptoLocalizacion .SelectedValue.ToString(), out idDepto);
        cboMunicipioLocalizacion .DataSource = SILPA.LogicaNegocio.ResumenEIA.Listas.ListaMunicipio(idDepto);
        cboMunicipioLocalizacion.DataValueField = "MUN_ID";
        cboMunicipioLocalizacion.DataTextField = "MUN_NOMBRE";
        cboMunicipioLocalizacion.DataBind();
        cboMunicipioLocalizacion.Items.Insert(0, new ListItem("Seleccione...", "-1"));
    }
    private void cargarComboUnidadLongitud()
    {
        DataSet ds = new DataSet();
        Contexto.cargarTabla(ds, "EIB_UNIDAD_LONGITUD");
        this.cboUnidLongInfracLineal.DataSource = ds.Tables[0];
        this.cboUnidLongInfracLineal.DataTextField = "EUL_UNIDAD_LONGITUD";
        this.cboUnidLongInfracLineal.DataValueField = "EUL_ID";
        cboUnidLongInfracLineal.DataBind();
        cboUnidLongInfracLineal.Items.Insert(0, new ListItem("Seleccione...", "-1"));
    }
    private void cargarComboUnidadArea()
    { 
    DataSet ds = new DataSet();
    Contexto.cargarTabla(ds, "EIB_UNIDAD_AREA");
    cboUnidAreaInfracLineal.DataSource = ds.Tables[0];
    cboUnidAreaInfracLineal.DataTextField = "EUA_UNIDAD_AREA";
    cboUnidAreaInfracLineal.DataValueField = "EUA_ID";
    cboUnidAreaInfracLineal.DataBind();
    cboUnidAreaInfracLineal.Items.Insert(0, new ListItem("Seleccione...", "-1"));

    cboUnidAreaInfracNoLinealProy.DataSource = ds.Tables[0];
    cboUnidAreaInfracNoLinealProy.DataTextField = "EUA_UNIDAD_AREA";
    cboUnidAreaInfracNoLinealProy.DataValueField = "EUA_ID";
    cboUnidAreaInfracNoLinealProy.DataBind();
    cboUnidAreaInfracNoLinealProy.Items.Insert(0, new ListItem("Seleccione...", "-1"));
    }
    


    private void cargarComboInfraestructuraNoLineal(string p)
    {
        DataSet ds = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@ESP_ID", SqlDbType.Int, 10, "ESP_ID");
        par.Value = int.Parse(p);
        parametros.Add(par);
        Contexto.cargarTabla(parametros, ds, "EIB_DESCRIP_INFRA_NO_LINEAL");
        cboDescInfracNoLinealProy.DataSource = ds.Tables[0];
        cboDescInfracNoLinealProy.DataTextField = "EDN_DESCRIP_INFRA_NO_LINEAL";
        cboDescInfracNoLinealProy.DataValueField = "EDN_ID";
        cboDescInfracNoLinealProy.DataBind();
        cboDescInfracNoLinealProy.Items.Insert(0, new ListItem("Seleccione...", "-1"));
    }
    private void cargarComboInfraestructuraLineal(string p)
    {
        DataSet ds = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@ESP_ID", SqlDbType.Int, 10, "ESP_ID");
        par.Value = int.Parse(p);
        parametros.Add(par);
        Contexto.cargarTabla(parametros, ds, "EIB_DESCRIP_INFRA_LINEAL");
        cboDescInfracLinealProy.DataSource = ds.Tables[0];
        cboDescInfracLinealProy.DataTextField = "EDL_DESCRIP_INFRA_LINEAL";
        cboDescInfracLinealProy.DataValueField = "EDL_ID";
        cboDescInfracLinealProy.DataBind();
        cboDescInfracLinealProy.Items.Insert(0, new ListItem("Seleccione...", "-1"));

    }

    private void CargarEscalaTrabajo()
    {
        DataSet ds = new DataSet();
        Contexto.cargarTabla(ds, "EIB_ESCALA_TRABAJO");
        cboEscalaTrabajoCoord.DataSource = ds.Tables [0];
        cboEscalaTrabajoCoord.DataTextField = "EET_ESCALA_TRABAJO";
        cboEscalaTrabajoCoord.DataValueField = "EET_ID";
        cboEscalaTrabajoCoord.DataBind();
        cboEscalaTrabajoCoord.Items.Insert(0, new ListItem("Seleccione...", "-1"));

    }    
    private void cargarComboMunicipios()
    {
       
        this.cboDeptoLocalizacion.DataSource =SILPA.LogicaNegocio.ResumenEIA.Listas.ListaDepartamento();    
        cboDeptoLocalizacion.DataValueField = "DEP_ID";
        cboDeptoLocalizacion.DataTextField = "DEP_NOMBRE";
        cboDeptoLocalizacion.DataBind();
        cboDeptoLocalizacion.Items.Insert(0, new ListItem("Seleccione...", "-1"));
    }
    private void cargarComboSectorProductivo()
    {
        SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".CargarSectorProductivo.Inicio");

        //Reemplazar por información de la base de datos
        cboSectoProductivo.DataSource = SILPA.LogicaNegocio.ResumenEIA.Listas.ListaSectoresProductivos();
        cboSectoProductivo.DataValueField = "ESP_ID";
        cboSectoProductivo.DataTextField = "ESP_SECTOR_PRODUCTIVO";
        cboSectoProductivo.DataBind();
        cboSectoProductivo.Items.Insert(0, new ListItem("Seleccione...", "-1"));

    }  
    #endregion
    protected void btnAgregarLocProyecto_Click(object sender, EventArgs e)
    {
        
        plhInfoLocalizacion.Visible = true;
    }
    protected void btnCancelarLocalizacion_Click(object sender, EventArgs e)
    {
        cboDeptoLocalizacion.SelectedIndex = -1;
        cboMunicipioLocalizacion.SelectedIndex = -1;
        cboUnidadPoliticoAdmin.SelectedIndex = -1;
        txtNombreUPoliticoAdmin.Text = "";
        txtNombrePredio.Text = "";
        plhInfoLocalizacion.Visible = false;

    }
    protected void cboSectoProductivo_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.txtOtroSectorProductivo.Visible = false;
        this.txtOtroSectorProductivo.Text = "";
        this.rqrOtroSector.Visible = false;
        if (cboSectoProductivo.SelectedItem.Text.Contains("Hidrocarburos"))
        {
            plhHidrocarburos.Visible = true;
            plhMineria.Visible = false;
        }
        else if (cboSectoProductivo.SelectedItem.Text.Contains("Minería"))
        {
            plhHidrocarburos.Visible = false;
            plhMineria.Visible = true;
        }        
        else
        {            
            if (cboSectoProductivo.SelectedItem.Text.Contains("Otro"))
            {
                this.txtOtroSectorProductivo.Visible = true;
                this.rqrOtroSector.Visible = true;
            }
            plhHidrocarburos.Visible = false;
            plhMineria.Visible = false;
        }
        cargarComboInfraestructuraLineal(cboSectoProductivo .SelectedValue );
        cargarComboInfraestructuraNoLineal(cboSectoProductivo.SelectedValue);
    }

    protected void btnAgregarAutoridad_Click(object sender, EventArgs e)
    {
        plhAutoridadesAmbientales.Visible = true;
        plhAutoridadesAmbientales.Focus();
       

    }

    protected void btnCancelarAutorAmb_Click(object sender, EventArgs e)
    {
        plhAutoridadesAmbientales.Visible = false;
    }
    //Infraestructura Lineal del proyecto
    protected void btnNuevoInfracLinealProy_Click(object sender, EventArgs e)
    {
        plhInfracLinealProy.Visible = true;
        plhInfracLinealProy.Focus();

    }

    protected void btnCancelarInfracLinealProy_Click(object sender, EventArgs e)
    {
        plhInfracLinealProy.Visible = false;

    }
    //Infraestructura no lineal del proyecto    
    protected void btnNuevaInfracNoLinealProy_Click(object sender, EventArgs e)
    {
               
        plhInfracNoLinealProy.Visible = true;
        plhInfracNoLinealProy.Focus();        

    }

    protected void btnCancelarInfracNoLinealProy_Click(object sender, EventArgs e)
    {
        plhInfracNoLinealProy.Visible = false;

    }
    //Moviemiento de tierras Infraestructura lineal del proyecto
    protected void btnMovTierraInfracLineal_Click(object sender, EventArgs e)
    {
        plhMovTierraInfracLineal.Visible = true;
        plhMovTierraInfracLineal.Focus();

    }

    protected void btnCancelarMovTierraInfracLineal_Click(object sender, EventArgs e)
    {
        plhMovTierraInfracLineal.Visible = false;

    }
    //Moviemiento de tierras Infraestructura no lineal del proyecto
    protected void btnMovTierraInfracNoLineal_Click(object sender, EventArgs e)
    {
        plhMovTierraInfracNoLineal.Visible = true;
        plhMovTierraInfracNoLineal.Focus();

    }

    protected void btnCancelarMovTierraInfracNoLineal_Click(object sender, EventArgs e)
    {
        plhMovTierraInfracNoLineal.Visible = false;

    }
    //Área de Disposición de material
    protected void btnNuevaAreaDispMaterial_Click(object sender, EventArgs e)
    {
        plhAreasDispoMatProy.Visible = true;
        plhAreasDispoMatProy.Focus();

    }

    protected void btnCancelarAreasDispoMatProy_Click(object sender, EventArgs e)
    {
        plhAreasDispoMatProy.Visible = false;

    }
    //Infraestructura y servicios interceptados por el proyecto
    protected void btnInfracIntercProy_Click(object sender, EventArgs e)
    {
        plhInfracIntercProy.Visible = true;
        plhInfracIntercProy.Focus();

    }


    protected void btnCancelarInfracIntercProy_Click(object sender, EventArgs e)
    {
        plhInfracIntercProy.Visible = false;

    }
    //Infraestructura y servicios interceptados por el proyecto
    protected void btnNuevoAsentInfrac_Click(object sender, EventArgs e)
    {
        plhAsentInfrac.Visible = true;
        plhAsentInfrac.Focus();

    }

    protected void btnCancelarAsentInfrac_Click(object sender, EventArgs e)
    {
        plhAsentInfrac.Visible = false;

    }

    protected void btnAgregarLocalizacion_Click(object sender, EventArgs e)
    {
        this.cargardslocalizaciones();
        this.cargarGrvLocalizaciones();
        this.agregarLocalizacion();
        cargardslocalizaciones();

    }

    protected void cboDeptoLocalizacion_SelectedIndexChanged(object sender, EventArgs e)
    {
        cargarComboMunicipio();
    }
    //Autoridad Ambiental con Jurisdicción en Area del Proyecto
    protected void btnAgregarAutorAmb_Click(object sender, EventArgs e)
    {
        cargarGrvAutoridadAmbiental();

        DataSet ds = new DataSet();
        List<SqlParameter> Parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
        par.Value = IDProyecto;
        Parametros.Add(par);
        Contexto.cargarTabla(Parametros, ds, "EIH_AUTORI_AMBIEN_JURISDIC_PROY");

        DataRow dr = ds.Tables[0].NewRow();
        dr["EAA_ID"] = this.cboAutoridadAmbiental.SelectedValue;
        dr["EIP_ID"] = IDProyecto;
        dr["EAJ_FECHA_CREACION"] = DateTime.Now;
        dr["EAJ_EXP_PROYECTO"] = this.txtExpedienteProyecto.Text;
        ds.Tables[0].Rows.Add(dr);
        Contexto.guardarTabla(ds, "EIH_AUTORI_AMBIEN_JURISDIC_PROY");
        cargarGrvAutoridadAmbiental();
    }    

    protected void grvAutoridadesAmbientales_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        cargarGrvAutoridadAmbiental();

        DataSet ds = new DataSet();
        List<SqlParameter> Parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
        par.Value = IDProyecto;
        Parametros.Add(par);
        
        Contexto.cargarTabla(Parametros, ds, "EIV_AUTORI_AMBIEN_JURISDIC_PROY");
        DataRow dr= ds.Tables[0].Rows[e.RowIndex];

        DataSet dsEliminar= new DataSet ();
        Parametros .Clear();
        par=new SqlParameter ("@EAJ_ID",SqlDbType .Int ,10,"EAJ_ID");
        par.Value =dr["EAJ_ID"];

        Contexto.cargarTabla(Parametros, dsEliminar, "EIH_AUTORI_AMBIEN_JURISDIC_PROY");
        dsEliminar.Tables [0].Rows[e.RowIndex].Delete();
 
        Contexto.guardarTabla (dsEliminar ,"EIH_AUTORI_AMBIEN_JURISDIC_PROY");
        cargarGrvAutoridadAmbiental();


        


        
    }

    protected void cboUnidLongInfracLineal_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.lblLongitudInfracLinealProy.Text = "Longitud(" + cboUnidLongInfracLineal.SelectedItem.Text + ")";
    }

    protected void cboUnidAreaInfracLineal_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.lblAreaIntervenida.Text = "Área Intervenida(" + cboUnidAreaInfracLineal.SelectedItem.Text + ")";
    }

    protected void btnAgregarInfracLinealProy_Click(object sender, EventArgs e)
    {
        if (this.cboDescInfracLinealProy.SelectedValue == "")
         {           
             return;
         }
        DataSet ds = new DataSet();
        List<SqlParameter> Parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
        par.Value = IDProyecto;
        Parametros.Add(par);
        Contexto.cargarTabla(Parametros, ds, "EIH_INFRA_LINEAL_PROYECTO");
        DataRow dr = ds.Tables[0].NewRow();
        //dr["EHL_ID"]=    
        if (cboDescInfracLinealProy.SelectedValue=="-1")
            dr["EDL_ID"] = DBNull.Value;
        else
            dr["EDL_ID"]=cboDescInfracLinealProy.SelectedValue ;
        if (this.txtOtraDescInfracLinealProy.Text == "")
            dr["EDL_OTRO"] = DBNull.Value;
        else
            dr["EDL_OTRO"] = this.txtOtraDescInfracLinealProy.Text;
        dr["EIP_ID"]=IDProyecto;
        dr["EHL_FECHA_CREACION"] = DateTime.Now;
        dr["EUA_ID"]= cboUnidAreaInfracLineal.SelectedValue ;     
        dr["EUL_ID"]= cboUnidLongInfracLineal.SelectedValue ;     
        dr["EHL_LONGITUD"]=txtLongitudInfracLinealProy.Text ;                            
        dr["EHL_AREA"]=txtAreaInfracLinealProy.Text ;                                
        dr["EHL_COOR_NORTE_INICIO"]= ctrCoorPtoInicialInfracLineal.CoorNorte ;                  
        dr["EHL_COOR_ESTE_INICIO"]=  ctrCoorPtoInicialInfracLineal.CoorEste ;                  
        dr["EHL_COOR_NORTE_FIN"]= ctrCoorPtoFinalInfracLineal.CoorNorte ;
        dr["EHL_COOR_ESTE_FIN"] = ctrCoorPtoFinalInfracLineal.CoorEste;
        ds.Tables[0].Rows.Add(dr);
        Contexto.guardarTabla (ds,"EIH_INFRA_LINEAL_PROYECTO");
        this.cargarGrvInfraestructuraLineal();

        
    }

   
    protected void grvInfracLinealProy_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataSet ds = new DataSet();
        List<SqlParameter> Parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
        par.Value = IDProyecto;
        Parametros.Add(par);
        Contexto.cargarTabla(Parametros, ds, "EIV_INFRA_LINEAL_PROYECTO");
        DataRow dr = ds.Tables[0].NewRow();
        dr = ds.Tables[0].Rows[e.RowIndex];

        Parametros .Clear ();
        par =new SqlParameter ("@EHL_ID",SqlDbType .Int ,10,"EHL_ID");
        par .Value=dr["EHL_ID"];
        Parametros .Add (par);

        ds = new DataSet();
        Contexto.cargarTabla(Parametros, ds, "EIh_INFRA_LINEAL_PROYECTO");
        ds.Tables[0].Rows[0].Delete();
        Contexto.guardarTabla(ds, "EIh_INFRA_LINEAL_PROYECTO");
        cargarGrvInfraestructuraLineal();

    

    }
    DataSet dsDatos;

    private void cargarInfraNoLineal()
    {
        dsDatos = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
        par.Value = IDProyecto;
        parametros.Add(par);
        Contexto.cargarTabla(parametros,dsDatos, "EIH_INFRA_NO_LINEAL_PROYECTO");
        
    }

    private void cargarGrvInfraNoLineal()
    {
        DataSet ds = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
        par.Value = IDProyecto;
        parametros.Add(par);
        Contexto.cargarTabla(parametros, ds, "EIV_INFRA_NO_LINEAL_PROYECTO");
        this.grvInfracNoLinealProy.DataSource = ds.Tables[0];
        this.grvInfracNoLinealProy.DataBind();
    
    }

    protected void btnAgregarInfranNoLineal_click(object sender, EventArgs e)
    {

        if (this.ctrCoorInfracNoLineal.Validar(1, 5))
        {
            DataTable dt = this.ctrCoorInfracNoLineal.Coordenadas;
            if (dt != null)
            {
                cargarInfraNoLineal();
                DataRow dr = dsDatos.Tables[0].NewRow();
                //dr["EHN_ID"]=    
                if (cboDescInfracNoLinealProy.SelectedValue == "-1")
                    dr["EDN_ID"] = DBNull.Value;
                else
                    dr["EDN_ID"] = cboDescInfracNoLinealProy.SelectedValue;

                if (this.txtOtraDescInfracNoLineal.Text=="")
                    dr["EDN_OTRO"] = DBNull.Value;
                else
                    dr["EDN_OTRO"] = this.txtOtraDescInfracNoLineal.Text;
                dr["EIP_ID"] = IDProyecto;
                dr["EHN_FECHA_CREACION"] = DateTime.Now;
                dr["EUA_ID"] = cboUnidAreaInfracNoLinealProy.SelectedValue;
                dr["EHN_AREA"] = txtAreaInfracNoLinealProy.Text;                
                dsDatos.Tables[0].Rows.Add(dr);

                Contexto.guardarTabla(dsDatos, "EIH_INFRA_NO_LINEAL_PROYECTO");
                cargarInfraNoLineal();

                int idPadre = int.Parse(dsDatos.Tables[0].Rows[dsDatos.Tables[0].Rows.Count - 1]["EHN_ID"].ToString());
                guardarPoligonosinfracNoLineal(idPadre);
                cargarGrvInfraNoLineal();
            }
            else
            {

            }
        }
        
    }
    protected void grvInfracNoLinealProy_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataSet ds = new DataSet();
        List<SqlParameter> Parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
        par.Value = IDProyecto;
        Parametros.Add(par);
        Contexto.cargarTabla(Parametros, ds, "EIV_INFRA_NO_LINEAL_PROYECTO");
        DataRow dr = ds.Tables[0].NewRow();
        dr = ds.Tables[0].Rows[e.RowIndex];


        string ehnId = dr["EHN_ID"].ToString();

        EliminarCoorNoLinealProy(ehnId);

        Parametros.Clear();
        par = new SqlParameter("@EHN_ID", SqlDbType.Int, 10, "EHN_ID");
        par.Value = int.Parse(ehnId);
        Parametros.Add(par);

        ds = new DataSet();
        Contexto.cargarTabla(Parametros, ds, "EIh_INFRA_NO_LINEAL_PROYECTO");
        ds.Tables[0].Rows[0].Delete();
        Contexto.guardarTabla(ds, "EIh_INFRA_NO_LINEAL_PROYECTO");
        cargarGrvInfraNoLineal();
    }

    private void EliminarCoorNoLinealProy(string ehnId)
    {
        cargarPoligonosinfracNoLineal(int.Parse(ehnId));
        DataTable tmp = dsDatos.Tables[0];
        int i=0;
        for (i=tmp.Rows.Count;i>=0;i--)
        {
            dsDatos.Tables[0].Rows[i].Delete();
        }
        Contexto.guardarTabla(dsDatos, "EIH_COOR_INFRAC_NO_LINEAL");    
    }

    private void guardarPoligonosinfracNoLineal(int idPadre)
    {
        cargarPoligonosinfracNoLineal(idPadre);
        foreach (DataRow dr in ctrCoorInfracNoLineal.Coordenadas.Rows)
        {
            DataRow dr2 = dsDatos.Tables[0].NewRow();
            //dr2["ECN_ID"]=      
            dr2["EHN_ID"]= idPadre;
            dr2["ECN_COOR_NORTE"] = dr["CoorNorte"].ToString();
            dr2["ECN_COOR_ESTE"] = dr["CoorEste"].ToString();
            dsDatos.Tables[0].Rows.Add(dr2);
        }
        Contexto.guardarTabla(dsDatos, "EIH_COOR_INFRAC_NO_LINEAL");    
    }

    private void cargarPoligonosinfracNoLineal(int idPadre)
    {
        dsDatos = new DataSet();

        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EHN_ID", SqlDbType.Int, 10, "EHN_ID");
        par.Value = idPadre;
        parametros.Add(par);
        Contexto.cargarTabla(parametros, dsDatos , "EIH_COOR_INFRAC_NO_LINEAL");
    }

    protected void btnAgregarMovTierrasInfracL_Click(object sender, EventArgs e)
    {
      
        DataSet ds = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
        par.Value = IDProyecto;
        parametros.Add(par);
        Contexto .cargarTabla (parametros,ds,"EIH_MOV_TIERRAS_INFRA_LINEAL");

        DataRow dr = ds.Tables [0].NewRow ();               
        //dr["EML_ID"]=      
        

        dr["EIL_ID"]=cboInfracAsocMovTierLineal .SelectedValue ;
        if (txtInfracAsocMovTierLineal.Text=="")
            dr["EIL_OTRO"] = DBNull.Value;      
        else
            dr["EIL_OTRO"] = txtInfracAsocMovTierLineal.Text;      
        dr["EIP_ID"]=IDProyecto;
        dr["EML_FECHA_CREACION"] =  DateTime.Now;
        dr["EML_MOV_TIERRAS_CORT"]=txtCorteMovTierLineal.Text ;
        dr["EML_MOV_TIERRAS_RELLENO"]= txtRellenoMovTierLineal.Text ;
        dr["EML_MOV_TIERRAS_MAT_SOBR"] = txtMatSobMovTierLineal.Text;
        ds.Tables[0].Rows.Add(dr);
        Contexto.guardarTabla(ds, "EIH_MOV_TIERRAS_INFRA_LINEAL");
        cargarGrvMovTierraInfracLinealProy();
    }
   
    protected void grvMovTierraInfracLinealProy_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {

    }

    protected void grvMovTierraInfracLinealProy_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataSet ds = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
        par.Value = IDProyecto;
        parametros.Add(par);
        Contexto.cargarTabla(parametros, ds, "EIV_MOV_TIERRAS_INFRA_LINEAL");
        DataRow dr = ds.Tables[0].NewRow();
        dr = ds.Tables[0].Rows[e.RowIndex];


        ds = new DataSet();
        parametros.Clear();
        par = new SqlParameter("@EML_ID", SqlDbType.Int, 10, "EML_ID");
        par.Value = dr["EML_ID"];
        parametros.Add(par);
        Contexto.cargarTabla(parametros, ds, "EIh_MOV_TIERRAS_INFRA_LINEAL");

        ds.Tables[0].Rows[0].Delete();
        Contexto.guardarTabla(ds, "EIh_MOV_TIERRAS_INFRA_LINEAL");
        cargarGrvMovTierraInfracLinealProy();
    }

    protected void btnAgregarMovTierrasInfracNoL_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
        par.Value = IDProyecto;
        parametros.Add(par);
        Contexto.cargarTabla(parametros, ds, "EIH_MOV_TIERRAS_INFRA_NOLINEAL");

        DataRow dr = ds.Tables[0].NewRow();        
       // dr["EMN_ID"]=      
        dr["EIN_ID"] = cboInfracAsocMovTierNoLineal.SelectedValue;
        if (txtInfracAsocMovTierNoLineal.Text == "")
            dr["EIN_OTRO"] = DBNull.Value;
        else
            dr["EIN_OTRO"] = txtInfracAsocMovTierNoLineal.Text;
        dr["EIP_ID"] = IDProyecto;
        dr["EMN_FECHA_CREACION"] = DateTime.Now;
        dr["EMN_MOV_TIERRAS_CORT"] = txtCorteMovTierNoLineal.Text;
        dr["EMN_MOV_TIERRAS_RELLENO"] = txtRellenoMovTierNoLineal.Text;
        dr["EMN_MOV_TIERRAS_MAT_SOBR"] = txtMatSobMovTierNoLineal.Text;
                
        ds.Tables[0].Rows.Add(dr);
        Contexto.guardarTabla(ds, "EIH_MOV_TIERRAS_INFRA_NOLINEAL");
        cargarGrvMovTierraInfracNoLinealProy();
    }   

    protected void grvMovTierraInfracNoLinealProy_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataSet ds = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
        par.Value = IDProyecto;
        parametros.Add(par);
        Contexto.cargarTabla(parametros, ds, "EIV_MOV_TIERRAS_INFRA_NOLINEAL");
        DataRow dr = ds.Tables[0].NewRow();
        dr = ds.Tables[0].Rows[e.RowIndex];


        ds = new DataSet();
        parametros.Clear();
        par = new SqlParameter("@EML_ID", SqlDbType.Int, 10, "EMN_ID");
        par.Value = dr["EMN_ID"];
        parametros.Add(par);
        Contexto.cargarTabla(parametros, ds, "EIH_MOV_TIERRAS_INFRA_NOLINEAL");

        ds.Tables[0].Rows[0].Delete();
        Contexto.guardarTabla(ds, "EIH_MOV_TIERRAS_INFRA_NOLINEAL");
        cargarGrvMovTierraInfracNoLinealProy();
    }

    protected void btnAgregarInfracIntercProy_Click(object sender, EventArgs e)
    {
        if (this.ctrCoorUbiInfracIntercProy.Valido)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> parametros = new List<SqlParameter>();
            SqlParameter par = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
            par.Value = IDProyecto;
            parametros.Add(par);
            Contexto.cargarTabla(parametros, ds, "EIh_INFRA_AFECT_PROYECTO");

            DataRow dr = ds.Tables[0].NewRow();
            //dr["EIA_ID"]=      
            dr["EII_ID"] = cboTipoInfracIntercProy.SelectedValue;
            dr["EII_OTRO"] = txtTipoInfracIntercProy.Text;
            dr["EIP_ID"] = IDProyecto;
            dr["EIA_FECHA_CREACION"] = DateTime.Now;
            dr["EUL_ID"] = 1;
            dr["EIA_LONGITUD"] = txtLongInfracIntercProy.Text;
            dr["EIA_ANCHO"] = txtAncInfracIntercProy.Text;
            dr["EIA_COOR_NORTE_UBICACION"] = ctrCoorUbiInfracIntercProy.CoorNorte;
            dr["EIA_COOR_ESTE_UBICACION"] = ctrCoorUbiInfracIntercProy.CoorEste;

            ds.Tables[0].Rows.Add(dr);
            Contexto.guardarTabla(ds, "EIh_INFRA_AFECT_PROYECTO");
            cargarGrvInfracIntercProy();
        }

    }

    private void cargarGrvInfracIntercProy()
    {
        DataSet ds = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
        par.Value = IDProyecto;
        parametros.Add(par);
        Contexto.cargarTabla(parametros, ds, "EIv_INFRA_AFECT_PROYECTO");
        grvInfracIntercProy.DataSource = ds.Tables[0];
        grvInfracIntercProy.DataBind();

    }

    protected void grvInfracIntercProy_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataSet ds = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
        par.Value = IDProyecto;
        parametros.Add(par);
        Contexto.cargarTabla(parametros, ds, "EIv_INFRA_AFECT_PROYECTO");

        DataRow dr = ds.Tables[0].NewRow();
        dr = ds.Tables[0].Rows[e.RowIndex];

        ds = new DataSet();
        parametros.Clear();
        par = new SqlParameter("@EIA_ID", SqlDbType.Int, 10, "EIA_ID");
        par.Value = dr["EIA_ID"];
        parametros.Add(par);
        Contexto.cargarTabla(parametros, ds, "EIh_INFRA_AFECT_PROYECTO");
        ds.Tables[0].Rows[0].Delete();
        Contexto.guardarTabla(ds,"EIh_INFRA_AFECT_PROYECTO");

        cargarGrvInfracIntercProy();
    }

    protected void cboUnidLongAsentInfrac_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(this.cboUnidLongAsentInfrac.SelectedValue!="-1")
            this.lbldisAsesnt.Text = "Distancia (" + this.cboUnidLongAsentInfrac.SelectedItem.Text+ "):";
    }

    protected void btnAgregarAsentInfrac_Click(object sender, EventArgs e)
    {
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
        par.Value = IDProyecto;
        parametros.Add(par);

        DataSet ds = new DataSet();
        Contexto.cargarTabla(parametros,ds, "EIH_ASENT_HUMANOS");
        DataRow dr = ds.Tables[0].NewRow();

        //dr["EAH_ID"]=      
        dr["EIP_ID"]=IDProyecto;
        dr["EUL_ID"] = cboUnidLongAsentInfrac.SelectedValue;  
        dr["EAH_NOMBRE"]=txtNombreAsentInfrac.Text ;                                         
        dr["EAH_DISTANCIA"]=txtDistanciaAsentInfrac.Text ;                                      
        dr["EAH_INTERVENCION"]=cboIntervenirAsentInfrac .SelectedItem .Text ;
        dr["EAH_AFECTADOS_PROYECTO"] = txtCercanAsentInfrac.Text;

        ds.Tables[0].Rows.Add(dr);
        Contexto.guardarTabla(ds, "EIH_ASENT_HUMANOS");
        cargarGrvAsentInfrac();

    }

    private void cargarGrvAsentInfrac()
    {
        DataSet ds = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
        par.Value = IDProyecto;
        parametros.Add(par);
        Contexto.cargarTabla(parametros, ds, "EIH_ASENT_HUMANOS");
        grvAsentInfrac.DataSource = ds.Tables[0];
        grvAsentInfrac.DataBind();
        
    }

    protected void grvAsentInfrac_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataSet ds = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
        par.Value = IDProyecto;
        parametros.Add(par);
        Contexto.cargarTabla(parametros, ds, "EIH_ASENT_HUMANOS");

        ds.Tables[0].Rows[e.RowIndex].Delete();
        Contexto.guardarTabla(ds,"EIH_ASENT_HUMANOS");

        cargarGrvAsentInfrac();
    }

    protected void btnAgregarAreasDispoMatProy_Click(object sender, EventArgs e)
    {
        if (this.ctrCoordenadasAreaMaterial.Validar(1, 5))
        {
            DataTable dt = this.ctrCoordenadasAreaMaterial.Coordenadas;
            if (dt != null)
            {
                cargarAreasDispoMatProy();
                DataRow dr = dsDatos.Tables[0].NewRow();
                //dr["EHN_ID"]=   
                // dr["EAD_ID"]=      
                dr["EIP_ID"] = IDProyecto;
                dr["EAD_FECHA_CREACION"] = DateTime.Now;
                dr["EAD_NOMBRE_AREA"] = txtNombAreasDispoMatProy.Text;
                dsDatos.Tables[0].Rows.Add(dr);

                Contexto.guardarTabla(dsDatos, "EIH_AREAS_DISP_MATERIALES");
                cargarAreasDispoMatProy();

                int idPadre = int.Parse(dsDatos.Tables[0].Rows[dsDatos.Tables[0].Rows.Count - 1]["EAD_ID"].ToString());
                guardarPoligonosAreasDispoMatProy(idPadre);
                cargarGrvAreasDispoMatProy();
            }
            else
            {

            }
        }
        
    }

    private void cargarGrvAreasDispoMatProy()
    {
        DataSet ds = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
        par.Value = IDProyecto;
        parametros.Add(par);
        Contexto.cargarTabla(parametros, ds, "EIH_AREAS_DISP_MATERIALES");
        this.grvAreasDispoMatProy.DataSource = ds.Tables[0];
        this.grvAreasDispoMatProy.DataBind();
    }

    private void guardarPoligonosAreasDispoMatProy(int idPadre)
    {
        cargarPoligonosAreasDispoMatProy(idPadre);
        foreach (DataRow dr in ctrCoordenadasAreaMaterial.Coordenadas .Rows )
        {
            DataRow dr2 = dsDatos.Tables[0].NewRow();
            //dr2["ECN_ID"]=      
            dr2["EAD_ID"] = idPadre;
            dr2["ECM_COOR_NORTE"] = dr["CoorNorte"].ToString();
            dr2["ECM_COOR_ESTE"] = dr["CoorEste"].ToString();
            dsDatos.Tables[0].Rows.Add(dr2);
        }
        Contexto.guardarTabla(dsDatos, "EIH_COOR_DISP_MATER"); 
    }

    private void cargarPoligonosAreasDispoMatProy(int idPadre)
    {
        dsDatos = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EAD_ID", SqlDbType.Int, 10, "EAD_ID");
        par.Value = idPadre;
        parametros.Add(par);
        Contexto.cargarTabla(parametros, dsDatos, "EIH_COOR_DISP_MATER");
    }

    private void cargarAreasDispoMatProy()
    {
        dsDatos = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
        par.Value = IDProyecto;
        parametros.Add(par);
        Contexto.cargarTabla(parametros, dsDatos , "EIH_AREAS_DISP_MATERIALES");

    }

   
    private DataSet CargarProyecto()
    {
        DataSet ds = new DataSet();
        List<SqlParameter> Parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
        par.Value = IDProyecto;
        Parametros.Add(par);
        Contexto.cargarTabla(Parametros, ds, "EIH_INFORMACION_PROYECTO");
        return ds;
    }

    private DataSet CargarCoordenadas()
    {
        DataSet ds = new DataSet();
        List<SqlParameter> Parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
        par.Value = IDProyecto;
        Parametros.Add(par);
        Contexto.cargarTabla(Parametros, ds, "EIH_COOR_INFO_PROYECTO");
        return ds;
    }

    protected void btnAsignarInforProy_Click(object sender, EventArgs e)
    {
        DataSet ds = CargarProyecto();
        DataRow dr = ds.Tables[0].Rows[0];
        dr["EIP_NOMBRE_PROYECTO"] = txtNombreProyecto.Text;
        dr["ESP_ID"] = cboSectoProductivo.SelectedValue;
        if (this.txtOtroSectorProductivo.Text=="")
            dr["ESP_OTRO"] = DBNull.Value;      
        else
            dr["ESP_OTRO"] = this.txtOtroSectorProductivo.Text;      
        dr["EIP_DIRECCION_PROYECTO"] = txtDireccionProyecto.Text;    
        Contexto.guardarTabla(ds, "EIH_INFORMACION_PROYECTO");
        
    }

    protected void btnAsifnarCoordenadas_Click(object sender, EventArgs e)
    {
        if (this.ctrCoorUbicacionProyecto.Validar(1, 5))
        {
            DataSet ds = CargarProyecto();
            DataRow dr = ds.Tables[0].Rows[0];
            dr["EOP_ID"] = cboOrigenCoordenadas.SelectedValue;
            dr["EET_ID"] = cboEscalaTrabajoCoord.SelectedValue;
            dr["EET_OTRO"] = txtOtraEscala.Text;
            Contexto.guardarTabla(ds, "EIH_INFORMACION_PROYECTO");

            DataSet ds2 = CargarCoordenadas();

            if (ds2.Tables[0].Rows.Count > 0)
            {
                DataTable Temp = ds2.Tables[0];
                int i=0;
                for (i = Temp.Rows.Count - 1; i >= 0; i--)
                {
                    ds2.Tables[0].Rows[i].Delete();
                }
                Contexto.guardarTabla(ds2, "EIH_COOR_INFO_PROYECTO");
            }

            foreach (DataRow row in this.ctrCoorUbicacionProyecto.Coordenadas.Rows)
            { 
                DataRow dr2 = ds2.Tables[0].NewRow();
                dr2["EIP_ID"] = IDProyecto;
                dr2["ECP_COOR_NORTE"] = row["CoorNorte"].ToString();
                dr2["ECP_COOR_ESTE"] = row["CoorEste"].ToString();
                ds2.Tables[0].Rows.Add(dr2);
            }
            Contexto.guardarTabla(ds2, "EIH_COOR_INFO_PROYECTO");
        }
    }
    protected void cboEscalaTrabajoCoord_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.lblOtraEscala.Visible = false;
        this.txtOtraEscala.Visible = false;
        this.txtOtraEscala.Text = "";
        this.rfvOtraescala.Visible = false;
        if (this.cboEscalaTrabajoCoord.SelectedItem.Text.Contains("Otro"))
        {
            this.lblOtraEscala.Visible = true;
            this.txtOtraEscala.Visible = true;            
            this.rfvOtraescala.Visible = true;
        }        

    }

    protected void cboDescInfracLinealProy_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.txtOtraDescInfracLinealProy.Visible = false;
        this.txtOtraDescInfracLinealProy.Text = "";
        if (this.cboDescInfracLinealProy.SelectedItem.Text.Contains("Otro"))
        {
            this.txtOtraDescInfracLinealProy.Visible = true;
        }
        if (this.cboDescInfracLinealProy.SelectedItem.Text.Contains("otro"))
        {
            this.txtOtraDescInfracLinealProy.Visible = true;
        }
    }
    protected void cboDescInfracNoLinealProy_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.txtOtraDescInfracNoLineal.Text = "";
        this.txtOtraDescInfracNoLineal.Visible = false;
        if(this.cboDescInfracNoLinealProy.SelectedItem.Text.Contains("Otro"))
            this.txtOtraDescInfracNoLineal.Visible = true;
        if (this.cboDescInfracNoLinealProy.SelectedItem.Text.Contains("otro"))
            this.txtOtraDescInfracNoLineal.Visible = true;
    }

    protected void cboUnidAreaInfracNoLinealProy_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.cboUnidAreaInfracNoLinealProy.SelectedValue!="-1")
            this.Label42.Text = "Área Intervenida(" + this.cboUnidAreaInfracNoLinealProy.SelectedItem.Text + "):";
    }
    protected void cboInfracAsocMovTierLineal_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.txtInfracAsocMovTierLineal.Text="";
        this.txtInfracAsocMovTierLineal.Visible=false;
        if(this.cboInfracAsocMovTierLineal.SelectedItem.Text.Contains("Otro"))
            this.txtInfracAsocMovTierLineal.Visible = true;
        if (this.cboInfracAsocMovTierLineal.SelectedItem.Text.Contains("otro"))
            this.txtInfracAsocMovTierLineal.Visible = true;

    }
    protected void btnAsignarVal_Click(object sender, EventArgs e)
    {

        DataSet ds = CargarProyecto();
        DataRow dr = ds.Tables[0].Rows[0];         
        dr["EIP_VOL_TOTAL_CORTE"] = txtVolTotCorteMovTierra.Text;
        dr["EIP_VOL_TOTAL_RELLENO"] = txtVolTotRellenoMovTierra.Text;
        dr["EIP_MAT_TOTAL_SOBRANTE"] = txtVolTotSobranteMovTierra.Text;    
        Contexto.guardarTabla(ds, "EIH_INFORMACION_PROYECTO");
    }
    protected void cboTipoInfracIntercProy_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.txtTipoInfracIntercProy.Text = "";
        this.txtTipoInfracIntercProy.Visible = false;
        if (this.cboTipoInfracIntercProy.SelectedItem.Text.Contains("Otro"))
            this.txtTipoInfracIntercProy.Visible = true;
    }
    protected void btnAsignarVal3_Click(object sender, EventArgs e)
    {
        DataSet ds = CargarProyecto();
        DataRow dr = ds.Tables[0].Rows[0];
        dr["EIP_CANT_MANO_OBRA_CALIFICADA"] = txtCantManoObraCalf.Text;
        dr["EIP_CANT_MANO_OBRA_NOCALIFICADA"] = txtCantManoObraNoCalf.Text;
        Contexto.guardarTabla(ds, "EIH_INFORMACION_PROYECTO");
    }
    protected void btnAsignarVal4_Click(object sender, EventArgs e)
    {
        DataSet ds = CargarProyecto();
        DataRow dr = ds.Tables[0].Rows[0];
        dr["EIP_COSTO_PROYECTO"] = txtCostoProyecto.Text;
        dr["EIP_DURACION_PROYECTO"] = txtDuracionProyecto.Text;
        dr["EUT_ID"] = cboUnidadDurProyecto.SelectedValue;
        dr["EIP_MONTO_BASE_INVERSION"] = txtCostoBaseInversion.Text;
        dr["EET_ID"] = this.cboEscalaTrabajoCoord.SelectedValue;
        Contexto.guardarTabla(ds, "EIH_INFORMACION_PROYECTO");
    }
   
}
