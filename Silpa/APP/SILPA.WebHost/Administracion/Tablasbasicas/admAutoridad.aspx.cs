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
using SILPA.Comun;
using SILPA.LogicaNegocio.Audiencia.AdmTablasBasicas;
using SoftManagement.Log;

public partial class Administracion_Tablasbasicas_admAutoridad : System.Web.UI.Page
{
    #region Documentacion Metodo
    /// <summary>
    /// Este metodo se encarga de cargar la pagina en su estado inicial
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    #endregion
    protected void Page_Load(object sender, EventArgs e){
    }
    
    #region Insercion y edicion

    #region Documentacion Metodo
    /// <summary>
    /// Metodo que responde al evento click del segundo boton que se encarga de
    /// adicionar un nuevo parametro a la lista basica
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    #endregion
    protected void btn_salvar_valor_dos_Click(object sender, EventArgs e)
    {
        //HAVA:30-MAR-10
        bool existeBase = false;
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".btn_salvar_valor_dos_Click.Inicio");

            //existeBase = this.oTBasica.ExisteAutoridadBase(this.oConfig);

            if (this.chk_base.Checked == true && existeBase == true)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "msgBox", "<script>alert(' Ya existe una autoriad ambiental base .')</script>");
                return;
            }
            this.ods_valores.Insert();

            //this.oTBasica.NombreTabla = this.lbl_tablas_basicas.Text;
            //this.oTBasica.Nombre = this.txt_valor.Text;
            //this.oTBasica.Direccion = this.txt_direccion.Text;
            //this.oTBasica.Telefono = this.txt_telefono.Text;
            //this.oTBasica.Fax = this.txt_fax.Text;
            //this.oTBasica.Correo = this.txt_correo.Text;
            //this.oTBasica.Nit = this.txt_nit.Text;
            //this.oTBasica.Activo = this.chk_activo.Checked;


            this.txt_valor.Text = string.Empty;
            this.chk_activo.Checked = false;
            this.txt_direccion.Text = "";
            this.txt_telefono.Text = "";
            this.txt_fax.Text = "";
            this.txt_correo.Text = "";
            this.txt_nit.Text = "";
            this.chk_base.Checked = false;
            this.txtDescripcion.Text = "";
            this.chk_radicacion.Checked = false;
            //oBita.adicionarAccion_mc(Accion.Adicionar, Convert.ToInt32(Modulo.Tablas_Basicas), this.oTBasica.NombreTabla, "", this.oTBasica);
            //crea_folder_Reportes_AA(this.oTBasica.Nombre);
        }
        catch (Exception ex)
        {
            if (this.chk_base.Checked && existeBase)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "msgBox", "<script>alert('No se puede adicionar el registro, recuerde que no pueden existir dos autoridades ambientales marcadas como base.')</script>");
                return;
            }
            else
                ClientScript.RegisterStartupScript(this.GetType(), "msgBox", "<script>alert('Error al crear la autoridad ambiental')</script>");

            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".btn_salvar_valor_dos_Click.Finalizo");
        }
    }

    #region Documentacion Metodo
    /// <summary>
    /// Metodo que responde al evento click del boton encargado de la operacion
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    #endregion
    protected void btn_Cancelar_Click(object sender, EventArgs e)
    {
        //HAVA:30-MAR-10
        bool existeBase = false;
        try
        {

            Response.Redirect("TablasBasicas.aspx");
        }
        catch
        {
            if (this.chk_base.Checked && existeBase)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "msgBox", "<script>alert('No se puede adicionar el registro, recuerde que no pueden existir dos autoridades ambientales marcadas como base.')</script>");
                return;
            }
            else
                ClientScript.RegisterStartupScript(this.GetType(), "msgBox", "<script>alert('Error al crear la autoridad ambiental')</script>");
        }
    }

    #region Documentacion Metodo
    /// <summary>
    /// Metodo que responde al evento click del primer boton que se encarga de 
    /// adicionar un nuevo parametro a la lista basica
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    #endregion
    protected void btn_salvar_valor_Click(object sender, EventArgs e)
    {

        bool existeBase = false;
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".btn_salvar_valor_Click.Inicio");
            // hava:30-mar-10
            //existeBase = this.oTBasica.ExisteAutoridadBase(this.oConfig);

            if (this.chk_base.Checked && existeBase)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "msgBox", "<script>alert(' Ya existe una autoriad ambiental base .')</script>");
                return;
            }

            this.ods_valores.Insert();

            //this.oTBasica.NombreTabla = this.lbl_tablas_basicas.Text;
            //this.oTBasica.Nombre = this.txt_valor.Text;
            //this.oTBasica.Direccion = this.txt_direccion.Text;
            //this.oTBasica.Telefono = this.txt_telefono.Text;
            //this.oTBasica.Fax = this.txt_fax.Text;
            //this.oTBasica.Correo = this.txt_correo.Text;
            //this.oTBasica.Nit = this.txt_nit.Text;
            //this.oTBasica.Activo = this.chk_activo.Checked;


            this.txt_valor.Text = string.Empty;
            this.chk_activo.Checked = false;
            this.txt_direccion.Text = "";
            this.txt_telefono.Text = "";
            this.txt_fax.Text = "";
            this.txt_correo.Text = "";
            this.txt_nit.Text = "";
            this.chk_base.Checked = false;
            this.chk_radicacion.Checked = false;
            //oBita.adicionarAccion_mc(Accion.Adicionar, Convert.ToInt32(Modulo.Tablas_Basicas), this.oTBasica.NombreTabla, "", this.oTBasica);
            //crea_folder_Reportes_AA(this.oTBasica.Nombre);
        }
        catch (Exception ex)
        {
            if (this.chk_base.Checked && existeBase)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "msgBox", "<script>alert('No se puede adicionar el registro, recuerde que no pueden existir dos autoridades ambientales marcadas como base.')</script>");
                return;
            }
            else
                ClientScript.RegisterStartupScript(this.GetType(), "msgBox", "<script>alert('Error al crear la autoridad ambiental')</script>");

            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".btn_salvar_valor_Click.Finalizo");
        }
    }

    //private void crea_folder_Reportes_AA(string nombreAutAmb)
    //{
    //    ReportingService2005 rs = new ReportingService2005();

    //    ICredentials credentials = new NetworkCredential(ConfigurationManager.AppSettings["str_usr_rep_svr"], ConfigurationManager.AppSettings["str_pwd_rep_svr"], ConfigurationManager.AppSettings["str_dom_rep_svr"]);

    //    rs.Credentials = credentials;
    //    rs.Url = ConfigurationManager.AppSettings["ws_reports.ReportService2005"].ToString();

    //    try
    //    {
    //        //rs.CreateFolder(nombreAutAmb, "/", null);
    //        //rs.CreateFolder("SILAMC", "/" + nombreAutAmb, null);
    //        //rs.CreateFolder("Evaluacion", "/" + nombreAutAmb + "/SILAMC", null);
    //        //rs.CreateFolder("General", "/" + nombreAutAmb + "/SILAMC", null);
    //        //rs.CreateFolder("Permisos", "/" + nombreAutAmb + "/SILAMC", null);
    //        //rs.CreateFolder("RUS", "/" + nombreAutAmb + "/SILAMC", null);
    //        //rs.CreateFolder("Seguimiento", "/" + nombreAutAmb + "/SILAMC", null);

    //    }
    //    catch (SoapException e)
    //    {
    //        ClientScript.RegisterStartupScript(this.GetType(), "msgBox", "<script>alert('Message error:" + e.Detail.InnerXml + "')</script>");
    //    }

    //}

    #region Documentación
    /// <summary>
    /// Este metodo responde al evento click del boton que se encarga de visualizar todos 
    /// los registros contenidos en el GridView 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    #endregion
    protected void imb_ver_todos_Click(object sender, ImageClickEventArgs e)
    {
        //this.dgv_tabla.PageSize = this.oConfig.DataGridPageSize * this.dgv_tabla.PageCount;
        this.dgv_tabla.PageSize = 10 * this.dgv_tabla.PageCount;
        this.dgv_tabla.AllowPaging = false;
        this.dgv_tabla.DataSourceID = this.ods_valores.ID;
        this.ods_valores.Select();
    }


    #endregion

    #region Manipulacion de grids

    #region Documentación
    /// <summary>
    /// Este metodo responde a un evento de finalizacion de Binding
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    #endregion
    protected void dgv_tabla_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnk_cargar = (LinkButton)e.Row.FindControl("lnk_cargar");
            CheckBox chk_cargar = (CheckBox)e.Row.FindControl("chk_cargar");

            if (lnk_cargar != null )
                lnk_cargar.Enabled = chk_cargar.Checked;

            if (e.Row.RowState.ToString().IndexOf("Edit", 0) >= 0)
            {
                LinkButton lnk_eliminar = (LinkButton)e.Row.FindControl("lnk_eliminar");
                CheckBox chk_base = (CheckBox)e.Row.FindControl("chk_base_ed");
                if (chk_base.Checked)
                    lnk_eliminar.Enabled = false;
            }
            else
            {
                LinkButton lnk_eliminar = (LinkButton)e.Row.FindControl("lnk_eliminar");
                CheckBox chk_base = (CheckBox)e.Row.FindControl("chk_base");
                if (chk_base.Checked)
                    lnk_eliminar.Enabled = false;
            }
        }

        int int_pagina = this.dgv_tabla.PageIndex + 1;
        this.lbl_numero_pagina.Text = int_pagina.ToString();
        this.lbl_numero_paginas.Text = this.dgv_tabla.PageCount.ToString();
    }

    #region Documentación
    /// <summary>
    /// Este metodo responde a un evento de finalizacion de Binding
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    #endregion
    protected void dgv_tabla_DataBound(object sender, EventArgs e)
    {
        DataView dv = (DataView)this.ods_valores.Select();
        this.lbl_total.Visible = true;
        this.lbl_total.Text = "Número total de registros : " + dv.Table.Rows.Count;

        this.lbl_pagina.Visible = (this.dgv_tabla.Rows.Count > 0);
        this.lbl_de.Visible = (this.dgv_tabla.Rows.Count > 0);
        this.lbl_numero_pagina.Visible = (this.dgv_tabla.Rows.Count > 0);
        this.lbl_numero_paginas.Visible = (this.dgv_tabla.Rows.Count > 0);
        this.lbl_total.Visible = (this.dgv_tabla.Rows.Count > 0);
        this.imb_ver_todos.Visible = (this.dgv_tabla.PageCount > 1);

        foreach (GridViewRow dgv_fila in dgv_tabla.Rows)
        {
            LinkButton lnk_eliminar = (LinkButton)dgv_fila.FindControl("lnk_eliminar");
            if( lnk_eliminar.Enabled )
                lnk_eliminar.Attributes.Add("onclick", "return confirmar('¿Está seguro de Eliminar este registro?');");

            //LinkButton lnk_cargar = (LinkButton)dgv_fila.FindControl("lnk_cargar");
            //lnk_cargar.Attributes.Add("onclick", "confirmar('¿Está seguro de realizar el cargue inicial?');");
        }
    }

    #endregion
    
    protected void dgv_tabla_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".dgv_tabla_RowDeleting.Inicio");
            System.Web.UI.WebControls.Label lblAUT_ID = (System.Web.UI.WebControls.Label)((System.Web.UI.WebControls.GridView)(sender)).Rows[e.RowIndex].Cells[1].FindControl("lblAUT_ID");
            if (lblAUT_ID != null)
            {
                String strLAUT_ID = lblAUT_ID.Text;
                try
                {
                    AutoridadAmbiental aa = new AutoridadAmbiental();
                    aa.Eliminar_EXT_Autoridad(int.Parse(strLAUT_ID));
                }
                catch (Exception ex)
                {

                }
            }
            //Page.RegisterStartupScript("onclick", "<SCRIPT LANGUAGE=\"JavaScript\">if (window.confirm('msg')){}else{event.returnValue = false;};</Script>");
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".dgv_tabla_RowDeleting.Finalizo");
        }
    }

    protected void ods_valores_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {

    }

    protected void dgv_tabla_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        if (e.Exception != null)
        {
            Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "msgBox", "<script>alert('No se puede eliminar el registro, ya que está siendo utilizado por uno o más registros.')</script>");
        }
        else {
            Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "msgBox", "<script>alert('Registro Eliminado correctamente')</script>");        
        }
        e.ExceptionHandled = true;
    }

    protected void lnk_cargar_Click(object sender, EventArgs e)
    {
        LinkButton lnk_cargar = (LinkButton)sender;
        TableCell cell = lnk_cargar.Parent as TableCell;
        Label lbl_id = (Label)cell.FindControl("lbl_id");
        //Se realiza el cargue inicial
        //this.oAutoridad.RealizarCargueInicial(Convert.ToInt32(lbl_id.Text));
        lnk_cargar.Enabled = false;
        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "<script>alert('Cargue inicial realizado con éxito.')</script>");
    }

    protected void dgv_tabla_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //TablaBasica_Entity oTempBasica = new TablaBasica_Entity();
        //bool existeBase = this.oTBasica.ExisteAutoridadBase(this.oConfig);
        //GridViewRow gdv = (GridViewRow)this.dgv_tabla.Rows[e.RowIndex];
        //CheckBox chk = (CheckBox)gdv.FindControl("chk_base_ed");

        //if (chk.Checked == true && existeBase == true)
        //if (chk.Checked == true )
        //{
        //    ClientScript.RegisterStartupScript(this.GetType(), "msgBox", "<script>alert(' Ya existe una autoriad ambiental base .')</script>");
        //    return;
        //}

        //this.oTBasica.NombreTabla = this.lbl_tablas_basicas.Text;
        //this.oTBasica.Nombre = e.OldValues[0].ToString();
        //if (e.OldValues[3] != null)
        //    this.oTBasica.Direccion = e.OldValues[3].ToString();
        //else
        //    this.oTBasica.Direccion = string.Empty;
        //if (e.OldValues[4] != null)
        //    this.oTBasica.Telefono = e.OldValues[4].ToString();
        //else
        //    this.oTBasica.Telefono = string.Empty;
        //if (e.OldValues[5] != null)
        //    this.oTBasica.Fax = e.OldValues[5].ToString();
        //else
        //    this.oTBasica.Fax = string.Empty;
        //if (e.OldValues[6] != null)
        //    this.oTBasica.Correo = e.OldValues[6].ToString();
        //else
        //    this.oTBasica.Correo = string.Empty;
        //if (e.OldValues[1] != null)
        //    this.oTBasica.Nit = e.OldValues[1].ToString();
        //else
        //    this.oTBasica.Nit = string.Empty;
        //this.oTBasica.Activo = Convert.ToBoolean(e.OldValues[2]);

        //oTempBasica.NombreTabla = this.lbl_tablas_basicas.Text;
        //oTempBasica.Nombre = e.NewValues[0].ToString();
        //oTempBasica.Direccion = e.NewValues[3].ToString();
        //oTempBasica.Telefono = e.NewValues[4].ToString();
        //oTempBasica.Fax = e.NewValues[5].ToString();
        //oTempBasica.Correo = e.NewValues[6].ToString();
        //oTempBasica.Nit = e.NewValues[1].ToString();
        //oTempBasica.Activo = Convert.ToBoolean(e.OldValues[2]);

        //this.oBita.adicionarAccion_mc(Accion.Modificar, Convert.ToInt32(Modulo.Tablas_Basicas), this.oTBasica.NombreTabla, "", this.oTBasica, oTempBasica);
    }

    protected void dgv_tabla_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        if (e.Exception != null)
        {
            Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "msgBox", "<script>alert('No se puede actualizar el registro, recuerde que no pueden existir dos autoridades ambientales marcadas como base.')</script>");
        }
        e.ExceptionHandled = true;
    }
}
