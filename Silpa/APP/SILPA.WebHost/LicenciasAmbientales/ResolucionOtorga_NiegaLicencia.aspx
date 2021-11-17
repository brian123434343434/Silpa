<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="ResolucionOtorga_NiegaLicencia.aspx.cs" Inherits="FirmaDigital_FirmaDocumento" Title="Untitled Page" %>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
<div class="SubHeaderBack">
	<div class="pageHeader">
		<div class="subBannerPhoto"></div>
		<p>&nbsp;</p>
		<%--TITLE--%>
		<p> &nbsp; <span class="specialsBtn"><strong>FIRMAR DOCUMENTO</strong></span></p>
		<p>&nbsp;</p>		
	</div>
</div>
<div class="copy">
<%--BODY--%>

<asp:ScriptManager id="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table style="width: 600px">
        <tr>
            <td colspan="3" style="text-align: center">
                <asp:Button ID="btnFirmar" runat="server" Text="Firmar Documento" PostBackUrl="~/NotificacionElectronica/Documento.aspx" OnClientClick="return alert('Documento Firmado.')" /></td>
        </tr>
        <tr>
            <td colspan="3" rowspan="2">
            <iframe id="ifdoc" src="../documentos/Resolucion_que_Otorga_Niega_Licencia.pdf" height="600" width="600" ></iframe>
            </td>
        </tr>
        <tr>
        </tr>
    </table>
    </div>
</asp:Content>

