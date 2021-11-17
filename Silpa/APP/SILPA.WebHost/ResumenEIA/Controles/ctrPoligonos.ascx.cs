using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using SoftManagement.Persistencia;
using System.Collections.Generic;
using System.Data.SqlClient;


public partial class ResumenEIA_Controles_ctrPoligonos : System.Web.UI.UserControl
{

    DataTable dsCoordenadas;    
    public DataTable Coordenadas
    {
        get
        {
            dsCoordenadas = (DataTable)ViewState["dtCoordenadasPol"];
            if (dsCoordenadas != null)
            {                
                return dsCoordenadas;
            }
            else
                return null;
        }
        set
        {
            ViewState["dtCoordenadasPol"] = value;
        }
    }

    bool _dataGridObject;
    public bool DataGridObject
    {
        get
        {
            return _dataGridObject;
        }
        set
        {
            _dataGridObject = value;
        }
    }

    bool _nombreVista;
    public string NombreTabla
    {
        get { return (string)ViewState["NombreTabla"]; }
        set { ViewState["NombreTabla"] = value; }
    }

    public string ValorCampo
    {
        get
        {
            return (String)ViewState["ctrValorCampo"];
        }
        set
        {
            ViewState["ctrValorCampo"] = value;
        }
    }

    string paramPrueba;
    string _nombreCampo;

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

    private void CargarDataGrid()
    {
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@" + NombreCampo, SqlDbType.Int, 20, NombreCampo);
        par.Value = ValorCampo;
        parametros.Add(par);
        DataSet dsDatos= new DataSet();
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsDatos, NombreTabla);

        this.Coordenadas = dsDatos.Tables[0];
        cargarGrvPoligonos();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        lblMensaje.Text = "";
        cargarGrvPoligonos();

        if (DataGridObject)
        {
            this.txtInfraestruc.Visible = false;
            this.ctrCoordenadasPoligono.Visible = false;
            this.Label1.Visible = false;
            this.Label2.Visible = false;
            this.btnAgregarRegistro.Visible = false;
            this.btnCancelar.Visible = false;
            this.grvPoligonos.Columns[0].Visible = false;
            
            if (ValorCampo != null)
                CargarDataGrid();
        }
    }
    
    protected void btnAgregarRegistro_Click(object sender, EventArgs e)
    {        

        if (ctrCoordenadasPoligono.Coordenadas != null)
        {
            if (ctrCoordenadasPoligono.Coordenadas.Rows.Count == 5)
            {
                if (this.Coordenadas == null)
                    InicializarCoordenadas();
                DataTable dt = this.Coordenadas;                
                int numPol = 1;
                if (dt.Rows.Count > 0)
                    numPol+=int.Parse(dt.Rows[dt.Rows.Count - 1]["NumPoligono"].ToString());

                foreach (DataRow dr in ctrCoordenadasPoligono.Coordenadas.Rows)
                {
                    DataRow row = dt.NewRow();
                    row["NumPoligono"] = numPol.ToString();
                    row["DescPoligono"] = this.txtInfraestruc.Text;
                    row["CoorNorte"] = dr["CoorNorte"];
                    row["CoorEste"]=dr["CoorEste"];
                    dt.Rows.Add(row);
                }                                
                ViewState["dtCoordenadasPol"] = dt;
                cargarGrvPoligonos();
                this.ctrCoordenadasPoligono.LimpiarObjetos();
 
                this.txtInfraestruc.Text = "";
            }
            else
                lblMensaje.Text = "Debe agregar 5 coordenadas del poligono.";
        }
        else
            lblMensaje.Text = "Debe agregar las coordenadas del poligono.";
    }

    private void InicializarCoordenadas()
    {        
        DataTable dt = new DataTable();
        dt.Columns.Add("NumPoligono");
        dt.Columns.Add("DescPoligono");
        dt.Columns.Add("CoorNorte");
        dt.Columns.Add("CoorEste");        
        ViewState["dtCoordenadasPol"] = dt;
    }

    private void cargarGrvPoligonos()
    {
        
        if (this.Coordenadas != null)
        {
            Session["dtCoordenadasPol"] = this.Coordenadas;
            DataTable dtPolig = new DataTable();
            dtPolig.Columns.Add("NumPoligono");
            dtPolig.Columns.Add("DescPoligono");
            foreach (DataRow row in this.Coordenadas.Rows)
            {                
                if (dtPolig.Select("NumPoligono=" + row["NumPoligono"].ToString()).Length == 0)
                {
                    DataRow row2 = dtPolig.NewRow();                    
                    row2["NumPoligono"] = row["NumPoligono"].ToString();
                    row2["DescPoligono"] = row["DescPoligono"].ToString();
                    dtPolig.Rows.Add(row2);
                }
            }
            grvPoligonos.DataSource = dtPolig;
            grvPoligonos.DataBind();
        }

    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        this.txtInfraestruc.Text = "";
        this.ctrCoordenadasPoligono.LimpiarObjetos();
        this.Coordenadas = null;
        this.grvPoligonos.Columns.Clear();
        this.grvPoligonos.DataBind();    
    }

    protected void grvPoligonos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable dt=this.Coordenadas;
        int i = 0;
        for (i = dt.Rows.Count - 1; i >= 0;i-- )
        {
            if (int.Parse(dt.Rows[i]["NumPoligono"].ToString()) == e.RowIndex+1)
            {
                dt.Rows[i].Delete();
            }
            else
            {
                int j = AsignarNumeroColumna(e.RowIndex+1, int.Parse(dt.Rows[i]["NumPoligono"].ToString()));
                dt.Rows[i]["NumPoligono"] = j.ToString();
            }
        }
        this.Coordenadas = dt;
        cargarGrvPoligonos();
    }

    private int AsignarNumeroColumna(int numEli,int NumAct)
    {
        if (numEli > NumAct)
            return NumAct;
        else
        {
            return NumAct - 1;
        }
    }

    public bool Validar()
    {

        DataTable dt = this.Coordenadas;
        if (dt == null)
        {
            this.lblMensaje.Text = "No se han agregado poligonos";
            return false;
        }
        else
        {
            if (dt.Rows.Count == 0)
            {
                this.lblMensaje.Text = "No se han agregado poligonos";
                return false;
            }
        }
        return true;
    }

    public void Limpiar()
    {
        this.ctrCoordenadasPoligono.LimpiarObjetos();
        this.txtInfraestruc.Text = "";
        this.Coordenadas = null;
        this.grvPoligonos.Columns.Clear();
        this.grvPoligonos.DataBind();
    }
}
