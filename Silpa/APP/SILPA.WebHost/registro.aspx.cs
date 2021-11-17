using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class registro : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        remove_tab_border();
        //DATOS DE PERSONA NATURAL
        txt_clave.Text = "123456";
        txt_clave_confirmar.Text = "123456";
        txt_numero_identificacion.Text = "18703404";
		txt_nombre_usuario.Text = "18703404";
        txt_direccion_natural.Text = "Cra. 48 No. 30 - 20";
        txt_correspondecia_natural.Text = "Cra. 48 No. 30 - 20";
        txt_telefono_natural.Text = "4135067";
        txt_celular_natural.Text = "3405320320";
        txt_correo1_natural.Text = "jcarlos349@gmail.com";
        txt_primer_nombre_natural.Text = "Juan";
        txt_segundo_nombre_natural.Text = "Carlos";
        txt_primer_apellido_natural.Text = "Méndez";
        txt_segundo_apellido_natural.Text = "Rodríguez";

        //DATOS DEL APODERADO
        txt_primer_nombre_apoderado.Text = "Luisa";
        txt_segundo_nombre_apoderado.Text = "María";
        txt_primer_apellido_apoderado.Text = "Ordoñez";
        txt_segundo_apellido_apoderado.Text = "Zuluaga";
        txt_numero_identificacion_apoderado.Text = "51740313";
        txt_tarjeta_profesional_apoderado.Text = "99026CSJ";
        txt_telefono_apoderado.Text = "2538774";
        txt_celular_apoderado.Text = "3505257001";
        txt_correo1_apoderado.Text = "lordones2z@gmail.com";


        //tab_container.Tabs[1].Visible = false;
        tab_container.Tabs[2].Visible = false;
        //tab_container.Tabs[1].HeaderText = "";
        tab_container.Tabs[2].HeaderText = "";
        
    }

    private void remove_tab_border()
    {
        HtmlGenericControl style = new HtmlGenericControl("style");

        style.Attributes.Add("type", "text/css");
        style.ID = "TabControl Style";

        //style.InnerText = ".ajax__tab_header { visibility: hidden; height: 0px }" + Environment.NewLine;
        style.InnerText += "#" + tab_container.ClientID + "_body { border: 0px; padding:0px }";

        this.Page.Header.Controls.Add(style);

       // tab_container.Tabs.Remove(tab_container.Tabs[1]); //Loop if more than one tab to be removed
    }
    protected void rbl_tipo_usuario_SelectedIndexChanged(object sender, EventArgs e)
    {
        ////Persona Jurídica
        //if (rbl_tipo_usuario.SelectedItem.Value == "1")
        //{
        //    tab_container.Tabs[1].Visible = false;
        //    tab_container.Tabs[1].HeaderText = "";
        //    tab_container.Tabs[2].HeaderText = "Datos Persona Jurídica";
        //    tab_container.Tabs[2].Visible = true;
            
        //}
        ////Persona Natural
        //if (rbl_tipo_usuario.SelectedItem.Value == "2")
        //{
        //    tab_container.Tabs[1].Visible = true;
        //    tab_container.Tabs[2].Visible = false;
        //    tab_container.Tabs[2].HeaderText = "";
        //    tab_container.Tabs[1].HeaderText = "Datos Persona Natural";
        //}
    }
    protected void btn_guardar_Click(object sender, EventArgs e)
    {
        tab_container.Visible = false;
        btn_guardar.Visible = false;
        lbl_resultado.Text = "Sus datos han sido guardados exitosamente, recibirá una confirmación " +
		"por correo cuando el Administrador del sistema habilite su usuario";
        
        lbl_resultado2.Text = lbl_advertencia.Text;
        lbl_resultado3.Text = lbl_advertencia2.Text;
        lbl_resultado4.Text = lbl_advertencia3.Text;
        lbl_resultado2.Visible = true;
        lbl_resultado3.Visible = true;
        lbl_resultado4.Visible = true;

        GenerarLogAuditoria CrearLogAuditoria = new GenerarLogAuditoria();
        CrearLogAuditoria.Insertar("SILPA", 1, "Se almaceno Formulario de Registro");

    }

    protected void rbl_tipo_persona_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Persona Jurídica
        if (rbl_tipo_persona.SelectedItem.Value == "juridica")
        {
            tab_container.Tabs[1].Visible = false;
            tab_container.Tabs[1].HeaderText = "";
            tab_container.Tabs[2].HeaderText = "Datos Persona Jurídica";
            tab_container.Tabs[2].Visible = true;

        }
        //Persona Natural
        if (rbl_tipo_persona.SelectedItem.Value == "natural")
        {
            tab_container.Tabs[1].Visible = true;
            tab_container.Tabs[2].Visible = false;
            tab_container.Tabs[2].HeaderText = "";
            tab_container.Tabs[1].HeaderText = "Datos Persona Natural";
        }
    }
}
