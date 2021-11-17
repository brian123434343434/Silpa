<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctrl_lista_combo.ascx.cs" Inherits="controles_ctrl_lista_combo" %>
<link href="../App_Themes/skin/estilos.css" rel="stylesheet" type="text/css">
<asp:RequiredFieldValidator id="rfv_val_items_count" runat="server" ErrorMessage="El campo es requerido" Text="*" ControlToValidate="txt_items" Display="Dynamic">*</asp:RequiredFieldValidator>	
<table border="0" cellpadding="0" cellspacing="1">
  <tr>
    <td>
      <asp:DropDownList CssClass="texto_usuario" ID="lst_combo" runat="server" Width="400px">
        <asp:ListItem></asp:ListItem>
      </asp:DropDownList></td>
  </tr>
  <tr>
    <td><table border="0" cellpadding="0" cellspacing="1">
      <tr>
        <td><asp:ImageButton ID="cmi_adicionar" AlternateText="Adicionar" ImageUrl="../App_Themes/Img/b1_mas.gif" ToolTip="Adicionar" runat="server" OnClick="cmi_adicionar_Click" CausesValidation="False" /></td>
        <td><asp:ImageButton ID="cmi_retirar" AlternateText="Retirar" ImageUrl="../App_Themes/Img/b1_menos.gif" ToolTip="Retirar" runat="server" OnClick="cmi_retirar_Click" CausesValidation="False" /></td>
        <td><asp:ImageButton ID="cmi_subir" AlternateText="Subir" ImageUrl="../App_Themes/Img/b1_subir.gif" ToolTip="Subir" runat="server" OnClick="cmi_subir_Click" CausesValidation="False" /></td>
        <td style="width: 22px"><asp:ImageButton ID="cmi_bajar" AlternateText="Bajar" ImageUrl="../App_Themes/Img/b1_bajar.gif" ToolTip="Bajar" runat="server" OnClick="cmi_bajar_Click" CausesValidation="False" /></td>
      </tr>
    </table>      </td>
  </tr>
  <tr>
    <td><asp:ListBox CssClass="campos" ID="lbx_items" runat="server" OnDataBound="lbx_items_DataBound"></asp:ListBox></td>
  </tr>
</table>
<span style="display:none">
<asp:TextBox ID="txt_items" Text="" runat="server"></asp:TextBox>
</span>
<asp:RequiredFieldValidator id="rfv_val_items" runat="server" ErrorMessage="El Campo es requerido" ControlToValidate="lbx_items"
	Display="None" InitialValue="-1"></asp:RequiredFieldValidator>&nbsp;	