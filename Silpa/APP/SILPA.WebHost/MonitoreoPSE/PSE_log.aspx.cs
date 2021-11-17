using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SILPA.LogicaNegocio.Log_PSE;

public partial class MonitoreoPSE_PSE_log : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

          if (!Page.IsPostBack)
        {
            SILPA.LogicaNegocio.Generico.Listas _listas = new SILPA.LogicaNegocio.Generico.Listas();
            this.cboAutoridadAmbiental.DataSource = _listas.ListarAutoridades(null);
            this.cboAutoridadAmbiental.DataValueField = "AUT_ID";
            this.cboAutoridadAmbiental.DataTextField = "AUT_NOMBRE";
            this.cboAutoridadAmbiental.DataBind();
            this.cboAutoridadAmbiental.Items.Insert(0, new ListItem("Seleccione...", "-1"));
        }
    }
    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        MOH_Log_PSE objSMHLog = new MOH_Log_PSE();

        int iNumTransaccion = -1;

        if (txtCodTransaccion.Text.Trim() != "")
            iNumTransaccion = int.Parse(txtCodTransaccion.Text.Trim());

        
        Int32 iCandidato = -1;

        if (txtNumIdentifSolicitante.Text.Trim() != "")
            iCandidato = Int32.Parse(txtNumIdentifSolicitante.Text.Trim());

        Int32 iValor = -1;

        if (txtValorEntregado.Text.Trim() != "")
            iValor = Int32.Parse(txtValorEntregado.Text.Trim());
      

        grdResultado.DataSource = objSMHLog.ConsultaLog(iNumTransaccion, txtTransaccionFechaIni.Text.Trim(), txtTransaccionFechaFin.Text.Trim(),
                                                        txtCodigoBanco.Text.Trim(), int.Parse(cboAutoridadAmbiental.SelectedValue),
                                                        txtNumSILPA.Text.Trim(), txtNumExpediente.Text.Trim(), txtNumReferencia.Text.Trim(),
                                                        txtExpedicionFechaIni.Text.Trim(), txtExpedicionFechaFin.Text.Trim(),
                                                        txtVencimientoFechaIni.Text.Trim(), txtVencimientoFechaFin.Text.Trim(), iCandidato, iValor);



        grdResultado.DataBind();
    }

    protected void cboAutoridadAmbiental_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
