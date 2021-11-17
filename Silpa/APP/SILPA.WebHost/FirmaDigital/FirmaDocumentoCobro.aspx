<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true"
    CodeFile="FirmaDocumentoCobro.aspx.cs" Inherits="FirmaDigital_FirmaDocumento"
    Title="Untitled Page" %>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="div-titulo">
        <asp:Label ID="Label1" runat="server" Text="FIRMAR DOCUMENTO" SkinID="titulo_principal_blanco"></asp:Label>
    </div>
    <div class="div-contenido">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table style="width: 600px">
            <tr>
                <td colspan="3" style="text-align: center; width: 603px;">
                    <asp:Button ID="btnFirmar" runat="server" Text="Firmar Documento" PostBackUrl="~/NotificacionElectronica/Documento.aspx"
                        OnClientClick="return alert('Documento Firmado.')" />
                </td>
            </tr>
            <tr>
                <td colspan="3" rowspan="2" style="width: 603px">
                    <iframe id="ifdoc" src="../documentos/DocumentoCobro.pdf" height="600" width="600"
                        style="height: 433px"></iframe>
                </td>
            </tr>
            <tr>
            </tr>
        </table>
    </div>
</asp:Content>
