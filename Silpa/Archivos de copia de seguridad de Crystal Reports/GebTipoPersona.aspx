<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="GebTipoPersona.aspx.cs" Inherits="Administracion_Tablasbasicas_GebTipoPersona" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
<div class = "div-titulo">
        <asp:Label ID="lblTitulo" runat="server" Text="TIPO DE USUARIO" SkinID ="titulo_principal_blanco"></asp:Label>
</div>
<div class="div-contenido" style =" height:400px">
<table width="100%">
<tr>
<td colspan="4">
    <asp:UpdatePanel ID="updConsultar" runat="server">
    <ContentTemplate>
    <asp:Panel ID="pnlMaestro" runat="server" Width="100%">
    <table width="60%">
    <tr>
        <td style="width:15%" align="left">
            <asp:Label ID="lblNombreParametro" SkinID="etiqueta_negra" runat="server" Text="Descripción"></asp:Label>
        </td>
        <td style="width:30%" align="left">    
            <asp:TextBox ID="txtNombreParametro" SkinID="texto" runat="server" Width="100%"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td></td>
        <td align="right">
            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" SkinID="boton_copia" OnClick="btnBuscar_Click" />
            <asp:Button ID="btnagregar" runat="server" Text="Agregar" SkinID="boton_copia" OnClick="btnagregar_Click" />
        </td>
    </tr>
    </table>
    </asp:Panel>
     <asp:Panel ID="pnlConsultar" runat="server" Visible="true" Width="100%">
        <asp:GridView ID="grdDatos" runat="server"         
         AutoGenerateColumns="False"         
         Width="100%" 
         AllowPaging="True" 
         AllowSorting="True" EmptyDataText="No existen datos registrados en ésta tabla" OnRowCommand="grdDatos_RowCommand" DataKeyNames="TIPO_PERSONA_ID,TIPO_PERSONA_ACTIVO" OnPageIndexChanging="grdDatos_PageIndexChanging">
            <Columns>
                <asp:BoundField DataField="TIPO_PERSONA_NOMBRE" HeaderText="Descripción" />
                <asp:TemplateField HeaderText="Editar">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkModificar" CommandName="Modificar" runat="server"  CommandArgument='<%# Container.DataItemIndex %>'>Modificar Registro</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Eliminar">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkEliminar" CommandName="Eliminar" runat="server"  CommandArgument='<%# Container.DataItemIndex %>' OnClientClick="return confirm('Seguro que desea eliminar el registro?');" >Eliminar Registro</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        </asp:Panel>
        <asp:Panel ID="pnlEditar" runat="server" Visible="false" Width="100%">
        <table width="100%">
        <tr>
            <td style="width:10%" align="left">
                <asp:Label ID="lblNombre" runat="server" Text="Descripcion"></asp:Label>
            </td>
            <td  style="width:25%" align="left">
                <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>                
            </td>
            <td></td>
            <td>
                <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label></td>
        </tr>
        <tr>
        <td>
            <asp:CheckBox ID="chkEstado" Text="Estado" runat="server" />
        </td>
        </tr>
        <tr>
        <td colspan="4" align="right">
            <asp:Button ID="btnActualizar" runat="server" Text="Aceptar" SkinID="boton_copia" OnClick="btnActualizar_Click" />
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" SkinID="boton_copia" OnClick="btnCancelar_Click" />
        </td>
        </tr>
        </table>
        </asp:Panel>
        <asp:Panel ID="pnlNuevo" runat="server" Visible="false">
        <table>
        <tr>
            <td style="width:10%" align="left">
                <asp:Label ID="lblDescripcion_Nvo" runat="server" Text="Descripcion"></asp:Label>
            </td>
            <td  style="width:25%" align="left">
                <asp:TextBox ID="txtDescripcion_Nvo" runat="server"></asp:TextBox>                
            </td>
            <td></td>
            <td></td>               
        </tr>
        <tr>
        <td>
            <asp:CheckBox ID="chkEstado_Nvo" Text="Estado" runat="server" />
        </td>
        </tr>
        <tr>
        <td colspan="4" align="right">        
            &nbsp;<asp:Button ID="btnRegistrar" runat="server" Text="Aceptar" SkinID="boton_copia" OnClick="btnRegistrar_Click" />
            <asp:Button ID="btnCancelarReg" runat="server" Text="Cancelar" SkinID="boton_copia" OnClick="btnCancelarReg_Click" />
        </td>
        </tr>
        </table>
        </asp:Panel>
        <asp:Label ID="lblMensajeError" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
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

