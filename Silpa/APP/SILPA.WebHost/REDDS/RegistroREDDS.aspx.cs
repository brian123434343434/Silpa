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
using Subgurim.Controles;
using System.Data;

public partial class REDDS_RegistroREDDS : System.Web.UI.Page
{
    public List<SILPA.AccesoDatos.REDDS.ReddsParticipante> LstParticipantes { get { return (List<SILPA.AccesoDatos.REDDS.ReddsParticipante>)ViewState["LstParticipantes"]; } set { ViewState["LstParticipantes"] = value; } }
    public List<SILPA.AccesoDatos.REDDS.ReddsEstimadoReduccionEmisiones> LstEstimadoReduccionEmisiones { get { return (List<SILPA.AccesoDatos.REDDS.ReddsEstimadoReduccionEmisiones>)ViewState["LstEstimadoReduccionEmisiones"]; } set { ViewState["LstEstimadoReduccionEmisiones"] = value; } }
    public List<SILPA.AccesoDatos.REDDS.ReddsActividad> LstActividad { get { return (List<SILPA.AccesoDatos.REDDS.ReddsActividad>)ViewState["LstActividad"]; } set { ViewState["LstActividad"] = value; } }
    public List<SILPA.AccesoDatos.REDDS.ReddsCompartimientoCarbono> LstCompartimentoCarbono { get { return (List<SILPA.AccesoDatos.REDDS.ReddsCompartimientoCarbono>)ViewState["LstCompartimentoCarbono"]; } set { ViewState["LstCompartimentoCarbono"] = value; } }
    public List<SILPA.AccesoDatos.REDDS.ReddsAutoridadAmbiental> LstAutoridades { get { return (List<SILPA.AccesoDatos.REDDS.ReddsAutoridadAmbiental>)ViewState["LstAutoridades"]; } set { ViewState["LstAutoridades"] = value; } }
    public List<SILPA.AccesoDatos.REDDS.ReddsDeptoMunicipio> LstDeptoMunicipio { get { return (List<SILPA.AccesoDatos.REDDS.ReddsDeptoMunicipio>)ViewState["LstDeptoMunicipio"]; } set { ViewState["LstDeptoMunicipio"] = value; } }
    public List<SILPA.AccesoDatos.REDDS.ReddsArchivos> LstArchivo { get { return (List<SILPA.AccesoDatos.REDDS.ReddsArchivos>)ViewState["LstArchivo"]; } set { ViewState["LstArchivo"] = value; } }
    public List<SILPA.AccesoDatos.REDDS.ReddsLocalizacion> LstLocalizacion { get { return (List<SILPA.AccesoDatos.REDDS.ReddsLocalizacion>)ViewState["LstLocalizacion"]; } set { ViewState["LstLocalizacion"] = value; } }
    public List<SILPA.AccesoDatos.REDDS.ReddsEstimadoReduccionDeforestacion> LstEstimadoReduccionDeforestacion { get { return (List<SILPA.AccesoDatos.REDDS.ReddsEstimadoReduccionDeforestacion>)ViewState["LstEstimadoReduccionDeforestacion"]; } set { ViewState["LstEstimadoReduccionDeforestacion"] = value; } }
    public List<SILPA.AccesoDatos.REDDS.ReddsEstandarMercado> LstEstandarMercado { get { return (List<SILPA.AccesoDatos.REDDS.ReddsEstandarMercado>)ViewState["LstEstandarMercado"]; } set { ViewState["LstEstandarMercado"] = value; } }
    public int localizacionID { get { return (int)ViewState["localizacionID"]; } set { ViewState["localizacionID"] = value; } }

    protected void Page_Load(object sender, EventArgs e)
    {
        ProcesarFileUploadAjax();
       
        if (!IsPostBack)
        {
            //Session["Usuario"] = 429;
            if (ValidacionToken() == false)
            {
                Response.Redirect(@"..\Utilitario\MensajeValidacion.aspx");
            }
            else
            {
                CargarPagina();
                localizacionID = 0;
            }
        }
    }

    private void ProcesarFileUploadAjax()
    {
        this.managePost();
    }

    private void managePost()
    {
        HttpPostedFileAJAX Archivo = new HttpPostedFileAJAX();
        if (LstArchivo == null)
            LstArchivo = new List<SILPA.AccesoDatos.REDDS.ReddsArchivos>();
        if (fulaOtros.IsPosting)
        {
            Archivo = fulaOtros.PostedFile;
            fulaOtros.SaveAs("~/temp", Archivo.FileName);
        }
        if (fuplPDDoPIN.IsPosting)
        {
            Archivo = fuplPDDoPIN.PostedFile;
            fuplPDDoPIN.SaveAs("~/temp", Archivo.FileName);
        }
        if (fuplShape.IsPosting)
        {
            Archivo = fuplShape.PostedFile;
            fuplShape.SaveAs("~/temp", Archivo.FileName);
        }
    }
    public void CargarPagina()
    {
        SILPA.Comun.Configuracion _configuracion = new SILPA.Comun.Configuracion();
        Redds clsRedds = new Redds();
        Utilidades.LlenarComboLista(clsRedds.ConsultaListaEstadosIniciativa(), this.cboEstadoIniciativa, "NombreEstado", "EstadoID", true);
        Utilidades.LlenarComboLista(clsRedds.ConsultaListaActividades(), this.cboActividadREDDS, "NombreActividad", "ActividadID", true);
        Utilidades.LlenarComboLista(clsRedds.ConsultaListaCompartimientoCarbono(), this.cboCompartimientos, "NombreCompartimiento", "CompartimientoID", true);
        Utilidades.LlenarComboTabla(clsRedds.ConsultaRelacionJuridica(), this.cboRelacionJuridica, "NOMBRE_RELACION_JURIDICA", "RELACION_JURIDICA_ID", true);
        Listas listaAutoridades = new Listas();
        DataTable dtAutoridades = listaAutoridades.ListarAutoridadesActivas().Tables[0].AsEnumerable().Where(x => !x.Field<string>("AUT_NOMBRE").Contains("PNN") 
            && !x.Field<string>("AUT_NOMBRE").Contains("ANLA") 
            && !x.Field<string>("AUT_NOMBRE").Contains("Ministerio del Interior")).CopyToDataTable();
        Utilidades.LlenarComboTabla(dtAutoridades, cboAutoridadAmbiental, "AUT_NOMBRE", "AUT_ID", true);
        Utilidades.LlenarComboTabla(listaAutoridades.ListarDepartamentos(_configuracion.IdPaisPredeterminado).Tables[0], cboDepartamento, "DEP_NOMBRE", "DEP_ID", true);
        cboMunicipio.Items.Clear();
        cboMunicipio.Items.Insert(0, new ListItem("Seleccione.", ""));
    }
    protected void cboDepartamento_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cboDepartamento.SelectedValue == "")
        {
            cboMunicipio.Items.Clear();
            cboMunicipio.Items.Insert(0, new ListItem("Seleccione.", ""));
            return;
        }
        else
        {
            Listas litMunicipio = new Listas();
            DataTable dtMunicipios = litMunicipio.ListaMunicipios(null, int.Parse(cboDepartamento.SelectedValue), null).Tables[0];
            dtMunicipios = dtMunicipios.AsEnumerable().Where(x => !x.Field<string>("MUN_NOMBRE").Contains("Parque")).CopyToDataTable();
            Utilidades.LlenarComboTabla(dtMunicipios, cboMunicipio, "MUN_NOMBRE", "MUN_ID", true);
            cboMunicipio.Items.Insert(1, new ListItem("(Todos)", "-1"));
        }
    }
    protected void btnAgregarParticipante_Click(object sender, EventArgs e)
    {
        if (LstParticipantes == null)
            LstParticipantes = new List<SILPA.AccesoDatos.REDDS.ReddsParticipante>();
        bool bln_adicionar = true;

        if (LstParticipantes.Count < 10)
        {
            // Verifica si esta repetido
            string participante = this.txtNombreParticipante.Text.Trim();
            foreach (ListItem li in this.lstParticipante.Items)
            {
                if (li.Value.ToUpper() == participante.ToUpper())
                {
                    bln_adicionar = false;
                    return;
                }
            }

            if (bln_adicionar)
            {
                LstParticipantes.Add(new SILPA.AccesoDatos.REDDS.ReddsParticipante { NombreParticipante = participante });
                this.lstParticipante.DataTextField = "NombreParticipante";
                this.lstParticipante.DataSource = LstParticipantes;
                this.lstParticipante.DataBind();
                this.txtNombreParticipante.Text = string.Empty;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, Page.GetType(), "Repetido", "<script>alert('Este item ya ha sido adicionado!');</script>", false);
            }
        }
    }
    protected void btnQuitarParticipante_Click(object sender, EventArgs e)
    {
        if (this.lstParticipante.SelectedIndex != -1)
        {
            LstParticipantes.Remove(LstParticipantes[this.lstParticipante.SelectedIndex]);
            this.lstParticipante.DataSource = LstParticipantes;
            this.lstParticipante.DataBind();
        }
        
    }
    protected void btnAgregarEmision_Click(object sender, EventArgs e)
    {
        if (LstEstimadoReduccionEmisiones == null)
            LstEstimadoReduccionEmisiones = new List<SILPA.AccesoDatos.REDDS.ReddsEstimadoReduccionEmisiones>();

        // calculamos los años de diferencia entre las fechas de la iniciativa
        DateTime dtmFechaInico, dtmfechaFin;
        dtmFechaInico = Convert.ToDateTime(this.txtFechaInicioIniciativa.Text);
        dtmfechaFin = Convert.ToDateTime(this.txtFechaFinalIniciativa.Text);

        int numeroRegistrosAño = (dtmfechaFin.Year - dtmFechaInico.Year) + 1;
        
        int numeroEmisiones = Convert.ToInt32(this.txtValorEmisiones.Text);

        int numeroVerificaciones = Convert.ToInt32(this.txtValorVerificadas.Text);

        int contAños = LstEstimadoReduccionEmisiones.Count;
        if (contAños == 0)
        {
            contAños = 1;
        }
        else
        {
            contAños++;
        }
        if(LstEstimadoReduccionEmisiones.Where(x=> x.Año == Convert.ToInt32(this.cboNumeroAños.SelectedValue)).Count() == 0)
            LstEstimadoReduccionEmisiones.Add(new SILPA.AccesoDatos.REDDS.ReddsEstimadoReduccionEmisiones { Año = Convert.ToInt32(this.cboNumeroAños.SelectedValue), Valor = numeroEmisiones, ValorVerificacion = numeroVerificaciones });

        var lstEmisionesDetalle = LstEstimadoReduccionEmisiones.Select(x => new { Detalle = string.Format("Año {0}: Emisiones Reducidas: {1} | Emisiones verificadas: {2}", x.Año, x.Valor, x.ValorVerificacion) }).ToList();
        this.lstEmisiones.DataTextField = "Detalle";
        this.lstEmisiones.DataSource = lstEmisionesDetalle;
        this.lstEmisiones.DataBind();
        this.txtValorEmisiones.Text = string.Empty;
        this.txtValorVerificadas.Text = string.Empty;
    }
    protected void btnAgregarEmisionDeforestacion_Click(object sender, EventArgs e)
    {
        if (LstEstimadoReduccionDeforestacion == null)
            LstEstimadoReduccionDeforestacion = new List<SILPA.AccesoDatos.REDDS.ReddsEstimadoReduccionDeforestacion>();

        // calculamos los años de diferencia entre las fechas de la iniciativa
        DateTime dtmFechaInico, dtmfechaFin;
        dtmFechaInico = Convert.ToDateTime(this.txtFechaInicioIniciativa.Text);
        dtmfechaFin = Convert.ToDateTime(this.txtFechaFinalIniciativa.Text);

        int numeroRegistrosAño = (dtmfechaFin.Year - dtmFechaInico.Year) + 1;

        int numeroEmisionesDeforestacion = Convert.ToInt32(this.txtValorEstimadoDeforestacion.Text);

        int contAños = LstEstimadoReduccionDeforestacion.Count;
        if (contAños == 0)
        {
            contAños = 1;
        }
        else
        {
            contAños++;
        }
        if (LstEstimadoReduccionDeforestacion.Where(x => x.Año == Convert.ToInt32(this.cboNumeroAñosDeforestacion.SelectedValue)).Count() == 0)
            LstEstimadoReduccionDeforestacion.Add(new SILPA.AccesoDatos.REDDS.ReddsEstimadoReduccionDeforestacion { Año = Convert.ToInt32(this.cboNumeroAñosDeforestacion.SelectedValue), Valor = numeroEmisionesDeforestacion });

        var lstEmisionesDetalle = LstEstimadoReduccionDeforestacion.Select(x => new { Detalle = string.Format("Año {0}: {1} Ha de deforestación evitada", x.Año, x.Valor) }).ToList();
        this.lstEmisionesDeforestacion.DataTextField = "Detalle";
        this.lstEmisionesDeforestacion.DataSource = lstEmisionesDetalle;
        this.lstEmisionesDeforestacion.DataBind();
        this.txtValorEstimadoDeforestacion.Text = string.Empty;
    }
    protected void btnQuitarEmision_Click(object sender, EventArgs e)
    {
        if (this.lstEmisiones.SelectedIndex != -1)
        {
            LstEstimadoReduccionEmisiones.Remove(LstEstimadoReduccionEmisiones[this.lstEmisiones.SelectedIndex]);
            var lstEmisionesDetalle = LstEstimadoReduccionEmisiones.Select(x => new { Detalle = string.Format("Año {0}: Emisiones Reducidas: {1} | Emisiones verificadas: {2}", x.Año, x.Valor, x.ValorVerificacion) }).ToList();
            this.lstEmisiones.DataTextField = "Detalle";
            this.lstEmisiones.DataSource = lstEmisionesDetalle;
            this.lstEmisiones.DataBind();
        }
    }
    protected void btnQuitarEmisionDeforestacion_Click(object sender, EventArgs e)
    {
        if (this.lstEmisionesDeforestacion.SelectedIndex != -1)
        {
            LstEstimadoReduccionDeforestacion.Remove(LstEstimadoReduccionDeforestacion[this.lstEmisionesDeforestacion.SelectedIndex]);
            var lstEmisionesDetalle = LstEstimadoReduccionDeforestacion.Select(x => new { Detalle = string.Format("Año {0}: {1} Ha de deforestación evitada", x.Año, x.Valor) }).ToList();
            this.lstEmisionesDeforestacion.DataTextField = "Detalle";
            this.lstEmisionesDeforestacion.DataSource = lstEmisionesDetalle;
            this.lstEmisionesDeforestacion.DataBind();
        }
    }
    protected void txtFechaFinalIniciativa_TextChanged(object sender, EventArgs e)
    {
          this.cboNumeroAños.Items.Clear();
          int AñoIni= 0, AñoFin= 0;
            if (txtFechaInicioIniciativa.Text != "") {
                AñoIni = Convert.ToDateTime(txtFechaInicioIniciativa.Text).Year;
            }
            if (txtFechaFinalIniciativa.Text != "") {
                AñoFin = Convert.ToDateTime(txtFechaFinalIniciativa.Text).Year;
            }
            int numeroAños = (AñoFin - AñoIni) + 1;
            for (var i = 1; i <= numeroAños; i++) {
                cboNumeroAños.Items.Add(new ListItem(i.ToString(),i.ToString()));
                cboNumeroAñosDeforestacion.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
    }
    protected void btnAgregarActividad_Click(object sender, EventArgs e)
    {
        if (LstActividad == null)
            LstActividad = new List<SILPA.AccesoDatos.REDDS.ReddsActividad>();
        bool bln_adicionar = true;

        if (LstActividad.Count < 5)
        {
            if (LstActividad.Where(x => x.ActividadID == Convert.ToInt32(this.cboActividadREDDS.SelectedValue)).Count() > 0)
            {
                bln_adicionar = false;
                return;
            }

            if (bln_adicionar)
            {
                LstActividad.Add(new SILPA.AccesoDatos.REDDS.ReddsActividad { ActividadID = Convert.ToInt32(this.cboActividadREDDS.SelectedValue), NombreActividad = this.cboActividadREDDS.SelectedItem.Text });
                this.lstActividadRedds.DataTextField = "NombreActividad";
                this.lstActividadRedds.DataSource = LstActividad;
                this.lstActividadRedds.DataBind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, Page.GetType(), "Repetido", "<script>alert('Este item ya ha sido adicionado!');</script>",false);
            }
        }
    }
    protected void btnQuitarActividad_Click(object sender, EventArgs e)
    {
        if (this.lstActividadRedds.SelectedIndex != -1)
        {
            LstActividad.Remove(LstActividad[this.lstActividadRedds.SelectedIndex]);
            this.lstActividadRedds.DataTextField = "NombreActividad";
            this.lstActividadRedds.DataSource = LstActividad;
            this.lstActividadRedds.DataBind();
        }
    }
    protected void btnAgregarCompartimento_Click(object sender, EventArgs e)
    {
        if (LstCompartimentoCarbono == null)
            LstCompartimentoCarbono = new List<SILPA.AccesoDatos.REDDS.ReddsCompartimientoCarbono>();
        bool bln_adicionar = true;

        if (LstCompartimentoCarbono.Count < 5)
        {
            if (LstCompartimentoCarbono.Where(x => x.CompartimientoID == Convert.ToInt32(this.cboCompartimientos.SelectedValue)).Count() > 0)
            {
                bln_adicionar = false;
                return;
            }

            if (bln_adicionar)
            {
                LstCompartimentoCarbono.Add(new SILPA.AccesoDatos.REDDS.ReddsCompartimientoCarbono { CompartimientoID = Convert.ToInt32(this.cboCompartimientos.SelectedValue), NombreCompartimiento = this.cboCompartimientos.SelectedItem.Text });
                lstCompartimientos.DataTextField = "NombreCompartimiento";
                lstCompartimientos.DataValueField = "CompartimientoID";
                lstCompartimientos.DataSource = LstCompartimentoCarbono;
                lstCompartimientos.DataBind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, Page.GetType(), "Repetido", "<script>alert('Este item ya ha sido adicionado!');</script>", false);
            }
        }
    }
    protected void btnQuitarCompar_Click(object sender, EventArgs e)
    {
        if (lstCompartimientos.SelectedIndex != -1)
        {
            LstCompartimentoCarbono.Remove(LstCompartimentoCarbono[lstCompartimientos.SelectedIndex]);
            lstCompartimientos.DataTextField = "NombreCompartimiento";
            lstCompartimientos.DataValueField = "CompartimientoID";
            lstCompartimientos.DataSource = LstCompartimentoCarbono;
            lstCompartimientos.DataBind();
        }
    }
    protected void btnAgregarAutoridad_Click(object sender, EventArgs e)
    {
        if (LstAutoridades == null)
            LstAutoridades = new List<SILPA.AccesoDatos.REDDS.ReddsAutoridadAmbiental>();
        bool bln_adicionar = true;

        if (LstAutoridades.Count < 3)
        {
            if (LstAutoridades.Where(x => x.AutoridadID == Convert.ToInt32(this.cboAutoridadAmbiental.SelectedValue)).Count() > 0)
            {
                bln_adicionar = false;
                return;
            }

            if (bln_adicionar)
            {
                LstAutoridades.Add(new SILPA.AccesoDatos.REDDS.ReddsAutoridadAmbiental { AutoridadID = Convert.ToInt32(this.cboAutoridadAmbiental.SelectedValue), NombreAutoridad = this.cboAutoridadAmbiental.SelectedItem.Text });
                this.lstAutoridadRedds.DataTextField = "NombreAutoridad";
                this.lstAutoridadRedds.DataSource = LstAutoridades;
                this.lstAutoridadRedds.DataBind();
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Repetido", "<script>alert('Este item ya ha sido adicionado!');</script>");
            }
        }
    }
    protected void btnQuitarAutoridad_Click(object sender, EventArgs e)
    {
        if (this.lstAutoridadRedds.SelectedIndex != -1)
        {
            LstAutoridades.Remove(LstAutoridades[this.lstAutoridadRedds.SelectedIndex]);
            this.lstAutoridadRedds.DataTextField = "NombreAutoridad";
            this.lstAutoridadRedds.DataSource = LstAutoridades;
            this.lstAutoridadRedds.DataBind();
        }
    }
    protected void btnAgregarDeptoMuni_Click(object sender, EventArgs e)
    {
        if (LstDeptoMunicipio == null)
            LstDeptoMunicipio = new List<SILPA.AccesoDatos.REDDS.ReddsDeptoMunicipio>();
        bool bln_adicionar = true;
        
        if (LstDeptoMunicipio.Where(x => x.MunpioID == Convert.ToInt32(this.cboMunicipio.SelectedValue)).Count() > 0)
        {
            bln_adicionar = false;
            return;
        }

        if (bln_adicionar)
        {
            if (this.cboMunicipio.SelectedValue == "-1") // se agregan todos lo municipios del departamento
            {
                Listas litMunicipio = new Listas();
                DataTable dtMunicipios = litMunicipio.ListaMunicipios(null, int.Parse(cboDepartamento.SelectedValue), null).Tables[0];
                dtMunicipios = dtMunicipios.AsEnumerable().Where(x => !x.Field<string>("MUN_NOMBRE").Contains("Parque")).CopyToDataTable();
                foreach (DataRow dtrMunicipio in dtMunicipios.Rows)
                {
                    if(LstDeptoMunicipio.Where(x => x.MunpioID == Convert.ToInt32(dtrMunicipio["MUN_ID"])).Count() == 0)
                        LstDeptoMunicipio.Add(new SILPA.AccesoDatos.REDDS.ReddsDeptoMunicipio { DeptoID = Convert.ToInt32(this.cboDepartamento.SelectedValue), NombreDepartamento = this.cboDepartamento.SelectedItem.Text, MunpioID = Convert.ToInt32(dtrMunicipio["MUN_ID"]), NombreMunicipio = dtrMunicipio["MUN_NOMBRE"].ToString() });
                }
            }
            else
            {
                LstDeptoMunicipio.Add(new SILPA.AccesoDatos.REDDS.ReddsDeptoMunicipio { DeptoID = Convert.ToInt32(this.cboDepartamento.SelectedValue), NombreDepartamento = this.cboDepartamento.SelectedItem.Text, MunpioID = Convert.ToInt32(this.cboMunicipio.SelectedValue), NombreMunicipio = this.cboMunicipio.SelectedItem.Text });
            }
            
            var lstDetalle = LstDeptoMunicipio.Select(x => new { Detalle = string.Format("{0},{1}", x.NombreDepartamento, x.NombreMunicipio), MunicipioID = x.MunpioID }).ToList();
            lstDeptoMuni.DataTextField = "Detalle";
            lstDeptoMuni.DataValueField = "MunicipioID";
            lstDeptoMuni.DataSource = lstDetalle;
            lstDeptoMuni.DataBind();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, Page.GetType(), "Repetido", "<script>alert('Este item ya ha sido adicionado!');</script>", false);
        }
        
    }
    protected void btnQuitarDeptoMuni_Click(object sender, EventArgs e)
    {
        if (this.lstDeptoMuni.SelectedIndex != -1)
        {
            LstDeptoMunicipio.Remove(LstDeptoMunicipio[this.lstDeptoMuni.SelectedIndex]);
            var lstDetalle = LstDeptoMunicipio.Select(x => new { Detalle = string.Format("{0},{1}", x.NombreDepartamento, x.NombreMunicipio), MunicipioID = x.MunpioID }).ToList();
            lstDeptoMuni.DataTextField = "Detalle";
            lstDeptoMuni.DataValueField = "MunicipioID";
            lstDeptoMuni.DataSource = lstDetalle;
            lstDeptoMuni.DataBind();
            if(lstDeptoMuni.Items.Count > 0)
                lstDeptoMuni.SelectedIndex = 0;
        }
    }
    protected void dgv_localizaciones_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int LocID = (Int32)this.dgv_localizaciones.DataKeys[e.RowIndex].Value;
        LstLocalizacion.Remove(LstLocalizacion.Where(x => x.LocID == LocID).First());
        this.dgv_localizaciones.DataSource = LstLocalizacion;
        this.dgv_localizaciones.DataBind();
       
    }
    protected void btn_adicionar_localizacion_Click(object sender, EventArgs e)
    {
        if (LstLocalizacion == null)
            LstLocalizacion = new List<SILPA.AccesoDatos.REDDS.ReddsLocalizacion>();

        SILPA.AccesoDatos.REDDS.ReddsLocalizacion Localizacion = new SILPA.AccesoDatos.REDDS.ReddsLocalizacion();
        if (LstLocalizacion.Count < 8)
        {
            if (localizacionID != 0)
                LstLocalizacion.Remove(LstLocalizacion.Where(x => x.LocID == localizacionID).First());
            Localizacion.LocID = LstLocalizacion.Count()+1;
            Localizacion.GeoID = Int32.Parse(this.lst_tipo_geometria.SelectedValue);
            string[] coordenadas = this.txtCoordenadas.Text.Split(new char[] { ',' });
            if (coordenadas.Length % 2 == 0)
            {
                string strCoordenadasErroneas = "";
                if (ValidarCoordenadas(coordenadas, out strCoordenadasErroneas))
                {
                    // creamos los objetos de coordenadas
                    for (int i = 0; i < coordenadas.Length; i += 2)
                    {
                        SILPA.AccesoDatos.REDDS.ReddsCoordenadasLocalizacion oCoor = new SILPA.AccesoDatos.REDDS.ReddsCoordenadasLocalizacion();
                        oCoor.LocID = Localizacion.LocID;
                        oCoor.CoorNorte = double.Parse(coordenadas[i].Replace(".", ","));
                        oCoor.CoorEste = double.Parse(coordenadas[i + 1].Replace(".", ","));
                        if (Localizacion.LstCoordenadas == null)
                            Localizacion.LstCoordenadas = new List<SILPA.AccesoDatos.REDDS.ReddsCoordenadasLocalizacion>();
                        Localizacion.LstCoordenadas.Add(oCoor);
                    }
                }
                else
                {

                    ScriptManager.RegisterStartupScript(this, Page.GetType(), "alerta", "<script>alert('Las siguientes coordenadas estan fuera de los limites de la Autoridad: " + strCoordenadasErroneas + "')</script>", false);
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, Page.GetType(), "script", "<script>alert('Las coordenadas estan incompletas (Latitud, Longitud)')</script>", false);
                //is.Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", );
                return;
            }
            LstLocalizacion.Add(Localizacion);
            this.dgv_localizaciones.DataSource = LstLocalizacion;
            this.dgv_localizaciones.DataBind();
            if (localizacionID != 0)
            {
                localizacionID = 0;
            }

            this.limpiarFormulario();
        }
        else
        {
            this.limpiarFormulario();
            ScriptManager.RegisterStartupScript(this, Page.GetType(), "script", "<script>alert('El numero maximo de puntos es de 20')</script>", false);
        }

        
    }
    protected void btnCancelarEdicion_Click(object sender, EventArgs e)
    {
        this.btn_adicionar_localizacion.Text = "Adicionar localización";
        this.localizacionID = 0;
        this.btnCancelarEdicion.Visible = false;
        this.dgv_localizaciones.EditIndex = -1;
        limpiarFormulario();
    }
    private bool ValidarCoordenadas(string[] coordenadas, out string strCoordenadasErroneas)
    {
        int cont = 1;
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
                    dcoordenada = Convert.ToDouble(coordenada.Replace(".", ","));
                    strCoordenadasErroneas += "," + coordenada;
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
                    dcoordenada = Convert.ToDouble(coordenada.Replace(".", ","));
                    strCoordenadasErroneas += "," + coordenada;
                }
                catch (Exception)
                {
                    strCoordenadasErroneas = "El formato de las coordenadas esta incorrecto";
                    cumple = false;
                }
            }
            cont++;
        }
        return cumple;
    }
    protected void limpiarFormulario()
    {
        this.txtCoordenadas.Text = string.Empty;
        this.lst_tipo_geometria.SelectedValue = "-1";
        this.lst_tipo_geometria.SelectedIndex = -1;
        this.btnCancelarEdicion.Visible = false;
        this.btn_adicionar_localizacion.Text = "Adicionar localización";
    }
    protected void dgv_localizaciones_DataBound(object sender, EventArgs e)
    {
        ImageButton imb_aux;

        foreach (GridViewRow row in this.dgv_localizaciones.Rows)
        {
            int int_id = (Int32)this.dgv_localizaciones.DataKeys[row.RowIndex].Value;

            SILPA.AccesoDatos.REDDS.ReddsLocalizacion pLocalizacion = LstLocalizacion.Where(x => x.LocID == int_id).First();
            row.Cells[0].Text = "Poligono";

            int int_cont = 0;

            foreach (SILPA.AccesoDatos.REDDS.ReddsCoordenadasLocalizacion oCoor in pLocalizacion.LstCoordenadas)
            {
                if (int_cont == 0)
                {
                    row.Cells[1].Text += "N-" + oCoor.CoorNorte.ToString() + " " + "E-" + oCoor.CoorEste + "  ";
                    row.Cells[2].Text += "N-" + ConvertirRadianesAGrados(double.Parse(oCoor.CoorNorte.ToString())) + " E-" + ConvertirRadianesAGrados(double.Parse(oCoor.CoorEste.ToString()));
                }
                else
                {
                    row.Cells[1].Text += "<BR />N-" + oCoor.CoorNorte.ToString() + " " + "E-" + oCoor.CoorEste + "  ";
                    row.Cells[2].Text += "<br />N-" + ConvertirRadianesAGrados(double.Parse(oCoor.CoorNorte.ToString())) + " E-" + ConvertirRadianesAGrados(double.Parse(oCoor.CoorEste.ToString()));
                }

                int_cont++;
            }

            imb_aux = (ImageButton)row.FindControl("imb_borrar");
            imb_aux.Attributes.Add("onclick", "confirmar('¿Está seguro de borrar la localización?')");
        }
    }
    private string ConvertirRadianesAGrados(double radiandes)
    {
        //tomamos la parte entera del decimal
        string coordenada = "";
        int grados = (int)Math.Truncate(radiandes);
        coordenada = grados.ToString() + "°";
        //restamos los enteros a los radianes
        double ming = double.Parse(radiandes.ToString().Replace(grados.ToString(), "0")) * 60;
        //tomamos la parte entera para obtener los minutos
        int min = (int)Math.Truncate(ming);
        coordenada = coordenada + min.ToString() + "'";
        //restamos los enteros a los radianes
        double segg = double.Parse(ming.ToString().Replace(min.ToString(), "0")) * 60;
        coordenada = coordenada + segg.ToString() + @"""";
        return coordenada;

    }
    protected void dgv_localizaciones_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "editar":
                SILPA.AccesoDatos.REDDS.ReddsLocalizacion localizacion = LstLocalizacion.Where(x => x.LocID == Convert.ToInt32(e.CommandArgument)).First();
                this.lst_tipo_geometria.SelectedValue = localizacion.GeoID.ToString();
                string coordenadas = "";
                foreach (SILPA.AccesoDatos.REDDS.ReddsCoordenadasLocalizacion coordenada in localizacion.LstCoordenadas)
                {
                    coordenadas += "," + coordenada.CoorNorte.ToString().Replace(",", ".") + "," + coordenada.CoorEste.ToString().Replace(",", ".");
                }
                this.txtCoordenadas.Text = coordenadas.Remove(0, 1);

                localizacionID = localizacion.LocID;
                this.btn_adicionar_localizacion.Text = "Actualizar localización";
                this.btnCancelarEdicion.Visible = true;
                break;
        }
        
    }
    protected void btnActualizar_Click(object sender, EventArgs e)
    {
        if(ValidacionRedds())
        {
            try
            {
                SILPA.AccesoDatos.REDDS.Redds objRedds = new SILPA.AccesoDatos.REDDS.Redds();
                if(this.txtCostoEstiamdoFormulario.Text.Trim() != string.Empty)
                    objRedds.CostoEstimadoFormulacion = Convert.ToDouble(this.txtCostoEstiamdoFormulario.Text);
                objRedds.AreaInfluencia = Convert.ToDouble(this.txtAreaInfluencia.Text);
                objRedds.EstadoAvanceIniciativa = Convert.ToInt32(this.cboEstadoIniciativa.SelectedValue);
                //objRedds.EstandarMercadeo = this.txtEstandarMercadeoVoluntario.Text;
                objRedds.FechaFinImplementacion = Convert.ToDateTime(this.txtFechaFinalIniciativa.Text);
                objRedds.FechaInicioImplementacion = Convert.ToDateTime(this.txtFechaInicioIniciativa.Text);
                objRedds.LstActividad = LstActividad;
                objRedds.LstAutoridades = LstAutoridades;
                objRedds.LstCompartimentoCarbono = LstCompartimentoCarbono;
                objRedds.LstDeptoMunicipio = LstDeptoMunicipio;
                objRedds.LstEstimadoReduccionEmisiones = LstEstimadoReduccionEmisiones;
                objRedds.LstEstimadoReduccionDeforestacion = LstEstimadoReduccionDeforestacion;
                objRedds.LstLocalizacion = LstLocalizacion;
                objRedds.LstParticipante = LstParticipantes;
                objRedds.LstEstandarMercado = LstEstandarMercado;
                objRedds.MetodologiaEstandarMercadeo = this.txtMetodologiaEstandar.Text;
                objRedds.NombreIniciativa = this.txtNombreIniciativa.Text;
                objRedds.NombreRazonSocial = this.txtNombreRazonSocial.Text;
                if (this.cboRelacionJuridica.SelectedValue != "")
                {
                    objRedds.RelacionJuridicaID = Convert.ToInt32(this.cboRelacionJuridica.SelectedValue);
                    if (objRedds.RelacionJuridicaID != 5)
                    {
                        objRedds.DescRelacionJuridica = this.cboRelacionJuridica.SelectedItem.Text.Trim();
                    }
                    else
                    {
                        objRedds.DescRelacionJuridica = this.txtRelacionJuridica.Text.Trim();
                    }
                }
                Redds clsRedds = new Redds();

                if (fuplPDDoPIN.historial.Where(x => x.Deleted == false).Count() > 0)
                    objRedds.DocumentoDiseño = fuplPDDoPIN.historial.Where(x => x.Deleted == false).First().FileName;
                if (fuplShape.historial != null)
                {
                    if (fuplShape.historial.Where(x => x.Deleted == false).Count() > 0)
                        objRedds.ArchivosShape = fuplShape.historial.Where(x => x.Deleted == false).First().FileName;
                }
                if (fulaOtros.historial != null)
                {
                    if (fulaOtros.historial.Where(x => x.Deleted == false).Count() > 0)
                    {
                        LstArchivo = new List<SILPA.AccesoDatos.REDDS.ReddsArchivos>();
                        foreach (HttpPostedFileAJAX item in fulaOtros.historial.Where(x => x.Deleted == false).ToList())
                        {
                            LstArchivo.Add(new SILPA.AccesoDatos.REDDS.ReddsArchivos { Archivo = item.FileName });
                        }
                    }
                    objRedds.LstArchivo = LstArchivo;
                }
                objRedds.ReddsID = clsRedds.InsertarRedds(objRedds);
                objRedds.NumeroVital = /*(new Random()).Next(1000).ToString();*/ clsRedds.CrearProceso(ConfigurationManager.AppSettings["ReddsClientID"].ToString(), Convert.ToInt64(ConfigurationManager.AppSettings["ReddsFormID"]), Convert.ToInt64(Session["Usuario"]), CrearXml());
                clsRedds.ActualizarNumeroVital(objRedds.NumeroVital, objRedds.ReddsID);

                if(!Directory.Exists(ConfigurationManager.AppSettings["FILE_TRAFFIC"] + string.Format(@"Redds\{0}\",objRedds.NumeroVital)))
                Directory.CreateDirectory(ConfigurationManager.AppSettings["FILE_TRAFFIC"] + string.Format(@"Redds\{0}\",objRedds.NumeroVital));

                if(fulaOtros.historial != null)
                foreach (var item in fulaOtros.historial)
                {
                    if (item.Saved)
                    {
                        Utilidades.MoveFile(new FileInfo(Server.MapPath(string.Format("~/temp/{0}", item.FileName))), new DirectoryInfo(ConfigurationManager.AppSettings["FILE_TRAFFIC"] + string.Format(@"Redds\{0}\", objRedds.NumeroVital)));
                    }
                }
                if(fuplPDDoPIN.historial != null)
                foreach (var item in fuplPDDoPIN.historial)
                {
                    if (item.Saved)
                    {
                        Utilidades.MoveFile(new FileInfo(Server.MapPath(string.Format("~/temp/{0}", item.FileName))), new DirectoryInfo(ConfigurationManager.AppSettings["FILE_TRAFFIC"] + string.Format(@"Redds\{0}\", objRedds.NumeroVital)));
                    }
                }
                if(fuplShape.historial != null)
                foreach (var item in fuplShape.historial)
                {
                    if (item.Saved)
                    {
                        Utilidades.MoveFile(new FileInfo(Server.MapPath(string.Format("~/temp/{0}", item.FileName))), new DirectoryInfo(ConfigurationManager.AppSettings["FILE_TRAFFIC"] + string.Format(@"Redds\{0}\", objRedds.NumeroVital)));
                    }
                }
                


                string msg;
                string Line1 = "RESULTADO";
                string Line2 = "Proceso realizado correctamente ";
                string Line3 = "El número vital asignado a su proceso es el " + objRedds.NumeroVital;
                string Line4 = "Su solicítud será gestionado por la Autoridad Ambiental MADS";
                msg = "".PadLeft(37) + Line1 + "\\n";
                msg += "".PadLeft(25) + Line2 + "\\n";
                msg += "".PadLeft(35 - Line3.Length / 2) + Line3 + "\\n";
                msg += "".PadLeft(35 - Line4.Length / 2) + Line4;

                ScriptManager.RegisterStartupScript(this, typeof(string), Guid.NewGuid().ToString(), string.Format("alert('{0}.');", msg), true);
                string strScript = "<script language='JavaScript'>" +
                        "location.href = '" + "../../ventanillasilpa/" + "'" +
                        "</script>";
                ScriptManager.RegisterStartupScript(this,typeof(string),"PopupScript", strScript,false);
                //Page.RegisterStartupScript("PopupScript", strScript);

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, Page.GetType(), "script", "<script>alert('Error: " + ex.Message + "')</script>", false);
            }
        }
    }
    private bool ValidacionRedds()
    {
        string mensajeValidacion="";
        // validamos los participantes
        
        if (LstParticipantes == null || LstParticipantes.Count == 0)
        {
            mensajeValidacion += "<br /> Información general/Debe Ingresar al menos un Participante.";
        }
        if (this.cboRelacionJuridica.SelectedValue == "5" && this.txtRelacionJuridica.Text.Trim() == string.Empty)
        {
            mensajeValidacion += "<br /> Información general/Debe Ingresar la relación Juridica.";
        }
        if (LstEstimadoReduccionEmisiones == null ||LstEstimadoReduccionEmisiones.Count == 0)
        {
            mensajeValidacion += "<br /> Caracterización/Debe Ingresar el estimado de reduccion de emisiones.";
        }
        if (LstEstimadoReduccionDeforestacion == null || LstEstimadoReduccionDeforestacion.Count == 0)
        {
            mensajeValidacion += "<br /> Caracterización/Debe Ingresar el estimado de reduccion de emisiones de deforestación.";
        }
        if (LstActividad == null || LstActividad.Count == 0)
        {
            mensajeValidacion += "<br /> Caracterización/Debe Ingresar al menos una Actividad.";
        }
        if (LstCompartimentoCarbono == null || LstCompartimentoCarbono.Count == 0)
        {
            mensajeValidacion += "<br /> Caracterización/Debe Ingresar al menos un Compartimento de Carbono.";
        }
        if (LstAutoridades == null || LstAutoridades.Count == 0)
        {
            mensajeValidacion += "<br /> Localización/Debe Ingresar al menos una Autoridad Ambiental.";
        }
        if (LstDeptoMunicipio == null || LstDeptoMunicipio.Count == 0)
        {
            mensajeValidacion += "<br /> Localización/Debe Ingresar al menos un departamento y municipio.";
        }
        if (LstLocalizacion == null|| LstLocalizacion.Count == 0)
        {
            mensajeValidacion += "<br /> Localización/Debe Ingresar al menos 1 poligono.";
        }
        if (fuplPDDoPIN.historial == null)
        {
            mensajeValidacion += "<br /> Documentos Adicionales/Debe agregar un documento de Diseño del proyecto o Programa o Nota de Idea de Proyecto";
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
        objValoresList.Add(CargarValores(1, "Bas", this.txtNombreIniciativa.Text, 1, new Byte[1]));
        objValoresList.Add(CargarValores(2, "Bas", Session["Usuario"].ToString(), 1, new Byte[1]));
        objValoresList.Add(CargarValores(3, "Bas", this.txtNombreRazonSocial.Text, 1, new Byte[1]));
        objValoresList.Add(CargarValores(4, "Bas", "134", 1, new Byte[1])); // se envia a MADS
        MemoryStream memoryStream = new MemoryStream();
        XmlSerializer serializador = new XmlSerializer(typeof(List<ValoresIdentity>));
        serializador.Serialize(memoryStream, objValoresList);
        string xml = System.Text.UTF8Encoding.UTF8.GetString(memoryStream.ToArray());
        return xml;
    }
    protected void cboRelacionJuridica_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.divOtro.Visible = this.cboRelacionJuridica.SelectedValue == "5";
    }
    protected void btnAgregarEstandar_Click(object sender, EventArgs e)
    {
        if (LstEstandarMercado == null)
            LstEstandarMercado = new List<SILPA.AccesoDatos.REDDS.ReddsEstandarMercado>();
        bool bln_adicionar = true;

        if (LstEstandarMercado.Count < 2)
        {
            // Verifica si esta repetido
            string strEstandarMercado = this.txtEstandarMercado.Text.Trim();
            foreach (ListItem li in this.lstEstandarMercado.Items)
            {
                if (li.Value.ToUpper() == strEstandarMercado.ToUpper())
                {
                    bln_adicionar = false;
                    return;
                }
            }

            if (bln_adicionar)
            {
                LstEstandarMercado.Add(new SILPA.AccesoDatos.REDDS.ReddsEstandarMercado { NombreEstandar = strEstandarMercado, EstandarID = LstEstandarMercado.Count() + 1});
                this.lstEstandarMercado.DataTextField = "NombreEstandar";
                this.lstEstandarMercado.DataSource = LstEstandarMercado;
                this.lstEstandarMercado.DataBind();
                this.txtEstandarMercado.Text = string.Empty;
                Utilidades.LlenarComboLista(LstEstandarMercado, this.cboEstandarMercado, "NombreEstandar", "EstandarID", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, Page.GetType(), "Repetido", "<script>alert('Este item ya ha sido adicionado!');</script>", false);
            }
        }
    }
    protected void btnQuitarEstandar_Click(object sender, EventArgs e)
    {
        if (this.lstEstandarMercado.SelectedIndex != -1)
        {
            if (LstEstandarMercado[this.lstEstandarMercado.SelectedIndex].LstMetodologiaEstandar.Count == 0)
            {
                LstEstandarMercado.Remove(LstEstandarMercado[this.lstEstandarMercado.SelectedIndex]);
                this.lstEstandarMercado.DataSource = LstEstandarMercado;
                this.lstEstandarMercado.DataBind();
                Utilidades.LlenarComboLista(LstEstandarMercado, this.cboEstandarMercado, "NombreEstandar", "EstandarID", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, Page.GetType(), "Repetido", "<script>alert('El estandar tiene metodologias Asociadas');</script>", false);
            }
        }
    }
    protected void btnAgregarMetodologia_Click(object sender, EventArgs e)
    {
        if (this.cboEstandarMercado.SelectedValue != "")
        {
            if (LstEstandarMercado.Count() > 0)
            {
                bool bln_adicionar = true;
                var EstadoMercado = LstEstandarMercado.Where(x => x.EstandarID == Convert.ToInt32(this.cboEstandarMercado.SelectedValue)).First();
                if (EstadoMercado.LstMetodologiaEstandar == null)
                {
                    EstadoMercado.LstMetodologiaEstandar = new List<SILPA.AccesoDatos.REDDS.ReddsMetodologiaEstandar>();
                }
                if (EstadoMercado.LstMetodologiaEstandar.Count < 3)
                {
                    // Verifica si esta repetido
                    string strMetodologiaEstandarMercado = this.txtMetodologiaEstandar.Text.Trim();

                    if (EstadoMercado.LstMetodologiaEstandar.Where(x => x.NombreMetodologia.ToUpper().Trim() == strMetodologiaEstandarMercado.ToUpper().Trim()).Count() > 0)
                    {
                        bln_adicionar = false;
                        return;
                    }

                    if (bln_adicionar)
                    {
                        EstadoMercado.LstMetodologiaEstandar.Add(new SILPA.AccesoDatos.REDDS.ReddsMetodologiaEstandar { EstandarID = Convert.ToInt32(this.cboEstandarMercado.SelectedValue), 
                            NombreMetodologia = strMetodologiaEstandarMercado, 
                            MetodologiaID = EstadoMercado.LstMetodologiaEstandar.Count() +1 });


                        this.lstMetodologia.DataTextField = "EstandarMetodologia";
                        this.lstMetodologia.DataValueField = "ID";
                        this.lstMetodologia.DataSource = ListaEstandarMetodologia();
                        this.lstMetodologia.DataBind();
                        this.txtMetodologiaEstandar.Text = string.Empty;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, Page.GetType(), "Repetido", "<script>alert('Este item ya ha sido adicionado!');</script>", false);
                    }
                }

            }
        }
    }
    protected void btnQuitarMetodologia_Click(object sender, EventArgs e)
    {
        if (this.lstMetodologia.SelectedIndex != -1)
        {
            string[] strId = this.lstMetodologia.SelectedValue.Split(',');

            var EstandarMercado = LstEstandarMercado.Where(x => x.EstandarID == Convert.ToInt32(strId[0])).First();
            EstandarMercado.LstMetodologiaEstandar.Remove(EstandarMercado.LstMetodologiaEstandar.Where(x => x.MetodologiaID == Convert.ToInt32(strId[1])).First());
            this.lstMetodologia.DataTextField = "EstandarMetodologia";
            this.lstMetodologia.DataValueField = "ID";
            this.lstMetodologia.DataSource = ListaEstandarMetodologia();
            this.lstMetodologia.DataBind();
        }
    }
    protected List<object> ListaEstandarMetodologia()
    {
        List<object> LstEstandarMetodologia = new List<object>();

        foreach (var EstandarMercado in LstEstandarMercado)
        {
            if (EstandarMercado.LstMetodologiaEstandar != null)
            {
                foreach (var MetodologiaEstandar in EstandarMercado.LstMetodologiaEstandar)
                {
                    LstEstandarMetodologia.Add(new
                    {
                        EstandarMetodologia = string.Format("Estandar: {0} | Metodologia: {1}", EstandarMercado.NombreEstandar, MetodologiaEstandar.NombreMetodologia),
                        ID = string.Format("{0},{1}",MetodologiaEstandar.EstandarID,MetodologiaEstandar.MetodologiaID)
                    });
                }
            }
        }
        return LstEstandarMetodologia;
    }
}
