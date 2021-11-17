<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctrProyectos.ascx.cs" Inherits="ResumenEIA_Controles_ctrProyectos" %>
<table style="width:100%;">
    <tr>
        <td colspan="4" class="titleUpdate">
            </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Label ID="lblProgramaManejo" runat="server"></asp:Label>
            </td>
    </tr>
    <tr>
        <td width="30%">
            <asp:Label ID="lblNombreProyecto" runat="server" Text="Nombre Proyecto" 
                Width ="100%" SkinID="etiqueta_negra"></asp:Label>
        </td>
        <td colspan="3">
            <asp:TextBox ID="txtNombreProyecto" runat="server" Width="100%" 
                SkinID="texto_sintamano"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblEtapaAplicacion" runat="server" Text="Etapa de aplicacion" 
                SkinID="etiqueta_negra"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="cboEtapaAplicacion" runat="server" Width ="30%" 
                SkinID="lista_desplegable">
            </asp:DropDownList>
        </td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblTipoMedida" runat="server" Text="Tipo de medida" 
                SkinID="etiqueta_negra"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="cboTipoMedida" runat="server" Width ="30%" 
                SkinID="lista_desplegable">
            </asp:DropDownList>
        </td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="4" class="titleUpdate" >
            </td>
    </tr>
    <tr>
        <td colspan="4">
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="4" class="titleUpdate">
            </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label3" runat="server" Text="Item" SkinID="etiqueta_negra"></asp:Label>
        </td>
        <td>
            <asp:Label ID="Label4" runat="server" Text="Descripcion" 
                SkinID="etiqueta_negra"></asp:Label>
        </td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="4" class="titleUpdate">
            </td>
    </tr>
    <tr>
        <td class="style1">
            <asp:Label ID="lblObjetivos" runat="server" Text="Objetivos" 
                SkinID="etiqueta_negra"></asp:Label>
        </td>
        <td class="style1" colspan="3">
            <asp:TextBox ID="txtObjetivos" runat="server" Width="100%" 
                SkinID="texto_sintamano"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblMetas" runat="server" Text="Metas" SkinID="etiqueta_negra"></asp:Label>
        </td>
        <td colspan="3">
            <asp:TextBox ID="txtMetas" runat="server" Width="100%" 
                SkinID="texto_sintamano"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblImpactoManejar" runat="server" Text="Impactos a manejar" 
                SkinID="etiqueta_negra"></asp:Label>
        </td>
        <td colspan="3">
            <asp:TextBox ID="txtImpactoManejar" runat="server" Width="100%" 
                SkinID="texto_sintamano"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblIndicadores" runat="server" 
                Text="Indicadores de seguimiento y monitoreo" SkinID="etiqueta_negra"></asp:Label>
        </td>
        <td colspan="3">
            <asp:TextBox ID="txtIndicadores" runat="server" Width="100%" 
                SkinID="texto_sintamano"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td colspan="2" width="50%" style="text-align: center">
            &nbsp;</td>
        <td colspan="2" width="50%" style="text-align: center">
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="4" class= "titleUpdate">
            &nbsp;</td>
    </tr>
</table>