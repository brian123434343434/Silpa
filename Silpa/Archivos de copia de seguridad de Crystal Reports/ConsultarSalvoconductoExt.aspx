<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConsultarSalvoconductoExt.aspx.cs" Inherits="Salvoconducto_ConsultarSalvoconductoExt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Consultar Salvoconducto</title>
</head>
<body>
    <form id="form1" runat="server" dir="ltr" style="background-color: white;">
        <div>
            <table>
                <tbody>
                    <tr>
                        <td c>
                            <asp:Label ID="lblNumeroSalvoConducto" runat="server" SkinID="etiqueta_negra" Text="Número de Salvoconducto:" meta:resourcekey="lblNumeroSalvoConductoResource1"></asp:Label>
                        </td>
                        <td align="center">
                            <asp:TextBox ID="txtNumeroSalvoconducto" runat="server" SkinID="texto" meta:resourcekey="txtNumeroSalvoconductoResource1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <asp:Button ID="btnConsultar" OnClick="btnConsultar_Click" runat="server" SkinID="boton_copia" Text="Buscar" meta:resourcekey="btnConsultarResource1"></asp:Button>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblNumeroObligatorio" runat="server" Text="El número de salvoconducto es obligatorio" Visible="False" ForeColor="Red" meta:resourcekey="lblNumeroObligatorioResource1"></asp:Label>&nbsp;</td>
                    </tr>
                </tbody>
            </table>
            <table width='100%'>
                <tr>
                    <td>
                        <asp:GridView Style="text-align: center" ID="grdSalvoconductos" runat="server" SkinID="Grilla_simple_peq" Width="400px" AutoGenerateColumns="False" meta:resourcekey="grdSalvoconductosResource1" OnRowDataBound="grdSalvoconductos_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Solicitudes" Visible="true">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkSol" runat="server" Text="" CommandArgument='<%#((GridViewRow)Container).RowIndex %>' OnCommand="lbDocumentos_Click" CssClass="a_green"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView Style="text-align: Left" ID="grdDetalleSalvoconductos" runat="server" SkinID="Grilla_simple_peq" AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="VALORCAMPO" HeaderText="Campo" meta:resourceKey="BoundFieldResource1"></asp:BoundField>
                                <asp:BoundField DataField="VALOR" HeaderText="Valor" meta:resourceKey="BoundFieldResource2"></asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
