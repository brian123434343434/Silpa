<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="WSB_Servicios.aspx.cs" Inherits="Administracion_Tablasbasicas_WSB_Servicios" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <div class="div-titulo">
        <asp:Label ID="lblTitulo" runat="server" Text="SERVICIOS WEB" SkinID="titulo_principal_blanco"></asp:Label>
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
            <asp:Button id="btnVolver" runat="server" SkinID="boton_copia" Text="Cancelar" PostBackUrl="~/Administracion/Tablasbasicas/TablasBasicas.aspx"></asp:Button>
        </td>
    </tr>
    </table>
    </asp:Panel> <asp:Panel id="pnlConsultar" runat="server" Width="100%" Visible="true">
        <asp:GridView ID="grdDatos" runat="server"         
         AutoGenerateColumns="False"         
         Width="100%" 
         AllowPaging="True" 
         AllowSorting="True" EmptyDataText="No existen datos registrados en ésta tabla" 
         OnRowCommand="grdDatos_RowCommand" 
         DataKeyNames="WSB_ID" 
         OnPageIndexChanging="grdDatos_PageIndexChanging">
            <Columns>
                <asp:BoundField DataField="WSB_ID" HeaderText="ID Servicio" />
                <asp:BoundField DataField="WSB_NOMBRE_SERVICIO" HeaderText="Nombre Servicio" />
                <asp:BoundField DataField="WSB_URL" HeaderText="URL" />
                <asp:BoundField DataField="WSB_ACTIVO" HeaderText="Activo" />
                <asp:BoundField DataField="WSB_PRIORIDAD" HeaderText="Prioridad" />
                <asp:TemplateField HeaderText="Editar">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkModificar" CommandName="Modificar" runat="server"  CommandArgument='<%# Container.DataItemIndex %>'>Modificar Registro</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Eliminar">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkEliminar" CommandName="Eliminar" runat="server"  CommandArgument='<%# Container.DataItemIndex %>' OnClientClick="return confirm('Esta seguro de Eliminar este registro?')">Eliminar Registro</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        </asp:Panel> <asp:Panel id="pnlEditar" runat="server" Width="100%" Visible="false"><TABLE width="100%"><TBODY><TR><TD style="WIDTH: 10%" align=left><asp:Label id="lblNombreServicio" runat="server" Text="Nombre Servicio"></asp:Label> </TD><TD style="WIDTH: 25%" align=left><asp:TextBox id="txtNombreServicio" runat="server"></asp:TextBox> </TD><TD></TD><TD><asp:Label id="lblID" runat="server" Visible="False"></asp:Label> </TD></TR><TR><TD style="WIDTH: 10%" align=left><asp:Label id="lblURL" runat="server" Text="URL"></asp:Label></TD><TD style="WIDTH: 25%" align=left><asp:TextBox id="txtURL" runat="server"></asp:TextBox></TD><TD></TD><TD></TD></TR><TR><TD style="WIDTH: 10%" align=left><asp:Label id="lblActivo" runat="server" Text="Activo" __designer:wfdid="w1"></asp:Label></TD><TD style="WIDTH: 25%" align=left><asp:CheckBox id="chkActivo" runat="server" __designer:wfdid="w2"></asp:CheckBox></TD><TD></TD><TD></TD></TR><TR><TD style="WIDTH: 10%" align=left><asp:Label id="lblPrioridad" runat="server" Text="Prioridad" __designer:wfdid="w3"></asp:Label></TD><TD style="WIDTH: 25%" align=left><asp:TextBox id="txtPrioridad" runat="server" __designer:wfdid="w4"></asp:TextBox></TD><TD></TD><TD></TD></TR><TR><TD align=right colSpan=4><asp:Button id="btnActualizar" onclick="btnActualizar_Click" runat="server" SkinID="boton_copia" Text="Aceptar"></asp:Button> <asp:Button id="btnCancelar" onclick="btnCancelar_Click" runat="server" SkinID="boton_copia" Text="Cancelar"></asp:Button> </TD></TR></TBODY></TABLE></asp:Panel> <asp:Panel id="pnlNuevo" runat="server" Visible="false"><TABLE><TBODY><TR><TD style="WIDTH: 10%" align=left><asp:Label id="lblNombreServicio_Nvo" runat="server" Text="Nombre Servicio"></asp:Label> </TD><TD style="WIDTH: 25%" align=left><asp:TextBox id="txtNombreServicio_Nvo" runat="server"></asp:TextBox> </TD><TD></TD><TD></TD></TR><TR><TD style="WIDTH: 10%" align=left><asp:Label id="lblURL_Nvo" runat="server" Text="URL"></asp:Label></TD><TD style="WIDTH: 25%" align=left><asp:TextBox id="txtURL_Nvo" runat="server"></asp:TextBox></TD><TD></TD><TD></TD></TR><TR><TD style="WIDTH: 10%" align=left><asp:Label id="lblActivo_Nvo" runat="server" Text="Activo" __designer:wfdid="w5"></asp:Label></TD><TD style="WIDTH: 25%" align=left><asp:CheckBox id="chkActivo_Nvo" runat="server" __designer:wfdid="w6"></asp:CheckBox></TD><TD></TD><TD></TD></TR><TR><TD style="WIDTH: 10%" align=left><asp:Label id="lblPrioridad_Nvo" runat="server" Text="Prioridad" __designer:wfdid="w7"></asp:Label></TD><TD style="WIDTH: 25%" align=left><asp:TextBox id="txtPrioridad_Nvo" runat="server" __designer:wfdid="w8"></asp:TextBox></TD><TD></TD><TD></TD></TR><TR><TD align=right colSpan=4>&nbsp;<asp:Button id="btnRegistrar" onclick="btnRegistrar_Click" runat="server" SkinID="boton_copia" Text="Aceptar"></asp:Button> <asp:Button id="btnCancelarReg" onclick="btnCancelarReg_Click" runat="server" SkinID="boton_copia" Text="Cancelar"></asp:Button> </TD></TR></TBODY></TABLE></asp:Panel> 
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
