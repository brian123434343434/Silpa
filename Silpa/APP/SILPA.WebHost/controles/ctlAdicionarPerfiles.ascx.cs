using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class controles_ctlAdicionarPerfiles : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_adicionar_Click(object sender, EventArgs e)
    {
        if (lbx_lista.SelectedIndex > -1)
        {
            ListItem li = new ListItem(lbx_lista.SelectedItem.Text, lbx_lista.SelectedItem.Value);
            lbx_seleccion.Items.Add(li);
            lbx_lista.Items.Remove(li);
        }
        
    }
    protected void btn_quitar_Click(object sender, EventArgs e)
    {
        if (lbx_seleccion.SelectedIndex > -1)
        {
            ListItem li = new ListItem(lbx_seleccion.SelectedItem.Text,lbx_seleccion.SelectedValue);
            lbx_lista.Items.Add(li);
            lbx_seleccion.Items.Remove(lbx_seleccion.SelectedItem);
        }
    }
}
