using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using SoftManagement.Log;
using System.Data.SqlTypes;
using SILPA.LogicaNegocio.Generico;

public partial class Salvoconducto_ConsultarSalvoconductoWeb : System.Web.UI.Page
{
    private static bool Validado;
    private static int idUser;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Validado = false;
            idUser = 0;

            //DESCOMENTAR ANTES DEL COMMIT!!!
            Validado = EsAutoridad(Silpa.Workflow.AccesoDatos.ApplicationUserDao.ObtenerIdUsuario(DatosSesion.Usuario));

            // USUARIO PARA PRUEBAS
            //Validado = EsAutoridad(Silpa.Workflow.AccesoDatos.ApplicationUserDao.ObtenerIdUsuario("52010"));

            if (Validado)
            {
                SILPA.LogicaNegocio.Generico.Listas _listas = new SILPA.LogicaNegocio.Generico.Listas();

                this.pnAdfmin.Visible = true;

                DataSet _temp = _listas.ListarAutoridadesActivas();
                this.cboTipoAA.DataSource = _temp;
                this.cboTipoAA.DataValueField = "AUT_ID";
                this.cboTipoAA.DataTextField = "AUT_NOMBRE";
                this.cboTipoAA.DataBind();
                this.cboTipoAA.Items.Insert(0, new ListItem("Seleccione...", "-1"));

                _temp = _listas.ListarTipoSalvoconducto();
                this.cboTipoSalv.DataSource = _temp;
                this.cboTipoSalv.DataValueField = "TSA_ID";
                this.cboTipoSalv.DataTextField = "TSA_NOMBRE";
                this.cboTipoSalv.DataBind();
                this.cboTipoSalv.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            }
            else
                this.pnAdfmin.Visible = false;
        }
    }

    private bool EsAutoridad(long userId)
    {
        idUser = (int)userId;
        Persona objPer = new Persona();
        return objPer.ConsultarPersonaSun(idUser.ToString());        
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        try
        {          
            SILPA.LogicaNegocio.Generico.Listas lstSalvo2 = new SILPA.LogicaNegocio.Generico.Listas();
            DataSet ds2 = lstSalvo2.ListarSalvoconductoDetalle(0);
            this.grdDetalleSalvoconductos.DataSource = ds2;
            this.grdDetalleSalvoconductos.DataBind();

            DataSet ds4 = lstSalvo2.ListarSalvoconducto(0,"0");
            this.grdSalvoconductos.DataSource = ds4;
            this.grdSalvoconductos.DataBind();

            DataSet ds3 = lstSalvo2.ListarSalvoconductoEspecimen(0);
            this.grdSalvoconductoEspecimen.DataSource = ds3;
            this.grdSalvoconductoEspecimen.DataBind();

            if (!Validado)
            {
                if (!String.IsNullOrEmpty(this.txtNumeroSalvoconducto.Text))
                {
                    this.lblNumeroObligatorio.Visible = false;
                    SILPA.LogicaNegocio.Generico.Listas lstSalvo = new SILPA.LogicaNegocio.Generico.Listas();

                    pnlSalvoconductos.Visible = true;
                    DataSet ds = lstSalvo.ListarSalvoconducto(0, this.txtNumeroSalvoconducto.Text, null, null, null, null, idUser);

                    this.grdSalvoconductos.DataSource = ds;
                    this.grdSalvoconductos.DataBind();

                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count == 0) this.lblMensajeError.Visible = true;
                        else this.lblMensajeError.Visible = false;
                    }
                    else this.lblMensajeError.Visible = true;

                    GenerarLogAuditoria CrearLogAuditoria = new GenerarLogAuditoria();
                    CrearLogAuditoria.Insertar("SALVOCONDUCTO", 2, "Se consultó SalvoConducto Web");
                }
                else
                {
                    this.lblNumeroObligatorio.Text="El número de salvoconducto es obligatorio";
                    this.lblNumeroObligatorio.Visible = true;                   
                }
            }
            else
            {
                if (!String.IsNullOrEmpty(this.txtNumeroSalvoconducto.Text) || !String.IsNullOrEmpty(this.txtFechaInicial.Text) || !String.IsNullOrEmpty(this.txtFechaInicial2.Text) || this.cboTipoSalv.SelectedValue != "-1" || this.cboTipoAA.SelectedValue != "-1")
                {
                    this.lblNumeroObligatorio.Visible = false;
                    SILPA.LogicaNegocio.Generico.Listas lstSalvo = new SILPA.LogicaNegocio.Generico.Listas();

                    DateTime fechaInicial;
                    DateTime fechaFinal;
                    if (!DateTime.TryParse(this.txtFechaInicial.Text, out fechaInicial))
                        fechaInicial = Convert.ToDateTime("1900/01/01");
                    if (!DateTime.TryParse(this.txtFechaInicial2.Text, out fechaFinal))
                        fechaFinal = Convert.ToDateTime("2050/01/01");

                    int? tipoSalvoconducto;
                    if (this.cboTipoSalv.SelectedValue == "-1")
                        tipoSalvoconducto = null;
                    else
                        tipoSalvoconducto = int.Parse(this.cboTipoSalv.SelectedValue);

                    int? tipoAA;
                    if (this.cboTipoAA.SelectedValue == "-1")
                        tipoAA = null;
                    else
                        tipoAA = int.Parse(this.cboTipoAA.SelectedValue);

                    pnlSalvoconductos.Visible = true;
                    DataSet ds = lstSalvo.ListarSalvoconducto(0, this.txtNumeroSalvoconducto.Text.Trim(), fechaInicial, fechaFinal, tipoSalvoconducto, tipoAA, null);
                    this.grdSalvoconductos.DataSource = ds;
                    //this.grdSalvoconductos.DataSource = lstSalvo.ListarSalvoconducto(0, this.txtNumeroSalvoconducto.Text);
                    this.grdSalvoconductos.DataBind();

                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count == 0) this.lblMensajeError.Visible = true;
                        else this.lblMensajeError.Visible = false;
                    }
                    else this.lblMensajeError.Visible = true;

                    GenerarLogAuditoria CrearLogAuditoria = new GenerarLogAuditoria();
                    CrearLogAuditoria.Insertar("SALVOCONDUCTO", 2, "Se consultó SalvoConducto Web");
                }
                else
                {
                    this.lblNumeroObligatorio.Text = "Seleccione por lo menos un filtro para realizar la busqueda.";
                    this.lblNumeroObligatorio.Visible = true;       
                }
               
            }
        }
        catch (Exception ex)
        {
            this.lblNumeroObligatorio.Text ="Ha ocurrido un error en el proceso comuniquese con el Administrador";
            this.lblNumeroObligatorio.Visible = true;   
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ex.ToString());
        }        
    }
    private void MostrarLista(DataRow drDatos)
    {
        DataTable tbDatosFill = new DataTable();
        tbDatosFill.Columns.Add("VALORCAMPO");
        tbDatosFill.Columns.Add("VALOR");


        DataRow drFill = tbDatosFill.NewRow();
        drFill[0] = "DETALLE SALVOCONDUCTO";
        drFill[1] = "";
        tbDatosFill.Rows.Add(drFill);

        drFill = tbDatosFill.NewRow();
        drFill[0] = "Corporación Autónoma que expide el salvoconducto";
        drFill[1] = drDatos["AUT_NOMBRE"].ToString();
        tbDatosFill.Rows.Add(drFill);


        drFill = tbDatosFill.NewRow();
        drFill[0] = "Número de Salvoconducto";
        drFill[1] = drDatos["SAV_NUMERO"].ToString();
        tbDatosFill.Rows.Add(drFill);

        drFill = tbDatosFill.NewRow();
        drFill[0] = "Tipo Salvoconducto";
        drFill[1] = drDatos["TSA_NOMBRE"].ToString();
        tbDatosFill.Rows.Add(drFill);

        drFill = tbDatosFill.NewRow();
        drFill[0] = "Fecha Inicial";
        drFill[1] = drDatos["SAV_FECHA_DESDE"].ToString();
        tbDatosFill.Rows.Add(drFill);

        drFill = tbDatosFill.NewRow();
        drFill[0] = "Fecha Final";
        drFill[1] = drDatos["SAV_FECHA_HASTA"].ToString();
        tbDatosFill.Rows.Add(drFill);



        string idSav = drDatos["SAV_ID"].ToString();
        SILPA.LogicaNegocio.Generico.Listas lstSalvo = new SILPA.LogicaNegocio.Generico.Listas();


        DataSet ds4 = lstSalvo.ListarSalvoconductoRuta(Int64.Parse(idSav));

        if (ds4.Tables.Count > 0)
        {
            int i = 1;
            foreach (DataRow row in ds4.Tables[0].Rows)
            {
                drFill = tbDatosFill.NewRow();
                drFill[0] = "Ruta Punto No. " + i.ToString();
                drFill[1] = row["UBICACION"].ToString();
                tbDatosFill.Rows.Add(drFill);
                i += 1;
            }
        }


        DataSet ds2 = lstSalvo.ListarSalvoconductoEspecimen(Int64.Parse(idSav));

        if (ds2.Tables.Count > 0)
        {
            int i = 1;
            foreach (DataRow row in ds2.Tables[0].Rows)
            {
                drFill = tbDatosFill.NewRow();
                drFill[0] = "ESPECIMEN No. " + i.ToString();
                drFill[1] = "";
                tbDatosFill.Rows.Add(drFill);


                drFill = tbDatosFill.NewRow();
                drFill[0] = "Nombre Comun";
                drFill[1] = row["ESP_NOMBRE_COMUN"].ToString();
                tbDatosFill.Rows.Add(drFill);

                drFill = tbDatosFill.NewRow();
                drFill[0] = "Descripción";
                drFill[1] = row["ESP_DESCRIPCION"].ToString();
                tbDatosFill.Rows.Add(drFill);

                drFill = tbDatosFill.NewRow();
                drFill[0] = "Cantidad";
                drFill[1] = Convert.ToDouble(row["ESP_CANTIDAD"]).ToString("N2");
                tbDatosFill.Rows.Add(drFill);

                drFill = tbDatosFill.NewRow();
                drFill[0] = "Dimensiones";
                drFill[1] = row["ESP_DIMENSIONES"].ToString();
                tbDatosFill.Rows.Add(drFill);

                drFill = tbDatosFill.NewRow();
                drFill[0] = "Unidad de Medida";
                drFill[1] = row["UME_NOMBRE"].ToString();
                tbDatosFill.Rows.Add(drFill);

                i += 1;
            }
        }

        DataSet ds3 = lstSalvo.ListarSalvoconductoTransporte(Int64.Parse(idSav));

        if (ds3.Tables.Count > 0)
        {
            int i = 1;
            foreach (DataRow row in ds3.Tables[0].Rows)
            {
                drFill = tbDatosFill.NewRow();
                drFill[0] = "TRANSPORTE No. " + i.ToString();
                drFill[1] = "";
                tbDatosFill.Rows.Add(drFill);


                drFill = tbDatosFill.NewRow();
                drFill[0] = "Modo Transporte";
                drFill[1] = row["MTR_NOMBRE"].ToString();
                tbDatosFill.Rows.Add(drFill);

                drFill = tbDatosFill.NewRow();
                drFill[0] = "Nombre empresa";
                drFill[1] = row["TRA_NOMBRE_EMPRESA"].ToString();
                tbDatosFill.Rows.Add(drFill);

                drFill = tbDatosFill.NewRow();
                drFill[0] = "Tipo Vehiculo";
                drFill[1] = row["TVE_NOMBRE"].ToString();
                tbDatosFill.Rows.Add(drFill);

                drFill = tbDatosFill.NewRow();
                drFill[0] = "Matricula";
                drFill[1] = row["TRA_MATRICULA"].ToString();
                tbDatosFill.Rows.Add(drFill);

                drFill = tbDatosFill.NewRow();
                drFill[0] = "Tipo Identificación Responsable";
                drFill[1] = row["TID_NOMBRE"].ToString();
                tbDatosFill.Rows.Add(drFill);

                drFill = tbDatosFill.NewRow();
                drFill[0] = "Número Identificación Responsable";
                drFill[1] = row["TRA_NUMERO_IDENTIFICACION"].ToString();
                tbDatosFill.Rows.Add(drFill);

                drFill = tbDatosFill.NewRow();
                drFill[0] = "Nombre Responsable";
                drFill[1] = row["TRA_NOMBRE_RESPONSABLE"].ToString();
                tbDatosFill.Rows.Add(drFill);

                i += 1;
            }
        }


        this.grdDetalleSalvoconductos.DataSource = tbDatosFill;
        this.grdDetalleSalvoconductos.DataBind();

    }
    protected void grdSalvoconductos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        
        if (Convert.ToInt32(e.CommandArgument) > -1)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            Label idProcessInstance = (Label)this.grdSalvoconductos.Rows[index].FindControl("lblProcessInstance");
            Label idSalvoconducto = (Label)this.grdSalvoconductos.Rows[index].FindControl("lblID");
            if (!string.IsNullOrEmpty(idProcessInstance.Text))
            {
                SILPA.LogicaNegocio.Generico.Listas lstSalvo = new SILPA.LogicaNegocio.Generico.Listas();
                DataSet ds = lstSalvo.ListarSalvoconducto(0, this.txtNumeroSalvoconducto.Text);
                //DataSet ds = lstSalvo.ListarSalvoconductoDetalle(Int64.Parse(idProcessInstance.Text));
                //this.grdDetalleSalvoconductos.DataSource = ds;                
                //this.grdDetalleSalvoconductos.DataBind();
                string prueba = String.Concat("1", "1");
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        MostrarLista(ds.Tables[0].Rows[0]);
                    }
                    else
                    {
                        this.grdDetalleSalvoconductos.DataSource = ds;
                        this.grdDetalleSalvoconductos.DataBind();
                    }
                }

                DataSet ds2 = lstSalvo.ListarSalvoconductoEspecimen(Int64.Parse(idSalvoconducto.Text));
                this.grdSalvoconductoEspecimen.DataSource = ds2;
                this.grdSalvoconductoEspecimen.DataBind();
            }
            else
            {
                SILPA.LogicaNegocio.Generico.Listas lstSalvo = new SILPA.LogicaNegocio.Generico.Listas();
                DataSet ds = lstSalvo.ListarSalvoconductoDetalle(0);
                this.grdDetalleSalvoconductos.DataSource = ds;
                this.grdDetalleSalvoconductos.DataBind();

                DataSet ds2 = lstSalvo.ListarSalvoconductoEspecimen(0);
                this.grdSalvoconductoEspecimen.DataSource = ds2;
                this.grdSalvoconductoEspecimen.DataBind();
            }
        }
    }
}
