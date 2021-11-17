<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="ConsultaValidacion.aspx.cs" Inherits="Validacion_ConsultaValidacion" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="div-titulo">
    <asp:Label ID="lblTituloPrincipal" runat="server" Text="VALIDACIÓNES" SkinID="titulo_principal_blanco"></asp:Label>
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
                                <td>
                                    <asp:Panel ID="pnlValidaciones" runat="server" Width="520px" Height="200px" ScrollBars="Both">
                                        <asp:GridView ID="grdValidacion" runat="server" AutoGenerateColumns="False" SkinID="Grilla_simple" Width="500px" OnRowCommand="grdValidacion_RowCommand">
                                            <Columns>
                                                <asp:ButtonField CommandName="Actualizar" Text="Actualizar" />
                                                <asp:BoundField DataField="ID" HeaderText="C&#243;digo" />
                                                <asp:BoundField DataField="DESCRIPCION" HeaderText="Descripci&#243;n" />
                                                <asp:BoundField DataField="SENTENCIA" HeaderText="Sentencia" />
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>                        
                                </td>                        
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="pnlActualizarValidacion" runat="server" Visible="False">
                                        <table>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lblCodigoValidacion" runat="server" Text="Código" SkinID="etiqueta_negra"></asp:Label></td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtCodigoValidacion" runat="server" ReadOnly="True" SkinID="texto_corto" MaxLength="30"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lblDescripcionValidacion" runat="server" SkinID="etiqueta_negra" Text="Descripción de la Validación"></asp:Label></td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtDescripcionValidacion" runat="server" SkinID="texto" MaxLength="30"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvDescripcionValidacion" runat="server" ControlToValidate="txtDescripcionValidacion"
                                                        Display="Dynamic" ErrorMessage="Ingrese la descripción de la validación">*</asp:RequiredFieldValidator></td>
                                            </tr>
                                            <tr>
                                                <td align="left" valign="top">
                                                    <asp:Label ID="lblSentenciaValidacion" runat="server" SkinID="etiqueta_negra" Text="Sentencia de la Validación"></asp:Label></td>
                                                <td align="left" valign="top">
                                                    <asp:TextBox ID="txtSentenciaValidacion" runat="server" MaxLength="1000" Rows="3"
                                                        SkinID="texto" TextMode="MultiLine"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvSentenciaValidacion" runat="server" ControlToValidate="txtSentenciaValidacion"
                                                        ErrorMessage="Ingrese la sentencia del tipo de la sentencia">*</asp:RequiredFieldValidator></td>
                                            </tr>
                                            <tr align="center">
                                                <td colspan="2">
                                                    <asp:ValidationSummary ID="valResumen" runat="server" />
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td colspan="2">
                                                    <asp:Button ID="btnActualizarValidacion" runat="server" OnClick="btnActualizarValidacion_Click"
                                                        SkinID="boton_copia" Text="Actualizar" />
                                                    &nbsp;<asp:Button ID="btnCancelarValidacion" runat="server" SkinID="boton_copia" Text="Cancelar" OnClick="btnCancelarValidacion_Click" />&nbsp;</td>
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

