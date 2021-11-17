<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true"
    CodeFile="TablaCorreoEstado.aspx.cs" Inherits="Administracion_Tablasbasicas_TablaCorreoEstado" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <div class="div-titulo">
        <asp:Label ID="lblTitulo" runat="server" Text="ESTADO_CORREO" SkinID="titulo_principal_blanco"></asp:Label>
    </div>
    <div class="div-contenido" style="height: 400px">
        <table width="100%">
            <tr>
                <td colspan="4">
                    <asp:UpdatePanel ID="updConsultar" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="pnlMaestro" runat="server" Width="100%">
                                <table width="60%">
                                    <tr>
                                        <td style="width: 15%" align="left">
                                            <asp:Label ID="lblNombreParametro" SkinID="etiqueta_negra" runat="server" Text="Descripción"></asp:Label>
                                        </td>
                                        <td style="width: 30%" align="left">
                                            <asp:TextBox ID="txtNombreParametro" SkinID="texto" runat="server" Width="100%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td align="right">
                                            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" SkinID="boton_copia" OnClick="btnBuscar_Click" />
                                            <asp:Button ID="btnagregar" runat="server" Text="Agregar" SkinID="boton_copia" OnClick="btnagregar_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="pnlConsultar" runat="server" Width="100%" Visible="true">
                                <asp:GridView ID="grdDatos" runat="server" AutoGenerateColumns="False" Width="100%"
                                    AllowPaging="True" AllowSorting="True" EmptyDataText="No existen datos registrados en ésta tabla"
                                    OnRowCommand="grdDatos_RowCommand" DataKeyNames="CORREO_ESTADO_ID" OnPageIndexChanging="grdDatos_PageIndexChanging">
                                    <Columns>
                                        <asp:BoundField DataField="CORREO_ESTADO_ID" HeaderText="ID Estado Correo" />
                                        <asp:BoundField DataField="ESTADO" HeaderText="Estado Correo" />
                                        <asp:TemplateField HeaderText="Editar">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkModificar" CommandName="Modificar" runat="server" CommandArgument='<%# Container.DataItemIndex %>'>Modificar Registro</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Eliminar">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEliminar" CommandName="Eliminar" runat="server" CommandArgument='<%# Container.DataItemIndex %>'
                                                    OnClientClick="return confirm('Esta seguro de Eliminar este registro?')">Eliminar Registro</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                            <asp:Panel ID="pnlEditar" runat="server" Width="100%" Visible="false">
                                <table width="100%">
                                    <tbody>
                                        <tr>
                                            <td style="width: 10%" align="left">
                                                <asp:Label ID="lblNombre" runat="server" Text="Nombre Estado"></asp:Label>
                                            </td>
                                            <td style="width: 25%" align="left">
                                                <asp:TextBox ID="txtNombre" runat="server" MaxLength="50"></asp:TextBox>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" colspan="4">
                                                <asp:Button ID="btnActualizar" OnClick="btnActualizar_Click" runat="server" SkinID="boton_copia"
                                                    Text="Aceptar"></asp:Button>
                                                <asp:Button ID="btnCancelar" OnClick="btnCancelar_Click" runat="server" SkinID="boton_copia"
                                                    Text="Cancelar"></asp:Button>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="pnlNuevo" runat="server" Visible="false">
                                <table>
                                    <tbody>
                                        <tr>
                                            <td style="width: 10%" align="left">
                                                <asp:Label ID="lblNombre_Nvo" runat="server" Text="Nombre Estado"></asp:Label>
                                            </td>
                                            <td style="width: 25%" align="left">
                                                <asp:TextBox ID="txtNombre_Nvo" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="rfvNombreEstado" runat="server" ControlToValidate="txtNombre_Nvo"
                                                    ErrorMessage="Debe ingresar un nombre de estado" ToolTip="Debe ingresar un nombre de estado">*</asp:RequiredFieldValidator></td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" colspan="4">
                                                &nbsp;<asp:Button ID="btnRegistrar" OnClick="btnRegistrar_Click" runat="server" SkinID="boton_copia"
                                                    Text="Aceptar"></asp:Button>
                                                <asp:Button ID="btnCancelarReg" OnClick="btnCancelarReg_Click" runat="server" SkinID="boton_copia"
                                                    Text="Cancelar"></asp:Button>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </asp:Panel>
                            <asp:Label ID="lblMensajeError" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                    &nbsp;&nbsp;
                    <asp:ValidationSummary ID="vsValidaciones" runat="server" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
