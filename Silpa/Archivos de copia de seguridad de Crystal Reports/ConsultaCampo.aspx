<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="ConsultaCampo.aspx.cs" Inherits="Validacion_ConsultaCampo" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="div-titulo">
    <asp:Label ID="lblTituloPrincipal" runat="server" Text="CAMPOS" SkinID="titulo_principal_blanco"></asp:Label>
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
                                <td>
                                    <asp:Panel ID="pnlCampos" runat="server" Width="520px" Height="200px" ScrollBars="Both">
                                        <asp:GridView ID="grdCampo" runat="server" AutoGenerateColumns="False" SkinID="Grilla_simple" Width="500px" OnRowCommand="grdCampo_RowCommand" PageSize="5">
                                            <Columns>
                                                <asp:ButtonField CommandName="Actualizar" Text="Actualizar" />
                                                <asp:BoundField DataField="ID" HeaderText="C&#243;digo" />
                                                <asp:BoundField DataField="DESCRIPCION" HeaderText="Descripci&#243;n" />
                                                <asp:BoundField DataField="ID_TIPO_DATO" HeaderText="Tipo de dato" />
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>                        
                                </td>                        
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="pnlActualizarCampo" runat="server" Visible="False">
                                        <table>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label1" runat="server" SkinID="etiqueta_negra" Text="Código del Campo:"></asp:Label></td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtCodigoCampo" runat="server" MaxLength="30" ReadOnly="True" SkinID="texto"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lblDescripcionCampo" runat="server" SkinID="etiqueta_negra" Text="Descripción del Campo:"></asp:Label></td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtDescripcionCampo" runat="server" MaxLength="30" SkinID="texto"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvDescripcionCampo" runat="server" ControlToValidate="txtDescripcionCampo"
                                                        ErrorMessage="Ingresela descripción del campo">*</asp:RequiredFieldValidator></td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label2" runat="server" SkinID="etiqueta_negra" Text="Tipo de Dato:"></asp:Label></td>
                                                <td align="left">
                                                    <asp:DropDownList ID="cboTipoDato" runat="server" SkinID="lista_desplegable">
                                                    </asp:DropDownList></td>
                                            </tr>
                                            <tr align="center">
                                                <td colspan="2">
                                                    <asp:ValidationSummary ID="valResumen" runat="server" />
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td colspan="2">
                                                    <asp:Button ID="btnActualizarCampo" runat="server" OnClick="btnActualizarCampo_Click"
                                                        SkinID="boton_copia" Text="Actualizar" />
                                                    &nbsp;<asp:Button ID="btnCancelarCampo" runat="server" SkinID="boton_copia" Text="Cancelar" OnClick="btnCancelarCampo_Click" />&nbsp;</td>
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

