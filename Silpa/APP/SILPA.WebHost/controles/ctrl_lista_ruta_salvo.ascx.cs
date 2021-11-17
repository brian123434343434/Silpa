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

public partial class controles_ctrl_lista_ruta_salvo : System.Web.UI.UserControl
{
    #region Atributos

    #region Documentacion Atributo
    /// <summary>
    /// DataSet con los datos a cargar en los combos
    /// </summary>
    #endregion
    protected DataSet ds_data;
    #region Documentación Atributo
    /// <summary>
    ///     Objeto de configuracion
    /// </summary>
    #endregion
   

    /// <summary>
    /// Identificador del Expediente
    /// </summary>
    protected Int32 exp_id;


   
    #endregion

    #region Carga de pagina

    #region Documentacion Metodo
    /// <summary>
    /// Inicializa los parametros y controles de la página
    /// </summary>
    #endregion
    protected void Page_Load(object sender, System.EventArgs e)
    {
        this.iniciarPagina();
        if (!IsPostBack)
        {
            this.generarPagina();
        }
    }
    #region Documentacion Metodo
    /// <summary>
    /// Metodo que asegura la persistencia de algunos de los datos y de los procedimientos de la pagina
    /// </summary>
    #endregion
    private void iniciarPagina()
    {
        //Configuración
        //this.oConfig = new Configuracion();
        //this.oConfig = (Configuracion)Session["oConfig"];
        //this.oLB = new ListasBasicas(this.oConfig);
    }

    #region Documentacion Metodo
    /// <summary>
    /// Metodo que se encarga de inicializar los controles la primera vez que se carga la página
    /// </summary>
    #endregion
    private void generarPagina()
    {
        //Inicializa el DataSet
        //this.ds_data = new DataSet();
        //cargar combos
        //this.cargarDepartamento();
        //this.cargarArea();
    }

    #region Documentacion Metodo
    /// <summary>
    /// Carga el listado de items padre
    /// </summary>
    #endregion
    private void cargarDepartamento()
    {
        //Carga de los datos del departamento en la lista
        //this.ddl_departamento.DataSource = this.oLB.consultarDepartamento(-1);
        //this.ddl_departamento.DataTextField = "DEP_NOMBRE";
        //this.ddl_departamento.DataValueField = "DEP_ID";
        //this.ddl_departamento.DataBind();
        //this.ddl_departamento.Items.Insert(0, new ListItem("Seleccione.....", "-1"));

        ////inicializa los demas combos
        //this.ddl_municipio.Items.Clear();
        //this.ddl_corregimiento.Items.Clear();
        //this.ddl_vereda.Items.Clear();
        //this.ddl_municipio.Items.Insert(0, new ListItem("Seleccione....", "-1"));
        //this.ddl_corregimiento.Items.Insert(0, new ListItem("Seleccione....", "-1"));
        //this.ddl_vereda.Items.Insert(0, new ListItem("Seleccione....", "-1"));
    }

    #region Documentacion Metodo
    /// <summary>
    /// Carga el listado de items hijo
    /// </summary>
    #endregion
    private void cargarMunicipio()
    {
        //Carga el listado de items municipio consultados
        //this.ddl_municipio.DataSource = this.oLB.consultarMunicipio(Convert.ToInt32(this.ddl_departamento.SelectedValue), -1);
        //this.ddl_municipio.DataTextField = "MUN_NOMBRE";
        //this.ddl_municipio.DataValueField = "MUN_ID";
        //this.ddl_municipio.DataBind();
        //this.ddl_municipio.Items.Insert(0, new ListItem("Seleccione.....", "-1"));

        ////inicializa los demas combos
        //this.ddl_corregimiento.Items.Clear();
        //this.ddl_vereda.Items.Clear();
        //this.ddl_corregimiento.Items.Insert(0, new ListItem("Seleccione....", "-1"));
        //this.ddl_vereda.Items.Insert(0, new ListItem("Seleccione....", "-1"));
    }
    //cargar corregimientos 
    //private void cargarCorregimiento()
    //{
    //    //Carga el listado de items corregimientos consultados
    //    this.ddl_corregimiento.DataSource = this.oLB.consultarCorregimiento(Convert.ToInt32(this.ddl_municipio.SelectedValue), -1);
    //    this.ddl_corregimiento.DataTextField = "COR_NOMBRE";
    //    this.ddl_corregimiento.DataValueField = "COR_ID";
    //    this.ddl_corregimiento.DataBind();
    //    this.ddl_corregimiento.Items.Insert(0, new ListItem("Seleccione.....", "-1"));
    //}
    //Cargar veredas
    //private void cargarVereda()
    //{
    //    //Carga el listado de items vereda consultados
    //    this.ddl_vereda.DataSource = this.oLB.consultarVereda(Convert.ToInt32(this.ddl_municipio.SelectedValue), Convert.ToInt32(this.ddl_corregimiento.SelectedValue), -1);
    //    this.ddl_vereda.DataTextField = "VER_NOMBRE";
    //    this.ddl_vereda.DataValueField = "VER_ID";
    //    this.ddl_vereda.DataBind();
    //    this.ddl_vereda.Items.Insert(0, new ListItem("Seleccione.....", "-1"));
    //}
    //Cargar area hidrografica
    //private void cargarArea()
    //{
    //    //Carga el listado de items hijo consultados
    //    this.ddl_area.DataSource = this.oLB.consultarArea();
    //    this.ddl_area.DataTextField = "AHI_NOMBRE";
    //    this.ddl_area.DataValueField = "AHI_ID";
    //    this.ddl_area.DataBind();
    //    this.ddl_area.Items.Insert(0, new ListItem("Seleccione.....", "-1"));

    //    this.ddl_zona.Items.Clear();
    //    this.ddl_subzona.Items.Clear();
    //    this.ddl_zona.Items.Insert(0, new ListItem("Seleccione....", "-1"));
    //    this.ddl_subzona.Items.Insert(0, new ListItem("Seleccione....", "-1"));
    //}
    //Cargar zona
    //private void cargarZona()
    //{
    //    //Carga el listado de items hijo consultados
    //    this.ddl_zona.DataSource = this.oLB.consultarZona(Convert.ToInt32(this.ddl_area.SelectedValue));
    //    this.ddl_zona.DataTextField = "ZHI_NOMBRE";
    //    this.ddl_zona.DataValueField = "ZHI_ID";
    //    this.ddl_zona.DataBind();
    //    this.ddl_zona.Items.Insert(0, new ListItem("Seleccione.....", "-1"));

    //    this.ddl_subzona.Items.Clear();
    //    this.ddl_subzona.Items.Insert(0, new ListItem("Seleccione....", "-1"));
    //}
    //Cargar zona
    //private void cargarSubzona()
    //{
    //    //Carga el listado de items hijo consultados
    //    this.ddl_subzona.DataSource = this.oLB.consultarSubzona(Convert.ToInt32(this.ddl_zona.SelectedValue));
    //    this.ddl_subzona.DataTextField = "SHI_NOMBRE";
    //    this.ddl_subzona.DataValueField = "SHI_ID";
    //    this.ddl_subzona.DataBind();
    //    this.ddl_subzona.Items.Insert(0, new ListItem("Seleccione.....", "-1"));
    //}

    #endregion

    #region Metodos
    #region Documentacion Metodo
    /// <summary>
    /// Establece focus sobre un control
    /// </summary>
    #endregion
    private void SetFocusControl(string ControlName)
    {
        string script = "<script language=\"javascript\">var control = document.getElementById(\"" + ControlName + "\"); if( control != null ){control.focus();}</script>";
        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "Focus", script);
    }
    #endregion

    #region Eventos de controles

    #region Documentacion Metodo
    /// <summary>
    /// Evento click sobre el boton adicionar (+)
    /// </summary>
    #endregion
    protected void cmi_adicionar_Click(object sender, ImageClickEventArgs e)
    {
        string str_valor = string.Empty;
        string str_nombre = string.Empty;

        if (this.ddl_departamento.SelectedItem.Value != "-1")
        {
            str_valor = "D" + this.ddl_departamento.SelectedItem.Value;
            str_nombre = this.ddl_departamento.SelectedItem.Text;
        }
        else
        {
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "Repetido", "<script>alert('Debe seleccionar el departamento!');</script>");
            return;
        }

        if (this.ddl_municipio.SelectedItem.Value != "-1")
        {
            str_valor = str_valor + "_M" + this.ddl_municipio.SelectedItem.Value;
            str_nombre = str_nombre + ", " + this.ddl_municipio.SelectedItem.Text;
        }
        else
        {
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "Repetido", "<script>alert('Debe seleccionar el municipio!');</script>");
            return;
        }

        if (this.ddl_corregimiento.SelectedItem.Value != "-1")
        {
            str_valor = str_valor + "_C" + this.ddl_corregimiento.SelectedItem.Value;
            str_nombre = str_nombre + ", " + this.ddl_corregimiento.SelectedItem.Text;
        }
        if (this.ddl_vereda.SelectedItem.Value != "-1")
        {
            str_valor = str_valor + "_V" + this.ddl_vereda.SelectedItem.Value;
            str_nombre = str_nombre + ", " + this.ddl_vereda.SelectedItem.Text;
        }
        if (this.ddl_area.SelectedItem.Value != "-1")
        {
            str_valor = str_valor + "_A" + this.ddl_area.SelectedItem.Value;
            str_nombre = str_nombre + ", " + this.ddl_area.SelectedItem.Text;
        }
        if (this.ddl_zona.SelectedItem.Value != "-1")
        {
            str_valor = str_valor + "_Z" + this.ddl_zona.SelectedItem.Value;
            str_nombre = str_nombre + ", " + this.ddl_zona.SelectedItem.Text;
        }
        if (this.ddl_subzona.SelectedItem.Value != "-1")
        {
            str_valor = str_valor + "_S" + this.ddl_subzona.SelectedItem.Value;
            str_nombre = str_nombre + ", " + this.ddl_subzona.SelectedItem.Text;
        }

       this.lst_mun.Items.Add(new ListItem(str_nombre, str_valor));
        // Focus
        this.SetFocusControl(this.lst_mun.ClientID);
    }

    #region Documentacion Metodo
    /// <summary>
    /// Evento de click sobre el boton de retirar
    /// </summary>
    #endregion
    protected void cmi_retirar_Click(object sender, ImageClickEventArgs e)
    {
        bool bln_borrar = true;
        while (bln_borrar)
        {
            bln_borrar = false;
            //Recorre el listbox buscando el item seleccionado para removerlo
            foreach (ListItem li in this.lst_mun.Items)
            {
                if (li.Selected)
                {
                    //Remueve el item seleccionado
                    this.lst_mun.Items.Remove(li);
                    bln_borrar = true;
                    break;
                }
            }
        }

        // Focus
        this.SetFocusControl(this.lst_mun.ClientID);


    }

    #region Documentacion Metodo
    /// <summary>
    /// Evento de click sobre el boton subir
    /// </summary>
    #endregion
    protected void cmi_subir_Click(object sender, ImageClickEventArgs e)
    {
        if (this.lst_mun.SelectedIndex > 0)
        {
            //Variables auxiliares
            string str_text, str_value;
            //Guarda los valores del items seleccionado en la variables auxiliares
            str_text = this.lst_mun.Items[this.lst_mun.SelectedIndex].Text;
            str_value = this.lst_mun.Items[this.lst_mun.SelectedIndex].Value;

            //Cambia el valor del item seleccionado por el del anterior en la lista
            this.lst_mun.Items[this.lst_mun.SelectedIndex].Text = this.lst_mun.Items[this.lst_mun.SelectedIndex - 1].Text;
            this.lst_mun.Items[this.lst_mun.SelectedIndex].Value = this.lst_mun.Items[this.lst_mun.SelectedIndex - 1].Value;

            //Cambia el valor del item siguiente por el contenido en las variables temporale
            this.lst_mun.Items[this.lst_mun.SelectedIndex - 1].Text = str_text;
            this.lst_mun.Items[this.lst_mun.SelectedIndex - 1].Value = str_value;

        }
    }

    #region Documentacion Metodo
    /// <summary>
    /// Evento de click sobre el boton bajar
    /// </summary>
    #endregion
    protected void cmi_bajar_Click(object sender, ImageClickEventArgs e)
    {
        if (this.lst_mun.SelectedIndex < this.lst_mun.Items.Count - 1)
        {
            //Variables temporales
            string str_text, str_value;
            //Almacena en las variables tmporales el valor del item seleccionado
            str_text = this.lst_mun.Items[this.lst_mun.SelectedIndex].Text;
            str_value = this.lst_mun.Items[this.lst_mun.SelectedIndex].Value;

            //Cambia el valor del item seleccionado por el del siguiente en la lista
            this.lst_mun.Items[this.lst_mun.SelectedIndex].Text = this.lst_mun.Items[this.lst_mun.SelectedIndex + 1].Text;
            this.lst_mun.Items[this.lst_mun.SelectedIndex].Value = this.lst_mun.Items[this.lst_mun.SelectedIndex + 1].Value;

            //Cambia el valor del siguiente item en la lista por el de las variables auxiliares
            this.lst_mun.Items[this.lst_mun.SelectedIndex + 1].Text = str_text;
            this.lst_mun.Items[this.lst_mun.SelectedIndex + 1].Value = str_value;

        }
    }

    #endregion

    #region Propiedades


    public Int32 ExpId
    {
        set
        {
            this.exp_id = value;
        }
        get
        {
            return this.exp_id;
        }
    }

    #region Documentacion Propiedad
    /// <summary>
    ///  Obtiene o establece el DataSet con los datos a listar en los dropdownlist
    /// </summary>
    /// <remarks>
    ///		<strong>Consideraciones</strong>
    /// </remarks>
    #endregion
    public DataSet DobleSource
    {
        set
        {
            this.ds_data = value;
            this.ds_data.EnforceConstraints = true;
        }
        get { return this.ds_data; }
    }


    #region Documentacion Propiedad
    /// <summary>
    ///  Obtiene el listbox del control
    /// </summary>
    #endregion
    public ListBox Selecciones
    {
        get
        {
            return this.lst_mun;
        }
        set
        {
            this.lst_mun = value;
        }
    }

    #region Documentacion Propiedad
    /// <summary>
    ///  Obtiene o establece el atributo que determina si el campo hijo es requerido
    /// </summary>
    #endregion
    public bool Validador
    {
        set { this.rfv_val_items.Enabled = value; }
        get { return this.rfv_val_items.Enabled; }
    }


    public DropDownList DepId
    {
        get
        {
            return this.ddl_departamento;
        }
        set
        {
            this.ddl_departamento = value;
        }
    }


    public DropDownList MunId
    {
        get
        {
            return this.ddl_municipio;
        }
        set
        {
            this.ddl_municipio = value;
        }
    }

    public DropDownList CorId
    {
        get
        {
            return this.ddl_corregimiento;
        }
        set
        {
            this.ddl_corregimiento = value;
        }
    }

    public DropDownList VerId
    {
        get
        {
            return this.ddl_vereda;
        }
        set
        {
            this.ddl_vereda = value;
        }
    }


    public DropDownList CueId
    {
        get
        {
            return this.ddl_area;
        }
        set
        {
            this.ddl_area = value;
        }
    }



    #endregion


    protected void ddl_corregimiento_SelectedIndexChanged(object sender, EventArgs e)
    {
        //this.cargarVereda();
    }
    protected void ddl_departamento_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.cargarMunicipio();
    }
    protected void ddl_municipio_SelectedIndexChanged(object sender, EventArgs e)
    {
        //this.cargarCorregimiento();
        //this.cargarVereda();
        //this.cargarArea();
    }
    protected void ddl_area_SelectedIndexChanged(object sender, EventArgs e)
    {
        //this.cargarZona();
    }
    protected void ddl_zona_SelectedIndexChanged(object sender, EventArgs e)
    {
        //this.cargarSubzona();
    }

    public void QuitarHidrografia(bool vis)
    {
        this.trAreaH.Visible = vis;
        this.trZona.Visible = vis;
        this.trSubzona.Visible = vis;

    }
}
