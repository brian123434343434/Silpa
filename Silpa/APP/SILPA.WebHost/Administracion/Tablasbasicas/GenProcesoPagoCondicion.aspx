<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPASinMenu.master" AutoEventWireup="true"
    CodeFile="GenProcesoPagoCondicion.aspx.cs" Inherits="Administracion_Tablasbasicas_GenProcesoPagoCondicion"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <div class="div-titulo">
        <asp:Label ID="lblTitulo" runat="server" Text="CONDICIONES PARA DOCUMENTO DE COBRO"
            SkinID="titulo_principal_blanco"></asp:Label>
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
                                                <asp:Label ID="lblCodConPago" runat="server" Text=" Código de Condición de Pago "></asp:Label></td>
                                            <td align="left">
                                                <asp:TextBox ID="txtCodConPago" runat="server" SkinID="texto" Width="100%"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblCodConImprimir" runat="server" Text="Código de Condición al Imprimir"></asp:Label></td>
                                            <td align="left">
                                                <asp:TextBox ID="txtCodConImprimir" runat="server" SkinID="texto" Width="100%"></asp:TextBox></td>
                                        </tr>
                                         <tr>
                                            <td>
                                                <asp:Label ID="lblIdProceso" runat="server" Text="Id Caso de Proceso"></asp:Label></td>
                                            <td align="left">
                                                <asp:TextBox ID="txtIdProceso" runat="server" SkinID="texto" Width="100%"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td align="right">
                                                <asp:Button ID="btnBuscar" OnClick="btnBuscar_Click" runat="server" SkinID="boton_copia"
                                                    Text="Buscar"></asp:Button>
                                                <asp:Button ID="btnAgregar" OnClick="btnAgregar_Click" runat="server" SkinID="boton_copia"
                                                    Text="Agregar" CausesValidation="False"></asp:Button>&nbsp;<asp:Button ID="btnVolver" runat="server"
                                                        SkinID="boton_copia" Text="Cancelar" PostBackUrl="~/Administracion/Tablasbasicas/TablasBasicas.aspx">
                                                    </asp:Button></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="pnlConsultar" runat="server" Width="100%" Visible="true">
                                <asp:GridView ID="grdDatos" runat="server" Width="100%" OnPageIndexChanging="grdDatos_PageIndexChanging"
                                    DataKeyNames="ID" OnRowCommand="grdDatos_RowCommand" EmptyDataText="No existen datos registrados en ésta tabla"
                                    AllowSorting="True" AllowPaging="True" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="ID" HeaderText="Id"></asp:BoundField>
                                        <asp:BoundField DataField="ID_PROCESS_CASE" HeaderText="Id Caso de Proceso"></asp:BoundField>
                                        <asp:BoundField DataField="CONDICION_PAGO" HeaderText="Código de Condición de Pago"></asp:BoundField>
                                        <asp:BoundField DataField="CONDICION_IMPRIMIR" HeaderText="Código de Condición al Imprimir"></asp:BoundField>
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
                                                <asp:Label ID="lblCodConPagoEdit" runat="server" Text="Código de Condición de Pago"></asp:Label></td>
                                            <td style="width: 25%" align="left">
                                                <asp:TextBox ID="txtCodConPagoEdit" runat="server" MaxLength="50"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="lblCodConImprimirEdit" runat="server" Text="Código de Condición al Imprimir"></asp:Label></td>
                                            <td style="width: 25%" align="left">
                                                <asp:TextBox ID="txtCodConImprimirEdit" runat="server" MaxLength="50"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="lblIdProcesoEdit" runat="server" Text="Id Caso de Proceso"></asp:Label>
                                            </td>
                                            <td style="width: 25%" align="left">
                                                <asp:TextBox ID="txtIdProcesoEdit" runat="server" MaxLength="9">0</asp:TextBox></td>
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
                                            <td style="width: 250px" align="left">
                                                <asp:Label ID="lblCodConPagoNvo" runat="server" Text="Código de Condición de Pago"></asp:Label></td>
                                            <td style="width: 25%" align="left">
                                                <asp:TextBox ID="txtCodConPagoNvo" runat="server" MaxLength="50"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 250px" align="left">
                                                <asp:Label ID="lblCodConImprimirNvo" runat="server" Text="Código de Condición al Imprimir"></asp:Label></td>
                                            <td style="width: 25%" align="left">
                                                <asp:TextBox ID="txtCodConImprimirNvo" runat="server" MaxLength="50"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 250px" align="left">
                                                <asp:Label ID="lblIdProcesoNvo" runat="server" Text="Id Caso de Proceso"></asp:Label>
                                            </td>
                                            <td style="width: 25%" align="left">
                                                <asp:TextBox ID="txtIdProcesoNvo" runat="server" MaxLength="9"></asp:TextBox></td>
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
                            <asp:Label ID="lblMensajeError" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </ContentTemplate>
                        
                    </asp:UpdatePanel>
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
