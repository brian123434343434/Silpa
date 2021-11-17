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
//using SILPA.AccesoDatos.FirmaDigital;
using SILPA.LogicaNegocio;

public partial class FirmaDigital_DocumentosFirma : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

          //  this.grdConsulta.DataSource = DocumentoFirma.ConsultarDocumentosFirma("", "");         

            //this.grdConsulta.DataSource = DocumentoFirma.ConsultarDocumentosFirma("", ""); 
            DataTable dt = new DataTable("DOCUMENTOS");

        }
    }
}
