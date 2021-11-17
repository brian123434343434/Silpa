<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="ConsultaTipoDatos.aspx.cs" Inherits="Validacion_ConsultaTipoDatos" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="div-titulo">
    <asp:Label ID="lblTituloPrincipal" runat="server" Text="TIPOS DE DATO" SkinID="titulo_principal_blanco"></asp:Label>
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
                                <td>
                                    <asp:Panel ID="pnlTiposDatos" runat="server" Width="370px" Height="200px" ScrollBars="Both">
                                        <asp:GridView ID="grdTiposDatos" runat="server" AutoGenerateColumns="False" SkinID="Grilla_simple" Width="350px" OnRowCommand="grdTiposDatos_RowCommand">
                                            <Columns>
                                                <asp:ButtonField CommandName="Actualizar" Text="Actualizar" />
                                                <asp:BoundField DataField="ID" HeaderText="C&#243;digo" />
                                                <asp:BoundField DataField="DESCRIPCION" HeaderText="Descripci&#243;n" />
                                                <asp:BoundField DataField="SEPARADOR" HeaderText="Separador" />
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>                        
                                </td>                        
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="pnlActualizarTipoDato" runat="server" Visible="False">
                                        <table>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lblCodigoTipoDato" runat="server" Text="Código" SkinID="etiqueta_negra"></asp:Label></td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtCodigoTipoDato" runat="server" ReadOnly="True" SkinID="texto_corto"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lblDescripcionTipoDato" runat="server" SkinID="etiqueta_negra" Text="Descripción del Tipo de Dato"></asp:Label></td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtDescripcionTipoDato" runat="server" SkinID="texto"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvDescripcionTipoDato" runat="server" ControlToValidate="txtDescripcionTipoDato"
                                                        Display="Dynamic" ErrorMessage="Ingrese la descripción del tipo de dato">*</asp:RequiredFieldValidator></td>
                                            </tr>
                                            <tr align="center">
                                                <td colspan="2">
                                                    <asp:ValidationSummary ID="valResumen" runat="server" />
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td colspan="2">
                                                    <asp:Button ID="btnActualizarTipoDato" runat="server" OnClick="btnActualizarTipoDato_Click"
                                                        SkinID="boton_copia" Text="Actualizar" />
                                                    &nbsp;<asp:Button ID="btnCancelarTipoDato" runat="server" SkinID="boton_copia" Text="Cancelar" OnClick="btnCancelarTipoDato_Click" />&nbsp;</td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    
                                </td>
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

