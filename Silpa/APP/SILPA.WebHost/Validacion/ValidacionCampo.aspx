<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="ValidacionCampo.aspx.cs" Inherits="Validacion_ValidacionCampo" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="div-titulo">
    <asp:Label ID="lblTituloPrincipal" runat="server" Text="ADICIONAR VALIDACIÓN CAMPO" SkinID="titulo_principal_blanco"></asp:Label>
</div>
<div class="div-contenido">
<asp:ScriptManager ID="scmManejador" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="uppPanelValidacionCampo" runat="server">
    <ContentTemplate>
        <div style="text-align: center">
            <table style="width: 70%; border: solid 1px #D8D8D8">
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td align="left">
                                    <asp:Label ID="lblCampo" runat="server" SkinID="etiqueta_negra" Text="Campo:"></asp:Label></td>
                                <td align="left">
                                    <asp:DropDownList ID="cboCampo" runat="server" SkinID="lista_desplegable">
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Label ID="lblValidacion" runat="server" SkinID="etiqueta_negra" Text="Validación:"></asp:Label></td>
                                <td align="left">
                                    <asp:DropDownList ID="cboValidacion" runat="server" SkinID="lista_desplegable">
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Label ID="lblActivo" runat="server" SkinID="etiqueta_negra" Text="Activo:"></asp:Label></td>
                                <td align="left">
                                    <asp:CheckBox ID="chkActivo" runat="server" /></td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Button ID="btnAgregarValidacionCampo" runat="server" SkinID="boton_copia" Text="Agregar" OnClick="btnAgregarValidacionCampo_Click" />
                                    &nbsp;<asp:Button ID="btnCancelarValidacionCampo" runat="server" SkinID="boton_copia" Text="Cancelar" OnClick="btnCancelarValidacionCampo_Click" />&nbsp;</td>
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

