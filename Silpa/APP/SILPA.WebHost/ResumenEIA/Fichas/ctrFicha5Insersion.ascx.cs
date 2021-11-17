using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class ResumenEIA_Fichas_ctrFicha5Insersion : System.Web.UI.UserControl
{

    private int tipoformulario;
    public int Tipoformulario
    {
        get { return tipoformulario; }
        set { tipoformulario = value; }
    }

    
    private string codigo;
    public string Codigo
    {
        get { return codigo; }
        set { codigo = value; }
    }
    private string unidadAIntervenir;
    public string UnidadAIntervenir
    {
        get { return unidadAIntervenir; }
        set { unidadAIntervenir = value; }
    }
    private string fuentes;
    public string Fuentes
    {
        get { return fuentes; }
        set { fuentes = value; }
    }
    private string dimension;
    public string Dimension
    {
        get { return dimension; }
        set { dimension = value; }
    }
    private string componenteAfectado;
    public string ComponenteAfectado
    {
        get { return componenteAfectado; }
        set { componenteAfectado = value; }
    }
    private string infraestructura;
    public string Infraestructura
    {
        get { return infraestructura; }
        set { infraestructura = value; }
    }
    private string infraestructuraGenera;
    public string InfraestructuraGenera
    {
        get { return infraestructuraGenera; }
        set { infraestructuraGenera = value; }
    }
    private string actividadesProyAfectan;

    public string ActividadesProyAfectan
    {
        get { return actividadesProyAfectan; }
        set { actividadesProyAfectan = value; }
    }
    private string actividadesProyInterviene;

    
    public string ActividadesProyInterviene
    {
        get { return actividadesProyInterviene; }
        set { actividadesProyInterviene = value; }
    }
    private string area;

    public string Area
    {
        get { return area; }
        set { area = value; }
    }
    private string ptge;

    public string Ptge
    {
        get { return ptge; }
        set { ptge = value; }
    }
    private string impacto;

    public string Impacto
    {
        get { return impacto; }
        set { impacto = value; }
    }
    private int tipoImpacto;

    public int TipoImpacto
    {
        get { return tipoImpacto; }
        set { tipoImpacto = value; }
    }






    
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }

    public void habilitarCampos()
    {
        switch (tipoformulario)
        {
            case 1://geotecnia
                habilitar1();
                break;
            case 2:
                habilitar1();
                break;
            case 3:
                habilitar3();
                break;
            case 4:
                habilitar3();
                break;
            case 5:
                habilitar1();
                    break;
            case 6:
               habilitar1();
                break;
            case 7:
                habilitar1();
                break;  
            case 8:
                habilitar1();
                break;
            case 9:
                habilitar1();
                break;
            case 10:
                break;
        }
    }

    private void habilitar5()
    {
        throw new NotImplementedException();
    }

    private void habilitar4()
    {
        throw new NotImplementedException();
    }

    private void habilitar3()
    {
        txtCodigoMapa.Visible = true;
        lblCodigoMapa.Visible = true;
        txtCodigoMapa.Visible = true;
        lblUnidadIntervenir.Visible = true;
        this.txtUnidadAIntervenir.Visible = true;
        lblInfraestructura.Visible = true;
        txtInfraestructura.Visible = true;
        lblArea.Visible = true;
        txtArea.Visible = true;
        lblptge.Visible = true;
        txtptge.Visible = true;
        lblImpacto.Visible = true;
        txtImpacto.Visible = true;
        cboTipoImpacto.Visible = true;
        lblTipoImpacto.Visible = true;    
    }
    private void habilitar1()
    {
        this.lblNro.Visible = true;       
        txtCodigoMapa.Visible = true;
   

        lblUnidadIntervenir.Visible = true;
        this.txtUnidadAIntervenir.Visible = true;
      
        
        lblInfraestructura.Visible = true;
        txtInfraestructura.Visible = true;
     

        lblArea.Visible = true;
        txtArea.Visible = true;
  
        
        lblImpacto.Visible = true;
        txtImpacto.Visible = true;
 
        
        cboTipoImpacto.Visible = true;
        lblTipoImpacto.Visible = true;
          

    }


    protected void btnRegistrar_Click(object sender, EventArgs e)
    {
        this.Visible = false;
    }
    protected void txtcomponenteAfectado_TextChanged(object sender, EventArgs e)
    {

    }
}
