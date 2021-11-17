using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SILPA.LogicaNegocio.Generico;
using System.Configuration;
using System.IO;
using Subgurim.Controles;
using System.Data;
using SILPA.LogicaNegocio.ReportesSolictudesVital;
using SILPA.AccesoDatos.Generico;
using AjaxControlToolkit;
using System.Web.UI.HtmlControls;
using System.Text;

public partial class ReportesSolicitudesVital_DatosBasicosReporte : System.Web.UI.Page
{
    private string TitularSalvoconducto
    {
        get { return ViewState["TitularSalvoconducto"].ToString(); }
        set { ViewState["TitularSalvoconducto"] = value; }
    }

    public DataSet dt_resultado
    {
        get
        {
            return (DataSet)ViewState["dt_resultado"];
        }

        set
        {
            ViewState["dt_resultado"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Session["Usuario"] = 22361;
            if (this.ValidacionToken() == false)
            {
                Response.Redirect(@"..\Utilitario\MensajeValidacion.aspx");
            }
            else
            {
                //Iniciliazar datos de la página
                CargarPagina();
            }
        }
    }

    private bool ValidacionToken()
    {
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

    protected void lnkSolicitante_Click(object sender, EventArgs e)
    {
        this.mpeSolicitantes.Show();
    }


    public DataSet ObtenerResultadoConsulta(int TipoReporte)
    {
        ReportesSolicitudesVital clsReportesSolicitudesVital = new ReportesSolicitudesVital();
        int AUT_ID = 0;
        DateTime FEC_EXP_DESDE = Convert.ToDateTime("01/01/1900");
        DateTime FEC_EXP_HASTA = Convert.ToDateTime("01/01/1900");
        string NUMERO_VITAL = "";
        string DPTO_DESTINO = string.Empty;
        string MUNICIPIO_DESTINO = string.Empty;
        int TITULAR_ID = 0;
        int TIPO_TRAMITE_ID = 0;
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        if (CboAutoridadAmbiental.SelectedValue != string.Empty)
        {
            AUT_ID = Convert.ToInt32(CboAutoridadAmbiental.SelectedValue);
        }

        if (!string.IsNullOrEmpty(this.TxtFecExpDesde.Text) && DateTime.TryParse(this.TxtFecExpDesde.Text, out FEC_EXP_DESDE))
        {
            FEC_EXP_DESDE = Convert.ToDateTime(this.TxtFecExpDesde.Text);
        }

        if (!string.IsNullOrEmpty(this.TxtFecExpHasta.Text) && DateTime.TryParse(this.TxtFecExpHasta.Text, out FEC_EXP_HASTA))
        {
            FEC_EXP_HASTA = Convert.ToDateTime(this.TxtFecExpHasta.Text);
        }

        if (!string.IsNullOrEmpty(TxtNumeroVital.Text))
        {
            NUMERO_VITAL = TxtNumeroVital.Text.Trim();
        }

        if (!string.IsNullOrEmpty(CboDepartamentoDestino.SelectedValue) && Convert.ToInt32(CboDepartamentoDestino.SelectedValue) > 0)
        {
            DPTO_DESTINO = CboDepartamentoDestino.SelectedItem.ToString();
        }

        if (!string.IsNullOrEmpty(CboMunicipioDestino.SelectedValue) && Convert.ToInt32(CboMunicipioDestino.SelectedValue) > 0)
        {
            MUNICIPIO_DESTINO = CboMunicipioDestino.SelectedItem.ToString();
        }

        if (!string.IsNullOrEmpty(CboTipoTramite.SelectedValue))
        {
            TIPO_TRAMITE_ID = Convert.ToInt32(CboTipoTramite.SelectedValue);
        }

        if (!string.IsNullOrEmpty(txtSolicitante.Text))
        {
            TITULAR_ID = Convert.ToInt32(TitularSalvoconducto);
        }
        else
        {
            TITULAR_ID = 0;
        }

        ds = clsReportesSolicitudesVital.ListarSolicitudesVital(TIPO_TRAMITE_ID, NUMERO_VITAL, FEC_EXP_DESDE, FEC_EXP_HASTA, TITULAR_ID, 0, AUT_ID, TipoReporte, DPTO_DESTINO, MUNICIPIO_DESTINO);
        return ds;
    }



    protected void BtnConsultar_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        ds = ObtenerResultadoConsulta(1);

        if (ds.Tables[0].Rows.Count > 0)
        {
            dt_resultado = ds;
            this.GrvResultados.DataSource = ds;
            this.GrvResultados.DataBind();
            this.lbl_total.Visible = true;
            this.lbl_total.Text = "Número total de registros : " + ds.Tables[0].Rows.Count;
        }
        else
        {
            this.GrvResultados.DataSource = ds;
            this.GrvResultados.DataBind();
            this.lbl_total.Visible = true;
            this.lbl_total.Text = "Número total de registros : " + 0;
        }
    }


    public void CargarPagina()
    {
        PersonaDalc per = new PersonaDalc();
        PersonaIdentity p = new PersonaIdentity();
        //string autNombre = per.ObtenerAutoridadPorPersona(long.Parse(Session["Usuario"].ToString()), out autID);
        //p = per.BuscarPersonaByUserId(Session["Usuario"].ToString());
        ReportesSolicitudesVital clsReportesSolicitudesVital = new ReportesSolicitudesVital();

        SILPA.LogicaNegocio.Generico.Listas _listaAutoridades = new SILPA.LogicaNegocio.Generico.Listas();
        SILPA.Comun.Configuracion _configuracion = new SILPA.Comun.Configuracion();
        Utilidades.LlenarComboTabla(_listaAutoridades.ListarAutoridades(null).Tables[0], CboAutoridadAmbiental, "AUT_NOMBRE", "AUT_ID", true);
        Listas _listaTiposId = new Listas();
        DataSet _temp = _listaTiposId.ListaTipoIdentificacion();
        cboTipoIdentificacion.DataSource = _temp.Tables[0]; // ListaDocumentos(_temp, "TPE_ID = " + Convert.ToString((int)TipoPersona.Natural));

        DataSet _dTipoTRamite = clsReportesSolicitudesVital.ObtenerTramites();

        Utilidades.LlenarComboTabla(_dTipoTRamite.Tables[0], CboTipoTramite, "NOMBRE_TIPO_TRAMITE", "ID", true);
        Utilidades.LlenarComboTabla(_temp.Tables[0], cboTipoIdentificacion, "TID_NOMBRE", "TID_ID", true);
        Utilidades.LlenarComboTabla(_listaAutoridades.ListarDepartamentos(_configuracion.IdPaisPredeterminado).Tables[0], CboDepartamentoDestino, "DEP_NOMBRE", "DEP_ID", false);
        Utilidades.LlenarComboVacio(this.CboMunicipioDestino);
        this.txtSolicitante.Text = "";
        this.TxtNumeroVital.Text = "";
        this.TxtFecExpDesde.Text = "";
        this.TxtFecExpHasta.Text = "";
    }

    protected void btnBuscarSolicitante_Click(object sender, EventArgs e)
    {
        PersonaDalc persona = new PersonaDalc();
        DataTable datos = persona.ConsultarPersona(Convert.ToInt32(this.cboTipoIdentificacion.SelectedValue), this.txtNumeroIdentificacion.Text);
        if (datos.Rows.Count > 0)
        {
            this.lblNombreSolicitante.Text = datos.Rows[0]["NOMBRE"].ToString();
            this.TitularSalvoconducto = datos.Rows[0]["ID_APPLICATION_USER"].ToString();
            this.lnkSeleccionarSolicitante.Visible = true;
        }
        else
        {
            this.lblNombreSolicitante.Text = "";
            this.lnkSeleccionarSolicitante.Visible = false;
            this.lnkSeleccionarSolicitante.Visible = false;
            lblNombreSolicitante.Text = "No existe";
        }
        this.mpeSolicitantes.Show();
    }

    protected void lnkSeleccionarSolicitante_Click(object sender, EventArgs e)
    {
        this.txtSolicitante.Text = lblNombreSolicitante.Text;
        this.hfIdSolicitante.Value = TitularSalvoconducto;
        LimpiarSolicitante();
        this.mpeSolicitantes.Hide();
    }

    protected void LimpiarSolicitante()
    {
        this.cboTipoIdentificacion.SelectedIndex = 0;
        this.txtNumeroIdentificacion.Text = "";
        this.lblNombreSolicitante.Text = "";
        this.lnkSeleccionarSolicitante.Visible = false;
    }
    protected void btnCerrarVinculosActividad_Click(object sender, EventArgs e)
    {
        LimpiarSolicitante();
        this.mpeSolicitantes.Hide();
    }


    protected void CboDepartamentoDestino_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CboDepartamentoDestino.SelectedValue == "" || CboDepartamentoDestino.SelectedValue == "-1")
        {
            CboMunicipioDestino.Items.Clear();
            CboMunicipioDestino.Items.Insert(0, new ListItem("Todos.", "-1"));
            return;
        }
        else
        {
            Listas litMunicipio = new Listas();
            DataTable dtMunicipios = litMunicipio.ListaMunicipios(null, int.Parse(CboDepartamentoDestino.SelectedValue), null).Tables[0];
            dtMunicipios = dtMunicipios.AsEnumerable().Where(x => !x.Field<string>("MUN_NOMBRE").Contains("Parque")).CopyToDataTable();
            Utilidades.LlenarComboTabla(dtMunicipios, CboMunicipioDestino, "MUN_NOMBRE", "MUN_ID", false);
            CboMunicipioDestino.Items.Insert(0, new ListItem("(Todos)", "-1"));
        }
    }

    public string urlDetalleSolicitud(object SOL_NUMERO_SILPA, object SOL_ID_SOLICITANTE)
    {
        //Response.Redirect("ReporteTramiteDetallesCiudadano.aspx?NSilpa=" + Convert.ToString(SOL_NUMERO_SILPA) + "&Id=" + Convert.ToString(SOL_ID_SOLICITANTE) + "&Ubic=Ext");
        string parametros = "?NSilpa=" + Convert.ToString(SOL_NUMERO_SILPA) + "&Id=" + Convert.ToString(SOL_ID_SOLICITANTE) + "&Ubic=Ext";
        string query = parametros;
        string queryEncriptado = "../ReporteTramite/ReporteTramiteDetallesCiudadano.aspx" + query;
        return queryEncriptado;
    }



    protected void LnkArchivoAdjuntoCreacion_Click(object sender, EventArgs e)
    {

    }

    protected void grvEstadoSalvoconducto_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrvResultados.PageIndex = e.NewPageIndex;
        GrvResultados.DataSource = dt_resultado;
        GrvResultados.DataBind();

    }


    protected void imbExportarExcel_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dt = new DataTable();
        dt = ObtenerResultadoConsulta(3).Tables[0];
        if (dt.Rows.Count > 0)
        {
            string style = @"<style>.text { mso-number-format:\@; } </style>";
            string style2 = @"<style>.NumAlfaGrid { mso-number-format:\@; } </style>";

            var html = "";
            html += "<table>";

            html += "<tr>";

            if (dt != null && dt.Rows.Count > 0)
            {

                var lstColum = (from DataColumn d in dt.Columns
                                select d.ColumnName).ToList();

                foreach (var l in lstColum)
                {
                    html += "<td>";
                    html += l.ToString();
                    html += "</td>";

                }

                foreach (DataRow dr in dt.Rows)
                {
                    html += "<tr>";
                    for (var i = 0; i < lstColum.Count(); i++)
                    {
                        html += "<td class='text'>";
                        html += HttpUtility.HtmlEncode(dr[i].ToString());
                        html += "</td>";
                    }

                    html += "</tr>";
                }
            }
            html += "</tr>";
            html += "</table>";
            Response.ClearContent();
            Response.AddHeader("Content-Disposition", "attachment;filename=" + CboTipoTramite.SelectedItem.ToString() + ".xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            Response.Charset = "UTF-8";
            Response.Write(style);
            Response.Write(style2);
            Response.Write(html);
            Response.Flush();
            Response.End();
        }
    }

    protected void LnkLimpiarSolicitante_Click(object sender, EventArgs e)
    {
        TitularSalvoconducto = "";
        this.txtSolicitante.Text = "";
    }

    protected void LnkLimpiarFechas_Click(object sender, EventArgs e)
    {
        this.TxtFecExpDesde.Text = "";
        this.TxtFecExpHasta.Text = "";
    }

    protected void GrvResultados_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string str_prueba = e.Row.Cells[1].Text;
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            switch (CboTipoTramite.SelectedValue)
            {
                case "41":
                    e.Row.Cells[6].Visible = true;
                    break;

                case "73":

                    e.Row.Cells[7].Visible = false;

                    break;

                case "74":
                    e.Row.Cells[6].Visible = true;
                    break;
                default:
                    break;
            }
        }


        if (e.Row.RowType == DataControlRowType.Header)
        {
            switch (CboTipoTramite.SelectedValue)
            {
                case "41":
                    e.Row.Cells[1].Text = "Numero Vital";
                    e.Row.Cells[2].Text = "Solicitante";
                    e.Row.Cells[3].Text = "Nombre del Proyecto";
                    e.Row.Cells[4].Text = "Sector";
                    e.Row.Cells[5].Text = "Departamento Incidente";
                    e.Row.Cells[6].Text = "Municipio Incidente";
                    e.Row.Cells[7].Text = "Autoridad Ambiental";
                    break;

                case "73":
                    e.Row.Cells[1].Text = "Numero Vital";
                    e.Row.Cells[2].Text = "Solicitante";
                    e.Row.Cells[3].Text = "Tipo Reporte";
                    e.Row.Cells[4].Text = "Fecha Reporte Parcial o de Finalizacion de la contingencia";
                    e.Row.Cells[5].Text = "Descripcion Zona Afectada y sitios Claves";
                    e.Row.Cells[6].Text = "Autoridad Ambiental";
                    break;

                case "74":

                    e.Row.Cells[1].Text = "Numero Vital";
                    e.Row.Cells[2].Text = "Solicitante";
                    e.Row.Cells[3].Text = "Tipo Reporte";
                    e.Row.Cells[4].Text = "Numero Vital Asociado";
                    e.Row.Cells[5].Text = "Fecha del reporte del avance del proceso de recuperacion ambiental";
                    e.Row.Cells[6].Text = "Descripcion de las areas en proceso de recuperacion ambiental";
                    e.Row.Cells[7].Text = "Autoridad Ambiental";
                    break;
                default:
                    break;
            }
        }
    }

    protected void GrvResultados_DataBound(object sender, EventArgs e)
    {
        int int_pagina = this.GrvResultados.PageIndex + 1;
        this.lbl_numero_pagina.Text = int_pagina.ToString();
        this.lbl_numero_paginas.Text = this.GrvResultados.PageCount.ToString();

        lbl_pagina.Visible = (this.GrvResultados.Rows.Count > 0);
        lbl_de.Visible = (this.GrvResultados.Rows.Count > 0);
        lbl_numero_pagina.Visible = (this.GrvResultados.Rows.Count > 0);
        lbl_numero_paginas.Visible = (this.GrvResultados.Rows.Count > 0);
    }

    protected void GrvResultados_PageIndexChanged(object sender, EventArgs e)
    {

    }

    protected void GrvResultados_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrvResultados.PageIndex = e.NewPageIndex;
        GrvResultados.DataSource = dt_resultado;
        GrvResultados.DataBind();
    }

    protected void CboTipoTramite_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch (CboTipoTramite.SelectedValue)
        {
            case "41":
                trDepartamentos.Visible = true;
                break;
            default:
                trDepartamentos.Visible = false;
                break;

        }
    }

}
