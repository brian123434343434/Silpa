<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="NotEstadoNotificacion.aspx.cs" Inherits="Administracion_Tablasbasicas_NotEstadoNotificacion" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script language="javascript" type="text/javascript">
<!--

function TABLE1_onclick() {

}

// -->
</script>

<cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</cc1:ToolkitScriptManager>
<div class = "div-titulo">
        <asp:Label ID="lblTitulo" runat="server" Text="ESTADOS DE NOTIFICACION" SkinID ="titulo_principal_blanco"></asp:Label>
</div>
<div>
<asp:UpdatePanel ID="updConsultar" runat="server">
    <ContentTemplate>
<table width="100%">
<tr>
<td colspan="4" style="text-align:left;">
    
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
 </td>
<tr>
<td>
    <asp:Panel ID="pnlConsultar" runat="server" Visible="true" Width="100%">
        <asp:GridView ID="grdDatos" runat="server"         
         AutoGenerateColumns="False"         
         Width="100%" 
         AllowPaging="True" 
         AllowSorting="True" EmptyDataText="No existen datos registrados en ésta tabla" 
         OnRowCommand="grdDatos_RowCommand" 
         DataKeyNames="ID_ESTADO,ESTADO_ACTIVO,DIAS_VENCIMIENTO,ESTADO_PDI,MOSTRAR_INFO,ENVIA_CORREO,MENSAJE_CORREO,ES_PUBLICO" 
         OnPageIndexChanging="grdDatos_PageIndexChanging">
            <Columns>
                <asp:BoundField DataField="ESTADO" HeaderText="Nombre" />
                <asp:BoundField DataField="DESCRIPCION_MOSTRAR" HeaderText="Descripción" />
                <asp:BoundField DataField="DESCRIPCION" HeaderText="Estado" />
                <asp:BoundField DataField="DIAS_VENCIMIENTO" HeaderText="Dias vencimiento" />
                <asp:BoundField DataField="ESTADO_PDI" HeaderText="Estado PDI" Visible="False" />
                <asp:CheckBoxField DataField="ESTADO_PDI" HeaderText="Estado PDI" />
                <asp:CheckBoxField DataField="MOSTRAR_INFO" HeaderText="No Mostrar Info. Notificación" />
                <asp:CheckBoxField DataField="ENVIA_CORREO" HeaderText="Envía Correo" />
                <asp:CheckBoxField DataField="ES_PUBLICO" HeaderText="Es Público" />
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
        </asp:Panel>
</td>
</tr>
<tr>
    <td>
        <asp:Panel ID="pnlEditar" runat="server" Visible="false" Width="100%">
        <table>
        <tr>
            <td style="width:69px" align="left">
                <asp:Label ID="lblNombre" runat="server" Text="Nombre"></asp:Label>
            </td>
            <td  style="width:25%" align="left">
                <asp:TextBox ID="txtNombre" runat="server" MaxLength="50"></asp:TextBox>                
            </td>
            <td></td>
            <td>
                <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label></td>
        </tr>
        <tr>
            <td style="width:69px" align="left">
                <asp:Label ID="Label1" runat="server" Text="Descripción"></asp:Label>
            </td>
            <td  style="width:25%" align="left">
                <asp:TextBox ID="txtDescripcionMostrar" runat="server"  MaxLength="50"></asp:TextBox>                
            </td>
            <td></td>
            <td></td>
        </tr>
        <tr>
        <td style="width: 69px">
            <asp:CheckBox ID="chkEstado" Text="Estado" runat="server" />
        </td>
        
        <td style="width:69px" align="left">
           <asp:Label ID="lblDiasVencimientoEdit" runat="server" Text="Dias Vencimiento:"></asp:Label>
        </td>

        <td style="width: 69px">
            <asp:TextBox ID="txtDiasVencimientoEdit" Text="" runat="server" />
        </td>
        
        
        <td style="width: 69px">
            <asp:CheckBox ID="ChkEstadoPdI" Text="EstadoPDI" runat="server" />
        </td>
        
        </tr>
        <tr>
            <td style="width: 69px">
            <asp:CheckBox ID="chkMostrarInfo" Text="Mostrar Info. Notificación" runat="server" />
        </td>
        
        <td style="width:69px" align="left">
            <asp:CheckBox ID="chkEnviaCorreo" runat="server" Text="Envía Correo" />
        </td>

        <td style="width: 69px">
            <asp:CheckBox ID="chkEsPublico" runat="server" Text="Es Público" />
            </td>
        </tr>
        <tr>
            <td>
                Mensaje para Correo:
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtMensajeCorreo" runat="server" ></asp:TextBox>
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
    </td>
</tr>
<tr>
    <td>
        <asp:Panel ID="pnlNuevo" runat="server" Visible="false">
        <table>
        <tr>
            <td style="width:69px" align="left">
                <asp:Label ID="lblDescripcion_Nvo" runat="server" Text="Nombre"></asp:Label>
            </td>
            <td  style="width:25%" align="left">
                <asp:TextBox ID="txtDescripcion_Nvo" runat="server" MaxLength="50"></asp:TextBox>                
            </td>
            <td></td>
            <td></td>               
        </tr>
        <tr>
            <td style="width:69px" align="left">
                <asp:Label ID="Label2" runat="server" Text="Descripción"></asp:Label>
            </td>
            <td  style="width:25%" align="left">
                <asp:TextBox ID="txtDescripcionMostrar_Nvo" runat="server" MaxLength="50"></asp:TextBox>                
            </td>
            <td></td>
            <td></td>
        </tr>
        <tr>
        <td style="width: 69px">
            <asp:CheckBox ID="chkEstado_Nvo" Text="Estado" runat="server" />
        </td>
        <td style="width: 69px">
            <asp:Label ID="lblDiasVenceNvo" Text="Dias Vencimiento" runat="server" />
            <asp:TextBox ID="txtDiasVencimiento_Nvo" Text="" runat="server" />
        </td>
        <td style="width: 69px">
            <asp:CheckBox ID="ChkEstadoPdI_Nvo" Text="Estado PDI" runat="server" />
        </td>
        </tr>
        <tr>
            <td style="width: 69px">
            <asp:CheckBox ID="chkMostrarInfo_Nvo" Text="Mostrar Info. Notificación" 
                    runat="server" />
        </td>
        
        <td style="width:69px" align="left">
            <asp:CheckBox ID="chkEnviaCorreo_Nvo" runat="server" Text="Envía Correo" />
        </td>

        <td style="width: 69px">
            <asp:CheckBox ID="chkEsPublico_Nvo" runat="server" Text="Es Público" />
            </td>
        </tr>
        <tr>
            <td>
                Mensaje para Correo:
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtMensajeCorreo_Nvo" runat="server" ></asp:TextBox>
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
    </td>
</tr>
 </table>        
<asp:Label ID="lblMensajeError" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
</ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
    </Triggers>    
</asp:UpdatePanel>

</div>
</asp:Content>

