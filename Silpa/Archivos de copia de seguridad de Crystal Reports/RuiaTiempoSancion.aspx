<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="RuiaTiempoSancion.aspx.cs" Inherits="Administracion_Tablasbasicas_RuiaTiempoSancion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
<div class = "div-titulo">
        <asp:Label ID="lblTitulo" runat="server" Text="TIEMPO PUBLICACION POR TIPO SANCION" SkinID ="titulo_principal_blanco"></asp:Label>
</div>
<div class="div-contenido" style =" height:400px">
<table width="100%">
<tr>
<td colspan="4">
    <asp:UpdatePanel ID="updConsultar" runat="server">
    <ContentTemplate>
<asp:Panel id="pnlMaestro" runat="server" Width="100%"><TABLE width="60%"><TBODY><TR><TD><asp:Label id="lblTipoCon" runat="server" Text="Nombre"></asp:Label></TD><TD align=left>&nbsp; <asp:TextBox id="txtNombreParametro" runat="server" SkinID="texto" Width="100%" __designer:wfdid="w14"></asp:TextBox></TD></TR><TR><TD></TD><TD align=right><asp:Button id="btnBuscar" onclick="btnBuscar_Click" runat="server" SkinID="boton_copia" Text="Buscar"></asp:Button> <asp:Button id="btnAgregar" onclick="btnAgregar_Click" runat="server" SkinID="boton_copia" Text="Agregar" CausesValidation="False"></asp:Button><asp:Button id="btnVolver" runat="server" SkinID="boton_copia" Text="Cancelar" PostBackUrl="~/Administracion/Tablasbasicas/TablasBasicas.aspx"></asp:Button> </TD></TR></TBODY></TABLE></asp:Panel> <asp:Panel id="pnlConsultar" runat="server" Width="100%" Visible="true">
<asp:GridView id="grdDatos" runat="server" Width="100%" OnPageIndexChanging="grdDatos_PageIndexChanging" 
DataKeyNames="OPS_ID_OPCION,OPS_ACTIVO" OnRowCommand="grdDatos_RowCommand" 
EmptyDataText="No existen datos registrados en ésta tabla" AllowSorting="True" AllowPaging="True" AutoGenerateColumns="False"><Columns>
<asp:BoundField DataField="OPS_NOMBRE_OPCION" HeaderText="Nombre"></asp:BoundField>
<asp:BoundField DataField="OPS_DIAS" HeaderText="Tiempo (dias)"></asp:BoundField>
<asp:BoundField DataField="ESTADO" HeaderText="Estado"></asp:BoundField>
<asp:TemplateField HeaderText="Editar"><ItemTemplate>
                        <asp:LinkButton ID="lnkModificar" CommandName="Modificar" runat="server"  CommandArgument='<%# Container.DataItemIndex %>'>Modificar Registro</asp:LinkButton>
                    
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Eliminar"><ItemTemplate>
                        <asp:LinkButton ID="lnkEliminar" CommandName="Eliminar" runat="server"  CommandArgument='<%# Container.DataItemIndex %>'>Eliminar Registro</asp:LinkButton>
                    
</ItemTemplate>
</asp:TemplateField>
</Columns>
</asp:GridView> </asp:Panel> <asp:Panel id="pnlEditar" runat="server" Width="100%" Visible="false"><TABLE width="100%"><TBODY><TR><TD align=left><asp:Label id="lblNombre" runat="server" Text="Nombre"></asp:Label></TD><TD style="WIDTH: 25%" align=left><asp:TextBox id="txtNombre" runat="server" MaxLength="30"></asp:TextBox></TD></TR><TR><TD align=left><asp:Label id="lblTiempo" runat="server" Text="Tiempo (días)"></asp:Label> </TD><TD style="WIDTH: 25%" align=left><asp:TextBox id="txtTiempo" runat="server" __designer:wfdid="w15" MaxLength="9">0</asp:TextBox></TD></TR><TR><TD><asp:Label id="lblEstado" runat="server" Text="Estado"></asp:Label> </TD><TD><asp:CheckBox id="chkEstado" runat="server"></asp:CheckBox> <asp:Label id="lblId" runat="server" Visible="False"></asp:Label></TD></TR><TR><TD align=right colSpan=4><asp:Button id="btnActualizar" onclick="btnActualizar_Click" runat="server" SkinID="boton_copia" Text="Aceptar"></asp:Button> <asp:Button id="btnCancelar" onclick="btnCancelar_Click" runat="server" SkinID="boton_copia" Text="Cancelar" CausesValidation="False"></asp:Button> </TD></TR></TBODY></TABLE></asp:Panel> <asp:Panel id="pnlNuevo" runat="server" Visible="false"><TABLE><TBODY><TR><TD style="WIDTH: 48px" align=left><asp:Label id="lblNombreNvo" runat="server" Text="Nombre" __designer:wfdid="w17"></asp:Label></TD><TD style="WIDTH: 25%" align=left><asp:TextBox id="txtNombreNvo" runat="server" MaxLength="30"></asp:TextBox></TD></TR><TR><TD style="WIDTH: 48px" align=left><asp:Label id="lblTiempoNvo" runat="server" Text="Tiempo (días)"></asp:Label> </TD><TD style="WIDTH: 25%" align=left><asp:TextBox id="txtTiempoNvo" runat="server" __designer:wfdid="w16" MaxLength="9">0</asp:TextBox></TD></TR><TR><TD style="WIDTH: 48px"><asp:Label id="lblEstadoNvo" runat="server" Text="Estado"></asp:Label> </TD><TD><asp:CheckBox id="chkEstadoNvo" runat="server"></asp:CheckBox> </TD></TR><TR><TD align=right colSpan=4>&nbsp;<asp:Button id="btnRegistrar" onclick="btnRegistrar_Click" runat="server" SkinID="boton_copia" Text="Aceptar"></asp:Button> <asp:Button id="btnCancelarReg" onclick="btnCancelarReg_Click" runat="server" SkinID="boton_copia" Text="Cancelar" CausesValidation="False"></asp:Button> </TD></TR></TBODY></TABLE></asp:Panel> 
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
