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

public partial class Salvoconducto_ConsultaDetalleSalvoconducto : System.Web.UI.Page
{
    private String SalvoconductoID;
    private bool SNBloquearSalvoconducto;

    public List<RutaEntity> LstRutaEntity { get { return (List<RutaEntity>)ViewState["LstRutaEntity"]; } set { ViewState["LstRutaEntity"] = value; } }
    private int autoridadLogeada { get { return (int)ViewState["autID"]; } set { ViewState["autID"] = value; } }

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
            SNBloquearSalvoconducto = Convert.ToBoolean(QueryStringParameters[1].Substring(QueryStringParameters[1].IndexOf("=") + 1));
            #endregion

            #region carga inro_odtial del formulario [modo: encriptado]
            if (!Page.IsPostBack)
            {
                #region deshabilitar boton atras navegador
                Response.Cache.SetCacheability(HttpCacheability.ServerAndNoCache);
                Response.Cache.SetAllowResponseInBrowserHistory(false);
                Response.Cache.SetNoStore();
                #endregion
                //**************************************************************
                //DESCOMENTAR ANTES DEL COMMIT
                //Session["Usuario"] = 32511;
                //CargarSalvoconducto(Convert.ToInt32(SalvoconductoID));
                //return;
                //**************************************************************
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
                if (new Utilidades().ValidacionToken() == false)
                {
                    Response.Redirect(@"..\Utilitario\MensajeValidacion.aspx");
                }
                else
                {
                #region recepción de parámetros [modo: normal]
                SalvoconductoID = this.Request.QueryString["SalvoConductoID"];
                SNBloquearSalvoconducto = Convert.ToBoolean(this.Request.QueryString["BloqueoSalvoConducto"]);
                    #endregion
                    //ejecutar métodos normales de tu formulario
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
                                           y.Volumen,
                                       })
                            group x by new { x.NombreComunEspecie, x.NombreEspecie, x.TipoProducto, x.UnidadMedida } into t
                            select new { t.Key.NombreComunEspecie, t.Key.NombreEspecie, t.Key.TipoProducto, t.Key.UnidadMedida, TotalCantidad = t.Sum(y => y.Cantidad), TotalVolumen = t.Sum(y => y.Volumen) };

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
        vSalvoconducto = clsSalvoconductoNew.ConsultaSalvoconductoXSalvoconductoId(SalvoconductoID);
        HtmlAnchor lnk;
        Label lbl;
        //Expedicion
        this.LblFechaExp.Text = "";
        this.LblVigenciaDesde.Text = "";
        this.LblVigenciaHasta.Text = "";
        PersonaDalc per = new PersonaDalc();
        PersonaIdentity p = new PersonaIdentity();
        int autID = 0;
        string autNombre = per.ObtenerAutoridadPorPersona(long.Parse(Session["Usuario"].ToString()), out autID);
        Boolean Sn_SalvoconductoPrecargado = false;
        
        autoridadLogeada = autID;
        if (SNBloquearSalvoconducto && vSalvoconducto.EstadoID == 2)
        {
            // solo una autoridad ambiental puede realizar el bloqueo de un salvoconducto
            if (autID != 0)
            {
                this.BRBloqueo.Visible = true;
                this.TblBloqueoSalvoconducto.Visible = true;
                SILPA.LogicaNegocio.Salvoconducto.AsignarNumeracionSalvoconducto _listaEstadosSeries = new SILPA.LogicaNegocio.Salvoconducto.AsignarNumeracionSalvoconducto();
                Utilidades.LlenarComboTabla(_listaEstadosSeries.ObtenerMotivoBloqueo(), CboMotivoBloqueo, "DESCRIPCION", "ID_TIPO_BLOQUEO", true);
                
            }
            else
                this.TblBloqueoSalvoconducto.Visible = false;
        }
        
        if (vSalvoconducto.EstadoID == 4 && vSalvoconducto.IdTipoBloqueo > 0) //BLOQUEADOS
        {
            this.BRBloqueo.Visible = true;
            this.TblBloqueoSalvoconducto.Visible = true;
            SILPA.LogicaNegocio.Salvoconducto.AsignarNumeracionSalvoconducto _listaEstadosSeries = new SILPA.LogicaNegocio.Salvoconducto.AsignarNumeracionSalvoconducto();
            Utilidades.LlenarComboTabla(_listaEstadosSeries.ObtenerMotivoBloqueo(), CboMotivoBloqueo, "DESCRIPCION", "ID_TIPO_BLOQUEO", true);
            this.CboMotivoBloqueo.Enabled = false;
            this.BtnBloquearSalvoconducto.Visible = false;
            this.CboMotivoBloqueo.SelectedValue = vSalvoconducto.IdTipoBloqueo.ToString(); //seteo el tipo de bloqueo 

            // validamos si el salvoconducto se encuentra bloqueado
            if (vSalvoconducto.EstadoID == 4)
            {
                this.lblAutoridadBloquea.Text = vSalvoconducto.AutoridadCambiaEstado.ToString();
                // solo si la autoridad que bloquea es la misma que esta haciendo la validacion del salvoconducto se da la opcion de desbloquear
                if (vSalvoconducto.AutoridadCambiaEstado == autoridadLogeada)
                {
                    this.tblDesbloqueo.Visible = true;
                }
            }
            else
            {
                this.tblDesbloqueo.Visible = false;
            }
        }

        if (vSalvoconducto.EstadoID == 3) //para los salvocondcutos rechazados muestro el motivo de rechazo
        {
            this.TblMotivoRechazo.Visible = true;
            this.BRMotivoRechazo.Visible = true;
            this.TxtMotivoRechazo.Text = vSalvoconducto.MotivoRechazo.ToString();
            this.ColSerieSalvoconducto.Visible = false;
            this.TituloSerieSalvoconducto.Visible = false;
            this.ColDescCodigoSeguridad.Visible = false;
            this.ColCodigoSeguridad.Visible = false;
            
        }
        else
        {
            LblSerieAsignada.Text = vSalvoconducto.Numero;
            this.LblCodigoSeguridad.Text = vSalvoconducto.CodigoSeguridad.ToString();
            //vigencia del salvoconducto
            if (Convert.ToDateTime(vSalvoconducto.FechaExpedicion.ToString("dd/MM/yyyy").Trim()) >= Convert.ToDateTime("01/01/1900"))
            {
                this.LblFechaExp.Text = vSalvoconducto.FechaExpedicion.ToString("dd/MM/yyyy").Trim(); ;
            }
            if (vSalvoconducto.FechaInicioVigencia != null)
            {
                if (Convert.ToDateTime(vSalvoconducto.FechaInicioVigencia.Value.ToString("dd/MM/yyyy").Trim()) >= Convert.ToDateTime("01/01/1900"))
                {
                    this.LblVigenciaDesde.Text = vSalvoconducto.FechaInicioVigencia.Value.ToString("dd/MM/yyyy").Trim();
                }
            }
            if (vSalvoconducto.FechaFinalVigencia != null)
            if (Convert.ToDateTime(vSalvoconducto.FechaFinalVigencia.Value.ToString("dd/MM/yyyy").Trim()) >= Convert.ToDateTime("01/01/1900"))
            {
                this.LblVigenciaHasta.Text = vSalvoconducto.FechaFinalVigencia.Value.ToString("dd/MM/yyyy").Trim();
            }
        }


        this.LblEstadoSalvoconducto.Text = vSalvoconducto.Estado.ToString().ToUpper();


        //jmartinez se realiza control para los salvocnductos precargados
        if (vSalvoconducto.AutoridadCargueID > 0)
        {
            Sn_SalvoconductoPrecargado = true;
        }

        if (vSalvoconducto.LstRuta != null && vSalvoconducto.LstRuta.Count > 0)
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
        else if (Sn_SalvoconductoPrecargado)
        {
            this.LblDptoExp.Text = "No Aplica";
            this.LblMunicipioExp.Text = "No Aplica";
            this.LblCodigoDptoExp.Text = "No Aplica";
            this.LblCodigoMunExp.Text = "No Aplica";
        }



        //tipo de salvoconducto
        switch (vSalvoconducto.TipoSalvoconductoID)
        {
            case 1:
                LblTipoSalvoconducto.Text = vSalvoconducto.TipoSalvoconducto.ToUpper();
                if (vSalvoconducto.LstAprovechamientoOrigen.Count() == 1)
                {
                    this.TblProcEspecimenesUnico.Visible = true;
                    this.TblProcEspecimenesVarios.Visible = false;
                    Aprovechamiento vAprovechamiento = new Aprovechamiento();
                    vSalvoconducto.Aprovechamiento = vAprovechamiento.ConsultaAprovechamientoXAprovechamientoId(vSalvoconducto.LstAprovechamientoOrigen.First().AprovechamientoID);
                }
                else if (vSalvoconducto.LstAprovechamientoOrigen.Count() > 1)
                {
                    this.TblProcEspecimenesVarios.Visible = true; 
                    this.TblProcEspecimenesUnico.Visible = false;
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
                this.TblSalvoconductosAnteriores.Visible = false;
                break;
            case 2:
                LblTipoSalvoconducto.Text = vSalvoconducto.TipoSalvoconducto.ToUpper();
                this.TblProcEspecimenesUnico.Visible = false; //para el movimiento de removilizacion no se muestra la procedencia de los especimenes
                this.TblSalvoconductosAnteriores.Visible = true;

                if (vSalvoconducto.LstSalvoconductoAnterior != null && vSalvoconducto.LstSalvoconductoAnterior.Count > 0)
                {
                    foreach (var SalvoConductoAnterior in vSalvoconducto.LstSalvoconductoAnterior)
                    {
                        #region creo enlaces por cada salvoconducto
                        lnk = new HtmlAnchor();
                        string parametros = "SalvoConductoID=" + SalvoConductoAnterior.SalvoconductoID.ToString() + "&BloqueoSalvoConducto = false ";
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
                }
                else if (Sn_SalvoconductoPrecargado)
                {

                    lnk = new HtmlAnchor();
                    lnk.InnerHtml = vSalvoconducto.NumeroSUNAnterior;
                    PlaceHolder1.Controls.Add(new LiteralControl("<img src='../App_Themes/Img/ok.png' />&nbsp;"));
                    PlaceHolder1.Controls.Add(lnk);
                    PlaceHolder1.Controls.Add(new LiteralControl("<br />"));
                }


                    break;
            default:
                LblTipoSalvoconducto.Text = vSalvoconducto.TipoSalvoconducto.ToUpper();
                this.TblProcEspecimenesUnico.Visible = false;
                this.TblSalvoconductosAnteriores.Visible = true;
                if (vSalvoconducto.LstSalvoconductoAnterior != null && vSalvoconducto.LstSalvoconductoAnterior.Count > 0)
                {
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
                }
                else if (Sn_SalvoconductoPrecargado)
                {
                    lnk = new HtmlAnchor();
                    lnk.InnerHtml = vSalvoconducto.NumeroSUNAnterior;
                    PlaceHolder1.Controls.Add(new LiteralControl("<img src='../App_Themes/Img/ok.png' />&nbsp;"));
                    PlaceHolder1.Controls.Add(lnk);
                    PlaceHolder1.Controls.Add(new LiteralControl("<br />"));
                }
                break;
        }
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
        LblClaseRecurso.Text = vSalvoconducto.ClaseRecurso.ToUpper();

        //informacion de la obtencion de los especimenes
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
                if (!string.IsNullOrEmpty(vSalvoconducto.Aprovechamiento.Solicitante.NumeroIdentificacion))
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
        }
        else if (Sn_SalvoconductoPrecargado)
        {
            this.TblRutaDesplazamiento.Visible = false;
        }



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

        //informacion de Transporte
        //jmartinez Ajustes salvoconductos precargados
        if (vSalvoconducto.LstTransporte != null && vSalvoconducto.LstTransporte.Count > 0)
        {
            grvTransporte.DataSource = vSalvoconducto.LstTransporte;
            grvTransporte.DataBind();
        }
        else if (Sn_SalvoconductoPrecargado)
        {
            this.TblTransporte.Visible = false;
        }



        //informacion especimenes
        gdvEspecimenes.DataSource = vSalvoconducto.LstEspecimen;
        gdvEspecimenes.DataBind();
        //jmartinez salvoconducto fase 2
        TotalizarCantidadesProdUnidadMedida();

        if (Sn_SalvoconductoPrecargado)
        {
            //this.TblRutaDesplazamiento.Visible = false;
            //this.TblTransporte.Visible = false;
            this.TblProcEspecimenesVarios.Visible = false;
            this.LblModo.Text = vSalvoconducto.ModoAdquisicionRecurso;
            this.LblActoAdministrativo.Text = vSalvoconducto.ActoAdministrativoAprovechamientoSUNLPreimpreso;
            this.LblFechaObtLegalEsp.Text = vSalvoconducto.FechaActoAdministrativoAprovechamientoSUNLPreimpreso.ToString("dd/MM/yyyy").Trim();
            this.LblVeredaProcEsp.Text = vSalvoconducto.VeredaProcedencia.ToString();
            //LblMunicipioExp.Text = "No Aplica";
            //LblCodigoMunExp.Text = "No Aplica";
            //LblDptoExp.Text = "No Aplica";
            //LblCodigoDptoExp.Text = "No Aplica";
            //LblModo.Text = "No Aplica";
            //LblActoAdministrativo.Text = "No Aplica";
            //LblFechaObtLegalEsp.Text = "No Aplica";
            LblNomTitularsObtLegalEsp.Text = vSalvoconducto.TitularAprovechamientoSUNLPreimpreso.ToString();
            LblIdentTitularsObtLegalEsp.Text = vSalvoconducto.IdentificacionTitularAprovechamientoSUNLPreimpreso.ToString();
            LblMunTitularsObtLegalEsp.Text = "No Aplica";
            LblTelTitularsObtLegalEsp.Text = "No Aplica";
            LblDirTitularsObtLegalEsp.Text = "No Aplica";

        }


        if (vSalvoconducto.SalvoconductoAsociados != null && vSalvoconducto.SalvoconductoAsociados.Rows.Count > 0)
        {
            this.tblSalvoRelacionados.Visible = true;
            this.gdvSalvoconductoRelacionados.DataSource = vSalvoconducto.SalvoconductoAsociados;
            this.gdvSalvoconductoRelacionados.DataBind();
        }
        else
        {
            this.tblSalvoRelacionados.Visible = false;
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
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

        if (LstRutaEntity == null)
            LstRutaEntity = new List<RutaEntity>();


        if (this.LstRutaEntity.FirstOrDefault().TipoRutaID == 1)
        {
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
                    IdOrigenDestino = 0
                });
            }
        }
        else if (LstRutaEntity.FirstOrDefault().TipoRutaID == 2)
        {
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
                    IdOrigenDestino = 0
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
                    break;
                case 1:
                    ruta.Orden = 1;
                    break;
                case 2:
                    ruta.Orden = LstRutaEntity.Count();
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
                        break;
                    case 1:
                        ruta.Orden = 1;
                        break;
                    case 2:
                        ruta.Orden = LstRutaEntity.Count();
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


    protected void BtnBloquearSalvoconducto_Click(object sender, EventArgs e)
    {
        BloquearSalvoconducto(Convert.ToInt32(SalvoconductoID));
    }

    public void BloquearSalvoconducto(int salvID)
    {

        if (String.IsNullOrEmpty(CboMotivoBloqueo.SelectedValue))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Info", "alert('Debe Seleccionar Un Motivo de Bloqueo');", true);
            return;
        }
        bool respuesta = false;
        SalvoconductoNew clsSalvoconductoNew = new SalvoconductoNew();
        respuesta = clsSalvoconductoNew.GrabarBloqueoSalvoconducto(Convert.ToInt32(salvID), Convert.ToInt32(this.CboMotivoBloqueo.SelectedValue), Session["Usuario"].ToString(), autoridadLogeada);
        //redirijo la pagina
        string parametros = "SalvoConductoID=" + salvID.ToString() + "&BloqueoSalvoConducto = false";
        string query = Utilidades.Encrypt(parametros);
        string queryEncriptado = "../Salvoconducto/ConsultaDetalleSalvoconducto.aspx" + query;
        string _strPagina = queryEncriptado;
        Response.Redirect(queryEncriptado);


    }
    protected void btnDesbloquear_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            if (this.lblAutoridadBloquea.Text == autoridadLogeada.ToString())
            {
                if (this.fuplSoporteDesbloqueo.HasFile)
                {
                    SalvoconductoNew clsSalvoconductoNew = new SalvoconductoNew();
                    clsSalvoconductoNew.GrabarDesbloqueoSalvoconducto(Convert.ToInt32(SalvoconductoID), this.txtMotivoDesbloqueo.Text, DateTime.Now, Session["Usuario"].ToString(), autoridadLogeada, this.fuplSoporteDesbloqueo.FileBytes);
                    //redirijo la pagina
                    string parametros = "SalvoConductoID=" + SalvoconductoID + "&BloqueoSalvoConducto = true";
                    string query = Utilidades.Encrypt(parametros);
                    string queryEncriptado = "../Salvoconducto/ConsultaDetalleSalvoconducto.aspx" + query;
                    string _strPagina = queryEncriptado;
                    Response.Redirect(queryEncriptado);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Info", "alert('Debe agregar un arhcivo que soporte el desbloqueo');", true);
                }

            }
        }
    }

    protected void gdvEspecimenes_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gdvEspecimenes.PageIndex = e.NewPageIndex;
        gdvEspecimenes.DataSource = vSalvoconducto.LstEspecimen;
        gdvEspecimenes.DataBind();
    }
    protected void gdvSalvoconductoRelacionados_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        if (vSalvoconducto.SalvoconductoAsociados.Rows.Count > 0)
        {
            this.tblSalvoRelacionados.Visible = true;
            this.gdvSalvoconductoRelacionados.PageIndex = e.NewPageIndex;
            this.gdvSalvoconductoRelacionados.DataSource = vSalvoconducto.SalvoconductoAsociados;
            this.gdvSalvoconductoRelacionados.DataBind();
        }
        else
        {
            this.tblSalvoRelacionados.Visible = false;
        }
    }
}