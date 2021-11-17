<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConsultarSalvoconductoExt.aspx.cs" Inherits="Salvoconducto_ConsultarSalvoconductoExt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Consultar Salvoconducto</title>
</head>
<body>
    <form id="form1" runat="server" dir="ltr" style="background-color: white;">
    <div>
    <TABLE >
        <TBODY>
        <TR><TD c><asp:Label id="lblNumeroSalvoConducto" runat="server"  Text="Número de Salvoconducto:" meta:resourcekey="lblNumeroSalvoConductoResource1" Font-Names="Tahoma"></asp:Label> </TD>
        <TD align="center"><asp:TextBox id="txtNumeroSalvoconducto" runat="server"  meta:resourcekey="txtNumeroSalvoconductoResource1"></asp:TextBox> </TD></TR>
        <TR><TD align="center" colSpan=2><asp:Button id="btnConsultar" onclick="btnConsultar_Click" runat="server"  Text="Buscar" meta:resourcekey="btnConsultarResource1" Font-Names="Tahoma"></asp:Button> </TD></TR>
        <TR><TD colSpan=2><asp:Label id="lblNumeroObligatorio" runat="server" Text="El número de salvoconducto es obligatorio" Visible="False" ForeColor="Red" meta:resourcekey="lblNumeroObligatorioResource1" Font-Names="Tahoma"></asp:Label>&nbsp;</TD></TR></TBODY>
    </TABLE>
    <table width='100%'>
    <tr>
    <td>
    <asp:GridView style="TEXT-ALIGN: center" id="grdSalvoconductos" runat="server"  Width="400px" AutoGenerateColumns="False" meta:resourcekey="grdSalvoconductosResource1" OnRowDataBound="grdSalvoconductos_RowDataBound" Font-Names="Tahoma" 
    ><Columns>                
                <asp:TemplateField HeaderText="Solicitudes">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkSol" runat="server" Text="" CommandArgument='<%#((GridViewRow)Container).RowIndex %>' OnCommand="lbDocumentos_Click"></asp:LinkButton>                    
                </ItemTemplate>                
                </asp:TemplateField>                
            </Columns>
            </asp:GridView>
    </td>
    </tr>
    <tr><td>
        <asp:GridView style="TEXT-ALIGN: Left" id="grdDetalleSalvoconductos" runat="server" AutoGenerateColumns="False" Font-Names="Tahoma">
            <Columns>
                <asp:BoundField DataField="VALORCAMPO" ShowHeader="false" ></asp:BoundField>
                <asp:BoundField DataField="VALOR" ShowHeader="false"></asp:BoundField>
            </Columns>
        </asp:GridView>
        </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
