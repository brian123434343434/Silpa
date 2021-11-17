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

public partial class Salvoconducto_ConsultarSalvoconductoExt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        this.grdSalvoconductos.DataSource = new DataTable();
        this.grdSalvoconductos.DataBind();
        this.grdDetalleSalvoconductos.DataSource = new DataTable();
        this.grdDetalleSalvoconductos.DataBind();
        SILPA.LogicaNegocio.Generico.Listas lstSalvo = new SILPA.LogicaNegocio.Generico.Listas();
        DataSet ds = lstSalvo.ListarSalvoconducto(0, this.txtNumeroSalvoconducto.Text);
        //this.grdSalvoconductos.DataSource = ds;
        //this.grdSalvoconductos.DataBind();
        string prueba=String.Concat("1", "1");
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count == 1)
            {
                MostrarLista(ds.Tables[0].Rows[0]);
            }
            else
            {
                this.grdSalvoconductos.DataSource = ds;
                this.grdSalvoconductos.DataBind();
            }
        }
    }
        
    private void MostrarLista(DataRow drDatos)
    {
        DataTable tbDatosFill = new DataTable();
        tbDatosFill.Columns.Add("VALORCAMPO");
        tbDatosFill.Columns.Add("VALOR");

        DataRow drFill = tbDatosFill.NewRow();
        drFill[0]="DETALLE SALVOCONDUCTO";
        drFill[1]="";
        tbDatosFill.Rows.Add(drFill);


        drFill = tbDatosFill.NewRow();
        drFill[0]="Número de Expediente";
        drFill[1] = drDatos["EXP_NUMERO"].ToString();
        tbDatosFill.Rows.Add(drFill);

        drFill = tbDatosFill.NewRow();
        drFill[0]="Número de Salvoconducto";
        drFill[1] = drDatos["SAV_NUMERO"].ToString();
        tbDatosFill.Rows.Add(drFill);

        drFill = tbDatosFill.NewRow();
        drFill[0]="Fecha Inicial";
        drFill[1] = drDatos["SAV_FECHA_DESDE"].ToString();
        tbDatosFill.Rows.Add(drFill);

        drFill = tbDatosFill.NewRow();
        drFill[0]="Fecha final";
        drFill[1] = drDatos["SAV_FECHA_HASTA"].ToString();
        tbDatosFill.Rows.Add(drFill);

        drFill = tbDatosFill.NewRow();
        drFill[0]="Tipo Salvoconducto";
        drFill[1] = drDatos["TSA_NOMBRE"].ToString();
        tbDatosFill.Rows.Add(drFill);

        //drFill = tbDatosFill.NewRow();
        //drFill[0]="Fecha de expedición";
        //drFill[1]=tbDatos.Rows[0]["COB_FECHA_EXPEDICION"].ToString();
        //tbDatosFill.Rows.Add(drFill);                        
                
        string idSav = drDatos["SAV_ID"].ToString();
        string idProcessInstance = drDatos["IDPROCESSINSTANCE"].ToString();

        SILPA.LogicaNegocio.Generico.Listas lstSalvo = new SILPA.LogicaNegocio.Generico.Listas();

        DataSet ds2 = lstSalvo.ListarSalvoconductoEspecimen(Int64.Parse(idSav));
        
        if (ds2.Tables.Count>0)
        {
            int i=1;
            foreach(DataRow row in ds2.Tables[0].Rows)
            {
                drFill = tbDatosFill.NewRow();
                drFill[0]="ESPECIMEN APROBADO " +i.ToString();
                drFill[1] = "";
                tbDatosFill.Rows.Add(drFill);

                drFill = tbDatosFill.NewRow();
                drFill[0]="Nombre Cientifico";
                drFill[1] = row["ESP_NOMBRE_CIENTIFICO"].ToString();
                tbDatosFill.Rows.Add(drFill);

                drFill = tbDatosFill.NewRow();
                drFill[0]="Nombre Comun";
                drFill[1] = row["ESP_NOMBRE_COMUN"].ToString();
                tbDatosFill.Rows.Add(drFill);

                drFill = tbDatosFill.NewRow();
                drFill[0]="Descripción";
                drFill[1] = row["ESP_DESCRIPCION"].ToString();
                tbDatosFill.Rows.Add(drFill);                        
    
                drFill = tbDatosFill.NewRow();
                drFill[0]="Identificación";
                drFill[1] = row["ESP_IDENTIFICACION"].ToString();
                tbDatosFill.Rows.Add(drFill);                        

                drFill = tbDatosFill.NewRow();
                drFill[0]="Cantidad";
                drFill[1] = row["ESP_CANTIDAD"].ToString();
                tbDatosFill.Rows.Add(drFill);                        

                drFill = tbDatosFill.NewRow();
                drFill[0]="Dimensiones";
                drFill[1] = row["ESP_DIMENSIONES"].ToString();
                tbDatosFill.Rows.Add(drFill);   

                i+=1;
            }
        }
         

        
        DataSet ds = lstSalvo.ListarSalvoconductoDetalle(Int64.Parse(idProcessInstance));
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            drFill = tbDatosFill.NewRow();
            drFill[0] = row["VALORCAMPO"];
            drFill[1] = row["VALOR"];
            tbDatosFill.Rows.Add(drFill);
        }

        this.grdDetalleSalvoconductos.DataSource = tbDatosFill;
        this.grdDetalleSalvoconductos.DataBind();

    }

    protected void lbDocumentos_Click(object sender, CommandEventArgs e)
    {
        int index = int.Parse(e.CommandArgument.ToString());
        SILPA.LogicaNegocio.Generico.Listas lstSalvo = new SILPA.LogicaNegocio.Generico.Listas();
        DataSet ds = lstSalvo.ListarSalvoconducto(0, this.txtNumeroSalvoconducto.Text);
        MostrarLista(ds.Tables[0].Rows[index]);
    }

    protected void grdSalvoconductos_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowIndex > -1)
        {
            LinkButton lbText = (LinkButton)e.Row.FindControl("lnkSol");
            lbText.Text = "Solicitud " + (e.Row.RowIndex +1).ToString();
        }
    }
}
