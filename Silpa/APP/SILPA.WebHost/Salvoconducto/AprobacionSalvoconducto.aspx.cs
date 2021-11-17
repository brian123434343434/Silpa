using SILPA.AccesoDatos.Salvoconducto;
using SILPA.LogicaNegocio.Generico;
using SILPA.LogicaNegocio.Salvoconducto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using SILPA.LogicaNegocio.Aprovechamiento;
using SILPA.AccesoDatos.Generico;

public partial class Salvoconducto_AprobacionSalvoconducto : System.Web.UI.Page
{
    private string SalvoconductoID { get { return (string)ViewState["SalvoconductoID"]; } set { ViewState["SalvoconductoID"] = value; } }

    public List<RutaEntity> LstRutaEntity { get { return (List<RutaEntity>)ViewState["LstRutaEntity"]; } set { ViewState["LstRutaEntity"] = value; } }

    public bool cambioRuta { get { return (bool)ViewState["cambioRuta"]; } set { ViewState["cambioRuta"] = value; } }

    public int ID_AUT_AMBIENTAL { get { return (int)ViewState["ID_AUT_AMBIENTAL"]; } set { ViewState["ID_AUT_AMBIENTAL"] = value; } }

    public SalvoconductoNewIdentity vSalvoconducto { get { return (SalvoconductoNewIdentity)ViewState["vSalvoconducto"]; } set { ViewState["vSalvoconducto"] = value; } }


    protected void Page_Load(object sender, EventArgs e)
    {
        #region recibir parámetros de url
        if (Request.QueryString["enc"] != null && Request.QueryString["enc"].ToString() != "")
        {
            #region obtengo el valor de cada uno de los parámetros [modo: encriptado]
            string encryptedQueryString = Request.QueryString["enc"].Replace(" ", "+");
            string decryptedQueryString = Utilidades.Decrypt(encryptedQueryString);
            string[] QueryStringParameters = decryptedQueryString.Split(new char[] { '&' });

            SalvoconductoID = QueryStringParameters[0].Substring(QueryStringParameters[0].IndexOf("=") + 1);
            #endregion

            #region carga inro_odtial del formulario [modo: encriptado]
            if (!Page.IsPostBack)
            {
                #region deshabilitar boton atras navegador
                Response.Cache.SetCacheability(HttpCacheability.ServerAndNoCache);
                Response.Cache.SetAllowResponseInBrowserHistory(false);
                Response.Cache.SetNoStore();
                #endregion

                //DESCOMENTAR ANTES DEL COMMIT
                //Session["Usuario"] = 32511;
                //CargarSalvoconducto(Convert.ToInt32(SalvoconductoID));
                //return;

                if (new Utilidades().ValidacionToken() == false)
                {
                    Response.Redirect(@"..\Utilitario\MensajeValidacion.aspx");
                }
                else
                {
                    #region recepción de parámetros [modo: encriptado]
                    if (!String.IsNullOrEmpty(SalvoconductoID))
                    {
                        //ejecutar métodos normales de tu formulario
                        CargarSalvoconducto(Convert.ToInt32(SalvoconductoID));
                    }
                    else
                    {
                        #region redirijo al usuario a la página de Login
                        string _strPagina = "window.top.location.href='../Utilitario/MensajeValidacion.aspx'";
                        Utilidades.AlertWindow(this.Page, "", _strPagina, (int)Utilidades._modo_apertura_formulario.ejecutarScript);
                        #endregion
                    }
                    #endregion

                }
                #endregion
            }
            else if (Request.QueryString["SalvoConductoID"] != null)
            {
                #region carga inro_odtial del formulario [modo: normal]
                if (!Page.IsPostBack)
                {
                    #region deshabilitar boton atras navegador
                    Response.Cache.SetCacheability(HttpCacheability.ServerAndNoCache);
                    Response.Cache.SetAllowResponseInBrowserHistory(false);
                    Response.Cache.SetNoStore();
                    #endregion
                    //Session["Usuario"] = 32404;
                    if (new Utilidades().ValidacionToken() == false)
                    {
                        Response.Redirect(@"..\Utilitario\MensajeValidacion.aspx");
                    }
                    else
                    {
                        #region recepción de parámetros [modo: normal]
                        SalvoconductoID = this.Request.QueryString["SalvoConductoID"];
                        #endregion
                        // ejecutar métodos normales de tu formulario
                        SalvoconductoID = Request.QueryString["SalvoConductoID"];
                        CargarSalvoconducto(Convert.ToInt32(SalvoconductoID));
                    }
                }
                #endregion
                else
                {
                    #region redirijo al usuario a la página de Login
                    string _strPagina = "window.top.location.href='../Utilitario/MensajeValidacion.aspx'";
                    Utilidades.AlertWindow(this.Page, "", _strPagina, (int)Utilidades._modo_apertura_formulario.ejecutarScript);
                    #endregion
                }
            }
            #endregion
        }
    }

    /// <summary>
    /// Metodo para calcular el aprovechamiento por producto y unidad de medidad
    /// </summary>
    protected void TotalizarCantidadesProdUnidadMedida()
    {
        if (vSalvoconducto.LstEspecimen.Count > 0)
        {
            var Resultado = from x in (from y in vSalvoconducto.LstEspecimen
                                       select new
                                       {
                                           y.NombreComunEspecie,
                                           y.NombreEspecie,
                                           y.TipoProducto,
                                           y.UnidadMedida,
                                           y.Cantidad,
                                           y.Volumen
                                       })
                            group x by new {x.NombreComunEspecie, x.NombreEspecie, x.TipoProducto, x.UnidadMedida } into t
                            select new {t.Key.NombreComunEspecie,t.Key.NombreEspecie, t.Key.TipoProducto, t.Key.UnidadMedida, TotalCantidad = t.Sum(y => y.Cantidad), TotalVolumen = t.Sum(y => y.Volumen) };

            LblLstTotalTipProductUm.Visible = true;
            this.GrvTotalesEspecies.DataSource = Resultado.ToList();
            this.GrvTotalesEspecies.DataBind();
        }
        else
        {
            LblLstTotalTipProductUm.Visible = false;
            this.GrvTotalesEspecies.DataSource = null;
            this.GrvTotalesEspecies.DataBind();
        }
    }

    public void CargarSalvoconducto(int SalvoconductoID)
    {
        SalvoconductoNew clsSalvoconductoNew = new SalvoconductoNew();
        HtmlAnchor lnk;
        vSalvoconducto = clsSalvoconductoNew.ConsultaSalvoconductoXSalvoconductoId(SalvoconductoID);
        this.TblProcEspecimenesUnico.Visible = false;
        this.TblProcEspecimenesVarios.Visible = false;



        if (vSalvoconducto.EstadoID == 2 || vSalvoconducto.EstadoID == 3)
        {
            string parametros = "SalvoConductoID=" + SalvoconductoID.ToString() + "&BloqueoSalvoConducto = false";
            string query = Utilidades.Encrypt(parametros);
            string queryEncriptado = "../Salvoconducto/ConsultaDetalleSalvoconducto.aspx" + query;
            string _strPagina = queryEncriptado;
            Response.Redirect(queryEncriptado);
            return;
        }

        //Expedicion
        //this.TxtFechaExp.Text = "";
        this.TxtFechaExp.Text = DateTime.Today.ToString("dd/MM/yyyy");
        this.TxtVigenciaDesde.Text = "";
        this.TxtVigenciaHasta.Text = "";

        ID_AUT_AMBIENTAL = vSalvoconducto.AutoridadEmisoraID;

        if (Convert.ToDateTime(vSalvoconducto.FechaExpedicion.ToString("dd/MM/yyyy").Trim()) >= Convert.ToDateTime("01/01/1900"))
        {
            this.TxtFechaExp.Text = vSalvoconducto.FechaExpedicion.ToString("dd/MM/yyyy").Trim(); ;
        }

        //jmartinez salvoconducto fase 2
        if (vSalvoconducto.LstRuta != null)
        {
            var rutaOrigen = vSalvoconducto.LstRuta.Where(x => x.DepartamentoID == vSalvoconducto.DepartamentoOrigenID && x.MunicipioID == vSalvoconducto.MunicipioOrigenID).FirstOrDefault();
            if (rutaOrigen != null)
            {
                this.LblDptoExp.Text = rutaOrigen.Departamento;
                this.LblMunicipioExp.Text = rutaOrigen.Municipio;
                this.LblCodigoDptoExp.Text = rutaOrigen.DepartamentoID.ToString();
                this.LblCodigoMunExp.Text = rutaOrigen.MunicipioID.ToString();
            }
        }
    
        
        if (vSalvoconducto.FechaInicioVigencia != null)
            this.TxtVigenciaDesde.Text = vSalvoconducto.FechaInicioVigencia.Value.ToShortDateString();
        if (vSalvoconducto.FechaFinalVigencia != null)
            this.TxtVigenciaHasta.Text = vSalvoconducto.FechaFinalVigencia.Value.ToShortDateString();

        //tipo de salvoconducto
        switch (vSalvoconducto.TipoSalvoconductoID)
        {
            case 1:
                LblTipoSalvoconducto.Text = vSalvoconducto.TipoSalvoconducto.ToUpper();
                
                if (vSalvoconducto.LstAprovechamientoOrigen.Count() == 1)
                {
                    this.TblProcEspecimenesUnico.Visible = true;
                    Aprovechamiento vAprovechamiento = new Aprovechamiento();
                    vSalvoconducto.Aprovechamiento = vAprovechamiento.ConsultaAprovechamientoXAprovechamientoId(vSalvoconducto.LstAprovechamientoOrigen.First().AprovechamientoID);
                }
                else if (vSalvoconducto.LstAprovechamientoOrigen.Count() > 1)
                {
                    this.TblProcEspecimenesVarios.Visible = true;
                    vSalvoconducto.Aprovechamiento = null;
                    foreach (var aprovechamiento in vSalvoconducto.LstAprovechamientoOrigen)
                    {
                        #region creo enlaces por cada salvoconducto
                        lnk = new HtmlAnchor();
                        lnk.InnerHtml = aprovechamiento.Detalle;
                        PlaceHolder2.Controls.Add(new LiteralControl("<img src='../App_Themes/Img/ok.png' />&nbsp;"));
                        PlaceHolder2.Controls.Add(lnk);
                        PlaceHolder2.Controls.Add(new LiteralControl("<br />"));
                        #endregion
                    }
                }
                break;
            case 2:
                LblTipoSalvoconducto.Text = vSalvoconducto.TipoSalvoconducto.ToUpper();
                this.TblProcEspecimenesUnico.Visible = false; //para el movimiento de removilizacion no se muestra la procedencia de los especimenes
                this.TblProcEspecimenesVarios.Visible = false; //para el movimiento de removilizacion no se muestra la procedencia de los especimenes
                this.TblSalvoconductosAnteriores.Visible = true;
                foreach (var SalvoConductoAnterior in vSalvoconducto.LstSalvoconductoAnterior)
                {
                    #region creo enlaces por cada salvoconducto
                    lnk = new HtmlAnchor();
                    string parametros = "SalvoConductoID=" + SalvoConductoAnterior.SalvoconductoID.ToString() + "&BloqueoSalvoConducto = false";
                    string query = Utilidades.Encrypt(parametros);
                    string queryEncriptado = "../Salvoconducto/ConsultaDetalleSalvoconducto.aspx" + query;
                    string _strPagina = queryEncriptado;
                    lnk.ID = "lnk" + SalvoConductoAnterior.SalvoconductoID.ToString();
                    lnk.HRef = _strPagina;
                    lnk.Target = "_blank";
                    lnk.InnerHtml = SalvoConductoAnterior.Detalle;
                    PlaceHolder1.Controls.Add(new LiteralControl("<img src='../App_Themes/Img/ok.png' />&nbsp;"));
                    PlaceHolder1.Controls.Add(lnk);
                    PlaceHolder1.Controls.Add(new LiteralControl("<br />"));
                    #endregion
                }
                break;
            default:
                LblTipoSalvoconducto.Text = vSalvoconducto.TipoSalvoconducto.ToUpper();
                this.TblProcEspecimenesUnico.Visible = false;
                this.TblProcEspecimenesVarios.Visible = false;
                this.TblSalvoconductosAnteriores.Visible = true;
                foreach (var SalvoConductoAnterior in vSalvoconducto.LstSalvoconductoAnterior)
                {
                    #region creo enlaces por cada salvoconducto
                    lnk = new HtmlAnchor();
                    string parametros = "SalvoConductoID=" + SalvoConductoAnterior.SalvoconductoID.ToString() + "&BloqueoSalvoConducto = false";
                    string query = Utilidades.Encrypt(parametros);
                    string queryEncriptado = "../Salvoconducto/ConsultaDetalleSalvoconducto.aspx" + query;
                    string _strPagina = queryEncriptado;
                    lnk.ID = "lnk" + SalvoConductoAnterior.SalvoconductoID.ToString();
                    lnk.HRef = _strPagina;
                    lnk.Target = "_blank";
                    lnk.InnerHtml = SalvoConductoAnterior.Detalle;
                    PlaceHolder1.Controls.Add(new LiteralControl("<img src='../App_Themes/Img/ok.png' />&nbsp;"));
                    PlaceHolder1.Controls.Add(lnk);
                    PlaceHolder1.Controls.Add(new LiteralControl("<br />"));
                    #endregion
                }


                break;
        }

        //vigencia del salvoconducto
        if (vSalvoconducto.FechaInicioVigencia != null)
        {
            if (Convert.ToDateTime(vSalvoconducto.FechaInicioVigencia.Value.ToString("dd/MM/yyyy").Trim()) >= Convert.ToDateTime("01/01/1900"))
            {
                this.TxtVigenciaDesde.Text = vSalvoconducto.FechaInicioVigencia.Value.ToString("dd/MM/yyyy").Trim();
            }
        }

        if (vSalvoconducto.FechaFinalVigencia != null)
        {
            if (Convert.ToDateTime(vSalvoconducto.FechaFinalVigencia.Value.ToString("dd/MM/yyyy").Trim()) >= Convert.ToDateTime("01/01/1900"))
            {
                this.TxtVigenciaHasta.Text = vSalvoconducto.FechaFinalVigencia.Value.ToString("dd/MM/yyyy").Trim();
            }
        }

        //titular del salvoconducto
        string NombreTitularSalvoconducto = "";

        if (!string.IsNullOrEmpty(vSalvoconducto.SolicitanteTitularPersonaIdentity.PrimerApellido))
        {
            NombreTitularSalvoconducto = vSalvoconducto.SolicitanteTitularPersonaIdentity.PrimerApellido.ToString();
        }
        if (!string.IsNullOrEmpty(vSalvoconducto.SolicitanteTitularPersonaIdentity.SegundoApellido))
        {
            NombreTitularSalvoconducto = NombreTitularSalvoconducto + " " + vSalvoconducto.SolicitanteTitularPersonaIdentity.SegundoApellido.ToString();
        }
        if (!string.IsNullOrEmpty(vSalvoconducto.SolicitanteTitularPersonaIdentity.PrimerNombre))
        {
            NombreTitularSalvoconducto = NombreTitularSalvoconducto + " " + vSalvoconducto.SolicitanteTitularPersonaIdentity.PrimerNombre.ToString();
        }
        if (!string.IsNullOrEmpty(vSalvoconducto.SolicitanteTitularPersonaIdentity.SegundoNombre))
        {
            NombreTitularSalvoconducto = NombreTitularSalvoconducto + " " + vSalvoconducto.SolicitanteTitularPersonaIdentity.SegundoNombre.ToString();
        }
        if (NombreTitularSalvoconducto == string.Empty)
        {
            NombreTitularSalvoconducto = vSalvoconducto.SolicitanteTitularPersonaIdentity.RazonSocial;
        }
        LblNombreTitular.Text = NombreTitularSalvoconducto.Trim();


        if (!string.IsNullOrEmpty(vSalvoconducto.SolicitanteTitularPersonaIdentity.NumeroIdentificacion))
        {
            LblIdentificacionTitular.Text = vSalvoconducto.SolicitanteTitularPersonaIdentity.NumeroIdentificacion.ToString();
        }

        if (!string.IsNullOrEmpty(vSalvoconducto.SolicitanteTitularPersonaIdentity.DireccionPersona.NombreMunicipio))
        {
            LbLMunicipioDomicilio.Text = vSalvoconducto.SolicitanteTitularPersonaIdentity.DireccionPersona.NombreMunicipio.ToString();
        }

        if (!string.IsNullOrEmpty(vSalvoconducto.SolicitanteTitularPersonaIdentity.DireccionPersona.DireccionPersona))
        {
            LblDireccion.Text = vSalvoconducto.SolicitanteTitularPersonaIdentity.DireccionPersona.DireccionPersona.ToString();
        }
        if (!string.IsNullOrEmpty(vSalvoconducto.SolicitanteTitularPersonaIdentity.Telefono))
        {
            LblTelefono.Text = vSalvoconducto.SolicitanteTitularPersonaIdentity.Telefono.ToString();
        }

        //clase de recurso
        LblClaseRecurso.Text = vSalvoconducto.ClaseRecurso;

        if (vSalvoconducto.TipoSalvoconductoID != 2) // para removilizacion no se carga la informacion de los especimenes
        {
            //infromacion de la obtencion de los especimenes
            string NombreTitularSalvoconductoEspecimen = "";

            if (!string.IsNullOrEmpty(vSalvoconducto.SolicitanteTitularPersonaIdentity.PrimerApellido))
            {
                NombreTitularSalvoconductoEspecimen = vSalvoconducto.SolicitanteTitularPersonaIdentity.PrimerApellido;
            }
            if (!string.IsNullOrEmpty(vSalvoconducto.SolicitanteTitularPersonaIdentity.SegundoApellido))
            {
                NombreTitularSalvoconductoEspecimen = NombreTitularSalvoconductoEspecimen + " " + vSalvoconducto.SolicitanteTitularPersonaIdentity.SegundoApellido.ToString();
            }
            if (!string.IsNullOrEmpty(vSalvoconducto.SolicitanteTitularPersonaIdentity.PrimerNombre))
            {
                NombreTitularSalvoconductoEspecimen = NombreTitularSalvoconductoEspecimen + " " + vSalvoconducto.SolicitanteTitularPersonaIdentity.PrimerNombre.ToString();
            }
            if (!string.IsNullOrEmpty(vSalvoconducto.SolicitanteTitularPersonaIdentity.SegundoNombre))
            {
                NombreTitularSalvoconductoEspecimen = NombreTitularSalvoconductoEspecimen + " " + vSalvoconducto.SolicitanteTitularPersonaIdentity.SegundoNombre.ToString();
            }

            if (!string.IsNullOrEmpty(NombreTitularSalvoconductoEspecimen))
            {
                LblNomTitularsObtLegalEsp.Text = NombreTitularSalvoconductoEspecimen;
            }
            if (vSalvoconducto.SolicitanteTitularPersonaIdentity.TipoPersona.CodigoTipoPersona == 3)
            {

                LblNomTitularsObtLegalEsp.Text = vSalvoconducto.SolicitanteTitularPersonaIdentity.RazonSocial;
            }

            if (vSalvoconducto.Aprovechamiento != null)
            {
                if (!string.IsNullOrEmpty(vSalvoconducto.Aprovechamiento.Solicitante.NumeroIdentificacion.ToString()))
                {
                    LblIdentTitularsObtLegalEsp.Text = vSalvoconducto.Aprovechamiento.Solicitante.NumeroIdentificacion.ToString();
                }
                if (!string.IsNullOrEmpty(vSalvoconducto.Aprovechamiento.Solicitante.DireccionPersona.NombreMunicipio))
                {
                    LblMunTitularsObtLegalEsp.Text = vSalvoconducto.Aprovechamiento.Solicitante.DireccionPersona.NombreMunicipio.ToString();
                }
                if (!string.IsNullOrEmpty(vSalvoconducto.Aprovechamiento.Solicitante.Telefono))
                {
                    LblTelTitularsObtLegalEsp.Text = vSalvoconducto.Aprovechamiento.Solicitante.Telefono.ToString();
                }
                if (!string.IsNullOrEmpty(vSalvoconducto.Aprovechamiento.Solicitante.DireccionPersona.DireccionPersona))
                {
                    LblDirTitularsObtLegalEsp.Text = vSalvoconducto.Aprovechamiento.Solicitante.DireccionPersona.DireccionPersona.ToString();
                }
                this.LblModo.Text = vSalvoconducto.Aprovechamiento.ModoAdquisicionRecurso;
                //this.LblActoAdministrativo.Text = vSalvoconducto.AprovechamientoID.ToString();
                this.LblActoAdministrativo.Text = vSalvoconducto.Aprovechamiento.Numero.ToString();
                this.LblFechaObtLegalEsp.Text = vSalvoconducto.Aprovechamiento.FechaExpedicion.Value.ToString("dd/MM/yyyy").Trim();

            }
            this.LblAutoridadAmbiental.Text = vSalvoconducto.AutoridadEmisora;
            this.LblFinalidadUso.Text = vSalvoconducto.Finalidad;
            this.LblVeredaProcEsp.Text = vSalvoconducto.VeredaProcedencia;
            this.LblMunicipioProcEsp.Text = vSalvoconducto.MunicipioProcedencia;
            this.LblCodMunProcEsp.Text = vSalvoconducto.MunicipioProcedenciaID.ToString();
            this.LblDepartamentoProcEsp.Text = vSalvoconducto.DepartamentoProcedencia;
            this.LblCodDptoProcEsp.Text = vSalvoconducto.DepartamentoProcedenciaID.ToString();

        }

        //ruta de desplazamiento

        if (vSalvoconducto.LstRuta != null && vSalvoconducto.LstRuta.Count > 0)
        {
            LblTipoMovilizacion.Text = vSalvoconducto.LstRuta.FirstOrDefault().TipoRuta;
        }

        switch (vSalvoconducto.LstRuta.FirstOrDefault().TipoRutaID)
        {
            case 1:
                grvRutaDesplazamiento.Columns[4].Visible = false; //intermunicipal
                break;
            case 2:
                grvRutaDesplazamiento.Columns[4].Visible = true; //casco urbano
                break;
        }


        LstRutaEntity = vSalvoconducto.LstRuta;
        grvRutaDesplazamiento.DataSource = vSalvoconducto.LstRuta;
        grvRutaDesplazamiento.DataBind();

        //JMARTINEZ SALVOCONDUCTO FASE 2
        if (vSalvoconducto.TipoSalvoconductoID != 1) //si es removilizacion o renovacion no muestro la columna de los salvocondcutos anteriores
        {
            gdvEspecimenes.Columns[0].Visible = true;
            //GrvTotalesEspecies.Columns[0].Visible = true;
        }
        else
        {
            gdvEspecimenes.Columns[0].Visible = false;
            //GrvTotalesEspecies.Columns[0].Visible = false;
        }
        
        grvTransporte.DataSource = vSalvoconducto.LstTransporte;
        grvTransporte.DataBind();
        //informacion especimenes
        gdvEspecimenes.DataSource = vSalvoconducto.LstEspecimen;
        gdvEspecimenes.DataBind();
        //JMARTINEZ SALVOCONDUCTO FASE 2
        TotalizarCantidadesProdUnidadMedida();

        ValidarSalvoconducto();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int id_tipo_origen = 0;
        Label LblIdOrigenDestino = (Label)e.Row.FindControl("LblIdOrigenDestino");
        LinkButton LnkEliminarRuta = (LinkButton)e.Row.FindControl("LnkEliminarRuta");

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (!string.IsNullOrEmpty(LblIdOrigenDestino.Text))
            {
                id_tipo_origen = Convert.ToInt32(LblIdOrigenDestino.Text);
                if (id_tipo_origen > 0)
                {
                    LnkEliminarRuta.Visible = false;
                }
            }
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            SILPA.LogicaNegocio.Generico.Listas _listaAutoridades = new SILPA.LogicaNegocio.Generico.Listas();
            SILPA.Comun.Configuracion _configuracion = new SILPA.Comun.Configuracion();
            DropDownList CboDepartamento = (DropDownList)e.Row.FindControl("CboDepartamento");
            DropDownList CboMunicipio = (DropDownList)e.Row.FindControl("CboMunicipio");
            Utilidades.LlenarComboTabla(_listaAutoridades.ListarDepartamentos(_configuracion.IdPaisPredeterminado).Tables[0], CboDepartamento, "DEP_NOMBRE", "DEP_ID", true);
            Listas litMunicipio = new Listas();
            DataTable dtMunicipios = litMunicipio.ListaMunicipios(null, LstRutaEntity.FirstOrDefault().DepartamentoID, null).Tables[0];
            dtMunicipios = dtMunicipios.AsEnumerable().Where(x => !x.Field<string>("MUN_NOMBRE").Contains("Parque")).CopyToDataTable();
            Utilidades.LlenarComboTabla(dtMunicipios, CboMunicipio, "MUN_NOMBRE", "MUN_ID", true);

            switch (LstRutaEntity.FirstOrDefault().TipoRutaID)
            {
                case 1: //Intermunicipal 
                    Utilidades.LlenarComboVacio(CboMunicipio);
                    CboDepartamento.Enabled = true;
                    break;
                case 2: //casco urbano
                    CboDepartamento.Items.FindByValue(LstRutaEntity.FirstOrDefault().DepartamentoID.ToString()).Selected = true; //por defecto dejo el departamento
                    CboDepartamento.Enabled = false;
                    CboMunicipio.Items.FindByValue(LstRutaEntity.FirstOrDefault().MunicipioID.ToString()).Selected = true; //por defecto dejo el departamento
                    CboMunicipio.Enabled = false;
                    break;
            }
        }
    }

    protected void CboDepartamento_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList CboDepartamento = (DropDownList)grvRutaDesplazamiento.FooterRow.FindControl("CboDepartamento");
        DropDownList CboMunicipio = (DropDownList)grvRutaDesplazamiento.FooterRow.FindControl("CboMunicipio");
        if (CboDepartamento.SelectedValue == "")
        {
            Utilidades.LlenarComboVacio(CboMunicipio);
            return;
        }
        else
        {
            Listas litMunicipio = new Listas();
            DataTable dtMunicipios = litMunicipio.ListaMunicipios(null, int.Parse(CboDepartamento.SelectedValue), null).Tables[0];
            dtMunicipios = dtMunicipios.AsEnumerable().Where(x => !x.Field<string>("MUN_NOMBRE").Contains("Parque")).CopyToDataTable();
            Utilidades.LlenarComboTabla(dtMunicipios, CboMunicipio, "MUN_NOMBRE", "MUN_ID", true);
        }
    }

    protected void grvRutaDesplazamiento_lnkInsertar_Click(object sender, EventArgs e)
    {
        DropDownList CboDepartamento = (DropDownList)grvRutaDesplazamiento.FooterRow.FindControl("CboDepartamento");
        DropDownList CboMunicipio = (DropDownList)grvRutaDesplazamiento.FooterRow.FindControl("CboMunicipio");
        TextBox TxtBarrio = (TextBox)grvRutaDesplazamiento.FooterRow.FindControl("TxtBarrio");
        int nuevoOrden = 1;
        string msj = "";

        if (LstRutaEntity == null)
            LstRutaEntity = new List<RutaEntity>();


        if (this.LstRutaEntity.FirstOrDefault().TipoRutaID == 1)
        {

            if (string.IsNullOrEmpty(CboDepartamento.SelectedValue))
            {
                msj = "Debe Ingresar Departamento";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Info", "alert('" + msj + "');", true);
                return;
            }

            if (string.IsNullOrEmpty(CboMunicipio.SelectedValue))
            {
                msj = "Debe Ingresar Municipio";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Info", "alert('" + msj + "');", true);
                return;
            }

            if (LstRutaEntity.Where(x => x.DepartamentoID == Convert.ToInt32(CboDepartamento.SelectedValue) && x.MunicipioID == Convert.ToInt32(CboMunicipio.SelectedValue)).Count() == 0)
            {
                LstRutaEntity.Add(new RutaEntity
                {
                    Barrio = TxtBarrio.Text.Trim(),
                    DepartamentoID = Convert.ToInt32(CboDepartamento.SelectedValue),
                    Departamento = CboDepartamento.SelectedItem.Text,
                    MunicipioID = Convert.ToInt32(CboMunicipio.SelectedValue),
                    Municipio = CboMunicipio.SelectedItem.Text,
                    Orden = LstRutaEntity.Count() - 1,
                    TipoRutaID = this.LstRutaEntity.FirstOrDefault().TipoRutaID,
                    RutaID = this.LstRutaEntity.FirstOrDefault().RutaID,
                    TipoRuta = this.LstRutaEntity.FirstOrDefault().TipoRuta,
                    IdOrigenDestino = 0,
                });
            }
        }
        else if (LstRutaEntity.FirstOrDefault().TipoRutaID == 2)
        {

            if (string.IsNullOrEmpty(CboDepartamento.SelectedValue))
            {
                msj = "Debe Ingresar Departamento";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Info", "alert('" + msj + "');", true);
                return;
            }

            if (string.IsNullOrEmpty(CboMunicipio.SelectedValue))
            {
                msj = "Debe Ingresar Municipio";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Info", "alert('" + msj + "');", true);
                return;
            }

            if (string.IsNullOrEmpty(TxtBarrio.Text))
            {
                msj = "Debe Ingresar Barrio";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Info", "alert('" + msj + "');", true);
                return;
            }


            if (LstRutaEntity.Where(x => x.DepartamentoID == Convert.ToInt32(CboDepartamento.SelectedValue) && x.MunicipioID == Convert.ToInt32(CboMunicipio.SelectedValue) && x.Barrio == TxtBarrio.Text.Trim()).Count() == 0)
            {
                LstRutaEntity.Add(new RutaEntity
                {
                    Barrio = TxtBarrio.Text.Trim(),
                    DepartamentoID = Convert.ToInt32(CboDepartamento.SelectedValue),
                    Departamento = CboDepartamento.SelectedItem.Text,
                    MunicipioID = Convert.ToInt32(CboMunicipio.SelectedValue),
                    Municipio = CboMunicipio.SelectedItem.Text,
                    Orden = LstRutaEntity.Count() - 1,
                    TipoRutaID = this.LstRutaEntity.FirstOrDefault().TipoRutaID,
                    RutaID = this.LstRutaEntity.FirstOrDefault().RutaID,
                    TipoRuta = this.LstRutaEntity.FirstOrDefault().TipoRuta,
                    IdOrigenDestino = 0,
                });
            }
        }

        //proceso para reindexar la ruta
        foreach (var ruta in LstRutaEntity)
        {
            switch (ruta.IdOrigenDestino)
            {
                case 0:
                    nuevoOrden = nuevoOrden + 1;
                    ruta.Orden = nuevoOrden;
                    ruta.Estado = false;
                    break;
                case 1:
                    ruta.Orden = 1;
                    ruta.Estado = true;
                    break;
                case 2:
                    ruta.Orden = LstRutaEntity.Count();
                    ruta.Estado = false;
                    break;
            };
        }


        this.grvRutaDesplazamiento.DataSource = LstRutaEntity.OrderBy(c => c.Orden).ToList();
        this.grvRutaDesplazamiento.DataBind();
    }

    protected void grvRutaDesplazamiento_lnkEliminar_Click(object sender, EventArgs e)
    {
        int ordenEminar = 0;
        int nuevoOrden = 1;
        try
        {
            //Cargamos el id del salvoconducto
            ordenEminar = Convert.ToInt32(((LinkButton)sender).CommandArgument.ToString().Trim());
            LstRutaEntity.RemoveAll(x => x.Orden == ordenEminar);
            //proceso para reindexar la ruta
            foreach (var ruta in LstRutaEntity)
            {
                switch (ruta.IdOrigenDestino)
                {
                    case 0:
                        nuevoOrden = nuevoOrden + 1;
                        ruta.Orden = nuevoOrden;
                        ruta.Estado = false;
                        break;
                    case 1:
                        ruta.Orden = 1;
                        ruta.Estado = true;
                        break;
                    case 2:
                        ruta.Orden = LstRutaEntity.Count();
                        ruta.Estado = false;
                        break;
                };
            }
            this.grvRutaDesplazamiento.DataSource = LstRutaEntity.OrderBy(c => c.Orden).ToList();
            this.grvRutaDesplazamiento.DataBind();
        }
        catch (Exception exc)
        {
        }
    }

    public string ObtenerCodigoSeguridad(int CntNumeros, int CntLetras)
    {
        string Codigo = "";

        Random obj = new Random();

        string Letras = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        int Numeros = obj.Next(1, 99999);
        string NumeroString = Convert.ToString(Numeros);
        int longitudLetras = Letras.Length;
        char letra;

        for (int i = 0; i < CntLetras; i++)
        {
            letra = Letras[obj.Next(longitudLetras)];
            Codigo += letra;
        }

        Codigo = Codigo + NumeroString.PadLeft(CntNumeros, '0');

        return Codigo;
    }

    protected void BtnEmitir_Click(object sender, EventArgs e)
    {

        string msj;
        DateTime FecExp;
        DateTime FecDesde;
        DateTime FecHasta;
        int DiasVigenciaSalvoconducto = 0;
        int MaximoDiasVigenciaSalvoconducto = 0;
        int pjeSerie = 0;

        

        if (!string.IsNullOrEmpty(this.TxtFechaExp.Text))
        {
            if (DateTime.TryParse(this.TxtFechaExp.Text, out FecExp) == false)
            {
                msj = "Formato Fecha Expedicion No valido";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Info", "alert('" + msj + "');", true);
                return;
            }
        }
        else
        {
            msj = "Debe Ingresar Fecha de Expedicion del Salvoconducto";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Info", "alert('" + msj + "');", true);
            return;
        }


        if (!string.IsNullOrEmpty(this.TxtVigenciaDesde.Text))
        {
            if (DateTime.TryParse(this.TxtVigenciaDesde.Text, out FecDesde) == false)
            {
                msj = "Formato Fecha Vigencia Desde No valido";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Info", "alert('" + msj + "');", true);
                return;
            }
        }
        else
        {
            msj = "Debe Ingresar Fecha Vigencia Desde del Salvoconducto";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Info", "alert('" + msj + "');", true);
            return;
        }

        if (!string.IsNullOrEmpty(this.TxtVigenciaHasta.Text))
        {
            if (DateTime.TryParse(this.TxtVigenciaHasta.Text, out FecHasta) == false)
            {
                msj = "Formato Fecha Vigencia Hasta No valido";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Info", "alert('" + msj + "');", true);
                return;
            }
        }
        else
        {
            msj = "Debe Ingresar Fecha Vigencia Hasta del Salvoconducto";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Info", "alert('" + msj + "');", true);
            return;
        }

        //valido que la fecha desde no sea inferior a la fecha de expedicion
        if (FecDesde < FecExp)
        {
            msj = "La Fecha Vigencia Desde debe ser igual o mayor a la Fecha de Expedicion";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Info", "alert('" + msj + "');", true);
            return;
        }

        if (FecExp > DateTime.Today)
        {
            msj = "la Fecha de Expedicion No puede ser mayor a la Fecha Actual";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Info", "alert('" + msj + "');", true);
            return;
        }


        #region Jmartinez se realiza cambio debido a que la fecha de expedicion no puede ser inferior a la fecha actual
        if (FecExp < DateTime.Today)
        {
            msj = "la Fecha de Expedicion No puede ser menor a la Fecha Actual";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Info", "alert('" + msj + "');", true);
            return;
        }
        #endregion


        if (FecHasta < FecDesde)
        {
            msj = "La Fecha Vigencia Hasta no puede ser mayor a la Vigencia Desde";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Info", "alert('" + msj + "');", true);
            return;
        }


        string CodigoSeguridad = ObtenerCodigoSeguridad(5, 3);


        TimeSpan ts = FecHasta - FecDesde;
        DiasVigenciaSalvoconducto = ts.Days;
        MaximoDiasVigenciaSalvoconducto = ValidarVigenciaSalvoconducto();

        if (DiasVigenciaSalvoconducto > MaximoDiasVigenciaSalvoconducto)
        {
            msj = "La Vigencia del Salvoconducto no pude ser mayor a " + MaximoDiasVigenciaSalvoconducto.ToString() + " dias";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Info", "alert('" + msj + "');", true);
            return;
        }


        SalvoconductoNew clsSalvoconductoNew = new SalvoconductoNew();
        string mensaje = "";
        mensaje = clsSalvoconductoNew.EmitirSalvoconducto(ID_AUT_AMBIENTAL, Convert.ToInt32(SalvoconductoID), FecExp, FecDesde, FecHasta, Convert.ToString(Session["Usuario"]), LstRutaEntity, CodigoSeguridad);

        if (!string.IsNullOrEmpty(mensaje))
        {
            if (int.TryParse(mensaje,out pjeSerie))
            {
                if (pjeSerie > 0)
                {
                    clsSalvoconductoNew.EnviarCorreoVencimientoSerieSUNL(Convert.ToString(Session["Usuario"]), mensaje);
                    string parametros = "SalvoConductoID=" + SalvoconductoID.ToString() + "&BloqueoSalvoConducto = false";
                    string query = Utilidades.Encrypt(parametros);
                    string queryEncriptado = "../Salvoconducto/ConsultaDetalleSalvoconducto.aspx" + query;
                    string _strPagina = queryEncriptado;
                    Response.Redirect(queryEncriptado);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Info", "alert('" + mensaje + "');", true);
                return;
            }
        }
        else
        {
            string parametros = "SalvoConductoID=" + SalvoconductoID.ToString() + "&BloqueoSalvoConducto = false";
            string query = Utilidades.Encrypt(parametros);
            string queryEncriptado = "../Salvoconducto/ConsultaDetalleSalvoconducto.aspx" + query;
            string _strPagina = queryEncriptado;
            Response.Redirect(queryEncriptado);
        }


    }

    private class Rutas
    {
        public int DptoOrigen;
        public int DptoDestino;
        public int MunicipioOrigen;
        public int MunicipioDestino;
        public int CantidadDias;


        public Rutas(int DptoOrigen, int MunicipioOrigen, int DptoDestino, int MunicipioDestino, int CantidadDias)
        {
            this.DptoOrigen = DptoOrigen;
            this.MunicipioOrigen = MunicipioOrigen;
            this.DptoDestino = DptoDestino;
            this.MunicipioDestino = MunicipioDestino;
            this.CantidadDias = CantidadDias;
        }
    }

    public int ValidarVigenciaSalvoconducto()
    {
        int dias = 8;
        int DPTO_ID_ORIGEN = 0;
        int MUNICIPIO_ID_ORIGEN = 0;

        int DPTO_ID_DESTINO = 0;
        int MUNICIPIO_ID_DESTINO = 0;
        List<Rutas> Rutas = new List<Rutas>();

        Rutas.Add(new Rutas(91, 91798, 86, 86568, 30)); //TARAPACA -  PTO ASIS
        Rutas.Add(new Rutas(86, 86568, 91, 91798, 30));//PTO ASIS - TARAPACA  
        Rutas.Add(new Rutas(91, 91001, 86, 86568, 30)); //LETICIA -  PTO ASIS
        Rutas.Add(new Rutas(86, 86568, 91, 91001, 30));//PTO ASIS - LETICIA

        var origen = from DepartamentoOrigen in LstRutaEntity.AsEnumerable()
                     where DepartamentoOrigen.IdOrigenDestino == 1
                     select new
                     {
                         DPTO_ORIGEN = DepartamentoOrigen.DepartamentoID,
                         MUNICIPIO_ORIGEN = DepartamentoOrigen.MunicipioID,
                     };
        foreach (var resultado_origen in origen)
        {
            DPTO_ID_ORIGEN = resultado_origen.DPTO_ORIGEN;
            MUNICIPIO_ID_ORIGEN = resultado_origen.MUNICIPIO_ORIGEN;
        }


        var destino = from DepartamentoDestino in LstRutaEntity.AsEnumerable()
                      where DepartamentoDestino.IdOrigenDestino == 2
                      select new
                      {
                          DPTO_DESTINO = DepartamentoDestino.DepartamentoID,
                          MUNICIPIO_DESTINO = DepartamentoDestino.MunicipioID,
                      };
        foreach (var resultado_destino in destino)
        {
            DPTO_ID_DESTINO = resultado_destino.DPTO_DESTINO;
            MUNICIPIO_ID_DESTINO = resultado_destino.MUNICIPIO_DESTINO;
        }

        //VALIDO RUTA AMAZONAS - TARAPACA - PUTUMAYO PTO ASIS Y VICEVERSA
        //VALIDO RUTA AMAZONAS LETICIA - PUTUMAYO PTO ASIS Y VICEVERSA
        if (Rutas.Where(x => x.DptoOrigen == DPTO_ID_ORIGEN && x.MunicipioOrigen == MUNICIPIO_ID_ORIGEN && x.DptoDestino == DPTO_ID_DESTINO && x.MunicipioDestino == MUNICIPIO_ID_DESTINO).Count() > 0)
        {
            dias = 30;
        }

        return dias;
    }

    protected void BtnRechazar_Click(object sender, EventArgs e)
    {
        this.TxtMotivoRechazo.Text = "";
        this.TblMotivoRechazo.Visible = true;
    }

    protected void BtnGrabarRechazo_Click(object sender, EventArgs e)
    {
        string msj = "";
        if (string.IsNullOrEmpty(this.TxtMotivoRechazo.Text))
        {
            msj = "Debe Ingresar un Motivo de Rechazo";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Info", "alert('" + msj + "');", true);
            return;
        }
        SalvoconductoNew clsSalvoconductoNew = new SalvoconductoNew();
        clsSalvoconductoNew.RechazarSalvoconducto(Convert.ToInt32(SalvoconductoID), this.TxtMotivoRechazo.Text, Convert.ToString(Session["Usuario"]));
        string parametros = "SalvoConductoID=" + SalvoconductoID.ToString() + "&BloqueoSalvoConducto = false";
        string query = Utilidades.Encrypt(parametros);
        string queryEncriptado = "../Salvoconducto/ConsultaDetalleSalvoconducto.aspx" + query;
        string _strPagina = queryEncriptado;
        Response.Redirect(queryEncriptado);
        return;
    }

    protected void gdvEspecimenes_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gdvEspecimenes.PageIndex = e.NewPageIndex;
        gdvEspecimenes.DataSource = vSalvoconducto.LstEspecimen;
        gdvEspecimenes.DataBind();
    }


    /// <summary>
    /// Metodo Para validar el salvocnducto antes de emitir
    /// </summary>
    protected void ValidarSalvoconducto()
    {

        SalvoconductoNew clsSalvoconductoNew = new SalvoconductoNew();
        DataSet ds = new DataSet();
        try
        {
            ds = clsSalvoconductoNew.ValidarSalvoconducto(Convert.ToInt32(SalvoconductoID));
            this.BtnEmitir.Enabled = true;
            if (ds != null && ds.Tables.Count > 0 && ((ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0) || (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)))
            {
                mpeValidacionSUNL.Show();
                this.BtnEmitir.Enabled = false;
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    GrvSaldoEspecies.DataSource = ds.Tables[0];
                    GrvSaldoEspecies.DataBind();
                }

                if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                {
                    GrvValidacionGeneral.DataSource = ds.Tables[1];
                    GrvValidacionGeneral.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Info", "alert('" + ex.Message + "');", true);
        }
       
    }


    protected void Salir_Click(object sender, EventArgs e)
    {
        mpeValidacionSUNL.Hide();
    }
}