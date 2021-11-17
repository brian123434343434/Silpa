<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="VAB_Validaciones.aspx.cs" Inherits="Administracion_Tablasbasicas_VAB_Validaciones" ValidateRequest="false"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <div class="div-titulo">
        <asp:Label ID="lblTitulo" runat="server" Text="VALIDACION POR SENTENCIA" SkinID="titulo_principal_blanco"></asp:Label>
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
         DataKeyNames="ID" 
         OnPageIndexChanging="grdDatos_PageIndexChanging">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" />
                <asp:BoundField DataField="DESCRIPCION" HeaderText="Descripción" />
                <asp:BoundField DataField="SENTENCIA" HeaderText="Sentencia" />
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
        </asp:Panel> <asp:Panel id="pnlEditar" runat="server" Width="100%" Visible="false"><TABLE width="100%"><TBODY><TR><TD style="WIDTH: 10%" align=left><asp:Label id="lblDescripcion" runat="server" Text="Descripción"></asp:Label> </TD><TD style="WIDTH: 25%" align=left><asp:TextBox id="txtDescripcion" runat="server" Width="255px"></asp:TextBox> </TD><TD></TD><TD><asp:Label id="lblID" runat="server" Visible="False"></asp:Label> </TD></TR><TR><TD style="WIDTH: 10%" align=left><asp:Label id="lblSentencia" runat="server" Text="Sentencia"></asp:Label></TD><TD style="WIDTH: 25%" align=left><asp:TextBox id="txtSentencia" runat="server" Width="255px"></asp:TextBox></TD><TD></TD><TD></TD></TR><TR><TD align=right colSpan=4><asp:Button id="btnActualizar" onclick="btnActualizar_Click" runat="server" SkinID="boton_copia" Text="Aceptar"></asp:Button> <asp:Button id="btnCancelar" onclick="btnCancelar_Click" runat="server" SkinID="boton_copia" Text="Cancelar"></asp:Button> </TD></TR></TBODY></TABLE></asp:Panel> <asp:Panel id="pnlNuevo" runat="server" Visible="false" Width="302px"><TABLE width="100%"><TBODY><TR><TD style="WIDTH: 10%" align=left><asp:Label id="lblDescripcion_Nvo" runat="server" Text="Descripción"></asp:Label> </TD><TD style="WIDTH: 85%" align=left><asp:TextBox id="txtDescripcion_Nvo" runat="server" Width="259px"></asp:TextBox> </TD><TD></TD><TD></TD></TR><TR><TD style="WIDTH: 10%" align=left><asp:Label id="lblSentencia_Nvo" runat="server" Text="Sentencia"></asp:Label></TD><TD style="WIDTH: 85%" align=left><asp:TextBox id="txtSentencia_Nvo" runat="server" Width="258px"></asp:TextBox></TD><TD></TD><TD></TD></TR><TR><TD align=right colSpan=4>&nbsp;<asp:Button id="btnRegistrar" onclick="btnRegistrar_Click" runat="server" SkinID="boton_copia" Text="Aceptar"></asp:Button> <asp:Button id="btnCancelarReg" onclick="btnCancelarReg_Click" runat="server" SkinID="boton_copia" Text="Cancelar"></asp:Button> </TD></TR></TBODY></TABLE></asp:Panel> 
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
