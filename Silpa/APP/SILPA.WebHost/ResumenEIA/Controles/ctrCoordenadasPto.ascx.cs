using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Control para ingreso de coordenadas puntuales.
/// Es necesario en la acción del botón que toma los datos de coordenadas del control
/// utilizar el siguiente código para "disparar" la validación
/// if (nombreControl.Valido)
/// {
///     CoordenadaEste = nombreControl.CoorEste.ToString();
///     CoordenadaNorte = nombreControl.CoorNorte.ToString();
/// }
/// </summary>
public partial class ResumenEIA_Controles_ctrCoordenadasPto : System.Web.UI.UserControl
{

    public bool Valido
    {
        get
        {
            string nombreControl = this.ID.ToString();
            Page.Validate("ValPto" + nombreControl);
            if (Page.IsValid)
                return true;
            else
                return false;
        }
        set { }
    }
    public string CoorNorte
    {
        get
        {
            return txtCoorNorte.Text;
        }
        set
        {
            txtCoorNorte.Text=value;
        }
    }
    public string CoorEste
    {
        get
        {
            return txtCoorEste.Text;
        }
        set
        {
            txtCoorEste.Text = value;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            cambValidationGroup();
        }
    }

    protected void cambValidationGroup()
    {
        string nombreControl = this.ID.ToString();
        rfvCoorEstePto.ValidationGroup = "ValPto" + nombreControl;
        rfvCoorEstePto.ValidationGroup = "ValPto" + nombreControl;
        vasCoordPto.ValidationGroup = "ValPto" + nombreControl;
    }
}
