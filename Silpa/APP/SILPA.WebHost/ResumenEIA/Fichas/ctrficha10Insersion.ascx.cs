using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
//using SoftManagement.Persistencia;

public partial class ResumenEIA_Fichas_ctrficha10Insersion : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        estadoControles(true);
    }
    private string etiqueta;

    private DataRow fila;

    public DataRow Fila
    {
        get { return fila; }
        set { fila = value; }
    }


    public string Etiqueta
    {
        get { return this.lblEtiqueta.Text; }
        set { this.lblEtiqueta .Text  = value; }
    }

    private int idSeleccion;
    public int IdSeleccion
    {
        get { return idSeleccion; }
        set { idSeleccion = value; }
    }


    public void estadoControles(bool flag)
    {
        this.txtActividad.Enabled = flag;
        this.txtElementosSeguimiento.Enabled = flag;
        this.txtIndicadores.Enabled = flag;
        this.txtPrograma.Enabled = flag;
        this.txtUbicacion.Enabled = flag;
        this.btnRegistrar.Enabled = flag;

        this.lblActividad.Enabled = flag;
        this.lblElementosSeguimiento.Enabled = flag;
        this.lblIndicadores.Enabled = flag;
        this.lblPrograma.Enabled = flag;
        this.lblUbicacion.Enabled = flag;
        
    }
    protected void btnRegistrar_Click(object sender, EventArgs e)
    {
        Fila["EPS_ACTIVIDAD"] = this.txtActividad.Text;
        Fila["EPS_ELEMENTOS_SEGUIMIENTO"] = this.txtElementosSeguimiento.Text;
        Fila["EPS_INDICADORES"] = this.txtIndicadores.Text;
        Fila["EPS_PROGRAMA"] = this.txtPrograma.Text;
        Fila["EPS_UBICACION"] = this.txtUbicacion.Text;


    }
}
