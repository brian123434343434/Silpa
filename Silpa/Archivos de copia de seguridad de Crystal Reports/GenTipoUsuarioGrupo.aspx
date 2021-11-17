        <%@ Page Language="C#" MasterPageFile="~/plantillas/SILPASinMenu.master" AutoEventWireup="true" 
        CodeFile="GenTipoUsuarioGrupo.aspx.cs" Inherits="Administracion_Tablasbasicas_GenTipoUsuarioGrupo" 
        Title="Untitled Page" %>

        <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
        <asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
        <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </cc1:ToolkitScriptManager>
        <div class="div-titulo">
            <asp:Label ID="lblTitulo" runat="server" Text="TIPO USUARIO GRUPO" SkinID="titulo_principal_blanco"></asp:Label>
        </div>
         <div class="div-contenido" style="height: 400px">
         <table width="100%">
         <tr>
         <td colspan="4">
         <asp:UpdatePanel ID="updConsultar" runat="server">
         <ContentTemplate>
<asp:Panel id="pnlMaestro" runat="server" Width="100%">
<TABLE width="60%"><TBODY><TR><TD><asp:Label id="lblTipoUsuario" runat="server" Text="Tipo Usuario">
        </asp:Label></TD><TD align=left><asp:DropDownList id="cboTipoUsuario" runat="server" Width="250">
                                                    </asp:DropDownList> </TD><TD align=left></TD></TR><TR><TD>
                                                    <asp:Label id="lblUserGroup" runat="server" Text="Grupo de Usuarios">
                                                    </asp:Label></TD><TD align=left><asp:DropDownList id="cboUserGroup" runat="server" Width="150">
                                                    </asp:DropDownList> </TD></TR><TR><TD style="HEIGHT: 37px"></TD>
                                                    <TD style="HEIGHT: 37px" align=right>
                                                    
                                                    <asp:Button id="btnBuscar" onclick="btnBuscar_Click" runat="server" SkinID="boton_copia" Text="Buscar">
                                                    </asp:Button>
                                                     <asp:Button id="btnAgregar" onclick="btnAgregar_Click" runat="server" SkinID="boton_copia" Text="Agregar" CausesValidation="False">
                                                        </asp:Button>
                                                        <asp:Button id="btnVolver" runat="server" SkinID="boton_copia" Text="Cancelar" PostBackUrl="~/Administracion/Tablasbasicas/TablasBasicas.aspx">
                                                        </asp:Button>
                                                        </TD></TR></TBODY></TABLE> </asp:Panel>
                                                        
                                                         <asp:Panel id="pnlConsultar" runat="server" Width="100%" Visible="true">
                                    <asp:GridView ID="grdDatos" runat="server" Width="100%" OnPageIndexChanging="grdDatos_PageIndexChanging"
                                        DataKeyNames="ID_TIPO_USUARIO,IDUserGroup" OnRowCommand="grdDatos_RowCommand" EmptyDataText="No existen datos registrados en ésta tabla"
                                        AllowSorting="True" AllowPaging="True" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:BoundField DataField="Name" HeaderText="Nombre Grupo de Usuarios"></asp:BoundField>
                                            <asp:BoundField DataField="descripcion_tipo_usuario" HeaderText="Tipo de Usuario"></asp:BoundField>
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
                                
                                 <asp:Panel id="pnlEditar" runat="server" Width="100%" Visible="false">
                                    <table width="100%">
                                        <tbody>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lblTipoUsuarioEdit" runat="server" Text="Tipo Usuario"></asp:Label></td>
                                                <td style="width: 25%" align="left">
                                                     <asp:DropDownList id="cboTipoUsuarioEdit" runat="server" Width="150">
                                                    </asp:DropDownList>
                                                    </td>
                                            </tr>
                                          <tr>
                                                <td align="left">
                                                    <asp:Label ID="lblUserGroupEdit" runat="server" Text="Grupo de Usuarios"></asp:Label></td>
                                                <td style="width: 25%" align="left">
                                                     <asp:DropDownList id="cboUserGroupEdit" runat="server" Width="150">
                                                    </asp:DropDownList>
                                                    </td>
                                                    <asp:Label ID="lblIdTU" runat="server" Visible="False"></asp:Label></td>
                                                    <asp:Label ID="lblIdUG" runat="server" Visible="False"></asp:Label></td>
                                                    
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
                                    <asp:Panel id="pnlNuevo" runat="server" Visible="false">
                                    <TABLE>
                                    <TBODY>
                                    <TR>
                                    <TD style="WIDTH: 48px" align=left><asp:Label id="lblTipoUsuarioNvo" runat="server" Text="Tipo Usuario">
                                    </asp:Label></TD><TD style="WIDTH: 25%" align=left>
                                    <asp:DropDownList id="cboTipoUsuarioNvo" runat="server" Width="100%">
                                                </asp:DropDownList>
                                    </TD></TR><TR><TD style="WIDTH: 48px" align=left>
                                    <asp:Label id="lblUserGroupNvo" runat="server" Text="Grupo de Usuarios"></asp:Label>
                                    </TD><TD style="WIDTH: 25%" align=left>
                                    <asp:DropDownList id="cboUserGroupNvo" runat="server" Width="100%">
                                                </asp:DropDownList>
                                                </TD>
                                                </TR>
                                                <TR>
                                                <TD align=right colSpan=4>&nbsp;
                                                <asp:Button id="btnRegistrar" onclick="btnRegistrar_Click" runat="server" SkinID="boton_copia" Text="Aceptar"></asp:Button>
                                                 <asp:Button id="btnCancelarReg" onclick="btnCancelarReg_Click" runat="server" SkinID="boton_copia" Text="Cancelar" CausesValidation="False">
                                                </asp:Button>
                                                 </TD>
                                                 </TR>
                                                 </TBODY>
                                                 </TABLE>
                                                 </asp:Panel> 
                                    
                               <asp:Label id="lblMensajeError" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label> 
</ContentTemplate>
         
         </asp:UpdatePanel>
         </td>
         </tr>
        </table>
         </div>
        </asp:Content>




