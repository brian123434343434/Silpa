using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SILPA.LogicaNegocio.REDDS;
using SILPA.LogicaNegocio.Generico;
using System.Configuration;
using SILPA.AccesoDatos.BPMProcess;
using System.IO;
using System.Xml.Serialization;
using System.Data;
using SILPA.LogicaNegocio.Salvoconducto;
using SILPA.LogicaNegocio.Aprovechamiento;
using SILPA.AccesoDatos.Generico;
using SILPA.AccesoDatos.Aprovechamiento;
using SILPA.AccesoDatos.Salvoconducto;
using AjaxControlToolkit;
using SILPA.AccesoDatos.RegistroMinero;
using System.Globalization;

public partial class CargueSalfo_SaldoAprovechamiento : System.Web.UI.Page
{
    [Serializable]
    public class Coordenada { public string Puntos { get; set; } public string Grados { get; set; } }
    public List<EspecieAprovechamientoIdentity> LstEspecimen { get { return (List<EspecieAprovechamientoIdentity>)ViewState["LstEspecimen"]; } set { ViewState["LstEspecimen"] = value; } }
    public List<CoordenadaAprovechamientoIndentity> LstCoordenadas { get { return (List<CoordenadaAprovechamientoIndentity>)ViewState["LstCordenadas"]; } set { ViewState["LstCordenadas"] = value; } }
    public LimitesAutoridadAmbiental Limites { get { return (LimitesAutoridadAmbiental)ViewState["Limites"]; } set { ViewState["Limites"] = value; } }
    private string Usuario
    {
        get { return ViewState["Usuario"].ToString(); }
        set { ViewState["Usuario"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            //Session["Usuario"] = 32511;
            //CargarPagina();
            //return;
            if (ValidacionToken() == false)
            {
                Response.Redirect(@"..\Utilitario\MensajeValidacion.aspx");
            }
            else
            {
                CargarPagina();
            }
        }
    }

    public void CargarPagina()
    {
        //jmartinez ajustes expedicion
        RVFechaExpedicion.MinimumValue = "01/01/1900";
        RVFechaExpedicion.MaximumValue = DateTime.Now.Date.ToString("dd/MM/yyyy");

        //jmartinez ajustes expedicion
        PersonaDalc per = new PersonaDalc();
        SILPA.LogicaNegocio.Generico.Listas _listaAutoridades = new SILPA.LogicaNegocio.Generico.Listas();
        SILPA.Comun.Configuracion _configuracion = new SILPA.Comun.Configuracion();
        Aprovechamiento vAprovechamiento = new Aprovechamiento();
        int autID = 0;
        string autNombre = per.ObtenerAutoridadPorPersona(long.Parse(Session["Usuario"].ToString()), out autID);

        Utilidades.LlenarComboTabla(_listaAutoridades.ListarAutoridades(null).Tables[0].AsEnumerable().Where(x => x.Field<int>("AUT_ID") == autID).CopyToDataTable(), cboAutoridadAmbiental, "AUT_NOMBRE", "AUT_ID", true);
        ClaseRecurso vClaseRecurso = new ClaseRecurso();
        Utilidades.LlenarComboLista(vClaseRecurso.ListaClaseRecurso(), cboClaseRecurso, "ClaseRecurso", "ClaseRecursoID", true);
        Utilidades.LlenarComboLista(vAprovechamiento.ListaTipoAprovechamiento(), cboTipoAprovechamiento, "TipoAprovechamiento", "TipoAprovechamientoID", true);
        cboTipoAprovechamiento.SelectedValue = "1";
        cboTipoAprovechamiento.Enabled = false;
        Utilidades.LlenarComboVacio(cboClaseAprovechamiento);
        Utilidades.LlenarComboVacio(cboFormaOtorgamiento);
        Utilidades.LlenarComboVacio(cboModoAdquisicion);
        Utilidades.LlenarComboVacio(cboDepartamento);
        Utilidades.LlenarComboVacio(cboMunicipio);
        Utilidades.LlenarComboVacio(cboClaseProducto);
        Utilidades.LlenarComboVacio(cboTipoProducto);
        Utilidades.LlenarComboVacio(cboUnidadMedida);
        Listas _listaTiposId = new Listas();
        DataSet _temp = _listaTiposId.ListaTipoIdentificacion();
        cboTipoIdentificacion.DataSource = _temp.Tables[0]; // ListaDocumentos(_temp, "TPE_ID = " + Convert.ToString((int)TipoPersona.Natural));
        Utilidades.LlenarComboTabla(_temp.Tables[0], cboTipoIdentificacion, "TID_NOMBRE", "TID_ID", true);

    }
    private void InicializarFormulario()
    {
        this.dgv_Especies.DataSource = null;
        this.dgv_Especies.DataBind();

        this.txtCoordenadas.Text = string.Empty;
        this.dgv_localizaciones.DataSource = null;

        this.cboAutoridadAmbiental.SelectedIndex = 0;
        this.cboClaseRecurso.Enabled = true;
        this.cboClaseRecurso.SelectedIndex = 0;
        Utilidades.LlenarComboVacio(cboClaseAprovechamiento);
        Utilidades.LlenarComboVacio(cboFormaOtorgamiento);
        Utilidades.LlenarComboVacio(cboModoAdquisicion);
        Utilidades.LlenarComboVacio(cboClaseProducto);
        Utilidades.LlenarComboVacio(cboTipoProducto);
        Utilidades.LlenarComboVacio(cboUnidadMedida);
        this.txtNumeroActoAdministrativo.Text = string.Empty;
        this.txtFechaActoAdminstrativo.Text = string.Empty;
        this.txtFechaFinalizacionActoAdminstrativo.Text = string.Empty;
        this.cboDepartamento.SelectedIndex = 0;
        Utilidades.LlenarComboVacio(this.cboMunicipio);
        this.txtCorregimiento.Text = string.Empty;
        this.txtVereda.Text = string.Empty;
        this.txtPredio.Text = string.Empty;
        this.txtSolicitante.Text = string.Empty;
        this.hfIdSolicitante.Value = string.Empty;
        this.dgv_localizaciones.DataBind();
        this.gdvEspecimenes.DataSource = null;
        this.gdvEspecimenes.DataBind();
        this.LstCoordenadas = null;
        this.LstEspecimen = null;
        this.lblArchivo.Visible = false;
        lnkAdicionarArchivo_Click(null, null);
        this.lnkCancelarArchivo.Visible = false;
        this.tblAreaTotalAut.Visible = false;
        //jmartinez salvocondcuto fase 2
        this.tblUbicacionArbolAislado.Visible = false;
        this.txtAreaTotalAut.Text = string.Empty;
        this.GrvTotalesEspecies.DataSource = null;
        this.GrvTotalesEspecies.DataBind();
        TxtDiametroAlturaPecho.Text = string.Empty;
        TxtAlturaComercial.Text = string.Empty;
        Utilidades.LlenarComboVacio(CboTratamientoSilvicultura);
        this.ChkValidaInfoAprovechamiento.Checked = false;
        this.DivChkValidaEspecies.Visible = false;
        this.DivChkValidaCantEspecies.Visible = false;
        this.ChkValidaEspecies.Checked = false;
        this.ChkValidaCntEspecies.Checked = false;
        this.ChkValidaInfoLocalizacion.Checked = false;
        this.LblLstTotalTipProductUm.Visible = false;
        txtNombreEspecie.Text = string.Empty;
        TxtNombreComunEspecie.Text = string.Empty;
    }
    protected void cboDepartamento_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cboDepartamento.SelectedValue == "")
        {
            Utilidades.LlenarComboVacio(cboMunicipio);
            return;
        }
        else
        {
            Listas litMunicipio = new Listas();
            DataTable dtMunicipios = litMunicipio.ListaMunicipios(null, int.Parse(cboDepartamento.SelectedValue), null).Tables[0];
            dtMunicipios = dtMunicipios.AsEnumerable().Where(x => !x.Field<string>("MUN_NOMBRE").Contains("Parque")).CopyToDataTable();
            if (dtMunicipios.AsEnumerable().Where(x => x.Field<int>("AUT_ID") == Convert.ToInt32(this.cboAutoridadAmbiental.SelectedValue)).Count() > 0)
                dtMunicipios = dtMunicipios.AsEnumerable().Where(x => x.Field<int>("AUT_ID") == Convert.ToInt32(this.cboAutoridadAmbiental.SelectedValue)).CopyToDataTable();
            if (cboDepartamento.SelectedValue == "13")
            {
                if (dtMunicipios.AsEnumerable().Where(x => x.Field<int>("AUT_ID") == Convert.ToInt32("195")).Count() > 0)
                    dtMunicipios = dtMunicipios.AsEnumerable().Where(x => x.Field<int>("AUT_ID") == Convert.ToInt32("195")).CopyToDataTable();
            }
            Utilidades.LlenarComboTabla(dtMunicipios, cboMunicipio, "MUN_NOMBRE", "MUN_ID", true);
        }
    }
    protected void dgv_localizaciones_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        LstCoordenadas = null;
        this.dgv_localizaciones.DataSource = LstCoordenadas;
        this.dgv_localizaciones.DataBind();

    }
    protected void btn_adicionar_localizacion_Click(object sender, EventArgs e)
    {
        if (this.rblOpcionCoordenada.SelectedIndex != -1)
        {
            try
            {
                if (LstCoordenadas == null)
                    LstCoordenadas = new List<CoordenadaAprovechamientoIndentity>();
                if (LstCoordenadas.Count == 0)
                {
                    string[] coordenadas = this.txtCoordenadas.Text.Trim().Split(new char[] { ',' });
                    string[] coordenadas2 = this.txtCoordenadas.Text.Trim().Split(new char[] { ',' });

                    if (coordenadas.Length % 2 == 0)
                    {
                        string strCoordenadasErroneas = "";

                        if (this.rblOpcionCoordenada.SelectedValue == "G")
                        {
                            // pasamos de geograficas a lineales
                            coordenadas = PasarPlanasAGeograficas(coordenadas);
                        }

                        if (ValidarCoordenadas(coordenadas,coordenadas2, out strCoordenadasErroneas))
                        {
                            // creamos los objetos de coordenadas
                            for (int i = 0; i < coordenadas.Length; i += 2)
                            {
                                CoordenadaAprovechamientoIndentity oCoor = new CoordenadaAprovechamientoIndentity();
                                oCoor.Norte = Convert.ToDouble(coordenadas[i], CultureInfo.InvariantCulture);
                                oCoor.Este = Convert.ToDouble(coordenadas[i + 1], CultureInfo.InvariantCulture); 
                                LstCoordenadas.Add(oCoor);
                            }
                        }
                        else
                        {

                            ScriptManager.RegisterStartupScript(this, Page.GetType(), "alerta", "<script>alert('Las siguientes coordenadas estan fuera de los limites de la Autoridad:" + strCoordenadasErroneas + "')</script>", false);
                            return;
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, Page.GetType(), "script", "<script>alert('Las coordenadas estan incompletas (Latitud, Longitud)')</script>", false);
                        //is.Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", );
                        return;
                    }

                    int int_cont = 0;
                    List<Coordenada> lstCordenadas = new List<Coordenada>();
                    string puntos = string.Empty;
                    string grados = string.Empty;
                    foreach (CoordenadaAprovechamientoIndentity oCoor in LstCoordenadas)
                    {
                        if (int_cont == 0)
                        {
                            
                            puntos += "N:" + oCoor.Norte.ToString().Replace(",",".") + " " + "E:" + oCoor.Este.ToString().Replace(",",".") + "  ";
                            if (this.rblOpcionCoordenada.SelectedValue == "G")
                            {
                                grados += "N:" + coordenadas2[int_cont] + " E:" + coordenadas2[int_cont + 1];
                            }
                            else
                                grados += "N:" + ConvertirRadianesAGrados(Convert.ToDouble(oCoor.Norte.ToString().Replace(",","."), CultureInfo.InvariantCulture)) + " E:" + ConvertirRadianesAGrados(Convert.ToDouble(oCoor.Este.ToString().Replace(",","."), CultureInfo.InvariantCulture));
                        }
                        else
                        {
                            puntos += "\n \r N:" + oCoor.Norte.ToString().Replace(",",".") + " " + "E:" + oCoor.Este + "  ";
                            if (this.rblOpcionCoordenada.SelectedValue == "G")
                                grados += "\n \r N:" + coordenadas2[int_cont]+ " E:" + coordenadas2[int_cont+1];
                            else
                                grados += "\n \r N:" + ConvertirRadianesAGrados(Convert.ToDouble(oCoor.Norte.ToString().Replace(",","."), CultureInfo.InvariantCulture)) + " E:" + ConvertirRadianesAGrados(Convert.ToDouble(oCoor.Este.ToString().Replace(",","."), CultureInfo.InvariantCulture));
                        }

                        int_cont+=2;
                    }
                    lstCordenadas.Add(new Coordenada { Puntos = puntos, Grados = grados });

                    this.dgv_localizaciones.DataSource = lstCordenadas;
                    this.dgv_localizaciones.DataBind();
                    this.dgv_localizaciones.Visible = true;
                    this.limpiarFormulario();
                }
            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, Page.GetType(), "script", "<script>alert('Se genero el siguiente error:"+ex.Message+"')</script>", false);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, Page.GetType(), "script", "<script>alert('Debe seleccionar un tipo de coordenada para cargar')</script>", false);
        }


    }

    private string[] PasarPlanasAGeograficas(string[] coordenadas)
    {
        string coordenadaAValidar = string.Empty;
        try
        {
            string[] coordenadasPlanas = new string[coordenadas.Length];
            int index = 0;
            foreach (string coordenada in coordenadas)
            {
                coordenadaAValidar = coordenada;
                double grado, minuto, segundo;
                int indexGrado, indexMinuto, indexSeg;
                indexGrado = coordenada.IndexOf("g");
                indexMinuto = coordenada.IndexOf("m");
                indexSeg = coordenada.IndexOf("s");
				
                grado = Convert.ToDouble(coordenada.Substring(0, indexGrado), CultureInfo.InvariantCulture);
                minuto = Convert.ToDouble(coordenada.Substring(indexGrado + 1, 2), CultureInfo.InvariantCulture);
                segundo = Convert.ToDouble(coordenada.Substring(indexMinuto + 1, 2), CultureInfo.InvariantCulture);
				
                coordenadasPlanas[index] = CoordenadaPlana(grado, minuto, segundo);
                index++;
            }
            return coordenadasPlanas;
        }
        catch (Exception)
        {
            throw new Exception("Formato de coordenada geografica incorrecta: " + coordenadaAValidar + ", las coordenadas se deben ingresar en el sistema Geografica, el formato debe ser ##g##m##s  y el orden debe ser Latitud, Longitud separado por coma. Ejemplo 1g20m30s, -72g10m40s ");
        }
    }
    private string CoordenadaPlana(double grados, double minutos, double segundos)
    {
       
        //double dec = (Math.Abs(grados) + (minutos / 60)) + (segundos / 3600);
        //double dec = Math.Round((Math.Abs(grados) + ((minutos * 60) + segundos) / 3600) * 1000000) / 1000000;
        bool esNegativo = false;
        if (grados < 0)
        {
            grados = grados * -1;
            esNegativo = true;
        }
        double dec = grados + minutos / 60 + segundos / 60 / 60;
        if (esNegativo)
            dec = dec * -1;
        return dec.ToString().Replace(",",".");
       
    }

    protected void btnCancelarEdicion_Click(object sender, EventArgs e)
    {
        this.btn_adicionar_localizacion.Text = "Adicionar localización";
        this.btnCancelarEdicion.Visible = false;
        this.dgv_localizaciones.EditIndex = -1;
        limpiarFormulario();
    }
    private bool ValidarCoordenadas(string[] coordenadas,string[] coordenadas2, out string strCoordenadasErroneas)
    {
        int cont = 1;
        int posicionLista = 0;
        bool cumple = true;
        strCoordenadasErroneas = "";
        // validamos si las coordenadas estan dentro de los limites de la autoridad ambiental.
        foreach (string coordenada in coordenadas)
        {
            double dcoordenada = 0;
            if (cont % 2 == 0) // pares son LONGITUDES
            {
                try
                {
                    dcoordenada = Convert.ToDouble(coordenada, CultureInfo.InvariantCulture);
                    if (dcoordenada < 0)
                    {
                        if (!(Limites.LngMin*-1 >= dcoordenada*-1 && dcoordenada*-1 >= Limites.LngMax*-1))
                        {
                            cumple = false;
                            if (this.rblOpcionCoordenada.SelectedValue == "G")
                                strCoordenadasErroneas += "," + coordenadas2[posicionLista];
                            else
                                strCoordenadasErroneas += "," + coordenada;
                        }
                    }
                    else
                    {
                        if (!(Limites.LngMin <= dcoordenada && dcoordenada <= Limites.LngMax))
                        {
                            cumple = false;
                            if (this.rblOpcionCoordenada.SelectedValue == "G")
                                strCoordenadasErroneas += "," + coordenadas2[posicionLista];
                            else
                                strCoordenadasErroneas += "," + coordenada;
                        }
                    }
                }
                catch (Exception)
                {
                    strCoordenadasErroneas = "El formato de las coordenadas esta incorrecto";
                    cumple = false;
                }
            }
            else // inpares son LATITUD
            {
                try
                {
                    dcoordenada = Convert.ToDouble(coordenada, CultureInfo.InvariantCulture);
                    if (!(Limites.LatMin <= dcoordenada && dcoordenada <= Limites.LatMax))
                    {
                        cumple = false;
                        if (this.rblOpcionCoordenada.SelectedValue == "G")
                            strCoordenadasErroneas += "," + coordenadas2[posicionLista];
                        else
                            strCoordenadasErroneas += "," + coordenada;
                    }
                }
                catch (Exception)
                {
                    strCoordenadasErroneas = "El formato de las coordenadas esta incorrecto";
                    cumple = false;
                }
            }
            cont++;
            posicionLista++;
        }
        return cumple;
    }
    protected void limpiarFormulario()
    {
        this.txtCoordenadas.Text = string.Empty;
        this.btnCancelarEdicion.Visible = false;
        this.btn_adicionar_localizacion.Text = "Adicionar localización";
    }
    protected void dgv_localizaciones_DataBound(object sender, EventArgs e)
    {
        ImageButton imb_aux;

        foreach (GridViewRow row in this.dgv_localizaciones.Rows)
        {
            imb_aux = (ImageButton)row.FindControl("imb_borrar");
            imb_aux.Attributes.Add("onclick", "confirmar('¿Está seguro de borrar las Coordenadas?')");
        }
    }
    private string ConvertirRadianesAGrados(double radiandes)
    {
        //tomamos la parte entera del decimal
        string coordenada = "";
        int grados = (int)Math.Truncate(radiandes);
        coordenada = grados.ToString() + "g";
        //restamos los enteros a los radianes
        double ming = double.Parse(radiandes.ToString().Replace(grados.ToString(), "0")) * 60;
        //tomamos la parte entera para obtener los minutos
        int min = (int)Math.Truncate(ming);
        coordenada = coordenada + min.ToString() + "m";
        //restamos los enteros a los radianes
        double segg = double.Parse(ming.ToString().Replace(min.ToString(), "0")) * 60;
        coordenada = coordenada + Math.Round(segg).ToString() + "s";
        return coordenada;

    }
    protected void btnActualizar_Click(object sender, EventArgs e)
    {

        if (!string.IsNullOrEmpty(this.fuplActoAdministrativo.FileName))
        {
            FileInfo fi = new FileInfo(this.fuplActoAdministrativo.FileName);

            if (!fi.ToString().ToUpper().Contains(".PDF"))
            {
                ScriptManager.RegisterStartupScript(this, Page.GetType(), "script", "<script>alert('La Exension del archivo de soporte la obtención del recurso debe ser PDF')</script>", false);
                return;
            }
        }

        if (ValidacionSaldo())
        {
            try
            {
                if (fuplActoAdministrativo.PostedFile != null)
                {
                    AprovechamientoIdentity objAprovechamientoIdentity = new AprovechamientoIdentity();
                    objAprovechamientoIdentity.Numero = this.txtNumeroActoAdministrativo.Text;
                    objAprovechamientoIdentity.FechaExpedicion = Convert.ToDateTime(this.txtFechaActoAdminstrativo.Text);
                    //jmartinez Salvoconducto Fase 2
                    objAprovechamientoIdentity.FechaFinalizacion = Convert.ToDateTime(this.txtFechaFinalizacionActoAdminstrativo.Text);
                    objAprovechamientoIdentity.AutoridadEmisoraID = Convert.ToInt32(this.cboAutoridadAmbiental.SelectedValue);
                    objAprovechamientoIdentity.ClaseRecursoId = Convert.ToInt32(this.cboClaseRecurso.SelectedValue);
                    objAprovechamientoIdentity.CorregimientoProcedencia = this.txtCorregimiento.Text;
                    objAprovechamientoIdentity.DepartamentoProcedenciaID = Convert.ToInt32(this.cboDepartamento.SelectedValue);
                    objAprovechamientoIdentity.TipoAprovechamientoID = Convert.ToInt32(this.cboTipoAprovechamiento.SelectedValue);
                    if (this.cboModoAdquisicion.SelectedValue != string.Empty)
                        objAprovechamientoIdentity.ModoAdquisicionRecursoID = Convert.ToInt32(this.cboModoAdquisicion.SelectedValue);
                    objAprovechamientoIdentity.MunicipioProcedenciaID = Convert.ToInt32(this.cboMunicipio.SelectedValue);
                    if (this.cboFormaOtorgamiento.SelectedValue != string.Empty)
                        objAprovechamientoIdentity.FormatOtorgamientoID = Convert.ToInt32(this.cboFormaOtorgamiento.SelectedValue);
                    objAprovechamientoIdentity.VeredaProcedencia = this.txtVereda.Text;
                    objAprovechamientoIdentity.Predio = this.txtPredio.Text;
                    objAprovechamientoIdentity.SolicitanteID = Convert.ToInt32(this.hfIdSolicitante.Value);
                    objAprovechamientoIdentity.LstEspecies = LstEspecimen;
                    objAprovechamientoIdentity.LstCoordenadas = LstCoordenadas;
                    objAprovechamientoIdentity.UsuarioRegistra = Session["Usuario"].ToString();


                    if (objAprovechamientoIdentity.ModoAdquisicionRecursoID == 8) //arbol aislado
                    {
                        objAprovechamientoIdentity.CodigoUbicacionArbolAislado = Convert.ToInt32(CboUbicacionArbolAislado.SelectedValue);
                    }
                    else
                    {
                        objAprovechamientoIdentity.CodigoUbicacionArbolAislado = 0;
                    }
                    
                    Aprovechamiento vAprovechamiento = new Aprovechamiento();
                    if (this.tblAreaTotalAut.Visible)
                    {
                        if (this.txtAreaTotalAut.Text.Trim() != string.Empty)
                        {
                            objAprovechamientoIdentity.AreaTotalAutorizada = Convert.ToDouble(this.txtAreaTotalAut.Text.Trim().Replace(",", "."), CultureInfo.InvariantCulture);
                        }
                    }
                    string numeroAprovechamiento = vAprovechamiento.CrearAprovechamiento(ref objAprovechamientoIdentity);
                    string rutaArchivo = ConfigurationManager.AppSettings["DireccionFus"] + @"SaldoAprovechamiento\";
                    if (!Directory.Exists(rutaArchivo + numeroAprovechamiento))
                        Directory.CreateDirectory(rutaArchivo + numeroAprovechamiento);
                    fuplActoAdministrativo.SaveAs(rutaArchivo + numeroAprovechamiento + @"\\" + fuplActoAdministrativo.FileName);
                    vAprovechamiento.ActualizarRutaArchivoSaldoAprovechamiento(objAprovechamientoIdentity.AprovechamientoID, rutaArchivo + numeroAprovechamiento + "\\" + fuplActoAdministrativo.FileName);
                    ScriptManager.RegisterStartupScript(this, Page.GetType(), "script", "<script>alert('Nro. Registro " + numeroAprovechamiento + "')</script>", false);
                    InicializarFormulario();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, Page.GetType(), "script", "<script>alert('Debe seleccionar una archivo en donde se soprte la obtención del recurso')</script>", false);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, Page.GetType(), "script", "<script>alert('Error: " + ex.Message + "')</script>", false);
            }
        }
    }
    private bool ValidacionSaldo()
    {
        string mensajeValidacion = "";
        // validamos los participantes


        //jmartinez salvoconducto fase 2
        Aprovechamiento vAprovechamiento = new Aprovechamiento();
        string str_ActoAdministrativo = this.txtNumeroActoAdministrativo.Text;
        str_ActoAdministrativo = str_ActoAdministrativo.Replace(" ", "");

        if (!vAprovechamiento.ValidarActoAdministrativo(str_ActoAdministrativo,Convert.ToInt32(this.cboClaseRecurso.SelectedValue), Convert.ToInt32(this.cboTipoAprovechamiento.SelectedValue), Convert.ToInt32(this.cboAutoridadAmbiental.SelectedValue),Convert.ToDateTime(txtFechaActoAdminstrativo.Text)))
        {
            mensajeValidacion += "<br /> no se puede grabar este aprovechamiento con el acto administrativo " + str_ActoAdministrativo + " para esta autoridad ambiental porque ya existe.";
        }

        if (this.tblAreaTotalAut.Visible == true && Convert.ToDecimal(txtAreaTotalAut.Text) == 0)
        {
            mensajeValidacion += "<br /> El Area Total Autorizada debe ser mayor a cero.";
        }

        if (LstEspecimen == null || LstEspecimen.Count == 0)
        {
            mensajeValidacion += "<br /> Información de especimenes/Debe Ingresar al menos un Especimen.";
        }
        if (LstCoordenadas == null || LstCoordenadas.Count == 0)
        {
            mensajeValidacion += "<br /> Localiación/Debe Ingresar las coordenadas de la ubicación.";
        }
        if (Convert.ToDateTime(txtFechaFinalizacionActoAdminstrativo.Text) < Convert.ToDateTime(txtFechaActoAdminstrativo.Text))
        {
            mensajeValidacion = "<br /> La fecha de Expedicion no puede ser mayor a la fecha de Finalizacion.";
        }

        if (mensajeValidacion != string.Empty)
        {
            lblErrorReds.Text = mensajeValidacion;
            lblErrorReds.Visible = true;
            return false;
        }

     

        lblErrorReds.Visible = false;
        return true;
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
    private ValoresIdentity CargarValores(int id, string grupo, string valor, int orden, Byte[] archivo)
    {
        ValoresIdentity objValores = new ValoresIdentity();
        objValores.Id = id;
        objValores.Grupo = grupo;
        objValores.Valor = valor;
        objValores.Orden = orden;
        objValores.Archivo = archivo;
        return objValores;
    }
    private string CrearXml()
    {
        List<ValoresIdentity> objValoresList = new List<ValoresIdentity>();
        //objValoresList.Add(CargarValores(1, "Bas", this.txtNombreIniciativa.Text, 1, new Byte[1]));
        //objValoresList.Add(CargarValores(2, "Bas", Session["Usuario"].ToString(), 1, new Byte[1]));
        //objValoresList.Add(CargarValores(3, "Bas", this.txtNombreRazonSocial.Text, 1, new Byte[1]));
        objValoresList.Add(CargarValores(4, "Bas", "134", 1, new Byte[1])); // se envia a MADS
        MemoryStream memoryStream = new MemoryStream();
        XmlSerializer serializador = new XmlSerializer(typeof(List<ValoresIdentity>));
        serializador.Serialize(memoryStream, objValoresList);
        string xml = System.Text.UTF8Encoding.UTF8.GetString(memoryStream.ToArray());
        return xml;
    }
    protected void cboClaseRecurso_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.cboClaseRecurso.SelectedValue != string.Empty)
        {
            ClaseAprovechamiento vClaseAprovechamiento = new ClaseAprovechamiento();
            ModoAdquisicionRecurso vModoAdquisicionRecurso = new ModoAdquisicionRecurso();
            ClaseProducto vClaseProducto = new ClaseProducto();
            FormaOtorgamiento vFormaOtorgamiento = new FormaOtorgamiento();
            //Utilidades.LlenarComboLista(vClaseAprovechamiento.ListaClaseAprovechamiento(Convert.ToInt32(this.cboClaseRecurso.SelectedValue)),cboClaseAprovechamiento, "ClaseAprovechamiento", "ClaseAprovechamientoId", true);
            ///JMARTINEZ CASO 21 SE CAMBIA A OTRO METODO LA CARGA DE A EVENTO DEL COMBO  DE FORMA OTRORGAMIENTO
            //Utilidades.LlenarComboLista(vModoAdquisicionRecurso.ListaModoAdquisicionRecurso(Convert.ToInt32(this.cboClaseRecurso.SelectedValue), false), cboModoAdquisicion, "ModAdqRecurso", "ModAdqRecursoID", true);
            Utilidades.LlenarComboLista(vClaseProducto.ListaClaseProducto(Convert.ToInt32(this.cboClaseRecurso.SelectedValue), false), cboClaseProducto, "ClaseProducto", "ClaseProductoID", true);
            Utilidades.LlenarComboLista(vFormaOtorgamiento.ListaFormaOtorgamiento(Convert.ToInt32(this.cboClaseRecurso.SelectedValue), false), cboFormaOtorgamiento, "FormaOtorgamiento", "FormaOtorgamientoID", true);

            switch (Convert.ToInt32(this.cboClaseRecurso.SelectedValue))
            {
                case 1:

                    this.cboClaseAprovechamiento.Visible = false;
                    this.rfvClaseAprovechamiento.Enabled = false;
                    lblNAClaseAprovechamiento.Visible = true;
                    this.cboModoAdquisicion.Visible = true;
                    this.rfvModoAdquisicion.Enabled = true;
                    lblNAModAdqui.Visible = false;
                    this.txtPorcentajeDesperdicio.Text = "0";
                    this.txtPorcentajeDesperdicio.Enabled = true;
                    this.tblAreaTotalAut.Visible = true;
                    this.txtAreaTotalAut.Text = string.Empty;

                    //jmartinez Salvoconducto fase 2
                    this.tblUbicacionArbolAislado.Visible = false;
                    this.trUbicArbolAisladoUrbano.Visible = false;
                    rfvUbicArbolAislado.Enabled = false;
                    RfvTratamientoSilvicultura.Enabled = true;
                    TxtDiametroAlturaPecho.Enabled = true;
                    rfvDiametroAlturaPecho.Enabled = true;

                    //jmartinez 20201105 
                    this.hfEspcimenID.Value = string.Empty;
                    this.txtNombreEspecie.Text = string.Empty;
                    this.TxtNombreComunEspecie.Text = string.Empty;
                    break;
                case 2:

                    this.cboClaseAprovechamiento.Visible = false;
                    this.rfvClaseAprovechamiento.Enabled = false;
                    lblNAClaseAprovechamiento.Visible = true;
                    this.cboModoAdquisicion.Visible = true;
                    this.rfvModoAdquisicion.Enabled = true;
                    lblNAModAdqui.Visible = false;
                    this.txtPorcentajeDesperdicio.Text = "0";
                    this.txtPorcentajeDesperdicio.Enabled = true;
                    this.tblAreaTotalAut.Visible = true;
                    this.txtAreaTotalAut.Text = string.Empty;
                    //jmartinez Salvoconducto fase 2
                    this.tblUbicacionArbolAislado.Visible = false;
                    this.trUbicArbolAisladoUrbano.Visible = false;
                    rfvUbicArbolAislado.Enabled = false;
                    RfvTratamientoSilvicultura.Enabled = true;
                    TxtDiametroAlturaPecho.Enabled = true;
                    rfvDiametroAlturaPecho.Enabled = true;
                    this.hfEspcimenID.Value = string.Empty;
                    this.txtNombreEspecie.Text = string.Empty;
                    this.TxtNombreComunEspecie.Text = string.Empty;
                    break;
                case 3:

                    this.cboClaseAprovechamiento.Visible = false;
                    this.rfvClaseAprovechamiento.Enabled = false;
                    lblNAClaseAprovechamiento.Visible = true;
                    this.cboModoAdquisicion.Visible = true;
                    this.rfvModoAdquisicion.Enabled = true;
                    lblNAModAdqui.Visible = false;
                    this.txtPorcentajeDesperdicio.Text = "0";
                    this.txtPorcentajeDesperdicio.Enabled = false;
                    this.tblAreaTotalAut.Visible = false;
                    this.txtAreaTotalAut.Text = string.Empty;
                    //jmartinez Salvoconducto fase 2
                    this.tblUbicacionArbolAislado.Visible = false;
                    this.trUbicArbolAisladoUrbano.Visible = false;
                    rfvUbicArbolAislado.Enabled = false;
                    RfvTratamientoSilvicultura.Enabled = true;
                    TxtDiametroAlturaPecho.Enabled = true;
                    rfvDiametroAlturaPecho.Enabled = true;
                    this.hfEspcimenID.Value = string.Empty;
                    this.txtNombreEspecie.Text = string.Empty;
                    this.TxtNombreComunEspecie.Text = string.Empty;
                    break;
                case 4:

                    this.cboClaseAprovechamiento.Visible = false;
                    this.rfvClaseAprovechamiento.Enabled = false;
                    lblNAClaseAprovechamiento.Visible = true;
                    this.cboModoAdquisicion.Visible = true;
                    this.rfvModoAdquisicion.Enabled = true;
                    lblNAModAdqui.Visible = false;
                    this.txtPorcentajeDesperdicio.Text = "0";
                    this.txtPorcentajeDesperdicio.Enabled = false;
                    this.tblAreaTotalAut.Visible = false;
                    //jmartinez Salvoconducto fase 2
                    this.tblUbicacionArbolAislado.Visible = false;
                    this.trUbicArbolAisladoUrbano.Visible = false;
                    rfvUbicArbolAislado.Enabled = false;
                    RfvTratamientoSilvicultura.Enabled = true;
                    TxtDiametroAlturaPecho.Enabled = true;
                    rfvDiametroAlturaPecho.Enabled = true;
                    this.hfEspcimenID.Value = string.Empty;
                    this.txtNombreEspecie.Text = string.Empty;
                    this.TxtNombreComunEspecie.Text = string.Empty;
                    break;
                default:
                    //jmartinez Salvoconducto fase 2
                    this.tblUbicacionArbolAislado.Visible = false;
                    this.trUbicArbolAisladoUrbano.Visible = false;
                    rfvUbicArbolAislado.Enabled = false;
                    RfvTratamientoSilvicultura.Enabled = true;
                    TxtDiametroAlturaPecho.Enabled = true;
                    rfvDiametroAlturaPecho.Enabled = true;
                    this.hfEspcimenID.Value = string.Empty;
                    this.txtNombreEspecie.Text = string.Empty;
                    this.TxtNombreComunEspecie.Text = string.Empty;
                    break;
            }
            Utilidades.LlenarComboVacio(cboTipoProducto);
            Utilidades.LlenarComboVacio(cboUnidadMedida);

        }
        else
        {
            Utilidades.LlenarComboVacio(cboClaseAprovechamiento);
            Utilidades.LlenarComboVacio(cboModoAdquisicion);
            Utilidades.LlenarComboVacio(cboTipoProducto);
            Utilidades.LlenarComboVacio(cboUnidadMedida);
            this.tblUbicacionArbolAislado.Visible = false;
        }
    }
    protected void lnkSolicitante_Click(object sender, EventArgs e)
    {
        this.mpeSolicitantes.Show();
    }
    protected void btnBuscarSolicitante_Click(object sender, EventArgs e)
    {
        PersonaDalc persona = new PersonaDalc();
        if (persona.ConsultarPersona(Convert.ToInt32(this.cboTipoIdentificacion.SelectedValue), this.txtNumeroIdentificacion.Text).AsEnumerable().Where(x => x.Field<string>("active") == "T").Count() > 0)
        {
            DataTable datos = persona.ConsultarPersona(Convert.ToInt32(this.cboTipoIdentificacion.SelectedValue), this.txtNumeroIdentificacion.Text).AsEnumerable().Where(x => x.Field<string>("active") == "T").CopyToDataTable();
            if (datos.Rows.Count > 0)
            {
                datos = datos.AsEnumerable().Where(x => x.Field<int>("TIENE_GRUPO") != 0).CopyToDataTable();
                if (datos.Rows.Count > 0)
                {
                    this.lblNombreSolicitante.Text = datos.Rows[0]["NOMBRE"].ToString();
                    this.Usuario = datos.Rows[0]["ID_APPLICATION_USER"].ToString();
                    this.lnkSeleccionarSolicitante.Visible = true;
                }
                else
                {
                    this.lblNombreSolicitante.Text = "";
                    this.lnkSeleccionarSolicitante.Visible = false;
                    this.lnkSeleccionarSolicitante.Visible = false;
                    lblNombreSolicitante.Text = "El usuario debe estar registrado y activo en VITAL";
                }
            }
            else
            {
                this.lblNombreSolicitante.Text = "";
                this.lnkSeleccionarSolicitante.Visible = false;
                this.lnkSeleccionarSolicitante.Visible = false;
                lblNombreSolicitante.Text = "El usuario debe estar registrado y activo en VITAL";
            }
        }
        else
        {
            this.lblNombreSolicitante.Text = "";
            this.lnkSeleccionarSolicitante.Visible = false;
            this.lnkSeleccionarSolicitante.Visible = false;
            lblNombreSolicitante.Text = "El usuario debe estar registrado y activo en VITAL";
        }
        this.mpeSolicitantes.Show();
    }
    protected void lnkSeleccionarSolicitante_Click(object sender, EventArgs e)
    {
        this.txtSolicitante.Text = lblNombreSolicitante.Text;
        this.hfIdSolicitante.Value = Usuario;
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
    protected void LimpiarEspecimen()
    {
        this.txtNombreComun.Text = "";
        this.dgv_Especies.DataSource = null;
        this.dgv_Especies.DataBind();
    }
    protected void lnkEspecie_Click(object sender, EventArgs e)
    {
        if (this.cboClaseRecurso.SelectedValue != string.Empty)
        {
            this.mpeEspecimen.Show();
        }
        else
            this.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "asociacion", "<script>alert('Debe seleccionar una clase de recurso');</script>");
    }
    protected void btnBuscarEspecie_Click(object sender, EventArgs e)
    {
        BuscarEspecie();
    }
    protected void dgv_Especies_RowEditing(object sender, GridViewEditEventArgs e)
    {
        string dataKeys = ((GridView)sender).DataKeys[e.NewEditIndex].Value.ToString();
        Label lblNombreComun = (Label)((GridView)sender).Rows[e.NewEditIndex].FindControl("lblNombreComun");
        string[] especie = { dataKeys, lblNombreComun.Text };
        this.txtNombreEspecie.Text = lblNombreComun.Text;
        this.hfEspcimenID.Value = especie[0];
        this.mpeEspecimen.Hide();
        LimpiarEspecimen();
    }
    protected void dgv_Especies_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.dgv_Especies.PageIndex = e.NewPageIndex;
        BuscarEspecie();
    }
    protected void BuscarEspecie()
    {
        if (this.cboClaseRecurso.SelectedValue != string.Empty && this.txtNombreComun.Text.Trim() != string.Empty)
        {
            Especie vEspecie = new Especie();
            var lstEspecimen = vEspecie.ListaEspecie(this.txtNombreComun.Text, Convert.ToInt32(this.cboClaseRecurso.SelectedValue));

            var lista = (from datos in lstEspecimen
                         select new { ESEPCIE_ID = datos.EspecieID.ToString(), NOMBRE_COMUN = datos.NombreComun, NOMBRE_CIENTIFICO = datos.NombreCientifico });
            this.dgv_Especies.DataSource = lista.ToList();
            this.dgv_Especies.DataBind();
        }
        else
        {
            this.dgv_Especies.DataSource = null;
            this.dgv_Especies.DataBind();
        }
        this.mpeEspecimen.Show();
    }
    protected void cboClaseProducto_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.cboClaseProducto.SelectedValue != string.Empty)
        {
            TipoProducto vTipoProducto = new TipoProducto();
            List<TipoProductoIdentity> ObjLstTipoProductoIdentity = new List<TipoProductoIdentity>();
            ObjLstTipoProductoIdentity = vTipoProducto.ListaTipoProducto(Convert.ToInt32(this.cboClaseProducto.SelectedValue), false);

            //jmartinez 05/11/2020 si el registro del tipo de producto es unico para esta clase de producto se asigna por defecto y se carga las unidades de medida
            if (ObjLstTipoProductoIdentity.Count == 1)
            {
                
                List<UnidadMedidaIdentity> ObjLstUnidadMedidaIdentity = new List<UnidadMedidaIdentity>();
                Utilidades.LlenarComboLista(ObjLstTipoProductoIdentity, this.cboTipoProducto, "TipoProducto", "TipoProductoID", false);

                UnidadMedida vUnidadMedida = new UnidadMedida();
                ObjLstUnidadMedidaIdentity = vUnidadMedida.ListaUnidadMedidaTipoProducto(Convert.ToInt32(this.cboTipoProducto.SelectedValue));

                if (ObjLstUnidadMedidaIdentity.Count == 1)
                {
                    Utilidades.LlenarComboLista(ObjLstUnidadMedidaIdentity, this.cboUnidadMedida, "UnidadMedidad", "UnidadMedidaId", false);
                }
                else if (ObjLstUnidadMedidaIdentity.Count > 1)
                {
                    Utilidades.LlenarComboLista(ObjLstUnidadMedidaIdentity, this.cboUnidadMedida, "UnidadMedidad", "UnidadMedidaId", true);
                }
                
            }
            else if (ObjLstTipoProductoIdentity.Count > 1)
            {
                Utilidades.LlenarComboLista(ObjLstTipoProductoIdentity, this.cboTipoProducto, "TipoProducto", "TipoProductoID", true);
                Utilidades.LlenarComboVacio(this.cboUnidadMedida);
            }
        }
        else
        {
            Utilidades.LlenarComboVacio(this.cboTipoProducto);
            Utilidades.LlenarComboVacio(this.cboUnidadMedida);
        }
    }
    protected void cboTipoProducto_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.cboTipoProducto.SelectedValue != string.Empty)
        {
            UnidadMedida vUnidadMedida = new UnidadMedida();
            Utilidades.LlenarComboLista(vUnidadMedida.ListaUnidadMedidaTipoProducto(Convert.ToInt32(this.cboTipoProducto.SelectedValue)), this.cboUnidadMedida, "UnidadMedidad", "UnidadMedidaId", true);
        }
        else
            Utilidades.LlenarComboVacio(this.cboUnidadMedida);
    }

    /// <summary>
    /// Metodo para calcular el aprovechamiento por producto y unidad de medidad
    /// </summary>
    protected void TotalizarCantidadesProdUnidadMedida()
    {
        if (LstEspecimen.Count > 0)
        {
            var Resultado = from x in (from y in LstEspecimen
                                       select new
                                       {
                                           y.TipoProducto,
                                           y.UnidadMedida,
                                           y.Cantidad,
                                       })
                            group x by new { x.TipoProducto, x.UnidadMedida } into t
                            select new { t.Key.TipoProducto, t.Key.UnidadMedida, Total = t.Sum(y => y.Cantidad) };

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


    protected void btnAgregarEspecie_Click(object sender, EventArgs e)
    {
        //valido que la cantidad asignada + el remanente sea igual al total otorgado
        double CantidadAutoizado = 0;
        double VolumenMovilizar = 0;
        double Remanente = 0;
        double Movido = 0;
        //jmartinez Salvoconducto Fase 2
        double valDiametroAlturaPecho = 0;
        double valAlturaComercial = 0;
        int int_TratamientoSilvicultura = 0;


        if (cboModoAdquisicion.SelectedValue == "8" && this.CboUbicacionArbolAislado.SelectedValue == "2")
        {
            if (!string.IsNullOrEmpty(TxtDiametroAlturaPecho.Text))
            {
                valDiametroAlturaPecho = Convert.ToDouble(this.TxtDiametroAlturaPecho.Text.Trim().Replace(",", "."), CultureInfo.InvariantCulture);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, Page.GetType(), "script", "<script>alert('Debe registrar un valor Diametro Altura Pecho')</script>", false);
                return;
            }

            if (!string.IsNullOrEmpty(TxtAlturaComercial.Text))
            {
                valAlturaComercial = Convert.ToDouble(this.TxtAlturaComercial.Text.Trim().Replace(",", "."), CultureInfo.InvariantCulture);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, Page.GetType(), "script", "<script>alert('Debe registrar un valor Altura Comercial')</script>", false);
                return;
            }
            int_TratamientoSilvicultura = Convert.ToInt32(this.CboTratamientoSilvicultura.SelectedValue);
        }

      


        if (!string.IsNullOrEmpty(this.txtMovido.Text))
        {
            Movido = Convert.ToDouble(this.txtMovido.Text, CultureInfo.InvariantCulture);
        }


        if (!string.IsNullOrEmpty(this.txtCantidadAutorizado.Text))
        {
            CantidadAutoizado = Convert.ToDouble(this.txtCantidadAutorizado.Text, CultureInfo.InvariantCulture);
        }

        if (!string.IsNullOrEmpty(this.txtCantVolTotal.Text))
        {
            VolumenMovilizar = Convert.ToDouble(this.txtCantVolTotal.Text, CultureInfo.InvariantCulture);
        }

        if (!string.IsNullOrEmpty(this.txtCantVolRemanente.Text))
        {
            Remanente = Convert.ToDouble(this.txtCantVolRemanente.Text, CultureInfo.InvariantCulture);
        }

        if ((VolumenMovilizar + Remanente + Movido) != CantidadAutoizado)
        {
            ScriptManager.RegisterStartupScript(this, Page.GetType(), "script", "<script>alert('La Sumatoria de los valores de Cantidad/Volumen movido, Cantidad/Volumen Disponible para mover y Cantidad/Volumen despedicio(remanente) no corresponde al Cantidad/Volumen Autorizado u Otorgado')</script>", false);
            return;
        }

        if (LstEspecimen == null)
            LstEspecimen = new List<EspecieAprovechamientoIdentity>();
        LstEspecimen.Add(new EspecieAprovechamientoIdentity
        {
            EspecieTaxonomiaID = Convert.ToInt32(this.hfEspcimenID.Value),
            NombreEspecie = this.txtNombreEspecie.Text,
            //jmartinez Salvoconducto Fase 2
            NombreComunEspecie = this.TxtNombreComunEspecie.Text,
            UnidadMedidaID = Convert.ToInt32(this.cboUnidadMedida.SelectedValue),
            UnidadMedida = this.cboUnidadMedida.SelectedItem.Text,
            Cantidad = CantidadAutoizado,
            CantidadEspecieLetras = new Utilidades().NumeroALetras(this.txtCantidadAutorizado.Text),
            ClaseProductoID = Convert.ToInt32(this.cboClaseProducto.SelectedValue),
            ClaseProducto = this.cboClaseProducto.SelectedItem.Text,
            TipoProductoID = Convert.ToInt32(this.cboTipoProducto.SelectedValue),
            TipoProducto = this.cboTipoProducto.SelectedItem.Text,
            CantidadVolumenMovilizar = VolumenMovilizar,
            CantidadVolumenRemanente = Remanente,
            CantidadMovido = Movido,
            TratamientoSilviculturaID = int_TratamientoSilvicultura,
            AlturaComercial = valAlturaComercial,
            DiametroAlturaPecho = valDiametroAlturaPecho
        });
        this.gdvEspecimenes.DataSource = LstEspecimen;
        this.gdvEspecimenes.DataBind();
        if (this.LstEspecimen.Count > 0)
        {
            DivChkValidaCantEspecies.Visible = true;
            DivChkValidaEspecies.Visible = true;
            cboClaseRecurso.Enabled = false;
        }
        else
        {
            DivChkValidaCantEspecies.Visible = true;
            DivChkValidaEspecies.Visible = false;
            cboClaseRecurso.Enabled = true;
        }

        //jmartinez salvocnducto Fase 2
        TotalizarCantidadesProdUnidadMedida();
        LimpiarEspecie();

    }
    protected void LimpiarEspecie()
    {
        //jmartinez salvoconducto Fase 2
        this.TxtNombreComunEspecie.Text = string.Empty;
        this.hfEspcimenID.Value = null;
        this.txtNombreEspecie.Text = string.Empty;
        this.cboClaseProducto.SelectedIndex = 0;
        this.lblCantidadLetras.Text = string.Empty;
        this.txtCantidadAutorizado.Text = string.Empty;
        //this.cboUnidadMedida.SelectedIndex = 0;
        //this.cboTipoProducto.SelectedIndex = 0;
        Utilidades.LlenarComboVacio(this.cboTipoProducto);
        Utilidades.LlenarComboVacio(this.cboUnidadMedida);
        this.txtCantVolRemanente.Text = "0";
        this.txtCantVolTotal.Text = string.Empty;
        this.txtMovido.Text = string.Empty;
        this.txtPorcentajeDesperdicio.Text = "0";
        this.TxtDiametroAlturaPecho.Text = "0";
        this.TxtAlturaComercial.Text = "0";
    }

    protected void gdvEspecimenes_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string id = this.gdvEspecimenes.DataKeys[e.RowIndex].Value.ToString();
        LstEspecimen.Remove(LstEspecimen.Where(x => x.EspecieTaxonomiaID == Convert.ToInt32(id)).First());
        this.gdvEspecimenes.DataSource = LstEspecimen;
        this.gdvEspecimenes.DataBind();
        if (this.LstEspecimen.Count > 0)
        {
            //jmartinez salvoconducto Fase 2
            DivChkValidaCantEspecies.Visible = true;
            DivChkValidaEspecies.Visible = true;
            cboClaseRecurso.Enabled = false;
        }
        else
        {
            DivChkValidaCantEspecies.Visible = false;
            DivChkValidaEspecies.Visible = false;
            cboClaseRecurso.Enabled = true;
        }
        TotalizarCantidadesProdUnidadMedida();

    }

    /// <summary>
    /// Evento que se ejecuta cuando se da clic en Modificar Archivo. Muestra el control de carga de archivo y cancelar, y oculta los demas controles
    /// </summary>
    protected void lnkAdicionarArchivo_Click(object sender, EventArgs e)
    {
        AsyncFileUpload objFileUpload = null;
        Label objLabel = null;
        LinkButton objLinkAdicionar = null;
        LinkButton objLinkCancelar = null;
        HyperLink objLinkVerArchivo = null;

        try
        {
            //Cargar controles de la fila
            objFileUpload = fuplActoAdministrativo;
            objLinkAdicionar = lnkAdicionarArchivo;
            objLinkCancelar = lnkCancelarArchivo;
            objLinkVerArchivo = lnkVerArchivo;
            objLabel = lblArchivo;

            //Mostrar y ocultar controles
            objFileUpload.Visible = true;
            objLabel.Visible = false;
            objLinkAdicionar.Visible = false;
            objLinkCancelar.Visible = true;
            objLinkCancelar.Text = "Cancelar";

            //Ocultar boton de ver archivo
            if (objLinkVerArchivo != null)
            {
                objLinkVerArchivo.Visible = false;
            }
        }
        catch (Exception exc)
        {
            ////Escribir error
            //SMLog.Escribir(Severidad.Critico, "PDV_CrearCertificado :: lnkAdicionarArchivo_Click -> Error Inesperado: " + exc.Message + " " + exc.StackTrace);

            ////Cargar mensaje de error                        
            //this.MostrarMensaje("Se genero un error habilitando adición de archivo. Si el error sigue presentandose por favor comunicarse con el Administrador del Sistema");
        }
        finally
        {
            //Regenerar listados
            //RecargarListados();
        }
    }


    /// <summary>
    /// Evento que se ejecuta cuando se da clic en Cancelar. Muestra el control de Modificar Archivo, y oculta los demas controles
    /// </summary>
    protected void lnkCancelarArchivo_Click(object sender, EventArgs e)
    {
        AsyncFileUpload objFileUpload = null;
        Label objLabel = null;
        LinkButton objLinkAdicionar = null;
        LinkButton objLinkCancelar = null;
        HyperLink objLinkVerArchivo = null;

        try
        {
            //Cargar controles de la fila
            objFileUpload = fuplActoAdministrativo;
            objLinkAdicionar = lnkAdicionarArchivo;
            objLinkCancelar = lnkCancelarArchivo;
            objLinkVerArchivo = lnkVerArchivo;
            objLabel = lblArchivo;
            //Mostrar y ocultar controles                    
            if (objFileUpload.HasFile)
            {
                objFileUpload.Visible = false;
                objLabel.Visible = true;
                objLabel.Text = objFileUpload.FileName;
                objLinkAdicionar.Visible = true;
                objLinkCancelar.Visible = false;
                objLinkCancelar.Text = "Cancelar";
                if (objLinkVerArchivo != null)
                {
                    objLinkVerArchivo.Visible = false;
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(objLinkCancelar.Text))
                {
                    objFileUpload.Visible = false;
                    objLabel.Visible = true;
                    objLinkAdicionar.Visible = true;
                    objLinkCancelar.Visible = false;
                    objLinkCancelar.Text = "Cancelar";
                    if (objLinkVerArchivo != null)
                    {
                        objLinkVerArchivo.Visible = true;
                    }
                }
                else
                {
                    objFileUpload.Visible = true;
                    objLabel.Visible = false;
                    objLinkAdicionar.Visible = false;
                    objLinkCancelar.Visible = false;
                    if (objLinkVerArchivo != null)
                    {
                        objLinkVerArchivo.Visible = false;
                    }
                }
            }
        }
        catch (Exception exc)
        {
            //Escribir error
            //SMLog.Escribir(Severidad.Critico, "PDV_CrearCertificado :: lnkCancelarArchivo_Click -> Error Inesperado: " + exc.Message + " " + exc.StackTrace);

            ////Cargar mensaje de error                        
            //this.MostrarMensaje("Se genero un error cancelando adjuntar archivo. Si el error sigue presentandose por favor comunicarse con el Administrador del Sistema");
        }
        finally
        {
            //Regenerar listados
            //RecargarListados();
        }
    }

    protected void cboAutoridadAmbiental_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.cboAutoridadAmbiental.SelectedValue != "")
        {
            SILPA.LogicaNegocio.Generico.Listas _listaAutoridades = new SILPA.LogicaNegocio.Generico.Listas();
            SILPA.Comun.Configuracion _configuracion = new SILPA.Comun.Configuracion();

            DataTable dtDepartamentos = _listaAutoridades.ListarDepartamentosPorAutoridadAmbiental(_configuracion.IdPaisPredeterminado, Convert.ToInt32(this.cboAutoridadAmbiental.SelectedValue)).Tables[0];
            if(this.cboAutoridadAmbiental.SelectedValue == "144")
            {
                DataRow row = dtDepartamentos.NewRow();
                row["DEP_ID"] = "13";
                row["DEP_NOMBRE"] = "BOLIVAR";
                dtDepartamentos.Rows.Add(row);
            }

            Utilidades.LlenarComboTabla(dtDepartamentos, cboDepartamento, "DEP_NOMBRE", "DEP_ID", true);
            Utilidades.LlenarComboVacio(this.cboMunicipio);
            Limites = new LimitesAutoridadAmbiental(Convert.ToInt32(this.cboAutoridadAmbiental.SelectedValue)).ConsultaLimitesAutoridadAmbiental();
        }
        else
        {
            Utilidades.LlenarComboVacio(this.cboDepartamento);
            Utilidades.LlenarComboVacio(this.cboMunicipio);
            Limites = null;
        }
    }
    protected void txtPorcentajeDesperdicio_TextChanged(object sender, EventArgs e)
    {
        //CalcularDesperdicio();
    }
    private void CalcularDesperdicio()
    {
        Double DporcentajeDesperdicio = 0.0;
        double DVolumenMovido = 0.0;
        double DVolumenAutorizado = 0.0;
        double DVolumenDesperdicio = 0.0;
        if (this.txtPorcentajeDesperdicio.Text.Trim() != string.Empty)
        {
            DporcentajeDesperdicio = Convert.ToDouble(this.txtPorcentajeDesperdicio.Text.Replace(".", ",").ToString());
        }
        if (this.txtMovido.Text.Trim() != string.Empty)
            DVolumenMovido = Convert.ToDouble(this.txtMovido.Text.Replace(".", ",").ToString());
        if (this.txtCantidadAutorizado.Text.Trim() != string.Empty)
            DVolumenAutorizado = Convert.ToDouble(this.txtCantidadAutorizado.Text.Replace(".", ",").ToString());

        DVolumenDesperdicio = ((DVolumenAutorizado - DVolumenMovido) * DporcentajeDesperdicio) / 100;
        this.txtCantVolTotal.Text = DVolumenDesperdicio.ToString();

    }
    protected void rblOpcionCoordenada_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblOpcionCoordenada.SelectedIndex != -1)
        {
            switch (rblOpcionCoordenada.SelectedValue)
            {
                case "L":
                    this.txtCoordenadas.ToolTip = "Las coordenadas se deben ingresar en el sistema WGS84, el formato del numero debe ser Decimal utilizando el punto como separador y el orden debe ser Latitud, Longitud separado por coma. Ejemplo 1.05987,-66.056987";
                    break;
                case "G":
                    this.txtCoordenadas.ToolTip = "Las coordenadas se deben ingresar en el sistema Geografica, el formato debe ser ##g##m##s  y el orden debe ser Latitud, Longitud separado por coma. Ejemplo 1g20m30s, -72g10m40s";
                    break;
                default:
                    break;
            }

        }
    }

    protected void cboModoAdquisicion_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch (cboModoAdquisicion.SelectedValue)
        {
            case "8": //Ubicacion Arbol Aislado
                Aprovechamiento vAprovechamiento = new Aprovechamiento();
                Utilidades.LlenarComboVacio(CboTratamientoSilvicultura);
                this.tblUbicacionArbolAislado.Visible = true;
                CboUbicacionArbolAislado.Visible = true;
                rfvUbicArbolAislado.Enabled = true;
                Utilidades.LlenarComboLista(vAprovechamiento.ListarUbicacionArbolAislado(), CboUbicacionArbolAislado, "Descripcion", "CodUbicArbolAislado", true);
                break;

            default:
                this.tblUbicacionArbolAislado.Visible = false;
                CboUbicacionArbolAislado.Visible = false;
                rfvUbicArbolAislado.Enabled = false;
                Utilidades.LlenarComboVacio(CboUbicacionArbolAislado);
                break;
        }
    }



    protected void CboUbicacionArbolAislado_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch (this.CboUbicacionArbolAislado.SelectedValue)
        {
            case "1": //rural
                this.trUbicArbolAisladoUrbano.Visible = false;
                TxtDiametroAlturaPecho.Enabled = false;
                rfvDiametroAlturaPecho.Enabled = false;
                TxtAlturaComercial.Enabled = false;
                RfvAlturaComercial.Enabled = false;
                TxtDiametroAlturaPecho.Text = string.Empty;
                TxtAlturaComercial.Text = string.Empty;
                this.CboTratamientoSilvicultura.Enabled = false;
                RfvTratamientoSilvicultura.Enabled = false;
                Utilidades.LlenarComboVacio(CboTratamientoSilvicultura);
                break;

            case "2": //urbano
                Aprovechamiento vAprovechamiento = new Aprovechamiento();
                Utilidades.LlenarComboVacio(CboTratamientoSilvicultura);
                Utilidades.LlenarComboLista(vAprovechamiento.ListarTratamientoSilvicultura(), CboTratamientoSilvicultura, "Descripcion", "CodTratamientoSilvicultura", true);
                this.trUbicArbolAisladoUrbano.Visible = true;
                TxtDiametroAlturaPecho.Enabled = true;
                rfvDiametroAlturaPecho.Enabled = true;
                TxtAlturaComercial.Enabled = true;
                RfvAlturaComercial.Enabled = true;
                this.CboTratamientoSilvicultura.Enabled = true;
                RfvTratamientoSilvicultura.Enabled = true;
                TxtDiametroAlturaPecho.Text = string.Empty;
                TxtAlturaComercial.Text = string.Empty;
                break;

            default:
                break;
        }
    }


    /// <summary>
    /// JMARTINEZ CASO 21 SE COLOCA EVENTO PARA VALIDACION DE DATO PARA LA FORMA DE OTRORGAMIENTO PARA CERCAS VIVASD
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cboFormaOtorgamiento_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClaseAprovechamiento vClaseAprovechamiento = new ClaseAprovechamiento();
        ModoAdquisicionRecurso vModoAdquisicionRecurso = new ModoAdquisicionRecurso();
        ClaseProducto vClaseProducto = new ClaseProducto();
        FormaOtorgamiento vFormaOtorgamiento = new FormaOtorgamiento();

        switch (this.cboFormaOtorgamiento.SelectedValue)
        {
            case "10":
                cboTipoAprovechamiento.SelectedValue = "7";
                cboTipoAprovechamiento.Enabled = false;
                break;

            case "11":
                cboTipoAprovechamiento.SelectedValue = "8";
                cboTipoAprovechamiento.Enabled = false;
                break;

            default:
                cboTipoAprovechamiento.SelectedValue = "1";
                cboTipoAprovechamiento.Enabled = false;
                break;
        }

        Utilidades.LlenarComboLista(vModoAdquisicionRecurso.ListaModoAdquisicionRecurso(Convert.ToInt32(this.cboClaseRecurso.SelectedValue), false, Convert.ToInt32(this.cboFormaOtorgamiento.SelectedValue)), cboModoAdquisicion, "ModAdqRecurso", "ModAdqRecursoID", true);
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        InicializarFormulario();
    }
}
