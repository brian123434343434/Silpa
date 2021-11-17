<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="TipoDato.aspx.cs" Inherits="Validacion_TipoDato" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="div-titulo">
    <asp:Label ID="lblTituloPrincipal" runat="server" Text="ADICIONAR TIPO DE DATO" SkinID="titulo_principal_blanco"></asp:Label>
</div>
<div class="div-contenido">
<asp:ScriptManager ID="scmManejador" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="uppPanelTipoDato" runat="server">
    <ContentTemplate>
        <div style="text-align: center">
            <table style="width: 70%; border: solid 1px #D8D8D8">
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td align="left">
                                    <asp:Label ID="lblDescripcionTipoDato" runat="server" SkinID="etiqueta_negra" Text="Descripción del Tipo de Dato"></asp:Label></td>
                                <td align="left">
                                    <asp:TextBox ID="txtDescripcionTipoDato" runat="server" SkinID="texto"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvDescripcionTipoDato" runat="server" Display="Dynamic"
                                        ErrorMessage="Ingrese la descripción del tipo de dato" ControlToValidate="txtDescripcionTipoDato">*</asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:ValidationSummary ID="valResumen" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Button ID="btnAgregarTipoDato" runat="server" SkinID="boton_copia" Text="Agregar" OnClick="btnAgregarTipoDato_Click" />
                                    &nbsp;<asp:Button ID="btnCancelarTipoDato" runat="server" SkinID="boton_copia" Text="Cancelar" OnClick="btnCancelarTipoDato_Click" />&nbsp;</td>
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

