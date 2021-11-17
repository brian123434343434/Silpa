<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BasicData.ascx.cs" Inherits="BasicData" %>
<table style="width: 627px">
    <tr>
        <td style="height: 26px">
        </td>
        <td style="width: 509px; height: 26px">
        </td>
        <td style="width: 368px; height: 26px">
        </td>
        <td style="height: 26px; width: 92px;">
        </td>
        <td style="width: 2529px; height: 26px">
        </td>
    </tr>
    <tr>
        <td style="height: 26px">
            <asp:Label ID="LBL_TITULO" runat="server" Font-Bold="True" Text="Titulo" Width="129px"></asp:Label></td>
        <td style="width: 509px; height: 26px">
        </td>
        <td style="width: 368px; height: 26px">
            <asp:TextBox ID="TXT_VALOR_TITULO" runat="server" Width="137px"></asp:TextBox></td>
        <td style="height: 26px; width: 92px;">
        </td>
        <td style="width: 2529px; height: 26px">
        
        </td>
    </tr>
    <tr>
        <td style="height: 7px">
            <asp:RadioButtonList ID="RDG_LST_TIPO_DOCUMENTO" runat="server" RepeatDirection="Horizontal"
                Width="128px">
                <asp:ListItem Value="1" Selected="True">CC</asp:ListItem>
                <asp:ListItem Value="2">NIT</asp:ListItem>
            </asp:RadioButtonList></td>
        <td style="width: 509px; height: 38px; text-align: right;">
            <asp:Label ID="LBL_NUMERO" runat="server" Text="No"></asp:Label></td>
        <td style="width: 368px; height: 38px">
            <div>
                <asp:TextBox ID="TXT_NUMERO_DOCUMENTO" runat="server" Width="137px">88123456</asp:TextBox></div>
        </td>
        <td style="width: 92px; height: 38px">
            <div style="text-align: right">
                <asp:Label ID="LBL_DE" runat="server" Text="de"></asp:Label>&nbsp;</div>
        </td>
        <td style="width: 2529px; height: 38px">
            <div>
                <asp:TextBox ID="TXT_LUGAR_EXPEDICION" runat="server" Width="137px">Bogota</asp:TextBox></div>
        </td>
    </tr>
    <tr>
        <td colspan="1" style="height: 15px">
            &nbsp;<div>
                &nbsp;</div>
        </td>
        <td colspan="1" style="width: 509px; height: 15px; text-align: right;">
                <asp:Label ID="LBL_DIRECCION" runat="server" Text="Dirección" Width="55px"></asp:Label></td>
        <td colspan="1" style="width: 368px; height: 15px">
            <div>
                <asp:TextBox ID="TXT_DIRECCION" runat="server" Width="137px">Cra. 7 # 6-66</asp:TextBox></div>
        </td>
        <td colspan="1" style="width: 92px; height: 15px; text-align: right;">
            <asp:Label ID="LBL_CIUDAD" runat="server" Text="Ciudad" Width="35px" Height="20px"></asp:Label></td>
        <td colspan="1" style="width: 2529px; height: 15px">
            <div>
                <asp:TextBox ID="TXT_CIUDAD" runat="server" Width="137px">Bogota</asp:TextBox></div>
        </td>
    </tr>
    <tr>
        <td style="height: 21px">
            <div id="Div1">
                &nbsp;</div>
        </td>
        <td style="width: 509px; height: 21px; text-align: right;">
                <asp:Label ID="LBL_TELEFONO" runat="server" Text="Telefono" Width="45px"></asp:Label></td>
        <td style="width: 368px; height: 21px">
            <div>
                <div>
                    <asp:TextBox ID="TXT_TELEFONO" runat="server" Width="137px">523569</asp:TextBox></div>
            </div>
        </td>
        <td style="width: 92px; height: 21px">
            <div style="text-align: right">
                <asp:Label ID="LBL_FAX" runat="server" Text="Fax" Width="29px"></asp:Label></div>
        </td>
        <td style="width: 2529px; height: 21px">
            <div>
                <asp:TextBox ID="TXT_FAX" runat="server" Width="137px"></asp:TextBox></div>
        </td>
    </tr>
    <tr>
        <td style="height: 26px">
            <div>
                &nbsp;</div>
        </td>
        <td style="width: 509px; height: 26px; text-align: right;">
                <asp:Label ID="LBL_EMAIL" runat="server" Text="E-mail" Width="44px"></asp:Label></td>
        <td style="width: 368px; height: 26px">
            <div>
                <asp:TextBox ID="TXT_EMAIL" runat="server" Width="137px">email@dominio.com</asp:TextBox></div>
        </td>
        <td style="width: 92px; height: 26px">
            <div>
                &nbsp;</div>
        </td>
        <td style="width: 2529px; height: 26px">
            <div>
                &nbsp;</div>
        </td>
    </tr>
    <tr>
        <td colspan="5" style="height: 3px">
            <div>
                <hr />
                </div>
        </td>
    </tr>
</table>
