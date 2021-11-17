<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true"
    CodeFile="CorreoPlantilla.aspx.cs" Inherits="Administracion_Tablasbasicas_CorreoPlantilla"
    ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <div class="div-titulo">
        <asp:Label ID="lblTitulo" runat="server" Text="PLANTILLA CORREOS" SkinID="titulo_principal_blanco"></asp:Label>
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
                                            <asp:Button ID="btnVolver" runat="server" SkinID="boton_copia" Text="Cancelar" PostBackUrl="~/Administracion/Tablasbasicas/TablasBasicas.aspx">
                                            </asp:Button>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="pnlConsultar" runat="server" Width="100%" Visible="true">
                                <asp:Panel ID="pnlConsultaPlantilla" runat="server" Height="350px" Width="800px"
                                    ScrollBars="Auto">
                                    <asp:GridView ID="grdDatos" runat="server" Width="100%" OnPageIndexChanging="grdDatos_PageIndexChanging"
                                        DataKeyNames="CORREO_PLANTILLA_ID" OnRowCommand="grdDatos_RowCommand" EmptyDataText="No existen datos registrados en ésta tabla"
                                        AllowSorting="True" AllowPaging="True" AutoGenerateColumns="False" PageSize="10">
                                        <Columns>
                                            <asp:BoundField DataField="CORREO_PLANTILLA_ID" HeaderText="Id Plantilla"></asp:BoundField>
                                            <asp:BoundField DataField="DE" HeaderText="De"></asp:BoundField>
                                            <asp:BoundField DataField="CC" HeaderText="CC"></asp:BoundField>
                                            <asp:BoundField DataField="PLANTILLA" HeaderText="Plantilla" />
                                            <asp:BoundField DataField="ASUNTO" HeaderText="Asunto" />
                                            <asp:BoundField DataField="CORREO_SERVIDOR_ID" HeaderText="Id Servidor Correo"></asp:BoundField>
                                            <asp:BoundField DataField="NOMBRE_SERVIDOR" HeaderText="Nombre Servidor"></asp:BoundField>
                                            <asp:BoundField DataField="CONFIRMAR_ENVIO" HeaderText="Confirma Envio"></asp:BoundField>
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
                            </asp:Panel>
                            <asp:Panel ID="pnlEditar" runat="server" Width="75%" Visible="false">
                                <table width="100%">
                                    <tbody>
                                        <tr>
                                            <td style="width: 10%" align="left">
                                                <asp:Label ID="lblDe" runat="server" Text="De"></asp:Label>
                                            </td>
                                            <td style="width: 25%" align="left">
                                                <asp:TextBox ID="txtDe" runat="server" Width="190px"></asp:TextBox>
                                            </td>
                                            <td>
                                            </td>
                                            <td style="width: 173px">
                                                <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10%" align="left">
                                                <asp:Label ID="lblPara" runat="server" Text="Para"></asp:Label></td>
                                            <td style="width: 25%" align="left" colspan="3">
                                                <asp:TextBox ID="txtPara" runat="server" Width="193px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10%" align="left">
                                                <asp:Label ID="lblPlantilla" runat="server" Text="Plantilla"></asp:Label></td>
                                            <td style="width: 25%" align="left" colspan="3">
                                                <asp:TextBox ID="txtPlantilla" runat="server" Width="240px" TextMode="MultiLine"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10%; height: 26px" align="left">
                                                <asp:Label ID="lblAsunto" runat="server" Text="Asunto"></asp:Label></td>
                                            <td style="width: 25%; height: 26px" align="left" colspan="3">
                                                <asp:TextBox ID="txtAsunto" runat="server" Width="194px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10%" align="left">
                                                <asp:Label ID="lblIdCorreoServidor" runat="server" Text="Correo Servidor"></asp:Label></td>
                                            <td style="width: 25%" align="left" colspan="3">
                                                <asp:DropDownList ID="cmbNombreServidor" runat="server" __designer:wfdid="w2">
                                                </asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10%" align="left">
                                                <asp:Label ID="lblConfirmarEnvio" runat="server" Text="Confirmar Envio"></asp:Label></td>
                                            <td style="width: 25%" align="left" colspan="3">
                                                <asp:DropDownList ID="cmbConfirmarEnvio" runat="server" __designer:wfdid="w4">
                                                    <asp:ListItem Value="0">NO</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="1">SI</asp:ListItem>
                                                </asp:DropDownList></td>
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
                                                <asp:Label ID="lblDe_Nuevo" runat="server" Text="De"></asp:Label>
                                            </td>
                                            <td style="width: 25%" align="left" colspan="3">
                                                <asp:TextBox ID="txtDe_Nuevo" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10%" align="left">
                                                <asp:Label ID="lblPara_Nuevo" runat="server" Text="Para"></asp:Label>
                                            </td>
                                            <td style="width: 25%" align="left" colspan="3">
                                                <asp:TextBox ID="txtPara_Nuevo" runat="server" Width="193px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10%" align="left">
                                                <asp:Label ID="lblPlantilla_Nuevo" runat="server" Text="Plantilla"></asp:Label>
                                            </td>
                                            <td style="width: 25%" align="left" colspan="3">
                                                <asp:TextBox ID="txtPlantilla_Nuevo" runat="server" Width="349px" TextMode="MultiLine"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10%" align="left">
                                                <asp:Label ID="lblAsunto_Nuevo" runat="server" Text="Asunto"></asp:Label>
                                            </td>
                                            <td style="width: 25%" align="left" colspan="3">
                                                <asp:TextBox ID="txtAsunto_Nuevo" runat="server" Width="194px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10%" align="left">
                                                <asp:Label ID="lblIdCorreoServidor_Nuevo" runat="server" Text="Correo Servidor"></asp:Label>
                                            </td>
                                            <td style="width: 25%" align="left" colspan="3">
                                                <asp:DropDownList ID="cmbNombreServidor_Nuevo" runat="server" __designer:wfdid="w1">
                                                </asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10%" align="left">
                                                <asp:Label ID="lblConfirmarEnvio_Nuevo" runat="server" Text="Confirmar Envio"></asp:Label>
                                            </td>
                                            <td style="width: 25%" align="left" colspan="3">
                                                &nbsp;<asp:DropDownList ID="cmbConfirmaEnvio_Nuevo" runat="server" __designer:wfdid="w5">
                                                    <asp:ListItem Value="0">NO</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="1">SI</asp:ListItem>
                                                </asp:DropDownList></td>
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
                            <asp:Label ID="lblMensajeError" runat="server" text="dasdasdasd" Font-Bold="true" ForeColor="red"></asp:Label>
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
