<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true"
    CodeFile="FirmaDocumento.aspx.cs" Inherits="FirmaDigital_FirmaDocumento" Title="Untitled Page" %>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="div-titulo">
        <asp:Label ID="Label1" SkinID="titulo_principal_blanco" runat="server" Text="FIRMAR DOCUMENTO"></asp:Label>
    </div>
    <div class="div-contenido">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table style="width: 600px">
            <tr>
                <td colspan="3" style="text-align: center">
                    <asp:Button ID="btnFirmar" SkinID="boton" runat="server" Text="Firmar Documento"
                        PostBackUrl="~/NotificacionElectronica/Documento.aspx" OnClientClick="return alert('Documento Firmado.')" />
                </td>
            </tr>
            <tr>
                <td colspan="3" rowspan="2">
                    <iframe id="ifdoc" src="../documentos/ActoAdministrativo2.pdf" height="600" width="600">
                    </iframe>
                </td>
            </tr>
            <tr>
            </tr>
        </table>
    </div>
</asp:Content>
