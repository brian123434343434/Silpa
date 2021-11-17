using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using SoftManagement.Persistencia;

/// <summary>
/// Control para ingreso de grupos de coordenadas.
/// Retorna un DataTable con las coordenadas.
/// Se debe definir el número de coordenadas máximas que aceptará el control.
/// Si no se define este valor el control no validará la cantidad de coordenadas recibidas.
/// </summary>
public partial class ResumenEIA_Controles_ctrCoordenadas : System.Web.UI.UserControl
{
    protected DataTable _coordenadas;
    protected Int32 _noCoordenadas;
    DataSet dsCoordenadas = new DataSet();
    bool _dataGridObject;    
    string _nombreCampo;
    static string paramPrueba;

    /// <summary>
    /// Número máximo de coordenadas que pueden ser agregadas
    /// </summary>
    public Int32 NoCoordenadas
    {
        get
        {
            return (Int32)ViewState["noCoordenadas"];
        }
        set
        {
            ViewState["noCoordenadas"] = value;
        }
    }

    public bool DataGridObject
    {
        get
        {
            return _dataGridObject;
        }
        set
        {
           _dataGridObject  = value;
        }
    }

    public DataSet TablaValores
    {
        get { return (DataSet)ViewState ["tablaValores"]; }
        set { ViewState["tablaValores"] = value; }
    }

    public string NombreTabla
    {
        get { return (string)ViewState["NombreTabla"];}
        set { ViewState["NombreTabla"] = value; }
    }

    public string nombreParametro
    {
        get { return (string)ViewState["nombreParametro"]; }
        set { ViewState["nombreParametro"] = value; }
    }
    
    public string NombreCampo
    {
        get
        {
            return _nombreCampo;
        }
        set
        {
            _nombreCampo = value;
        }
    }

    public string ValorCampo
    {
        get
        {
            return (String)ViewState["ctrValorCampo"] ;        
        }
        set
        {
            ViewState["ctrValorCampo"] = value;                
        }
    }

    public string ValorCampo2
    {
        get
        {
            return paramPrueba;            
        }
        set
        {            
            paramPrueba = value;
            if (DataGridObject)
            {
                if (ValorCampo != null)
                {
                    CargarDataGrid();
                }
            }
        }
    }

    public DataTable Coordenadas
    {
        get
        {
            dsCoordenadas = (DataSet)ViewState["dsCoordenadas"];
            if (dsCoordenadas != null)
            {               
                if (dsCoordenadas.Tables.Count == 0)
                    return null;
                else
                    return dsCoordenadas.Tables[0];
            }
            else
                return null;
        }
        set
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(value);
            ViewState["dsCoordenadas"] = ds;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {   
        if (!IsPostBack)
        {
            ViewState.Add("dsCoordenadas", dsCoordenadas);
            cambValidationGroup();
            NoCoordenadas = 5;           
        }                            
        
        if (DataGridObject)
        {
            this.pnlGridView.Visible = true;
            this.pnlOriginal.Visible = false;
            if (ValorCampo != null)
                CargarDataGrid();
        }
         
    }

    public void cargarComponente()
    {
        inicializarCoordenadas();
        cargarCoordenadas();
    
    }

    public void CargarDataGrid()
    {
        DataTable dt = new DataTable();    
        dt.Columns.Add("COOR_NORTE");
        dt.Columns.Add("COOR_ESTE");
        DataSet dsDatos = new DataSet();
        if (NombreTabla == "VIEWSTATEPOLIG")
        {
            DataTable dtView = (DataTable)Session["dtCoordenadasPol"];            
            foreach (DataRow dr in dtView.Rows)
            {
                string prueba = ValorCampo;
                string prueba2 = dr["NUMPOLIGONO"].ToString();
                if (ValorCampo == dr["NUMPOLIGONO"].ToString())
                {
                    DataRow dr2 = dt.NewRow();
                    dr2["COOR_NORTE"] = dr["COORNORTE"].ToString();
                    dr2["COOR_ESTE"] = dr["COORESTE"].ToString();
                    dt.Rows.Add(dr2);
                }
            }
        } 
        else
        {
          
            List<SqlParameter> parametros = new List<SqlParameter>();
            SqlParameter par = new SqlParameter("@" + NombreCampo, SqlDbType.Int, 20, NombreCampo);
            par.Value = ValorCampo;
            parametros.Add(par);
            SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsDatos, NombreTabla);


            foreach (DataRow dr in dsDatos.Tables[0].Rows)
            {
                DataRow dr2 = dt.NewRow();
                dr2["COOR_NORTE"] = dr[2].ToString();
                dr2["COOR_ESTE"] = dr[3].ToString();
                dt.Rows.Add(dr2);
            }
        }
        this.grvUbicacion.DataSource = dt;
        this.grvUbicacion.DataBind();
    }

    protected void cambValidationGroup()
    {
        string nombreControl = this.ID.ToString();
        rfvCoorNorte.ValidationGroup = "Val" + nombreControl;
        rfvCoorEste.ValidationGroup = "Val" + nombreControl;
        btnAgregarCoordenada.ValidationGroup = "Val" + nombreControl;
        ValidationSumaryCoord.ValidationGroup = "Val" + nombreControl;
    }

    protected void btnNuevaCoordenada_Click(object sender, EventArgs e)
    {
        plhIngCoordenadas.Visible = true;
        plhBotonNuevo.Visible = false;
        plhIngCoordenadas.Focus();
    }

    public void LimpiarObjetos()
    {
        this.txtCoorEste.Text = "";
        this.txtCoorNorte.Text = "";
        dsCoordenadas = (DataSet)ViewState["dsCoordenadas"];
        if (dsCoordenadas.Tables.Count>0)
        {            
            dsCoordenadas.Clear();
            ViewState.Add("dsCoordenadas", dsCoordenadas);
            this.grvCoordenadas.DataSource = dsCoordenadas;
            this.grvCoordenadas.DataBind();
        }
        plhIngCoordenadas.Visible = false;
        plhBotonNuevo.Visible = true;
        plhIngCoordenadas.Focus();
        lblMensaje.Text = "";

    }

    protected void btnCancelarCoordenada_Click(object sender, EventArgs e)
    {
        this.LimpiarObjetos();
        plhIngCoordenadas.Visible = false;
        plhBotonNuevo.Visible = true;
        plhIngCoordenadas.Focus();
    }

    public void inicializarCoordenadas()
    {
        dsCoordenadas = new DataSet();
        if (grvCoordenadas.Rows.Count == 0)
        {
            DataTable dtCoordenadas = new DataTable();
            DataColumn colCoorNorte = new DataColumn("CoorNorte");
            DataColumn colCoorEste = new DataColumn("CoorEste");

            dtCoordenadas.Columns.Add(colCoorNorte);
            dtCoordenadas.Columns.Add(colCoorEste);
            dsCoordenadas.Tables.Add(dtCoordenadas);
            ViewState.Add("dsCoordenadas", dsCoordenadas);

        }
        else
        {
            dsCoordenadas = (DataSet)ViewState["dsCoordenadas"];
        }
    }

    protected void btnAgregarCoordenada_Click(object sender, EventArgs e)
    {
        Int32 noCoordenadas = NoCoordenadas;
        if (grvCoordenadas.Rows.Count != NoCoordenadas)
        {
            dsCoordenadas = new DataSet();
            if (grvCoordenadas.Rows.Count == 0)
            {
                DataTable dtCoordenadas = new DataTable();          
                DataColumn colCoorNorte = new DataColumn("CoorNorte");
                DataColumn colCoorEste = new DataColumn("CoorEste");

                dtCoordenadas.Columns.Add(colCoorNorte);
                dtCoordenadas.Columns.Add(colCoorEste);
                dsCoordenadas.Tables.Add(dtCoordenadas);
                ViewState.Add("dsCoordenadas", dsCoordenadas);
                
            }
            else
            {
                dsCoordenadas = (DataSet)ViewState["dsCoordenadas"];
            }

            DataRow drNuevaFila = dsCoordenadas.Tables[0].NewRow();
            drNuevaFila["CoorEste"] = txtCoorEste.Text;
            drNuevaFila["CoorNorte"] = txtCoorNorte.Text;
            dsCoordenadas.Tables[0].Rows.Add(drNuevaFila);
            grvCoordenadas.DataSource = dsCoordenadas.Tables[0];
            grvCoordenadas.DataBind();
            ViewState["dsCoordenadas"] = dsCoordenadas;


        
        }
        else
        {
            //mostrar mensaje
            lblMensaje.Text = "No se pueden agregar mas coordenadas para este caso";
        }

    }

    public void VisualizarGridTemp()
    {
        plhIngCoordenadas.Visible = true;
        DataSet dsCoordenadas = (DataSet)ViewState["dsCoordenadas"];
        grvCoordenadas.DataSource = dsCoordenadas.Tables[0];
        grvCoordenadas.DataBind();
    }

    void cargarCoordenadas()
    {
        TablaValores = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();       
        foreach (DataRow dr in TablaValores .Tables [0].Rows )
        {
            DataRow fila = dsCoordenadas.Tables[0].NewRow();
            fila["CoorEste"] = dr[4];
            fila["CoorNorte"] = dr[3];
            dsCoordenadas.Tables[0].Rows.Add(fila);
        }

        grvCoordenadas.DataSource = dsCoordenadas.Tables[0];
        grvCoordenadas.DataBind();
        ViewState["dsCoordenadas"] = dsCoordenadas;


    }

    public bool Validar(int min,int max)
    {
        if (this.Coordenadas == null)
            if (min == 0)
                return true;
            else 
            {
                this.lblMensaje.Visible = true;
                this.lblMensaje.Text = "Ingrese Coordenadas";
                return false;
            }
        else
            if (this.Coordenadas.Rows.Count < min)
            {
                this.lblMensaje.Text = "Ingrese Minimo " + min.ToString() + " Coordenadas.";
                this.lblMensaje.Visible = true;
                return false;
            }
            else
                if (this.Coordenadas.Rows.Count > max)
                {
                    this.lblMensaje.Text = "Ingrese Maximo " + max.ToString() + " Coordenadas.";
                    this.lblMensaje.Visible = true;
                    return false;
                }
                else
                    return true;

        
    }
}
