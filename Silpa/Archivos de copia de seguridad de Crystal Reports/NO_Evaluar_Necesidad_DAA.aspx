<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NO_Evaluar_Necesidad_DAA.aspx.cs" Inherits="LicenciasAmbientales_LPA2_02_NO_Evaluar_Necesidad_DAA" MasterPageFile="~/plantillas/SILPA.master" enableEventValidation="false"%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


    <form id="form1">
    <div>
        
        <table style="width: 1366px">
            <tr>
                <td colspan="3">
                    <asp:Label ID="Label1" runat="server" Text="SOLICITUD DE LICENCIAS"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="4" style="height: 113px">
        <asp:Label ID="Label2" runat="server" Text="CONSULTAR REQUERIMIENTO DE DIAGNOSTICO AMBIENTAL DE ALTERNATIVAS"></asp:Label>
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
        <asp:Label ID="Label3" runat="server" Text="LPA-LIC-002a"></asp:Label></td>
            </tr>
            <tr>
                <td style="height: 169px" colspan="3">
                    <ul>
                        <li>
                <asp:Label ID="Label4" runat="server" Text="La Autoridad Ambiental competente ante la que realizará su trámite es:"></asp:Label>&nbsp;
            <asp:Label ID="Label5" runat="server" Text="MINISTERIO AMBIENTE, VIVIENDA Y DESARROLLO TERRITORIAL."></asp:Label></li>
                    </ul>
                    <p>
                        &nbsp;</p>
                    <ul>
                        <li>
                <asp:Label ID="Label6" runat="server" Text="Este proyecto NO requiere DIAGNOSTICO AMBIENTAL DE ALTERNATIVAS."></asp:Label></li>
                    </ul>
                </td>
            </tr>
            <tr>
                <td colspan="3" style="height: 167px">
            <asp:Label ID="Label7" runat="server" Text="Descargar:"></asp:Label>
                    <%--<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Documentos/15. ACTA RESPUESTA No. 1 Y ADENDA NO. 2 PROCESO 80073.pdf" ImageUrl="~/images/PDF_ico.GIF" Text="dfgsdfg sdfg s "></asp:HyperLink><br />--%>
                    <br />
                    <br />
            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">CONTINUAR</asp:LinkButton></td>
            </tr>
        </table>
    </div>
        <div>
            &nbsp;</div>
    </form>

</asp:Content>