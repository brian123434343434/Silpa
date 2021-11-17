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
using SILPA.AccesoDatos;
using SILPA.LogicaNegocio.RUIA;

public partial class RUIA_ActualizarFechaSancion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //
        if (!IsPostBack)
        {
            this.CargarSanciones();
         }   
    }

    #region Funciones Programador ...

      
    private void CargarSanciones()
    {
        //obtiene el id de la autoridad ambiental logueada
        int? _idAutoridad = null;
        // 12-jul-2010 - aegb      
        DataTable merge = new DataTable();      
        if (DatosSesion.Usuario != string.Empty)
        {
            SILPA.LogicaNegocio.Usuario.Usuario usuario = new SILPA.LogicaNegocio.Usuario.Usuario();
            DataTable autoridad = usuario.ConsultarUsuarioSistemaCompania(DatosSesion.Usuario);
            if (autoridad.Rows.Count > 0)

            {
                Sancion _sancion = new Sancion();
               
                foreach (DataRow row in autoridad.Rows)
                {
                    _idAutoridad = int.Parse(row["IDAutoridad"].ToString());
                    DataSet _temp=new DataSet();
                    if (merge.Rows.Count > 0)
                    {
                        if (merge.Select("SAN_AUT_ID=" + _idAutoridad.ToString()).Length == 0)
                        {
                            _temp = _sancion.ListaSancionDetalle(-1, _idAutoridad, "");
                        }
                    }
                    else
                    {
                        _temp = _sancion.ListaSancionDetalle(-1, _idAutoridad, "");
                        merge.Merge(_temp.Tables[0]);
                    }
                    
                }
            }
        }            
                       
        grdSanciones.DataSource = merge;
        grdSanciones.DataBind();
    }

  
    #endregion  
        
    protected void grdSanciones_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        TextBox txtFechaEjecucion = (TextBox)grdSanciones.Rows[e.RowIndex].FindControl("txtFechaEjecucion");
        Label lblSancionPrincipal = (Label)grdSanciones.Rows[e.RowIndex].FindControl("lblSancionPrincipal");
        Sancion _sancion = new Sancion();
        _sancion.ActualizarSancion(long.Parse(grdSanciones.DataKeys[e.RowIndex].Value.ToString()), txtFechaEjecucion.Text, int.Parse(lblSancionPrincipal.Text));
        this.grdSanciones.EditIndex = -1;
        this.CargarSanciones();
    }

    protected void grdSanciones_RowEditing(object sender, GridViewEditEventArgs e)
    {
        this.grdSanciones.EditIndex = e.NewEditIndex;
        this.CargarSanciones();
    }
    protected void grdSanciones_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        this.grdSanciones.EditIndex = -1;
        this.CargarSanciones();
    }
    protected void grdSanciones_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.grdSanciones.PageIndex = e.NewPageIndex;
        this.CargarSanciones();
    }
    protected void grdSanciones_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
           if (e.Row.RowState.ToString().IndexOf("Edit", 0) >= 0)               
            {
                LinkButton lnkGuardar = (LinkButton)e.Row.FindControl("lnkGuardar");
                lnkGuardar.Attributes.Add("onClick", "return confirm('Esta seguro que actualizar la fecha de cumplimiento?')");
            }
        }
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("../../SILPA/TESTSILPA/security/default.aspx");
    }
}
