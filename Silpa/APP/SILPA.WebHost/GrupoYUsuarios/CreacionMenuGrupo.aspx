<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPALimpia.master" AutoEventWireup="true" CodeFile="CreacionMenuGrupo.aspx.cs" Inherits="GrupoYUsuarios_CreacionMenuGrupo" Title="Grupos y Menu"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<div class="div-titulo">
        <asp:Label ID="lblTituloPrincipal" runat="server" SkinID="titulo_principal_blanco"
            Text="GRUPO Y MENU"></asp:Label>
    </div>
    <div class="div-contenido">
    <asp:ScriptManager id="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table width="60%">
        <tr style="display:none;">
            <td style="width: 15%">
                <asp:Label ID="lblMenus" runat="server" Text="Menus:"></asp:Label></td>
            <td style="width: 30%" align="left">
                <asp:DropDownList ID="cboMenus" runat="server" Width="174px">
                </asp:DropDownList></td>
            <td rowspan="2">
                <asp:ListBox ID="lstMenus" runat="server" Width="350px" SelectionMode="Multiple"></asp:ListBox></td>
        </tr>
        <tr style="display:none;">
            <td align="center" colspan="2" style="height: 37px">
                <asp:Button ID="btnAdicionarM" runat="server" Text=" >> " OnClick="btnAdicionarM_Click" /><asp:Button ID="btnQuitarM" runat="server" Text=" << " CausesValidation="False" OnClick="btnQuitarM_Click" /></td>
        </tr>
        <tr>
            <td><span class="Label">Nombre Menú</span></td>
            <td colspan="2">
                <asp:TextBox ID="txtNombreMenu" runat="server" Text="" style="width:50%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 15%; height: 21px;" align="left">
                <asp:Label ID="lblGrupos" runat="server" Text="Grupos:"></asp:Label></td>
            <td style="width: 30%" align="left">
                <asp:DropDownList ID="cboGrupos" runat="server" Width="174px">
                </asp:DropDownList>
            </td>
            <td align="center" rowspan="2">
                <asp:ListBox ID="lstGrupos" runat="server" Width="350px" SelectionMode="Multiple"></asp:ListBox></td>
        </tr>
        <tr>
            <td align="center" colspan="2"><asp:Button ID="btnAdicionarG" runat="server" Text=" >> " OnClick="btnAdicionarG_Click" /><asp:Button ID="btnQuitarG" runat="server" Text=" << " CausesValidation="False" OnClick="btnQuitarG_Click" /></td>
        </tr>
        <tr>
            <td align="center" colspan="3">
                <asp:Button ID="btnAceptar" runat="server" Text="Crear Menú" OnClick="btnAceptar_Click" /></td>
        </tr>
    </table>
    <br />
    <table width="60%">
        <tr>
            <td style="width=5%">
                <asp:Label ID="lblId" runat="server" Visible="False"></asp:Label></td>           
            <td style="width=5%">
            </td>
        </tr>
    </table>  
</div>
</asp:Content>


