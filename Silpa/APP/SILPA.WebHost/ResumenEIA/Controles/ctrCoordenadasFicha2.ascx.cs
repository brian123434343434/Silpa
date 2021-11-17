using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SoftManagement.Persistencia;
using System.Data.SqlClient;
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
    public int IDPadre
    {
        get { return (int)ViewState["IDPadre"]; }
        set { ViewState["IDPadre"] = value; }
    }
    public string nombreParametro
    {
        get { return (string)ViewState["nombreParametro"]; }
        set { ViewState["nombreParametro"] = value; }
    }
    public DataTable Coordenadas
    {
        get
        {
            dsCoordenadas = (DataSet)ViewState["dsCoordenadas"];
            if (dsCoordenadas != null)
                return dsCoordenadas.Tables[0];
            else
                return null;
        }
        set
        {
            ViewState["dsCoordenadas"] = value;
        }
    }
    public bool estadoGrillaCoordenadas
    {
        get { return grvCoordenadas.Visible; }
        set { grvCoordenadas.Visible = value;}
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
            ViewState.Add("dsCoordenadas", dsCoordenadas);
            cambValidationGroup();
            NoCoordenadas = 0;
        }
    }
    public void cargarComponente()
    {
        inicializarCoordenadas();
        if(!IsPostBack )
        cargarCoordenadas();
    
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
    protected void btnCancelarCoordenada_Click(object sender, EventArgs e)
    {
        plhIngCoordenadas.Visible = false;
        plhBotonNuevo.Visible = true;
        plhIngCoordenadas.Focus();
    }
    void inicializarCoordenadas()
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
        if (grvCoordenadas.Rows.Count != NoCoordenadas || NoCoordenadas==0)
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


            TablaValores = new DataSet();
            List<SqlParameter> parametros = new List<SqlParameter>();
            SqlParameter par = new SqlParameter("@" + nombreParametro, SqlDbType.Int, 10, nombreParametro);
            par.Value = IDPadre;
            parametros.Add(par);
            Contexto.cargarTabla(parametros, TablaValores, NombreTabla);

            DataRow fila = TablaValores.Tables[0].NewRow();
            fila[2] = drNuevaFila["CoorNorte"];
            fila[3] = drNuevaFila["CoorEste"];
            fila[nombreParametro] = IDPadre;

            TablaValores.Tables[0].Rows.Add(fila);
            Contexto.guardarTabla(TablaValores, NombreTabla);
        }
        else
        {
            //mostrar mensaje
            lblMensaje.Text = "No se pueden agregar mas coordenadas para este caso";
        }

    }
    void cargarCoordenadas()
    {
        TablaValores = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@" + nombreParametro, SqlDbType.Int, 10, nombreParametro);
        par.Value = IDPadre;
        parametros.Add(par);
        Contexto.cargarTabla(parametros, TablaValores, NombreTabla);
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
}
