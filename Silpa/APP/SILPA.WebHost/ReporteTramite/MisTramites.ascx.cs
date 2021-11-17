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
using SoftManagement.Log;
using System.Data.SqlTypes;
using SILPA.AccesoDatos.Generico;

public partial class ReporteTramite_MisTramites : System.Web.UI.UserControl
{

    DataTable dtReporte = new DataTable();
    string _sectores = "";
    private long idUsuario = 0;  

    protected void Page_Load(object sender, EventArgs e)
    {     
       
        this.idUsuario = 0;
        if (Page.Request["Ubic"] == null)
        {
            if (ValidacionToken())
            {
                this.idUsuario = Int64.Parse((string)Session["Usuario"]);
            }
        }
        if (this.idUsuario != 0)
        {
            this.uppConsultaReporte.Width = 900;
            this.pnConsulta.Width = 900;
        }
        if( !IsPostBack ) {
            CargarCombosLugares( );
            CargarAutoridades( );      
            CargarSector();
            CargarSolicitantes();
            calFechaInicial.SelectedDate = DateTime.Today.AddMonths(-1);
            calFechaFinal.SelectedDate = DateTime.Today;

        }
        else
        {
            calFechaInicial.SelectedDate =  DateTime.Parse(txtFechaInicial.Text);
            calFechaFinal.SelectedDate = DateTime.Parse(txtFechaFinal.Text); 
        }
      
    }

    protected void grdReporte_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DETALLE")
        {            
            int index = Convert.ToInt32(e.CommandArgument);
            Label lbNumeroSilpa = (Label)this.grdReporte.Rows[index].FindControl("lbNumeroSilpa");
            Label lbIdSol = (Label)this.grdReporte.Rows[index].FindControl("lblIdSol");
            Label lblExpCod = (Label)this.grdReporte.Rows[index].FindControl("lblExpCod");
            Label lblTipoTramite = (Label)this.grdReporte.Rows[index].FindControl("lblTipoTramite");
            if (this.idUsuario != 0)
            {
                if (lblTipoTramite.Text == "Reconocimiento como Tercero Interviniente")
                {
                    if (lblExpCod.Text != "")
                    {
                        string exp = lblExpCod.Text.Replace("--", "").Trim();
                        Response.Redirect("ReporteTramiteDos.aspx?NSilpa=" + lbNumeroSilpa.Text + "&Id=" + lbIdSol.Text + "&Exp=" + exp);
                    }
                    else
                    {
                        Response.Redirect("ReporteTramiteDos.aspx?NSilpa=" + lbNumeroSilpa.Text + "&Id=" + lbIdSol.Text);

                    }
                }
                else
                {
                    Response.Redirect("ReporteTramiteDos.aspx?NSilpa=" + lbNumeroSilpa.Text + "&Id=" + lbIdSol.Text);

                }
            }
            else
            {
                Response.Redirect("ReporteTramiteDetallesCiudadano.aspx?NSilpa=" + lbNumeroSilpa.Text + "&Id=" + lbIdSol.Text + "&Ubic=Ext");
            }
        }
    }
   
    /// <summary>
    /// Valida si hay token para el usuario que intenta acceder a la página, y si no ha expirado
    /// </summary>
    /// <returns>Verdadero si hay token válido para el usuario</returns>
 
    private bool ValidacionToken()
    {
        //DESCOMENTAR ANTES DEL COMMIT!!!
        //Session["IDForToken"] = Request.QueryString["IdRelated"];
        //Session["IDForToken"] = "7";

        if (DatosSesion.Usuario == string.Empty)
        {
            return false;
        }
        string idUsuario = Silpa.Workflow.AccesoDatos.ApplicationUserDao.ObtenerIdUsuario(DatosSesion.Usuario).ToString();

        Session["IDForToken"] = (object)idUsuario;

        Session["Usuario"] = Session["IDForToken"];

        SILPA.LogicaNegocio.Usuario.TokenUsuario token = new SILPA.LogicaNegocio.Usuario.TokenUsuario();

        Session["Token"] = token.TomarTokenUsuario(Int32.Parse(Session["IDForToken"].ToString()));

        using (WSValidacionToken.GattacaSecurityServices9000 servicio = new WSValidacionToken.GattacaSecurityServices9000())
        {
            servicio.Url = SILPA.Comun.DireccionamientoWS.UrlWS("WSValidacionToken");
            string mensaje = servicio.GetUserInfoByToken("Softmanagement", Session["Token"].ToString());
            //string mensaje = "Token valido";

            if (mensaje.IndexOf("Token invalido") > 0)
                return false;
        }
        return true;
    }

    protected void cboDepartamento_SelectedIndexChanged( object sender, EventArgs e ) 
    {
        try
        {
            SILPA.LogicaNegocio.Generico.Listas _listaMunicipios = new SILPA.LogicaNegocio.Generico.Listas();
            int _codigoDep = int.Parse(cboDepartamento.SelectedItem.Value);
            cboMunicipio.DataSource = _listaMunicipios.ListaMunicipios(null, _codigoDep, null);
            cboMunicipio.DataTextField = "MUN_NOMBRE";
            cboMunicipio.DataValueField = "MUN_ID";
            cboMunicipio.DataBind();
            cboMunicipio.Items.Insert(0, new ListItem("Seleccione.", "-1"));
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        
    }

    #region Funciones del programador ...

    ///<sumary>
    /// Funcion que carga los combos de departamento y municipio
    /// </sumary>
    private void CargarCombosLugares( ) 
    {
        try
        {
            SILPA.Comun.Configuracion _configuracion = new SILPA.Comun.Configuracion();
            SILPA.LogicaNegocio.Generico.Listas _lista = new SILPA.LogicaNegocio.Generico.Listas();

            cboDepartamento.DataSource = _lista.ListarDepartamentos(_configuracion.IdPaisPredeterminado);
            cboDepartamento.DataTextField = "DEP_NOMBRE";
            cboDepartamento.DataValueField = "DEP_ID";
            cboDepartamento.DataBind();
            cboDepartamento.Items.Insert(0, new ListItem("Seleccione.", "-1"));

            CboArea.DataSource = _lista.ListarAreaHidrografica(null);
            CboArea.DataTextField = "AHI_NOMBRE";
            CboArea.DataValueField = "AHI_ID";
            CboArea.DataBind();
            CboArea.Items.Insert(0, new ListItem("Seleccione.", "-1"));
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
      
    }

    private void CargarAutoridades( ) 
    {
        try
        {
            SILPA.LogicaNegocio.Generico.Listas _listaAutoridades = new SILPA.LogicaNegocio.Generico.Listas();
            cboAutoridadAmbiental.DataSource = _listaAutoridades.ListarAutoridades(null);
            cboAutoridadAmbiental.DataValueField = "AUT_ID";
            cboAutoridadAmbiental.DataTextField = "AUT_NOMBRE";
            cboAutoridadAmbiental.DataBind();
            cboAutoridadAmbiental.Items.Insert(0, new ListItem("Seleccione.", "-1"));


            ddlTipoTramite.DataSource = _listaAutoridades.ListarTipoTramiteVisible();
            ddlTipoTramite.DataValueField = "ID";
            ddlTipoTramite.DataTextField = "NOMBRE_TIPO_TRAMITE";
            ddlTipoTramite.DataBind();
            ddlTipoTramite.Items.Insert(0, new ListItem("Seleccione.", "-1"));
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
     

    }

    private void CargarPublicaciones( ) {
        //SILPA.LogicaNegocio.Publicacion.Publicacion _listaPublicaciones = new SILPA.LogicaNegocio.Publicacion.Publicacion();
        //DataSet _temp = _listaPublicaciones.ListarPublicacion(null, null, null, null,
        //    ((txtNombreProyecto.Text == "") ? null : txtNombreProyecto.Text), null, null, null,null, null, null,
        //    null);
    }

    protected void btnConsultar_Click( object sender, EventArgs e ) {

        if ((txtFechaInicial.Text == "") || (txtFechaFinal.Text == ""))
        {
            grdReporte.DataSource = dtReporte;
            grdReporte.EmptyDataText = "Se debe ingresar el filtro de fecha desde - hasta.";
            grdReporte.DataBind();
            pnlReporte.Visible = true;
            grdReporte.Visible = true;
        }
        else
        {

            Consultar();
        }
    }

    private void Consultar( ) 
    {
        try
        {            
            DateTime fechaInicial = new DateTime();
            DateTime fechaFinal = new DateTime();

            if (!DateTime.TryParse(txtFechaInicial.Text, out fechaInicial))
                fechaInicial = Convert.ToDateTime(SqlDateTime.MinValue.ToString());
            if (!DateTime.TryParse(txtFechaFinal.Text, out fechaFinal))
                fechaFinal = Convert.ToDateTime(SqlDateTime.MaxValue.ToString());

           

            _sectores = SectoresEscogidos();
            if (_sectores.Equals(""))
            {
                SILPA.LogicaNegocio.DAA.SolicitudDAAEIA objTramite = new SILPA.LogicaNegocio.DAA.SolicitudDAAEIA();
                dtReporte = objTramite.ListarTramitesRPT(false, fechaInicial, fechaFinal,
                Convert.ToInt32(ddlTipoTramite.SelectedValue), TxtSilpaNumero.Text.Trim(), Convert.ToInt32(cboAutoridadAmbiental.SelectedValue),
                txtNombreProyecto.Text.Trim(), Convert.ToInt32(cboDepartamento.Text), Convert.ToInt32(cboMunicipio.SelectedValue), TxtNumeroExpediente.Text.Trim(), "", CadenaHidrografia(), int.Parse(idUsuario.ToString()),this.cboEstadoResolucion.SelectedValue,this.cboEstadoTramite.SelectedValue,this.cboSolicitantes.SelectedValue);
            }
            else
            {

                SILPA.LogicaNegocio.DAA.SolicitudDAAEIA objTramite = new SILPA.LogicaNegocio.DAA.SolicitudDAAEIA();
                dtReporte = objTramite.ListarTramitesRPT(false, fechaInicial, fechaFinal,
                Convert.ToInt32(ddlTipoTramite.SelectedValue), TxtSilpaNumero.Text.Trim(), Convert.ToInt32(cboAutoridadAmbiental.SelectedValue),
                txtNombreProyecto.Text.Trim(), Convert.ToInt32(cboDepartamento.Text), Convert.ToInt32(cboMunicipio.SelectedValue), TxtNumeroExpediente.Text.Trim(), _sectores, CadenaHidrografia(), int.Parse(idUsuario.ToString()),this.cboEstadoResolucion.SelectedValue,this.cboEstadoTramite.SelectedValue,this.cboSolicitantes.SelectedValue);
            }            
            grdReporte.DataSource = dtReporte;
            grdReporte.EmptyDataText = "No se han encontrado Registros.";
            grdReporte.DataBind();
            pnlReporte.Visible = true;
            grdReporte.Visible = true;
        //    this.uppConsultaReporte.Visible = false;
            if (dtReporte.Rows.Count > 0)
            {
                this.grdReporte.HeaderRow.Cells[0].Text = "Resultado de su busqueda: " + dtReporte.Rows.Count.ToString();
            }
            GenerarLogAuditoria CrearLogAuditoria = new GenerarLogAuditoria();
            CrearLogAuditoria.Insertar("REPORTE TRAMITE", 2, "Se consulto inscritos audiencia pública");
        }
        catch (Exception ex)
        {
            Mensaje.MostrarMensaje(this.Page,"Error al consultar tramites, comunique con el administrador");
            SMLog.Escribir(ex);
        }    

    }

    protected void grdReporte_PageIndexChanging( object sender, GridViewPageEventArgs e ) {
        //grdReporte.PageIndexChanged();
        grdReporte.PageIndex = e.NewPageIndex;
        Consultar( );
        grdReporte.DataSource = dtReporte;
        grdReporte.DataBind( );
        grdReporte.PageSize = 5;
        //CargarSector();
    }

    private void CargarSector( ) 
    {
        try
        {
            SILPA.LogicaNegocio.Generico.Sector sector = new SILPA.LogicaNegocio.Generico.Sector();
            DataTable dat = sector.ConsultarSectores();
            
            cboSectores.DataSource = dat;
            cboSectores.DataValueField = "SEC_ID";
            cboSectores.DataTextField = "SEC_NOMBRE";
            cboSectores.DataBind();
            cboSectores.Items.Insert(0, new ListItem("Seleccione.", "-1"));
           
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
            Mensaje.MostrarMensaje(this.Page, "Ha ocurrio un error a cargar los sectores.");
        }
       
    }
    private void CargarSolicitantes()
    {
        try
        {
            SILPA.LogicaNegocio.Generico.Persona personas = new SILPA.LogicaNegocio.Generico.Persona();
            DataTable dat = personas.ConsultarPersonasActivas();

            cboSolicitantes.DataSource = dat;
            cboSolicitantes.DataValueField = "PER_ID";
            cboSolicitantes.DataTextField = "PER_NOMBRE_COMPLETO";
            cboSolicitantes.DataBind();
            cboSolicitantes.Items.Insert(0, new ListItem("Seleccione.", "-1"));

        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
            Mensaje.MostrarMensaje(this.Page, "Ha ocurrio un error a cargar los sectores.");
        }

    }

    private string SectoresEscogidos( ) {
        //Primero hace el recorrido por el panel Zurdo.
        string cadena = "";
        string nombre = "";
        //foreach( Control c in PanSectorIzquierda.Controls ) {
        //    if( c is CheckBox ) {
        //        if( ( ( CheckBox )c ).Checked ) {
        //            nombre = c.ID.ToString( ).Substring( 3 ).Trim( );
        //            cadena += ",";
        //            cadena += nombre;
        //        }

        //    }
       // }

        //foreach( Control c in PanSectorDerecha.Controls ) {
        //    if( c is CheckBox ) {
        //        if( ( ( CheckBox )c ).Checked ) {
        //            nombre = c.ID.ToString( ).Substring( 3 ).Trim( );
        //            cadena += ",";
        //            cadena += nombre;
        //        }
        //    }
        //}
        cadena = this.lblSectoresSeleccionados.Text;
        if( !cadena.Equals( "" ) )
            cadena = cadena.Substring(0, cadena.Length-1);
        return cadena;
    }

    protected void grdReporte_SelectedIndexChanged( object sender, EventArgs e ) {

    }

    protected void CboArea_SelectedIndexChanged( object sender, EventArgs e ) 
    {
        try
        {
            SILPA.LogicaNegocio.Generico.Listas _lista = new SILPA.LogicaNegocio.Generico.Listas();

            CboZona.DataSource = _lista.ListarZonaHidrografica(null, Convert.ToInt32(CboArea.SelectedValue));
            CboZona.DataTextField = "ZHI_NOMBRE";
            CboZona.DataValueField = "ZHI_ID";
            CboZona.DataBind();
            CboZona.Items.Insert(0, new ListItem("Seleccione.", "-1"));
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
    
    }

    protected void CboZona_SelectedIndexChanged( object sender, EventArgs e ) 
    {
        try
        {
            SILPA.LogicaNegocio.Generico.Listas _lista = new SILPA.LogicaNegocio.Generico.Listas();

            CboSubZona.DataSource = _lista.ListarSubZonaHidrografica(null, Convert.ToInt32(CboZona.SelectedValue));
            CboSubZona.DataTextField = "SHI_NOMBRE";
            CboSubZona.DataValueField = "SHI_ID";
            CboSubZona.DataBind();
            CboSubZona.Items.Insert(0, new ListItem("Seleccione.", "-1"));
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
      
    }

    protected string CadenaHidrografia( ) {
        string cadena = "";
        {
            if( CboArea.SelectedItem.Text.IndexOf( "Seleccione" ) < 0 )
                cadena += @"\" + CboArea.SelectedItem.Text;
            if( CboZona.SelectedItem.Text.IndexOf( "Seleccione" ) < 0 )
                cadena += @"\" + CboZona.SelectedItem.Text;
            if( CboSubZona.SelectedItem.Text.IndexOf( "Seleccione" ) < 0 )
                cadena += @"\" + CboSubZona.SelectedItem.Text;
            return cadena;
        }
    }
    protected void btnAddSector_Click(object sender, EventArgs e)
    {

        if (this.cboSectores.SelectedValue != "-1")
        {
            if (this.lblSectoresSeleccionados.Text.Contains(this.cboSectores.SelectedValue.ToString() + ","))
                Mensaje.MostrarMensaje(this.Page, "Este Sector ya ha sido seleccionado");
            else
            {
                this.lblSectoresSeleccionados.Text = this.lblSectoresSeleccionados.Text+this.cboSectores.SelectedValue.ToString()+",";
                this.lstSectorSel.Items.Add(this.cboSectores.SelectedItem.ToString());
            }
        }
        else
            Mensaje.MostrarMensaje(this.Page, "Seleccione un Sector");
    }
    protected void btnElimSector_Click(object sender, EventArgs e)
    {
        if (this.lstSectorSel.SelectedIndex != -1)
        {
            int index = this.lstSectorSel.SelectedIndex;
            string[] SectorSel = this.lblSectoresSeleccionados.Text.Split(',');
            int i;
            string cadenaTemp = "";
            for (i = 0; i < SectorSel.Length-1; i++)
            {
                if (i != index)
                    cadenaTemp = cadenaTemp + SectorSel[i].Replace(",","") + ",";
                this.lstSectorSel.Items.Remove(this.lstSectorSel.SelectedItem);
            }
            this.lblSectoresSeleccionados.Text = cadenaTemp;
        }
        else
            Mensaje.MostrarMensaje(this.Page, "Seleccione el sector que va a retirar de la seleccion");
    }
}
    #endregion



