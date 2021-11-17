<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctrl_lista_ruta_salvo.ascx.cs" Inherits="controles_ctrl_lista_ruta_salvo" %>
<link href="../estilos.css" rel="stylesheet" type="text/css"/>
<table border="0" cellpadding="0" cellspacing="1">
  <tr>
  <td align="right" valign="top" class="texto_usuario">Departamento:</td>
    <td><asp:DropDownList CssClass="texto_usuario" ID="ddl_departamento" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_departamento_SelectedIndexChanged">
    </asp:DropDownList></td>
  </tr>
  <tr>
  <td  align="right" valign="top" class="texto_usuario">Municipio:</td>
    <td>
      <asp:DropDownList CssClass="texto_usuario" ID="ddl_municipio" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_municipio_SelectedIndexChanged">
      </asp:DropDownList>
    </td>
  </tr>
    <tr>
  <td  align="right" valign="top" class="texto_usuario">Corregimiento:</td>
    <td>
      <asp:DropDownList CssClass="texto_usuario" ID="ddl_corregimiento" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_corregimiento_SelectedIndexChanged">
      </asp:DropDownList>
    </td>
  </tr>
  <tr>

<td  align="right" valign="top" class="texto_usuario" style="height: 19px">Vereda:</td>
    <td style="height: 19px">
      <asp:DropDownList CssClass="texto_usuario" ID="ddl_vereda" runat="server" >
      </asp:DropDownList>

    </td>
  </tr>
    <tr runat="server" id="trAreaH">
<td  align="right" valign="top" class="texto_usuario" style="height: 19px">
    Área Hidrográfica:</td>
    <td style="height: 19px">

      <asp:DropDownList CssClass="texto_usuario" ID="ddl_area" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_area_SelectedIndexChanged" >
      </asp:DropDownList>

    </td>
  </tr>
    <tr runat="server" id="trZona">
        <td align="right" class="texto_usuario" valign="top">
            Zona Hidrográfica:</td>
        <td>
            <asp:DropDownList CssClass="texto_usuario" ID="ddl_zona" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_zona_SelectedIndexChanged" >
            </asp:DropDownList></td>
    </tr>
    <tr runat="server" id="trSubzona">
        <td align="right" class="texto_usuario" valign="top">
            Subzona Hidrológica:</td>
        <td>
            <asp:DropDownList CssClass="texto_usuario" ID="ddl_subzona" runat="server" >
            </asp:DropDownList></td>
    </tr>
  <tr>
  <td></td>
    <td><table border="0" cellpadding="0" cellspacing="1">
      <tr>
        <td><asp:ImageButton ID="btn_adicionarMun" AlternateText="Adicionar" ImageUrl="../images/b1_mas.gif" ToolTip="Adicionar" runat="server" OnClick="cmi_adicionar_Click" /></td>
        <td><asp:ImageButton ID="btn_retirarMun" AlternateText="Retirar" ImageUrl="../images/b1_menos.gif" ToolTip="Retirar" runat="server" OnClick="cmi_retirar_Click" /></td>
        <td><asp:ImageButton ID="btn_subirMun" AlternateText="Subir" ImageUrl="../images/b1_subir.gif" ToolTip="Subir" runat="server" OnClick="cmi_subir_Click" /></td>
        <td><asp:ImageButton ID="btn_bajarMun" AlternateText="Bajar" ImageUrl="../images/b1_bajar.gif" ToolTip="Bajar" runat="server" OnClick="cmi_bajar_Click" /></td>
      </tr>
    </table><asp:ListBox CssClass="campos" ID="lst_mun" runat="server" SelectionMode="multiple" TabIndex="12"></asp:ListBox></td>
  </tr>
</table>
<asp:RequiredFieldValidator id="rfv_val_items" runat="server" ErrorMessage="El campo es requerido" ControlToValidate="lst_mun"
	Display="None" InitialValue="-1"></asp:RequiredFieldValidator>

