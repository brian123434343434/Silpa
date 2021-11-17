<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true"
    CodeFile="TipoTramite.aspx.cs" Inherits="Administracion_Tablasbasicas_TipoTramite"
    Title="Tabla Basica Tipo Tramite" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <div class="div-titulo">
        <asp:Label ID="lblTitulo" runat="server" Text="TRAMITE" SkinID="titulo_principal_blanco"></asp:Label>
    </div>    
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
                                                <asp:Label ID="lblTipoCon" runat="server" Text="Nombre"></asp:Label></td>
                                            <td align="left">
                                                <asp:TextBox ID="txtNombreParametro" runat="server" SkinID="texto" Width="100%"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblTipoParametro" runat="server" Text="Proceso"></asp:Label></td>
                                            <td align="left">
                                                <asp:DropDownList ID="cboTipoParametro" runat="server" Width="100%">
                                                </asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td align="right">
                                                <asp:Button ID="btnBuscar" OnClick="btnBuscar_Click" runat="server" SkinID="boton"
                                                    Text="Buscar"></asp:Button>
                                                <asp:Button ID="btnAgregar" OnClick="btnAgregar_Click" runat="server" SkinID="boton"
                                                    Text="Agregar" CausesValidation="False"></asp:Button><asp:Button ID="btnVolver" runat="server"
                                                        SkinID="boton" Text="Cancelar" PostBackUrl="~/Administracion/Tablasbasicas/TablasBasicas.aspx">
                                                    </asp:Button>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="pnlConsultar" runat="server" Width="100%" Visible="true">
                                <asp:GridView ID="grdDatos" runat="server" Width="100%" OnPageIndexChanging="grdDatos_PageIndexChanging"
                                    DataKeyNames="ID,ID_TIPO_PROCESO" OnRowCommand="grdDatos_RowCommand" EmptyDataText="No existen datos registrados en ésta tabla"
                                    AllowSorting="True" AllowPaging="True" AutoGenerateColumns="False" >
                                    <Columns>
                                        <asp:BoundField DataField="NOMBRE_TIPO_TRAMITE" HeaderText="Nombre"></asp:BoundField>
                                        <asp:BoundField DataField="PRO_CLAVE_PROCESO" HeaderText="Proceso"></asp:BoundField>
                                        <asp:CheckBoxField DataField="Visible" HeaderText="Visible"></asp:CheckBoxField>
                                        <asp:CheckBoxField DataField="MOSTRAR_DOCUMENTOS" HeaderText="Mostrar Documentos"></asp:CheckBoxField>
                                        <asp:TemplateField HeaderText="Editar">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkModificar" CommandName="Modificar" runat="server" CommandArgument='<%# Container.DataItemIndex %>'>Modificar Registro</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Eliminar">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEliminar" CommandName="Eliminar" runat="server" CommandArgument='<%# Container.DataItemIndex %>'>Eliminar Registro</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                            <asp:Panel ID="pnlEditar" runat="server" Width="100%" Visible="false">
                                <table width="70%">
                                    <tbody>
                                        <tr>
                                            <td align="left" style="width: 25%">
                                                <asp:Label ID="lblNombre" runat="server" Text="Nombre" ></asp:Label></td>
                                            <td  align="left">
                                                <asp:TextBox ID="txtNombre" runat="server" MaxLength="30" SkinID="texto"></asp:TextBox>
                                                <asp:Label ID="lblId" runat="server" Visible="False"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="lblTipo" runat="server" Text="Proceso"></asp:Label></td>
                                            <td  align="left">
                                                <asp:DropDownList ID="cboTipo" runat="server" Width="100%">
                                                </asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label1" runat="server" Text="Visible"></asp:Label></td>
                                            <td  align="left">
                                                <asp:CheckBox ID="chkVisible" runat="server">
                                                </asp:CheckBox></td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label2" runat="server" Text="Mostrar Documentos"></asp:Label></td>
                                            <td  align="left">
                                                <asp:CheckBox ID="chkMostrarDocu" runat="server">
                                                </asp:CheckBox></td>
                                        </tr>
                                        <tr>
                                            <td align="right" colspan="4">
                                                <asp:Button ID="btnActualizar" OnClick="btnActualizar_Click" runat="server" SkinID="boton"
                                                    Text="Aceptar"></asp:Button>
                                                <asp:Button ID="btnCancelar" OnClick="btnCancelar_Click" runat="server" SkinID="boton"
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
                                                <asp:Label ID="lblNombreNvo" runat="server" Text="Nombre"></asp:Label></td>
                                            <td style="width: 25%" align="left">
                                                <asp:TextBox ID="txtNombreNvo" runat="server" MaxLength="30" SkinID="texto"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 48px" align="left">
                                                <asp:Label ID="lblTipoNvo" runat="server" Text="Proceso"></asp:Label></td>
                                            <td style="width: 25%" align="left">
                                                <asp:DropDownList ID="cboTipoNvo" runat="server" Width="100%">
                                                </asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td align="right" colspan="4">
                                                &nbsp;<asp:Button ID="btnRegistrar" OnClick="btnRegistrar_Click" runat="server" SkinID="boton"
                                                    Text="Aceptar"></asp:Button>
                                                <asp:Button ID="btnCancelarReg" OnClick="btnCancelarReg_Click" runat="server" SkinID="boton"
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
</asp:Content>
