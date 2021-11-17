using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class QuejasDenuncias_QyD : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_enviar_Click(object sender, EventArgs e)
    {
        string strNombre="";
        if(txt_primer_nombre_natural.Text!=string.Empty && txt_segundo_nombre_natural.Text!=String.Empty && txt_primer_apellido_natural.Text!=string.Empty&&txt_segundo_apellido_natural.Text!=string.Empty)
        {
           strNombre=txt_primer_apellido_natural.Text+" "+txt_segundo_apellido_natural.Text+", "+txt_primer_nombre_natural.Text+" "+txt_segundo_nombre_natural.Text;
        }
        else
        {
            strNombre = "Anónimo ";
                //+ClientID.ToString();
        }
        string strRadicado = "";
        string strNumeroVITAL ="";
        Random r= new Random(1000);
        strNumeroVITAL="VSAN-"+r.Next().ToString();
        strRadicado="RAD-"+r.Next();
        pnl_identificacion.Visible = false;
        pnl_infractor.Visible = false;
        pnl_localizacion.Visible = false;
        lbl_resultado.Text = "Queja Enviada, Su número VITAL es: "+strNumeroVITAL +". Radicado: "+strRadicado+ " "+DateTime.Now.ToString();
        lbl_resultado.Visible = true;
        btn_cancelar.Visible = false;
        btn_enviar.Visible = false;
        //TODO - verificar integración servicio
        //RadicacionDocumentoServicio rd = new RadicacionDocumentoServicio();
        //rd.RadicarCorrespondencia(strRadicado, strNumeroVITAL, strNombre,"Queja: Queja de Prueba");
        

    }
    protected void btn_cancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }
}
