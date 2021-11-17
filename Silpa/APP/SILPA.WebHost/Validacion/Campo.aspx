<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="Campo.aspx.cs" Inherits="Validacion_Campo" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="div-titulo">
    <asp:Label ID="lblTituloPrincipal" runat="server" Text="ADICIONAR CAMPO" SkinID="titulo_principal_blanco"></asp:Label>
</div>
<div class="div-contenido">
<asp:ScriptManager ID="scmManejador" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="uppPanelCampo" runat="server">
    <ContentTemplate>
        <div style="text-align: center">
            <table style="width: 70%; border: solid 1px #D8D8D8">
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td align="left">
                                    <asp:Label ID="lblCodigoCampo" runat="server" SkinID="etiqueta_negra" Text="Código del Campo:"></asp:Label></td>
                                <td align="left">
                                    <asp:TextBox ID="txtCodigoCampo" runat="server" SkinID="texto" MaxLength="30"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvCodigoCampo" runat="server" Display="Dynamic"
                                        ErrorMessage="Ingrese el código del campo" ControlToValidate="txtCodigoCampo">*</asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Label ID="lblDescripcionCampo" runat="server" SkinID="etiqueta_negra" Text="Descripción del Campo:"></asp:Label></td>
                                <td align="left">
                                    <asp:TextBox ID="txtDescripcionCampo" runat="server" SkinID="texto" MaxLength="30"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvDescripcionCampo" runat="server" ControlToValidate="txtDescripcionCampo"
                                        ErrorMessage="Ingresela descripción del campo">*</asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Label ID="lblTipoDato" runat="server" SkinID="etiqueta_negra" Text="Tipo de Dato:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="cboTipoDato" runat="server" SkinID="lista_desplegable">
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:ValidationSummary ID="valResumen" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Button ID="btnAgregarCampo" runat="server" SkinID="boton_copia" Text="Agregar" OnClick="btnAgregarCampo_Click" />
                                    &nbsp;<asp:Button ID="btnCancelarCampo" runat="server" SkinID="boton_copia" Text="Cancelar" OnClick="btnCancelarCampo_Click" />&nbsp;</td>
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

