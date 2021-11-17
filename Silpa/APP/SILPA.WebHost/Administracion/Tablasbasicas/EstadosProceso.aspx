<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true"
    CodeFile="EstadosProceso.aspx.cs" Inherits="Administracion_Tablasbasicas_EstadosProceso" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <div class="div-titulo">
        <asp:Label ID="lblTitulo" runat="server" Text="ESTADOS DE CONFLICTO" SkinID="titulo_principal_blanco"></asp:Label>
    </div>
    <div class="div-contenido" style="height: 400px">
        <table width="100%">
            <tr>
                <td colspan="4">
                    <asp:UpdatePanel ID="updConsultar" runat="server">
                        <contenttemplate>
<asp:Panel id="pnlMaestro" runat="server" Width="100%">
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
                            </asp:Panel> <asp:Panel id="pnlConsultar" runat="server" Width="100%" Visible="true">
                                <asp:GridView 
                                        ID="grdDatos" 
                                        runat="server" 
                                        Width="100%" 
                                        OnPageIndexChanging="grdDatos_PageIndexChanging"
                                        DataKeyNames="TES_ID_ESTADO_PROCESO" 
                                        OnRowCommand="grdDatos_RowCommand" 
                                        EmptyDataText="No existen datos registrados en ésta tabla"
                                        AllowSorting="True" 
                                        AllowPaging="True" 
                                        AutoGenerateColumns="False" 
                                        PageSize="10">
                                        <Columns>
                                            <asp:BoundField DataField="TES_ID_ESTADO_PROCESO" HeaderText="Id Estado"></asp:BoundField>
                                            <asp:BoundField DataField="TES_ESTADO_PROCESO" HeaderText="Nombre Estado"></asp:BoundField>
                                            <asp:BoundField DataField="TES_DESCRIPCION" HeaderText="Descripción"></asp:BoundField>
                                            <asp:BoundField DataField="TES_ACTIVO" HeaderText="Activo" />
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
  
                            </asp:Panel> <asp:Panel id="pnlEditar" runat="server" Width="75%" Visible="false"><TABLE width="100%"><TBODY><TR><TD style="WIDTH: 10%" align=left><asp:Label id="lblNombreEstado" runat="server" Text="Estado Proceso"></asp:Label> </TD><TD style="WIDTH: 25%" align=left><asp:TextBox id="txtNombreEstado" runat="server" Width="190px"></asp:TextBox> </TD><TD></TD><TD style="WIDTH: 173px"><asp:Label id="lblID" runat="server" Visible="False"></asp:Label> </TD></TR><TR><TD style="WIDTH: 10%" align=left><asp:Label id="lblDescripcion" runat="server" Text="Descripcion"></asp:Label></TD><TD style="WIDTH: 25%" align=left colSpan=3><asp:TextBox id="txtDescripcion" runat="server" Width="193px"></asp:TextBox></TD></TR><TR><TD style="WIDTH: 10%" align=left><asp:Label id="lblActivo" runat="server" Text="Activo"></asp:Label></TD><TD style="WIDTH: 25%" align=left colSpan=3><asp:CheckBox id="chkActivo" runat="server" __designer:wfdid="w1"></asp:CheckBox></TD></TR><TR><TD align=right colSpan=4><asp:Button id="btnActualizar" onclick="btnActualizar_Click" runat="server" SkinID="boton_copia" Text="Aceptar"></asp:Button> <asp:Button id="btnCancelar" onclick="btnCancelar_Click" runat="server" SkinID="boton_copia" Text="Cancelar"></asp:Button> </TD></TR></TBODY></TABLE></asp:Panel> <asp:Panel id="pnlNuevo" runat="server" Visible="false"><TABLE><TBODY><TR><TD style="WIDTH: 10%" align=left><asp:Label id="lblNombreEstado_Nuevo" runat="server" Text="Estado Proceso"></asp:Label> </TD><TD style="WIDTH: 20%" align=left colSpan=3><asp:TextBox id="txtNombreEstado_Nuevo" runat="server" Width="191px"></asp:TextBox> </TD></TR><TR><TD style="WIDTH: 10%" align=left><asp:Label id="lblDescripcion_Nuevo" runat="server" Text="Descripción"></asp:Label> </TD><TD style="WIDTH: 20%" align=left colSpan=3><asp:TextBox id="txtDescripcion_Nuevo" runat="server" Width="193px"></asp:TextBox></TD></TR><TR><TD style="WIDTH: 10%" align=left><asp:Label id="lblActivo_Nuevo" runat="server" Text="Activo"></asp:Label> </TD><TD style="WIDTH: 20%" align=left colSpan=3><asp:CheckBox id="chkActivo_Nuevo" runat="server" __designer:wfdid="w2"></asp:CheckBox></TD></TR><TR><TD align=right colSpan=4>&nbsp;<asp:Button id="btnRegistrar" onclick="btnRegistrar_Click" runat="server" SkinID="boton_copia" Text="Aceptar"></asp:Button> <asp:Button id="btnCancelarReg" onclick="btnCancelarReg_Click" runat="server" SkinID="boton_copia" Text="Cancelar"></asp:Button> </TD></TR></TBODY></TABLE></asp:Panel> 
                            <asp:Label ID="lblMensajeError" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
</contenttemplate>
                        <triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                        </triggers>
                    </asp:UpdatePanel>
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
