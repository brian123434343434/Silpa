#region NameSpaces fijos
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

#region Documentacion Clase
/// <summary>
/// Clase para el manejo del controlde usuario ctrl_lista_combo
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
///				<term>Julio César Cruz Fajardo</term>
///				<term>30/05/2006</term>
///				<term>1.0</term>
///				<term>Creación de la clase</term>
///			</item>
///		</list>
/// </remarks>
#endregion

public partial class controles_ctrl_lista_combo : System.Web.UI.UserControl
{
    #region Atributos

    #region Documentacion Atributo
    /// <summary>
    /// DataSet con los datos a cargar en los combos
    /// </summary>
    #endregion
    protected DataSet ds_data;

    #region Documentacion Atributo
    /// <summary>
    /// Almacena el nombre del campo del dataset en el que se almacena el id
    /// </summary>
    #endregion
    protected string str_campo_id = "ID";

    #region Documentacion Atributo
    /// <summary>
    /// Almacena el nombre del campo del dataset en el que se almacena el texto
    /// </summary>
    #endregion
    protected string str_campo_texto = "TEXTO";

    #region Documentacion Atributo
    /// <summary>
    /// Indica si el control esta habilitado
    /// </summary>
    #endregion
    protected bool bln_enabled = true;

    #region Documentacion
    /// <summary>
    /// Evento disparado cuando se cambia el contenido del ListBox de Selecciones
    /// </summary>
    #endregion
    public event System.EventHandler SeleccionesBound;
  
    #endregion

    #region Carga de pagina

    #region Documentacion Metodo
    /// <summary>
    /// Inicializa los parametros y controles de la página
    /// </summary>
    #endregion
    protected virtual void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
        {
            this.generarPagina();
        }
  //hava:05-ABR-10
        //this.oConfig = (Configuracion)Session["oConfig"];
    }

    #region Documentacion Metodo
    /// <summary>
    /// Metodo que se encarga de inicializar los controles la primera vez que se carga la página
    /// </summary>
    #endregion
    private void generarPagina()
    {
        //carga los registros del dataset en el combo
        this.cargarCombo();
        this.habilitar();
        //hava:31-mar-10
        //this.oConfig = (Configuracion)Session["oConfig"];
    }

    #region Documentacion Metodo
    /// <summary>
    /// Carga el listado de items en el combo
    /// </summary>
    #endregion
    private void cargarCombo()
    {
        if (this.ds_data != null)
            if (this.ds_data.Tables.Count > 0)
            {
                //Carga de los datos del datasource en la lista
                this.lst_combo.DataSource = this.ds_data.Tables[0];
                this.lst_combo.DataTextField = this.str_campo_texto;
                this.lst_combo.DataValueField = this.str_campo_id;
                this.lst_combo.DataBind();
                this.lst_combo.Items.Insert(0, new ListItem("Seleccione.....", "-1"));
            }
    }

    #region Documentacion Metodo
    /// <summary>
    /// Habilita los controles dependiendo de la propiedad Enabled
    /// </summary>
    #endregion
    private void habilitar()
    {
        this.lst_combo.Enabled = this.bln_enabled;
        this.cmi_adicionar.Enabled = this.bln_enabled;
        this.cmi_retirar.Enabled = this.bln_enabled;
        this.cmi_subir.Enabled = this.bln_enabled;
        this.cmi_bajar.Enabled = this.bln_enabled;
        this.lbx_items.Enabled = this.bln_enabled;
    }
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
        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(),"Focus", script);
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
        bool bln_adicionar = true;

        // Mira si es un hijo
        if (this.lst_combo.Items.Count > 0)
        {
            // Verifica si esta repetido				
            foreach (ListItem li in this.lbx_items.Items)
            {
                if (li.Value == this.lst_combo.SelectedValue)
                {
                    bln_adicionar = false;
                    break;
                }
            }
            if (bln_adicionar)
            {
                //if(Int32.Parse(this.lst_combo.SelectedValue)>-1)
                //this.lbx_items.Items.Add(new ListItem(this.lst_combo.SelectedItem.Text, this.lst_combo.SelectedValue));
                //this.txt_items.Text = this.txt_items.Text + "," + this.lst_combo.SelectedValue;
                //this.OnBound(sender);

                // hava:31-mar-10
                //string valor = string.Empty;
                //if (Int32.Parse(this.lst_combo.SelectedValue) > -1) 
                //{
                //    valor = this.lst_combo.SelectedItem.Text + " ( " + this.oConfig.Nombre_AA+ " )";
                //    this.lbx_items.Items.Add(new ListItem(valor, this.lst_combo.SelectedValue));
                //}
                //this.txt_items.Text = this.txt_items.Text + "," + this.lst_combo.SelectedValue;
                //this.OnBound(sender);
            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(),"Repetido", "<script>alert('Este item ya ha sido adicionado!');</script>");
            }
        }

        // Focus
        this.SetFocusControl(this.lbx_items.ClientID);
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
            foreach (ListItem li in this.lbx_items.Items)
            {
                if (li.Selected)
                {
                    //Remueve el item seleccionado
                    this.lbx_items.Items.Remove(li);
                    bln_borrar = true;
                    break;
                }
            }
        }
        if (this.lbx_items.Items.Count < 1)
            this.txt_items.Text = "";
        // Focus
        this.SetFocusControl(this.lbx_items.ClientID);
        this.OnBound(sender);
    }

    #region Documentacion Metodo
    /// <summary>
    /// Evento de click sobre el boton subir
    /// </summary>
    #endregion
    protected void cmi_subir_Click(object sender, ImageClickEventArgs e)
    {
        if (this.lbx_items.SelectedIndex > 0)
        {
            //Variables auxiliares
            string str_text, str_value;
            //Guarda los valores del items seleccionado en la variables auxiliares
            str_text = this.lbx_items.Items[this.lbx_items.SelectedIndex].Text;
            str_value = this.lbx_items.Items[this.lbx_items.SelectedIndex].Value;

            //Cambia el valor del item seleccionado por el del anterior en la lista
            this.lbx_items.Items[this.lbx_items.SelectedIndex].Text = this.lbx_items.Items[this.lbx_items.SelectedIndex - 1].Text;
            this.lbx_items.Items[this.lbx_items.SelectedIndex].Value = this.lbx_items.Items[this.lbx_items.SelectedIndex - 1].Value;

            //Cambia el valor del item siguiente por el contenido en las variables temporale
            this.lbx_items.Items[this.lbx_items.SelectedIndex - 1].Text = str_text;
            this.lbx_items.Items[this.lbx_items.SelectedIndex - 1].Value = str_value;

        }
    }

    #region Documentacion Metodo
    /// <summary>
    /// Evento de click sobre el boton bajar
    /// </summary>
    #endregion
    protected void cmi_bajar_Click(object sender, ImageClickEventArgs e)
    {
        if (this.lbx_items.Items.Count > 1)
        {
            if (this.lbx_items.SelectedIndex < this.lbx_items.Items.Count - 1)
            {
                //Variables temporales
                string str_text, str_value;
                //Almacena en las variables tmporales el valor del item seleccionado
                str_text = this.lbx_items.Items[this.lbx_items.SelectedIndex].Text;
                str_value = this.lbx_items.Items[this.lbx_items.SelectedIndex].Value;

                //Cambia el valor del item seleccionado por el del siguiente en la lista
                this.lbx_items.Items[this.lbx_items.SelectedIndex].Text = this.lbx_items.Items[this.lbx_items.SelectedIndex + 1].Text;
                this.lbx_items.Items[this.lbx_items.SelectedIndex].Value = this.lbx_items.Items[this.lbx_items.SelectedIndex + 1].Value;

                //Cambia el valor del siguiente item en la lista por el de las variables auxiliares
                this.lbx_items.Items[this.lbx_items.SelectedIndex + 1].Text = str_text;
                this.lbx_items.Items[this.lbx_items.SelectedIndex + 1].Value = str_value;

            }
        }
    }

    #region Documentacion
    /// <summary> 
    /// Funcion llamada cuando se dispara el evento Databound del Listbox Seleccion 
    /// </summary> 
    #endregion
    protected virtual void OnBound(object sender)
    {
        // Dispara el evento Selecciones Bound 
        if (this.SeleccionesBound != null)
            this.SeleccionesBound(sender, new EventArgs());
    }
    #endregion

    #region Propiedades

    #region Documentacion Propiedad
    /// <summary>
    ///  Obtiene o establece el DataSet con los datos a listar en el dropdownlist
    /// </summary>
    /// <remarks>
    ///		<strong>Consideraciones</strong>
    /// </remarks>
    #endregion
    public DataSet Datos
    {
        set
        {
            this.ds_data = value;
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
            return this.lbx_items;
        }
        set
        {
            this.lbx_items = value;
        }
    }

    #region Documentacion Propiedad
    /// <summary>
    ///  Obtiene o establece el atributo que determina si el elemento es requerido
    /// </summary>
    #endregion
    public bool Validador
    {
        set 
        { 
            this.rfv_val_items.Enabled = value;
            this.rfv_val_items_count.Enabled = value;
        }
        get { return this.rfv_val_items.Enabled; }
    }

    #region Documentacion Propiedad
    /// <summary>
    ///  Obtiene o establece el atributo que determina si el elemento es requerido
    /// </summary>
    #endregion
    public string  GrupoValidador
    {
        set{this.rfv_val_items.ValidationGroup = value;}
        get { return this.rfv_val_items.ValidationGroup; }
    }


    #region Documentacion Propiedad
    /// <summary>
    ///  Almacena el nombre del campo identificador del dataset
    /// </summary>
    #endregion
    public string CampoID
    {
        get { return this.str_campo_id; }
        set { this.str_campo_id = value; }
    }

    #region Documentacion Propiedad
    /// <summary>
    ///  Almacena el nombre del campo texto del dataset
    /// </summary>
    #endregion
    public string CampoTexto
    {
        get { return this.str_campo_texto; }
        set { this.str_campo_texto = value; }
    }

    #region Documentacion Propiedad
    /// <summary>
    ///  Obtiene o establece el atributo que determina si el control esta habilitado
    /// </summary>
    #endregion
    public bool Enabled
    {
        set { this.bln_enabled = value; }
        get { return this.bln_enabled; }
    }

    #region Documentacion Propiedad
    /// <summary>
    ///  Obtiene o establece el atributo que determina si el control esta habilitado
    /// </summary>
    #endregion
    public DropDownList Combo
    {
        get { return this.lst_combo; }
    }
    #endregion

    /// <summary>
    /// Evento DataBound del list box 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbx_items_DataBound(object sender, EventArgs e)
    {
        this.txt_items.Text = this.lbx_items.Items.Count.ToString();
    }
}
