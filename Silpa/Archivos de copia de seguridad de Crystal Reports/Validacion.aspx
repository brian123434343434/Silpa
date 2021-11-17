<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="Validacion.aspx.cs" Inherits="Validacion_Validacion" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="div-titulo">
    <asp:Label ID="lblTituloPrincipal" runat="server" Text="ADICIONAR VALIDACIÓN" SkinID="titulo_principal_blanco"></asp:Label>
</div>
<div class="div-contenido">
<asp:ScriptManager ID="scmManejador" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="uppPanelValidacion" runat="server">
    <ContentTemplate>
        <div style="text-align: center">
            <table style="width: 70%; border: solid 1px #D8D8D8">
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td align="left">
                                    <asp:Label ID="lblDescripcionValidacion" runat="server" SkinID="etiqueta_negra" Text="Descripción de la Validación"></asp:Label></td>
                                <td align="left">
                                    <asp:TextBox ID="txtDescripcionValidacion" runat="server" SkinID="texto" MaxLength="30"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvDescripcionValidacion" runat="server" Display="Dynamic"
                                        ErrorMessage="Ingrese la descripción del tipo de la sentencia" ControlToValidate="txtDescripcionValidacion">*</asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td align="left" valign="top">
                                    <asp:Label ID="lblSentenciaValidacion" runat="server" SkinID="etiqueta_negra" Text="Sentencia de la Validación:"></asp:Label></td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="txtSentenciaValidacion" runat="server" SkinID="texto" MaxLength="1000" Rows="3" TextMode="MultiLine"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvSentenciaValidacion" runat="server" ControlToValidate="txtSentenciaValidacion"
                                        ErrorMessage="Ingrese la sentencia del tipo de la sentencia">*</asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:ValidationSummary ID="valResumen" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Button ID="btnAgregarValidacion" runat="server" SkinID="boton_copia" Text="Agregar" OnClick="btnAgregarValidacion_Click" />
                                    &nbsp;<asp:Button ID="btnCancelarValidacion" runat="server" SkinID="boton_copia" Text="Cancelar" OnClick="btnCancelarValidacion_Click" />&nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
</div>
</asp:Content>

