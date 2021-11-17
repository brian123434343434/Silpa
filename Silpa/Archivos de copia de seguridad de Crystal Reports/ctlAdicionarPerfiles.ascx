<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctlAdicionarPerfiles.ascx.cs" Inherits="controles_ctlAdicionarPerfiles" %>
<table id="tabla_perfiles">
    <tr>
        <td>
        
            <asp:ListBox ID="lbx_lista" runat="server" Width="150px" Height="150px" Font-Names="Tahoma" Font-Size="12.5px">
                <asp:ListItem Value="0">Funcionarios AA</asp:ListItem>
                <asp:ListItem Value="1">Solicitantes</asp:ListItem>
                <asp:ListItem Value="2">Coordinadores</asp:ListItem>
                <asp:ListItem Value="3">Público</asp:ListItem>
                <asp:ListItem>Todos</asp:ListItem>
            </asp:ListBox>
        </td>
        <td align="center" valign="middle">
            <asp:Button ID="btn_adicionar" runat="server" Text="Adicionar >" SkinID="boton" 
                onclick="btn_adicionar_Click" />
            <br />
            <asp:Button ID="btn_quitar" runat="server" Text="< Quitar" SkinID="boton" 
                onclick="btn_quitar_Click" />
        </td>
        <td>
            <asp:ListBox ID="lbx_seleccion" runat="server" Width="150px" Height="150px"></asp:ListBox>
        </td>
    </tr>
        

</table>