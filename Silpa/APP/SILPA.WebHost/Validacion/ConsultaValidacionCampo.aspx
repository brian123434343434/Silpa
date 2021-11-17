<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="ConsultaValidacionCampo.aspx.cs" Inherits="Validacion_ConsultaValidacionCampo" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="div-titulo">
    <asp:Label ID="lblTituloPrincipal" runat="server" Text="VALIDACIONES CAMPO" SkinID="titulo_principal_blanco"></asp:Label>
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
                                <td>
                                    <asp:Panel ID="pnlValidacionCampo" runat="server" Width="520px" Height="200px" ScrollBars="Both">
                                        <asp:GridView ID="grdValidacionCampo" runat="server" AutoGenerateColumns="False" SkinID="Grilla_simple" Width="500px" PageSize="5" OnRowCommand="grdValidacionCampo_RowCommand">
                                            <Columns>
                                                <asp:ButtonField CommandName="Actualizar" Text="Actualizar" />
                                                <asp:BoundField DataField="ID" HeaderText="C&#243;digo" />
                                                <asp:BoundField DataField="ID_CAMPO" HeaderText="C&#243;digo Campo" />
                                                <asp:BoundField DataField="ID_VALIDACION" HeaderText="C&#243;digo Validacion" />
                                                <asp:BoundField DataField="FECHA_INSERCION" HeaderText="Fecha de Inserci&#243;n" />
                                                <asp:BoundField DataField="ACTIVO_SN" HeaderText="Activo" />
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>                        
                                </td>                        
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="pnlActualizarValidacionCampo" runat="server" Visible="False">
                                        <table>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lblCodigoValidacionCampo" runat="server" SkinID="etiqueta_negra" Text="Código:"></asp:Label></td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtCodigoValidacionCampo" runat="server" ReadOnly="True" SkinID="texto"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lblCampo" runat="server" SkinID="etiqueta_negra" Text="Campo:"></asp:Label></td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtCampo" runat="server" ReadOnly="True" SkinID="texto_corto"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lblValidacion" runat="server" SkinID="etiqueta_negra" Text="Validación:"></asp:Label></td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtValidacion" runat="server" ReadOnly="True" SkinID="texto_corto"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lblFechaInsercion" runat="server" SkinID="etiqueta_negra" Text="Fecha de Inserción:"></asp:Label></td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtFechaInsercion" runat="server" MaxLength="10" SkinID="texto_corto"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="calFechaInsercion" runat="server" Format="yyyy/MM/dd" TargetControlID="txtFechaInsercion">
                                                    </cc1:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lblActivo" runat="server" SkinID="etiqueta_negra" Text="Activo:"></asp:Label></td>
                                                <td align="left">
                                                    <asp:CheckBox ID="chkActivo" runat="server" /></td>
                                            </tr>
                                            <tr align="center">
                                                <td colspan="2">
                                                    <asp:Button ID="btnActualizarValidacionCampo" runat="server" OnClick="btnActualizarValidacionCampo_Click"
                                                        SkinID="boton_copia" Text="Actualizar" />
                                                    &nbsp;<asp:Button ID="btnCancelarValidacionCampo" runat="server" SkinID="boton_copia" Text="Cancelar" OnClick="btnCancelarValidacionCampo_Click" />&nbsp;</td>
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

