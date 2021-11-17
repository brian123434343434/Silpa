<%@ Control Language="C#" AutoEventWireup="true" CodeFile="cltCaptcha.ascx.cs" Inherits="controles_cltCaptcha" %>
<asp:UpdatePanel runat="server" ID="upnlCaptcha" UpdateMode="Conditional">
    <ContentTemplate>
        <table>
            <tr>
                <td colspan="2">
                    <asp:Image runat="server" ID="imgCaptcha" ImageUrl="~/Captcha.ashx" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox runat="server" ID="txtCaptcha" Width="170"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfvCaptcha" ControlToValidate="txtCaptcha" Display="Dynamic" ErrorMessage="Debe ingresar el texto contenido en la imagen de verificación.">*</asp:RequiredFieldValidator>
                    <asp:CustomValidator runat="server" ID="cvCaptcha" OnServerValidate="cvCaptcha_ServerValidate" Display="Dynamic" ErrorMessage="El texto ingresado no concuerda con el texto de la imagen de verificación.">*</asp:CustomValidator>
                </td>
                <td>
                    <asp:ImageButton runat="server" ID="imgRecargar" ImageUrl="~/images/recargar.png" OnClick="imgRecargar_Click" AlternateText="Generar Nueva Imagen" Height="20" CausesValidation="false" />
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
