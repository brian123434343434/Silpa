<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPASinMenu.master" AutoEventWireup="true"
    CodeFile="Gen_Tipo_Documento_Acreditacion.aspx.cs" Inherits="Administracion_Tablasbasicas_Gen_Tipo_Documento_Acreditacion"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <div class="div-titulo">
        <asp:Label ID="lblTitulo" runat="server" Text="TIPO DOCUMENTO ACREDITADO" SkinID="titulo_principal_blanco"></asp:Label>
    </div>
    <div class="div-contenido" style="height: 400px">
        <table width="100%">
            <tr>
                <td colspan="4">
                    <asp:UpdatePanel ID="updConsultar" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="pnlMaestro" runat="server" Width="100%">
                                <table width="60%">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblTipoDocAcreditacion" runat="server" Text="Nombre"></asp:Label></td>
                                            <td align="left">
                                                <asp:TextBox ID="txtNombreTipoDocAcreditacion" runat="server" SkinID="texto" Width="100%"></asp:TextBox></td>
                                        </tr>

                                        <tr>
                                            <td>
                                            </td>
                                            <td align="right">
                                                <asp:Button ID="btnBuscar" OnClick="btnBuscar_Click" runat="server" SkinID="boton_copia"
                                                    Text="Buscar"></asp:Button>
                                                <asp:Button ID="btnAgregar" OnClick="btnAgregar_Click" runat="server" SkinID="boton_copia"
                                                    Text="Agregar" CausesValidation="False"></asp:Button><asp:Button ID="btnVolver" runat="server"
                                                        SkinID="boton_copia" Text="Cancelar" PostBackUrl="~/Administracion/Tablasbasicas/TablasBasicas.aspx">
                                                    </asp:Button>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="pnlConsultar" runat="server" Width="100%" Visible="true">
                                <asp:GridView ID="grdDatos" runat="server" Width="100%" OnPageIndexChanging="grdDatos_PageIndexChanging"
                                    DataKeyNames="ID_TIP_DOC_ACREDITACION" 
                                    OnRowCommand="grdDatos_RowCommand" 
                                    EmptyDataText="No existen datos registrados en ésta tabla"
                                    AllowSorting="True" 
                                    AllowPaging="True" 
                                    AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="ID_TIP_DOC_ACREDITACION" HeaderText="ID Campo"></asp:BoundField>
                                        <asp:BoundField DataField="NOMBRE_TIP_DOC_ACREDITACION" HeaderText="Tipo Documento Acreditacion"></asp:BoundField>
                                        <asp:TemplateField HeaderText="Editar">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkModificar" CommandName="Modificar" runat="server" CommandArgument='<%# Container.DataItemIndex %>'>Modificar Registro</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Eliminar">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEliminar" CommandName="Eliminar" runat="server"  CommandArgument='<%# Container.DataItemIndex %>' OnClientClick="return confirm('Esta seguro de Eliminar este registro?')">Eliminar Registro</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                            <asp:Panel ID="pnlEditar" runat="server" Width="100%" Visible="false">
                                <table width="100%">
                                    <tbody>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="lblTipoDocAcreditacionEdit" runat="server" Text="Nombre"></asp:Label></td>
                                            <td style="width: 25%" align="left">
                                                <asp:TextBox ID="txtTipoDocAcreditacionEdit" runat="server" MaxLength="30"></asp:TextBox>
                                                <asp:Label ID="lblId" runat="server" Visible="False"></asp:Label></td>
                                        </tr>

                                        <tr>
                                            <td align="right" colspan="4">
                                                <asp:Button ID="btnActualizar" OnClick="btnActualizar_Click" runat="server" SkinID="boton_copia"
                                                    Text="Aceptar"></asp:Button>
                                                <asp:Button ID="btnCancelar" OnClick="btnCancelar_Click" runat="server" SkinID="boton_copia"
                                                    Text="Cancelar" CausesValidation="False"></asp:Button>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="pnlNuevo" runat="server" Visible="false">
                                <table>
                                    <tbody>
                                        <tr>
                                            <td style="width: 48px" align="left">
                                                <asp:Label ID="lblTipoDocAcreditacionNvo" runat="server" Text="Nombre"></asp:Label></td>
                                            <td style="width: 25%" align="left">
                                                <asp:TextBox ID="txtTipoDocAcreditacionNvo" runat="server" MaxLength="30"></asp:TextBox></td>
                                        </tr>
    
                                        <tr>
                                            <td align="right" colspan="4">
                                                &nbsp;<asp:Button ID="btnRegistrar" OnClick="btnRegistrar_Click" runat="server" SkinID="boton_copia"
                                                    Text="Aceptar"></asp:Button>
                                                <asp:Button ID="btnCancelarReg" OnClick="btnCancelarReg_Click" runat="server" SkinID="boton_copia"
                                                    Text="Cancelar" CausesValidation="False"></asp:Button>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </asp:Panel>
                            <asp:Label ID="lblMensajeError" runat="server" Font-Bold="True" ForeColor="Red" ></asp:Label>
                       
                        </ContentTemplate>
                                                <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
