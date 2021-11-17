<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="Autoliquidar_Licencia_II.aspx.cs" Inherits="LicenciasAmbientales_Prueba" Title="Untitled Page" enableEventValidation="false"%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<form id="form1">
    <div>
        
        <table>
            <tr>
                <td colspan="3">
                    <asp:Label ID="LBL_SOL_LIC" runat="server" Text="SOLICITUD DE LICENCIAS"></asp:Label></td>
            </tr>
            <tr>
                <td>
        <asp:Label ID="LBL_LIQUIDACION" runat="server" Text="LIQUIDACIÓN:"></asp:Label></td>
                <td>
                </td>
                <td>
        <asp:TextBox ID="TXT_NUM_LIQ" runat="server" Enabled="False">No. 01328</asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 200px">
        <asp:Label ID="LBL_FECHA_CREACION" runat="server" Text="Fecha de Creación del Recibo:"></asp:Label></td>
                <td style="width: 633px">
        <asp:TextBox ID="TXT_FECHA" runat="server" Enabled="False" Width="198px">18/Diciembre/2008 - Bogota;</asp:TextBox></td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
        <asp:Label ID="LBL_NUM_SILPA" runat="server" Text="Número SILPA:"></asp:Label></td>
                <td>
        <asp:TextBox ID="TXT_NUM_SILPA" runat="server" Enabled="False" Width="300px">SILPA-0109</asp:TextBox></td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
        <asp:Label ID="LBL_NOMBRE_PROY" runat="server" Text="Nombre del Proyecto, Obra o Actividad:"></asp:Label></td>
                <td>
        <asp:TextBox ID="TXT_NOMBRE_PROY" runat="server" Enabled="False" Width="297px">Ampliaci&#243;n V&#237;a Cali - Pereira</asp:TextBox></td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
        <asp:Label ID="LBL_NUM_EXP" runat="server" Text="Número del Expediente:"></asp:Label></td>
                <td>
        <asp:TextBox ID="TXT_NUM_EXP" runat="server" Enabled="False" Width="250px">46545-46546</asp:TextBox></td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 200px; height: 26px">
        <asp:Label ID="LBL_NOM_SOLICITANTE" runat="server" Text="Nombre:"></asp:Label></td>
                <td>
        <asp:TextBox ID="TXT_NOMBRE_SOLICITANTE" runat="server" Enabled="False" Width="346px">Enrique Martinez</asp:TextBox></td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
        <asp:Label ID="LBL_TIPO_DOC" runat="server" Text="Tipo de Documento:"></asp:Label></td>
                <td>
        <asp:TextBox ID="TXT_TIPO_DOC" runat="server" Enabled="False" Width="25px">CC</asp:TextBox>&nbsp;&nbsp;&nbsp;</td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
        <asp:Label ID="LBL_NUM_DOC" runat="server" Text="Número:"></asp:Label></td>
                <td>
        <asp:TextBox ID="TXT_NUM_DOC" runat="server" Enabled="False">1234567</asp:TextBox></td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <asp:Label ID="LBL_LUGAR_DOC" runat="server" Text="De:"></asp:Label></td>
                <td>
                    <asp:TextBox ID="TXT_LUGAR_DOC" runat="server" Enabled="False">Bogota</asp:TextBox></td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
        <asp:Label ID="LBL_TIPO_SOLICITUD" runat="server" Text="Solicitud:"></asp:Label></td>
                <td>
        <asp:TextBox ID="TXT_TIPO_SOLICITUD" runat="server" Enabled="False" Font-Bold="True">LICENCIA AMBIENTAL</asp:TextBox></td>
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="3">
        <asp:Label ID="LBL_TITULO_LIQ" runat="server" Text="Liquidación:"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="3">
        <div>
        <table border="1" cellpadding="1">
            <tr>
                <td>
                    1 - LICENCIA AMBIENTAL</td>
                <td colspan="2">
                    $ -------------------</td>
            </tr>
            <tr>
                <td>
                    Total (Noventa y Nueve Mil Novecientos Noventa y Nueve)</td>
                <td colspan="2">
                    $ 99,999</td>
            </tr>
            <tr>
                <td>
                </td>
                <td colspan="2">

                <%-- 
                <img src="../images/CodigoBarras.gif" style="width: 174px; height: 89px" /></td>
                --%>
                    

                    <img src="../../App_Themes/img/CodigoBarras.gif" style="width: 174px; height: 89px" /></td>

            </tr>
        </table></div>
                </td>
            </tr>
            <tr>
                <td colspan="1">
        <asp:Button ID="Button1" runat="server" Text="PAGAR EN LÍNEA" OnClientClick="alert('Ejecuta el CU-GN-05-Pagar Electrónicamente');" /></td>
                <td colspan="1">
        <asp:Label ID="Label12" runat="server" Text="ó puede hacer su pago en los siguientes bancos y podrá continuar cuando registremos su pago"></asp:Label></td>
                <td colspan="3">
                    <asp:Button
            ID="Button2" runat="server" Text="IMPRIMIR RECIBO" OnClientClick="window.print();alert('Ejecuta CU-ARC-03-Imprimir Documento');" /></td>
            </tr>
        </table>
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp; &nbsp;&nbsp;<br />
        <br />
        <br />
        <br />
        &nbsp;</div>
    </form>

</asp:Content>

