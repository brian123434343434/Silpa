<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Si_Evaluar_Necesidad_DAA.aspx.cs" Inherits="LicenciasAmbientales_02_Si_Evaluar_Necesidad_DAA" MasterPageFile="~/plantillas/SILPA.master" enableEventValidation="false"%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


    <form id="form1">
    
    <table style="width: 1178px">
       
            <tr>
                <td colspan="3">
                    <asp:Label ID="LBL_SOLICITUD_LIC" runat="server" Text="SOLICITUD DE LICENCIAS"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 963px">
        <asp:Label ID="LBL_CONS_REQ_DAA" runat="server" Text="CONSULTAR REQUERIMIENTO DE DIAGNOSTICO AMBIENTAL DE ALTERNATIVAS"></asp:Label></td>
                <td colspan="2">
        <asp:Label ID="LBL_CU" runat="server" Text="LPA-LIC-002a"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 963px; height: 344px">
                    <ul>
                        <li>
            <asp:Label ID="LBL_AA" runat="server" Text="La Autoridad Ambiental competente ante la que realizará su trámite es:"></asp:Label>
        <asp:Label ID="LBL_AA_RES" runat="server" Text="MINISTERIO AMBIENTE, VIVIENDA Y DESARROLLO TERRITORIAL."></asp:Label></li></ul>
                    <p>
                        &nbsp;</p>
                    <ul>
                        <li>
                            <asp:Label ID="LBL_REQUIERE" runat="server" Text="Este proyecto SI requiere DIAGNOSTICO AMBIENTAL DE ALTERNATIVAS."></asp:Label>
                        </li>
                    </ul>
                    <p>
                        &nbsp;</p>
                    <p>
        <asp:Label ID="LBL_TR" runat="server" Text="Los Términos de referencia serán comunicados mediante un Acto Administrativo que podrá consultar en la página de Seguimiento de Trámites"></asp:Label>&nbsp;</p>
                    <p>
                        &nbsp;</p>
                    <p>
                    
        <asp:LinkButton ID="LNK_CONTINUAR" runat="server" OnClick="LinkButton1_Click">CONTINUAR</asp:LinkButton>&nbsp;</p>
                </td>
                <td style="height: 344px" colspan="2">
                </td>
            </tr>
        </table>
    
    <p>
        &nbsp;</p>

    </form>

</asp:Content>