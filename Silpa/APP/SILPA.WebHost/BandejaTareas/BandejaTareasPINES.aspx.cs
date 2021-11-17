using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SILPA.LogicaNegocio.Generico;
using Silpa.Workflow.AccesoDatos;
using Silpa.Workflow;
using SoftManagement.Log;
using System.Collections.Generic;
using System.Configuration;
using SILPA.AccesoDatos.PINES;
using SILPA.LogicaNegocio.PINES;
using System.IO;
using System.Data;
using AjaxControlToolkit;

public partial class BandejaTareas_BandejaTareasPINES : System.Web.UI.Page
{
    private const string urlAutoridadAmbiental = "InternalParticipant/WorkItem.aspx?IDActivityInstance={0}";
    private const string urlSolicitante = "Public/buildFormInstance.aspx?IdProcessInstance={0}&IdActivityInstance={1}&UserID={2}&Case={3}&Form={4}&EntryDataType={5}&EntryData={6}&IdEntryData={7}&ID={8}&op=&IdUser={9}&IdRelated={10}&ClientName={11}&URL={12}";

    public DataTable dttAcciones { get { return (DataTable)ViewState["dttAcciones"];} set { ViewState["dttAcciones"]=value;} }
    private const string usuario2 = "11112";
    
    public bool EsGerente { get { return (bool)ViewState["esGerente"]; } set { ViewState["esGerente"] = value; } }
    
    protected void Page_Load(object sender, EventArgs e)
    {
       // RegistrarControlesPostBack();
        if (!Page.IsPostBack)
        {
            EsGerente = false;
            ConsultarTareasSinIniciarPINES();
            this.txtFechaFinalVisor.Text = DateTime.Now.ToShortDateString();
            this.txtFechaInicialVisor.Text = DateTime.Now.AddDays(-8).ToShortDateString();
            CargarPagina();
            this.rfvArchivoComentario.Enabled = false;
            this.hdfUrlVital.Value = ConfigurationManager.AppSettings["URL_PROYECTO_VITAL"].ToString();
            
        }
    }
    protected void RegistrarControlesPostBack()
    {
        if (dtlDatos.Items.Count > 0)
        {
            foreach (DataListItem item in dtlDatos.Items)
	        {
                ScriptManager1.RegisterPostBackControl((ImageButton)item.FindControl("imgbArchivosSolicitud"));
                Accordion acActividades = (Accordion)item.FindControl("acActividades");
                acActividades.SelectedIndex = -1;
                GridView dtgDatos = (GridView)acActividades.Panes[0].FindControl("dtgDatos");
                if (dtgDatos != null)
                {
                    foreach (GridViewRow row in dtgDatos.Rows)
                    {
                        ScriptManager1.RegisterPostBackControl((ImageButton)row.FindControl("imgbComentarios"));
                    }
                }
                   
	        } 
        }
    }
    private void CargarPagina()
    {
        SILPA.Comun.Configuracion _configuracion = new SILPA.Comun.Configuracion();
       
        SILPA.LogicaNegocio.Generico.Listas _listaAutoridades = new SILPA.LogicaNegocio.Generico.Listas();
        cboAutoridadAmbiental.DataSource = _listaAutoridades.ListarAutoridades(null);
        cboAutoridadAmbiental.DataValueField = "AUT_ID";
        cboAutoridadAmbiental.DataTextField = "AUT_NOMBRE";
        cboAutoridadAmbiental.DataBind();
        cboAutoridadAmbiental.Items.Insert(0, new ListItem("Seleccione.", "-1"));

        SILPA.LogicaNegocio.Generico.Listas _lista = new SILPA.LogicaNegocio.Generico.Listas();
        cboDepartamento.DataSource = _lista.ListarDepartamentos(_configuracion.IdPaisPredeterminado);
        cboDepartamento.DataTextField = "DEP_NOMBRE";
        cboDepartamento.DataValueField = "DEP_ID";
        cboDepartamento.DataBind();
        cboDepartamento.Items.Insert(0, new ListItem("Seleccione.", "-1"));
        SILPA.LogicaNegocio.Usuario.Usuario clsusuario = new SILPA.LogicaNegocio.Usuario.Usuario();
        DataTable dtautoridad = clsusuario.ConsultarUsuarioSistemaCompania(usuario2);
        SILPA.AccesoDatos.Generico.ParametroDalc _parametroDalc = new SILPA.AccesoDatos.Generico.ParametroDalc();
        SILPA.AccesoDatos.Generico.ParametroEntity _parametro = new SILPA.AccesoDatos.Generico.ParametroEntity();
        _parametro.IdParametro = -1;
        _parametro.NombreParametro = "Gerente Pines";
        _parametroDalc.obtenerParametros(ref _parametro);

        DataRow[] dtrGrupoGerente = dtautoridad.Select("IDUserGroup = " + _parametro.Parametro.Trim());
        if (dtrGrupoGerente.Length > 0)
        {
            EsGerente = true;
        }
        _parametro.IdParametro = -1;
        _parametro.NombreParametro = "Usuario Pines";
        _parametroDalc.obtenerParametros(ref _parametro);
        DataRow[] dtrGrupoUsuario = dtautoridad.Select("IDUserGroup = " + _parametro.Parametro.Trim());
        if (dtrGrupoUsuario.Length > 0)
        {
            EsGerente = false;
        }
    }

    private void ConsultarTareasSinIniciarPINES()
    {
        try
        {
            DateTime? fechaInicio = null;
            DateTime? fechaFinal = null;

            if (this.txtFechaInicial.Text != "")
            {
                fechaInicio = Convert.ToDateTime(this.txtFechaInicial.Text);
            }
            if (this.txtFechaFinal.Text != "")
            {
                fechaFinal = Convert.ToDateTime(this.txtFechaFinal.Text);
                fechaFinal = new DateTime(fechaFinal.Value.Year, fechaFinal.Value.Month, fechaFinal.Value.Day, 23, 59, 59);
            }


            this.gdvTareasSinIniciar.DataSource = BandejaTareasDao.ConsultarTareasSinIniciarPINES(usuario2, this.txtNumeroVital.Text, fechaInicio, fechaFinal);
            this.gdvTareasSinIniciar.DataBind();
        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ex);
            Mensaje.MostrarMensaje(this.Page, "Ha ocurrido un error comuniquese con el administrador");
        }      
    }
    protected void gdvTareasSinIniciar_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Label lblUrlDataView;
        Label lblEntryDataType;
        HyperLink hypAutoridadAmbiental;
        Label lblActivityInstance;
        Label lblModeloDistribucion;
        Label lblProcessInstance;
        Label lblEntryDataTypeProcess;
        Label lblEntryData;
        Label lblIdEntryData;
        Label lblIdRelated;
        Label lblIdProcess;
        Label lblIdActivity;
        LinkButton lnkFinalizarTarea;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            if (e.Row.RowState == DataControlRowState.Alternate || e.Row.RowState == DataControlRowState.Normal)
            {
                lblUrlDataView = (Label)e.Row.FindControl("lblUrlDataView");
                lblEntryDataType = (Label)e.Row.FindControl("lblEntryDataType");
                lblActivityInstance = (Label)e.Row.FindControl("lblActivityInstance");
                hypAutoridadAmbiental = (HyperLink)e.Row.FindControl("hypAutoridadAmbiental");
                lblModeloDistribucion = (Label)e.Row.FindControl("lblModeloDistribucion");
                lblProcessInstance = (Label)e.Row.FindControl("lblProcessInstance");
                lblEntryDataTypeProcess = (Label)e.Row.FindControl("lblEntryDataTypeProcess");
                lblEntryData = (Label)e.Row.FindControl("lblEntryData");
                lblIdEntryData = (Label)e.Row.FindControl("lblIdEntryData");
                lblIdRelated = (Label)e.Row.FindControl("lblIdRelated");
                lblIdProcess = (Label)e.Row.FindControl("lblIdProcess");
                lblIdActivity = (Label)e.Row.FindControl("lblIdActivity");
                lnkFinalizarTarea = (LinkButton)e.Row.FindControl("lnkFinalizarTarea");
            }
        }
    }

    private void CargarComentario(ComentarioActividadIdentity vComentarioActividadIdentity)
    {
        hdfIDComentario.Value = vComentarioActividadIdentity.IDComentario.ToString();
        this.txtComentario.Text = vComentarioActividadIdentity.Comments;
        this.cboEstado.SelectedValue = vComentarioActividadIdentity.ActividadRealizada ? "1":"0";
        if (vComentarioActividadIdentity.ActividadRealizada)
            ClientScript.RegisterStartupScript(this.GetType(), "Ventana", "alert('La actividad se encuentra en espera de finalización por las demas Autoridades');", true);
        else
        {
            this.mpeComentario.Show();
            
        }
        if (vComentarioActividadIdentity.ActividadRealizada)
            this.rfvArchivoComentario.Enabled = true;
        else
            this.rfvArchivoComentario.Enabled = false;
    }
    protected void chkProrroga_CheckedChanged(object sender,  EventArgs e)
    {
        if (this.chkProrroga.Checked)
        {
            this.txtFechaProrroga.Enabled = true;
            this.rfvFechaProrroga.Enabled = true;
        }

        else
        {
            this.txtFechaProrroga.Text = string.Empty;
            this.txtFechaProrroga.Enabled = false;
            this.rfvFechaProrroga.Enabled = false;
        }
        this.mpeComentario.Show();
    }

    protected void lnkTarea_Click(object sender, EventArgs e)
    {
        IniciarModalPopup();
        TableCell tblRegistro = ((LinkButton)sender).Parent as TableCell;

        int IntActivityInstance,IntIdActivity,IntProcessInstance;
        IntActivityInstance = Convert.ToInt32(((Label)tblRegistro.FindControl("lblActivityInstance")).Text);
        IntIdActivity = Convert.ToInt32(((Label)tblRegistro.FindControl("lblIdActivity")).Text);
        IntProcessInstance = Convert.ToInt32(((Label)tblRegistro.FindControl("lblProcessInstance")).Text);

        hdfIdProcessInstance.Value = IntProcessInstance.ToString();
        hdfIdActivityInstance.Value = IntActivityInstance.ToString();
        hdfIdActivity.Value = IntIdActivity.ToString();

        // consultamos las acciones obligatorias para la actividad a la cual vamos a ingresar el comentario
        PINES vPINES = new PINES();
        // consultamos si el usuario ya ingreso algun comentario para la actividad
        List<ComentarioActividadIdentity> lstComentarios = new List<ComentarioActividadIdentity>();
        ComentarioActividadIdentity vComentarioActividad = new ComentarioActividadIdentity();
        vComentarioActividad.IDActivityInstance = IntActivityInstance;

        vComentarioActividad.IDActivity = IntIdActivity;
        vComentarioActividad.IDProcessInstance = IntProcessInstance;
        vComentarioActividad.Usuario = usuario2;
        vComentarioActividad.EsGerente = false;
        bool blTieneComentarios = TieneComentarios(vComentarioActividad, out lstComentarios);

        var listaAccions = vPINES.ConsultaAccionActividad(IntIdActivity).AsEnumerable();
        var accionesObligatorias = listaAccions.Where(x => x.Field<Boolean>("OBLIGATORIA") == true && x.Field<Boolean>("ACTIVO") == true).ToList();
        // si no tiene comentarios mostramos la primera obligatoria y las opcionales
        if (!blTieneComentarios)
        {
            var accionesOpc = listaAccions.Where(x => x.Field<Boolean>("OBLIGATORIA") == false && x.Field<Boolean>("ACTIVO") == true).ToList();
            var firstObligatoria = listaAccions.Where(x => x.Field<Boolean>("OBLIGATORIA") == true && x.Field<Boolean>("ACTIVO") == true && x.Field<Int32?>("ORDEN") == 1).ToList();

            var lstAccionesDisponibles = accionesOpc.Union(firstObligatoria);
            lstAccionesDisponibles = lstAccionesDisponibles.OrderBy(x => x.Field<Int32?>("ORDEN"));
            dttAcciones = lstAccionesDisponibles.CopyToDataTable();
            Utilidades.LlenarComboTabla(dttAcciones, this.cboAccionActividad, "DESCRIPCION", "IDACCION", true);
        }
        else
        {
            // validamos si existe alguna accion que detenga el flujo y no se encuentre finalizada
            bool blTieneAccionDetieneFlujo = TieneAccionDetieneFlujo(vComentarioActividad, out lstComentarios);
            if (blTieneAccionDetieneFlujo)
            {
                listaAccions = listaAccions.Where(y => y.Field<Int32>("IDACCION") == lstComentarios.First().IDAccion);
                dttAcciones = listaAccions.CopyToDataTable();
                Utilidades.LlenarComboTabla(dttAcciones, this.cboAccionActividad, "DESCRIPCION", "IDACCION", true);
            }
            else
            {
                // consultamos por cada accion si se encuetra finalizada por el usuario que desea ingresar el comentario
                bool validasiguiente = true;
                foreach (var item in accionesObligatorias)
                {
                    // validamos si el usuario ya creo un comentario para la actividad
                    ComentarioActividadIdentity vComentarioActividadIdentity = new ComentarioActividadIdentity();
                    vComentarioActividadIdentity.IDActivityInstance = IntActivityInstance;
                    vComentarioActividadIdentity.IDActivity = IntIdActivity;
                    vComentarioActividadIdentity.IDProcessInstance = IntProcessInstance;
                    vComentarioActividadIdentity.Usuario = usuario2;
                    vComentarioActividadIdentity.EsGerente = false;
                    vComentarioActividadIdentity.IDAccion = Convert.ToInt32(item.Field<int>("IDACCION"));
                    ComentariosActividad vComentariosActividad = new ComentariosActividad();
                    vComentariosActividad.ConsultarComentarioActividad(ref vComentarioActividadIdentity);
                    if (validasiguiente)
                    {
                        if (vComentarioActividadIdentity.ActividadRealizada == true)
                        {
                            validasiguiente = true;
                            listaAccions = listaAccions.Where(y => y.Field<Int32>("IDACCION") != vComentarioActividadIdentity.IDAccion);
                        }
                        else
                        {
                            validasiguiente = false;
                        }
                    }
                    else
                    {
                        listaAccions = listaAccions.Where(y => y.Field<Int32>("IDACCION") != vComentarioActividadIdentity.IDAccion);
                    }
                }

                if (listaAccions.Count() > 0)
                {
                    listaAccions = listaAccions.OrderBy(x => x.Field<Int32?>("ORDEN"));
                    foreach (DataRow accion in listaAccions)
                    {
                        if (accion.Field<Boolean>("OBLIGATORIA"))
                            accion["DESCRIPCION"] = string.Format("*{0}", accion.Field<string>("DESCRIPCION"));
                    }
                    dttAcciones = listaAccions.CopyToDataTable();
                    Utilidades.LlenarComboTabla(dttAcciones, this.cboAccionActividad, "DESCRIPCION", "IDACCION", true);
                    
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Ventana", "alert('No tiene acciones disponibles para esta actividad');", true);
                    this.mpeComentario.Hide();
                    return;
                }
            }
        }
        
        this.lblActividad.Text = ((LinkButton)sender).Text;
        this.mpeComentario.Show();
    }

    private bool TieneComentarios(ComentarioActividadIdentity vComentarioActividadIdentity, out List<ComentarioActividadIdentity> lstComentarios)
    {
        ComentariosActividad vComentariosActividad = new ComentariosActividad();
        lstComentarios = vComentariosActividad.ListaComentarioActividad(vComentarioActividadIdentity);
        if (lstComentarios.Count() > 0)
            return true;
        return false;
    }
    private bool TieneAccionDetieneFlujo(ComentarioActividadIdentity vComentarioActividadIdentity, out List<ComentarioActividadIdentity> lstComentarios)
    {
        ComentariosActividad vComentariosActividad = new ComentariosActividad();
        lstComentarios = vComentariosActividad.ListaComentarioActividad(vComentarioActividadIdentity);
        if (lstComentarios.Count() > 0)
        {
            var lstAccionDetieneFlujo = lstComentarios.Where(x => x.DetieneFlujo == true && x.ActividadRealizada == false);
            if (lstAccionDetieneFlujo.Count() > 0)
            {
                var AccionDetieneFlujo = lstAccionDetieneFlujo.OrderByDescending(x=> x.IDComentario).First();
                // ahora buscamos si existe una instancia de la accion mas reciente que cambien la propiedad detiene flujo a false
                if (lstComentarios.Where(x => x.DetieneFlujo == false && x.IDAccion == AccionDetieneFlujo.IDAccion && x.IDComentario > AccionDetieneFlujo.IDComentario).Count() > 0)
                    return false;
                return true;
            }
            else
                return false;
        }
        return false;
    }
    protected void IniciarModalPopup()
    {
        this.txtComentario.Text = string.Empty;
        this.txtFechaCompromiso.Text = string.Empty;
        this.txtFechaVencimiento.Text = string.Empty;
        this.chkProrroga.Checked = false;
        this.txtFechaProrroga.Text = string.Empty;
        this.txtFechaProrroga.Enabled = false;
        this.cboEstado.SelectedValue = "0";
        this.rfvFechaProrroga.Enabled = false;
        this.rfvArchivoComentario.Enabled = false;
        this.lnkbArchivoAccion.Visible = false;
        this.chkDetieneFlujo.Checked = false;
        this.chkDetieneFlujo.Enabled = true;
        
    }
    protected void btnAceptar_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            string nombreArchivo = "", extencioArhivo = "", rutarchivoPINES = "";
            string nombreCarpetaProceso = "";
            DateTime dtHoy = DateTime.Now;
            ComentarioActividadIdentity vComentarioActividadIdentity = new ComentarioActividadIdentity();
            ComentariosActividad vComentariosActividad = new ComentariosActividad();
            vComentarioActividadIdentity.IDActivityInstance = Convert.ToInt32(hdfIdActivityInstance.Value);
            vComentarioActividadIdentity.IDActivity = Convert.ToInt32(hdfIdActivity.Value);
            vComentarioActividadIdentity.IDProcessInstance = Convert.ToInt32(hdfIdProcessInstance.Value);
            vComentarioActividadIdentity.Usuario = usuario2;
            vComentarioActividadIdentity.Comments = this.txtComentario.Text;
            vComentarioActividadIdentity.IDAccion = Convert.ToInt32(this.cboAccionActividad.SelectedValue);
            vComentarioActividadIdentity.AutoridadId = Convert.ToInt32(ApplicationUserDao.ObtenerEmpresaUsuario(usuario2).Where(x => x.ToString() != ConfigurationManager.AppSettings["CODE_PINES"].ToString()).First());
            vComentarioActividadIdentity.DetieneFlujo = this.chkDetieneFlujo.Checked;
            vComentarioActividadIdentity.EsGerente = false;
            vComentarioActividadIdentity.ActividadRealizada = this.cboEstado.SelectedValue == "1" ? true : false;
            if (this.txtFechaCompromiso.Text != string.Empty)
                vComentarioActividadIdentity.FechaCompromiso = Convert.ToDateTime(this.txtFechaCompromiso.Text);
            vComentarioActividadIdentity.TieneProrroga = this.chkProrroga.Checked;
            if(vComentarioActividadIdentity.TieneProrroga.Value)
                vComentarioActividadIdentity.FechaProrroga = Convert.ToDateTime(this.txtFechaProrroga.Text);
            if (this.fluArchivoComentario.FileName != string.Empty)
            {
                rutarchivoPINES = ConfigurationManager.AppSettings["FILE_TRAFFIC"] + ConfigurationManager.AppSettings["ARCHIVOS_PINES"];
                nombreCarpetaProceso = string.Format(@"{0}\{1}\", vComentarioActividadIdentity.IDProcessInstance, vComentarioActividadIdentity.IDActivity);
                extencioArhivo = Path.GetExtension(this.fluArchivoComentario.FileName);
                nombreArchivo = string.Format("{0}{1}{2}_{3}{4}{5}{6}", dtHoy.Year, dtHoy.Month, dtHoy.Day, vComentarioActividadIdentity.IDProcessInstance,
                    vComentarioActividadIdentity.IDActivityInstance, vComentarioActividadIdentity.IDActivity, extencioArhivo);
                vComentarioActividadIdentity.Field = nombreCarpetaProceso + nombreArchivo;
                if (!Directory.Exists(rutarchivoPINES + nombreCarpetaProceso))
                {
                    Directory.CreateDirectory(rutarchivoPINES + nombreCarpetaProceso);
                }
                this.fluArchivoComentario.SaveAs(rutarchivoPINES + nombreCarpetaProceso + nombreArchivo);
            }
            vComentarioActividadIdentity.IDProgresoAccion = Convert.ToInt32(this.hdfIdProcesoAccionActividad.Value);
            vComentariosActividad.InsertarComentarioActividad(vComentarioActividadIdentity, Convert.ToBoolean(this.hdfContinuaProcesoAccion.Value));
            ConsultarTareasSinIniciarPINES();
            this.btnAceptar.Attributes.Remove("onclick");
        }
        else
        {
            this.mpeComentario.Show();
        }
       
    }
    protected void cboAccionActividad_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.cboAccionActividad.SelectedValue != "")
        {
            var accion = dttAcciones.AsEnumerable().Where(x => x.Field<Int32>("IDACCION") == Convert.ToInt32(this.cboAccionActividad.SelectedValue)).First();
            // consultamos si la accion seleccionada ya tiene un comentario, si es asi cargamos la fechas de la accion ya creada y solo se habilita el campo de Comentario
            // prorroga, fecha compromiso, estado.
            ComentarioActividadIdentity vComentarioActividadIdentity = new ComentarioActividadIdentity();
            vComentarioActividadIdentity.IDActivityInstance = Convert.ToInt32(hdfIdActivityInstance.Value);
            vComentarioActividadIdentity.IDActivity = Convert.ToInt32(hdfIdActivity.Value);
            vComentarioActividadIdentity.IDProcessInstance = Convert.ToInt32(hdfIdProcessInstance.Value);
            vComentarioActividadIdentity.Usuario = usuario2;
            vComentarioActividadIdentity.EsGerente = false;
            vComentarioActividadIdentity.IDAccion = accion.Field<Int32>("IDACCION");
            ComentariosActividad vComentariosActividad = new ComentariosActividad();
            vComentariosActividad.ConsultarComentarioActividad(ref vComentarioActividadIdentity);
            this.hdfIdProcesoAccionActividad.Value = "0";
            if (vComentarioActividadIdentity.IDComentario != null && vComentarioActividadIdentity.ActividadRealizada == false)
            {
                this.hdfContinuaProcesoAccion.Value = true.ToString();
                this.txtFechaCompromiso.Text = vComentarioActividadIdentity.FechaCompromiso.Value.ToShortDateString();
                this.txtFechaVencimiento.Text = vComentarioActividadIdentity.FechaVencimiento.Value.ToShortDateString();
                this.chkProrroga.Checked = vComentarioActividadIdentity.TieneProrroga.Value;
                this.hdfIdProcesoAccionActividad.Value = vComentarioActividadIdentity.IDProgresoAccion.ToString();
                if (vComentarioActividadIdentity.TieneProrroga.Value)
                    this.txtFechaProrroga.Text = vComentarioActividadIdentity.FechaProrroga.Value.ToShortDateString();
                if (vComentarioActividadIdentity.Field != string.Empty)
                {
                    lnkbArchivoAccion.CommandArgument = vComentarioActividadIdentity.Field;
                    lnkbArchivoAccion.Visible = true;
                }
                else
                {
                    lnkbArchivoAccion.Visible = false;
                }
                this.chkDetieneFlujo.Checked = vComentarioActividadIdentity.DetieneFlujo.Value;
            }
            else 
            {
                this.hdfContinuaProcesoAccion.Value = false.ToString();
                this.txtFechaVencimiento.Text = (new PINESDALC()).CalcularFecha(DateTime.Now, accion.Field<Int32>("DIAS_EJECUCION")).ToShortDateString();
                this.txtFechaCompromiso.Text = this.txtFechaVencimiento.Text;
            }
        }
        else
        {
            this.txtFechaVencimiento.Text = string.Empty;
            this.txtFechaCompromiso.Text = string.Empty;
        }
        this.mpeComentario.Show();

    }
    protected void lnkbArchivoAccion_Click(object sender, EventArgs e)
    {
        LinkButton lnkbArchivoAccion = (LinkButton)sender;
        string rutarchivoPINES = ConfigurationManager.AppSettings["FILE_TRAFFIC"] + ConfigurationManager.AppSettings["ARCHIVOS_PINES"];
        FileInfo Archivo = new FileInfo(rutarchivoPINES + lnkbArchivoAccion.CommandArgument);
        if (Archivo.Exists)
        {
            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + Archivo.Name);
            Response.AddHeader("Content-Length", Archivo.Length.ToString());
            Response.ContentType = "application/octet-stream";
            Response.WriteFile(Archivo.FullName);
            Response.End();
        }
        else
            Mensaje.MostrarMensaje(this, "El archivo no existe.");
        this.mpeComentario.Show();
    }
    protected void btnConsultarVisor_Click(object sender, EventArgs e)
    {
        ConsultarVisor();
    }

    private void ConsultarVisor()
    {
        DateTime? fechaIni=null, fechaFIn=null;
        string nroVital=null,nombreProyecto = null,departamento=null, solicitante = null;
        int? autoridadId=null;

        if(this.txtNroVitalVisor.Text.Trim() != string.Empty)
            nroVital = this.txtNroVitalVisor.Text;
        if(this.txtFechaInicialVisor.Text.Trim() != string.Empty)
            fechaIni = Convert.ToDateTime(this.txtFechaInicialVisor.Text);
        if (this.txtFechaFinalVisor.Text.Trim() != string.Empty)
        {
            fechaFIn = Convert.ToDateTime(this.txtFechaFinalVisor.Text);
            fechaFIn = new DateTime(fechaFIn.Value.Year, fechaFIn.Value.Month, fechaFIn.Value.Day, 23, 59, 59);
        }
        if (this.txtNroVitalVisor.Text.Trim() != string.Empty)
            nroVital = this.txtNroVitalVisor.Text;
        if (this.txtNOmbreProyecto.Text.Trim() != string.Empty)
            nombreProyecto = this.txtNOmbreProyecto.Text;
        if (this.cboDepartamento.SelectedValue != "-1")
            departamento = this.cboDepartamento.SelectedValue;
        if (this.cboAutoridadAmbiental.SelectedValue != "-1")
            autoridadId = Convert.ToInt32(this.cboAutoridadAmbiental.SelectedValue);
        if (this.txtSolicitante.Text != string.Empty)
            solicitante = txtSolicitante.Text;
        PINES clsPines = new PINES();

        DataTable dttVisor = clsPines.ConsultarVisorProceso(nroVital, fechaIni, fechaFIn, departamento, autoridadId, nombreProyecto, solicitante);
        DataTable dttAvanceProyectoEsperado = clsPines.ConsultaAvanceEsperadoHoyProyecto(nroVital, fechaIni, fechaFIn, departamento, autoridadId, nombreProyecto);
        DataSet DatosVisor = new DataSet();
        Session["DatosVisor"] = dttVisor;
        Session["dttAvanceProyectoEsperado"] = dttAvanceProyectoEsperado;
        if (dttVisor.Rows.Count > 0)
        {
            var dttProcesos = dttVisor.AsEnumerable().GroupBy(x => new { IDPROCESSINSTANCE = x.Field<Int32>("IDPROCESSINSTANCE"), NUMERO_VITAL = x.Field<string>("NUMERO_VITAL"), NOMBRE_PROYECTO = x.Field<string>("NOMBRE_PROYECTO"), STARTTIME = x.Field<DateTime>("STARTTIME"), FECHA_FINALIZACION_PROYECTO_CALCULADA = x.Field<DateTime>("FECHA_FINALIZACION_PROYECTO_CALCULADA"), DIAS_TRANSCURRIDOS = x.Field<int>("DIAS_TRANSCURRIDOS"), UBICACION = x.Field<string>("UBICACION"), SOLICITANTE = x.Field<string>("SOLICITANTE") })
                .Select(g => new { IDPROCESSINSTANCE = g.Key.IDPROCESSINSTANCE,
                                   NUMERO_VITAL = g.Key.NUMERO_VITAL,
                                   NOMBRE_PROYECTO = g.Key.NOMBRE_PROYECTO,
                                   STARTTIME = g.Key.STARTTIME,
                                   FECHA_FINALIZACION_PROYECTO_CALCULADA = g.Key.FECHA_FINALIZACION_PROYECTO_CALCULADA,
                                   DIAS_TRANSCURRIDOS = g.Key.DIAS_TRANSCURRIDOS,
                                   UBICACION = g.Key.UBICACION,
                                   SOLICITANTE = g.Key.SOLICITANTE
                }).ToList();
            this.dtlDatos.DataSource = dttProcesos;
            //this.acActividades.Visible = true;
        }
        else
        {
            this.dtlDatos.DataSource = null;
            //this.acActividades.Visible = false;
            // mostrar mensaje de no se encontraron registros
        }
        this.dtlDatos.DataBind();
    }
    protected void dtlDatos_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        decimal porcentajeEsperado = 0;
        int IdProcessInstance = Convert.ToInt32(((Label)e.Item.FindControl("lblProcessInstance")).Text);
        Label lblPorcentajeEsperado = (Label)e.Item.FindControl("lblPorcentajeEsperado");
        Label lblPorcentajeActual = (Label)e.Item.FindControl("lblPorcentajeActual");
       
        Accordion acActividades = (Accordion)e.Item.FindControl("acActividades");
        acActividades.SelectedIndex = -1;
        GridView dtgDatos = (GridView)acActividades.Panes[0].FindControl("dtgDatos");

        var porcentajeAvanceEsperadoProceso = ((DataTable)Session["dttAvanceProyectoEsperado"]).AsEnumerable().Where(x => x.Field<int>("id") == IdProcessInstance).ToList();
        if (porcentajeAvanceEsperadoProceso.Count > 0)
        {
            porcentajeEsperado = (decimal)porcentajeAvanceEsperadoProceso[0][2];
        }
        
        ImageButton imgbArchivosSolicitud = (ImageButton)e.Item.FindControl("imgbArchivosSolicitud");
        ScriptManager1.RegisterPostBackControl(imgbArchivosSolicitud);
        imgbArchivosSolicitud.CommandArgument = IdProcessInstance.ToString();
        dtgDatos.Columns[11].Visible = EsGerente;
        dtgDatos.DataSource = CruzarActividadesCreadasVSNoCreadas(IdProcessInstance, lblPorcentajeActual, lblPorcentajeEsperado, porcentajeEsperado);
        dtgDatos.DataBind();
    }

    private object CruzarActividadesCreadasVSNoCreadas(int IdProcessInstance, Label lblPorcentajeActual, Label lblPorcentajeEsperado, decimal porcentajeEsperado)
    {
        decimal porcentajeActual = 0;
        DataTable dtRegistros = ((DataTable)Session["DatosVisor"]);
        PINES clsPines = new PINES();
        DataTable dtActividadesProceso = clsPines.ConsultaActividadesWorkFlowPINES();
        DataRow[] dtrActividadesProceso = dtRegistros.Select("IDPROCESSINSTANCE=" + IdProcessInstance.ToString());
        foreach (DataRow drtActividad in dtActividadesProceso.Rows)
        {
            int actividades = dtrActividadesProceso.AsEnumerable().Where(y => y.Field<int>("IDACTIVITY") == Convert.ToInt32(drtActividad["IDACTIVITY"])).ToList().Count;
            if (actividades == 0)
            {
                DataRow dtrActividadFaltante = dtRegistros.NewRow();
                dtrActividadFaltante["NUMERO_VITAL"] = dtrActividadesProceso[0]["NUMERO_VITAL"];
                dtrActividadFaltante["NOMBRE_TIPO_TRAMITE"] = dtrActividadesProceso[0]["NOMBRE_TIPO_TRAMITE"];
                dtrActividadFaltante["TAREA"] = drtActividad["TAREA"];
                dtrActividadFaltante["IDPROCESSINSTANCE"] = dtrActividadesProceso[0]["IDPROCESSINSTANCE"];
                dtrActividadFaltante["IDACTIVITYINSTANCE"] = dtrActividadesProceso[0]["IDACTIVITYINSTANCE"];
                dtrActividadFaltante["IDACTIVITY"] = dtrActividadesProceso[0]["IDACTIVITY"];
                dtrActividadFaltante["STATUS"] = -1;
                dtrActividadFaltante["total_comments"] = 0;
                dtrActividadFaltante["porc_ponderado"] = 0.0;
                dtrActividadFaltante["TOTAL_VINCULOS"] = 0;
                dtRegistros.Rows.Add(dtrActividadFaltante);
            }
        }

        var tareasProceso = dtRegistros.AsEnumerable().Where(x => x.Field<Int32>("IDPROCESSINSTANCE") == IdProcessInstance)
            .Select(y => new
            {
                NUMERO_VITAL = y.Field<string>("NUMERO_VITAL"),
                NOMBRE_TIPO_TRAMITE = y.Field<string>("NOMBRE_TIPO_TRAMITE"),
                TAREA = y.Field<string>("TAREA"),
                FECHA_INICIO = y.Field<DateTime?>("FECHA_INICIO"),
                FECHA_VENCIMIENTO = y.Field<DateTime?>("FECHA_VENCIMIENTO"),
                FECHA_FINALIZACION = y.Field<DateTime?>("FECHA_FINALIZACION"),
                GRUPO_RESPONSABLE = y.Field<string>("GRUPO_RESPONSABLE"),
                LAST_COMMENT = y.Field<string>("LAST_COMMENT"),
                total_comments = y.Field<Int32?>("total_comments"),
                STATUS = y.Field<Int32>("STATUS"),
                IDPROCESSINSTANCE = y.Field<Int32>("IDPROCESSINSTANCE"),
                IDACTIVITYINSTANCE = y.Field<Int32>("IDACTIVITYINSTANCE"),
                NOMBRE_PROYECTO = y.Field<string>("NOMBRE_PROYECTO"),
                TIEMPO_ESTIMADO_DIAS = y.Field<int?>("TIEMPO_ESTIMADO_DIAS"),
                FECHA_FINALIZACION_ACT_CALCULADA = y.Field<DateTime?>("FECHA_FINALIZACION_ACT_CALCULADA"),
                LAST_COMMENT_GERENTE = y.Field<string>("LAST_COMMENT_GERENTE"),
                DIAS_A_VENCERCE = y.Field<int?>("DIAS_A_VENCERCE"),
                IDACTIVITY = y.Field<int>("IDACTIVITY"),
                porc_ponderado = y.Field<decimal>("porc_ponderado"),
                TOTAL_VINCULOS = y.Field<int>("TOTAL_VINCULOS")
            }).ToList();

        porcentajeActual = tareasProceso.Where(y => y.STATUS == 248).Sum(x => x.porc_ponderado);

        lblPorcentajeEsperado.Text = string.Format("{0}%", porcentajeEsperado);
        lblPorcentajeActual.Text = string.Format("{0}%", porcentajeActual);
        if (porcentajeActual >= porcentajeEsperado)
        {
            lblPorcentajeEsperado.ForeColor = System.Drawing.ColorTranslator.FromHtml("#319E4C");
            lblPorcentajeActual.ForeColor = System.Drawing.ColorTranslator.FromHtml("#319E4C");
        }
        else
        {
            lblPorcentajeEsperado.ForeColor = System.Drawing.ColorTranslator.FromHtml("#D01C2E");
            lblPorcentajeActual.ForeColor = System.Drawing.ColorTranslator.FromHtml("#D01C2E");
        }
            return tareasProceso;
    }
    protected void dtgDatos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label LblSatus = (Label)e.Row.FindControl("LblSatus");
            Label LblTotalComment = (Label)e.Row.FindControl("LblTotalComment");
            Label lblTotalVinculos = (Label)e.Row.FindControl("lblTotalVinculos");
            LinkButton lnkViculosProyecto = (LinkButton)e.Row.FindControl("lnkViculosProyecto");
            ImageButton imgbComentarios = (ImageButton)e.Row.FindControl("imgbComentarios");
            Image imgEstadoActividad = (Image)e.Row.FindControl("imgEstadoActividad");
            Label LblDiasAVencerce = (Label)e.Row.FindControl("LblDiasAVencerce");
            LinkButton lnkAcciones = (LinkButton)e.Row.FindControl("lnkAcciones");
            Label lblProcessInstance = (Label)e.Row.FindControl("lblProcessInstance");
            Label lblIdActivity = (Label)e.Row.FindControl("lblIdActivity");
            Label lblGrupoResponsable = (Label)e.Row.FindControl("lblGrupoResponsable");
            int diasAVencerce = 0;
            if (LblDiasAVencerce.Text != string.Empty)
                diasAVencerce = Convert.ToInt32(LblDiasAVencerce.Text);
            switch (LblSatus.Text)
            {
                case "248":
                    //e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#DAF7C3");
                    break;
                case "4":
                    //e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2CC81");
                    break;
            }
            if (LblTotalComment.Text == "0")
            {
                imgbComentarios.Visible = false;
                LblTotalComment.Visible = false;
                lnkAcciones.Visible = false;
            }
            DateTime? fechaInicioAct = null;
            if (e.Row.Cells[3].Text != "&nbsp;")
                fechaInicioAct = Convert.ToDateTime(e.Row.Cells[3].Text);
            DateTime? fechaFinAct = null;
            DateTime? fechaFinCalcAct = null;
            if (e.Row.Cells[6].Text != "&nbsp;")
                fechaFinCalcAct = Convert.ToDateTime(e.Row.Cells[6].Text);
            if (e.Row.Cells[4].Text != "&nbsp;")
                fechaFinAct = Convert.ToDateTime(e.Row.Cells[4].Text);
            CalcularEstadoActividad(imgEstadoActividad, fechaInicioAct, fechaFinAct, fechaFinCalcAct, LblSatus.Text, diasAVencerce);
            if (lblTotalVinculos.Text == "0")
                lnkViculosProyecto.Visible = false;
            // consultamos el numero de autoridades ambientales
            PINES clsPines = new PINES();
            List<int> LstActividades = clsPines.ListaActividadesVariosIntervinientes();
            if (LstActividades.Contains(Convert.ToInt32(lblIdActivity.Text)))
            {
                var lstAutoridades = clsPines.ListaAutoridadesSolicitudNombre(Convert.ToInt32(lblProcessInstance.Text), Convert.ToInt32(ConfigurationManager.AppSettings["IDFIELD_FORM_PINES_MUNICIPIO"]));
                lblGrupoResponsable.Text += "</ br> Cars: ";
                foreach (string autoridad in lstAutoridades)
                {
                    lblGrupoResponsable.Text += string.Format("{0}, ", autoridad);
                }
                lblGrupoResponsable.Text = lblGrupoResponsable.Text.Substring(0, lblGrupoResponsable.Text.Length - 2);
            }
        }
    }

    private void CalcularEstadoActividad(Image imgEstadoActividad, DateTime? fechaInicioAct, DateTime? fechaFinAct, DateTime? fechaFinCalcAct, string estado, int? diasProximaaVencerce)
    {
        switch (estado)
        {
            case "248":
                // varificamo si la fecha de finalizacion esta dentro del tiempo esperado
                if (fechaFinAct.Value <= fechaFinCalcAct)
                    imgEstadoActividad.ImageUrl = "~/App_Themes/Img/Chulo_verde.png";
                else if (fechaFinAct.Value > fechaFinCalcAct)
                    imgEstadoActividad.ImageUrl = "~/App_Themes/Img/Chulo_rojo.png";
                imgEstadoActividad.ToolTip = "Finalizada";
                break;
            case "4":
                if (diasProximaaVencerce < Convert.ToInt32(ConfigurationManager.AppSettings["DIAS_ALARMA_VENCIMIENTO"]) && diasProximaaVencerce > 0)
                    imgEstadoActividad.ImageUrl = "~/App_Themes/Img/BotonAmarillo.png";
                else if (diasProximaaVencerce > Convert.ToInt32(ConfigurationManager.AppSettings["DIAS_ALARMA_VENCIMIENTO"]))
                    imgEstadoActividad.ImageUrl = "~/App_Themes/Img/BotonVerde.png";
                else if (diasProximaaVencerce <= 0)
                    imgEstadoActividad.ImageUrl = "~/App_Themes/Img/BotonRojo.png";
                imgEstadoActividad.ToolTip = "En Proceso";
                break;
            case "-1":
                imgEstadoActividad.ImageUrl = "~/App_Themes/Img/BotonGris.png";
                break;

        }
    }
    protected void dtgDatos_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView grilla = (GridView)sender;
        GridViewRow registro = grilla.SelectedRow;
        Label LblIdActivityInstance = (Label)registro.FindControl("LblIdActivityInstance");
        ClientScript.RegisterStartupScript(this.GetType(), "CommentsWindow", string.Format("abrirFormulario('../PINES/ComentarioActividad.aspx?IdActivityInstance={0}',{1},{2});", LblIdActivityInstance.Text,800,400), true);
    }
    protected void imgbArchivosSolicitud_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imgbArchivosSolicitud = (ImageButton)sender;
        ClientScript.RegisterStartupScript(this.GetType(), "CommentsWindow", string.Format("abrirFormulario('../PINES/ArchivosSolicitud.aspx?IdProcessInstance={0}',{1},{2});", imgbArchivosSolicitud.CommandArgument, 350, 400), true);
    }
    protected void cboEstado_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.cboEstado.SelectedValue == "1")
        {
            this.rfvArchivoComentario.Enabled = true;
            this.btnAceptar.Attributes.Add("onclick", "return confirm('Esta seguro de finalizar la Actividad');");
        }
        else
        {
            this.rfvArchivoComentario.Enabled = false;
            this.btnAceptar.Attributes.Remove("onclick");
        }
        this.mpeComentario.Show();

    }
    protected void lnkComentarioGerente_Click(object sender, EventArgs e)
    {
        TableCell tblRegistro = ((LinkButton)sender).Parent as TableCell;

        int IntActivityInstance, IntIdActivity, IntProcessInstance;
        IntActivityInstance = Convert.ToInt32(((Label)tblRegistro.FindControl("LblIdActivityInstance")).Text);
        IntIdActivity = Convert.ToInt32(((Label)tblRegistro.FindControl("lblIdActivity")).Text);
        IntProcessInstance = Convert.ToInt32(((Label)tblRegistro.FindControl("lblProcessInstance")).Text);

        hdfIdProcessInstance.Value = IntProcessInstance.ToString();
        hdfIdActivityInstance.Value = IntActivityInstance.ToString();
        hdfIdActivity.Value = IntIdActivity.ToString();

        ComentarioActividadIdentity vComentarioActividadIdentity = new ComentarioActividadIdentity();
        vComentarioActividadIdentity.IDActivityInstance = IntActivityInstance;
        vComentarioActividadIdentity.IDActivity = IntIdActivity;
        vComentarioActividadIdentity.IDProcessInstance = IntProcessInstance;
        vComentarioActividadIdentity.Usuario = usuario2;
        vComentarioActividadIdentity.EsGerente = true;
        ComentariosActividad vComentariosActividad = new ComentariosActividad();
        vComentariosActividad.ConsultarComentarioActividad(ref vComentarioActividadIdentity);
        GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
        this.lblActividadComentGerente.Text = row.Cells[1].Text;
        this.txtComentGerente.Text = string.Empty;
        this.mpeComentarioGerente.Show();
    }
    protected void btnAceptarComentGerente_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            //string nombreArchivo = "", extencioArhivo = "", rutarchivoPINES = "";
            //string nombreCarpetaProceso = "";
            DateTime dtHoy = DateTime.Now;
            ComentarioActividadIdentity vComentarioActividadIdentity = new ComentarioActividadIdentity();
            ComentariosActividad vComentariosActividad = new ComentariosActividad();
            vComentarioActividadIdentity.IDActivityInstance = Convert.ToInt32(hdfIdActivityInstance.Value);
            vComentarioActividadIdentity.IDActivity = Convert.ToInt32(hdfIdActivity.Value);
            vComentarioActividadIdentity.IDProcessInstance = Convert.ToInt32(hdfIdProcessInstance.Value);
            vComentarioActividadIdentity.Usuario = usuario2;
            vComentarioActividadIdentity.Comments = this.txtComentGerente.Text;
            vComentarioActividadIdentity.EsGerente = true;
            vComentariosActividad.InsertarComentarioActividad(vComentarioActividadIdentity, false);
            ConsultarVisor();
        }
        else
        {
            this.mpeComentarioGerente.Show();
        }

    }
    protected void btnAceptarVincular_OnClick(object sender, EventArgs e)
    {
        if (IsValid)
        {
            ActividadProcesoURL vActividadProcesoURL = new ActividadProcesoURL();
            vActividadProcesoURL.IdActivityInstance = Convert.ToInt32(hdfIdActivityInstance.Value);
            vActividadProcesoURL.IdProcessInstance = Convert.ToInt32(hdfIdProcessInstance.Value);
            vActividadProcesoURL.Usuario = usuario2;
            vActividadProcesoURL.EsVITAL = this.chkVital.Checked;
            if (this.chkVital.Checked == true)
            {
                string urlVital = ConfigurationManager.AppSettings["URL_PROYECTO_VITAL"].ToString();
                SILPA.LogicaNegocio.DAA.SolicitudDAAEIA objTramite = new SILPA.LogicaNegocio.DAA.SolicitudDAAEIA();
                objTramite.ConsultarSolicitudNumeroSILPA(this.txtUrlActividadProyeto.Text);
                if (objTramite.Identity != null)
                {
                    int solicitante = Convert.ToInt32(objTramite.Identity.IdSolicitante);
                    // armamos la url
                    string urlProyectoVITAL = string.Format(urlVital, this.txtUrlActividadProyeto.Text, solicitante);
                    urlProyectoVITAL = urlProyectoVITAL.Replace("y", "&");
                    vActividadProcesoURL.UrlProyecto= urlProyectoVITAL;
                    vActividadProcesoURL.NroVITAL = this.txtUrlActividadProyeto.Text;
                }

            }
            else
            {
                vActividadProcesoURL.UrlProyecto = this.txtUrlActividadProyeto.Text.Trim();
            }
            PINES clsPines = new PINES();
            clsPines.InsertarActividadProcesoUrl(vActividadProcesoURL);
        }
        else
        {
            this.mpeVincularProyecto.Show();
        }
    }
    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        ConsultarTareasSinIniciarPINES();
    }
    protected void lnkVincularProyecto_Click(object sender, EventArgs e)
    {
        TableCell tblRegistro = ((LinkButton)sender).Parent as TableCell;

        this.txtUrlActividadProyeto.Text = string.Empty;

        int IntActivityInstance, IntIdActivity, IntProcessInstance;
        IntActivityInstance = Convert.ToInt32(((Label)tblRegistro.FindControl("lblActivityInstance")).Text);
        IntIdActivity = Convert.ToInt32(((Label)tblRegistro.FindControl("lblIdActivity")).Text);
        IntProcessInstance = Convert.ToInt32(((Label)tblRegistro.FindControl("lblProcessInstance")).Text);
        string actividad = ((LinkButton)tblRegistro.FindControl("lnkTarea")).Text;

        hdfIdProcessInstance.Value = IntProcessInstance.ToString();
        hdfIdActivityInstance.Value = IntActivityInstance.ToString();
        hdfIdActivity.Value = IntIdActivity.ToString();
        GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
        this.lblActividadVincular.Text = actividad;
        // consultamos si ya existe una url asociada a la actividad y con el usuario
        PINES clsPines = new PINES();
        ActividadProcesoURL vActividadProcesoURL = new ActividadProcesoURL { IdProcessInstance = IntProcessInstance, IdActivityInstance = IntActivityInstance, Usuario = usuario2};
        clsPines.ConsultarActividadProcesoUrl(ref vActividadProcesoURL);
        if (vActividadProcesoURL.UrlProyecto != null)
        {
            this.chkVital.Checked = vActividadProcesoURL.EsVITAL;
            if (vActividadProcesoURL.EsVITAL)
            {
                this.lblValorProyecto.Text = "Nro.Vital:";
                this.txtUrlActividadProyeto.Text = vActividadProcesoURL.NroVITAL;
            }
            else
            {
                this.lblValorProyecto.Text = "URL:";
                this.txtUrlActividadProyeto.Text = vActividadProcesoURL.UrlProyecto;
            }
        }
        else
        {
            this.chkVital.Checked = false;
            this.lblValorProyecto.Text = "URL:";
        }
        this.mpeVincularProyecto.Show();
     }
    protected void lnkViculosProyecto_Click(object sender, EventArgs e)
    {
        TableCell tblRegistro = ((LinkButton)sender).Parent as TableCell;
        int IntActivityInstance, IntProcessInstance;
        IntActivityInstance = Convert.ToInt32(((Label)tblRegistro.FindControl("LblIdActivityInstance")).Text);
        IntProcessInstance = Convert.ToInt32(((Label)tblRegistro.FindControl("lblProcessInstance")).Text);
        GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
        this.lblActividadVinculo.Text = row.Cells[1].Text;

        hdfIdProcessInstance.Value = IntProcessInstance.ToString();
        hdfIdActivityInstance.Value = IntActivityInstance.ToString();
        PINES clsPines = new PINES();
        this.grvVinculosProyecto.DataSource = clsPines.ListaActividadProcesoUrl(new ActividadProcesoURL { IdProcessInstance = IntProcessInstance, IdActivityInstance = IntActivityInstance });
        this.grvVinculosProyecto.DataBind();
        this.mpeVinculosActividad.Show();

    }
    protected void btnProbarUrl_Click(object sender, EventArgs e)
    {
        if (this.chkVital.Checked == true)
        {
            string urlVital = ConfigurationManager.AppSettings["URL_PROYECTO_VITAL"].ToString();
            SILPA.LogicaNegocio.DAA.SolicitudDAAEIA objTramite = new SILPA.LogicaNegocio.DAA.SolicitudDAAEIA();
            objTramite.ConsultarSolicitudNumeroSILPA(this.txtUrlActividadProyeto.Text);
            if (objTramite.Identity != null)
            {
                int solicitante = Convert.ToInt32(objTramite.Identity.IdSolicitante);
                // armamos la url
                string urlProyectoVITAL = string.Format(urlVital, this.txtUrlActividadProyeto.Text, solicitante);
                urlProyectoVITAL = urlProyectoVITAL.Replace("y", "&");
                ClientScript.RegisterStartupScript(this.GetType(), "URLProyecto", string.Format("ProyectoVital('{0}');", urlProyectoVITAL), true);
            }

        }
        this.mpeVincularProyecto.Show();
    }
    protected void lnkFinalizarTarea_Click(object sender, EventArgs e)
    {
        TableCell tblRegistro = ((LinkButton)sender).Parent as TableCell;

        this.txtUrlActividadProyeto.Text = string.Empty;

        int IntActivityInstance, IntIdActivity, IntProcessInstance;
        IntActivityInstance = Convert.ToInt32(((Label)tblRegistro.FindControl("lblActivityInstance")).Text);
        IntIdActivity = Convert.ToInt32(((Label)tblRegistro.FindControl("lblIdActivity")).Text);
        IntProcessInstance = Convert.ToInt32(((Label)tblRegistro.FindControl("lblProcessInstance")).Text);
        string actividad = ((LinkButton)tblRegistro.FindControl("lnkTarea")).Text;
        // validamos si la actividad tiene acciones pendientes por terminar
        AccionActividad clsAccionActividad = new AccionActividad();

        var accionesActividadProcessInstance = clsAccionActividad.ConsultaEstadoActivityProcessInstance(IntProcessInstance, IntIdActivity).AsEnumerable();
        if (accionesActividadProcessInstance.Where(x => x.Field<bool>("ActividadRealizada") == false).Count() > 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Ventana", "alert('Tiene Acciones Pendientes por finalizar');", true);
        }
        else
        {
            PINES clsPines = new PINES();
            try
            {
                clsPines.FinalizarActivityInstance(IntIdActivity, IntProcessInstance,
                IntActivityInstance, Convert.ToInt32(ConfigurationManager.AppSettings["IDFIELD_FORM_PINES_MUNICIPIO"]), usuario2);
                ConsultarTareasSinIniciarPINES();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Ventana", string.Format("alert('{0}');",ex.Message), true);
            }
            
        }

    }
    protected void chkVital_CheckedChanged(object sender, EventArgs e)
    {
        if (this.chkVital.Checked == true)
        {
            lblValorProyecto.Text = "Nro.Vital:";
        }
        else
        {
            lblValorProyecto.Text = "URL:";
        }
        this.mpeVincularProyecto.Show();
    }

    public object vComentarioActividadIdentity { get; set; }
    protected void dtgDatos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "VerAcciones":
                string strIdProcessInstance, strIdActivity;
                strIdProcessInstance = ((Label)dtgDatos.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblProcessInstance")).Text;
                strIdActivity = ((Label)dtgDatos.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblIdActivity")).Text;
                ClientScript.RegisterStartupScript(this.GetType(), "CommentsWindow", string.Format("abrirFormulario('../PINES/EstadoAccion.aspx?PI={0}&IDA={1}',{2},{3});", strIdProcessInstance, strIdActivity, 570, 500), true);
                break;
        }
    }
}

